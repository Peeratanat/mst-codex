using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Base.DTOs;
using Base.DTOs.MST;
using Database.Models;
using MST_Finacc.Params.Filters;
using MST_Finacc.Params.Outputs;
using MST_Finacc.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PagingExtensions;
using Common.Helper.HttpResultHelper;
using Base.DTOs.Common;
using Report.Integration;
using System.Net;

namespace MST_Finacc.API.Controllers
{
#if !DEBUG
    [Authorize]
#endif
    [Route(HelperStatic.BaseAPIRoute)]
    [ApiController]
    public class EDCsController : BaseController
    {
        private readonly IEDCService EDCService;
        private readonly ILogger<BankBranchsController> Logger;
        private readonly IHttpResultHelper _httpResultHelper;
        private readonly DatabaseContext DB;

        public EDCsController(IEDCService edcService, ILogger<BankBranchsController> logger, DatabaseContext db, IHttpResultHelper httpResultHelper)
        {
            this.EDCService = edcService;
            this.Logger = logger;
            this.DB = db;
            _httpResultHelper = httpResultHelper;
        }

        /// <summary>
        /// ดึงข้อมูลเครื่องรูดบัตรแบบ dropdown
        /// </summary>
        /// <param name="projectID"></param>
        /// <param name="bankName"></param>
        /// <returns></returns>
        [HttpGet("DropdownList")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<List<EDCDropdownDTO>>))]
        public async Task<IActionResult> GetEDCDropdownList([FromQuery] Guid? projectID = null, [FromQuery] string bankName = null)
        {
            var results = await EDCService.GetEDCDropdownListUrlAsync(projectID, bankName);
            return await _httpResultHelper.SuccessCustomResult(results, EDCService.logModel);
        }

        /// <summary>
        /// ดึงข้อมูลเครื่องรูดบัตรแบบ dropdown
        /// </summary>
        /// <param name="projectID"></param>
        /// <param name="bankName"></param>
        /// <returns></returns>
        [HttpGet("EDCBankDropdownList")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<List<BankDropdownDTO>>))]
        public async Task<IActionResult> GetEDCBankDropdownList([FromQuery] Guid? projectID = null, [FromQuery] string bankName = null, [FromQuery] bool? IsWrongProject = null)
        {

            var results = await EDCService.GetEDCBankDropdownListAsync(projectID, bankName, IsWrongProject);
            return await _httpResultHelper.SuccessCustomResult(results, EDCService.logModel);
        }

        /// <summary>
        /// ดึงข้อมูลเครื่องรูดบัตร
        /// </summary>
        /// <returns>The EDCL ist.</returns>
        /// <param name="filter">Filter.</param>
        /// <param name="pageParam">Page parameter.</param>
        /// <param name="sortByParam">Sort by parameter.</param>
        [HttpGet]
        [PagingResponseHeaders]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<List<EDCDTO>>))]
        public async Task<IActionResult> GetEDCList([FromQuery] EDCFilter filter,
            [FromQuery] PageParam pageParam,
            [FromQuery] EDCSortByParam sortByParam)
        {
            var result = await EDCService.GetEDCListAsync(filter, pageParam, sortByParam);
            AddPagingResponse(result.PageOutput);
            return await _httpResultHelper.SuccessCustomResult(result.EDCs, EDCService.logModel);
        }

        /// <summary>
        /// ดึงรายละเอียดเครื่องรูดบัตร
        /// </summary>
        /// <returns>The EDCD etail.</returns>
        /// <param name="id">Identifier.</param>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<List<EDCDTO>>))]
        public async Task<IActionResult> GetEDCDetail([FromRoute] Guid id)
        {

            var result = await EDCService.GetEDCDetailAsync(id);
            return await _httpResultHelper.SuccessCustomResult(result, EDCService.logModel);
        }

        /// <summary>
        /// สร้างเครื่องรูดบัตร
        /// </summary>
        /// <returns>The edc.</returns>
        /// <param name="input">Input.</param>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ResponseModel<EDCDTO>))]
        public async Task<IActionResult> CreateEDCAsync([FromBody] EDCDTO input)
        {
            using (var tran = await DB.Database.BeginTransactionAsync())
            {
                try
                {
                    var result = await EDCService.CreateEDCAsync(input);
                    await tran.CommitAsync();
                    return await _httpResultHelper.SuccessCustomResult(result, EDCService.logModel, HttpStatusCode.Created);
                }
                catch (Exception ex)
                {
                    Logger.LogError(message: string.Join(" : ", "CreateEDCAsync", ex?.InnerException?.Message ?? ex?.Message));
                    await tran.RollbackAsync();
                    throw ex;
                }
            }
        }

        /// <summary>
        /// แก้ไขเครื่องรูดบัตร
        /// </summary>
        /// <returns>The edc.</returns>
        /// <param name="id">Identifier.</param>
        /// <param name="input">Input.</param>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<EDCDTO>))]
        public async Task<IActionResult> UpdateEDCAsync([FromRoute] Guid id, [FromBody] EDCDTO input)
        {
            using (var tran = await DB.Database.BeginTransactionAsync())
            {
                try
                {
                    var result = await EDCService.UpdateEDCAsync(id, input);
                    await tran.CommitAsync();
                    return await _httpResultHelper.SuccessCustomResult(result, EDCService.logModel);
                }
                catch (Exception ex)
                {
                    Logger.LogError(message: string.Join(" : ", "UpdateEDCAsync", ex?.InnerException?.Message ?? ex?.Message));
                    await tran.RollbackAsync();
                    throw ex;
                }
            }
        }

        /// <summary>
        /// ลบเครื่องรูดบัตร
        /// </summary>
        /// <returns>The edc.</returns>
        /// <param name="id">Identifier.</param>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEDCAsync([FromRoute] Guid id)
        {
            using (var tran = await DB.Database.BeginTransactionAsync())
            {
                try
                {
                    await EDCService.DeleteEDCAsync(id);
                    await tran.CommitAsync();
                    return Ok();
                }
                catch (Exception ex)
                {
                    Logger.LogError(message: string.Join(" : ", "DeleteEDCAsync", ex?.InnerException?.Message ?? ex?.Message));
                    await tran.RollbackAsync();
                    throw ex;
                }
            }
        }

        /// <summary>
        /// ดึงธนาคารเครื่องรูดบัตร
        /// </summary>
        /// <returns>The EDCB ank list.</returns>
        /// <param name="filter">Filter.</param>
        /// <param name="pageParam">Page parameter.</param>
        /// <param name="sortByParam">Sort by parameter.</param>
        [HttpGet("Banks")]
        [PagingResponseHeaders]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<List<EDCBankDTO>>))]
        public async Task<IActionResult> GetEDCBankList([FromQuery] EDCBankFilter filter,
            [FromQuery] PageParam pageParam,
            [FromQuery] EDCBankSortByParam sortByParam)
        {

            var result = await EDCService.GetEDCBankListAsync(filter, pageParam, sortByParam);
            AddPagingResponse(result.PageOutput);
            return await _httpResultHelper.SuccessCustomResult(result.EDCBanks, EDCService.logModel);
        }

        /// <summary>
        /// ดึงค่าธรรมเนียมบัตร
        /// </summary>
        /// <returns>The EDCF ee list.</returns>
        /// <param name="filter">Filter.</param>
        /// <param name="pageParam">Page parameter.</param>
        /// <param name="sortByParam">Sort by parameter.</param>
        [HttpGet("Fees")]
        [PagingResponseHeaders]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<List<EDCFeeDTO>>))]
        public async Task<IActionResult> GetEDCFeeList([FromQuery] EDCFeeFilter filter,
            [FromQuery] PageParam pageParam,
            [FromQuery] EDCFeeSortByParam sortByParam)
        {

            var result = await EDCService.GetEDCFeeListAsync(filter, pageParam, sortByParam);

            AddPagingResponse(result.PageOutput);

            return await _httpResultHelper.SuccessCustomResult(result.EDCFees, EDCService.logModel);

        }

        /// <summary>
        /// สร้างค่าธรรมเนียมบัตร
        /// </summary>
        /// <returns>The EDCF ee.</returns>
        /// <param name="input">Input.</param>
        [HttpPost("Fees")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ResponseModel<EDCFeeDTO>))]
        public async Task<IActionResult> CreateEDCFeeAsync([FromBody] EDCFeeDTO input)
        {
            using (var tran = await DB.Database.BeginTransactionAsync())
            {
                try
                {
                    var result = await EDCService.CreateEDCFeeAsync(input);
                    await tran.CommitAsync();
                    return await _httpResultHelper.SuccessCustomResult(result, EDCService.logModel, HttpStatusCode.Created);
                }
                catch (Exception ex)
                {
                    Logger.LogError(message: string.Join(" : ", "CreateEDCFeeAsync", ex?.InnerException?.Message ?? ex?.Message));
                    await tran.RollbackAsync();
                    throw ex;
                }
            }
        }

        /// <summary>
        /// แก้ไขค่าธรรมเนียมบัตร
        /// </summary>
        /// <returns>The EDCF ee.</returns>
        /// <param name="id">Identifier.</param>
        /// <param name="input">Input.</param>
        [HttpPut("Fees/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<EDCFeeDTO>))]
        public async Task<IActionResult> UpdateEDCFeeAsync([FromRoute] Guid id, [FromBody] EDCFeeDTO input)
        {
            using (var tran = await DB.Database.BeginTransactionAsync())
            {
                try
                {
                    var result = await EDCService.UpdateEDCFeeAsync(id, input);
                    await tran.CommitAsync();
                    return await _httpResultHelper.SuccessCustomResult(result, EDCService.logModel);
                }
                catch (Exception ex)
                {
                    await tran.RollbackAsync();
                    Logger.LogError(message: string.Join(" : ", "UpdateEDCFeeAsync", ex?.InnerException?.Message ?? ex?.Message));
                    throw ex;
                }
            }
        }

        /// <summary>
        /// ลบค่าธรรมเนียมบัตร
        /// </summary>
        /// <returns>The EDCF ee.</returns>
        /// <param name="id">Identifier.</param>
        [HttpDelete("Fees/{id}")]
        public async Task<IActionResult> DeleteEDCFeeAsync([FromRoute] Guid id)
        {
            using (var tran = await DB.Database.BeginTransactionAsync())
            {
                try
                {
                    await EDCService.DeleteEDCFeeAsync(id);
                    await tran.CommitAsync();
                    return Ok();
                }
                catch (Exception ex)
                {
                    Logger.LogError(message: string.Join(" : ", "DeleteEDCFeeAsync", ex?.InnerException?.Message ?? ex?.Message));
                    await tran.RollbackAsync();
                    throw ex;
                }
            }
        }

        /// <summary>
        /// คำนวณค่าธรรมเนียม
        /// </summary>
        /// <param name="bankID"></param>
        /// <param name="creditCardTypeMasterCenterID"></param>
        /// <param name="creditCardPaymentTypeMasterCenterID"></param>
        /// <param name="paymentCardTypeMasterCenterID"></param>
        /// <param name="payAmount"></param>
        /// <returns></returns>
        [HttpGet("{id}/Fees/Calculate")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<DecimalResult>))]
        public async Task<IActionResult> GetFee([FromRoute] Guid id, [FromQuery] Guid bankID, [FromQuery] Guid creditCardTypeMasterCenterID, [FromQuery] Guid creditCardPaymentTypeMasterCenterID, [FromQuery] Guid paymentCardTypeMasterCenterID, [FromQuery] decimal payAmount)
        {
            var result = await EDCService.GetFeeAsync(id, bankID, creditCardTypeMasterCenterID, creditCardPaymentTypeMasterCenterID, paymentCardTypeMasterCenterID, payAmount);
            return await _httpResultHelper.SuccessCustomResult(result, EDCService.logModel);
        }

        /// <summary>
        /// คำนวณค่าธรรมเนียม
        /// </summary>
        /// <param name="bankID"></param>
        /// <param name="creditCardTypeMasterCenterID"></param>
        /// <param name="creditCardPaymentTypeMasterCenterID"></param>
        /// <param name="paymentCardTypeMasterCenterID"></param>
        /// <returns></returns>
        [HttpGet("{id}/Fees/Percent")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<DecimalResult>))]
        public async Task<IActionResult> GetFeePercent([FromRoute] Guid id, [FromQuery] Guid bankID, [FromQuery] Guid creditCardTypeMasterCenterID, [FromQuery] Guid creditCardPaymentTypeMasterCenterID, [FromQuery] Guid paymentCardTypeMasterCenterID)
        {
            var result = await EDCService.GetFeePercentAsync(id, bankID, creditCardTypeMasterCenterID, creditCardPaymentTypeMasterCenterID, paymentCardTypeMasterCenterID);
            return await _httpResultHelper.SuccessCustomResult(result, EDCService.logModel);
        }

        /// <summary>
        /// ดึง url สำหรับ export ตารางเครื่องรูดบัตร
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="downloadAs">0=excel, 1=showpdf</param>
        /// <returns></returns>
        [HttpGet("ExportEDCListUrl")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<ReportResult>))]
        public async Task<IActionResult> ExportEDCListUrl([FromQuery] EDCFilter filter, [FromQuery] ShowAs downloadAs)
        {
            var result = await EDCService.ExportEDCListUrlAsync(filter, downloadAs);
            return await _httpResultHelper.SuccessCustomResult(result, EDCService.logModel);
        }

        /// <summary>
        /// สำหรับ export ตารางเครื่องรูดบัตร
        /// </summary>
        /// <param name="filter"></param> 
        /// <returns></returns>
        [HttpGet("ExportEDC")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<FileDTO>))]
        public async Task<IActionResult> ExportEDC([FromQuery] EDCFilter filter)
        {
            var result = await EDCService.ExportExcelEDCAsync(filter);
            return await _httpResultHelper.SuccessCustomResult(result, EDCService.logModel);
        }

        [HttpGet("EDCCreditCardPaymentTypeDropdownList")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<List<MasterCenterDropdownDTO>>))]
        public async Task<IActionResult> GetEDCCreditCardPaymentTypeDropdownList([FromQuery] Guid? BankId = null)
        {
            var results = await EDCService.GetEDCCreditCardPaymentTypeDropdownListAsync(BankId);
            return await _httpResultHelper.SuccessCustomResult(results, EDCService.logModel);
        }

    }
}
