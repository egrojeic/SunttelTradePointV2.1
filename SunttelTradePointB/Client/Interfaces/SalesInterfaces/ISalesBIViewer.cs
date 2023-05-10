using SunttelTradePointB.Shared.DataViews.BI;

namespace SunttelTradePointB.Client.Interfaces.SalesInterfaces
{
    public interface ISalesBIViewer
    {
        /// <summary>
        /// Retrives a list with BISalesConsolidated items meeting search criteria
        /// </summary>
        /// <param name="filter"></param>      
        /// <returns></returns>
        Task<List<BISalesConsolidated>> GetSalesBIList(DateTime startDate, DateTime endDate, string documentTypeId, string? filter = null, int? page = 1, int? perPage = 10);

    }
}
