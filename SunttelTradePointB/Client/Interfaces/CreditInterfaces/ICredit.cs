
using SunttelTradePointB.Shared.Accounting;
using SunttelTradePointB.Shared.Common;
using SunttelTradePointB.Shared.InvetoryModels;

namespace SunttelTradePointB.Client.Interfaces.ICreditInterfaces
{
    public interface ICredit
    {
        /// <summary>
        /// Save a creditDocument item 
        /// </summary>
        /// <param name="creditDocument"></param>
        /// <returns></returns>   
        Task<CreditDocument> SaveCreditDocument(CreditDocument creditDocument);

        /// <summary>
        /// Save a creditType item 
        /// </summary>
        /// <param name="creditType"></param>
        /// <returns></returns>   
        Task<CreditType> SaveCreditType(CreditType creditType);


        /// <summary>
        /// Save a creditStatus item 
        /// </summary>
        /// <param name="creditStatus"></param>
        /// <returns></returns>  
        Task<CreditStatus> SaveCreditStatus(CreditStatus creditStatus);

        /// <summary>
        /// Save a CreditReason item 
        /// </summary>
        /// <param name="creditReason"></param>
        /// <returns></returns>  
        Task<CreditReason> SaveCreditReason(CreditReason creditReason);


        /// <summary>
        /// Retrives a list with creditDocument items meeting search criteria
        /// </summary>
        /// <param name="filter"></param>           
        /// <param name="endDate"></param>           
        /// <param name="filter"></param>           
        /// <param name="page"></param>           
        /// <param name="perPage"></param>           
        /// <returns></returns>
        Task<List<CreditDocument>> GetCreditDocumentList(DateTime startDate, DateTime endDate ,bool isASale, string? filter = null, int? page = 1, int? perPage = 10);

        /// <summary>
        /// Retrives a item with creditDocument meeting search criteria
        /// </summary>
        /// <param name="creditId"></param>       
        /// <returns></returns>
        Task<CreditDocument> GetCreditDocumentById(string creditId);


        /// <summary>
        /// Retrives a list with creditType meeting search criteria
        /// </summary>
        /// <param name="filter"></param>        
        /// <param name="filter"></param>           
        /// <param name="page"></param>           
        /// <param name="perPage"></param>         
        /// <returns></returns>
        Task<List<CreditType>> GetCreditTypes(string? filter = null, int? page = 1, int? perPage = 10);

        /// <summary>
        /// Retrives a item with CreditType meeting search criteria
        /// </summary>
        /// <param name="creditTypeId"></param>         
        /// <returns></returns>
        Task<CreditType> GetCreditTypeById(string creditTypeId);


        /// <summary>
        /// Retrives a list with CreditStatus meeting search criteria
        /// </summary>
        /// <param name="filter"></param>           
        /// <param name="endDate"></param>           
        /// <param name="filter"></param>           
        /// <param name="page"></param>           
        /// <param name="perPage"></param>         
        /// <returns></returns>
        Task<List<CreditStatus>> GetCreditStatuses(DateTime startDate, DateTime endDate, string? filter = null, int? page = 1, int? perPage = 10);


        /// <summary>
        /// Retrives a item with CreditStatus meeting search criteria
        /// </summary>
        /// <param name="creditStatusById"></param>      
        /// <returns></returns>
        Task<CreditStatus> CreditStatusById(string creditStatusById);

        /// <summary>
        /// Retrives a item with CreditReason meeting search criteria
        /// </summary>
        /// <param name="creditReasonById"></param>      
        /// <returns></returns>
        Task<CreditReason> CreditReasonById(string creditReasonById);


        /// <summary>
        /// Retrives a item with CreditReason meeting search criteria
        /// </summary>
        /// <param name="filter"></param>      
        /// <param name="page"></param>      
        /// <param name="perPage"></param>      
        /// <returns></returns>
        Task<List<CreditReason>> GetCreditReasonList(string? filter = null, int? page = 1, int? perPage = 10);

         /// <summary>
        /// Delete an CreditReason not associated with CreditDocument
        /// </summary>
        /// <param name="reasonId"></param>      
        /// <returns></returns>
        Task<bool> DeleteCreditReasonsById(string reasonId);

         /// <summary>
        /// Delete an CreditStatus not associated with CreditDocument
        /// </summary>
        /// <param name="statusId"></param>      
        /// <returns></returns>
        
        Task<bool> DeleteCreditStatusById(string statusId);

         /// <summary>
       /// Delete an CreditType not associated with CreditDocument
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <param name="squadId"></param>
        /// <param name="creditReasonId"></param>       
        /// <returns></returns>
        Task<bool> DeleteCreditTypeById(string creditTypeId);



    }


}
