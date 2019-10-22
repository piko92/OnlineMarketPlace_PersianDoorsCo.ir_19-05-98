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
        #region SaveFile
        public static async Task<string> ReadAndSaveFile(string contentRootPath, string folderPath, IFormFile FormFile)
        {
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
        #endregion
        #region Image
        public static string SaveImageInDirectoryResizing(
                string contentRootPath,
                string folderPath,
                IFormFile FormFile,
                ImageFormat format,
                bool nested = false,
                int entityId = 0,
                int maxSize = 1000
            )
        {
            
            GC.WaitForPendingFinalizers();
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
                var fs = file.OpenReadStream();
                var newDimensions = AspectRatioResizing(fs, maxSize);
                System.Drawing.Image image = System.Drawing.Image.FromStream(fs);
                Bitmap bitmap = new Bitmap(image, newDimensions.Item1, newDimensions.Item2);

                var fileName = Guid.NewGuid().ToString().Replace("-", "") + Path.GetExtension(file.FileName);
                using (var fileStream = new FileStream(Path.Combine(uploads, fileName), FileMode.Create))
                {
                    //await file.CopyToAsync(fileStream);
                    bitmap.Save(fileStream, format);
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

        //returns path of saved file
       

        public static async Task<string> SaveImageInDirectory(string contentRootPath, string folderPath, IFormFile FormFile, bool nested = false, int entityId = 0)
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
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
                    fileStream.Flush();
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

            GC.Collect();
            GC.WaitForPendingFinalizers();
            return result;
        }

        public static string ImageThumbnail(
                string contentRootPath,
                string folderPath,
                IFormFile FormFile,
                ImageFormat format,
                bool nested = false, int entityId = 0,
                int sizeW = 270,
                int sizeH = 270
            )
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
            var file = FormFile;
            string uploads, result = "";

            if (file.Length > 0)
            {

                if (nested == false)
                {
                    uploads = Path.Combine(contentRootPath, folderPath);
                }
                else
                {
                    uploads = Path.Combine(contentRootPath, folderPath, $"{entityId}", $"{sizeW}x{sizeH}");
                }
                bool exists = System.IO.Directory.Exists(uploads);

                if (!exists)
                {
                    System.IO.Directory.CreateDirectory(uploads);
                }

                var fs = file.OpenReadStream();

                System.Drawing.Image image = System.Drawing.Image.FromStream(fs);
                Bitmap bitmap = new Bitmap(image, sizeW, sizeH);

                var fileName = Guid.NewGuid().ToString().Replace("-", "") + Path.GetExtension(file.FileName);
                using (var fileStream = new FileStream(Path.Combine(uploads, fileName), FileMode.Create))
                {
                    //await file.CopyToAsync(fileStream);
                    bitmap.Save(fileStream, format);
                    fileStream.Flush();
                    if (nested == true)
                    {
                        result = folderPath + $"{entityId}\\{sizeW}x{sizeH}\\" + fileName;
                    }
                    else
                    {
                        result = folderPath + fileName;
                    }
                }
            }
            GC.Collect();
            GC.WaitForPendingFinalizers();
            return result;
        }

        public static string SaveThumbnail(
                            string savePath,
                            string contentRootPath,
                            string folderPath,
                            ImageFormat format,
                            bool nested = false, int entityId = 0,
                            int size = 270)
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
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
                FS.Dispose();
                FS.Close();
                GC.Collect();
                GC.WaitForPendingFinalizers();
                return result;
            }
            return null;
        }//end SaveThumbnailAsync

        /// <summary>
        /// به کمک این تابع میتوان به نسبت طول و عرض تصویر اندازه آنرا تغییر داد
        /// </summary>
        /// <param name="fileStream"></param>
        /// <returns>int newWidth, int newHeight</returns>
        private static (int, int) AspectRatioResizing(Stream fileStream, int sizeLimit)
        {
            System.Drawing.Image image = System.Drawing.Image.FromStream(fileStream);
            double intWidth = image.Width;
            double intHeight = image.Height;
            image.Dispose();

            double newWidth = intWidth;
            double newHeight = intHeight;

            if (intWidth > sizeLimit || intHeight > sizeLimit)
            {
                double aspectRatio = intWidth / intHeight;

                if (intWidth >= intHeight)
                {
                    newHeight = intHeight - ((intWidth - sizeLimit) / aspectRatio);
                    newWidth = sizeLimit;
                }
                else if (intWidth < intHeight)
                {
                    newWidth = intWidth - ((intHeight - sizeLimit) / aspectRatio);
                    newHeight = sizeLimit;
                }
            }
            return ((int)newWidth, (int)newHeight);
        }
        #endregion
        #region Delete File & Directory

        public static bool DeleteFile(string contentRootPath, string imagePath)
        {
            var fileName = contentRootPath + "\\" + imagePath;
            GC.Collect();
            GC.WaitForPendingFinalizers();
            if ((System.IO.File.Exists(fileName)))
            {

                System.IO.File.Delete(fileName);
                return true;
            }
            return false;
        }
        public static bool DeleteDirectory(string contentRootPath, string imagePath)
        {
            //var aPath = imagePath.Split("\\");
            //var newAPath = aPath.Take(aPath.Count() - 1).ToArray();
            //var newPath = String.Join("", newAPath);
            var fileName = contentRootPath + "\\" + imagePath;
            var newPath = Path.GetDirectoryName(fileName);
            GC.Collect();
            GC.WaitForPendingFinalizers();
            if ((System.IO.Directory.Exists(newPath)))
            {
                System.IO.Directory.Delete(newPath, true);
                return true;
            }
            return false;
        }
        #endregion

    }
}
