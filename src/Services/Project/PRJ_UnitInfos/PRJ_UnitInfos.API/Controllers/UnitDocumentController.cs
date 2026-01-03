using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Base.DTOs.Common;
using Base.DTOs.SAL;
using Common.Helper.HttpResultHelper;
using Database.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PagingExtensions;
using PRJ_UnitInfos.Params.Filters;
using PRJ_UnitInfos.Services;

namespace PRJ_UnitInfos.API.Controllers
{
#if !DEBUG
    [Authorize]
#endif
    [Route(HelperStatic.BaseAPIRoute)]
    [ApiController]
    public class UnitDocumentController : BaseController
    {
        private IUnitDocumentService UnitDocumentService;
        private readonly IHttpResultHelper _httpResultHelper;

        public UnitDocumentController(IUnitDocumentService unitDocumentService, IHttpResultHelper httpResultHelper)
        {
            this.UnitDocumentService = unitDocumentService;
            _httpResultHelper = httpResultHelper;
        }
        /// <summary>
        /// ดึงรายการสถานะแปลง
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="pageParam"></param>
        /// <param name="sortByParam"></param>
        /// <returns></returns>
        [HttpGet("UnitDocumentList")]
        [PagingResponseHeaders]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<List<UnitDocumentDTO>>))]
        public async Task<IActionResult> GetUnitDocumentListAsync([FromQuery] UnitDocumentFilter filter, [FromQuery] PageParam pageParam, [FromQuery] UnitDocumentSortByParam sortByParam, CancellationToken cancellationToken = default)
        {
            var result = await UnitDocumentService.GetUnitDocumentDropdownListAsync(filter, pageParam, sortByParam, cancellationToken);
            AddPagingResponse(result.PageOutput);
            return await _httpResultHelper.SuccessCustomResult(result.UnitDocuments, UnitDocumentService.logModel);
        }

        /// <summary>
        /// ดึงชื่อลูกค้า จาก BookingID
        /// </summary>
        /// <param name="BookingID"></param>
        /// <returns>DocumentOwnerDTO</returns>
        [HttpGet("GetDocumentOwner")]
        [ProducesResponseType(200, Type = typeof(ResponseModel<DocumentOwnerDTO>))]
        public async Task<IActionResult> GetDocumentOwnerAsync([FromQuery] Guid BookingID)
        {
            var result = await UnitDocumentService.GetDocumentOwnerAsync(BookingID);
            return await _httpResultHelper.SuccessCustomResult(result, UnitDocumentService.logModel);
        }

    }
}
