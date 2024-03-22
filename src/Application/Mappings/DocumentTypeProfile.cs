using AutoMapper;
using NewBalance.Application.Features.DocumentTypes.Commands.AddEdit;
using NewBalance.Application.Features.DocumentTypes.Queries.GetAll;
using NewBalance.Application.Features.DocumentTypes.Queries.GetById;
using NewBalance.Domain.Entities.Misc;

namespace NewBalance.Application.Mappings
{
    public class DocumentTypeProfile : Profile
    {
        public DocumentTypeProfile()
        {
            CreateMap<AddEditDocumentTypeCommand, DocumentType>().ReverseMap();
            CreateMap<GetDocumentTypeByIdResponse, DocumentType>().ReverseMap();
            CreateMap<GetAllDocumentTypesResponse, DocumentType>().ReverseMap();
        }
    }
}