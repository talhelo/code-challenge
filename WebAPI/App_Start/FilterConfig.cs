using System.Web;
using System.Web.Http;
using WebAPI.Filters;

namespace WebAPI
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(HttpConfiguration config)
        {
            config.Filters.Add(new GlobalExceptionFilterAttribute());
        }
    }
}
