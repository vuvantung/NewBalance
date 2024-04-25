using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewBalance.Client.Infrastructure.Routes
{
    public class CategoryEndpoints
    {
        public static string GetCategoryAccount ( int pageIndex, int pageSize, int account ) => $"api/Category/GetCategoryAccount?pageIndex={pageIndex}&pageSize={pageSize}&account={account}";
        public static string GetCategoryGiaVonChuan ( int pageIndex, int pageSize, int account ) => $"api/Category/GetCategoryGiaVonChuan?pageIndex={pageIndex}&pageSize={pageSize}&account={account}";
        public static string GetCategoryGiaVonChuanNT ( int pageIndex, int pageSize, int account ) => $"api/Category/GetCategoryGiaVonChuanNT?pageIndex={pageIndex}&pageSize={pageSize}&account={account}";
        public static string GetCategoryPostOffice ( int pageIndex, int pageSize, int account ) => $"api/Category/GetCategoryPostOffice?pageIndex={pageIndex}&pageSize={pageSize}&account={account}";

    }
}
