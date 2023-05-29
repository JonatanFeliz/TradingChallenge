using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageView
{
    public class Message
    {
        public static void ShowEveryMonthDetails(string date, double finalInversion, double MarketStockOpenValue, double stock)
        {
            Console.WriteLine("***********************************");
            Console.WriteLine($"Fecha: {date}");
            Console.WriteLine($"Inversion: {finalInversion} €");
            Console.WriteLine($"Precio por accion: {MarketStockOpenValue}€");
            Console.WriteLine($"Acciones obtenidas: {stock}");
        }

        public static void TotalDetails(double totalStock, double MarketStockCloseValue, double finalCapital)
        {
            Console.WriteLine("***********************************");
            Console.WriteLine($"Total Acciones: {totalStock}");
            Console.WriteLine($"Precio de venta por accion: {MarketStockCloseValue}€");
            Console.WriteLine($"Capital final: {finalCapital}€");
        }
    }
}
