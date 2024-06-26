﻿using Dapper.Oracle;
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
            finally
            {
                await con.DisposeAsync();
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
            finally
            {
                await con.DisposeAsync();
            }
        }

        public async Task<ResponseData<PostOffice>> GetCategoryPostOfficeAsync( int pageIndex, int pageSize, int ProvinceCode, int DistrictCode, int communeCode, int containVXHD )
        {
            if ( con.State == ConnectionState.Closed ) await con.OpenAsync();
            var parameters = new OracleDynamicParameters();
            parameters.Add("v_PROVINCECODE", ProvinceCode, OracleMappingType.Int32);
            parameters.Add("v_DISTRICTCODE", DistrictCode, OracleMappingType.Int32);
            parameters.Add("v_COMMUNECODE", communeCode, OracleMappingType.Int32);
            parameters.Add("v_CONTAINVXHD", containVXHD, OracleMappingType.Int32);
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
            finally
            {
                await con.DisposeAsync();
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
            finally
            {
                await con.DisposeAsync();
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
            finally
            {
                await con.DisposeAsync();
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
            finally
            {
                await con.DisposeAsync();
            }
        }

        public async Task<ResponsePost> AddProvinceAsync( Province data )
        {
            if ( con.State == ConnectionState.Closed ) await con.OpenAsync();
            var parameters = new OracleDynamicParameters();
            parameters.Add("v_PROVINCECODE", data.PROVINCECODE, dbType: OracleMappingType.Int32);
            parameters.Add("v_PROVINCENAME", data.PROVINCENAME, dbType: OracleMappingType.NVarchar2);
            parameters.Add("v_DESCRIPTION", data.DESCRIPTION, dbType: OracleMappingType.NVarchar2);
            parameters.Add("v_REGIONCODE", data.REGIONCODE, dbType: OracleMappingType.Int32);
            parameters.Add("v_PROVINCELISTCODE", data.PROVINCELISTCODE, dbType: OracleMappingType.Varchar2, size: 500);
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
            finally
            {
                await con.DisposeAsync();
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
            finally
            {
                await con.DisposeAsync();
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
                    code = "SUCCESS",
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
                    code = "SUCCESS",
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

        public async Task<ResponseData<MapProvinceDistrictCommune>> GetAllCategoryProvinceDistrictCommune()
        {
            if ( con.State == ConnectionState.Closed ) await con.OpenAsync();
            var parameters = new OracleDynamicParameters();
            parameters.Add("v_ListStage", dbType: OracleMappingType.RefCursor, direction: ParameterDirection.Output);

            try
            {
                var queryResult = await con.QueryAsync<MapProvinceDistrictCommune>(
                    "CATEGORY_PKG.GET_ALL_CATEGORY_PROVINCE_DISTRICT_COMMUNE",
                    parameters,
                    commandType: CommandType.StoredProcedure);



                var response = new ResponseData<MapProvinceDistrictCommune>
                {
                    code = "success",
                    message = "Thành công",
                    total = queryResult.Count(),
                    data = queryResult.ToList(),
                };
                return response;
            }
            catch ( OracleException ex )
            {
                var errorResponse = new ResponseData<MapProvinceDistrictCommune>
                {
                    code = "error",
                    message = ex.Message
                };
                return errorResponse;
            }
            catch ( Exception ex )
            {
                var errorResponse = new ResponseData<MapProvinceDistrictCommune>
                {
                    code = "error",
                    message = ex.Message
                };
                return errorResponse;
            }
            finally { await con.DisposeAsync(); }
        }

        public async Task<ResponsePost> AddPostOfficeAsync( PostOffice data )
        {
            if ( con.State == ConnectionState.Closed ) await con.OpenAsync();
            var parameters = new OracleDynamicParameters();
            parameters.Add("v_POSCODE", data.POSCODE, OracleMappingType.Varchar2);
            parameters.Add("v_POSNAME", data.POSNAME, OracleMappingType.NVarchar2);
            parameters.Add("v_ADDRESS", data.ADDRESS, OracleMappingType.NVarchar2);
            parameters.Add("v_POSTYPECODE", data.POSTYPECODE, OracleMappingType.NVarchar2);
            parameters.Add("v_PROVINCECODE", data.PROVINCECODE, OracleMappingType.Varchar2);
            parameters.Add("v_POSLEVELCODE", data.POSLEVELCODE, OracleMappingType.Varchar2);
            parameters.Add("v_COMMUNECODE", data.COMMUNECODE, OracleMappingType.Varchar2);
            parameters.Add("v_UNITCODE", data.UNITCODE, OracleMappingType.Varchar2);
            parameters.Add("v_STATUS", data.STATUS, OracleMappingType.Int32);
            parameters.Add("v_VX", data.VX, OracleMappingType.Int32);
            parameters.Add("v_VXHD", data.VXHD, OracleMappingType.Int32);
            parameters.Add("v_CODE", dbType: OracleMappingType.Varchar2, size: 500, direction: ParameterDirection.Output);
            parameters.Add("v_MESSAGE", dbType: OracleMappingType.NVarchar2, size: 1000, direction: ParameterDirection.Output);


            try
            {
                var queryResult = await con.ExecuteAsync(
                    "CATEGORY_PKG.ADD_CATEGORY_POSTCODE",
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

        #region Danh mục dịch vụ
        public async Task<ResponseData<DM_Dich_Vu>> GetCategoryDM_Dich_VuAsync(int pageIndex, int pageSize)
        {
            if (con.State == ConnectionState.Closed) await con.OpenAsync();
            var parameters = new OracleDynamicParameters();
            parameters.Add("v_PAGEINDEX", pageIndex, OracleMappingType.Int32);
            parameters.Add("v_PAGESIZE", pageSize, OracleMappingType.Int32);
            parameters.Add("v_TOTAL", dbType: OracleMappingType.Int32, direction: ParameterDirection.Output);
            parameters.Add("v_ListStage", dbType: OracleMappingType.RefCursor, direction: ParameterDirection.Output);

            try
            {
                var queryResult = await con.QueryAsync<DM_Dich_Vu>(
                    "CATEGORY_PKG.GET_CATEGORY_DICHVU",
                    parameters,
                    commandType: CommandType.StoredProcedure);

                int total = parameters.Get<int>("v_TOTAL");

                var response = new ResponseData<DM_Dich_Vu>
                {
                    code = "success",
                    message = "Thành công",
                    total = total,
                    data = queryResult.ToList(),
                };
                return response;
            }
            catch (OracleException ex)
            {
                var errorResponse = new ResponseData<DM_Dich_Vu>
                {
                    code = "error",
                    message = ex.Message
                };
                return errorResponse;
            }
            catch (Exception ex)
            {
                var errorResponse = new ResponseData<DM_Dich_Vu>
                {
                    code = "error",
                    message = ex.Message
                };
                return errorResponse;
            }
        }
        public async Task<ResponsePost> AddDM_Dich_VuAsync(DM_Dich_Vu data)
        {
            if (con.State == ConnectionState.Closed) await con.OpenAsync();
            var parameters = new OracleDynamicParameters();

            parameters.Add("v_DICHVU", data.DICHVU, OracleMappingType.NVarchar2);
            parameters.Add("v_TENDICHVU", data.TENDICHVU, OracleMappingType.NVarchar2);
            parameters.Add("v_PHANLOAI", data.PHANLOAI, OracleMappingType.Int32);
            parameters.Add("v_GHICHU", data.GHICHU, OracleMappingType.NVarchar2);
            parameters.Add("v_ACCOUNT", data.ACCOUNT, OracleMappingType.NVarchar2);
            parameters.Add("v_CODE", dbType: OracleMappingType.Varchar2, size: 500, direction: ParameterDirection.Output);
            parameters.Add("v_MESSAGE", dbType: OracleMappingType.NVarchar2, size: 1000, direction: ParameterDirection.Output);

            try
            {
                var queryResult = await con.ExecuteAsync(
                    "CATEGORY_PKG.ADD_CATEGORY_DICHVU",
                    parameters,
                    commandType: CommandType.StoredProcedure);


                var response = new ResponsePost
                {
                    code = parameters.Get<string>("v_CODE"),
                    message = parameters.Get<string>("v_MESSAGE"),
                };
                return response;
            }
            catch (OracleException ex)
            {
                var errorResponse = new ResponsePost
                {
                    code = "ERROR",
                    message = ex.Message
                };
                return errorResponse;
            }
            catch (Exception ex)
            {
                var errorResponse = new ResponsePost
                {
                    code = "ERROR",
                    message = ex.Message
                };
                return errorResponse;
            }
        }


        #endregion
        #region Danh mục giá vốn
        public async Task<ResponseData<GiaVonChuan>> GetCategoryGiaVonChuanAsync(int pageIndex, int pageSize, int account)
        {
            if (con.State == ConnectionState.Closed) await con.OpenAsync();
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
            catch (OracleException ex)
            {
                var errorResponse = new ResponseData<GiaVonChuan>
                {
                    code = "error",
                    message = ex.Message
                };
                return errorResponse;
            }
            catch (Exception ex)
            {
                var errorResponse = new ResponseData<GiaVonChuan>
                {
                    code = "error",
                    message = ex.Message
                };
                return errorResponse;
            }
        }
        public async Task<ResponsePost> AddGiaVonChuanAsync(GiaVonChuan data)
        {
            if (con.State == ConnectionState.Closed) await con.OpenAsync();
            var parameters = new OracleDynamicParameters();

            parameters.Add("v_ACCOUNT", data.ACCOUNT, OracleMappingType.Int32);
            parameters.Add("v_NOITINH", data.NOITINH, OracleMappingType.BinaryFloat);
            parameters.Add("v_ETLNV", data.ETLNV, OracleMappingType.BinaryFloat);
            parameters.Add("v_ETLLV", data.ETLLV, OracleMappingType.BinaryFloat);
            parameters.Add("v_EHNNV", data.EHNNV, OracleMappingType.BinaryFloat);
            parameters.Add("v_EHNLV", data.EHNLV, OracleMappingType.BinaryFloat);
            parameters.Add("v_ENNNV", data.ENNNV, OracleMappingType.BinaryFloat);
            parameters.Add("v_ENNLV", data.ENNLV, OracleMappingType.BinaryFloat);
            parameters.Add("v_LONV", data.LONV, OracleMappingType.BinaryFloat);
            parameters.Add("v_LOLV", data.LOLV, OracleMappingType.BinaryFloat);
            parameters.Add("v_PHTNT", data.PHTNT, OracleMappingType.BinaryFloat);
            parameters.Add("v_PHTLT", data.PHTLT, OracleMappingType.BinaryFloat);
            parameters.Add("v_THOATHUAN", data.THOATHUAN, OracleMappingType.BinaryFloat);
            parameters.Add("v_TTB", data.TTB, OracleMappingType.BinaryFloat);
            parameters.Add("v_TTC", data.TTC, OracleMappingType.BinaryFloat);
            parameters.Add("v_TTV", data.TTV, OracleMappingType.BinaryFloat);
            parameters.Add("v_ECT", data.ECT, OracleMappingType.BinaryFloat);
            parameters.Add("v_QUOCTE", data.QUOCTE, OracleMappingType.BinaryFloat);
            parameters.Add("v_TUNGAY", data.TUNGAY, OracleMappingType.Int32);
            parameters.Add("v_DENNGAY", data.DENNGAY, OracleMappingType.Int32);
            parameters.Add("v_ACCOUNTEMAIL", data.ACCOUNTEMAIL, OracleMappingType.Varchar2);
            parameters.Add("v_CODE", dbType: OracleMappingType.Varchar2, size: 500, direction: ParameterDirection.Output);
            parameters.Add("v_MESSAGE", dbType: OracleMappingType.NVarchar2, size: 1000, direction: ParameterDirection.Output);

            try
            {
                var queryResult = await con.ExecuteAsync(
                    "CATEGORY_PKG.ADD_CATEGORY_GIAVONCHUAN",
                    parameters,
                    commandType: CommandType.StoredProcedure);


                var response = new ResponsePost
                {
                    code = parameters.Get<string>("v_CODE"),
                    message = parameters.Get<string>("v_MESSAGE"),
                };
                return response;
            }
            catch (OracleException ex)
            {
                var errorResponse = new ResponsePost
                {
                    code = "ERROR",
                    message = ex.Message
                };
                return errorResponse;
            }
            catch (Exception ex)
            {
                var errorResponse = new ResponsePost
                {
                    code = "ERROR",
                    message = ex.Message
                };
                return errorResponse;
            }
        }

        public async Task<ResponsePost> UpdatePostOfficeAsync( PostOffice data )
        {
            if ( con.State == ConnectionState.Closed ) await con.OpenAsync();
            var parameters = new OracleDynamicParameters();
            parameters.Add("v_POSCODE", data.POSCODE, OracleMappingType.Varchar2);
            parameters.Add("v_POSNAME", data.POSNAME, OracleMappingType.NVarchar2);
            parameters.Add("v_ADDRESS", data.ADDRESS, OracleMappingType.NVarchar2);
            parameters.Add("v_POSTYPECODE", data.POSTYPECODE, OracleMappingType.NVarchar2);
            parameters.Add("v_PROVINCECODE", data.PROVINCECODE, OracleMappingType.Varchar2);
            parameters.Add("v_POSLEVELCODE", data.POSLEVELCODE, OracleMappingType.Varchar2);
            parameters.Add("v_COMMUNECODE", data.COMMUNECODE, OracleMappingType.Varchar2);
            parameters.Add("v_UNITCODE", data.UNITCODE, OracleMappingType.Varchar2);
            parameters.Add("v_STATUS", data.STATUS, OracleMappingType.Int32);
            parameters.Add("v_VX", data.VX, OracleMappingType.Int32);
            parameters.Add("v_VXHD", data.VXHD, OracleMappingType.Int32);
            parameters.Add("v_CODE", dbType: OracleMappingType.Varchar2, size: 500, direction: ParameterDirection.Output);
            parameters.Add("v_MESSAGE", dbType: OracleMappingType.NVarchar2, size: 1000, direction: ParameterDirection.Output);


            try
            {
                var queryResult = await con.ExecuteAsync(
                    "CATEGORY_PKG.UPDATE_CATEGORY_POSTCODE",
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

        public async Task<ResponsePost> UpdateProvinceAsync( Province data )
        {
            if ( con.State == ConnectionState.Closed ) await con.OpenAsync();
            var parameters = new OracleDynamicParameters();
            parameters.Add("v_PROVINCECODE", data.PROVINCECODE, dbType: OracleMappingType.Int32);
            parameters.Add("v_PROVINCENAME", data.PROVINCENAME, dbType: OracleMappingType.NVarchar2);
            parameters.Add("v_DESCRIPTION", data.DESCRIPTION, dbType: OracleMappingType.NVarchar2);
            parameters.Add("v_REGIONCODE", data.REGIONCODE, dbType: OracleMappingType.Int32);
            parameters.Add("v_PROVINCELISTCODE", data.PROVINCELISTCODE, dbType: OracleMappingType.Varchar2, size: 500);
            parameters.Add("v_CODE", dbType: OracleMappingType.Varchar2, size: 500, direction: ParameterDirection.Output);
            parameters.Add("v_MESSAGE", dbType: OracleMappingType.NVarchar2, size: 1000, direction: ParameterDirection.Output);

            try
            {
                var queryResult = await con.ExecuteAsync(
                    "CATEGORY_PKG.UPDATE_CATEGORY_PROVINCE",
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
            finally
            {
                await con.DisposeAsync();
            }
        }

        public async Task<ResponsePost> UpdateDistrictAsync( District data )
        {
            if ( con.State == ConnectionState.Closed ) await con.OpenAsync();
            var parameters = new OracleDynamicParameters();
            parameters.Add("v_DISTRICTCODE", data.DISTRICTCODE, OracleMappingType.Int32);
            parameters.Add("v_DISTRICTNAME", data.DISTRICTNAME, OracleMappingType.NVarchar2);
            parameters.Add("v_DESCRIPTION", data.DESCRIPTION, OracleMappingType.NVarchar2);
            parameters.Add("v_CODE", dbType: OracleMappingType.Varchar2, size: 500, direction: ParameterDirection.Output);
            parameters.Add("v_MESSAGE", dbType: OracleMappingType.NVarchar2, size: 1000, direction: ParameterDirection.Output);

            try
            {
                var queryResult = await con.ExecuteAsync(
                    "CATEGORY_PKG.UPDATE_CATEGORY_DISTRICT",
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
            finally
            {
                await con.DisposeAsync();
            }
        }

        public async Task<ResponsePost> UpdateCommuneAsync( Commune data )
        {
            if ( con.State == ConnectionState.Closed ) await con.OpenAsync();
            var parameters = new OracleDynamicParameters();
            parameters.Add("v_COMMUNECODE", data.COMMUNECODE, OracleMappingType.Varchar2);
            parameters.Add("v_COMMUNENAME", data.COMMUNENAME, OracleMappingType.NVarchar2);
            parameters.Add("v_CODE", dbType: OracleMappingType.Varchar2, size: 500, direction: ParameterDirection.Output);
            parameters.Add("v_MESSAGE", dbType: OracleMappingType.NVarchar2, size: 1000, direction: ParameterDirection.Output);

            try
            {
                var queryResult = await con.ExecuteAsync(
                    "CATEGORY_PKG.UPDATE_CATEGORY_COMMUNE",
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
        #endregion
    }
}
