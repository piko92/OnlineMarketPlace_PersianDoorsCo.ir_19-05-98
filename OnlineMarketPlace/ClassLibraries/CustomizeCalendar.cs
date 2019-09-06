using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineMarketPlace.ClassLibraries
{
    public class CustomizeCalendar
    {
        static PersianCalendar pc = new PersianCalendar();
        static GregorianCalendar gc = new GregorianCalendar();
        static DateTime dtNow = DateTime.Now;

        static public int CurrentPersianYear()
        {
            return pc.GetYear(dtNow);
        }
        static public DateTime PersianToGregorian(DateTime persianDate)
        {
            DateTime dt = new DateTime(persianDate.Year, persianDate.Month, persianDate.Day, pc);
            return dt;
        }
        static public DateTime GregorianToPersian(DateTime gregorianDate)
        {
            string greDate = gregorianDate.ToString();
            DateTime d = DateTime.Parse(greDate);
            var str_dt = $"{pc.GetYear(d)}/{pc.GetMonth(d)}/{pc.GetDayOfMonth(d)}";
            var dt = DateTime.Parse(str_dt);
            return dt;
        }
        static public DateTime GregorianToPersianDateTime(DateTime gregorianDate)
        {
            string greDate = gregorianDate.ToString();
            DateTime d = DateTime.Parse(greDate);
            var str_dt = $"{pc.GetYear(d)}/{pc.GetMonth(d)}/{pc.GetDayOfMonth(d)} {pc.GetHour(d)}:{pc.GetMinute(d)}:{pc.GetSecond(d)}";
            var dt = DateTime.Parse(str_dt);
            return dt;
        }
    }
}
