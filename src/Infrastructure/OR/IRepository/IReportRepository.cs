using NewBalance.Application.Features.Doi_Soat;
using NewBalance.Domain.Entities.Doi_Soat.Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewBalance.Infrastructure.OR.IRepository
{
    public interface IReportRepository
    {
        Task<ResponseData<BDT_TH01>> GetDataBDT_01ReportAsync(int acount, int fromDate, int toDate);
    }
}
