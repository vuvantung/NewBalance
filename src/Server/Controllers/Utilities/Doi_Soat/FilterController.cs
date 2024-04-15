using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using NewBalance.Infrastructure.OR.IRepository;
using NewBalance.Infrastructure.OR.Repository;
using System.Net;
using System.Threading.Tasks;
using System;


namespace NewBalance.Server.Controllers.Utilities.Doi_Soat
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilterController : ControllerBase
    {
        private IConfiguration _config;
        private IFilterRepository _filterRepository = null;

        public FilterController( IConfiguration config, IFilterRepository filterRepository )
        {
            _config = config;
            _filterRepository = filterRepository;
        }

        [HttpGet]
        [Route("GetFilterAccount")]
        public async Task<IActionResult> GetFilterAccount()
        {
            try
            {
                var response = await _filterRepository.GetFilterAccountAsync();
                return Ok(response);
            }
            catch ( Exception ex )
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        [Route("GetFilterTypeReport")]
        public async Task<IActionResult> GetFilterTypeReport()
        {
            try
            {
                var response = await _filterRepository.GetFilterTypeReportAsync();
                return Ok(response);
            }
            catch ( Exception ex )
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
