using SunttelTradePointB.Shared.Common;
using SunttelTradePointB.Shared.Quality;
using SunttelTradePointB.Shared.Sales.CommercialDocument;
using SunttelTradePointB.Shared.Sales;

namespace SunttelTradePointB.Client.Interfaces.QualityEvaluationInterfaces
{
    public interface IQualityEvaluationServices
    {
        /// <summary>
        /// Saved a item qualityEvaluation 
        /// </summary>
        /// <param name="qualityEvaluation"></param>             
        /// <returns></returns>        
        Task<QualityEvaluation> SaveQualityEvaluation(QualityEvaluation qualityEvaluation);

        /// <summary>
        /// Saved a item qualityReportType 
        /// </summary>
        /// <param name="qualityReportType"></param>             
        /// <returns></returns>     
        Task<QualityReportType> SaveQualityReportType(QualityReportType qualityReportType);

        /// <summary>
        /// Saved a item qualityReportType 
        /// </summary>
        /// <param name="quality"></param>             
        /// <returns></returns>     
        Task<QualityTrafficLight> SaveQualityTrafficLight(QualityTrafficLight quality);

        /// <summary>
        /// Saved a item QualityAssuranceParameter 
        /// </summary>
        /// <param name="quality"></param>             
        /// <returns></returns> 
        Task<QualityAssuranceParameter> SaveQualityAssuranceParameter(QualityAssuranceParameter quality);


        /// <summary>
        /// Saved a item QualityParameterGroup 
        /// </summary>
        /// <param name="quality"></param>             
        /// <returns></returns> 
        Task<QualityParameterGroup> SaveQualityParameterGroup(QualityParameterGroup quality);

        /// <summary>
        /// Saved a item QualityAssuranceParameter 
        /// </summary>
        /// <param name="quality"></param>             
        /// <returns></returns> 
        Task<QualityAssuranceParameter> SaveQualityParameterGroup(QualityAssuranceParameter quality);


        /// <summary>
        /// Saved a item QualityAction 
        /// </summary>
        /// <param name="quality"></param>             
        /// <returns></returns> 
        Task<QualityAction> SaveQualityPaction(QualityAction quality);


        /// <summary>
        /// Retrives a  QualityEvaluation item meeting search criteria
        /// </summary>      
        /// <param name="filter"></param>       
        /// <param name="page"></param>       
        /// <param name="perPage"></param>       
        /// <param name="qualityReportTypeName"></param>       
        /// <param name="startDate"></param>       
        /// <param name="endDate"></param>       
        /// <returns></returns>
        public Task<List<QualityEvaluation>> GetQualityEvaluationServicesList(string filter, DateTime startDate, DateTime endDate, string? qualityReportTypeName, int? page = 1, int? perPage = 10);

        /// <summary>
        /// Retrives a  QualityReportType item meeting search criteria
        /// </summary>      
        /// <param name="filter"></param>       
        /// <param name="page"></param>       
        /// <param name="perPage"></param>       
        /// <returns></returns>
        Task<List<QualityReportType>> GetQualityReportTypeList(string filter, int page, int perPage);

        /// <summary>
        /// Retrives a  QualityAction item meeting search criteria
        /// </summary>      
        /// <param name="filter"></param>       
        /// <param name="page"></param>       
        /// <param name="perPage"></param>       
        /// <returns></returns>
        Task<List<QualityAction>> GetQualityActionList(string filter, int page, int perPage);

        /// <summary>
        /// Retrives a  QualityParameterGroup item meeting search criteria
        /// </summary>      
        /// <param name="qualityId"></param>     
        /// <returns></returns>
        Task<QualityParameterGroup> GetQualityParameterGroupById(string qualityId);


        /// <summary>
        /// Retrives a  QualityReportType item meeting search criteria
        /// </summary>      
        /// <param name="qualityReportTypeId"></param>               
        /// <returns></returns>
        Task<QualityReportType> GetQualityReportType(string qualityReportTypeId);

