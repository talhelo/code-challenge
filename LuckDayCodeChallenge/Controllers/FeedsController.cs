using LuckDayCodeChallenge.Models.Flickrs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace LuckDayCodeChallenge.Controllers
{
    public class FeedsController : BaseController
    {
        [HttpGet]
        public IHttpActionResult Get([FromUri] FlickrQueryParams query)
        {
            return Ok();
        }

        [HttpPost]
        public IHttpActionResult Post()
        {
            return Ok();
        }

        [HttpPut]
        public IHttpActionResult Put()
        {
            return Ok();
        }

        [HttpDelete]
        public IHttpActionResult Delete()
        {
            return Ok();
        }
    }
}
