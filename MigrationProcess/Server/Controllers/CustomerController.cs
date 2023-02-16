using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MigrationProcess.Server.ServiceSQL;
using SunttelTradePointB.Shared;

namespace MigrationProcess.Server.Controllers
{
    [Route("api/Customer")]
    [ApiController]
    public class CustomerController : ControllerBase
    {

        private readonly ISerCustomer _serCustomer;

        public CustomerController(ISerCustomer serCustomer)
        {
            _serCustomer = serCustomer; 
        }



        [HttpPost("migrate")]
        [AllowAnonymous]
        public async Task<string> migrate()
        {
            string res = string.Empty;

            await _serCustomer.Migrate();

            return res;
        }
    }
}