        /// <summary>
        /// Retrives a  QualityAction item meeting search criteria
        /// </summary>      
        /// <param name="qualityId"></param>               
        /// <returns></returns>
        Task<QualityAction> GetQualityActionById(string qualityId);


        /// <summary>
        /// Retrives a  QualityEvaluation item meeting search criteria
        /// </summary>      
        /// <param name="qualityId"></param>               
        /// <returns></returns>
        Task<QualityEvaluation> GetItemQualityEvaluationById(string qualityReportTypeId);

        /// <summary>
        /// Retrives a  CommercialDocument item meeting search criteria
        /// </summary>      
        /// <param name="salesDocumentItemsDetailsId"></param>               
        /// <returns></returns>
        Task<CommercialDocument> GetItemSalesDocumentItemsDetailsById(string salesDocumentItemsDetailsId);

        /// <summary>
        /// Retrives a QualityTrafficLight item meeting search criteria
        /// </summary>      
        /// <param name="qualityId"></param>               
        /// <returns></returns>
        Task<QualityTrafficLight> GetQualityTrafficLightById(string qualityId);

        /// <summary>
        /// Retrives a QualityAssuranceParameter item meeting search criteria
        /// </summary>      
        /// <param name="qualityId"></param>               
        /// <returns></returns>
        Task<QualityAssuranceParameter> GetQualityAssuranceParameterById(string qualityId);

        /// <summary>
        /// Retrives a CommercialDocument item meeting search criteria
        /// </summary>      
        /// <param name="filter"></param>               
        /// <param name="page"></param>               
        /// <param name="perPage"></param>               
        /// <returns></returns>
        Task<List<CommercialDocument>> GetItemSalesDocumentItemsDetailList(string filter, int page, int perPage);

        /// <summary>
        /// Retrives a QualityTrafficLight item meeting search criteria
        /// </summary>      
        /// <param name="filter"></param>               
        /// <param name="page"></param>               
        /// <param name="perPage"></param>               
        /// <returns></returns>
        Task<List<QualityTrafficLight>> GetQualityTrafficLightList(string filter, int page, int perPage);

        /// <summary>
        /// Retrives a QualityParameterGroup item meeting search criteria
        /// </summary>      
        /// <param name="filter"></param>               
        /// <param name="page"></param>               
        /// <param name="perPage"></param>               
        /// <returns></returns>
        Task<List<QualityParameterGroup>> GetQualityParameterGrouplList(string filter, int page, int perPage);

        /// <summary>
        /// Retrives a QualityAssuranceParameter item meeting search criteria
        /// </summary>      
        /// <param name="filter"></param>               
        /// <param name="page"></param>               
        /// <param name="perPage"></param>               
        /// <returns></returns>
        Task<List<QualityAssuranceParameter>> GetQualityAssuranceParameterlList(string filter, int page, int perPage);

        /// <summary>
        /// Delete a QualityAction 
        /// </summary>      
        /// <param name="qualityActionId"></param>   
        /// <returns></returns>
        Task<bool> DeleteQualityActionById(string qualityActionId);

        /// <summary>
        /// Delete a QualityAssuranceParameter 
        /// </summary>      
        /// <param name="qualityAssuranceId"></param>   
        /// <returns></returns>
        Task<bool> DeleteQualityAssuranceParameterById(string qualityAssuranceId);


        /// <summary>
        /// Delete a QualityParameterGroup 
        /// </summary>      
        /// <param name="qualityParameterGroupId"></param>   
        /// <returns></returns>
        Task<bool> DeleteQualityParameterGroupById(string qualityParameterGroupId);

        /// <summary>
        /// Delete a QualityReportType
        /// </summary>      
        /// <param name="qualityReportTypeId"></param>   
        /// <returns></returns>
        Task<bool> DeleteQualityReportTypeById(string qualityReportTypeId);


        /// <summary>
        /// Delete a QualityTrafficLight
        /// </summary>      
        /// <param name="qualityTrafficLightId"></param>   
        /// <returns></returns>
        Task<bool> DeleteQualityTrafficLightById(string qualityTrafficLightId);

    }
}
