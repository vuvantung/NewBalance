using NewBalance.Application.Models.Chat;
using NewBalance.Application.Responses.Identity;
using NewBalance.Shared.Wrapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using NewBalance.Application.Interfaces.Chat;

namespace NewBalance.Client.Infrastructure.Managers.Communication
{
    public interface IChatManager : IManager
    {
        Task<IResult<IEnumerable<ChatUserResponse>>> GetChatUsersAsync();

        Task<IResult> SaveMessageAsync(ChatHistory<IChatUser> chatHistory);

        Task<IResult<IEnumerable<ChatHistoryResponse>>> GetChatHistoryAsync(string cId);
    }
}