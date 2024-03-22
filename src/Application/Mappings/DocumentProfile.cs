using AutoMapper;
using NewBalance.Application.Features.Documents.Commands.AddEdit;
using NewBalance.Application.Features.Documents.Queries.GetById;
using NewBalance.Domain.Entities.Misc;

namespace NewBalance.Application.Mappings
{
    public class DocumentProfile : Profile
    {
        public DocumentProfile()
        {
            CreateMap<AddEditDocumentCommand, Document>().ReverseMap();
            CreateMap<GetDocumentByIdResponse, Document>().ReverseMap();
        }
    }
}