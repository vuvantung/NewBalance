using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;
using System;
using Microsoft.Extensions.Configuration;
using NewBalance.Infrastructure.OR.IRepository;
using System.Collections.Generic;
using NewBalance.Application.Features.Doi_Soat;
using Microsoft.AspNetCore.Http;
using System.IO;
using NewBalance.Infrastructure.OR.Repository;
using NewBalance.Application.Requests.Doi_soat;
namespace NewBalance.Server.Controllers.Utilities.Doi_Soat
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrackingController : Controller
    {
        private IConfiguration _config;
        private ITrackingRepository _trackingRepository = null;

        public TrackingController( IConfiguration config, ITrackingRepository trackingRepository )
        {
            _config = config;
            _trackingRepository = trackingRepository;
        }

        [HttpGet]
        [Route("TrackingItem")]
        public async Task<IActionResult> TrackingItem( string ItemCode )
        {
            try
            {
                var response = await _trackingRepository.TrackingItem(ItemCode);
                return Ok(response);
            }
            catch ( Exception ex )
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        [RequestSizeLimit(52428800)]
        [Route("TrackingSLL")]
        public async Task<IActionResult> TrackingSLL( TrackingSLLRequest request  )
        {
            try
            {
                var response = await _trackingRepository.TrackingSLL(request);
                return Ok(response);
            }
            catch ( Exception ex )
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
