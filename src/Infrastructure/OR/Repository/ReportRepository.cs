using Dapper.Oracle;
using Dapper;
using Microsoft.Extensions.Configuration;
using NewBalance.Application.Features.Doi_Soat;
using NewBalance.Domain.Entities.Doi_Soat.ExportCasReport;
using NewBalance.Domain.Entities.Doi_Soat.Report;
using NewBalance.Infrastructure.OR.IRepository;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace NewBalance.Infrastructure.OR.Repository
{
    public class ReportRepository : IReportRepository
    {
        private readonly string _connectionString = "";
        private readonly IConfiguration _configuration;
        private OracleConnection con = new OracleConnection();

        public ReportRepository( IConfiguration configuration )
        {
            _connectionString = configuration.GetConnectionString("DBDS");
            _configuration = configuration;
            con = new OracleConnection(_connectionString);
        }
        public async Task<ResponseData<BDT_TH01>> GetDataBDT_01ReportAsync( int acount, int fromDate, int toDate )
        {
            try
            {
                if ( con.State == ConnectionState.Closed ) await con.OpenAsync();
                var parameters = new OracleDynamicParameters();
                parameters.Add(name: "v_Account", value: acount, dbType: OracleMappingType.Int32);
                parameters.Add(name: "v_FromDate", value: fromDate, dbType: OracleMappingType.Int32);
                parameters.Add(name: "v_ToDate", value: toDate, dbType: OracleMappingType.Int32);
                parameters.Add(name: "v_ListStage", dbType: OracleMappingType.RefCursor, direction: ParameterDirection.Output);
                var queryResult = await con.QueryAsync<BDT_TH01>(
                    "ems.REPORT_REVENUE_PKG.REPORT_TOTAL_REVENUE_COS",
                    parameters,
                    commandType: CommandType.StoredProcedure);
                return new ResponseData<BDT_TH01> 
                {
                    code = "success",
                    message = "Lấy dữ liêu thành công",
                    data = queryResult.ToList()
                };
            }
            catch( Exception ex )
            {
                return new ResponseData<BDT_TH01>
                {
                    code = "error",
                    message = ex.Message,
                    data = null
                };
            }
        }
    }
}
