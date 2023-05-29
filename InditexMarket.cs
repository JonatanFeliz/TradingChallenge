using CsvHelper.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inditex
{
    public class InditexMarket
    {
        [Name("Fecha")]
        public string MarketOpenDays { get; set; }

        [Name("Cierre")]
        public string MarketStockCloseValue { get; set; }

        [Name("Apertura")]
        public string MarketStockOpenValue { get; set; }
    }
}
