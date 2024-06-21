using NewBalance.Application.Features.Doi_Soat;
using NewBalance.Domain.Entities.Doi_Soat.Tracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewBalance.Client.Infrastructure.Managers.Doi_Soat.Tracking
{
    public interface ITrackingManager
    {
        Task<ResponseSingle<TrackingInfor>> TrackingItem( string ItemCode );
    }
}
