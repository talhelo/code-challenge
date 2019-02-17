using Common.Constants;
using Common.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;
using WebAPI.Models;
using WebAPI.Providers;

namespace WebAPI.Controllers
{
    public class FeedsController : BaseController
    {
        private IFlickrApiProvider flikerApiProvider;
        private IFlickrApiProvider fliker
        {
            get
            {
                return flikerApiProvider = new FlickrApiProvider();
            }
            set
            {
                flikerApiProvider = value;
            }
        }

        [HttpGet]
        public IHttpActionResult Get([FromUri] RequestQueryParams query)
        {
            var flikerResponse = fliker.Find(query.q, query.refresh, true);
            if (flikerResponse == null)
                return base.GetCustomResponse(HttpStatusCode.NoContent, MessageConstant.NoContent);

            var response = flikerResponse
                .Skip(query.index * query.size)
                .Take(query.size)
                .Select(x => new FeedResponse(x)).ToList();
            
            return Ok(response);
        }

        //[HttpPost]
        //public IHttpActionResult Post(Model model)
        //{
        //    return Ok();
        //}

        //[HttpPut]
        //public IHttpActionResult Put(string id, Model model)
        //{
        //    return Ok();
        //}

        //[HttpDelete]
        //public IHttpActionResult Delete(string id)
        //{
        //    return Ok();
        //}
    }
}
