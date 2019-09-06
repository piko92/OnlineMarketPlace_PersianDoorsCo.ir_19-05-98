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
        public AdminController(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
            contentRootPath = _hostingEnvironment.ContentRootPath;//returns the root path of the website
        }

        public IActionResult Dashboard(string notification)
        {
            if (notification != null)
            {
                ViewData["nvm"] = NotificationHandler.DeserializeMessage(notification);
            }
            return View();
        }
        public IActionResult Test(List<IFormFile> img)
        {
            //var result = NotificationHandler.SerializeMessage<NotificationViewModel>(NotificationHandler.Failed_Login, contentRootPath);
            //var result = NotificationHandler.SerializeMessage<NotificationViewModel>(NotificationHandler.Success_Update, contentRootPath);
            //var result = NotificationHandler.SerializeMessage<NotificationViewModel>(NotificationHandler.Success_Remove, contentRootPath);

            //ViewData["nvm"] = result;

            if (img.Count <= 0)
            {
                return View();
            }
            else
            {
                img.ForEach(x =>
                {
                    if (x != null)
                    {
                        byte[] b = new byte[x.Length];
                        x.OpenReadStream().Read(b, 0, b.Length);
                        var xx = b;

                        MemoryStream mem1 = new MemoryStream(b);
                        Image img2 = Image.FromStream(mem1);
                        Bitmap bmp = new Bitmap(img2, 300, 300);
                        MemoryStream mem2 = new MemoryStream();
                        bmp.Save(mem2, System.Drawing.Imaging.ImageFormat.Jpeg);
                        //bmp.Save("D:\\1.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
                        //p.imgThumbnail = mem2.ToArray();
                    }
                });

                var result = NotificationHandler.SerializeMessage<NotificationViewModel>(NotificationHandler.Success_Insert, contentRootPath);
                ViewData["nvm"] = result;
                return View();
            }
        }
    }
}