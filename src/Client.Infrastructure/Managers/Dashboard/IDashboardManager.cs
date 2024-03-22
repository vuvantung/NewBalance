using NewBalance.Shared.Wrapper;
using System.Threading.Tasks;
using NewBalance.Application.Features.Dashboards.Queries.GetData;

namespace NewBalance.Client.Infrastructure.Managers.Dashboard
{
    public interface IDashboardManager : IManager
    {
        Task<IResult<DashboardDataResponse>> GetDataAsync();
    }
}