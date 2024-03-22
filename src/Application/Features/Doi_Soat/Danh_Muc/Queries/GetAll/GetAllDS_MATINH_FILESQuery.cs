using NewBalance.Application.Extensions;
using NewBalance.Application.Interfaces.Repositories;
using NewBalance.Application.Interfaces.Services;
using NewBalance.Application.Specifications.Misc;
using NewBalance.Shared.Wrapper;
using MediatR;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using NewBalance.Application.Specifications.Doi_Soat.Danh_Muc;
using NewBalance.Domain.Entities.Doi_Soat.Danh_Muc;

namespace NewBalance.Application.Features.Doi_Soat.Danh_Muc.Queries.GetAll
{
    public class GetAllDS_MATINH_FILESQuery : IRequest<PaginatedResult<GetAllDS_MATINH_FILESResponse>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string SearchString { get; set; }

        public GetAllDS_MATINH_FILESQuery(int pageNumber, int pageSize, string searchString)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
            SearchString = searchString;
        }
    }

    internal class GetAllDS_MATINH_FILESQueryHandler : IRequestHandler<GetAllDS_MATINH_FILESQuery, PaginatedResult<GetAllDS_MATINH_FILESResponse>>
    {
        private readonly IUnitOfWork<int> _unitOfWork;

        private readonly ICurrentUserService _currentUserService;

        public GetAllDS_MATINH_FILESQueryHandler(IUnitOfWork<int> unitOfWork, ICurrentUserService currentUserService)
        {
            _unitOfWork = unitOfWork;
            _currentUserService = currentUserService;
        }

        public async Task<PaginatedResult<GetAllDS_MATINH_FILESResponse>> Handle(GetAllDS_MATINH_FILESQuery request, CancellationToken cancellationToken)
        {
            Expression<Func<DS_MATINH_FILES, GetAllDS_MATINH_FILESResponse>> expression = e => new GetAllDS_MATINH_FILESResponse
            {
                ID = e.ID,
                MATINH = e.MATINH,
                TENTINH = e.TENTINH,
                THANG = e.THANG,
                DATECREATED = e.DATECREATED,
                DATEUPDATED = e.DATEUPDATED,
                LINKS = e.LINKS,
                CREATEBY = e.CREATEBY,
                NOTES = e.NOTES
            };
            var docSpec = new DS_MATINH_FILESFilterSpecification(request.SearchString, _currentUserService.UserId);
            var data = await _unitOfWork.Repository<DS_MATINH_FILES>().Entities
               .Specify(docSpec)
               .Select(expression)
               .ToPaginatedListAsync(request.PageNumber, request.PageSize);
            return data;
        }
    }
}