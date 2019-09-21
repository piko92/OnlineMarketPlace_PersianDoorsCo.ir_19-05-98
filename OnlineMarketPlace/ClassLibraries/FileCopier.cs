using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineMarketPlace.ClassLibraries
{
    public class FileCopier
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        private static IConfiguration _configuration;
        static string contentRootPath;

        public FileCopier(
            IHostingEnvironment hostingEnvironment,
            IConfiguration configuration)
        {
            _hostingEnvironment = hostingEnvironment;
            _configuration = configuration;
            contentRootPath = hostingEnvironment.ContentRootPath;
        }

        public static async Task<string> SaveImageInDirectory(string ParentSectionName, string SectionName, IFormFile FormFile)
        {
            string folderPath = _configuration.GetSection(ParentSectionName).GetSection(SectionName).Value;
            string uploads = Path.Combine(contentRootPath, folderPath);
            bool exists = System.IO.Directory.Exists(uploads);
            string result = "";
            if (!exists)
            {
                System.IO.Directory.CreateDirectory(uploads);
            }
                var file = FormFile;

                if (file.Length > 0)
                {
                    var fileName = Guid.NewGuid().ToString().Replace("-", "") + Path.GetExtension(file.FileName);
                    using (var fileStream = new FileStream(Path.Combine(uploads, fileName), FileMode.Create))
                    {
                        await file.CopyToAsync(fileStream);
                        result = folderPath + fileName;
                    }
                }
            
            return result;

        }

        public static byte[] ImgThumbnail(byte[] b)
        {
            MemoryStream mem1 = new MemoryStream(b);
            Image img2 = Image.FromStream(mem1);
            Bitmap bmp = new Bitmap(img2, 120, 120);
            MemoryStream mem2 = new MemoryStream();
            bmp.Save(mem2, System.Drawing.Imaging.ImageFormat.Jpeg);

            return mem2.ToArray();
        }
    }
}
