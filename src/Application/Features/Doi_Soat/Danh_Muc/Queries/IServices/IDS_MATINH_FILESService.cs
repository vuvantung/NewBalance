using NewBalance.Application.Features.Doi_Soat.Danh_Muc.Queries.GetAll;
using NewBalance.Application.Features.Doi_Soat.Danh_Muc.Queries.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace NewBalance.Application.Features.Doi_Soat.Danh_Muc.Queries.IServices
{
    public interface IDS_MATINH_FILESService
	{
        Task<ResponseData<GetAllDS_MATINH_FILESResponse>> GetDS_MATINH_FILES(string pageIndex, string pageSize, int ma_tinh, int tu_ngay, int den_ngay);
        Task<ResponseData<int>> DS_MATINH_FILES_MODIFY_STATUS(List<int> _list, string createby);
    }
}