using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.Wordprocessing;
using NewBalance.Application.Features.Doi_Soat;
using NewBalance.Domain.Entities.Doi_Soat.Category;
using NewBalance.Domain.Entities.Doi_Soat.Filter;
using NewBalance.Shared.Wrapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace NewBalance.Client.Infrastructure.Managers.Doi_Soat.Category
{
    public class CategoryManager : ICategoryManager
    {
        private readonly HttpClient _httpClient;

        public CategoryManager( HttpClient httpClient )
        {
            _httpClient = httpClient;
        }

        public async Task<ResponseData<Account>> GetCategoryAccountAsync( int pageIndex, int pageSize, int account )
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<ResponseData<Account>>(Routes.CategoryEndpoints.GetCategoryAccount(pageIndex, pageSize, account));
                return response;
            }
            catch(Exception ex )
            {
                return new ResponseData<Account>
                {
                    code = "error",
                    message = $"Failed connect to API: {ex}"
                };
            }
        }

        

        

        public async Task<ResponseData<GiaVonChuan>> GetCategoryGiaVonChuanAsync( int pageIndex, int pageSize, int account )
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<ResponseData<GiaVonChuan>>(Routes.CategoryEndpoints.GetCategoryGiaVonChuan(pageIndex, pageSize, account));
                return response;
            }
            catch ( Exception ex )
            {
                return new ResponseData<GiaVonChuan>
                {
                    code = "error",
                    message = $"Failed connect to API: {ex}"
                };
            }
        }

        public async Task<ResponseData<GiaVonChuanNT>> GetCategoryGiaVonChuanNTAsync( int pageIndex, int pageSize, int account )
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<ResponseData<GiaVonChuanNT>>(Routes.CategoryEndpoints.GetCategoryGiaVonChuanNT(pageIndex, pageSize, account));
                return response;
            }
            catch ( Exception ex )
            {
                return new ResponseData<GiaVonChuanNT>
                {
                    code = "error",
                    message = $"Failed connect to API: {ex}"
                };
            }
        }

        public async Task<ResponseData<PostOffice>> GetCategoryPostOfficeAsync( int pageIndex, int pageSize, int communeCode, int containVXHD )
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<ResponseData<PostOffice>>(Routes.CategoryEndpoints.GetCategoryPostOffice(pageIndex, pageSize, communeCode, containVXHD));
                return response;
            }
            catch ( Exception ex )
            {
                return new ResponseData<PostOffice>
                {
                    code = "error",
                    message = $"Failed connect to API: {ex}"
                };
            }
        }

        public async Task<ResponseData<Province>> GetCategoryProvinceAsync( int pageIndex, int pageSize )
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<ResponseData<Province>>(Routes.CategoryEndpoints.GetCategoryProvince(pageIndex, pageSize));
                return response;
            }
            catch ( Exception ex )
            {
                return new ResponseData<Province>
                {
                    code = "error",
                    message = $"Failed connect to API: {ex}"
                };
            }
        }

        public async Task<ResponseData<District>> GetCategoryDistrictAsync( int pageIndex, int pageSize , int ProvinceCode )
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<ResponseData<District>>(Routes.CategoryEndpoints.GetCategoryDistrict(pageIndex, pageSize, ProvinceCode));
                return response;
            }
            catch ( Exception ex )
            {
                return new ResponseData<District>
                {
                    code = "error",
                    message = $"Failed connect to API: {ex}"
                };
            }
        }

        public async Task<ResponseData<Commune>> GetCategoryCommuneAsync( int pageIndex, int pageSize, int DistrictCode )
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<ResponseData<Commune>>(Routes.CategoryEndpoints.GetCategoryCommune(pageIndex, pageSize, DistrictCode));
                return response;
            }
            catch ( Exception ex )
            {
                return new ResponseData<Commune>
                {
                    code = "error",
                    message = $"Failed connect to API: {ex}"
                };
            }
        }

        public async Task<ResponsePost> AddProvinceAsync( Province data )
        {
            try
            {
                var result = await _httpClient.PostAsJsonAsync(Routes.CategoryEndpoints.AddProvince,data);
                var response = await result.Content.ReadFromJsonAsync<ResponsePost>();
                return response;
            }
            catch ( Exception ex )
            {
                return new ResponsePost
                {
                    code = "ERROR",
                    message = $"Failed connect to API: {ex}"
                };
            }
        }

        public async Task<ResponsePost> AddDistrictAsync( District data )
        {
            try
            {
                var result = await _httpClient.PostAsJsonAsync(Routes.CategoryEndpoints.AddDistrict, data);
                var response = await result.Content.ReadFromJsonAsync<ResponsePost>();
                return response;
            }
            catch ( Exception ex )
            {
                return new ResponsePost
                {
                    code = "ERROR",
                    message = $"Failed connect to API: {ex}"
                };
            }
        }

        public async Task<ResponsePost> AddCommuneAsync( Commune data )
        {
            try
            {
                var result = await _httpClient.PostAsJsonAsync(Routes.CategoryEndpoints.AddCommune, data);
                var response = await result.Content.ReadFromJsonAsync<ResponsePost>();
                return response;
            }
            catch ( Exception ex )
            {
                return new ResponsePost
                {
                    code = "ERROR",
                    message = $"Failed connect to API: {ex}"
                };
            }
        }

        public async Task<ResponsePost> UpdateCategoryAsync( SingleUpdateRequest data )
        {
            try
            {
                var result = await _httpClient.PostAsJsonAsync(Routes.CategoryEndpoints.UpdateCategory, data);
                var response = await result.Content.ReadFromJsonAsync<ResponsePost>();
                return response;
            }
            catch ( Exception ex )
            {
                return new ResponsePost
                {
                    code = "ERROR",
                    message = $"Failed connect to API: {ex}"
                };
            }
        }

        public async Task<ResponsePost> DeleteCategoryAsync( SingleUpdateRequest data )
        {
            try
            {
                var result = await _httpClient.PostAsJsonAsync(Routes.CategoryEndpoints.DeleteCategory, data);
                var response = await result.Content.ReadFromJsonAsync<ResponsePost>();
                return response;
            }
            catch ( Exception ex )
            {
                return new ResponsePost
                {
                    code = "ERROR",
                    message = $"Failed connect to API: {ex}"
                };
            }
        }
    }
}
