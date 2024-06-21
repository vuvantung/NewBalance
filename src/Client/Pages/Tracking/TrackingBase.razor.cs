using Microsoft.AspNetCore.Components;
using NewBalance.Client.Infrastructure.Managers.Doi_Soat.Filter;
using NewBalance.Client.Infrastructure.Managers.Doi_Soat.Tracking;
using NewBalance.Client.XLS;
using NewBalance.Domain.Entities.Doi_Soat.Tracking;
using System.IO;
using System.Net.Http;

namespace NewBalance.Client.Pages.Tracking
{
    public partial class TrackingBase
    {
        [Inject] HttpClient _httpClientFilte { get; set; }
        [Inject] private ITrackingManager _trackingManager { get; set; }
        public bool _loading { get; set; } = false;
        private TrackingInfor TrackingInfor { get; set; } = new TrackingInfor();

        private string ItemCode { get; set; }  = string.Empty;
        private async void Tracking()
        {

            var res = await _trackingManager.TrackingItem(ItemCode.Trim().ToUpper());
            TrackingInfor = res.data;
            StateHasChanged();
            
        }
    }
}
