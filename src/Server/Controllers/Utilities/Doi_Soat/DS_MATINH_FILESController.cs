using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;
using System;
using Microsoft.Extensions.Configuration;
using static Org.BouncyCastle.Math.EC.ECCurve;
using NewBalance.Infrastructure.OR.IRepository;
using System.Collections.Generic;
using NewBalance.Application.Features.Doi_Soat;

namespace NewBalance.Server.Controllers.Utilities.Doi_Soat
{
    [Route("api/[controller]")]
    [ApiController]
    public class DS_MATINH_FILESController : ControllerBase
    {
        private IConfiguration _config;
        private IDS_MATINH_FILESRepository _oDS_MATINH_FILESRepository = null;
        public DS_MATINH_FILESController(IConfiguration configuration, IDS_MATINH_FILESRepository DS_MATINH_FILESRepository)
        {
            _config = configuration;
            _oDS_MATINH_FILESRepository = DS_MATINH_FILESRepository;
        }

        [HttpGet]
        [Route("getlist")]
        public async Task<IActionResult> GetDS_MATINH_FILES(string pageIndex, string pageSize, int ma_tinh, int tu_ngay, int den_ngay)
        {
            try
            {
                var response = await _oDS_MATINH_FILESRepository.GetAllDS_MATINH_FILESResponse(pageIndex, pageSize, ma_tinh, tu_ngay, den_ngay);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        [HttpPost]
        [Route("modify_status")]
        public async Task<IActionResult> DS_MATINH_FILES_MODIFY_STATUS(ResponseData<int> model)
        {
            try
            {
                var _list = model.data;
                var response = new ResponseData<int>();
                string notes = "File đang tạo vui lòng chờ";
                foreach (var item in _list)
                {
                    response = await _oDS_MATINH_FILESRepository.DS_MATINH_FILES_MODIFY_STATUS(item, model.message, notes);
                }    
                //response = await _oDS_MATINH_FILESRepository.DS_MATINH_FILES_MODIFY_STATUS(id);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
