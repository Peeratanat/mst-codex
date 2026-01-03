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
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Net;
using Microsoft.AspNetCore.Authorization;
using Database.Models.DbQueries;
using Database.Models.DbQueries.MST;
using Common.Helper.HttpResultHelper;
using Base.DTOs.Common;
using Base.DTOs;
using Report.Integration;
using System.Web;

namespace MST_General.API.Controllers
{
#if !DEBUG
    [Authorize]
#endif
    [Route(HelperStatic.BaseAPIRoute)]
    [ApiController]
    public class MasterCentersController : BaseController
    {
        private IMasterCenterService MasterCenterService;
        private readonly DatabaseContext DB;

        private readonly ILogger<MasterCentersController> _logger;
        private readonly IHttpResultHelper _httpResultHelper;
        public MasterCentersController(IMasterCenterService masterCenterService, DatabaseContext db, ILogger<MasterCentersController> logger, IHttpResultHelper httpResultHelper)
        {
            this.MasterCenterService = masterCenterService;
            this.DB = db;
            _logger = logger;
            _httpResultHelper = httpResultHelper;
        }
        /// <summary>
        /// ลิสข้อมูลพื้นฐานทั่วไป Dropdown
        /// </summary>
        /// <param name="masterCenterGroupKey"></param>
        /// <param name="name"></param>
        /// <param name="bg"></param>
        /// <param name="brandId"></param>
        /// <returns></returns>
        [HttpGet("DropdownList")]
        [ProducesResponseType(typeof(ResponseModel<List<MasterCenterDropdownDTO>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetMasterCenterDropdownListAsync([FromQuery] string masterCenterGroupKey, [FromQuery] string name, [FromQuery] string bg, [FromQuery] string brandId, CancellationToken cancellationToken = default)
        {
            var result = await MasterCenterService.GetMasterCenterDropdownListAsync(masterCenterGroupKey, name, bg, brandId, cancellationToken);
            return await _httpResultHelper.SuccessCustomResult(result, MasterCenterService.logModel);
        }

        [HttpGet("Find")]
        [ProducesResponseType(typeof(ResponseModel<MasterCenterDropdownDTO>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetFindMasterCenterDropdownItemAsync([FromQuery] string masterCenterGroupKey, [FromQuery] string key, CancellationToken cancellationToken = default)
        {
            var result = await MasterCenterService.GetFindMasterCenterDropdownItemAsync(masterCenterGroupKey, key, cancellationToken);
            return await _httpResultHelper.SuccessCustomResult(result, MasterCenterService.logModel);
        }


        /// <summary>
        /// ลิสข้อมูลพื้นฐานทั่วไป
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="pageParam"></param>
        /// <param name="sortByParam"></param>
        /// <returns></returns>
        [HttpGet]
        [PagingResponseHeaders]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<List<MasterCenterDTO>>))]
        public async Task<IActionResult> GetMasterCenterListAsync([FromQuery] MasterCenterFilter filter, [FromQuery] PageParam pageParam, [FromQuery] MasterCenterSortByParam sortByParam, CancellationToken cancellationToken = default)
        {
            var result = await MasterCenterService.GetMasterCenterListAsync(filter, pageParam, sortByParam, cancellationToken);
            AddPagingResponse(result.PageOutput);
            return await _httpResultHelper.SuccessCustomResult(result.MasterCenters, MasterCenterService.logModel);
        }
        /// <summary>
        /// ข้อมูลพื้นฐานทั่วไป
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<MasterCenterDTO>))]
        public async Task<IActionResult> GetMasterCenterAsync([FromRoute] Guid id, CancellationToken cancellationToken = default)
        {
            var result = await MasterCenterService.GetMasterCenterAsync(id, cancellationToken);
            return await _httpResultHelper.SuccessCustomResult(result, MasterCenterService.logModel);
        }
        /// <summary>
        /// เพิ่มข้อมูลพื้นฐานทั่วไป
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ResponseModel<MasterCenterDTO>))]
        public async Task<IActionResult> CreateMasterCenterAsync([FromBody] MasterCenterDTO input)
        {
            try
            {
                var result = await MasterCenterService.CreateMasterCenterAsync(input);
                return await _httpResultHelper.SuccessCustomResult(result, MasterCenterService.logModel, HttpStatusCode.Created);
            }
            catch (Exception ex)
            {
                _logger.LogError(message: string.Join(" : ", "CreateMasterCenterAsync", ex?.InnerException?.Message ?? ex?.Message));
                throw ex;
            }
        }
        /// <summary>
        /// แก้ไข ข้อมูลพื้นฐานทั่วไป
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<MasterCenterDTO>))]
        public async Task<IActionResult> UpdateMasterCenterAsync([FromRoute] Guid id, [FromBody] MasterCenterDTO input)
        {
            try
            {
                var result = await MasterCenterService.UpdateMasterCenterAsync(id, input);
                return await _httpResultHelper.SuccessCustomResult(result, MasterCenterService.logModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(message: string.Join(" : ", "UpdateMasterCenterAsync", ex?.InnerException?.Message ?? ex?.Message));
                throw ex;
            }
        }
        /// <summary>
        /// ลบข้อมูลพื้นฐานทั่วไป
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMasterCenterAsync([FromRoute] Guid id)
        {
            try
            {
                await MasterCenterService.DeleteMasterCenterAsync(id);
                return await _httpResultHelper.SuccessCustomResult(null, MasterCenterService.logModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(message: string.Join(" : ", "DeleteMasterCenterAsync", ex?.InnerException?.Message ?? ex?.Message));
                throw ex;
            }
        }



        /// <summary>
        /// BankBranchBOT Dropdown
        /// </summary>
        /// <param name="bankCode"></param>
        /// <param name="bankBranchName"></param>
        /// <returns></returns>
        [HttpGet("BankBranchBOTDropdownList")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<List<BankBranchBOTDropdownDTO>>))]
        public async Task<IActionResult> GetBankBranchDropdownAsync([FromQuery] string bankCode, [FromQuery] string bankBranchName, CancellationToken cancellationToken = default)
        {
            var results = await MasterCenterService.GetBankBranchDropdownAsync(bankCode, bankBranchName, cancellationToken);
            return await _httpResultHelper.SuccessCustomResult(results, MasterCenterService.logModel);
        }


        /// <summary>
        /// ลิสข้อมูลพื้นฐานทั่วไป Dropdown
        /// </summary>
        /// <param name="masterCenterGroupKey"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpGet("LGDropdownList")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<List<MasterCenterDropdownDTO>>))]
        public async Task<IActionResult> GetLGMasterCenterDropdownListAsync([FromQuery] string masterCenterGroupKey, [FromQuery] string name, [FromQuery] decimal countNumber, CancellationToken cancellationToken = default)
        {
            var results = await MasterCenterService.GetLGMasterCenterDropdownListAsync(masterCenterGroupKey, name, countNumber, cancellationToken);
            return await _httpResultHelper.SuccessCustomResult(results, MasterCenterService.logModel);
        }
        [HttpPost("DecodeParamReport")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        public async Task<IActionResult> DecodeParamReport([FromBody] BaseDTO param)
        {
            string token = HttpUtility.UrlDecode(param.UpdatedBy);
            var deToken = Encrypt.DecryptString(token, Environment.GetEnvironmentVariable("report_SecretKey"));
            return await _httpResultHelper.SuccessCustomResult(deToken, MasterCenterService.logModel);
        }
    }
}