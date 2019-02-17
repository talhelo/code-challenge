using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebAPI.Models.Flickr;

namespace WebAPI.Models
{
    public class FeedResponse
    {
        public FeedResponse()
        {

        }

        public FeedResponse(FlickrResponse flickrResponse)
        {
            this.title = flickrResponse.title;
            this.published = flickrResponse.published;

            if (flickrResponse.media != null)
                this.media = flickrResponse.media.m;
        }

        public string title { get; set; }
        public string media { get; set; }
        public string published { get; set; } // DateTime
    }
}