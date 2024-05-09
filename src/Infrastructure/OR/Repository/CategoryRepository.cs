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
using System.Drawing.Printing;

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

        public async Task<ResponseData<Province>> GetCategoryProvinceAsync( int pageIndex, int pageSize )
        {
            if ( con.State == ConnectionState.Closed ) await con.OpenAsync();
            var parameters = new OracleDynamicParameters();
            parameters.Add("v_PAGEINDEX", pageIndex, OracleMappingType.Int32);
            parameters.Add("v_PAGESIZE", pageSize, OracleMappingType.Int32);
            parameters.Add("v_TOTAL", dbType: OracleMappingType.Int32, direction: ParameterDirection.Output);
            parameters.Add("v_ListStage", dbType: OracleMappingType.RefCursor, direction: ParameterDirection.Output);

            try
            {
                var queryResult = await con.QueryAsync<Province>(
                    "CATEGORY_PKG.GET_CATEGORY_PROVINCE",
                    parameters,
                    commandType: CommandType.StoredProcedure);

                int total = parameters.Get<int>("v_TOTAL");

                var response = new ResponseData<Province>
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
                var errorResponse = new ResponseData<Province>
                {
                    code = "error",
                    message = ex.Message
                };
                return errorResponse;
            }
            catch ( Exception ex )
            {
                var errorResponse = new ResponseData<Province>
                {
                    code = "error",
                    message = ex.Message
                };
                return errorResponse;
            }
        }
        public async Task<ResponseData<District>> GetCategoryDistrictAsync( int pageIndex, int pageSize, int ProvinceCode )
        {
            if ( con.State == ConnectionState.Closed ) await con.OpenAsync();
            var parameters = new OracleDynamicParameters();
            parameters.Add("v_PROVINCECODE", ProvinceCode, OracleMappingType.Int32);
            parameters.Add("v_PAGEINDEX", pageIndex, OracleMappingType.Int32);
            parameters.Add("v_PAGESIZE", pageSize, OracleMappingType.Int32);
            parameters.Add("v_TOTAL", dbType: OracleMappingType.Int32, direction: ParameterDirection.Output);
            parameters.Add("v_ListStage", dbType: OracleMappingType.RefCursor, direction: ParameterDirection.Output);

            try
            {
                var queryResult = await con.QueryAsync<District>(
                    "CATEGORY_PKG.GET_CATEGORY_DISTRICT",
                    parameters,
                    commandType: CommandType.StoredProcedure);

                int total = parameters.Get<int>("v_TOTAL");

                var response = new ResponseData<District>
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
                var errorResponse = new ResponseData<District>
                {
                    code = "error",
                    message = ex.Message
                };
                return errorResponse;
            }
            catch ( Exception ex )
            {
                var errorResponse = new ResponseData<District>
                {
                    code = "error",
                    message = ex.Message
                };
                return errorResponse;
            }
        }

        public async Task<ResponseData<Commune>> GetCategoryCommuneAsync( int pageIndex, int pageSize , int DistrictCode )
        {
            if ( con.State == ConnectionState.Closed ) await con.OpenAsync();
            var parameters = new OracleDynamicParameters();
            parameters.Add("v_DISTRICTCODE", DistrictCode, OracleMappingType.Int32);
            parameters.Add("v_PAGEINDEX", pageIndex, OracleMappingType.Int32);
            parameters.Add("v_PAGESIZE", pageSize, OracleMappingType.Int32);
            parameters.Add("v_TOTAL", dbType: OracleMappingType.Int32, direction: ParameterDirection.Output);
            parameters.Add("v_ListStage", dbType: OracleMappingType.RefCursor, direction: ParameterDirection.Output);

            try
            {
                var queryResult = await con.QueryAsync<Commune>(
                    "CATEGORY_PKG.GET_CATEGORY_COMMUNE",
                    parameters,
                    commandType: CommandType.StoredProcedure);

                int total = parameters.Get<int>("v_TOTAL");

                var response = new ResponseData<Commune>
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
                var errorResponse = new ResponseData<Commune>
                {
                    code = "error",
                    message = ex.Message
                };
                return errorResponse;
            }
            catch ( Exception ex )
            {
                var errorResponse = new ResponseData<Commune>
                {
                    code = "error",
                    message = ex.Message
                };
                return errorResponse;
            }
        }

        public async Task<ResponsePost> AddProvinceAsync( Province data )
        {
            if ( con.State == ConnectionState.Closed ) await con.OpenAsync();
            var parameters = new OracleDynamicParameters();
            parameters.Add("v_PROVINCECODE", data.PROVINCECODE, OracleMappingType.Int32);
            parameters.Add("v_PROVINCENAME", data.PROVINCENAME, OracleMappingType.NVarchar2);
            parameters.Add("v_DESCRIPTION", data.DESCRIPTION, OracleMappingType.NVarchar2);
            parameters.Add("v_REGIONCODE", data.PROVINCENAME, OracleMappingType.Int32);
            parameters.Add("v_PROVINCELISTCODE", data.PROVINCELISTCODE, OracleMappingType.Varchar2);
            parameters.Add("v_CODE", dbType: OracleMappingType.Varchar2, size: 500, direction: ParameterDirection.Output);
            parameters.Add("v_MESSAGE", dbType: OracleMappingType.NVarchar2, size: 1000, direction: ParameterDirection.Output);

            try
            {
                var queryResult = await con.ExecuteAsync(
                    "CATEGORY_PKG.ADD_CATEGORY_PROVINCE",
                    parameters,
                    commandType: CommandType.StoredProcedure);


                var response = new ResponsePost
                {
                    code = parameters.Get<string>("v_CODE"),
                    message = parameters.Get<string>("v_MESSAGE"),
                };
                return response;
            }
            catch ( OracleException ex )
            {
                var errorResponse = new ResponsePost
                {
                    code = "ERROR",
                    message = ex.Message
                };
                return errorResponse;
            }
            catch ( Exception ex )
            {
                var errorResponse = new ResponsePost
                {
                    code = "ERROR",
                    message = ex.Message
                };
                return errorResponse;
            }
        }

        public async Task<ResponsePost> AddDistrictAsync( District data )
        {
            if ( con.State == ConnectionState.Closed ) await con.OpenAsync();
            var parameters = new OracleDynamicParameters();
            parameters.Add("v_DISTRICTCODE", data.DISTRICTCODE, OracleMappingType.Int32);
            parameters.Add("v_DISTRICTNAME", data.DISTRICTNAME, OracleMappingType.NVarchar2);
            parameters.Add("v_DESCRIPTION", data.DESCRIPTION, OracleMappingType.NVarchar2);
            parameters.Add("v_PROVINCECODE", data.PROVINCECODE, OracleMappingType.Int32);
            parameters.Add("v_CODE", dbType: OracleMappingType.Varchar2,size: 500, direction: ParameterDirection.Output);
            parameters.Add("v_MESSAGE", dbType: OracleMappingType.NVarchar2, size: 1000, direction: ParameterDirection.Output);

            try
            {
                var queryResult = await con.ExecuteAsync(
                    "CATEGORY_PKG.ADD_CATEGORY_DISTRICT",
                    parameters,
                    commandType: CommandType.StoredProcedure);


                var response = new ResponsePost
                {
                    code = parameters.Get<string>("v_CODE"),
                    message = parameters.Get<string>("v_MESSAGE"),
                };
                return response;
            }
            catch ( OracleException ex )
            {
                var errorResponse = new ResponsePost
                {
                    code = "ERROR",
                    message = ex.Message
                };
                return errorResponse;
            }
            catch ( Exception ex )
            {
                var errorResponse = new ResponsePost
                {
                    code = "ERROR",
                    message = ex.Message
                };
                return errorResponse;
            }
        }

        public async Task<ResponsePost> AddCommuneAsync( Commune data )
        {
            if ( con.State == ConnectionState.Closed ) await con.OpenAsync();
            var parameters = new OracleDynamicParameters();
            parameters.Add("v_COMMUNECODE", data.COMMUNECODE, OracleMappingType.Varchar2);
            parameters.Add("v_COMMUNENAME", data.COMMUNENAME, OracleMappingType.NVarchar2);
            parameters.Add("v_DISTRICTCODE", data.DISTRICTCODE, OracleMappingType.Varchar2);
            parameters.Add("v_CODE", dbType: OracleMappingType.Varchar2,size: 500, direction: ParameterDirection.Output);
            parameters.Add("v_MESSAGE", dbType: OracleMappingType.NVarchar2,size: 1000, direction: ParameterDirection.Output);

            try
            {
                var queryResult = await con.ExecuteAsync(
                    "CATEGORY_PKG.ADD_CATEGORY_COMMUNE",
                    parameters,
                    commandType: CommandType.StoredProcedure);


                var response = new ResponsePost
                {
                    code = parameters.Get<string>("v_CODE"),
                    message = parameters.Get<string>("v_MESSAGE"),
                };
                return response;
            }
            catch ( OracleException ex )
            {
                var errorResponse = new ResponsePost
                {
                    code = "ERROR",
                    message = ex.Message
                };
                return errorResponse;
            }
            catch ( Exception ex )
            {
                var errorResponse = new ResponsePost
                {
                    code = "ERROR",
                    message = ex.Message
                };
                return errorResponse;
            }
        }

        public async Task<ResponsePost> UpdateCategoryAsync( SingleUpdateRequest data )
        {
            if ( con.State == ConnectionState.Closed ) await con.OpenAsync();
            var parameters = new OracleDynamicParameters();
            parameters.Add("v_TABLENAME", data.TABLENAME, OracleMappingType.Varchar2);
            parameters.Add("v_IDCOLUMNNAME", data.IDCOLUMNNAME, OracleMappingType.Varchar2);
            parameters.Add("v_IDCOLUMNVALUE", data.IDCOLUMNVALUE, OracleMappingType.NVarchar2);
            parameters.Add("v_UPDATECOLUMNNAME", data.CHANGECOLUMNNAME, OracleMappingType.Varchar2);
            parameters.Add("v_UPDATECOLUMNVALUE", data.CHANGECOLUMNVALUE, OracleMappingType.NVarchar2);
            
            try
            {
                var queryResult = await con.ExecuteAsync(
                    "CATEGORY_PKG.UPDATE_CATEGORY_ONE_DATE",
                    parameters,
                    commandType: CommandType.StoredProcedure);


                var response = new ResponsePost
                {
                    code = "SUCESSS",
                    message = "",
                };
                return response;
            }
            catch ( OracleException ex )
            {
                var errorResponse = new ResponsePost
                {
                    code = "ERROR",
                    message = ex.Message
                };
                return errorResponse;
            }
            catch ( Exception ex )
            {
                var errorResponse = new ResponsePost
                {
                    code = "ERROR",
                    message = ex.Message
                };
                return errorResponse;
            }
        }

        public async Task<ResponsePost> DeleteCategoryAsync( SingleUpdateRequest data )
        {
            if ( con.State == ConnectionState.Closed ) await con.OpenAsync();
            var parameters = new OracleDynamicParameters();
            parameters.Add("v_TABLENAME", data.TABLENAME, OracleMappingType.Varchar2);
            parameters.Add("v_IDCOLUMNNAME", data.IDCOLUMNNAME, OracleMappingType.Varchar2);
            parameters.Add("v_IDCOLUMNVALUE", data.IDCOLUMNVALUE, OracleMappingType.NVarchar2);

            try
            {
                var queryResult = await con.ExecuteAsync(
                    "CATEGORY_PKG.DELETE_CATEGORY",
                    parameters,
                    commandType: CommandType.StoredProcedure);


                var response = new ResponsePost
                {
                    code = "SUCESSS",
                    message = "",
                };
                return response;
            }
            catch ( OracleException ex )
            {
                var errorResponse = new ResponsePost
                {
                    code = "ERROR",
                    message = ex.Message
                };
                return errorResponse;
            }
            catch ( Exception ex )
            {
                var errorResponse = new ResponsePost
                {
                    code = "ERROR",
                    message = ex.Message
                };
                return errorResponse;
            }
        }
    }
}
