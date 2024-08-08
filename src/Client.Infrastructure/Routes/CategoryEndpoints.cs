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
        public static string GetCategoryGiaVonChuan ( int pageIndex, int pageSize, int account ) => $"api/Category/CategoryDM_GiaVonChuan?pageIndex={pageIndex}&pageSize={pageSize}&account={account}";
        public static string GetCategoryGiaVonChuanNT ( int pageIndex, int pageSize, int account ) => $"api/Category/GetCategoryGiaVonChuanNT?pageIndex={pageIndex}&pageSize={pageSize}&account={account}";
        public static string GetCategoryPostOffice ( int pageIndex, int pageSize, int ProvinceCode, int DistrictCode, int communeCode, int containVXHD ) => $"api/Category/GetCategoryPostOffice?pageIndex={pageIndex}&pageSize={pageSize}&ProvinceCode={ProvinceCode}&DistrictCode={DistrictCode}&communeCode={communeCode}&containVXHD={containVXHD}";
        public static string GetCategoryProvince ( int pageIndex, int pageSize) => $"api/Category/GetCategoryProvince?pageIndex={pageIndex}&pageSize={pageSize}";
        public static string GetCategoryProvince_V2( string strProvinceCode, string strProvinceName, int pageIndex, int pageSize ) => $"api/Category/GetCategoryProvince_V2?strProvinceCode={strProvinceCode}&strProvinceName={strProvinceName}&pageIndex={pageIndex}&pageSize={pageSize}";
        public static string GetCategoryDistrict ( int pageIndex, int pageSize, int ProvinceCode ) => $"api/Category/GetCategoryDistrict?pageIndex={pageIndex}&pageSize={pageSize}&ProvinceCode={ProvinceCode}";
        public static string GetCategoryDistrict_V2( string strProvinceCode, string strDistrictCode, string strDistrictName, int pageIndex, int pageSize ) => $"api/Category/GetCategoryDistrict_V2?strProvinceCode={strProvinceCode}&strDistrictCode={strDistrictCode}&strDistrictName={strDistrictName}&pageIndex={pageIndex}&pageSize={pageSize}";
        public static string GetCategoryCommune ( int pageIndex, int pageSize, int DistrictCode) => $"api/Category/GetCategoryCommune?pageIndex={pageIndex}&pageSize={pageSize}&DistrictCode={DistrictCode}";
        public static string GetCategoryCommune_V2 ( string strProvinceCode, string strDistrictCode, string strCommuneCode, string strCommuneName, int pageIndex, int pageSize ) => $"api/Category/GetCategoryCommune_V2?strProvinceCode={strProvinceCode}&strDistrictCode={strDistrictCode}&strCommuneCode={strCommuneCode}&strCommuneName={strCommuneName}&pageIndex={pageIndex}&pageSize={pageSize}";
        public static string GetAllCategoryProvinceDistrictCommune() => $"api/Category/GetAllCategoryProvinceDistrictCommune";
        public static string AddProvince = "api/Category/AddProvince";
        public static string AddDistrict = "api/Category/AddDistrict";
        public static string AddCommune = "api/Category/AddCommune";
        public static string AddPostOffice = "api/Category/AddPostOfficeEMS";
        public static string UpdateCategory = "api/Category/UpdateSingleData";
        public static string DeleteCategory = "api/Category/DeleteSingleData";
        public static string GetCategoryDM_Dich_Vu(int pageIndex, int pageSize) => $"api/Category/GetCategoryDM_Dich_Vu?pageIndex={pageIndex}&pageSize={pageSize}";
        public static string AddDM_Dich_Vu = "api/Category/AddDM_Dich_Vu";
        public static string AddGiaVonChuan = "api/Category/AddGiaVonChuan";
        public static string UpdatePostOffice = "api/Category/UpdatePostOffice";
        public static string UpdateProvince = "api/Category/UpdateProvince";
        public static string UpdateDistrict = "api/Category/UpdateDistrict";
        public static string UpdateCommune = "api/Category/UpdateCommune";

    }
}
