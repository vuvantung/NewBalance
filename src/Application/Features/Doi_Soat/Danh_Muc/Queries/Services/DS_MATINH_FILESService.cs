//using Blazored.LocalStorage;
using Azure.Core;
using EMS.Internal.BlazorWeb.Helper;
using Microsoft.AspNetCore.Http;

//using EMS.Internal.BlazorWeb.IServices;
using NewBalance.Application.Features.Doi_Soat;
using NewBalance.Application.Features.Doi_Soat.Danh_Muc.Queries.GetAll;
using NewBalance.Application.Features.Doi_Soat.Danh_Muc.Queries.IServices;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace NewBalance.Application.Features.Doi_Soat.Danh_Muc.Queries.Services
{
    public class DS_MATINH_FILESService : IDS_MATINH_FILESService
	{
		public HttpClient _httpClient { get; set; }

		public DS_MATINH_FILESService(HttpClient httpClient) //, ISyncLocalStorageService localStorage
        {
			_httpClient = httpClient;
			//_httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", localStorage.GetItem<string>("authToken").ToString());
		}

		public async Task<ResponseData<GetAllDS_MATINH_FILESResponse>> GetDS_MATINH_FILES(string pageIndex, string pageSize, int ma_tinh, int tu_ngay, int den_ngay)
		{
			try
			{

				var response = await _httpClient.GetFromJsonAsync<ResponseData<GetAllDS_MATINH_FILESResponse>>($"api/DS_MATINH_FILES/getlist?pageIndex={pageIndex}&pageSize={pageSize}&ma_tinh={ma_tinh}&tu_ngay={tu_ngay}&den_ngay={den_ngay}");

                if (response.code != "success")
				{
					response.message = $"Error: {response.message}";
				}

				return response;
			}
			catch (HttpRequestException ex)
			{
				var errorResponse = new ResponseData<GetAllDS_MATINH_FILESResponse>
                {
					code = "error",
					message = $"Error: {ex.Message}"
				};

				return errorResponse;
			}
		}
        public async Task<ResponseData<int>> DS_MATINH_FILES_MODIFY_STATUS(List<int> _list, string createby)
        {
            try
            {
                var data = new ResponseData<int>()
                {
                    message = createby,
                    data = _list
                };

                var result = await _httpClient.PostAsJsonAsync($"api/DS_MATINH_FILES/modify_status", data);

                var response = await result.Content.ReadFromJsonAsync<ResponseData<int>>();

                if (response.code != "success")
                {
                    response.message = $"Error: {response.message}";
                }

                return response;
            }
            catch (HttpRequestException ex)
            {
                var errorResponse = new ResponseData<int>
                {
                    code = "error",
                    message = $"Error: {ex.Message}"
                };

                return errorResponse;
            }
        }

        public async Task<ResponseData<int>> UploadFile(MultipartFormDataContent content)
        {
            try
            {

                var result = await _httpClient.PostAsync($"api/DS_MATINH_FILES/upload_file", content);

                var response = await result.Content.ReadFromJsonAsync<ResponseData<int>>();

                if (response.code != "success")
                {
                    response.message = $"Error: {response.message}";
                }

                return response;
            }
            catch (HttpRequestException ex)
            {
                var errorResponse = new ResponseData<int>
                {
                    code = "error",
                    message = $"Error: {ex.Message}"
                };

                return errorResponse;
            }
        }
    }
}