using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SunttelTradePointB.Client.Interfaces.MasterTablesInterfaces;
using SunttelTradePointB.Server.Controllers.MasterTablesCtrl;
using SunttelTradePointB.Server.Interfaces.MasterTablesInterfaces;
using SunttelTradePointB.Server.Interfaces.SalesBkServices;
using SunttelTradePointB.Shared.Common;
using SunttelTradePointB.Shared.ImportingData;
using SunttelTradePointB.Shared.Sales;
using SunttelTradePointB.Shared.SquadsMgr;
using ZstdSharp.Unsafe;

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

        #region Commercial Document
        /// <summary>
        /// Retrieves a Sales Document matching with its Id
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAdress"></param>
        /// <param name="documentId"></param>
        /// <returns></returns>
        [HttpGet]
        [ActionName("GetCommercialDocumentById")]
        public async Task<IActionResult> GetCommercialDocumentById(string userId, string ipAdress, string documentId)
        {
            var customHeaderValue = Request.Headers["SquadId"];
            var squadId = customHeaderValue.ToString() ?? ""; // Request.Headers["SquadId"];
            var response = await _commercialDocument.GetCommercialDocumentById(userId, ipAdress, squadId, documentId);

            if (response.IsSuccess)
            {
                return Ok(response.CommercialDocument);
            }
            else
                return NotFound(response.ErrorDescription);
        }

        /// <summary>
        /// Insert / Update a Commercial Document
        /// </summary>
        /// <param name="commercialDocument"></param>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <returns></returns>
        [HttpPost]
        [ActionName("SaveCommercialDocument")]
        public async Task<IActionResult> SaveCommercialDocument(string userId, string ipAddress, [FromBody] CommercialDocument commercialDocument)
        {
            var response = await _commercialDocument.SaveCommercialDocument(userId, ipAddress, commercialDocument);

            if (response.IsSuccess)
            {
                return Ok(response.commercialDocument);
            }
            else
            {
                return NotFound(response.ErrorDescription);
            }
        }


        /// <summary>
        /// Retrieves a list of Commercial documents of the Squad for the date span and Document type
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="documentTypeId"></param>
        /// <param name="filter"></param>
        /// <param name="page"></param>
        /// <param name="perPage"></param>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpGet]
        [ActionName("GetCommercialDocumentsByDateSpan")]
        public async Task<IActionResult> GetCommercialDocumentsByDateSpan(string userId, string ipAddress, DateTime startDate, DateTime endDate, string documentTypeId, string? vendorName, string? filter = null, int? page = 1, int? perPage = 10)
        {
            var customHeaderValue = Request.Headers["SquadId"];
            var squadId = customHeaderValue.ToString() ?? ""; // Request.Headers["SquadId"];
            var response = await _commercialDocument.GetCommercialDocumentsByDateSpan(userId, ipAddress, squadId, startDate, endDate, documentTypeId, vendorName, filter, page, perPage);

            if (response.IsSuccess)
            {
                return Ok(response.CommercialDocuments);
            }
            else
                return NotFound(response.ErrorDescription);
        }

        /// <summary>
        /// Retrieves a Sales Document matching with its Id
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAdress"></param>
        /// <param name="documentId"></param>
        /// <returns></returns>
        [HttpGet]
        [ActionName("UpdateCommercialDocumentShippingSummary")]
        public async Task<IActionResult> UpdateCommercialDocumentShippingSummary(string userId, string ipAdress, string documentId)
        {
            var customHeaderValue = Request.Headers["SquadId"];
            var squadId = customHeaderValue.ToString() ?? ""; // Request.Headers["SquadId"];
            var response = await _commercialDocument.UpdateCommercialDocumentShippingSummary(userId, ipAdress, squadId, documentId);

            if (response.IsSuccess)
            {
                return Ok(response.CommercialDocument);
            }
            else
                return NotFound(response.ErrorDescription);
        }


        #endregion

        #region Commercial Document Type
        /// <summary>
        /// Retrieves a list of commercial document types with the posibility to receive an optional paremeter
        /// </summary>
        /// <param name="filterCondition"></param>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <param name="isASale"></param>
        /// <returns></returns>
        [HttpGet]
        [ActionName("GetCommercialDocumentTypes")]
        public async Task<IActionResult> GetCommercialDocumentTypes(string userId, string ipAddress, bool isASale, string? filterCondition = null)
        {
            var customHeaderValue = Request.Headers["SquadId"];
            var squadId = customHeaderValue.ToString() ?? ""; // Request.Headers["SquadId"];
            var response = await _commercialDocument.GetCommercialDocumentTypes(userId, ipAddress, squadId, isASale, filterCondition);

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
        /// Retrieves a commercial document types object by Id
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <param name="commercialDocumentTypeId"></param>
        /// <returns></returns>
        [HttpGet]
        [ActionName("GetCommercialDocumentTypeById")]
        public async Task<IActionResult> GetCommercialDocumentTypeById(string userId, string ipAddress, string commercialDocumentTypeId)
        {
            var customHeaderValue = Request.Headers["SquadId"];
            var squadId = customHeaderValue.ToString() ?? ""; // Request.Headers["SquadId"];

            var response = await _commercialDocument.GetCommercialDocumentTypeById(userId, ipAddress, squadId, commercialDocumentTypeId);

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
        /// Insert / Update a commercial document types
        /// </summary>
        /// <param name="commercialDocumentType"></param>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <returns></returns>
        [HttpPost]
        [ActionName("SaveCommercialDocumentType")]
        public async Task<IActionResult> SaveCommercialDocumentType(string userId, string ipAddress, [FromBody] CommercialDocumentType commercialDocumentType)
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

        /// <summary>
        /// Delete a CommercialDocumentType if not associated with a Quality
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <param name="commercialDocumentTypeId"></param>
        /// <returns></returns>
        [HttpDelete]
        [ActionName("DeleteCommercialDocumentTypeById")]
        public async Task<IActionResult> DeleteCommercialDocumentTypeById(string userId, string ipAddress, string commercialDocumentTypeId)
        {
            try
            {
                var customHeaderValue = Request.Headers["SquadId"];
                var squadId = customHeaderValue.ToString() ?? "";
                (bool IsSuccess, bool iCanRemoveIt, string? ErrorDescription) response = await _commercialDocument.DeleteCommercialDocumentTypeById(userId, ipAddress, squadId, commercialDocumentTypeId);
                if (response.IsSuccess)
                {
                    if (response.iCanRemoveIt)
                    {
                        return Ok();
                    }
                    else
                    {
                        return NotFound("in use");
                    }
                }
                else
                {
                    return NotFound(response.ErrorDescription);
                }

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        #endregion

        #region Finance Status
        /// <summary>
        /// Retrieves a list of Transactional Item Types with the posibility to receive an optional paremeter
        /// </summary>
        /// <param name="filterCondition"></param>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <returns></returns>
        [HttpGet]
        [ActionName("GetFinanceStatuses")]
        public async Task<IActionResult> GetFinanceStatuses(string userId, string ipAddress, string? filterCondition = null)
        {
            var response = await _commercialDocument.GetFinanceStatuses(userId, ipAddress, filterCondition);

            if (response.IsSuccess)
            {
                return Ok(response.financeStatuses);
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
        [ActionName("GetFiananceStatusById")]
        public async Task<IActionResult> GetFiananceStatusById(string userId, string ipAddress, string commercialDocumentTypeId)
        {
            var response = await _commercialDocument.GetFiananceStatusById(userId, ipAddress, commercialDocumentTypeId);

            if (response.IsSuccess)
            {
                return Ok(response.financeStatus);
            }
            else
            {
                return NotFound(response.ErrorDescription);
            }
        }

        /// <summary>
        /// Insert / Update a Transactional Item Type
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <param name="financeStatus"></param>
        /// <returns></returns>
        [HttpPost]
        [ActionName("SaveFinanceStatus")]
        public async Task<IActionResult> SaveFinanceStatus(string userId, string ipAddress, [FromBody] FinanceStatus financeStatus)
        {
            var response = await _commercialDocument.SaveFinanceStatus(userId, ipAddress, financeStatus);

            if (response.IsSuccess)
            {
                return Ok(response.financeStatus);
            }
            else
            {
                return NotFound(response.ErrorDescription);
            }
        }
        #endregion

        #region Business Line Docs
        /// <summary>
        /// Save a Business line document
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <param name="business"></param>
        /// <returns></returns>
        [HttpPost]
        [ActionName("SaveBusinessLineDoc")]
        public async Task<IActionResult> SaveBusinessLineDoc(string userId, string ipAddress, [FromBody] BusinessLine business)
        {

            var customHeaderValue = Request.Headers["SquadId"];
            var squadId = customHeaderValue.ToString() ?? ""; // Request.Headers["SquadId"];

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
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpGet]
        [ActionName("GetBusinessLinesDocs")]
        public async Task<IActionResult> GetBusinessLinesDocs(string userId, string ipAddress, string filter)
        {
            var customHeaderValue = Request.Headers["SquadId"];
            var squadId = customHeaderValue.ToString() ?? ""; // Request.Headers["SquadId"];

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
        /// <param name="businessLineDocId"></param>
        /// <returns></returns>
        [HttpGet]
        [ActionName("GetBusinessLineDocById")]
        public async Task<IActionResult> GetBusinessLineDocById(string userId, string ipAddress, string businessLineDocId)
        {
            var customHeaderValue = Request.Headers["SquadId"];
            var squadId = customHeaderValue.ToString() ?? ""; // Request.Headers["SquadId"];

            var response = await _commercialDocument.GetBusinessLineDocById(userId, ipAddress, squadId, businessLineDocId);

            if (response.IsSuccess)
            {
                return Ok(response.businessLineDoc);
            }
            else
                return NotFound(response.ErrorDescription);

        }


        /// <summary>
        /// Delete a BusinessLine if not associated with a Quality
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <param name="businessLineId"></param>
        /// <returns></returns>
        [HttpDelete]
        [ActionName("DeleteBusinessLineById")]
        public async Task<IActionResult> DeleteBusinessLineById(string userId, string ipAddress, string businessLineId)
        {
            try
            {
                var customHeaderValue = Request.Headers["SquadId"];
                var squadId = customHeaderValue.ToString() ?? "";
                (bool IsSuccess, bool iCanRemoveIt, string? ErrorDescription) response = await _commercialDocument.DeleteBusinessLineById(userId, ipAddress, squadId, businessLineId);
                if (response.IsSuccess)
                {
                    if (response.iCanRemoveIt)
                    {
                        return Ok();
                    }
                    else
                    {
                        return NotFound("in use");
                    }
                }
                else
                {
                    return NotFound(response.ErrorDescription);
                }

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }



        #endregion

        #region shipping status docs
        /// <summary>
        /// Save a Shipping status
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <param name="shipping"></param>
        /// <returns></returns>
        [HttpPost]
        [ActionName("SaveShippingStatus")]
        public async Task<IActionResult> SaveShippingStatus(string userId, string ipAddress, [FromBody] ShippingStatus shipping)
        {
            var customHeaderValue = Request.Headers["SquadId"];
            var squadId = customHeaderValue.ToString() ?? ""; // Request.Headers["SquadId"];

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
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpGet]
        [ActionName("GetShippingStatusDocs")]
        public async Task<IActionResult> GetShippingStatusDocs(string userId, string ipAddress, string filter)
        {
            var customHeaderValue = Request.Headers["SquadId"];
            var squadId = customHeaderValue.ToString() ?? ""; // Request.Headers["SquadId"];

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
        /// <param name="shippingStatusId"></param>
        /// <returns></returns>
        [HttpGet]
        [ActionName("GetShippingStatusDocById")]
        public async Task<IActionResult> GetShippingStatusDocById(string userId, string ipAddress, string shippingStatusId)
        {
            var customHeaderValue = Request.Headers["SquadId"];
            var squadId = customHeaderValue.ToString() ?? ""; // Request.Headers["SquadId"];

            var response = await _commercialDocument.GetShippingStatusDocById(userId, ipAddress, squadId, shippingStatusId);

            if (response.IsSuccess)
            {
                return Ok(response.shippingStatus);
            }
            else
                return NotFound(response.ErrorDescription);

        }

        /// <summary>
        /// Retrieves a list of Commercial documents of the Squad for the date span and warehouseId
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <param name="shippingDate"></param>
        /// <param name="warehouseId"></param>
        /// <param name="filter"></param>
        /// <param name="page"></param>
        /// <param name="perPage"></param>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpGet]
        [ActionName("GetShippingInvoices")]
        public async Task<IActionResult> GetShippingInvoices(string userId, string ipAddress, DateTime shippingDate, string warehouseId, string? filter = null, int? page = 1, int? perPage = 10)
        {
            var customHeaderValue = Request.Headers["SquadId"];
            var squadId = customHeaderValue.ToString() ?? ""; // Request.Headers["SquadId"];
            var response = await _commercialDocument.GetShippingInvoices(userId, ipAddress, squadId, shippingDate, warehouseId, filter, page, perPage);

            if (response.IsSuccess)
            {
                return Ok(response.CommercialDocuments);
            }
            else
                return NotFound(response.ErrorDescription);
        }

        /// <summary>
        /// Delete a BusinessLine if not associated with a Quality
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <param name="shippingStatusId"></param>
        /// <returns></returns>
        [HttpDelete]
        [ActionName("DeleteShippingStatusById")]
        public async Task<IActionResult> DeleteShippingStatusById(string userId, string ipAddress, string shippingStatusId)
        {
            try
            {
                var customHeaderValue = Request.Headers["SquadId"];
                var squadId = customHeaderValue.ToString() ?? "";
                (bool IsSuccess, bool iCanRemoveIt, string? ErrorDescription) response = await _commercialDocument.DeleteShippingStatusById(userId, ipAddress, squadId, shippingStatusId);
                if (response.IsSuccess)
                {
                    if (response.iCanRemoveIt)
                    {
                        return Ok();
                    }
                    else
                    {
                        return NotFound("in use");
                    }
                }
                else
                {
                    return NotFound(response.ErrorDescription);
                }

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        #endregion

        #region Commercial Document Detail
        /// <summary>
        /// Saves an commercial document. If it doesn't exists, it'll be created
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAdress"></param>
        /// <param name="salesDocumentItemsDetails"></param>
        /// <returns></returns>
        [HttpPost]
        [ActionName("SaveCommercialDocumentDetail")]
        public async Task<IActionResult> SaveCommercialDocumentDetail(string userId, string ipAdress, [FromBody] SalesDocumentItemsDetails salesDocumentItemsDetails)
        {
            var customHeaderValue = Request.Headers["SquadId"];
            var squadId = customHeaderValue.ToString() ?? ""; // Request.Headers["SquadId"];

            var response = await _commercialDocument.SaveCommercialDocumentDetail(userId, ipAdress, squadId, salesDocumentItemsDetails);

            if (response.IsSuccess)
            {
                return Ok(salesDocumentItemsDetails);
            }
            else
                return NotFound(response.ErrorDescription);
        }

        /// <summary>
        /// Retrives a list of Transactional Items matching a search criteria
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <param name="commercialDocumentId"></param>
        /// <param name="page"></param>
        /// <param name="perPage"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpGet]
        [ActionName("GetCommercialDocumentDetails")]
        public async Task<IActionResult> GetCommercialDocumentDetails(
            string userId,
            string ipAddress,
            string commercialDocumentId,
            int? page = 1,
            int? perPage = 10)
        {

            var customHeaderValue = Request.Headers["SquadId"];
            var squadId = customHeaderValue.ToString() ?? ""; // Request.Headers["SquadId"];

            var response = await _commercialDocument.GetCommercialDocumentDetails(userId, ipAddress, commercialDocumentId, squadId, page, perPage);

            if (response.IsSuccess)
            {
                return Ok(response.GetCommercialDocumentDetails);
            }
            else
                return NotFound(response.ErrorDescription);
        }


        /// <summary>
        /// Retrives a list of Transactional Items matching a search criteria
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <param name="commercialDocumentId"></param>
        /// <param name="page"></param>
        /// <param name="perPage"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpGet]
        [ActionName("GetProcurementDetails")]
        public async Task<IActionResult> GetProcurementDetails(
            string userId,
            string ipAddress,
            int? page = 1,
            int? perPage = 10)
        {

            var customHeaderValue = Request.Headers["SquadId"];
            var squadId = customHeaderValue.ToString() ?? ""; // Request.Headers["SquadId"];

            var response = await _commercialDocument.GetProcurementDetails(userId, ipAddress, squadId, page, perPage);

            if (response.IsSuccess)
            {
                return Ok(response.GetProcurementDetails);
            }
            else
                return NotFound(response.ErrorDescription);
        }



        /// <summary>
        /// Retrives a list of Transactional Items matching a search criteria
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <param name="commercialDocumentId"></param>
        /// <param name="page"></param>
        /// <param name="perPage"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpGet]
        [ActionName("GetProcurementList")]
        public async Task<IActionResult> GetProcurementList(
            string userId,
            string ipAddress,
            int? page = 1,
            int? perPage = 10)
        {

            var customHeaderValue = Request.Headers["SquadId"];
            var squadId = customHeaderValue.ToString() ?? "";

            var response = await _commercialDocument.GetProcurementList(userId, ipAddress, squadId, page, perPage);

            if (response.IsSuccess)
            {
                return Ok(response.GetProcurementList);
            }
            else
                return NotFound(response.ErrorDescription);
        }

        /// <summary>
        /// Retrieves an object of a transactional Item by id
        /// </summary>
        /// <param name="commercialDocumentDetailId"></param>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <returns></returns>
        [HttpGet]
        [ActionName("GetCommercialDocumentDetailById")]
        public async Task<IActionResult> GetCommercialDocumentDetailById(string userId, string ipAddress, string commercialDocumentDetailId)
        {
            var response = await _commercialDocument.GetCommercialDocumentDetailById(userId, ipAddress, commercialDocumentDetailId);

            if (response.IsSuccess)
            {
                return Ok(response.GetCommercialDocumentDetailsById);
            }
            else
                return NotFound(response.ErrorDescription);
        }


        #endregion

        #region Account Receivable
        /// <summary>
        /// Retrieves a list of Commercial documents of the Squad for the date 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <param name="shippingDate"></param>
        /// <param name="filter"></param>
        /// <param name="page"></param>
        /// <param name="perPage"></param>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpGet]
        [ActionName("GetAccountReceivable")]
        public async Task<IActionResult> GetAccountReceivable(string userId, string ipAddress, DateTime shippingDate, string? filter = null, int? page = 1, int? perPage = 10)
        {
            var customHeaderValue = Request.Headers["SquadId"];
            var squadId = customHeaderValue.ToString() ?? ""; // Request.Headers["SquadId"];
            var response = await _commercialDocument.GetAccountReceivable(userId, ipAddress, squadId, shippingDate, filter, page, perPage);

            if (response.IsSuccess)
            {
                return Ok(response.CommercialDocuments);
            }
            else
                return NotFound(response.ErrorDescription);
        }
        #endregion

        #region Sales BI
        /// <summary>
        /// Retrieves a list of Commercial documents of the Squad for the date span and Document type
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="documentTypeId"></param>
        /// <param name="filter"></param>
        /// <param name="page"></param>
        /// <param name="perPage"></param>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpGet]
        [ActionName("GetSalesBI")]
        public async Task<IActionResult> GetSalesBI(string userId, string ipAddress, DateTime startDate, DateTime endDate, string documentTypeId, string? filter = null, int? page = 1, int? perPage = 10)
        {
            var customHeaderValue = Request.Headers["SquadId"];
            var squadId = customHeaderValue.ToString().ToUpper() ?? ""; // Request.Headers["SquadId"];
            var response = await _commercialDocument.GetSalesBI(userId, ipAddress, squadId, startDate, endDate, documentTypeId, filter, page, perPage);

            if (response.IsSuccess)
            {
                return Ok(response.CommercialDocuments);
            }
            else
                return NotFound(response.ErrorDescription);
        }
        #endregion

        // Para importar desde un archivo csv
        /// <summary>
        /// Upload file csv a commercial documents
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <param name="file"></param>
        /// <returns></returns>
        [HttpPost]
        [ActionName("SaveCommercialDocumentsCSV")]
        public async Task<IActionResult> SaveCommercialDocumentsCSV(string userId, string ipAddress, IFormFile file)
        {
            var customHeaderValue = Request.Headers["SquadId"];
            var squadId = customHeaderValue.ToString().ToUpper() ?? ""; // Request.Headers["SquadId"];
            var response = await _commercialDocument.SaveCommercialDocumentsCSV(userId, ipAddress, squadId, file);

            if (response.IsSuccess)
            {
                return Ok(response.CommercialDocumentsList);
            }
            else
            {
                return NotFound(response.ErrorDescription);
            }
        }



        /// <summary>
        /// Delete a CommercialDocumentType if not associated with a Quality
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <param name="commercialDocumentTypeId"></param>
        /// <returns></returns>
        [HttpDelete]
        [ActionName("DeleteSaleDetailById")]
        public async Task<IActionResult> DeleteSaleDetailById(string userId, string ipAddress, string saleId, string detailId)
        {
            try
            {
                var customHeaderValue = Request.Headers["SquadId"];
                var squadId = customHeaderValue.ToString() ?? "";
                (bool IsSuccess, bool iCanRemoveIt, string? ErrorDescription) response = await _commercialDocument.DeleteSaleDetailById(userId, ipAddress, squadId, saleId, detailId);
                if (response.IsSuccess)
                {
                    if (response.iCanRemoveIt)
                    {
                        return Ok();
                    }
                    else
                    {
                        return NotFound("Does not exist");
                    }
                }
                else
                {
                    return NotFound(response.ErrorDescription);
                }

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }




    }
}
