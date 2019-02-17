using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAPI.Models
{
    public class RequestQueryParams
    {
        public string q { get; set; }
        public int index { get; set; }
        public int size { get; set; }
    }
}