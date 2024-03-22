using NewBalance.Application.Responses.Audit;
using NewBalance.Shared.Wrapper;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NewBalance.Client.Infrastructure.Managers.Audit
{
    public interface IAuditManager : IManager
    {
        Task<IResult<IEnumerable<AuditResponse>>> GetCurrentUserTrailsAsync();

        Task<IResult<string>> DownloadFileAsync(string searchString = "", bool searchInOldValues = false, bool searchInNewValues = false);
    }
}