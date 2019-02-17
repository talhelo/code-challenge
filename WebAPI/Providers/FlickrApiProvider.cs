using Common.Helpers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using WebAPI.Models.Flickr;
using WebAPI.Properties;

namespace WebAPI.Providers
{
    internal class FlickrApiProvider : IFlickrApiProvider
    {
        private IExternalApiRequestHelper externalApiRequestHelper;
        private IExternalApiRequestHelper apiRequestHelper
        {
            get
            {
                return externalApiRequestHelper = new ExternalApiRequestHelper();
            }
            set
            {
                externalApiRequestHelper = value;
            }
        }

        internal FlickrApiProvider()
        {

        }

        public IList<FlickrResponse> Find(string keyword, bool refresh, bool jsonResponse)
        {
            string cacheKey = DataHelper.RemoveSpecialCharacters(keyword);
            string response = "";
            if (!refresh)
            {
                // Cache response, you can use advance caching like Redis
                var cachedResponse = MemoryCacheHelper.Get(cacheKey);
                if (cachedResponse != null)
                    response = cachedResponse.ToString();
            }

            if (string.IsNullOrEmpty(response)) // if cache is empty, call the api
            {
                response = this.apiRequestHelper.Get(Settings.Default.FlickrPublicFeed, this.GetFeedQueryString(keyword, jsonResponse));
                if (!string.IsNullOrEmpty(response))
                    MemoryCacheHelper.Set(cacheKey, response);
            }

            return ParseJsonResponse(response);
        }

        private string GetFeedQueryString(string keyword, bool jsonResponse)
        {
            if (!string.IsNullOrEmpty(keyword))
            {
                StringBuilder str = new StringBuilder();

                if (keyword.Contains(","))
                    str.Append(this.ParseMultipleKeyword(keyword));
                else
                    str.Append(this.ParseSingleKeyword(keyword));

                if (jsonResponse)
                    str.Append("&format=json&nojsoncallback=?");

                return str.ToString();
            }

            if (jsonResponse)
                return "?format=json&nojsoncallback=?";

            return keyword;
        }

        private string ParseSingleKeyword(string keyword)
        {
            StringBuilder str = new StringBuilder();

            // Note: based on my understanding of flicker api, user id contains @N. could be wrong
            if (keyword.Contains("@N"))
            {
                str.Append("?id=");
                str.Append(HttpUtility.UrlEncode(keyword));
            }
            else
            {
                str.Append("?tags=");
                str.Append(HttpUtility.UrlEncode(keyword));
                str.Append("&tagmode=any");
            }

            return str.ToString();
        }

        private string ParseMultipleKeyword(string keyword)
        {
            StringBuilder str = new StringBuilder();
            var keywords = keyword.Split(',');

            // Note: based on my understanding of flicker api, user id contains @N. could be wrong
            if (keywords.NotNullOrEmpty())
            {
                if (keyword.Contains("@N"))
                {
                    var ids = keywords.Where(x => x.Contains("@N")).ToList();
                    if (ids.NotNullOrEmpty())
                    {
                        str.Append("?ids=");
                        str.Append(HttpUtility.UrlEncode(string.Join(",", ids)));
                    }
                }

                var tags = keywords.Where(x => !x.Contains("@N")).ToList();
                if (tags.NotNullOrEmpty())
                {
                    if (str.Length != 0) // if not empty, ids already added
                        str.Append("&tags=");
                    else
                        str.Append("?tags=");

                    str.Append(HttpUtility.UrlEncode(string.Join(",", tags)));
                    str.Append("&tagmode=any");
                }
            }

            return str.ToString();
        }

        private IList<FlickrResponse> ParseJsonResponse(string response)
        {
            if (string.IsNullOrEmpty(response))
                return null;

            if (response.StartsWith("["))
            {
                var result = JsonConvert.DeserializeObject<FlickrResponse[]>(response);
                return result;
            }

            if (response.Contains("items\": ["))
            {
                var parsed = JObject.Parse(response);
                foreach (var kvp in parsed.Cast<KeyValuePair<string, JToken>>().ToList())
                {
                    if (kvp.Key.Equals("items", StringComparison.OrdinalIgnoreCase))
                    {
                        var value = kvp.Value.ToString();
                        var result = JsonConvert.DeserializeObject<FlickrResponse[]>(value);
                        return result;
                    }
                    //kvp.Key
                    //kvp.Value
                }
                //var items = parsed.SelectToken("items").Value<string>();
            }

            return null;
        }
    }
}