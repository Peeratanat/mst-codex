using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MST_General.Params.Filters;
using Base.DTOs.MST;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MST_General.Services;
using Database.Models;
using PagingExtensions;
using Base.DTOs;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Net;
using Microsoft.AspNetCore.Authorization;
using Database.Models.DbQueries;
using Common.Helper.HttpResultHelper;
using Base.DTOs.Common;

namespace MST_General.API.Controllers
{
#if !DEBUG
    [Authorize]
#endif
    [Route(HelperStatic.BaseAPIRoute)]
    [ApiController]
    public class MasterPriceItemsController : BaseController
    {
        private IMasterPriceItemService MasterPriceItemService;
        private readonly DatabaseContext DB;
        private readonly ILogger<MasterPriceItemsController> _logger;
        private readonly IHttpResultHelper _httpResultHelper;

        public MasterPriceItemsController(IMasterPriceItemService masterPriceItemService, DatabaseContext db, ILogger<MasterPriceItemsController> logger, IHttpResultHelper httpResultHelper)
        {
            MasterPriceItemService = masterPriceItemService;
            DB = db;
            _logger = logger;
            _httpResultHelper = httpResultHelper;
        }
        /// <summary>
        /// ลิสข้อมูลพื้นฐานทั่วไป Dropdown
        /// </summary>
        /// <param name="detail"></param> 
        /// <returns></returns>
        [HttpGet("DropdownList")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<List<MasterPriceItemDTO>>))]
        public async Task<IActionResult> GetMasterPriceItemDropdownListAsync([FromQuery] string detail, CancellationToken cancellationToken = default)
        {
            var results = await MasterPriceItemService.GetMasterPriceItemDropdownListAsync(detail, cancellationToken);
            return await _httpResultHelper.SuccessCustomResult(results, MasterPriceItemService.logModel);
        }

    }
}