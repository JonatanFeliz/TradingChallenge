using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UtilsFunctions
{
    public class Util
    {
        public static DateTime GetLastThursday(DateTime lastDayOfMonth)
        {
            DateTime lastThursday = lastDayOfMonth;

            while (lastThursday.DayOfWeek != DayOfWeek.Thursday)
            {
                lastThursday = lastThursday.AddDays(-1);
            }

            return lastThursday;
        }

        public static string AddDotToDate(string day)
        {
            int endMonthPosition = day.IndexOf("-") + 4;

            string dateWithPoint = day.Insert(endMonthPosition, ".");

            return dateWithPoint;
        }

        public static string RemoveDotToDate(string day)
        {
            string dateWithOutPoint = day.Replace(".", ""); ;

            return dateWithOutPoint;
        }
    }
}
