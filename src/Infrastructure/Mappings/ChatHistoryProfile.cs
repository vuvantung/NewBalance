using AutoMapper;
using NewBalance.Application.Interfaces.Chat;
using NewBalance.Application.Models.Chat;
using NewBalance.Infrastructure.Models.Identity;

namespace NewBalance.Infrastructure.Mappings
{
    public class ChatHistoryProfile : Profile
    {
        public ChatHistoryProfile()
        {
            CreateMap<ChatHistory<IChatUser>, ChatHistory<BlazorHeroUser>>().ReverseMap();
        }
    }
}