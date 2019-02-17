using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http.Filters;

namespace WebAPI.Filters
{
    public class GlobalExceptionFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext context)
        {
            if (context.Exception is Exception)
            {
                this.Log(context.Exception);
                context.Response = new HttpResponseMessage(System.Net.HttpStatusCode.InternalServerError);
            }
        }

        private void Log()
        {
            // TODO log here
        }
    }
}