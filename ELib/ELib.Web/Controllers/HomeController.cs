﻿using ELib.BL.Services.Concrete;
using ELib.DAL.Infrastructure.Abstract;
using ELib.Web.ApiControllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ELib.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult FilesTest()
        {
            return View();
        }

        public ActionResult AccountTest()
        {
            return View();
        }
    }
}
