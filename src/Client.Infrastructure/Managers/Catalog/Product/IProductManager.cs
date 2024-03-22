using NewBalance.Application.Features.Products.Commands.AddEdit;
using NewBalance.Application.Features.Products.Queries.GetAllPaged;
using NewBalance.Application.Requests.Catalog;
using NewBalance.Shared.Wrapper;
using System.Threading.Tasks;

namespace NewBalance.Client.Infrastructure.Managers.Catalog.Product
{
    public interface IProductManager : IManager
    {
        Task<PaginatedResult<GetAllPagedProductsResponse>> GetProductsAsync(GetAllPagedProductsRequest request);

        Task<IResult<string>> GetProductImageAsync(int id);

        Task<IResult<int>> SaveAsync(AddEditProductCommand request);

        Task<IResult<int>> DeleteAsync(int id);

        Task<IResult<string>> ExportToExcelAsync(string searchString = "");
    }
}