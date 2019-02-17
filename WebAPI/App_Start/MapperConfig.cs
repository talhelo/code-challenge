using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebAPI.Models;
using WebAPI.Models.Flickr;

namespace WebAPI
{
    public class MapperConfig
    {
        public static void Register()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<FeedResponse, FlickrResponse>().ReverseMap();
            });
        }
    }
}