using Microsoft.AspNetCore.Mvc;
using SunttelTradePointB.Server.Interfaces.EcommerceInterfaces;
using SunttelTradePointB.Shared.ecommerce;

namespace SunttelTradePointB.Server.Controllers.EcommerceBack
{
    /// <summary>
    /// Controller for Ecommerce
    /// </summary>
    [Tags("Ecommerce EndPoints")]
    [Route("api/[controller]/[action]")]
    public class EcommerceController : ControllerBase
    {
        private readonly IEcommerce _ecommerceService;

        /// <summary>
        /// Controller constructor
        /// </summary>
        /// <param name="ecommerce"></param>
        public EcommerceController(IEcommerce ecommerce)
        {
            _ecommerceService = ecommerce;
        }

        /// <summary>
        /// Add new product to shop
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        [HttpPost]
        [ActionName("AddProduct")]
        public async Task<IActionResult> AddProduct([FromBody] Product product)
        {
            var response = await _ecommerceService.AddProduct(product);

            if (response.IsSuccess)
                return Ok(response.Entity);
            else
                return BadRequest(response.ErrorDescription);
        }
    }
}
