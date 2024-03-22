using AutoMapper;
using NewBalance.Infrastructure.Models.Identity;
using NewBalance.Application.Responses.Identity;

namespace NewBalance.Infrastructure.Mappings
{
    public class RoleProfile : Profile
    {
        public RoleProfile()
        {
            CreateMap<RoleResponse, BlazorHeroRole>().ReverseMap();
        }
    }
}