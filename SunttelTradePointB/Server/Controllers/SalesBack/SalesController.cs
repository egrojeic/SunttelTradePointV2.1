using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SunttelTradePointB.Server.Controllers.MasterTablesCtrl;
using SunttelTradePointB.Server.Interfaces.MasterTablesInterfaces;
using SunttelTradePointB.Server.Interfaces.SalesBkServices;

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
    }
}
