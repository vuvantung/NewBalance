using Dapper.Oracle;
using Dapper;
using Microsoft.Extensions.Configuration;
using NewBalance.Domain.Entities.Doi_Soat.ExportCasReport;
using NewBalance.Infrastructure.OR.IRepository;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing.Printing;
using System.Text.RegularExpressions;

namespace NewBalance.Infrastructure.OR.Repository
{
    public class ExportCasRepository : IExportCasReportRepository
    {
        private readonly string _connectionString = "";
        private readonly IConfiguration _configuration;
        private OracleConnection con = new OracleConnection();

        public ExportCasRepository(IConfiguration configuration )
        {
            _connectionString = configuration.GetConnectionString("DBDS");
            _configuration = configuration;
            con = new OracleConnection(_connectionString);
        }

        public async Task<IEnumerable<DetailCasReport>> GetCasReportDetailDataAsync( int MaTinh, int TuNgay, int DenNgay,int PageIndex, int PageSize )
        {
            try
            {
                if ( con.State == ConnectionState.Closed ) await con.OpenAsync();
                var parameters = new OracleDynamicParameters();
                parameters.Add(name: "v_MATINH", value: MaTinh, dbType: OracleMappingType.Int32);
                parameters.Add(name: "v_TUNGAY", value: TuNgay, dbType: OracleMappingType.Int32);
                parameters.Add(name: "v_DENNGAY", value: DenNgay, dbType: OracleMappingType.Int32);
                parameters.Add(name: "v_PAGEINDEX", PageIndex, OracleMappingType.Int32);
                parameters.Add(name: "v_PAGESIZE", PageSize, OracleMappingType.Int32);
                parameters.Add(name: "v_ListStage", dbType: OracleMappingType.RefCursor, direction: ParameterDirection.Output);
                var queryResult = await con.QueryAsync<DetailCasReport>(
                    "ems.EXPORT_CAS_REPORT_PKG.GET_LIST_DETAIL_PROVINCE",
                    parameters,
                    commandType: CommandType.StoredProcedure);
                return queryResult;
            }
            catch
            {
                return Enumerable.Empty<DetailCasReport>();
            }
        }

        public async Task<IEnumerable<InforFileCasReport>> GetListFileCasReportAsync()
        {
            try
            {
                if ( con.State == ConnectionState.Closed ) await con.OpenAsync();
                var parameters = new OracleDynamicParameters();
                parameters.Add(name: "v_ListStage", dbType: OracleMappingType.RefCursor, direction: ParameterDirection.Output);
                var queryResult = await con.QueryAsync<InforFileCasReport>(
                    "ems.EXPORT_CAS_REPORT_PKG.GET_LIST_PROVINCE_EXPORT",
                    parameters,
                    commandType: CommandType.StoredProcedure);
                return queryResult;
            }
            catch ( Exception ex)
            {
                return Enumerable.Empty<InforFileCasReport>();
            }
        }

        public async Task<bool> UpdateFileCasReportSuccessAsync( int Id, string filePath )
        {
            try
            {
                if ( con.State == ConnectionState.Closed ) await con.OpenAsync();
                var parameters = new OracleDynamicParameters();
                parameters.Add(name: "v_ID", value: Id, dbType: OracleMappingType.Int32);
                parameters.Add(name: "v_FILEPATH", value: filePath, dbType: OracleMappingType.NVarchar2);
                var queryResult = await con.ExecuteAsync(
                    "ems.EXPORT_CAS_REPORT_PKG.UPDATE_SUCCESS_FILE",
                    parameters,
                    commandType: CommandType.StoredProcedure);
                return true;
            }
            catch ( Exception ex )
            {
                return false;
            }
        }
    }
}
