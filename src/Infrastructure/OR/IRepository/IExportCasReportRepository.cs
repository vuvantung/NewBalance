using NewBalance.Application.Features.Doi_Soat;
using NewBalance.Domain.Entities.Doi_Soat.ExportCasReport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewBalance.Infrastructure.OR.IRepository
{
    public interface IExportCasReportRepository
    {
        Task<IEnumerable<InforFileCasReport>> GetListFileCasReportAsync();
        Task<IEnumerable<DetailCasReport>> GetCasReportDetailDataAsync( int MaTinh, int TuNgay, int DenNgay, int PageIndex, int PageSize );
        Task<bool> UpdateFileCasReportSuccessAsync(int Id, string filePath );
        Task<ReponsePost> ImportXmlCastDataAsync( string xml, string filePath );
    }
}
