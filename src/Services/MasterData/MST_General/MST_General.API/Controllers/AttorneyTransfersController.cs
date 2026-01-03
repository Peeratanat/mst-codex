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
using Common.Helper.HttpResultHelper;
using Base.DTOs.Common;

namespace MST_General.API.Controllers
{
#if !DEBUG
    [Authorize]
#endif
    [Route(HelperStatic.BaseAPIRoute)]
    [ApiController]
    public class AttorneyTransfersController : BaseController
    {
        private IAttorneyTransferService AttorneyTransferService;
        private readonly DatabaseContext DB;
        private readonly ILogger<AttorneyTransfersController> _logger;
        private readonly IHttpResultHelper _httpResultHelper;

        public AttorneyTransfersController(IAttorneyTransferService AttorneyTransferService, DatabaseContext db, ILogger<AttorneyTransfersController> logger, IHttpResultHelper httpResultHelper)
        {
            this.AttorneyTransferService = AttorneyTransferService;
            this.DB = db;
            _logger = logger;
            _httpResultHelper = httpResultHelper;
        }


        /// <summary>
        /// Dropdown ลิส ข้อมูลผู้รับมอบอำนาจในเอกสารโอน 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpGet("DropdownList")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<List<AttorneyTransferListDTO>>))]
        public async Task<IActionResult> GetAttorneyTransferDropdownListAsync([FromQuery] string name, CancellationToken cancellationToken = default)
        {
            var result = await AttorneyTransferService.GetAttorneyTransferDropdownListAsync(name, cancellationToken);
            return await _httpResultHelper.SuccessCustomResult(result, AttorneyTransferService.logModel);

        }

    }
}