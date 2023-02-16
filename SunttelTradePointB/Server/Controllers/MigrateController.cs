using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SunttelTradePointB.Server.Interfaces.DAM_FilePreOders;
using SunttelTradePointB.Server.InterfacesMigration;

namespace SunttelTradePointB.Server.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("[controller]")]
    [ApiController]
    public class MigrateController : ControllerBase
    {
        private readonly ISerDBMigration _serCustomerMigracion;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="serCustomer"></param>
        public MigrateController(ISerDBMigration serCustomerMigracion)
        {
            _serCustomerMigracion = serCustomerMigracion;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpPost("customer")]
        [AllowAnonymous]
        public async Task<string> customer()
        {
            string res = string.Empty;

            res = await _serCustomerMigracion.MigrateCustomer();

            return res;
        }




    }
}
