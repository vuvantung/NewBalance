using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewBalance.Client.Infrastructure.Routes
{
    public static class FiltersEndpoints
    {
        public static string GetFilterAccount = "api/Filter/GetFilterAccount";
        public static string GetFilterTypeReport = "api/Filter/GetFilterTypeReport";
        public static string GetFilterProvince = "api/Filter/GetFilterProvince";
        public static string GetFilterDistrict (string ProvinceCode) => $"api/Filter/GetFilterDistrict?ProvinceCode={ProvinceCode}";
    }
}
