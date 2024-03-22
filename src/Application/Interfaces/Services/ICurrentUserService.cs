using NewBalance.Application.Interfaces.Common;

namespace NewBalance.Application.Interfaces.Services
{
    public interface ICurrentUserService : IService
    {
        string UserId { get; }
    }
}