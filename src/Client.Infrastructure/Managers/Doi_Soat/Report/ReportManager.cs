using NewBalance.Application.Features.Doi_Soat;
using NewBalance.Domain.Entities.Doi_Soat.Filter;
using NewBalance.Domain.Entities.Doi_Soat.Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace NewBalance.Client.Infrastructure.Managers.Doi_Soat.Report
{
    public class ReportManager : IReportManager
    {
        private readonly HttpClient _httpClient;

        public ReportManager( HttpClient httpClient )
        {
            _httpClient = httpClient;
        }

        public async Task<ResponseData<BDT_TH01>> GetDataBDT_01ReportAsync( int account, int fromDate, int toDate )
        {
            var response = await _httpClient.GetFromJsonAsync<ResponseData<BDT_TH01>>(Routes.ReportsEndpoints.GetBDT_01Report(account,fromDate,toDate));
            return response;
        }
    }
}
