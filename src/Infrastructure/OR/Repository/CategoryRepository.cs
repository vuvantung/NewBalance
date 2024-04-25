using Dapper.Oracle;
using Dapper;
using Microsoft.Extensions.Configuration;
using NewBalance.Application.Features.Doi_Soat;
using NewBalance.Application.Features.Doi_Soat.Danh_Muc.Queries.GetAll;
using NewBalance.Domain.Entities.Doi_Soat.Category;
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
    public class CategoryRepository : ICategoryRepository
    {
        private readonly string _connectionString = "";
        private readonly IConfiguration _configuration;
        private OracleConnection con = new OracleConnection();

        public CategoryRepository( IConfiguration configuration )
        {
            _connectionString = configuration.GetConnectionString("DBDS");
            _configuration = configuration;
            con = new OracleConnection(_connectionString);
        }
        public async Task<ResponseData<Account>> GetCategoryAccountAsync( int pageIndex, int pageSize, int account )
        {
            if ( con.State == ConnectionState.Closed ) await con.OpenAsync();
            var parameters = new OracleDynamicParameters();
            parameters.Add("v_ACCOUNT", account, OracleMappingType.Int32);
            parameters.Add("v_PAGEINDEX", pageIndex, OracleMappingType.Int32);
            parameters.Add("v_PAGESIZE", pageSize, OracleMappingType.Int32);
            parameters.Add("v_TOTAL", dbType: OracleMappingType.Int32, direction: ParameterDirection.Output);
            parameters.Add("v_ListStage", dbType: OracleMappingType.RefCursor, direction: ParameterDirection.Output);

            try
            {
                var queryResult = await con.QueryAsync<Account>(
                    "CATEGORY_PKG.GET_CATEGORY_ACCOUNT",
                    parameters,
                    commandType: CommandType.StoredProcedure);

                int total = parameters.Get<int>("v_TOTAL");

                var response = new ResponseData<Account>
                {
                    code = "success",
                    message = "Thành công",
                    total = total,
                    data = queryResult.ToList(),
                };
                return response;
            }
            catch ( OracleException ex )
            {
                var errorResponse = new ResponseData<Account>
                {
                    code = "error",
                    message = ex.Message
                };
                return errorResponse;
            }
            catch ( Exception ex )
            {
                var errorResponse = new ResponseData<Account>
                {
                    code = "error",
                    message = ex.Message
                };
                return errorResponse;
            }
        }

        public async Task<ResponseData<GiaVonChuan>> GetCategoryGiaVonChuanAsync( int pageIndex, int pageSize, int account )
        {
            if ( con.State == ConnectionState.Closed ) await con.OpenAsync();
            var parameters = new OracleDynamicParameters();
            parameters.Add("v_ACCOUNT", account, OracleMappingType.Int32);
            parameters.Add("v_PAGEINDEX", pageIndex, OracleMappingType.Int32);
            parameters.Add("v_PAGESIZE", pageSize, OracleMappingType.Int32);
            parameters.Add("v_TOTAL", dbType: OracleMappingType.Int32, direction: ParameterDirection.Output);
            parameters.Add("v_ListStage", dbType: OracleMappingType.RefCursor, direction: ParameterDirection.Output);

            try
            {
                var queryResult = await con.QueryAsync<GiaVonChuan>(
                    "CATEGORY_PKG.GET_CATEGORY_GIAVONCHUAN",
                    parameters,
                    commandType: CommandType.StoredProcedure);

                int total = parameters.Get<int>("v_TOTAL");

                var response = new ResponseData<GiaVonChuan>
                {
                    code = "success",
                    message = "Thành công",
                    total = total,
                    data = queryResult.ToList(),
                };
                return response;
            }
            catch ( OracleException ex )
            {
                var errorResponse = new ResponseData<GiaVonChuan>
                {
                    code = "error",
                    message = ex.Message
                };
                return errorResponse;
            }
            catch ( Exception ex )
            {
                var errorResponse = new ResponseData<GiaVonChuan>
                {
                    code = "error",
                    message = ex.Message
                };
                return errorResponse;
            }
        }

        public async Task<ResponseData<GiaVonChuanNT>> GetCategoryGiaVonChuanNTAsync( int pageIndex, int pageSize, int account )
        {
            if ( con.State == ConnectionState.Closed ) await con.OpenAsync();
            var parameters = new OracleDynamicParameters();
            parameters.Add("v_ACCOUNT", account, OracleMappingType.Int32);
            parameters.Add("v_PAGEINDEX", pageIndex, OracleMappingType.Int32);
            parameters.Add("v_PAGESIZE", pageSize, OracleMappingType.Int32);
            parameters.Add("v_TOTAL", dbType: OracleMappingType.Int32, direction: ParameterDirection.Output);
            parameters.Add("v_ListStage", dbType: OracleMappingType.RefCursor, direction: ParameterDirection.Output);

            try
            {
                var queryResult = await con.QueryAsync<GiaVonChuanNT>(
                    "CATEGORY_PKG.GET_CATEGORY_GIAVONCHUAN_NT",
                    parameters,
                    commandType: CommandType.StoredProcedure);

                int total = parameters.Get<int>("v_TOTAL");

                var response = new ResponseData<GiaVonChuanNT>
                {
                    code = "success",
                    message = "Thành công",
                    total = total,
                    data = queryResult.ToList(),
                };
                return response;
            }
            catch ( OracleException ex )
            {
                var errorResponse = new ResponseData<GiaVonChuanNT>
                {
                    code = "error",
                    message = ex.Message
                };
                return errorResponse;
            }
            catch ( Exception ex )
            {
                var errorResponse = new ResponseData<GiaVonChuanNT>
                {
                    code = "error",
                    message = ex.Message
                };
                return errorResponse;
            }
        }

        public async Task<ResponseData<PostOffice>> GetCategoryPostOfficeAsync( int pageIndex, int pageSize, int account )
        {
            if ( con.State == ConnectionState.Closed ) await con.OpenAsync();
            var parameters = new OracleDynamicParameters();
            parameters.Add("v_ACCOUNT", account, OracleMappingType.Int32);
            parameters.Add("v_PAGEINDEX", pageIndex, OracleMappingType.Int32);
            parameters.Add("v_PAGESIZE", pageSize, OracleMappingType.Int32);
            parameters.Add("v_TOTAL", dbType: OracleMappingType.Int32, direction: ParameterDirection.Output);
            parameters.Add("v_ListStage", dbType: OracleMappingType.RefCursor, direction: ParameterDirection.Output);

            try
            {
                var queryResult = await con.QueryAsync<PostOffice>(
                    "CATEGORY_PKG.GET_CATEGORY_POSCODE",
                    parameters,
                    commandType: CommandType.StoredProcedure);

                int total = parameters.Get<int>("v_TOTAL");

                var response = new ResponseData<PostOffice>
                {
                    code = "success",
                    message = "Thành công",
                    total = total,
                    data = queryResult.ToList(),
                };
                return response;
            }
            catch ( OracleException ex )
            {
                var errorResponse = new ResponseData<PostOffice>
                {
                    code = "error",
                    message = ex.Message
                };
                return errorResponse;
            }
            catch ( Exception ex )
            {
                var errorResponse = new ResponseData<PostOffice>
                {
                    code = "error",
                    message = ex.Message
                };
                return errorResponse;
            }
        }
    }
}
