using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace WebAPI.Controllers
{
    public class BaseController : ApiController
    {
        protected System.Web.Http.Results.ResponseMessageResult GetCustomResponse(HttpStatusCode errorCode, string message)
        {
            return new System.Web.Http.Results.ResponseMessageResult(Request.CreateErrorResponse(errorCode, message));
        }
    }
}