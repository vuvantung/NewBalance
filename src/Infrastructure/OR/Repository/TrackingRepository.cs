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
    }
}
