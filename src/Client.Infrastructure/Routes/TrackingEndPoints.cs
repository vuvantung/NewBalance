using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewBalance.Client.Infrastructure.Routes
{
    public static class TrackingEndPoints
    {
        public static string TrackingItem( string ItemCode ) => $"api/Tracking/TrackingItem?ItemCode={ItemCode}";
    }
}
