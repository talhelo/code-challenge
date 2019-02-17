using Common.Enums;
using Common.Providers.HTTP;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Helpers
{
    public class ExternalApiRequestHelper : IExternalApiRequestHelper
    {
        public IList<T> Get<T>(string url, string query = "")
        {
            try
            {
                var client = LoadHttpProvider(url, HttpVerb.GET);
                string response = client.MakeRequest(query);
                var result = JsonConvert.DeserializeObject<T[]>(response);
                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }

        private HttpProvider LoadHttpProvider(string url, HttpVerb verb, string requestDate = "")
        {
            var client = new HttpProvider(url, verb);
            if (verb == HttpVerb.POST || verb == HttpVerb.PUT)
                client.PostData = requestDate;

            return client;
        }
    }
}
