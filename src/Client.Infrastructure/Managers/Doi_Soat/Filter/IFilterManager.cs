using NewBalance.Domain.Entities.Doi_Soat.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewBalance.Client.Infrastructure.Managers.Doi_Soat.Filter
{
    public interface IFilterManager
    {
        Task<IEnumerable<FilterData>> GetAccountFilterAsync();
        Task<IEnumerable<FilterData>> GetTypeReportFilterAsync();
    }
}
