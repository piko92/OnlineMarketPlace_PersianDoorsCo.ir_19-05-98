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

        public class Months
        {
            public int Key { get; set; }
            public string Value { get; set; }
        }

        public static List<Months> months = new List<Months>
        {
            new Months{Key = 1, Value = "فروردین"},
            new Months{Key = 2, Value = "اردیبهشت"},
            new Months{Key = 3, Value = "خرداد"},
            new Months{Key = 4, Value = "تیر"},
            new Months{Key = 5, Value = "مرداد"},
            new Months{Key = 6, Value = "شهریور"},
            new Months{Key = 7, Value = "مهر"},
            new Months{Key = 8, Value = "آبان"},
            new Months{Key = 9, Value = "آذر"},
            new Months{Key = 10, Value = "دی"},
            new Months{Key = 11, Value = "بهمن"},
            new Months{Key = 12, Value = "اسفند"},
        };

        static public int CurrentPersianYear()
        {
            return pc.GetYear(dtNow);
        }
        static public DateTime PersianToGregorian(DateTime persianDate)
        {
            DateTime dt = new DateTime(persianDate.Year, persianDate.Month, persianDate.Day, pc);
            return dt;
        }
        static public DateTime PersianToGregorian(int year, int month, int day)
        {
            DateTime dt = new DateTime(year, month, day, pc);
            return dt;
        }
        static public string GregorianToPersian(DateTime gregorianDate)
        {
            string greDate = gregorianDate.ToString();
            DateTime d = DateTime.Parse(greDate);
            var str_dt = $"{pc.GetYear(d)}/{pc.GetMonth(d)}/{pc.GetDayOfMonth(d)}";
            //var dt = DateTime.Parse(str_dt);
            return str_dt;
        }
        static public string GregorianToPersianDateTime(DateTime gregorianDate)
        {
            string greDate = gregorianDate.ToString();
            DateTime d = DateTime.Parse(greDate);
            var str_dt = $"{pc.GetYear(d)}/{pc.GetMonth(d)}/{pc.GetDayOfMonth(d)} - {pc.GetHour(d)}:{pc.GetMinute(d)}:{pc.GetSecond(d)}";
            //var dt = DateTime.Parse(str_dt);
            return str_dt;
        }


    }
}
