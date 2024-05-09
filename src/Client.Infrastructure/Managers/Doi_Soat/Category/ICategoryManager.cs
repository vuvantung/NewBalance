﻿using NewBalance.Application.Features.Doi_Soat;
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
        Task<ResponseData<PostOffice>> GetCategoryPostOfficeAsync( int pageIndex, int pageSize, int account );
        Task<ResponseData<Province>> GetCategoryProvinceAsync( int pageIndex, int pageSize );
        Task<ResponseData<District>> GetCategoryDistrictAsync( int pageIndex, int pageSize ,int ProvinceCode);
        Task<ResponseData<Commune>> GetCategoryCommuneAsync( int pageIndex, int pageSize, int DistrictCode );
        Task<ResponsePost> AddProvinceAsync( Province data );
        Task<ResponsePost> AddDistrictAsync( District data );
        Task<ResponsePost> AddCommuneAsync( Commune data );
        Task<ResponsePost> UpdateCategoryAsync( SingleUpdateRequest data );
        Task<ResponsePost> DeleteCategoryAsync( SingleUpdateRequest data );
    }
}
