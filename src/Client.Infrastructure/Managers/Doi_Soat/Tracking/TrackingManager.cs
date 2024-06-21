using NewBalance.Application.Features.Doi_Soat;
using NewBalance.Domain.Entities.Doi_Soat.Tracking;
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
        }
        public async Task<ResponseSingle<TrackingInfor>> TrackingItem( string ItemCode )
        {
            var response = await _httpClient.GetFromJsonAsync<ResponseSingle<TrackingInfor>>(Routes.TrackingEndPoints.TrackingItem(ItemCode));
            return response;
        }
    }
}
