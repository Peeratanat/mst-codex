using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MST_Finacc.Params.Filters;
using Base.DTOs.MST;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MST_Finacc.Services;
using Database.Models;
using PagingExtensions;
using Base.DTOs;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Net;
using Microsoft.AspNetCore.Authorization;
using Common.Helper.HttpResultHelper;
using Base.DTOs.Common;

namespace MST_Finacc.API.Controllers
{
#if !DEBUG
    [Authorize]
#endif
    [Route(HelperStatic.BaseAPIRoute)]
    [ApiController]
    public class BankBranchsController : BaseController
    {
        private readonly IBankBranchService BankBranchService;
        private readonly ILogger<BankBranchsController> Logger;
        private readonly IHttpResultHelper _httpResultHelper;
        private readonly DatabaseContext DB;

        public BankBranchsController(IBankBranchService bankBranchService, ILogger<BankBranchsController> logger, DatabaseContext db, IHttpResultHelper httpResultHelper)
        {
            BankBranchService = bankBranchService;
            Logger = logger;
            DB = db;
            _httpResultHelper = httpResultHelper;
        }

        /// <summary>
        /// ดึง Dropdown สาขาธนาคาร
        /// </summary>
        /// <returns>The bank branch list.</returns>
        /// <param name="bankID">Bank identifier.</param>
        /// <param name="name">Name.</param>
        [HttpGet("DropdownList")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<List<BankBranchDropdownDTO>>))]
        public async Task<IActionResult> GetBankBranchDropdownList([FromQuery] Guid bankID, [FromQuery] string name, [FromQuery] Guid? provinceID = null, CancellationToken cancellationToken = default)
        {
            var results = await BankBranchService.GetBankBrachDropdownListAsync(bankID, name, provinceID, cancellationToken);
            return await _httpResultHelper.SuccessCustomResult(results, BankBranchService.logModel);
        }
        /// <summary>
        /// ลิสของสาขาธนาคาร
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="pageParam"></param>
        /// <param name="sortByParam"></param>
        /// <returns></returns>
        [HttpGet]
        [PagingResponseHeaders]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<List<BankBranchDTO>>))]
        public async Task<IActionResult> GetBankBranchList([FromQuery] BankBranchFilter filter,
            [FromQuery] PageParam pageParam,
            [FromQuery] BankBranchSortByParam sortByParam, CancellationToken cancellationToken = default)
        {
            var result = await BankBranchService.GetBankBranchListAsync(filter, pageParam, sortByParam, cancellationToken);
            AddPagingResponse(result.PageOutput);
            return await _httpResultHelper.SuccessCustomResult(result.BankBranches, BankBranchService.logModel);
        }
        /// <summary>
        /// ข้อมูลสาขาธนาคาร
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<BankBranchDTO>))]
        public async Task<IActionResult> GetBankBranch([FromRoute] Guid id, CancellationToken cancellationToken = default)
        {
            var result = await BankBranchService.GetBankBranchAsync(id, cancellationToken);
            return await _httpResultHelper.SuccessCustomResult(result, BankBranchService.logModel);
        }
        /// <summary>
        /// สร้างสาขาธนาคาร
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ResponseModel<BankBranchDTO>))]
        public async Task<IActionResult> CreateBankBranchAsync([FromBody] BankBranchDTO input)
        {
            using (var tran = await DB.Database.BeginTransactionAsync())
            {
                try
                {
                    var result = await BankBranchService.CreateBankBranchAsync(input);
                    await tran.CommitAsync();
                    return await _httpResultHelper.SuccessCustomResult(result, BankBranchService.logModel, HttpStatusCode.Created);
                }
                catch (Exception ex)
                {
                    Logger.LogError(message: string.Join(" : ", "CreateBankBranchAsync", ex?.InnerException?.Message ?? ex?.Message));
                    await tran.RollbackAsync();
                    throw ex;
                }
            }
        }
        /// <summary>
        /// แก้ไขสาขาธนาคาร
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<BankBranchDTO>))]
        public async Task<IActionResult> UpdateBankBranchAsync([FromRoute] Guid id, [FromBody] BankBranchDTO input)
        {
            using (var tran = await DB.Database.BeginTransactionAsync())
            {
                try
                {
                    var result = await BankBranchService.UpdateBankBranchAsync(id, input);
                    await tran.CommitAsync();
                    return await _httpResultHelper.SuccessCustomResult(result, BankBranchService.logModel);
                }
                catch (Exception ex)
                {
                    Logger.LogError(message: string.Join(" : ", "UpdateBankBranchAsync", ex?.InnerException?.Message ?? ex?.Message));
                    await tran.RollbackAsync();
                    throw ex;
                }
            }
        }
        /// <summary>
        /// ลบสาขาธนาคาร
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBankBranchAsync([FromRoute] Guid id)
        {
            using (var tran = await DB.Database.BeginTransactionAsync())
            {
                try
                {
                    await BankBranchService.DeleteBankBranchAsync(id);
                    await tran.CommitAsync();
                    return Ok();
                }
                catch (Exception ex)
                {
                    Logger.LogError(message: string.Join(" : ", "DeleteBankBranchAsync", ex?.InnerException?.Message ?? ex?.Message));
                    await tran.RollbackAsync();
                    throw ex;
                }
            }
        }
    }
}