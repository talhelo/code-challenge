using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAPI.Models.Flickr
{
    public class FlickrQueryParams
    {
        public FlickrQueryParams()
        {

        }

        public FlickrQueryParams(string query)
        {
            this.id = query;
            this.ids = query;
            this.tags = query;
            this.tagmode = query;
            this.format = query;
            this.lang = query;
        }

        public string id { get; set; }
        public string ids { get; set; }
        public string tags { get; set; }
        public string tagmode { get; set; }
        public string format { get; set; }
        public string lang { get; set; }
    }
}