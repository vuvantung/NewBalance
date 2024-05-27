using NewBalance.Application.Features.Doi_Soat;
using NewBalance.Domain.Entities.Doi_Soat.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewBalance.Client.Infrastructure.Managers.Doi_Soat.Category
{
    public interface ICategoryManager
    {
        Task<ResponseData<Account>> GetCategoryAccountAsync( int pageIndex, int pageSize, int account );
        Task<ResponseData<GiaVonChuan>> GetCategoryGiaVonChuanAsync( int pageIndex, int pageSize, int account );
        Task<ResponseData<GiaVonChuanNT>> GetCategoryGiaVonChuanNTAsync( int pageIndex, int pageSize, int account );
        Task<ResponseData<PostOffice>> GetCategoryPostOfficeAsync(int pageIndex, int pageSize, int ProvinceCode, int DistrictCode, int communeCode, int containVXHD );
        Task<ResponseData<Province>> GetCategoryProvinceAsync( int pageIndex, int pageSize );
        Task<ResponseData<District>> GetCategoryDistrictAsync( int pageIndex, int pageSize ,int ProvinceCode);
        Task<ResponseData<Commune>> GetCategoryCommuneAsync( int pageIndex, int pageSize, int DistrictCode );
        Task<ResponseData<MapProvinceDistrictCommune>> GetAllCategoryProvinceDistrictCommuneAsync( );
        Task<ResponsePost> AddProvinceAsync( Province data );
        Task<ResponsePost> AddDistrictAsync( District data );
        Task<ResponsePost> AddCommuneAsync( Commune data );
        Task<ResponsePost> AddPostOfficeAsync( PostOffice data );
        Task<ResponsePost> UpdateCategoryAsync( SingleUpdateRequest data );
        Task<ResponsePost> DeleteCategoryAsync( SingleUpdateRequest data );
        Task<ResponseData<DM_Dich_Vu>> GetCategoryDM_Dich_VuAsync(int pageIndex, int pageSize, int account);

        Task<ResponsePost> AddDM_Dich_VuAsync(DM_Dich_Vu data);
        Task<ResponsePost> AddGiaVonChuanAsync(GiaVonChuan data);
        Task<ResponsePost> UpdatePostOfficeAsync( PostOffice data );
        Task<ResponsePost> UpdateProvinceAsync( Province data );
        Task<ResponsePost> UpdateDistrictAsync( District data );
        Task<ResponsePost> UpdateCommuneAsync( Commune data );

    }
}
