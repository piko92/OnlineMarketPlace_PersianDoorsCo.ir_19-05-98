using Microsoft.AspNetCore.Hosting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Resources;
using System.Threading.Tasks;

namespace OnlineMarketPlace.ClassLibraries.NotificationHandler
{
    public class NotificationHandler
    {

        /// <summary>
        /// عملیات ثبت موفق
        /// </summary>
        public static string Success_Insert { get; set; } = "Success_Insert";

        /// <summary>
        /// عملیات ثبت ناموفق
        /// </summary>
        public static string Failed_Insert { get; set; } = "Failed_Insert";


        /// <summary>
        /// عملیات ویرایش موفق
        /// </summary>
        public static string Success_Update { get; set; } = "Success_Update";

        /// <summary>
        /// عملیات ویرایش ناموفق
        /// </summary>
        public static string Failed_Update { get; set; } = "Failed_Update";


        /// <summary>
        /// عملیات حذف موفق
        /// </summary>
        public static string Success_Remove { get; set; } = "Success_Remove";

        /// <summary>
        /// عملیات حذف ناموفق
        /// </summary>
        public static string Failed_Remove { get; set; } = "Failed_Remove";


        /// <summary>
        /// عملیات ورود موفق
        /// </summary>
        public static string Success_Login { get; set; } = "Success_Login";

        /// <summary>
        /// عملیات ورود ناموفق
        /// </summary>
        public static string Failed_Login { get; set; } = "Failed_Login";


        /// <summary>
        /// اطلاعات ورودی نادرست و یا ناقص
        /// </summary>
        public static string Wrong_Values { get; set; } = "Wrong_Values";

        /// <summary>
        /// خطا در انجام عملیات درخواستی
        /// </summary>
        public static string Failed_Operation { get; set; } = "Failed_Operation";

        /// <summary>
        /// نیازمند به ورود به سیستم
        /// </summary>
        public static string Require_Login { get; set; } = "Require_Login";

        /// <summary>
        /// خطا در ثبت تصویر
        /// </summary>
        public static string Failed_Insert_Image { get; set; } = "Failed_Insert_Image";

        /// <summary>
        /// رکورد موجود نمیباشد
        /// </summary>
        public static string Record_Not_Exist { get; set; } = "Record_Not_Exist";

        /// <summary>
        /// عدم دسترسی
        /// </summary>
        public static string Access_denied { get; set; } = "Access_denied";

        /// <summary>
        /// عدم دسترسی حذف
        /// </summary>
        public static string Access_denied_delete { get; set; } = "Access_denied_delete";

        /// <summary>
        /// عدم دسترسی حذف حساب کاربری
        /// </summary>
        public static string Access_denied_self_delete { get; set; } = "Access_denied_self_delete";

        /// <summary>
        /// این اطلاعات تکراری میباشند
        /// </summary>
        public static string DuplicatedValue { get; set; } = "DuplicatedValue";

        /// <summary>
        /// <para>Read Json file and serialize to 'NotificationViewModel'</para>
        /// خواندن فایل متن و تبدیل آن به مدل 'NotificationViewModel'
        /// </summary>
        /// <typeparam name="T">'NotificationViewModel', 'String'</typeparam>
        /// <param name="subject">عنوان عملیات درخواستی</param>
        /// <param name="RootPath">مسیر ریشه سایت</param>
        /// <returns>'NotificationViewModel', 'String'</returns>
        public static T SerializeMessage<T>(string subject, string RootPath)
        {
            T result;
            string resPath = RootPath + "/ClassLibraries/NotificationHandler/Json/NotificationMessages.json";
            var loadedList = LoadFile(resPath);
            if (loadedList.Count > 0)
            {
                var nvm = loadedList.Where(x => x.Subject == subject).FirstOrDefault();
                if (nvm != null)
                {
                    if (typeof(T) == typeof(NotificationViewModel))
                    {
                        result = (T)Convert.ChangeType(nvm, typeof(NotificationViewModel));
                        return result;
                    }
                    else if (typeof(T) == typeof(string))
                    {
                        var serializedModel = JsonConvert.SerializeObject(nvm);
                        result = (T)Convert.ChangeType(serializedModel, typeof(string));
                        return result;
                    }
                    else
                    {
                        result = (T)Convert.ChangeType("Null", typeof(string));
                        return result;
                    }
                }
            }
            result = (T)Convert.ChangeType("Null", typeof(string));
            return result;
        }
        private static List<NotificationViewModel> LoadFile(string path)
        {
            try
            {
                StreamReader sr = new StreamReader(path);
                var file = sr.ReadToEnd();
                var serilizedModel = JsonConvert.DeserializeObject<List<NotificationViewModel>>(file);
                return serilizedModel;
            }
            catch(Exception ex)
            {
                List<NotificationViewModel> notificationHandlers = null;
                return notificationHandlers;
            }
        }
        /// <summary>
        /// <para>Convert string to 'NotificationViewModel' Model</para>
        /// تبدیل رشته حاوی جیسون به مدل
        /// </summary>
        /// <param name="serialiazedNotification">رشته سریالایز شده</param>
        /// <returns></returns>
        public static NotificationViewModel DeserializeMessage(string serialiazedNotification)
        {
            var result = JsonConvert.DeserializeObject<NotificationViewModel>(serialiazedNotification);
            return result;
        }
    }
}
