using NewBalance.Application.Features.Documents.Commands.AddEdit;
using NewBalance.Application.Features.Documents.Queries.GetAll;
using NewBalance.Application.Requests.Documents;
using NewBalance.Shared.Wrapper;
using System.Threading.Tasks;
using NewBalance.Application.Features.Documents.Queries.GetById;

namespace NewBalance.Client.Infrastructure.Managers.Misc.Document
{
    public interface IDocumentManager : IManager
    {
        Task<PaginatedResult<GetAllDocumentsResponse>> GetAllAsync(GetAllPagedDocumentsRequest request);

        Task<IResult<GetDocumentByIdResponse>> GetByIdAsync(GetDocumentByIdQuery request);

        Task<IResult<int>> SaveAsync(AddEditDocumentCommand request);

        Task<IResult<int>> DeleteAsync(int id);
    }
}