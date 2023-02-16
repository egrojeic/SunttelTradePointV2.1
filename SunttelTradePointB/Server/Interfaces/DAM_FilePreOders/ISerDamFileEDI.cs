using SunttelTradePointB.Shared.Common;
using SunttelTradePointB.Shared.FilePreOders;

namespace SunttelTradePointB.Server.Interfaces.DAM_FilePreOders
{
    public interface ISerDamFileEDI
    {
        Task<Response<List<PreOrders>>> getPreOrders();
        Task<Response<string>> upLoadPreOrders(List<PreOrders> myPreOrders);
    }
}
