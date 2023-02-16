using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SunttelTradePointB.Server.Interfaces.BL_FilePreOders;
using SunttelTradePointB.Server.Interfaces.DAM_FilePreOders;
using SunttelTradePointB.Shared.Common;
using SunttelTradePointB.Shared.FilePreOders;

namespace SunttelTradePointB.Server.Controllers
{

    
    /// <summary>
    /// 
    /// </summary>
    [Route("api/preorders")]
    [ApiController]
    public class FilePreOrdersController : ControllerBase
    {
        private readonly ISerDamFileEDI _serDamFileEDI;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="serDamFileEDI"></param>
        public FilePreOrdersController(ISerDamFileEDI serDamFileEDI)
        {
            _serDamFileEDI = serDamFileEDI;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet("getPreOrders")]
        [AllowAnonymous]
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
        [HttpPost("upLoadPreOrders")]
        [AllowAnonymous]
        public async Task<Response<string>> upLoadPreOrders(List<PreOrders> myPreOrders)
        {
            //List<PreOrders> ls = new List<PreOrders>();           

            var response = await _serDamFileEDI.upLoadPreOrders(myPreOrders);
            if (response.Error)
            {
                return new Response<string>(true, response.Message, "");
            }
            else
            {
                return new Response<string>(false, "", response.data);
            }
        }


    }
}
