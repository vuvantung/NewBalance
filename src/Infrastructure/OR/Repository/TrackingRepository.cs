using Dapper.Oracle;
using Dapper;
using Microsoft.Extensions.Configuration;
using NewBalance.Application.Features.Doi_Soat;
using NewBalance.Application.Features.Doi_Soat.Danh_Muc.Queries.GetAll;
using NewBalance.Domain.Entities.Doi_Soat.Tracking;
using NewBalance.Infrastructure.OR.IRepository;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NewBalance.Application.Requests.Doi_soat;
using NewBalance.Domain.Entities.Doi_Soat.Report;

namespace NewBalance.Infrastructure.OR.Repository
{
    public class TrackingRepository : ITrackingRepository
    {
        private readonly string _connectionString = "";
        private readonly IConfiguration _configuration;
        private OracleConnection con = new OracleConnection();

        public TrackingRepository( IConfiguration configuration )
        {
            _connectionString = configuration.GetConnectionString("DBDS");
            _configuration = configuration;
            con = new OracleConnection(_connectionString);
        }
        public async Task<ResponseSingle<TrackingInfor>> TrackingItem( string ItemCode )
        {
            if ( con.State == ConnectionState.Closed ) await con.OpenAsync();
            var parameters = new OracleDynamicParameters();
            parameters.Add("v_ItemCode", ItemCode, OracleMappingType.Varchar2);
            parameters.Add("p_ItemInfor", dbType: OracleMappingType.RefCursor, direction: ParameterDirection.Output);
            parameters.Add("p_ServiceInfor", dbType: OracleMappingType.RefCursor, direction: ParameterDirection.Output);
            parameters.Add("p_AffairInfor", dbType: OracleMappingType.RefCursor, direction: ParameterDirection.Output);
            parameters.Add("p_StatusTrace", dbType: OracleMappingType.RefCursor, direction: ParameterDirection.Output);
            parameters.Add("p_DeliveryStatus", dbType: OracleMappingType.RefCursor, direction: ParameterDirection.Output);

            try
            {
                var queryResult = await con.QueryMultipleAsync(
                    "EMS.TRACKING_ITEM_DS",
                    parameters,
                    commandType: CommandType.StoredProcedure);


                var response = new ResponseSingle<TrackingInfor>
                {
                    code = "success",
                    message = "",
                    data = new TrackingInfor
                    {
                        ItemInfor = (await queryResult.ReadAsync<ItemInfor>()).SingleOrDefault(),
                        ServiceInfor = (await queryResult.ReadAsync<ServiceInfor>()).AsList(),
                        AffairInfor = (await queryResult.ReadAsync<AffairInfor>()).AsList(),
                        StatusTrace = (await queryResult.ReadAsync<StatusTrace>()).AsList(),
                        DeliveryStatus = (await queryResult.ReadAsync<DeliveryStatus>()).AsList()
                    }
                };
                return response;
            }
            catch ( OracleException ex )
            {
                var errorResponse = new ResponseSingle<TrackingInfor>
                {
                    code = "error",
                    message = ex.Message
                };
                return errorResponse;
            }
            catch ( Exception ex )
            {
                var errorResponse = new ResponseSingle<TrackingInfor>
                {
                    code = "error",
                    message = ex.Message
                };
                return errorResponse;
            }
        }

        public async Task<ResponseData<LastStatusItem>> TrackingSLL( TrackingSLLRequest request )
        {
            if ( con.State == ConnectionState.Closed ) await con.OpenAsync();
            var parametersRequest = new OracleDynamicParameters();
            var parametersExecute = new OracleDynamicParameters();
            var parametersQuery = new OracleDynamicParameters();
            parametersRequest.Add("P_INPUT", request.XmlData, OracleMappingType.Clob);
            parametersExecute.Add("v_IdSession", request.SessionID, OracleMappingType.Varchar2);
            parametersQuery.Add("v_idSession", request.SessionID, OracleMappingType.Varchar2);
            parametersQuery.Add("v_ListStage", dbType: OracleMappingType.RefCursor, direction: ParameterDirection.Output);
            try
            {
                // Insert vào bảng tạm
                await con.ExecuteAsync(
                    "EMS.TrackAndTrace_SLL.INSERT_E1_XML",
                    parametersRequest,
                    commandType: CommandType.StoredProcedure, commandTimeout: 1800);

                // Chạy lấy dữ liệu vào bảng tạm
                await con.ExecuteAsync(
                    "EMS.TrackAndTrace_SLL.UDP_Delivery",
                    parametersExecute,
                    commandType: CommandType.StoredProcedure, commandTimeout: 1800);

                // Query dữ liệu sau khi chạy xong
                var resultQuery = await con.QueryAsync<LastStatusItem>(
                    "EMS.TrackAndTrace_SLL.GetListDelivery",
                    parametersQuery,
                    commandType: CommandType.StoredProcedure, commandTimeout: 1800);

                return new ResponseData<LastStatusItem>
                {
                    code = "success",
                    message = "Lấy dữ liêu thành công",
                    data = resultQuery.ToList()
                };
            }
            catch ( OracleException ex )
            {
                return new ResponseData<LastStatusItem>
                {
                    code = "error",
                    message = $"Lấy dữ liêu thất bại: {ex.Message.Substring(0,200)}"
                };
            }
            catch ( Exception ex )
            {
                return new ResponseData<LastStatusItem>
                {
                    code = "error",
                    message = $"Lấy dữ liêu thất bại: {ex.Message.Substring(0, 200)}"
                };
            }
            finally
            {
                await con.DisposeAsync();
            }
        }
    }
}
