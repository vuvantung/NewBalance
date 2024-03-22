using System.Collections.Generic;
using System.Threading.Tasks;
using NewBalance.Application.Features.DocumentTypes.Commands.AddEdit;
using NewBalance.Application.Features.DocumentTypes.Queries.GetAll;
using NewBalance.Shared.Wrapper;

namespace NewBalance.Client.Infrastructure.Managers.Misc.DocumentType
{
    public interface IDocumentTypeManager : IManager
    {
        Task<IResult<List<GetAllDocumentTypesResponse>>> GetAllAsync();

        Task<IResult<int>> SaveAsync(AddEditDocumentTypeCommand request);

        Task<IResult<int>> DeleteAsync(int id);

        Task<IResult<string>> ExportToExcelAsync(string searchString = "");
    }
}