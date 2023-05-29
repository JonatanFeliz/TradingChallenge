using DatesManagement;
using Files;
using Inditex;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.VisualStyles;
using UtilsFunctions;
using MessageView;
using CsvHelper;

namespace App
{
    public class RunApp
    {
        public static void RunApplication()
        {
            try
            {
                const double initialInversion = 50;
                const double brokerComision = 0.02;
                const double finalInversion = (initialInversion * (1 - brokerComision));
                var csvFile = "./../../stocks-ITX.csv";
                var totalStock = 0.0;
                var finalCapital = 0.0;
                var CSVReaded = ReadCSVFile.ReadCSV(csvFile);

                var inditexMarketStartDate = new DateTime(2001, 5, 1);
                var inditexMarketEndDate = new DateTime(2017, 12, 28);
                var lastThursdays = Date.GetAllLastThursdayBetweenTwoDates(inditexMarketStartDate, inditexMarketEndDate);
                var MarketOpenDays = Date.GetMarketOpenDaysInDateFormat(CSVReaded);

                GetInvestmentDay(lastThursdays, MarketOpenDays, CSVReaded, finalInversion, totalStock, finalCapital);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al descargar el archivo: " + ex.Message);
            }
        }

        public static void GetInvestmentDay(DateTime[] lastThursdays, DateTime[] MarketOpenDays, IEnumerable<InditexTrading> CSVReaded, double finalInversion, double totalStock, double finalCapital) 
        {
            foreach (var thursday in lastThursdays)
            {
                var nextDate = MarketOpenDays.FirstOrDefault(date => date > thursday);

                if (nextDate != default)
                {
                    var nextDateToString = Util.RemoveDotToDate(nextDate.ToString("dd-MMM-yyyy"));

                    foreach (var csvLine in CSVReaded)
                    {
                        if (csvLine.MarketOpenDays == nextDateToString)
                        {
                            var MarketStockOpenValue = Convert.ToDouble(csvLine.MarketStockOpenValue.Replace('.', ','));

                            var stock = (double)Math.Round(finalInversion / MarketStockOpenValue, 3);

                            totalStock += stock;

                            Message.ShowEveryMonthDetails(csvLine.MarketOpenDays, finalInversion, MarketStockOpenValue, stock);
                        }
                    }
                }
            }

            double MarketStockCloseValue = Convert.ToDouble(CSVReaded.ElementAt(CSVReaded.Count() - 1).MarketStockCloseValue.Replace('.', ','));

            finalCapital = Math.Round(totalStock * MarketStockCloseValue, 3);

            Message.TotalDetails(totalStock, MarketStockCloseValue, finalCapital);
        }
    }
}
