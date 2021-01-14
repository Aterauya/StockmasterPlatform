using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CompaniesApi.DataTransferObjects;
using CompaniesApi.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CompaniesQueryService.Controllers
{
    public class CompaniesController : Controller
    {
        private readonly ICompanyReadProxy _readProxy;
        private readonly ILogger<CompaniesController> _logger;

        public CompaniesController(ICompanyReadProxy readProxy, ILogger<CompaniesController> logger)
        {
            _readProxy = readProxy;
            _logger = logger;
        }

        [Route("GetCompaniesInformation")]
        [HttpGet]
        [ProducesResponseType(typeof(List<CompanyInformationDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Produces("application/json")]
        public async Task<ActionResult<List<CompanyInformationDto>>> GetCompaniesInformation()
        {
            var companiesInformation = await _readProxy.GetCompanyInformation();

            if (companiesInformation.Count == 0)
            {
                _logger.LogError("No company information");
                return NotFound();
            }

            return companiesInformation;
        }

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
    }
}