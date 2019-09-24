using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace OnlineMarketPlace.ClassLibraries
{
    public class FileManager
    {

        public static async Task<string> SaveImageInDirectory(string contentRootPath, string folderPath, IFormFile FormFile, bool nested = false, int entityId = 0)
        {
            string uploads;
            if (nested == false)
            {
                uploads = Path.Combine(contentRootPath, folderPath);
            }
            else
            {
                uploads = Path.Combine(contentRootPath, folderPath, $"{entityId}");
            }
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
                    if (nested == true)
                    {
                        result = folderPath + $"{entityId}\\" + fileName;
                    }
                    else
                    {
                        result = folderPath + fileName;
                    }
                }
            }

            return result;
        }

        public static string SaveThumbnail(
                            string savePath, 
                            string contentRootPath, 
                            string folderPath, 
                            ImageFormat format, 
                            bool nested = false, int entityId = 0,
                            int size = 120)
        {
            var url = Path.Combine(contentRootPath, savePath);
            FileStream FS = new FileStream(url, FileMode.Open);

            if (FS.Length > 0)
            {
                Image img = Image.FromStream(FS);
                Bitmap bmp = new Bitmap(img, size, size);
                string uploads;
                if (nested == false)
                {
                    uploads = Path.Combine(contentRootPath, folderPath);
                }
                else
                {
                    uploads = Path.Combine(contentRootPath, folderPath, $"{entityId}", $"{size}x{size}");
                }
                bool exists = System.IO.Directory.Exists(uploads);
                string result = "";
                if (!exists)
                {
                    System.IO.Directory.CreateDirectory(uploads);
                }
                var fileName = Guid.NewGuid().ToString().Replace("-", "") + Path.GetExtension(".png");
                using (var fileStream = new FileStream(Path.Combine(uploads, fileName), FileMode.Create))
                {
                    bmp.Save(fileStream, format);
                    
                    if (nested == true)
                    {
                        result = folderPath + $"{entityId}\\{size}x{size}\\" + fileName;
                    }
                    else
                    {
                        result = folderPath + fileName;
                    }
                }
                return result;
            }
            return null;
        }//end SaveThumbnailAsync

        public static bool DeleteFile(string contentRootPath, string imagePath)
        {
            var fileName = contentRootPath + "\\" + imagePath;
            if ((System.IO.File.Exists(fileName)))
            {
                System.IO.File.Delete(fileName);
                return true;
            }
            return false;
        }
        public static bool DeleteDirectory(string contentRootPath, string imagePath)
        {
            var aPath = imagePath.Split("\\");
            var newAPath = aPath.Take(aPath.Count() - 1).ToArray();
            var newPath = String.Join("", newAPath);

            var fileName = contentRootPath + "\\" + imagePath;
            if ((System.IO.Directory.Exists(newPath)))
            {
                System.IO.File.Delete(newPath);
                return true;
            }
            return false;
        }
    }
}
