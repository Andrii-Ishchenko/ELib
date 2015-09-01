﻿using ELib.BL.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using ELib.Common;
namespace ELib.Web
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
             ELogger loger = ELoggerFactory.GetInstance().GetLogger(GetType().FullName);
            loger.Info("Application Start");           
            AreaRegistration.RegisterAllAreas();
            loger.Info("Areas Registered");
            WebApiConfig.Register(GlobalConfiguration.Configuration);
            loger.Info("WebApiConfig Registered");
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);

            loger.Info("Filters Registered");

            RouteConfig.RegisterRoutes(RouteTable.Routes);
            loger.Info("Routes Registered");
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            loger.Info("Bundles Registered");

            AutoMapperConfig.Configure();
            loger.Info("AutoMapper Registered");
        }
    }
}