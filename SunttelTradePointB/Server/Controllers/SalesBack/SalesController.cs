using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SunttelTradePointB.Client.Interfaces.MasterTablesInterfaces;
using SunttelTradePointB.Server.Controllers.MasterTablesCtrl;
using SunttelTradePointB.Server.Interfaces.MasterTablesInterfaces;
using SunttelTradePointB.Server.Interfaces.SalesBkServices;
using SunttelTradePointB.Shared.Sales;

namespace SunttelTradePointB.Server.Controllers.SalesBack
{


    /// <summary>
    /// Controller for sales
    /// </summary>
    [Tags("Sales operations EndPoints")]
    [Route("api/[controller]/[action]")]
    public class SalesController : ControllerBase
    {
        private ICommercialDocument _commercialDocument;
        private readonly ILogger<SalesController> _logger;
        IConfiguration config;

        public SalesController(ILogger<SalesController> logger, IConfiguration _config, ICommercialDocument commercialDocument)
        {
            _logger = logger;
            config = _config;
            _commercialDocument = commercialDocument;
        }

        /// <summary>
        /// Retrieves a Sales Documemt matching with its Id
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAdress"></param>
        /// <param name="squadId"></param>
        /// <param name="documentId"></param>
        /// <returns></returns>
        [HttpGet]
        [ActionName("GetCommercialDocumentById")]
        public async Task<IActionResult> GetCommercialDocumentById(string userId, string ipAdress, string squadId, string documentId)
        {

            var response = await _commercialDocument.GetCommercialDocumentById(userId, ipAdress, squadId, documentId);

            if (response.IsSuccess)
            {
                return Ok(response.CommercialDocument);
            }
            else
                return NotFound(response.ErrorDescription);
        }


        /// <summary>
        /// Retrieves a list of Commercial documents of the Squad for the date span and Document type
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAdress"></param>
        /// <param name="squadId"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="documentTypeId"></param>
        /// <returns></returns>
        [HttpGet]
        [ActionName("GetCommercialDocumentsByDateSpan")]
        public async Task<IActionResult> GetCommercialDocumentsByDateSpan(string userId, string ipAdress, string squadId, DateTime startDate, DateTime endDate, string documentTypeId)
        {
            var response = await _commercialDocument.GetCommercialDocumentsByDateSpan(userId, ipAdress, squadId, startDate, endDate, documentTypeId);

            if (response.IsSuccess)
            {
                return Ok(response.CommercialDocuments);
            }
            else
                return NotFound(response.ErrorDescription);
        }

        /// <summary>
        /// Retrieves a list of Transactional Item Types with the posibility to receive an optional paremeter
        /// </summary>
        /// <param name="filterCondition"></param>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <returns></returns>
        [HttpGet]
        [ActionName("GetCommercialDocumentTypes")]
        public async Task<IActionResult> GetCommercialDocumentTypes(string userId, string ipAddress, string? filterCondition = null)
        {
            var response = await _commercialDocument.GetCommercialDocumentTypes(userId, ipAddress, filterCondition);

            if (response.IsSuccess)
            {
                return Ok(response.commercialDocumentTypes);
            }
            else
            {
                return NotFound(response.ErrorDescription);
            }
        }

        /// <summary>
        /// Retrieves a Transactional Item Type object by Id
        /// </summary>
        /// <param name="commercialDocumentTypeId"></param>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <returns></returns>
        [HttpGet]
        [ActionName("GetCommercialDocumentTypeById")]
        public async Task<IActionResult> GetCommercialDocumentTypeById(string userId, string ipAddress, string commercialDocumentTypeId)
        {
            var response = await _commercialDocument.GetCommercialDocumentTypeById(userId, ipAddress, commercialDocumentTypeId);

            if (response.IsSuccess)
            {
                return Ok(response.commercialDocumentType);
            }
            else
            {
                return NotFound(response.ErrorDescription);
            }
        }

