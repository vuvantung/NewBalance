using NewBalance.Application.Features.Doi_Soat;
using NewBalance.Application.Requests.Doi_soat;
using NewBalance.Domain.Entities.Doi_Soat.Tracking;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace NewBalance.Client.Infrastructure.Managers.Doi_Soat.Tracking
{
    public class TrackingManager : ITrackingManager
    {
        private readonly HttpClient _httpClient;

        public TrackingManager( HttpClient httpClient )
        {
            _httpClient = httpClient;
            _httpClient.Timeout = TimeSpan.FromMinutes(5);
        }
        public async Task<ResponseSingle<TrackingInfor>> TrackingItem( string ItemCode )
        {
            var response = await _httpClient.GetFromJsonAsync<ResponseSingle<TrackingInfor>>(Routes.TrackingEndPoints.TrackingItem(ItemCode));
            return response;
        }

        public async Task<ResponseData<LastStatusItem>> TrackingSLL( TrackingSLLRequest request )
        {
            try
            {
                //_httpClient.Timeout = TimeSpan.FromMinutes(30);
                var result = await _httpClient.PostAsJsonAsync(Routes.TrackingEndPoints.TrackingSLL, request);
                var response = await result.Content.ReadFromJsonAsync<ResponseData<LastStatusItem>>();
                return response;
            }
            catch ( Exception ex )
            {
                return new ResponseData<LastStatusItem>
                {
                    code = "ERROR",
                    message = $"Failed connect to API: {ex}"
                };
            }
            
        }
    }
}
