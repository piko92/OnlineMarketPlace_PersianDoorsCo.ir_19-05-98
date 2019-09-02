using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace OnlineMarketPlace.Controllers
{
    //صرفا جهت تست و نمایش قالب ایجاد گردیده
    public class ProductController : Controller
    {
        public IActionResult Search()
        {
            return View();
        }
    }
}