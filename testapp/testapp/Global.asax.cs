using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using testapp.Controllers;
namespace testapp
{
    public class MvcApplication : System.Web.HttpApplication
    {
        public static IMapper Mapper { get; private set; }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<Mappers>(); // Replace 'AutoMapper' with the correct profile class name  
            });
                                
            Mapper = config.CreateMapper();
        }
    }
}
