using System;
using System.Collections.Generic;
using UtilsFunctions;
using Files;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using CsvHelper;
using System.IO;
using System.Globalization;
using System.Runtime.InteropServices;
using CsvHelper.Configuration;
using CsvHelper.Configuration.Attributes;
using System.Text;
using System.Runtime.InteropServices.ComTypes;
using System.Runtime.Remoting.Contexts;
using Inditex;

namespace TrandingChallenge
{
    internal static class Program
    {

        [STAThread]
        static void Main()
        {
            RunApp();
        }

        public static void RunApp()
        {
            const double initialInversion = 50;
            const double brokerComision = 0.02;
            const double finalInversion = (initialInversion * (1 - brokerComision));
            double totalStock = 0;
            double finalCapital = 0;
            var CSVReaded = ReadCSVFile.ReadCSV();

            DateTime inditexMarketStartDate = new DateTime(2001, 5, 1);
            DateTime inditexMarketEndDate = new DateTime(2017, 12, 28);
            DateTime[] lastThursdays = GetAllLastThursday(inditexMarketStartDate, inditexMarketEndDate);
            DateTime[] MarketOpenDays = GetMarketOpenDaysInDateFormat(CSVReaded);

            foreach (var thursday in lastThursdays)
            {
                DateTime nextDate = MarketOpenDays.FirstOrDefault(date => date > thursday);

                if (nextDate != default)
                {
                    string nextDateToString = Util.RemoveDotToDate(nextDate.ToString("dd-MMM-yyyy"));

                    foreach (var csvLine in CSVReaded)
                    {
                        if (csvLine.MarketOpenDays == nextDateToString)
                        {
                            double MarketStockOpenValue = Convert.ToDouble(csvLine.MarketStockOpenValue.Replace('.', ','));

                            double stock = (double)Math.Round(finalInversion / MarketStockOpenValue, 3);

                            totalStock += stock;

                            Console.WriteLine("***********************************");
                            Console.WriteLine($"Fecha: {csvLine.MarketOpenDays}");
                            Console.WriteLine($"Inversion: {finalInversion} €");
                            Console.WriteLine($"Precio por accion: {MarketStockOpenValue}€");
                            Console.WriteLine($"Acciones obtenidas: {stock}");
                        }
                    }
                }
            }

            double MarketStockCloseValue = Convert.ToDouble(CSVReaded.ElementAt(CSVReaded.Count() - 1).MarketStockCloseValue.Replace('.', ','));

            finalCapital = Math.Round(totalStock * MarketStockCloseValue, 3);

            Console.WriteLine("***********************************");
            Console.WriteLine($"Capital final: {finalCapital}€");

        }

        public static DateTime[] GetAllLastThursday(DateTime startDate, DateTime endDate)
        {
            List<DateTime> lastMonthThursday = new List<DateTime>();

            DateTime currentMonth = new DateTime(startDate.Year, startDate.Month, 1);

            while (currentMonth <= endDate)
            {
                DateTime lastDayOfMonth = new DateTime(currentMonth.Year, currentMonth.Month, DateTime.DaysInMonth(currentMonth.Year, currentMonth.Month));

                if (currentMonth >= startDate && currentMonth <= endDate)
                {
                    DateTime lastThursday = Util.GetLastThursday(lastDayOfMonth);

                    lastMonthThursday.Add(lastThursday);
                }

                currentMonth = currentMonth.AddMonths(1);
            }

            return lastMonthThursday.ToArray();
        }

        public static DateTime[] GetMarketOpenDaysInDateFormat(IEnumerable<InditexMarket> CSVReaded) 
        {
            List<DateTime> MarketOpenDays = new List<DateTime>();

            foreach (var csvLine in CSVReaded)
            {
                MarketOpenDays.Add(DateTime.ParseExact(Util.AddDotToDate(csvLine.MarketOpenDays), "dd-MMM-yyyy", null));
            }

            return MarketOpenDays.ToArray();
        }

    }

}
