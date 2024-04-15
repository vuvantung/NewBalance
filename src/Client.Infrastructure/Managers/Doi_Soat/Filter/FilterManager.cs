using Azure.Core;
using NewBalance.Domain.Entities.Doi_Soat.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace NewBalance.Client.Infrastructure.Managers.Doi_Soat.Filter
{
    public class FilterManager : IFilterManager
    {
        private readonly HttpClient _httpClient;

        public FilterManager( HttpClient httpClient )
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<FilterData>> GetAccountFilterAsync()
        {
            var response = await _httpClient.GetFromJsonAsync< IEnumerable<FilterData>>(Routes.FiltersEndpoints.GetFilterAccount);
            if ( response.Any() )
            {
                return response;
            }
            else
            {

                return Enumerable.Empty<FilterData>();
            }
        }

        public async Task<IEnumerable<FilterData>> GetTypeReportFilterAsync()
        {
            var response = await _httpClient.GetFromJsonAsync<IEnumerable<FilterData>>(Routes.FiltersEndpoints.GetFilterTypeReport);
            if ( response.Any() )
            {
                return response;
            }
            else
            {

                return Enumerable.Empty<FilterData>();
            }
        }
    }
}
