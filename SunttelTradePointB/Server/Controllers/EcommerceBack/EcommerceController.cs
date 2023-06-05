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
            try
            {
                
                var response = await _ecommerceService.AddProduct(product);

                if (response.IsSuccess)
                    return Ok(response.Entity);
                else
                    return BadRequest(response.ErrorDescription);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }


        /// <summary>
        /// Get all products
        /// </summary>
        /// <param></param>
        /// <returns></returns>
        [HttpGet]
        [ActionName("GetProducts")]
        public async Task<IActionResult> GetProducts()
        {
            try
            {
                var response = await _ecommerceService.GetProducts();

                if (response.IsSuccess)
                    return Ok(response.Entity);
                else
                    return BadRequest(response.ErrorDescription);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        /// <summary>
        /// Upload Images of a product
        /// </summary>
        /// <param name="files"></param>
        /// <returns></returns>
        [HttpPost]
        [ActionName("UploadProductImages")]
        public async Task<IActionResult> UploadProductImages(List<IFormFile> files)
        {
            var uploadedImages = new List<string>();

            try
            {
                foreach (var file in files)
                {
                    if (file.Length > 0)
                    {
                        var fileName = Path.GetFileName(file.FileName);
                        var filePath = Path.Combine("C:\\Proyectos\\SunttelTradePointV2.1\\SunttelTradePointB\\Server\\Public", fileName);

                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await file.CopyToAsync(stream);
                        }

                        uploadedImages.Add(filePath);
                    }
                }

                return Ok(uploadedImages);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
