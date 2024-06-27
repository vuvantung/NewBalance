using NewBalance.Application.Features.Doi_Soat;
using NewBalance.Application.Requests.Doi_soat;
using NewBalance.Domain.Entities.Doi_Soat.Tracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewBalance.Infrastructure.OR.IRepository
{
    public interface ITrackingRepository
    {
        Task<ResponseSingle<TrackingInfor>> TrackingItem( string ItemCode );
        Task<ResponseData<LastStatusItem>> TrackingSLL( TrackingSLLRequest request );
    }
}
