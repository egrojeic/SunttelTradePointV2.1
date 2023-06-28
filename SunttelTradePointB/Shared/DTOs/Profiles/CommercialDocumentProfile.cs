using AutoMapper;
using SunttelTradePointB.Shared.Sales;
using SunttelTradePointB.Shared.Sales.CommercialDocumentDTO;

namespace SunttelTradePointB.Shared.DTOs.Profiles
{
    public class CommercialDocumentProfile : Profile
    {
        public CommercialDocumentProfile()
        {
            CreateMap<CommercialDocument, CommercialDocumentDTO>()
            .ReverseMap();
            //CreateMap<CommercialDocumentType, CommercialDocumentTypeDTO>().ReverseMap();
            //CreateMap<ShippingStatus, ShippingStatusDTO>().ReverseMap();
            //CreateMap<BusinessLine, BusinessLineDTO>().ReverseMap();
            //CreateMap<FinanceStatus, FinanceStatusDTO>().ReverseMap();
            ////CreateMap<SalesDocumentItemsDetails, SalesDocumentItemsDetailsDTO>().ReverseMap(); is a list
            //CreateMap<SalesDocumentPrintingSatus, SalesDocumentPrintingSatusDTO>().ReverseMap();
            //CreateMap<FinanceSalesDocumentSummary, FinanceSalesDocumentSummaryDTO>().ReverseMap();
            //CreateMap<ShippingSalesDocumentSummary, ShippingSalesDocumentSummaryDTO>().ReverseMap();
            //CreateMap<CancelationTrack, CancelationTrackDTO>().ReverseMap();
            //CreateMap<PurchaseItemDetails, PurchaseItemDetailsDTO>().ReverseMap();
            //CreateMap<CancelationTrack, CancelationTrackDTO>().ReverseMap();
            // Agrega configuraciones adicionales de mapeo si es necesario
        }

        //private List<SalesDocumentItemsDetailsDTO> ConvertItems(List<SalesDocumentItemsDetails> source)
        //{
        //    var itemsDto = Mapper.Map<List<SalesDocumentItemsDetailsDTO>>(source);
        //    return itemsDto;
        //}

    }

}
