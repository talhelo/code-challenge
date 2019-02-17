using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAPI.Models
{
    public class FeedResponse
    {
        public string title { get; set; }
        public string media { get; set; }
        public string published { get; set; } // DateTime
    }
}