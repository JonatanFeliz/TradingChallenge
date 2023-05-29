using Inditex;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UtilsFunctions;

namespace DatesManagement
{
    public class Date
    {
        public static DateTime[] GetMarketOpenDaysInDateFormat(IEnumerable<InditexTrading> CSVReaded)
        {
            var MarketOpenDays = new List<DateTime>();

            foreach (var csvLine in CSVReaded)
            {
                try
                {
                    MarketOpenDays.Add(DateTime.ParseExact(Util.AddDotToDate(csvLine.MarketOpenDays), "dd-MMM-yyyy", null));
                }
                catch (Exception ex) { Console.WriteLine("Se ha producido una excepción: " + ex.Message); }
            }

            return MarketOpenDays.ToArray();
        }

        public static DateTime GetLastThursday(DateTime lastDayOfMonth)
        {
            var lastThursday = lastDayOfMonth;

            while (lastThursday.DayOfWeek != DayOfWeek.Thursday)
            {
                lastThursday = lastThursday.AddDays(-1);
            }

            return lastThursday;
        }

        public static DateTime[] GetAllLastThursdayBetweenTwoDates(DateTime startDate, DateTime endDate)
        {
            var lastMonthThursday = new List<DateTime>();

            var currentMonth = new DateTime(startDate.Year, startDate.Month, 1);

            while (currentMonth <= endDate)
            {
                var lastDayOfMonth = new DateTime(currentMonth.Year, currentMonth.Month, DateTime.DaysInMonth(currentMonth.Year, currentMonth.Month));

                if (currentMonth >= startDate && currentMonth <= endDate)
                {
                    var lastThursday = GetLastThursday(lastDayOfMonth);

                    lastMonthThursday.Add(lastThursday);
                }

                currentMonth = currentMonth.AddMonths(1);
            }

            return lastMonthThursday.ToArray();
        }
    }
}
