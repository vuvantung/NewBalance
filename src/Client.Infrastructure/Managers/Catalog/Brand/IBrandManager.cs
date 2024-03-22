using NewBalance.Application.Features.Brands.Queries.GetAll;
using NewBalance.Shared.Wrapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using NewBalance.Application.Features.Brands.Commands.AddEdit;

namespace NewBalance.Client.Infrastructure.Managers.Catalog.Brand
{
    public interface IBrandManager : IManager
    {
        Task<IResult<List<GetAllBrandsResponse>>> GetAllAsync();

        Task<IResult<int>> SaveAsync(AddEditBrandCommand request);

        Task<IResult<int>> DeleteAsync(int id);

        Task<IResult<string>> ExportToExcelAsync(string searchString = "");
    }
}