using System.Collections.Generic;
using System.Threading.Tasks;
using NewBalance.Application.Interfaces.Common;
using NewBalance.Application.Requests.Identity;
using NewBalance.Application.Responses.Identity;
using NewBalance.Shared.Wrapper;

namespace NewBalance.Application.Interfaces.Services.Identity
{
    public interface IRoleClaimService : IService
    {
        Task<Result<List<RoleClaimResponse>>> GetAllAsync();

        Task<int> GetCountAsync();

        Task<Result<RoleClaimResponse>> GetByIdAsync(int id);

        Task<Result<List<RoleClaimResponse>>> GetAllByRoleIdAsync(string roleId);

        Task<Result<string>> SaveAsync(RoleClaimRequest request);

        Task<Result<string>> DeleteAsync(int id);
    }
}