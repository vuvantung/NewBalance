using NewBalance.Application.Requests.Identity;
using NewBalance.Application.Responses.Identity;
using NewBalance.Shared.Wrapper;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NewBalance.Client.Infrastructure.Managers.Identity.Roles
{
    public interface IRoleManager : IManager
    {
        Task<IResult<List<RoleResponse>>> GetRolesAsync();

        Task<IResult<string>> SaveAsync(RoleRequest role);

        Task<IResult<string>> DeleteAsync(string id);

        Task<IResult<PermissionResponse>> GetPermissionsAsync(string roleId);

        Task<IResult<string>> UpdatePermissionsAsync(PermissionRequest request);
    }
}