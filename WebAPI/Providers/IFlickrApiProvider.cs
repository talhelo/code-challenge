using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebAPI.Models.Flickr;

namespace WebAPI.Providers
{
    public interface IFlickrApiProvider
    {
        IList<FlickrResponse> Find(string keyword);
    }
}