        /// <summary>
        /// Insert / Update a Transactional Item Type
        /// </summary>
        /// <param name="commercialDocumentType"></param>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <returns></returns>
        [HttpPost]
        [ActionName("SaveCommercialDocumentType")]
        public async Task<IActionResult> SaveCommercialDocumentType(string userId, string ipAddress, CommercialDocumentType commercialDocumentType)
        {
            var response = await _commercialDocument.SaveCommercialDocumentType(userId, ipAddress, commercialDocumentType);

            if (response.IsSuccess)
            {
                return Ok(response.commercialDocumentType);
            }
            else
            {
                return NotFound(response.ErrorDescription);
            }
        }

        #region Business Line Docs
        /// <summary>
        /// Save a Business line document
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <param name="squadId"></param>
        /// <param name="business"></param>
        /// <returns></returns>
        [HttpPost]
        [ActionName("SaveBusinessLineDoc")]
        public async Task<IActionResult> SaveBusinessLineDoc(string userId, string ipAddress, string squadId, [FromBody] BusinessLine business)
        {

            var response = await _commercialDocument.SaveBusinessLineDoc(userId, ipAddress, squadId, business);

            if (response.IsSuccess)
            {
                return Ok(response.entity);
            }
            else
                return NotFound(response.ErrorDescription);

        }

        /// <summary>
        /// Retrieves a the list of Business lines documents
        /// </summary>
        /// /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <param name="squadId"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpGet]
        [ActionName("GetBusinessLinesDocs")]
        public async Task<IActionResult> GetBusinessLinesDocs(string userId, string ipAddress, string squadId, string filter)
        {
            var response = await _commercialDocument.GetBusinessLinesDocs(userId, ipAddress, squadId, filter);

            if (response.IsSuccess)
            {
                return Ok(response.businessesLinesDocs);
            }
            else
                return NotFound(response.ErrorDescription);

        }

        /// <summary>
        /// Retrieves a Business line document by id
        /// </summary>
        /// /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <param name="squadId"></param>
        /// <param name="businessLineDocId"></param>
        /// <returns></returns>
        [HttpGet]
        [ActionName("GetBusinessLineDocById")]
        public async Task<IActionResult> GetBusinessLineDocById(string userId, string ipAddress, string squadId, string businessLineDocId)
        {
            var response = await _commercialDocument.GetBusinessLineDocById(userId, ipAddress, squadId, businessLineDocId);

            if (response.IsSuccess)
            {
                return Ok(response.businessLineDoc);
            }
            else
                return NotFound(response.ErrorDescription);

        }
        #endregion

        #region shipping status docs
        /// <summary>
        /// Save a Shipping status
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <param name="squadId"></param>
        /// <param name="shipping"></param>
        /// <returns></returns>
        [HttpPost]
        [ActionName("SaveShippinStatus")]
        public async Task<IActionResult> SaveShippinStatus(string userId, string ipAddress, string squadId, [FromBody] ShippingStatus shipping)
        {

            var response = await _commercialDocument.SaveShippinStatus(userId, ipAddress, squadId, shipping);

            if (response.IsSuccess)
            {
                return Ok(response.entity);
            }
            else
                return NotFound(response.ErrorDescription);

        }

        /// <summary>
        /// Retrieves a the list of the shipping status docs
        /// </summary>
        /// /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <param name="squadId"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpGet]
        [ActionName("GetShippingStatusDocs")]
        public async Task<IActionResult> GetShippingStatusDocs(string userId, string ipAddress, string squadId, string filter)
        {
            var response = await _commercialDocument.GetShippingStatusDocs(userId, ipAddress, squadId, filter);

            if (response.IsSuccess)
            {
                return Ok(response.shippingStatuses);
            }
            else
                return NotFound(response.ErrorDescription);

        }

        /// <summary>
        /// Retrieves a Shipping status by Id
        /// </summary>
        /// /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <param name="squadId"></param>
        /// <param name="shippingStatusId"></param>
        /// <returns></returns>
        [HttpGet]
        [ActionName("GetShippingStatusDocById")]
        public async Task<IActionResult> GetShippingStatusDocById(string userId, string ipAddress, string squadId, string shippingStatusId)
        {
            var response = await _commercialDocument.GetShippingStatusDocById(userId, ipAddress, squadId, shippingStatusId);

            if (response.IsSuccess)
            {
                return Ok(response.shippingStatus);
            }
            else
                return NotFound(response.ErrorDescription);

        }
        #endregion
    }
}
