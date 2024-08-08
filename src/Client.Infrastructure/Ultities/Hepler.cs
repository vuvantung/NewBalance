using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewBalance.Client.Infrastructure.Ultities
{
    public static class Hepler
    {
        public static string GetValueFromTextSelect(string input )
        {
            if ( input == "-- Tat ca --" )
            {
                return "0";
            }
            else
            {
                return input.Split('-')[0];
            }
        }
    }
}
