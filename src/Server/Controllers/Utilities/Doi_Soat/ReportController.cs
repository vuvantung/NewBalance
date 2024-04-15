using Microsoft.AspNetCore.Http;
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
    public class ReportController : ControllerBase
    {
        private IConfiguration _config;
        private IReportRepository _reportRepository = null;

        public ReportController( IConfiguration config, IReportRepository reportRepository )
        {
            _config = config;
            _reportRepository = reportRepository;
        }

        [HttpGet]
        [Route("GetBDT_01Report")]
        public async Task<IActionResult> GetBDT_01Report(int account, int fromDate, int toDate)
        {
            try
            {
                var response = await _reportRepository.GetDataBDT_01ReportAsync(account, fromDate, toDate);
                return Ok(response);
            }
            catch ( Exception ex )
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
