using AutoMapper;
using NewBalance.Infrastructure.Models.Audit;
using NewBalance.Application.Responses.Audit;

namespace NewBalance.Infrastructure.Mappings
{
    public class AuditProfile : Profile
    {
        public AuditProfile()
        {
            CreateMap<AuditResponse, Audit>().ReverseMap();
        }
    }
}