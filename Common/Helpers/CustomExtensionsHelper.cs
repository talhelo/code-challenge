using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Helpers
{
    public static class CustomExtensions
    {
        /// <summary>
        /// check list if not null and not empty
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static bool NotNullOrEmpty<T>(this IEnumerable<T> source)
        {
            return source?.Any() == true; // C# 6
            //return source != null && source.Any(); // old version
        }
    }
}
