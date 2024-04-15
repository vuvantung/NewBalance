using NewBalance.Application.Features.Doi_Soat;
using NewBalance.Domain.Entities.Doi_Soat.Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewBalance.Client.Infrastructure.Managers.Doi_Soat.Report
{
    public interface IReportManager
    {
        Task<ResponseData<BDT_TH01>> GetDataBDT_01ReportAsync( int account, int fromDate, int toDate );
    }
}
