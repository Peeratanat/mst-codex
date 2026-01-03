using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Base.DTOs;
using Base.DTOs.Common;
using Base.DTOs.PRJ;
using Common.Helper.HttpResultHelper;
using Database.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PagingExtensions;
using PRJ_Budget.API;
using PRJ_Budget.Params.Filters;
using PRJ_Budget.Params.Outputs;
using PRJ_Budget.Services;

namespace PRJ_Budget.API.Controllers
{
#if !DEBUG
    [Authorize]
#endif
    [Route(HelperStatic.BaseAPIRoute)]
    [ApiController]
    public class BudgetMinPricesController : BaseController
    {
        private readonly DatabaseContext DB;
        private readonly IBudgetMinPriceService BudgetMinPriceService;
        private readonly IHttpResultHelper _httpResultHelper;
        private readonly ILogger<BudgetMinPricesController> _logger;
        public BudgetMinPricesController(IBudgetMinPriceService budgetMinPriceService, DatabaseContext db, IHttpResultHelper httpResultHelper, ILogger<BudgetMinPricesController> logger)
        {
            this.BudgetMinPriceService = budgetMinPriceService;
            this.DB = db;
            _httpResultHelper = httpResultHelper;
            _logger = logger;
        }



        /// <summary>
        /// ดึงค่า Budget Min Price ตามโครงการและควอเตอร์
        /// ดึงรายการ Unit จากโครงการและควอเตอร์
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpGet("GetBudgetMinPriceList")]
        [PagingResponseHeaders]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<BudgetMinPriceListDTO>))]
        public async Task<IActionResult> GetBudgetMinPriceListAsync([FromQuery] BudgetMinPriceFilter filter, [FromQuery] PageParam pageParam, [FromQuery] BudgetMinPriceSortByParam sortByParam,CancellationToken cancellationToken = default)
        {
            var results = await BudgetMinPriceService.GetBudgetMinPriceListAsync(filter, pageParam, sortByParam,cancellationToken);

            AddPagingResponse(results.PageOutput);
            return await _httpResultHelper.SuccessCustomResult(results.BudgetMinPriceListDTO, BudgetMinPriceService.logModel);

        }



        /// <summary>
        /// บันทึก หรือ แก้ไข Budget Min Price
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("Save")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<BudgetMinPriceDTO>))]
        public async Task<IActionResult> SaveBudgetMinPriceAsync([FromQuery] BudgetMinPriceFilter filter, [FromBody] BudgetMinPriceDTO input)
        {
            using (var tran = await DB.Database.BeginTransactionAsync())
            {
                try
                {
                    var result = await BudgetMinPriceService.SaveBudgetMinPriceAsync(filter, input);
                    await tran.CommitAsync();
                    return await _httpResultHelper.SuccessCustomResult(result, BudgetMinPriceService.logModel);
                }
                catch (Exception ex)
                {
                    _logger.LogError(message: string.Join(" : ", "SaveBudgetMinPriceAsync", ex?.InnerException?.Message ?? ex?.Message));
                    await tran.RollbackAsync();
                    throw ex;
                }
            }
        }

        /// <summary>
        /// แก้ไข Budget Min Price Unit
        /// สร้าง Budget Min Price Unit หากยังไม่มีข้อมูล
        /// </summary>
        /// <param name="inputs"></param>
        /// <returns></returns>
        [HttpPut("SaveBudgetMinPriceUnitList")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> SaveBudgetMinPriceUnitListAsync([FromBody] BudgetMinPriceListDTO inputs)
        {
            using (var tran = await DB.Database.BeginTransactionAsync())
            {
                try
                {
                    //Get user ID
                    Guid userID = Guid.Empty;
                    Guid parsedUserID;
                    if (Guid.TryParse(HttpContext?.User?.Claims.SingleOrDefault(x => x.Type == "userid")?.Value, out parsedUserID))
                    {
                        userID = parsedUserID;
                    }

                    await BudgetMinPriceService.SaveBudgetMinPriceUnitListAsync(inputs, userID, true);
                    await tran.CommitAsync();
                    return Ok();
                }
                catch (Exception ex)
                {
                    _logger.LogError(message: string.Join(" : ", "SaveBudgetMinPriceUnitListAsync", ex?.InnerException?.Message ?? ex?.Message));
                    await tran.RollbackAsync();
                    throw ex;
                }
            }
        }

        /// <summary>
        /// แก้ไข Budget Min Price Unit
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("Units/Save")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<BudgetMinPriceUnitDTO>))]
        public async Task<IActionResult> SaveBudgetMinPriceUnitAsync([FromQuery] BudgetMinPriceFilter filter, [FromQuery] BudgetMinPriceUnitDTO input)
        {
            using (var tran = await DB.Database.BeginTransactionAsync())
            {
                try
                {
                    var result = await BudgetMinPriceService.SaveBudgetMinPriceUnitAsync(filter, input);
                    await tran.CommitAsync();

                    return await _httpResultHelper.SuccessCustomResult(result, BudgetMinPriceService.logModel);
                }
                catch (Exception ex)
                {
                    _logger.LogError(message: string.Join(" : ", "SaveBudgetMinPriceUnitAsync", ex?.InnerException?.Message ?? ex?.Message));
                    await tran.RollbackAsync();
                    throw ex;
                }
            }
        }

        /// <summary>
        /// Import Quarterly Budget Excel เพื่อดึงรายการไปแสดง
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="input"></param>
        /// <param name="notChkAmountZero"></param>
        /// <returns></returns>
        [HttpPost("QuarterlyBudgets/Import")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<BudgetMinPriceQuarterlyDTO>))]
        public async Task<IActionResult> ImportQuarterlyBudgetAsync([FromBody] FileDTO input, [FromQuery] bool notChkAmountZero)
        {
            try
            {
                var results = await BudgetMinPriceService.ImportQuarterlyBudgetAsync(input, notChkAmountZero);
                return await _httpResultHelper.SuccessCustomResult(results, BudgetMinPriceService.logModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(message: string.Join(" : ", "ImportQuarterlyBudgetAsync", ex?.InnerException?.Message ?? ex?.Message));
                throw ex;
            }
        }

        /// <summary>
        /// ยืนยัน Import Quarterly Budget
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("QuarterlyBudgets/ConfirmImport")]
        public async Task<IActionResult> ConfirmImportQuarterlyBudgetAsync([FromBody] BudgetMinPriceQuarterlyDTO input)
        {
            using (var tran = await DB.Database.BeginTransactionAsync())
            {
                try
                {
                    //Get user ID
                    Guid userID = Guid.Empty;
                    Guid parsedUserID;
                    if (Guid.TryParse(HttpContext?.User?.Claims.SingleOrDefault(x => x.Type == "userid")?.Value, out parsedUserID))
                    {
                        userID = parsedUserID;
                    }

                    await BudgetMinPriceService.ConfirmImportQuarterlyBudgetAsync(input, userID);
                    await tran.CommitAsync();

                    return Ok();
                }
                catch (Exception ex)
                {
                    _logger.LogError(message: string.Join(" : ", "ConfirmImportQuarterlyBudgetAsync", ex?.InnerException?.Message ?? ex?.Message));
                    await tran.RollbackAsync();
                    throw ex;
                }
            }
        }

        /// <summary>
        /// Export Quarterly Budget Excel
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpGet("QuarterlyBudgets/Export")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<FileDTO>))]
        public async Task<IActionResult> ExportQuarterlyBudgetAsync([FromQuery] BudgetMinPriceFilter filter,CancellationToken cancellationToken = default)
        {
            try
            {
                var result = await BudgetMinPriceService.ExportQuarterlyBudgetAsync(filter,cancellationToken);

                return await _httpResultHelper.SuccessCustomResult(result, BudgetMinPriceService.logModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(message: string.Join(" : ", "ExportQuarterlyBudgetAsync", ex?.InnerException?.Message ?? ex?.Message));
                throw ex;
            }
        }

        /// <summary>
        /// Import Transfer Budget Excel เพื่อดึงรายการไปแสดง
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("TransferBudgets/Import")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<BudgetMinPriceTransferDTO>))]
        public async Task<IActionResult> ImportTransferBudgetAsync([FromBody] FileDTO input, [FromQuery] bool notChkAmountZero)
        {
            try
            {
                var results = await BudgetMinPriceService.ImportTransferBudgetAsync(input, notChkAmountZero);
                return await _httpResultHelper.SuccessCustomResult(results, BudgetMinPriceService.logModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(message: string.Join(" : ", "ImportTransferBudgetAsync", ex?.InnerException?.Message ?? ex?.Message));
                throw ex;
            }
        }

        /// <summary>
        /// ยืนยัน Import Transfer Budget
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("TransferBudgets/ConfirmImport")]
        public async Task<IActionResult> ConfirmImportTransferBudgetAsync([FromBody] BudgetMinPriceTransferDTO input)
        {
            using (var tran = await DB.Database.BeginTransactionAsync())
            {
                try
                {
                    await BudgetMinPriceService.ConfirmImportTransferBudgetAsync(input);
                    await tran.CommitAsync();
                    return Ok();
                }
                catch (Exception ex)
                {
                    _logger.LogError(message: string.Join(" : ", "ConfirmImportTransferBudgetAsync", ex?.InnerException?.Message ?? ex?.Message));
                    await tran.RollbackAsync();
                    throw ex;
                }
            }
        }

        /// <summary>
        /// Export Transfer Budget Excel
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpGet("TransferBudgets/Export")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<FileDTO>))]
        public async Task<IActionResult> ExportTransferBudgetAsync([FromQuery] BudgetMinPriceFilter filter,CancellationToken cancellationToken = default)
        {
            try
            {
                var result = await BudgetMinPriceService.ExportTransferBudgetAsync(filter,cancellationToken);

                return await _httpResultHelper.SuccessCustomResult(result, BudgetMinPriceService.logModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(message: string.Join(" : ", "ExportTransferBudgetAsync", ex?.InnerException?.Message ?? ex?.Message));
                throw ex;
            }
        }

    }
}
