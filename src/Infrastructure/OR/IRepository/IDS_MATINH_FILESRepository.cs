using NewBalance.Application.Features.Doi_Soat.Danh_Muc.Queries.GetAll;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NewBalance.Application.Features.Doi_Soat;
namespace NewBalance.Infrastructure.OR.IRepository
{
    public interface IDS_MATINH_FILESRepository
    {
        Task<ResponseData<GetAllDS_MATINH_FILESResponse>> GetAllDS_MATINH_FILESResponse(string pageIndex, string pageSize, int ma_tinh, int tu_ngay, int den_ngay);
        Task<ResponseData<int>> DS_MATINH_FILES_MODIFY_STATUS(int id, string createby, string notes);
    }
}
