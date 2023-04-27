using SunttelTradePointB.Shared.Common;
using SunttelTradePointB.Shared.Sales;

namespace SunttelTradePointB.Client.Interfaces.AccountsReceivableInterfaces
{
    public interface QualityEvaluation
    {
        /// <summary>
        /// Retrives a item with CommercialDocument meeting search criteria
        /// </summary>
        /// <param name="startDate"></param>       
        /// <param name="endDate"></param>       
        /// <param name="warehouseId"></param>       
        /// <param name="filter"></param>       
        /// <param name="page"></param>       
        /// <param name="perPage"></param>       
        /// <returns></returns>
        Task<List<CommercialDocument>> GetAccountsReceivableServicesList(DateTime startDate, DateTime endDate, string warehouseId, string filter, int page, int perPage);



    }
}
