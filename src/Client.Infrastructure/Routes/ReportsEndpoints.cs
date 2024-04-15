using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewBalance.Client.Infrastructure.Routes
{
    public static class ReportsEndpoints
    {
        public static string GetBDT_01Report( int account, int fromDate, int toDate )
        {
            return $"api/Report/GetBDT_01Report?account={account}&fromdate={fromDate}&todate={toDate}";
        }
    }
}
