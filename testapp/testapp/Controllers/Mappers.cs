using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using testapp.Models;

namespace testapp.Controllers
{
    public class Mappers:Profile
    {
        public Mappers()
        {
            CreateMap<Todo, TodoViewModel>().ReverseMap();
        }
    }
}