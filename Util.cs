using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UtilsFunctions
{
    public class Util
    {
        public static string AddDotToDate(string day)
        {
            try
            {
                var endMonthPosition = day.IndexOf("-") + 4;

                var dateWithPoint = day.Insert(endMonthPosition, ".");

                return dateWithPoint;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Se ha producido una excepción: " + ex.Message);
                return "";
            }
            
        }

        public static string RemoveDotToDate(string day)
        {
            try
            {
                var dateWithOutPoint = day.Replace(".", ""); ;

                return dateWithOutPoint;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Se ha producido una excepción: " + ex.Message);
                return "";
            }
            
        }
    }
}
