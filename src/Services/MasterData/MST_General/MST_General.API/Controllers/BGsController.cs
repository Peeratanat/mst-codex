

using Base.DTOs.MST;
using Common.Helper.HttpResultHelper;
using Database.Models;
using MST_General.Params.Filters;
using Microsoft.AspNetCore.Mvc;
using MST_General.Services;
using PagingExtensions;
using Microsoft.AspNetCore.Authorization;
using System.Net;
using Base.DTOs.Common;

namespace MST_General.API.Controllers
{


#if !DEBUG
    [Authorize]
#endif
    [Route(HelperStatic.BaseAPIRoute)]
    [ApiController]
    public class BGsController : BaseController
    {
        private readonly DatabaseContext DB;
        private readonly ILogger<BGsController> _logger;
        private readonly IHttpResultHelper _httpResultHelper;
        private IBGService BGService;

        public BGsController(IBGService bGService, DatabaseContext db, IHttpResultHelper httpResultHelper, ILogger<BGsController> logger)
        {
            this.BGService = bGService;
            this.DB = db;
            _httpResultHelper = httpResultHelper;
            _logger = logger;
        }
        /// <summary>
        /// Get BG Dropdown List
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpGet("DropdownList")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<List<BGDropdownDTO>>))]
        public async Task<IActionResult> GetBGDropdownListAsync([FromQuery] string? productTypeKey = null, [FromQuery] string? name = null
               , CancellationToken cancellationToken = default)
        {
            var result = await BGService.GetBGDropdownListAsync(productTypeKey, name, cancellationToken);
            return await _httpResultHelper.SuccessCustomResult(result, BGService.logModel);
        }

        /// <summary>
        /// Get BG List
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="pageParam"></param>
        /// <param name="sortByParam"></param>
        /// <returns></returns>
        [HttpGet]
        [PagingResponseHeaders]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<List<BGDTO>>))]
        public async Task<IActionResult> GetBGListAsync([FromQuery] BGFilter filter, [FromQuery] PageParam pageParam, [FromQuery] BGSortByParam sortByParam, CancellationToken cancellationToken = default)
        {
            var result = await BGService.GetBGListAsync(filter, pageParam, sortByParam, cancellationToken);
            AddPagingResponse(result.PageOutput);
            return await _httpResultHelper.SuccessCustomResult(result, BGService.logModel);
        }
        /// <summary>
        /// Get BG
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<BGDTO>))]
        public async Task<IActionResult> GetBGAsync([FromRoute] Guid id, CancellationToken cancellationToken = default)
        {
            var result = await BGService.GetBGAsync(id, cancellationToken);
            return await _httpResultHelper.SuccessCustomResult(result, BGService.logModel);
        }
        /// <summary>
        /// Create BG
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ResponseModel<BGDTO>))]
        public async Task<IActionResult> CreateBGAsync([FromBody] BGDTO input)
        {
            var result = await BGService.CreateBGAsync(input);
            return await _httpResultHelper.SuccessCustomResult(result, BGService.logModel, HttpStatusCode.Created);
        }
        /// <summary>
        /// Edit BG
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<BGDTO>))]
        public async Task<IActionResult> UpdateBGAsync([FromRoute] Guid id, [FromBody] BGDTO input)
        {
            var result = await BGService.UpdateBGAsync(id, input);
            return await _httpResultHelper.SuccessCustomResult(result, BGService.logModel);
        }
        /// <summary>
        /// Delete BG
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBGAsync([FromRoute] Guid id)
        {
            using var tran = await DB.Database.BeginTransactionAsync();
            try
            {
                await BGService.DeleteBGAsync(id);
                await tran.CommitAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                await tran.RollbackAsync();
                _logger.LogError(message: string.Join(" : ", "DeleteBG", ex?.InnerException?.Message ?? ex?.Message));
                throw ex;
            }
        }

    }
}