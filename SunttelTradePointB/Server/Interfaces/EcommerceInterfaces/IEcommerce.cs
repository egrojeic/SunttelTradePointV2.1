using SunttelTradePointB.Shared.Common;
using SunttelTradePointB.Shared.ecommerce;

namespace SunttelTradePointB.Server.Interfaces.EcommerceInterfaces
{
    /// <summary>
    /// Interfaz of EcommerceService
    /// </summary>
    public interface IEcommerce
    {
        /// <summary>
        /// Add new Product
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
       Task<ServiceResponse<Product>> AddProduct(Product product);
    }
}
