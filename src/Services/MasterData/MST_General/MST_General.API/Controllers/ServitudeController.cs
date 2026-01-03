using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Base.DTOs;
using Base.DTOs.MST;
using Database.Models;
using MST_General.Params.Filters;
using MST_General.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PagingExtensions;
using MST_General.API;
using Common.Helper.HttpResultHelper;
using Base.DTOs.Common;

namespace MST_General.API.Controllers
{
#if !DEBUG
    [Authorize]
#endif
    [Route(HelperStatic.BaseAPIRoute)]
    [ApiController]
    public class ServitudeController : BaseController
    {
        private IServitudeService ServitudeService;
        private readonly DatabaseContext DB;
        private readonly ILogger<ServitudeController> _logger;
        private readonly IHttpResultHelper _httpResultHelper;

        public ServitudeController(IServitudeService servitudeService, DatabaseContext db, ILogger<ServitudeController> logger, IHttpResultHelper httpResultHelper)
        {
            ServitudeService = servitudeService;
            DB = db;
            _logger = logger;
            _httpResultHelper = httpResultHelper;
        }

        /// <summary>
        /// ลิสของข้อมูลนิติบุคคล
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="pageParam"></param>
        /// <param name="sortByParam"></param>
        /// <returns></returns>
        [HttpGet("GetServitudeList")]
        [PagingResponseHeaders]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<List<ServitudeDTO>>))]
        public async Task<IActionResult> GetServitudeListAsync([FromQuery] ServitudeFilter filter, [FromQuery] PageParam pageParam, [FromQuery] ServitudeSortByParam sortByParam, CancellationToken cancellationToken = default)
        {
            var result = await ServitudeService.GetServitudeListAsync(filter, pageParam, sortByParam, cancellationToken);
            AddPagingResponse(result.PageOutput);
            return await _httpResultHelper.SuccessCustomResult(result.Servitude, ServitudeService.logModel);
        }

        /// <summary>
        /// เพิ่มหนังสือสัญญา
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("AddServitude")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<ServitudeDTO>))]
        public async Task<IActionResult> AddServitudeAsync([FromBody] ServitudeDTO input)
        {
            using var tran = await DB.Database.BeginTransactionAsync();
            try
            {
                var result = await ServitudeService.AddServitudeAsync(input);
                await tran.CommitAsync();
                return await _httpResultHelper.SuccessCustomResult(result, ServitudeService.logModel);
            }
            catch (Exception ex)
            {
                await tran.RollbackAsync();
                _logger.LogError(message: string.Join(" : ", "AddServitudeAsync", ex?.InnerException?.Message ?? ex?.Message));
                throw ex;
            }

        }

        /// <summary>
        /// เพิ่มหนังสือสัญญา
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("EditServitude")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<ServitudeDTO>))]
        public async Task<IActionResult> EditServitudeAsync([FromBody] ServitudeDTO input)
        {
            using var tran = await DB.Database.BeginTransactionAsync();
            try
            {
                var result = await ServitudeService.EditServitudeAsync(input);
                await tran.CommitAsync();
                return await _httpResultHelper.SuccessCustomResult(result, ServitudeService.logModel);
            }
            catch (Exception ex)
            {
                await tran.RollbackAsync();
                _logger.LogError(message: string.Join(" : ", "EditServitudeAsync", ex?.InnerException?.Message ?? ex?.Message));
                throw ex;
            }
        }

        /// <summary>
        /// เพิ่มหนังสือสัญญา
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("DeleteServitude")]
        public async Task<IActionResult> DeleteServitudeAsync([FromBody] ServitudeDTO input)
        {
            try
            {
                await ServitudeService.DeleteServitudeAsync(input);
                return await _httpResultHelper.SuccessCustomResult(null, ServitudeService.logModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(message: string.Join(" : ", "DeleteServitudeAsync", ex?.InnerException?.Message ?? ex?.Message));
                throw ex;
            }
        }
    }
}