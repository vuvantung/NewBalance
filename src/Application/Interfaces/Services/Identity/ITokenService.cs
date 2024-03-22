using NewBalance.Application.Interfaces.Common;
using NewBalance.Application.Requests.Identity;
using NewBalance.Application.Responses.Identity;
using NewBalance.Shared.Wrapper;
using System.Threading.Tasks;

namespace NewBalance.Application.Interfaces.Services.Identity
{
    public interface ITokenService : IService
    {
        Task<Result<TokenResponse>> LoginAsync(TokenRequest model);

        Task<Result<TokenResponse>> GetRefreshTokenAsync(RefreshTokenRequest model);
    }
}