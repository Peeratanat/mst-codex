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
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Net;
using Microsoft.AspNetCore.Authorization;
using Common.Helper.HttpResultHelper;
using Base.DTOs.Common;

namespace MST_Finacc.API.Controllers
{
//#if !DEBUG
    [Authorize]
//#endif
    [Route(HelperStatic.BaseAPIRoute)]
    [ApiController]
    public class BanksController : BaseController
    {
        private IBankService BankService;
        private readonly DatabaseContext DB;
        private readonly ILogger<BankBranchsController> Logger;
        private readonly IHttpResultHelper _httpResultHelper;

        public BanksController(IBankService bankService, DatabaseContext db, IHttpResultHelper httpResultHelper, ILogger<BankBranchsController> logger)
        {
            this.BankService = bankService;
            this.DB = db;
            _httpResultHelper = httpResultHelper;
            Logger = logger;
        }
        /// <summary>
        /// ลิสข้อมูลธนาคาร Dropdown
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpGet("DropdownList")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<List<BankDropdownDTO>>))]
        public async Task<IActionResult> GetBankDropdownList([FromQuery] string name, [FromQuery] bool? IsCreditCard, CancellationToken cancellationToken = default)
        {
            var result = await BankService.GetBankDropdownListAsync(name, IsCreditCard, cancellationToken);
            return await _httpResultHelper.SuccessCustomResult(result, BankService.logModel);
        }
        /// <summary>
        /// ลิสของธนาคาร
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="pageParam"></param>
        /// <param name="sortByParam"></param>
        /// <returns></returns>
        [HttpGet]
        [PagingResponseHeaders]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<List<BankDTO>>))]
        public async Task<IActionResult> GetBankList([FromQuery] BankFilter filter, [FromQuery] PageParam pageParam, [FromQuery] BankSortByParam sortByParam, CancellationToken cancellationToken = default)
        {
            var result = await BankService.GetBankListAsync(filter, pageParam, sortByParam, cancellationToken);
            AddPagingResponse(result.PageOutput);
            return await _httpResultHelper.SuccessCustomResult(result.Banks, BankService.logModel);
        }
        /// <summary>
        /// ข้อมูลธนาคาร
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<BankDTO>))]
        public async Task<IActionResult> GetBank([FromRoute] Guid id, CancellationToken cancellationToken = default)
        {

            var result = await BankService.GetBankAsync(id, cancellationToken);
            return await _httpResultHelper.SuccessCustomResult(result, BankService.logModel);
        }
        /// <summary>
        /// สร้างธนาคาร
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ResponseModel<BankDTO>))]
        public async Task<IActionResult> CreateBankAsync([FromBody] BankDTO input)
        {
            using (var tran = await DB.Database.BeginTransactionAsync())
            {
                try
                {
                    var result = await BankService.CreateBankAsync(input);
                    await tran.CommitAsync();
                    return await _httpResultHelper.SuccessCustomResult(result, BankService.logModel, HttpStatusCode.Created);
                }
                catch (Exception ex)
                {
                    Logger.LogError(message: string.Join(" : ", "CreateBankAsync", ex?.InnerException?.Message ?? ex?.Message));
                    await tran.RollbackAsync();
                    throw ex;
                }
            }
        }
        /// <summary>
        /// แก้ไขข้อมูลธนาคาร
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<BankDTO>))]
        public async Task<IActionResult> UpdateBankAsync([FromRoute] Guid id, [FromBody] BankDTO input)
        {
            using (var tran = await DB.Database.BeginTransactionAsync())
            {
                try
                {
                    var result = await BankService.UpdateBankAsync(id, input);
                    await tran.CommitAsync();
                    return await _httpResultHelper.SuccessCustomResult(result, BankService.logModel);
                }
                catch (Exception ex)
                {
                    Logger.LogError(message: string.Join(" : ", "UpdateBankAsync", ex?.InnerException?.Message ?? ex?.Message));
                    await tran.RollbackAsync();
                    throw ex;
                }
            }
        }
        /// <summary>
        /// ลบธนาคาร
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBankAsync([FromRoute] Guid id)
        {
            using (var tran = await DB.Database.BeginTransactionAsync())
            {
                try
                {
                    await BankService.DeleteBankAsync(id);
                    await tran.CommitAsync();
                    return Ok();
                }
                catch (Exception ex)
                {
                    Logger.LogError(message: string.Join(" : ", "DeleteBankAsync", ex?.InnerException?.Message ?? ex?.Message));
                    await tran.RollbackAsync();
                    throw ex;
                }
            }
        }

        /// <summary>
        /// ลิสข้อมูลธนาคาร Dropdown (Bank only)
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpGet("BankOnlyDropdownList")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<List<BankDropdownDTO>>))]
        public async Task<IActionResult> GetBankOnlyDropdownListAsync([FromQuery] string name, CancellationToken cancellationToken = default)
        {
            var result = await BankService.GetBankOnlyDropdownListAsync(name, cancellationToken);
            return await _httpResultHelper.SuccessCustomResult(result, BankService.logModel);
        }
    }
}