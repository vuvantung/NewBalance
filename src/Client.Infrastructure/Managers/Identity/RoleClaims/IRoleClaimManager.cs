using System.Collections.Generic;
using System.Threading.Tasks;
using NewBalance.Application.Requests.Identity;
using NewBalance.Application.Responses.Identity;
using NewBalance.Shared.Wrapper;

namespace NewBalance.Client.Infrastructure.Managers.Identity.RoleClaims
{
    public interface IRoleClaimManager : IManager
    {
        Task<IResult<List<RoleClaimResponse>>> GetRoleClaimsAsync();

        Task<IResult<List<RoleClaimResponse>>> GetRoleClaimsByRoleIdAsync(string roleId);

        Task<IResult<string>> SaveAsync(RoleClaimRequest role);

        Task<IResult<string>> DeleteAsync(string id);
    }
}