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
    public class CategoryController : ControllerBase
    {
        private IConfiguration _config;
        private ICategoryRepository _categoryRepository = null;

        public CategoryController( IConfiguration config, ICategoryRepository categoryRepository )
        {
            _config = config;
            _categoryRepository = categoryRepository;
        }

        [HttpGet]
        [Route("GetCategoryAccount")]
        public async Task<IActionResult> GetCategoryAccount( int pageIndex, int pageSize, int account )
        {
            try
            {
                var response = await _categoryRepository.GetCategoryAccountAsync(pageIndex, pageSize, account);
                return Ok(response);
            }
            catch ( Exception ex )
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        [Route("GetCategoryGiaVonChuan")]
        public async Task<IActionResult> GetCategoryGiaVonChuan( int pageIndex, int pageSize, int account )
        {
            try
            {
                var response = await _categoryRepository.GetCategoryGiaVonChuanAsync(pageIndex, pageSize, account);
                return Ok(response);
            }
            catch ( Exception ex )
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        [Route("GetCategoryGiaVonChuanNT")]
        public async Task<IActionResult> GetCategoryGiaVonChuanNT( int pageIndex, int pageSize, int account )
        {
            try
            {
                var response = await _categoryRepository.GetCategoryGiaVonChuanNTAsync(pageIndex, pageSize, account);
                return Ok(response);
            }
            catch ( Exception ex )
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        [Route("GetCategoryPostOffice")]
        public async Task<IActionResult> GetCategoryPostOffice( int pageIndex, int pageSize, int account )
        {
            try
            {
                var response = await _categoryRepository.GetCategoryPostOfficeAsync(pageIndex, pageSize, account);
                return Ok(response);
            }
            catch ( Exception ex )
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

    }
}
