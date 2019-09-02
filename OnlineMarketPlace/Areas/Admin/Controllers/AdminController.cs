using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace OnlineMarketPlace.Areas.Admin.Controllers
{
    [Area("admin")]

    public class AdminController : Controller
    {

        public IActionResult Dashboard() => View();
    }
}