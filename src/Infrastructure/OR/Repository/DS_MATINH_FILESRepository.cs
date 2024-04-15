using Dapper.Oracle;
using Microsoft.Extensions.Configuration;
using NewBalance.Infrastructure.OR.IRepository;
using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Oracle.ManagedDataAccess.Client;
using NewBalance.Application.Features.Doi_Soat;
using NewBalance.Application.Features.Doi_Soat.Danh_Muc.Queries.GetAll;
using Dapper;



namespace NewBalance.Infrastructure.OR.Repository
{
    public class DS_MATINH_FILESRepository : IDS_MATINH_FILESRepository
    {
        private readonly string _connectionString = "";
        private readonly IConfiguration _configuration;
        private OracleConnection con = new OracleConnection();
        public DS_MATINH_FILESRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DBDS");
            _configuration = configuration;
            con = new OracleConnection(_connectionString);
        }

        public async Task<ResponseData<GetAllDS_MATINH_FILESResponse>> GetAllDS_MATINH_FILESResponse(string pageIndex, string pageSize, int ma_tinh, int tu_ngay, int den_ngay)
        {

            if (con.State == ConnectionState.Closed) await con.OpenAsync();
            var parameters = new OracleDynamicParameters();
            parameters.Add("v_PAGEINDEX", Convert.ToInt32(pageIndex), OracleMappingType.Int32);
            parameters.Add("v_PAGESIZE", Convert.ToInt32(pageSize), OracleMappingType.Int32);
            parameters.Add("v_ma_tinh", ma_tinh, OracleMappingType.Int32);
            parameters.Add("v_tu_ngay", tu_ngay, OracleMappingType.Int32);
            parameters.Add("v_den_ngay", den_ngay, OracleMappingType.Int32);
            parameters.Add("v_TotalRecord", dbType: OracleMappingType.Int32, direction: ParameterDirection.Output);
            parameters.Add("v_cursor", dbType: OracleMappingType.RefCursor, direction: ParameterDirection.Output);

            try
            {
                var queryResult = await con.QueryAsync<GetAllDS_MATINH_FILESResponse>(
                    "pk_doi_soat.DS_MATINH_FILES_LIST",
                    parameters,
                    commandType: CommandType.StoredProcedure);

                int total = parameters.Get<int>("v_TotalRecord");

                var response = new ResponseData<GetAllDS_MATINH_FILESResponse>
                {
                    code = "success",
                    message = "Danh sách file",
                    total = total,
                    data = queryResult.ToList(),
                };
                return response;
            }
            catch (OracleException ex)
            {
                var errorResponse = new ResponseData<GetAllDS_MATINH_FILESResponse>
                {
                    code = "error",
                    message = ex.Message
                };
                return errorResponse;
            }
            catch (Exception ex)
            {
                var errorResponse = new ResponseData<GetAllDS_MATINH_FILESResponse>
                {
                    code = "error",
                    message = ex.Message
                };
                return errorResponse;
            }
        }

        public async Task<ResponseData<int>> DS_MATINH_FILES_MODIFY_STATUS(int id,string createby, string notes)
        {

            try
            {
                if (con.State == ConnectionState.Closed) await con.OpenAsync();
                var parameters = new OracleDynamicParameters();
                parameters.Add("v_id", Convert.ToInt32(id), OracleMappingType.Int32);
                parameters.Add("v_createby", createby, OracleMappingType.Varchar2);
                parameters.Add("v_notes", notes, OracleMappingType.NVarchar2);
                await con.QueryAsync(sql: "pk_doi_soat.MATINH_FILES_MODIFY_STATUS", param: parameters, commandType: CommandType.StoredProcedure);
                var response = new ResponseData<int>
                {
                    code = "success",
                    message = "Cập nhật trạng thái chờ tạo file thành công. Vui lòng chờ 5 phút."
                };
                return response;
            }
            catch (Exception ex)
            {
                var response = new ResponseData<int>
                {
                    code = "error",
                    message = ex.Message
                };
                return response;
            }

        }


    }
}
