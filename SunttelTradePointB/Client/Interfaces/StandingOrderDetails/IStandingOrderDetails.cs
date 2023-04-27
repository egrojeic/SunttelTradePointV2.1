using SunttelTradePointB.Shared.Common;
using SunttelTradePointB.Shared.Sales;

namespace SunttelTradePointB.Client.Interfaces.StandingOrderDetails
{
    public interface IStandingOrderDetails
    {



        /// <summary>
        /// Saved a item StandingOrder 
        /// </summary>
        /// <param name="standing"></param>  
        /// <returns></returns>    
        Task<StandingOrder> SaveStandingOrde(StandingOrder standing);


        /// <summary>
        /// Retrives a list with StandingOrder items meeting search criteria
        /// </summary>
        /// <param name="filter"></param>       
        /// <param name="page"></param>       
        /// <param name="perPage"></param>       
        /// <returns></returns>      
        Task<List<StandingOrder>> GetStandingOrderList(string? filter = null, int? page = 1, int? perPage = 10);


        /// <summary>
        /// Retrives a item with StandingOrder meeting search criteria
        /// </summary>
        /// <param name="standingOrderId"></param>    
        /// <returns></returns>      
        Task<StandingOrder> GetStandingOrderById(string? standingOrderId);
    }
}
