using Microsoft.AspNetCore.Components;
using NewBalance.Domain.Entities.Doi_Soat.Tracking;

namespace NewBalance.Client.Pages.Tracking
{
    public partial class TrackingSingle
    {
        [Parameter] public TrackingInfor TrackingInfor { get; set; } = new TrackingInfor();
    }
}
