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
        public static string GetCategoryPostOffice ( int pageIndex, int pageSize, int ProvinceCode, int DistrictCode, int communeCode, int containVXHD ) => $"api/Category/GetCategoryPostOffice?pageIndex={pageIndex}&pageSize={pageSize}&ProvinceCode={ProvinceCode}&DistrictCode={DistrictCode}&communeCode={communeCode}&containVXHD={containVXHD}";
        public static string GetCategoryProvince ( int pageIndex, int pageSize) => $"api/Category/GetCategoryProvince?pageIndex={pageIndex}&pageSize={pageSize}";
        public static string GetCategoryDistrict ( int pageIndex, int pageSize, int ProvinceCode ) => $"api/Category/GetCategoryDistrict?pageIndex={pageIndex}&pageSize={pageSize}&ProvinceCode={ProvinceCode}";
        public static string GetCategoryCommune ( int pageIndex, int pageSize, int DistrictCode) => $"api/Category/GetCategoryCommune?pageIndex={pageIndex}&pageSize={pageSize}&DistrictCode={DistrictCode}";
        public static string GetAllCategoryProvinceDistrictCommune() => $"api/Category/GetAllCategoryProvinceDistrictCommune";
        public static string AddProvince = "api/Category/AddProvince";
        public static string AddDistrict = "api/Category/AddDistrict";
        public static string AddCommune = "api/Category/AddCommune";
        public static string AddPostOffice = "api/Category/AddPostOfficeEMS";
        public static string UpdateCategory = "api/Category/UpdateSingleData";
        public static string DeleteCategory = "api/Category/DeleteSingleData";

    }
}
