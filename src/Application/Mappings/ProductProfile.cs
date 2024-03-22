using AutoMapper;
using NewBalance.Application.Features.Products.Commands.AddEdit;
using NewBalance.Domain.Entities.Catalog;

namespace NewBalance.Application.Mappings
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<AddEditProductCommand, Product>().ReverseMap();
        }
    }
}