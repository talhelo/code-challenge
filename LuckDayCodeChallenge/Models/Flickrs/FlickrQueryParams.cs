using Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LuckDayCodeChallenge.Models.Flickrs
{
    public class FlickrQueryParams : BaseQueryParams
    {
        public string id { get; set; }
        public string ids { get; set; }
        public string tags { get; set; }
        public string tagmode { get; set; }
        public string format { get; set; }
        public string lang { get; set; }
    }
}