using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using OnlineMarketPlace.ClassLibraries;
using OnlineMarketPlace.ClassLibraries.NotificationHandler;

namespace OnlineMarketPlace.Areas.Admin.Controllers
{
    [Area("admin")]
    [Authorize(Roles = "Admin,SuperVisor")]
    public class AdminController : Controller
    {
        private readonly IHostingEnvironment _hostingEnvironment; //returns the hosting environment data
        string contentRootPath;
        private IConfiguration _configuration;

        public AdminController(IHostingEnvironment hostingEnvironment,
            IConfiguration configuration)
        {
            _hostingEnvironment = hostingEnvironment;
            contentRootPath = _hostingEnvironment.ContentRootPath;//returns the root path of the website
            _configuration = configuration;
        }

        public IActionResult Dashboard(string notification)
        {
            if (notification != null)
            {
                ViewData["nvm"] = NotificationHandler.DeserializeMessage(notification);
            }
            return View();
        }
        public IActionResult Test()
        {
            var config = _configuration.GetSection("DefaultPaths").GetSection("GalleryImage").Value;
            var x = 1;
            return View();
        }
    }
}