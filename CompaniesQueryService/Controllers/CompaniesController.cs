using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CompaniesApi.DataTransferObjects;
using CompaniesApi.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing.Constraints;
using Microsoft.Extensions.Logging;

namespace CompaniesQueryService.Controllers
{
    /// <summary>
    /// The controller for Getting companies information
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CompaniesController : Controller
    {
        private readonly ICompanyReadProxy _readProxy;
        private readonly ILogger<CompaniesController> _logger;

        /// <summary>
        /// Constructs a CompaniesController
        /// </summary>
        /// <param name="readProxy">The read proxy</param>
        /// <param name="logger">The logger</param>
        public CompaniesController(ICompanyReadProxy readProxy, ILogger<CompaniesController> logger)
        {
            _readProxy = readProxy;
            _logger = logger;
        }

        /// <summary>
        /// Gets all of the companies symbols
        /// </summary>
        /// <returns>A list of company symbols</returns>
        [Route("GetAllSymbols")]
        [HttpGet]
        [ProducesResponseType(typeof(List<StockSymbolDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Produces("application/json")]
        public async Task<ActionResult<List<StockSymbolDTO>>> GetAllCompanySymbols()
        {
            var companiesSymbols = await _readProxy.GetCompanySymbols();

            if (companiesSymbols.Count == 0)
            {
                _logger.LogError("No company information");
                return NotFound();
            }

            return companiesSymbols;
        }

        /// <summary>
        /// Gets all of the information for all of the companies
        /// </summary>
        /// <returns>A list of company information</returns>
        [Route("GetCompaniesInformation")]
        [HttpGet]
        [ProducesResponseType(typeof(List<CompanyInformationDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Produces("application/json")]
        public async Task<ActionResult<List<CompanyListDto>>> GetCompaniesInformation()
        {
            var companiesInformation = await _readProxy.GetAllCompanyInformation();

            if (companiesInformation.Count == 0)
            {
                _logger.LogError("No company information");
                return NotFound();
            }

            return companiesInformation;
        }

        /// <summary>
        /// Gets information about a specific company
        /// </summary>
        /// <param name="companyId">The id of the company</param>
        /// <returns>Information about a specific company</returns>
        [Route("GetCompanyInformation/{companyId}")]
        [HttpGet]
        [ProducesResponseType(typeof(CompanyInformationDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Produces("application/json")]
        public async Task<ActionResult<CompanyInformationDto>> GetCompanyInformation(Guid companyId)
        {
            if (companyId.Equals(Guid.Empty))
            {
                _logger.LogError("CompanyId is empty");
                return NotFound();
            }

            var companyInformation = await _readProxy.GetCompanyInformation(companyId);

            if (companyInformation.CompanyId == Guid.Empty)
            {
                _logger.LogError("No companies found with the given id " + companyId);
                return NotFound();
            }

            return companyInformation;
        }

        /// <summary>
        /// Gets the companies list information.
        /// </summary>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="itemsPerPage">The amount of items per page.</param>
        /// <returns>The company list with pagination</returns>
        [Route("GetCompaniesList/pageNumber={pageNumber},itemsPerPage={itemsPerPage}")]
        [HttpGet]
        [ProducesResponseType(typeof(CompanyListDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Produces("application/json")]
        public async Task<ActionResult<CompanyPageListDto>> GetCompaniesListInformation(int pageNumber, int itemsPerPage)
        {
            var startIndex = pageNumber == 0 ? 0 : pageNumber * itemsPerPage + 1;
            var endIndex = startIndex + itemsPerPage;
            var companyListPage = await _readProxy.GetCompanyList(startIndex, endIndex, pageNumber);

            if (!companyListPage.Data.Any())
            {
                _logger.LogError("No data was found in the database");
                return NotFound();
            }

            return companyListPage;
        }
    }
}