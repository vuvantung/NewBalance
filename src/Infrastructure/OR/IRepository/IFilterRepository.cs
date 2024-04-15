using NewBalance.Domain.Entities.Doi_Soat.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewBalance.Infrastructure.OR.IRepository
{
    public interface IFilterRepository
    {
        Task<IEnumerable<FilterData>> GetFilterAccountAsync();
        Task<IEnumerable<FilterData>> GetFilterTypeReportAsync();
    }
}
