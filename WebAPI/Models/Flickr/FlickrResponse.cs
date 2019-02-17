using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAPI.Models.Flickr
{
    public class FlickrResponse
    {
        public string title { get; set; }
        public string link { get; set; }
        public FlickrMedia media { get; set; }
        public string date_taken { get; set; } // DateTime
        public string description { get; set; }
        public string published { get; set; } // DateTime
        public string author { get; set; }
        public string author_id { get; set; }
        public string tags { get; set; }
    }

    public class FlickrMedia
    {
        public string m { get; set; }
    }
}