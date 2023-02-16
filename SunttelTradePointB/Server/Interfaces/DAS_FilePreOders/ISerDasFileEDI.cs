using SunttelTradePointB.Shared.Common;
using SunttelTradePointB.Shared.FilePreOders;

namespace SunttelTradePointB.Server.Interfaces.DAS_FilePreOders
{
    public interface ISerDasFileEDI
    {
        Task<Response<List<PreOrders>>> getPreOrders();
    }
}
