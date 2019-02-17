using Common.Enums;
using Common.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Common.Providers.HTTP
{
    /// <summary>
    ///Usage
    ///Basic call
    ///
    ///string endPoint = @"http:\\myRestService.com\api\";
    ///var client = new RestClient(endPoint);
    ///var json = client.MakeRequest();
    ///If you want to append parameters you can pass them into the make request method like so.
    ///
    ///var json = client.MakeRequest("?param=0");
    ///To set the HttpVerb(i.e.GET, POST, PUT, or DELETE), simply use the provided HttpVerb enumeration.Here is an expample of making a POST request:
    ///
    ///var client = new RestClient(endpoint: endPoint,
    ///method: HttpVerb.POST,
    ///postData: "{'someValueToPost': 'The Value being Posted'}");
    ///You can also just assign the values in line if you want:
    ///
    ///var client = new RestClient();
    ///client.EndPoint = @"http:\\myRestService.com\api\"; ;
    ///client.Method = HttpVerb.POST;
    ///client.PostData = "{postData: value}";
    ///var json = client.MakeRequest();
    /// </summary>
    public class HttpProvider
    {
        private const int AsyncRequestTimeout = 20000;

        public string EndPoint { get; set; }
        public HttpVerb Method { get; set; }
        public string ContentType { get; set; }
        public string PostData { get; set; }
        private Dictionary<string, string> Headers { get; set; }

        public HttpProvider()
        {
            EndPoint = "";
            Method = HttpVerb.GET;
            ContentType = ContentTypeConstant.ApplicationJson;
            PostData = "";
        }
        public HttpProvider(string endpoint)
        {
            EndPoint = endpoint;
            Method = HttpVerb.GET;
            ContentType = ContentTypeConstant.ApplicationJson;
            PostData = "";
        }
        public HttpProvider(HttpVerb endpoint)
        {
            EndPoint = "";
            Method = endpoint;
            ContentType = ContentTypeConstant.ApplicationJson;
            PostData = "";
        }
        public HttpProvider(string endpoint, HttpVerb method)
        {
            EndPoint = endpoint;
            Method = method;
            ContentType = ContentTypeConstant.ApplicationJson;
            PostData = "";
        }
        public HttpProvider(HttpVerb method, string contentType)
        {
            EndPoint = "";
            Method = method;
            ContentType = contentType;
            PostData = "";
        }
        public HttpProvider(string endpoint, HttpVerb method, string postData)
        {
            EndPoint = endpoint;
            Method = method;
            ContentType = ContentTypeConstant.ApplicationJson;
            PostData = postData;
        }
        public HttpProvider(string endpoint, HttpVerb method, string contentType, string postData)
        {
            EndPoint = endpoint;
            Method = method;
            ContentType = contentType;
            PostData = postData;
        }

        public void SetHeaders(string name, string value)
        {
            if (!string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(value))
            {
                if (Headers == null)
                    Headers = new Dictionary<string, string>();

                Headers.Add(name, value);
            }
        }

        public void SetHeaders(Dictionary<string, string> headers)
        {
            Headers = headers;
        }

        public string MakeRequest()
        {
            return MakeRequest("");
        }

        public string MakeRequest(string parameters)
        {
            try
            {
                var request = GetWebRequest(parameters);

                using (var response = (HttpWebResponse)request.GetResponse())
                {
                    var responseValue = string.Empty;

                    if (response.StatusCode != HttpStatusCode.OK)
                    {
                        var message = String.Format("Request failed. Received HTTP {0}", response.StatusCode);
                        throw new ApplicationException(message);
                    }

                    // grab the response
                    using (var responseStream = response.GetResponseStream())
                    {
                        if (responseStream != null)
                            using (var reader = new StreamReader(responseStream))
                            {
                                responseValue = reader.ReadToEnd();
                            }
                    }

                    return responseValue;
                }
            }
            catch (Exception ex)
            {
                // TODO log here
                return string.Empty;
            }
        }

        public async Task<string> MakeAsyncRequest()
        {
            return await MakeAsyncRequest("");
        }

        public async Task<string> MakeAsyncRequest(string parameters)
        {
            try
            {
                var request = GetWebRequest(parameters, AsyncRequestTimeout);

                Task<WebResponse> task = Task.Factory.FromAsync(
                     request.BeginGetResponse,
                     asyncResult => request.EndGetResponse(asyncResult),
                     (object)null);

                return await task.ContinueWith(t => ReadStreamFromResponse(t.Result));

            }
            catch (Exception ex)
            {
                // TODO log here
                return string.Empty;
            }
        }

        private static string ReadStreamFromResponse(WebResponse response)
        {
            using (Stream responseStream = response.GetResponseStream())
            using (StreamReader sr = new StreamReader(responseStream))
            {
                //Need to return this response 
                string strContent = sr.ReadToEnd();
                return strContent;
            }
        }

        private HttpWebRequest GetWebRequest(string parameters, int? timeout = null)
        {
            var request = (HttpWebRequest)WebRequest.Create(EndPoint + parameters);
            request.Method = Method.ToString();
            request.ContentLength = 0;
            request.ContentType = ContentType;
            //request.KeepAlive = false;

            if (timeout.HasValue)
            {
                request.Timeout = timeout.Value;
                request.Proxy = null;
            }

            // set headers
            if (Headers.NotNullOrEmpty())
                foreach (var item in Headers)
                    request.Headers.Add(item.Key, item.Value);


            if (!string.IsNullOrEmpty(PostData) && Method == HttpVerb.POST)
            {
                var encoding = new UTF8Encoding();
                var bytes = Encoding.GetEncoding("iso-8859-1").GetBytes(PostData);
                request.ContentLength = bytes.Length;

                using (var writeStream = request.GetRequestStream())
                {
                    writeStream.Write(bytes, 0, bytes.Length);
                }
            }

            return request;
        }

        /// <summary>
        /// Creates a multipart/form-data boundary.
        /// </summary>
        /// <returns>
        /// A dynamically generated form boundary for use in posting multipart/form-data requests.
        /// </returns>
        private static string CreateFormDataBoundary()
        {
            return "---------------------------" + DateTime.Now.Ticks.ToString("x");
        }
    }

    public class ContentTypeConstant
    {
        public const string ApplicationJson = "application/json";
    }
}
