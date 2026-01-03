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
    public class BankBranchBOTsController : BaseController
    {
        private readonly IBankBranchBOTService BankBranchBOTService;
        private readonly ILogger<BankBranchBOTsController> Logger;
        private readonly IHttpResultHelper _httpResultHelper;
        private readonly DatabaseContext DB;

        public BankBranchBOTsController(IBankBranchBOTService bankBranchBOTService, ILogger<BankBranchBOTsController> logger, DatabaseContext db, IHttpResultHelper httpResultHelper)
        {
            this.BankBranchBOTService = bankBranchBOTService;
            this.Logger = logger;
            this.DB = db;
            _httpResultHelper = httpResultHelper;
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
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<List<BankBranchBOTDTO>>))]
        public async Task<IActionResult> GetBankBranchBOTList([FromQuery] BankBranchBOTFilter filter,
            [FromQuery] PageParam pageParam,
            [FromQuery] BankBranchBOTSortByParam sortByParam, CancellationToken cancellationToken = default)
        {
            var result = await BankBranchBOTService.GetBankBranchBOTListAsync(filter, pageParam, sortByParam, cancellationToken);
            AddPagingResponse(result.PageOutput);
            return await _httpResultHelper.SuccessCustomResult(result.BankBrancheBOTs, BankBranchBOTService.logModel);
        }

        /// <summary>
        /// ข้อมูลสาขาธนาคาร
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<BankBranchBOTDTO>))]
        public async Task<IActionResult> GetBankBranchBOT([FromRoute] Guid id, CancellationToken cancellationToken = default)
        {
            var result = await BankBranchBOTService.GetBankBranchBOTAsync(id, cancellationToken);
            return await _httpResultHelper.SuccessCustomResult(result, BankBranchBOTService.logModel);
        }
        /// <summary>
        /// สร้างสาขาธนาคาร
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ResponseModel<BankBranchBOTDTO>))]
        public async Task<IActionResult> CreateBankBranchBOTAsync([FromBody] BankBranchBOTDTO input)
        {
            using (var tran = await DB.Database.BeginTransactionAsync())
            {
                try
                {
                    var result = await BankBranchBOTService.CreateBankBranchBOTAsync(input);
                    await tran.CommitAsync();
                    return await _httpResultHelper.SuccessCustomResult(result, BankBranchBOTService.logModel, HttpStatusCode.Created);
                }
                catch (Exception ex)
                {
                    Logger.LogError(message: string.Join(" : ", "CreateBankBranchBOTAsync", ex?.InnerException?.Message ?? ex?.Message));
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
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<BankBranchBOTDTO>))]
        public async Task<IActionResult> UpdateBankBranchBOTAsync([FromRoute] Guid id, [FromBody] BankBranchBOTDTO input)
        {
            using (var tran = await DB.Database.BeginTransactionAsync())
            {
                try
                {
                    var result = await BankBranchBOTService.UpdateBankBranchBOTAsync(id, input);
                    await tran.CommitAsync();
                    return await _httpResultHelper.SuccessCustomResult(result, BankBranchBOTService.logModel);
                }
                catch (Exception ex)
                {
                    Logger.LogError(message: string.Join(" : ", "UpdateBankBranchBOTAsync", ex?.InnerException?.Message ?? ex?.Message));
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
        public async Task<IActionResult> DeleteBankBranchBOTAsync([FromRoute] Guid id)
        {
            using (var tran = await DB.Database.BeginTransactionAsync())
            {
                try
                {
                    await BankBranchBOTService.DeleteBankBranchBOTAsync(id);
                    await tran.CommitAsync();
                    return Ok();
                }
                catch (Exception ex)
                {
                    Logger.LogError(message: string.Join(" : ", "DeleteBankBranchBOTAsync", ex?.InnerException?.Message ?? ex?.Message));
                    await tran.RollbackAsync();
                    throw ex;
                }
            }
        }
    }
}