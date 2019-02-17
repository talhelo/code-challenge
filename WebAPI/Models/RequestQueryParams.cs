using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebAPI.Models
{
    public class RequestQueryParams
    {
        /// <summary>
        /// search keyword, you can pass multiple values using comman
        /// </summary>
        public string q { get; set; }
        /// <summary>
        /// page index starting from zero
        /// </summary>
        public int index { get; set; } = 0;
        /// <summary>
        /// page size
        /// </summary>
        public int size { get; set; } = 10;
        /// <summary>
        /// refresh the cache, retrieve data from the api not the cache
        /// </summary>
        public bool refresh { get; set; }
    }
}