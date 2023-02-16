using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using SunttelTradePointB.Server.Interfaces.DAS_FilePreOders;
using SunttelTradePointB.Shared.Common;
using SunttelTradePointB.Shared.FilePreOders;

namespace SunttelTradePointB.Server.Services.DAS_FilePreOders
{
    /// <summary>
    /// Acceso a datos sqlserver
    /// </summary>
    public class SerDasFileEDI : ISerDasFileEDI
    {
        private readonly string _conexionString;

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="configuration"></param>
        public SerDasFileEDI(IConfiguration configuration)
        {
            _conexionString = configuration.GetConnectionString("SUNTTEL_TRADEPOINT_EDI");
        }

        /// <summary>
        /// Obtiene todas la preordenes
        /// </summary>
        /// <returns></returns>
        public async Task<Response<List<PreOrders>>> getPreOrders()
        {
            List<PreOrders> lsResp = new List<PreOrders>();
            try
            {
                using var connection = new SqlConnection(_conexionString);

                var resp = await connection.QueryAsync<PreOrders>("dbo.getPreOrders", new { }, commandType: System.Data.CommandType.StoredProcedure);
                lsResp = resp.ToList();

                return new Response<List<PreOrders>>(false, "", lsResp); ;
            }
            catch (Exception ex)
            {
                return new Response<List<PreOrders>>(true, ex.Message, lsResp); ;
            }
        }



    }
}
