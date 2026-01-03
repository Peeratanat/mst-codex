using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Base.DTOs;
using Base.DTOs.MST;
using Database.Models;
using MST_Finacc.Params.Filters;
using MST_Finacc.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PagingExtensions;
using Common.Helper.HttpResultHelper;
using Base.DTOs.Common;

namespace MST_Finacc.API.Controllers
{
#if !DEBUG
    [Authorize]
#endif
    [Route(HelperStatic.BaseAPIRoute)]
    [ApiController]
    public class BankAccountsController : BaseController
    {
        private readonly IBankAccountService BankAccountService;
        private readonly ILogger<BankBranchsController> Logger;
        private readonly DatabaseContext DB;
        private readonly IHttpResultHelper _httpResultHelper;

        public BankAccountsController(IBankAccountService bankAccountService, ILogger<BankBranchsController> logger, DatabaseContext db, IHttpResultHelper httpResultHelper)
        {
            BankAccountService = bankAccountService;
            Logger = logger;
            DB = db;
            _httpResultHelper = httpResultHelper;
        }

        [HttpGet("DropdownList")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<List<BankAccountDropdownDTO>>))]
        public async Task<IActionResult> GetBankAccountDropdownListAsync([FromQuery] string displayName, [FromQuery] string bankAccountTypeKey, [FromQuery] Guid? companyID, [FromQuery] bool? IsWrongAccount, [FromQuery] string paymentMethodTypeKey, [FromQuery] bool? IsActive, [FromQuery] BankAccountDropdownSortByParam sortByParam, CancellationToken cancellationToken = default)
        {
            var results = await BankAccountService.GetBankAccountDropdownListAsync(displayName, bankAccountTypeKey, companyID, IsWrongAccount, paymentMethodTypeKey, IsActive, sortByParam, cancellationToken);
            return await _httpResultHelper.SuccessCustomResult(results, BankAccountService.logModel);
        }

        /// <summary>
        /// ดึงข้อมูลบัญชีธนาคาร
        /// </summary>
        /// <returns>The bank account list.</returns>
        /// <param name="filter">Filter.</param>
        /// <param name="pageParam">Page parameter.</param>
        /// <param name="sortByParam">Sort by parameter.</param>
        [HttpGet]
        [PagingResponseHeaders]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<List<BankAccountDTO>>))]
        public async Task<IActionResult> GetBankAccountListAsync([FromQuery] BankAccountFilter filter, [FromQuery] PageParam pageParam, [FromQuery] BankAccountSortByParam sortByParam, CancellationToken cancellationToken = default)
        {
            var result = await BankAccountService.GetBankAccountListAsync(filter, pageParam, sortByParam, cancellationToken);
            AddPagingResponse(result.PageOutput);
            return await _httpResultHelper.SuccessCustomResult(result.BankAccounts, BankAccountService.logModel);
        }

        /// <summary>
        /// ดึงรายละเอียดบัญชีธนาคาร
        /// </summary>
        /// <returns>The bank account detail.</returns>
        /// <param name="id">Identifier.</param>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<BankAccountDTO>))]
        public async Task<IActionResult> GetBankAccountDetailAsync([FromRoute] Guid id, CancellationToken cancellationToken = default)
        {
            var result = await BankAccountService.GetBankAccountDetailAsync(id, cancellationToken);
            return await _httpResultHelper.SuccessCustomResult(result, BankAccountService.logModel);
        }

        /// <summary>
        /// สร้างบัญชีธนาคาร
        /// </summary>
        /// <returns>The bank account.</returns>
        /// <param name="input">Input.</param>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ResponseModel<BankAccountDTO>))]
        public async Task<IActionResult> CreateBankAccountAsync([FromBody] BankAccountDTO input)
        {
            using (var tran = await DB.Database.BeginTransactionAsync())
            {
                try
                {
                    var result = await BankAccountService.CreateBankAccountAsync(input);
                    await tran.CommitAsync();
                    return await _httpResultHelper.SuccessCustomResult(result, BankAccountService.logModel, HttpStatusCode.Created);
                }
                catch (Exception ex)
                {
                    Logger.LogError(message: string.Join(" : ", "CreateBankAccountAsync", ex?.InnerException?.Message ?? ex?.Message));
                    await tran.RollbackAsync();
                    throw ex;
                }
            }
        }

        /// <summary>
        /// สร้าง ข้อมูลคู่บัญชี
        /// </summary>
        /// <returns>The bank account.</returns>
        /// <param name="input">Input.</param>
        [HttpPost("ChartOfAccount")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ResponseModel<BankAccountDTO>))]
        public async Task<IActionResult> CreateChartOfAccountAsync([FromBody] BankAccountDTO input)
        {
            using (var tran = await DB.Database.BeginTransactionAsync())
            {
                try
                {
                    var result = await BankAccountService.CreateChartOfAccountAsync(input);
                    await tran.CommitAsync();
                    return await _httpResultHelper.SuccessCustomResult(result, BankAccountService.logModel,HttpStatusCode.Created);
                }
                catch (Exception ex)
                {
                    Logger.LogError(message: string.Join(" : ", "CreateChartOfAccountAsync", ex?.InnerException?.Message ?? ex?.Message));
                    await tran.RollbackAsync();
                    throw ex;
                }
            }
        }

        /// <summary>
        /// แก้ไขบัญชีธนาคาร
        /// </summary>
        /// <returns>The bank account.</returns>
        /// <param name="id">Identifier.</param>
        /// <param name="input">Input.</param>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<BankAccountDTO>))]
        public async Task<IActionResult> UpdateBankAccountAsync([FromRoute] Guid id, [FromBody] BankAccountDTO input)
        {
            using (var tran = await DB.Database.BeginTransactionAsync())
            {
                try
                {
                    input.Id = id;
                    var result = await BankAccountService.UpdateBankAccountAsync(id, input);
                    await tran.CommitAsync();
                    return await _httpResultHelper.SuccessCustomResult(result, BankAccountService.logModel);
                }
                catch (Exception ex)
                {
                    Logger.LogError(message: string.Join(" : ", "UpdateBankAccountAsync", ex?.InnerException?.Message ?? ex?.Message));
                    await tran.RollbackAsync();
                    throw ex;
                }
            }
        }

        /// <summary>
        /// แก้ไข ข้อมูลคู่บัญชี
        /// </summary>
        /// <returns>The bank account.</returns>
        /// <param name="id">Identifier.</param>
        /// <param name="input">Input.</param>
        [HttpPut("ChartOfAccount/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<BankAccountDTO>))]
        public async Task<IActionResult> UpdateChartOfAccountAsync([FromRoute] Guid id, [FromBody] BankAccountDTO input)
        {
            using (var tran = await DB.Database.BeginTransactionAsync())
            {
                try
                {
                    var result = await BankAccountService.UpdateChartOfAccountAsync(id, input);
                    await tran.CommitAsync();
                    return await _httpResultHelper.SuccessCustomResult(result, BankAccountService.logModel);
                }
                catch (Exception ex)
                {
                    Logger.LogError(message: string.Join(" : ", "UpdateChartOfAccountAsync", ex?.InnerException?.Message ?? ex?.Message));
                    await tran.RollbackAsync();
                    throw ex;
                }
            }
        }

        /// <summary>
        /// ลบบัญชีธนาคาร
        /// </summary>
        /// <returns>The bank account.</returns>
        /// <param name="id">Identifier.</param>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBankAccountAsync([FromRoute] Guid id)
        {
            using (var tran = await DB.Database.BeginTransactionAsync())
            {
                try
                {
                    await BankAccountService.DeleteBankAccountAsync(id);
                    await tran.CommitAsync();
                    return Ok();
                }
                catch (Exception ex)
                {
                    Logger.LogError(message: string.Join(" : ", "DeleteBankAccountAsync", ex?.InnerException?.Message ?? ex?.Message));
                    await tran.RollbackAsync();
                    throw ex;
                }
            }
        }
        /// <summary>
        /// ลบบัญชีธนาคาร ทีละหลายรายการ
        /// </summary>
        /// <param name="inputs"></param>
        /// <returns></returns>
        [HttpPost("MultipleDelete")]
        public async Task<IActionResult> DeleteBankAccountListAsync([FromBody] List<BankAccountDTO> inputs)
        {
            using (var tran = await DB.Database.BeginTransactionAsync())
            {
                try
                {
                    await BankAccountService.DeleteBankAccountListAsync(inputs);
                    await tran.CommitAsync();
                    return Ok();
                }
                catch (Exception ex)
                {
                    Logger.LogError(message: string.Join(" : ", "DeleteBankAccountListAsync", ex?.InnerException?.Message ?? ex?.Message));
                    await tran.RollbackAsync();
                    throw ex;
                }
            }
        }
        /// <summary>
        /// สำหรับ export Bank Account
        /// </summary>
        /// <param name="filter"></param> 
        /// <returns></returns>
        [HttpGet("ExportBankAcc")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<FileDTO>))]
        public async Task<IActionResult> ExportBankAcc([FromQuery] BankAccountFilter filter, [FromQuery] PageParam pageParam, [FromQuery] BankAccountSortByParam sortByParam)
        {
            var result = await BankAccountService.ExportExcelBankAccAsync(filter, pageParam, sortByParam);
            return await _httpResultHelper.SuccessCustomResult(result, BankAccountService.logModel);
        }
    }
}
