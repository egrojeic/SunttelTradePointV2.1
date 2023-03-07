using SunttelTradePointB.Shared.Sales;

namespace SunttelTradePointB.Server.Interfaces.Sales
{

    /// <summary>
    /// Interface of service to manage commercial documents
    /// </summary>
    public interface ICommercialDocument
    {
        /// <summary>
        /// Get a commercial document by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<CommercialDocument> GetCommercialDocument(int id);

    }
}
