using AutoMapper;
using NewBalance.Application.Features.Brands.Commands.AddEdit;
using NewBalance.Application.Features.Brands.Queries.GetAll;
using NewBalance.Application.Features.Brands.Queries.GetById;
using NewBalance.Domain.Entities.Catalog;

namespace NewBalance.Application.Mappings
{
    public class BrandProfile : Profile
    {
        public BrandProfile()
        {
            CreateMap<AddEditBrandCommand, Brand>().ReverseMap();
            CreateMap<GetBrandByIdResponse, Brand>().ReverseMap();
            CreateMap<GetAllBrandsResponse, Brand>().ReverseMap();
        }
    }
}