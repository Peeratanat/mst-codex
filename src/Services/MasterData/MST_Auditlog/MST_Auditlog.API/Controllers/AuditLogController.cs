using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Base.DTOs;
using Base.DTOs.MST;
using Base.DTOs.SAL;
using Base.DTOs.SAL.Sortings;
using Database.Models;
using Database.Models.MST;
using MST_Auditlog.Params.Filters;
using MST_Auditlog.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PagingExtensions;
using Common.Helper.HttpResultHelper;
using Base.DTOs.Common;
using Base.DTOs.CTM;
using MST_Auditlog.Services.ContactServices;


namespace MST_Auditlog.API.Controllers
{
#if !DEBUG
    [Authorize]
#endif
    [Route(HelperStatic.BaseAPIRoute)]
    [ApiController]
    public class AuditLogController : BaseController
    {
        private IAudtiLogService AudtiLogService;
        private readonly IHttpResultHelper _httpResultHelper;

        public AuditLogController(IAudtiLogService audtiLogService, IHttpResultHelper httpResultHelper)
        {
            AudtiLogService = audtiLogService;
            _httpResultHelper = httpResultHelper;
        }


        /// <summary>
        /// ContactAuditList
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="pageParam"></param>
        /// <param name="sortByParam"></param>
        /// <returns>ContactListDTO</returns>
        [HttpGet("ContactAuditList")]
        [PagingResponseHeaders]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<ContactAuditDTO>))]
        public async Task<IActionResult> GetContactAuditAsync([FromQuery] ContactAuditFilter filter, [FromQuery] PageParam pageParam, [FromQuery] ContactAuditSortByParam sortByParam, CancellationToken cancellationToken = default)
        {
            Guid? userID = null;
            Guid parsedUserID;
            if (Guid.TryParse(HttpContext?.User?.Claims.SingleOrDefault(x => x.Type == "userid")?.Value, out parsedUserID))
            {
                userID = parsedUserID;
            }
            var result = await AudtiLogService.GetContactAuditAsync(filter, pageParam, sortByParam, userID, cancellationToken);
            AddPagingResponse(result.PageOutput);
            return await _httpResultHelper.SuccessCustomResult(result.ContactAudit, AudtiLogService.logModel);
        }

        /// <summary>
        /// ContactChangeLog
        /// </summary>
        /// <param name="ContactID"></param>
        /// <param name="pageParam"></param>
        /// <param name="sortByParam"></param>
        /// <returns>ContactChangeLogDTO</returns>
        [HttpGet("ContactChangeLog")]
        [PagingResponseHeaders]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<ContactChangeLogDTO>))]
        public async Task<IActionResult> GetContactChangeLogAsync([FromQuery] Guid ContactID, [FromQuery] PageParam pageParam, [FromQuery] ContactChangeLogSortByParam sortByParam, CancellationToken cancellationToken = default)
        {
            Guid? userID = null;
            Guid parsedUserID;
            if (Guid.TryParse(HttpContext?.User?.Claims.SingleOrDefault(x => x.Type == "userid")?.Value, out parsedUserID))
            {
                userID = parsedUserID;
            }
            var result = await AudtiLogService.GetContactChangeLogAsync(ContactID, pageParam, sortByParam, cancellationToken);
            AddPagingResponse(result.PageOutput);
            return await _httpResultHelper.SuccessCustomResult(result.ContactChangeLog, AudtiLogService.logModel);
        }
    }
}