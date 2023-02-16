using SunttelTradePointB.Server.Interfaces.BL_FilePreOders;
using SunttelTradePointB.Server.Interfaces.DAM_FilePreOders;
using SunttelTradePointB.Server.Interfaces.DAS_FilePreOders;
using SunttelTradePointB.Server.Services.DAS_FilePreOders;
using SunttelTradePointB.Shared.Common;
using SunttelTradePointB.Shared.FilePreOders;

namespace SunttelTradePointB.Server.Services.BL_FilePreOders
{
    /// <summary>
    /// Clase para manerjar a logica de negocio
    /// </summary>
    public class SerBLFileEDI : ISerBLFileEDI
    {
        private readonly ISerDamFileEDI _serDamFileEDI;       

        /// <summary>
        /// 
        /// </summary>
        public SerBLFileEDI(IConfiguration configuration, ISerDamFileEDI serDamFileEDI)
        {
            _serDamFileEDI = serDamFileEDI;
        }

        /// <summary>
        /// Obtiene todas las preordenes
        /// </summary>
        /// <returns></returns>
        public async Task<Response<List<PreOrders>>> getPreOrders()
        {            
                var resp = await _serDamFileEDI.getPreOrders();
                return resp;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="myPreOrders"></param>
        /// <returns></returns>
        public async Task<Response<string>> upLoadPreOrders(List<PreOrders> myPreOrders)
        {

            var resp = await _serDamFileEDI.upLoadPreOrders(myPreOrders);
            return resp;

        }




    }
}
