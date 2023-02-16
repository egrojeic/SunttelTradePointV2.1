using SunttelTradePointB.Shared.Common;
using SunttelTradePointB.Shared.FilePreOders;

namespace SunttelTradePointB.Server.Interfaces.BL_FilePreOders
{
    public interface ISerBLFileEDI
    {
        Task<Response<List<PreOrders>>> getPreOrders();
    }
}
