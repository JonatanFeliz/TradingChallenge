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

namespace Files
{
    public class ReadCSVFile
    {
        public static IEnumerable<InditexMarket> ReadCSV()
        {
            string csvFile = "./../../stocks-ITX.csv";

            var records = new List<InditexMarket>();

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

                    foreach (var inditex in data)
                    {
                        records.Add(inditex);
                    }
                }
            }

            return records.ToArray().Reverse();
        }
    }
}
