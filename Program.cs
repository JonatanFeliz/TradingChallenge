using System;
using System.Collections.Generic;
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

namespace TrandingChallenge
{
    internal static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        static void Main()
        {
            RunApp();

            //Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new Form1());
        }

        public static void RunApp()
        {
            double clientInversion = 50;
            double brokerComision = 0.02;

            Console.WriteLine($"Inversion cliente {clientInversion} y comision del broker {brokerComision}");

            ReadFile.ReadCSV();
        }

    }

    public class ReadFile
    {
        public static void ReadCSV()
        {
            string csvFile = "./../../stocks-ITX.csv";

            var csvHelperConfiguration = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                Encoding = Encoding.UTF8,
                Delimiter = ";"
            };

            using (var fileStream = File.Open(csvFile, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                using (var csvReader = new StreamReader(fileStream, Encoding.UTF8))
                using (var csv = new CsvReader(csvReader, csvHelperConfiguration))
                {
                    var data = csv.GetRecords<InditexMarket>();

                    foreach (var person in data)
                    {
                        Console.WriteLine(person.MarketOpenDays);
                        Console.WriteLine(Convert.ToDouble(person.MarketCloseValue.Replace('.', ',')));
                        Console.WriteLine(Convert.ToDouble(person.MarketOpenValue.Replace('.', ',')));
                    }
                }
            }
        }

    }

    public class InditexMarket
    {
        [Name("Fecha")]
        public string MarketOpenDays { get; set; }

        [Name("Cierre")]
        public string MarketCloseValue { get; set; }

        [Name("Apertura")]
        public string MarketOpenValue { get; set; }

    }
}

/*
 - Leer csv x
 - Encontrar ultimo jueves de cada mes
 - Calcular el dia siguiente
 */
