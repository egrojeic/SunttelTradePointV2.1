using SunttelTradePointB.Client.Interfaces.SalesInterfaces;
using SunttelTradePointB.Shared.Sales.SalesDTO;
using Syncfusion.Blazor;
using Syncfusion.Blazor.Data;

namespace SunttelTradePointB.Client.Services.SalesServices
{
    public class CommercialDocumentDetailsAdaptor: DataAdaptor
    {
        private readonly ISalesDocuments _salesDocuments;

        public CommercialDocumentDetailsAdaptor(ISalesDocuments salesDocuments)
        {
            _salesDocuments = salesDocuments;
        }

        public async override Task<object> ReadAsync(DataManagerRequest dataManagerRequest, string key = null)
        {
            CommercialDocumentDetailsResult result = new CommercialDocumentDetailsResult();

            if (dataManagerRequest.Params != null && dataManagerRequest.Params.Count > 0)
            {
                var val = dataManagerRequest.Params;

                val.TryGetValue("EntityId", out var EntityId);

                if(EntityId is not null)
                    result = await _salesDocuments.GetSalesOrders(EntityId.ToString(), dataManagerRequest.Skip, dataManagerRequest.Take);
            }

            DataResult dataResult = new DataResult()
            {
                Result = result.results,
                Count = result.Count
            };

            return dataResult;
        }
    }
}
