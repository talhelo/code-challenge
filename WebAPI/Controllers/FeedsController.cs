using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
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
            var flikerResponse = fliker.Find(query.q);
            var response = Mapper.Map<FeedResponse>(flikerResponse);
            // TODO cache the response
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
