﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace OnlineMarketPlace.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index() //Home Page
        {
            return View();
        }

        [Route("[action]")]
        public ViewResult PageNotFound() => View(); //Independant layout
    }
}