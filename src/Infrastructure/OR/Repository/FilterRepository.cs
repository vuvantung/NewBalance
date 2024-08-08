using Dapper.Oracle;
using Dapper;
using Microsoft.Extensions.Configuration;
using NewBalance.Domain.Entities.Doi_Soat.ExportCasReport;
using NewBalance.Domain.Entities.Doi_Soat.Filter;
using NewBalance.Infrastructure.OR.IRepository;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewBalance.Infrastructure.OR.Repository
{
    public class FilterRepository : IFilterRepository
    {
        private readonly string _connectionString = "";
        private readonly IConfiguration _configuration;
        private OracleConnection con = new OracleConnection();

        public FilterRepository( IConfiguration configuration )
        {
            _connectionString = configuration.GetConnectionString("DBDS");
            _configuration = configuration;
            con = new OracleConnection(_connectionString);
        }
        public async Task<IEnumerable<FilterData>> GetFilterAccountAsync()
        {
            try
            {
                if ( con.State == ConnectionState.Closed ) await con.OpenAsync();
                var parameters = new OracleDynamicParameters();
                parameters.Add(name: "v_ListStage", dbType: OracleMappingType.RefCursor, direction: ParameterDirection.Output);
                var queryResult = await con.QueryAsync<FilterData>(
                    "ems.FILTER_PKG.FILTER_ACCOUNT",
                    parameters,
                    commandType: CommandType.StoredProcedure);
                return queryResult;
            }
            catch ( Exception ex )
            {
                return Enumerable.Empty<FilterData>();
            }
        }

        public async Task<IEnumerable<FilterData>> GetFilterDistrictAsync( string ProvinceCode )
        {
            try
            {
                if ( con.State == ConnectionState.Closed ) await con.OpenAsync();
                var parameters = new OracleDynamicParameters();
                parameters.Add(name: "v_ProvinceCode", ProvinceCode, dbType: OracleMappingType.Varchar2);
                parameters.Add(name: "v_ListStage", dbType: OracleMappingType.RefCursor, direction: ParameterDirection.Output);
                var queryResult = await con.QueryAsync<FilterData>(
                    "ems.FILTER_PKG.FILTER_DISTRICT",
                    parameters,
                    commandType: CommandType.StoredProcedure);
                return queryResult;
            }
            catch ( Exception ex )
            {
                return Enumerable.Empty<FilterData>();
            }
        }

        public async Task<IEnumerable<FilterData>> GetFilterProvinceAsync()
        {
            try
            {
                if ( con.State == ConnectionState.Closed ) await con.OpenAsync();
                var parameters = new OracleDynamicParameters();
                parameters.Add(name: "v_ListStage", dbType: OracleMappingType.RefCursor, direction: ParameterDirection.Output);
                var queryResult = await con.QueryAsync<FilterData>(
                    "ems.FILTER_PKG.FILTER_PROVINCE",
                    parameters,
                    commandType: CommandType.StoredProcedure);
                return queryResult;
            }
            catch ( Exception ex )
            {
                return Enumerable.Empty<FilterData>();
            }
        }

        public async Task<IEnumerable<FilterData>> GetFilterTypeReportAsync()
        {
            try
            {
                if ( con.State == ConnectionState.Closed ) await con.OpenAsync();
                var parameters = new OracleDynamicParameters();
                parameters.Add(name: "v_ListStage", dbType: OracleMappingType.RefCursor, direction: ParameterDirection.Output);
                var queryResult = await con.QueryAsync<FilterData>(
                    "ems.FILTER_PKG.FILTER_TYPE_REPORT",
                    parameters,
                    commandType: CommandType.StoredProcedure);
                return queryResult;
            }
            catch ( Exception ex )
            {
                return Enumerable.Empty<FilterData>();
            }
        }
    }
}
