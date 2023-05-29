using CsvHelper.Configuration;
using CsvHelper;
using Inditex;
using UtilsFunctions;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Net;
using System.Security.Policy;

namespace Files
{
    public class ReadCSVFile
    {
        public static IEnumerable<InditexTrading> ReadCSV(string csvFile)
        {

            var records = new List<InditexTrading>();

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
                    var data = csv.GetRecords<InditexTrading>();

                    foreach (var inditex in data)
                    {
                        records.Add(inditex);
                    }
                }
            }

            return records.ToArray().Reverse();
        }

        public static string GetCSV()
        {
            var csvPath = ConfigurationManager.AppSettings["CSVPath"];

            return csvPath;
        }

        public static void DownloadCSVFile(string url, string outputPath)
        {
            using (var client = new WebClient())
            {
                try
                {
                    client.DownloadFile(url, outputPath);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error al descargar el archivo: " + ex.Message);
                }
            }
        }

        public static bool FileExists(string path)
        {
            var fileExist = File.Exists(path);

            if (fileExist)
            {
                return true;
            }
            
            return false;
        }
    }
}
