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
using MST_Event.Params.Filters;
using MST_Event.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PagingExtensions;
using Common.Helper.HttpResultHelper;
using Base.DTOs.Common;


namespace MST_Event.API.Controllers
{
#if !DEBUG
    [Authorize]
#endif
    [Route(HelperStatic.BaseAPIRoute)]
    [ApiController]
    public class EventController : BaseController
    {
        private IEventService EventService;
        private readonly DatabaseContext DB;
        private readonly IHttpResultHelper _httpResultHelper;

        public EventController(IEventService eventService, DatabaseContext db, IHttpResultHelper httpResultHelper)
        {
            EventService = eventService;
            DB = db;
            _httpResultHelper = httpResultHelper;
        }

        /// <summary>
        /// ลิสของข้อมูลนิติบุคคล
        /// </summary>

        /// <returns></returns>
        [HttpGet("GetEventDropownList")]
        [PagingResponseHeaders]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<List<EventDTO>>))]
        public async Task<IActionResult> GetEventDropownList(CancellationToken cancellationToken = default)
        {
            var result = await EventService.GetEventDropownList(cancellationToken);
            return await _httpResultHelper.SuccessCustomResult(result, EventService.logModel);
        }


        /// <returns></returns>
        [HttpGet("GetPaymentOnlineConfig")]
        [PagingResponseHeaders]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<PaymentGatewayConfig>))]
        public async Task<IActionResult> GetPaymentOnlineConfig(Guid ProjectID, CancellationToken cancellationToken = default)
        {
            var result = await EventService.GetPaymentOnlineConfig(ProjectID, cancellationToken);
            return await _httpResultHelper.SuccessCustomResult(result, EventService.logModel);
        }
    }
}