using Common.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
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


        public IList<FlickrResponse> Find(string keyword)
        {
            var feedsUrl = Settings.Default.FlickrPublicFeed;
            var response = this.apiRequestHelper.Get<FlickrResponse>(feedsUrl, this.GetFeedQueryString(keyword));
            return response;
        }

        private string GetFeedQueryString(string keyword)
        {
            var query = keyword;
            if (string.IsNullOrEmpty(keyword))
            {
                if (keyword.Contains(","))
                {
                    query = $"?ids={keyword}&tags={keyword}&tagmode=any";
                }
                else
                    query = $"?id={keyword}";
            }

            return HttpUtility.UrlEncode(query);
        }
    }
}