using NewBalance.Application.Features.Doi_Soat;
using NewBalance.Domain.Entities.Doi_Soat.Category;
using NewBalance.Domain.Entities.Doi_Soat.Filter;
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

        public async Task<ResponseData<PostOffice>> GetCategoryPostOfficeAsync( int pageIndex, int pageSize, int account )
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<ResponseData<PostOffice>>(Routes.CategoryEndpoints.GetCategoryPostOffice(pageIndex, pageSize, account));
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
    }
}
