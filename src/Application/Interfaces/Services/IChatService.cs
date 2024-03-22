using NewBalance.Application.Responses.Identity;
using NewBalance.Shared.Wrapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using NewBalance.Application.Interfaces.Chat;
using NewBalance.Application.Models.Chat;

namespace NewBalance.Application.Interfaces.Services
{
    public interface IChatService
    {
        Task<Result<IEnumerable<ChatUserResponse>>> GetChatUsersAsync(string userId);

        Task<IResult> SaveMessageAsync(ChatHistory<IChatUser> message);

        Task<Result<IEnumerable<ChatHistoryResponse>>> GetChatHistoryAsync(string userId, string contactId);
    }
}