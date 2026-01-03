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
    public class BudgetPromotionsController : BaseController
    {
        private readonly DatabaseContext DB;
        private readonly IBudgetPromotionService BudgetPromotionService;

        private readonly IHttpResultHelper _httpResultHelper;
        private readonly ILogger<BudgetPromotionsController> _logger;
        public BudgetPromotionsController(DatabaseContext db, IHttpResultHelper httpResultHelper, IBudgetPromotionService budgetPromotionService, ILogger<BudgetPromotionsController> logger)
        {
            this.DB = db;
            _httpResultHelper = httpResultHelper;
            BudgetPromotionService = budgetPromotionService;
            _logger = logger;
        }
        /// <summary>
        /// ลิส ข้อมูลBudgetPromotions
        /// </summary>
        /// <param name="projectID"></param>
        /// <param name="filter"></param>
        /// <param name="pageParam"></param>
        /// <param name="sortByParam"></param>
        /// <returns></returns>
        [HttpGet("{projectID}/BudgetPromotions")]
        [PagingResponseHeaders]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<List<BudgetPromotionDTO>>))]
        public async Task<IActionResult> GetBudgetPromotionListAsync([FromRoute] Guid projectID, [FromQuery] BudgetPromotionFilter filter, [FromQuery] PageParam pageParam, [FromQuery] BudgetPromotionSortByParam sortByParam, CancellationToken cancellationToken = default)
        {
            var result = await BudgetPromotionService.GetBudgetPromotionListAsync(projectID, filter, pageParam, sortByParam, cancellationToken);
            AddPagingResponse(result.PageOutput);
            return await _httpResultHelper.SuccessCustomResult(result.BudgetPromotions, BudgetPromotionService.logModel);
        }

        [HttpGet("{projectID}/BudgetPromotions/{unitID}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<BudgetPromotionDTO>))]
        public async Task<IActionResult> GetBudgetPromotionAsync([FromRoute] Guid projectID, [FromRoute] Guid unitID, CancellationToken cancellationToken = default)
        {
            var result = await BudgetPromotionService.GetBudgetPromotionAsync(projectID, unitID, cancellationToken);
            return await _httpResultHelper.SuccessCustomResult(result, BudgetPromotionService.logModel);
        }

        [HttpPost("{projectID}/BudgetPromotions")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ResponseModel<BudgetPromotionDTO>))]
        public async Task<IActionResult> CreateBudgetPromotionAsync([FromRoute] Guid projectID, [FromBody] BudgetPromotionDTO input)
        {
            using (var tran = await DB.Database.BeginTransactionAsync())
            {
                try
                {
                    var result = await BudgetPromotionService.CreateBudgetPromotionAsync(projectID, input);
                    await tran.CommitAsync();
                    return await _httpResultHelper.SuccessCustomResult(result, BudgetPromotionService.logModel);
                }
                catch (Exception ex)
                {
                    _logger.LogError(message: string.Join(" : ", "CreateBudgetPromotionAsync", ex?.InnerException?.Message ?? ex?.Message));
                    await tran.RollbackAsync();
                    throw ex;
                }
            }
        }
        /// <summary>
        /// แก้ไข ข้อมูลBudgetPromotions
        /// </summary>
        /// <param name="projectID"></param>
        /// <param name="unitID"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut("{projectID}/BudgetPromotions/{unitID}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<BudgetPromotionDTO>))]
        public async Task<IActionResult> UpdateBudgetPromotionAsync([FromRoute] Guid projectID, [FromRoute] Guid unitID, [FromBody] BudgetPromotionDTO input)
        {
            using (var tran = await DB.Database.BeginTransactionAsync())
            {
                try
                {
                    var result = await BudgetPromotionService.UpdateBudgetPromotionAsync(projectID, unitID, input);
                    await tran.CommitAsync();
                    return await _httpResultHelper.SuccessCustomResult(result, BudgetPromotionService.logModel);
                }
                catch (Exception ex)
                {
                    _logger.LogError(message: string.Join(" : ", "UpdateBudgetPromotionAsync", ex?.InnerException?.Message ?? ex?.Message));
                    await tran.RollbackAsync();
                    throw ex;
                }
            }
        }
        /// <summary>
        /// ลบ ข้อมูลBudgetPromotions
        /// </summary>
        /// <param name="projectID"></param>
        /// <param name="unitID"></param>
        /// <returns></returns>
        [HttpDelete("{projectID}/BudgetPromotions/{unitID}")]
        public async Task<IActionResult> DeleteBudgetPromotionAsync([FromRoute] Guid projectID, [FromRoute] Guid unitID)
        {
            using (var tran = await DB.Database.BeginTransactionAsync())
            {
                try
                {
                    await BudgetPromotionService.DeleteBudgetPromotionAsync(projectID, unitID);
                    await tran.CommitAsync();
                    return Ok();
                }
                catch (Exception ex)
                {
                    _logger.LogError(message: string.Join(" : ", "DeleteBudgetPromotionAsync", ex?.InnerException?.Message ?? ex?.Message));
                    await tran.RollbackAsync();
                    throw ex;
                }
            }
        }
        /// <summary>
        /// Import BudgetPromotion
        /// </summary>
        /// <param name="projectID"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("{projectID}/BudgetPromotions/Import")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<BudgetPromotionExcelDTO>))]
        public async Task<IActionResult> ImportBudgetPromotionAsync([FromRoute] Guid projectID, [FromBody] FileDTO input)
        {
            using (var tran = await DB.Database.BeginTransactionAsync())
            {
                try
                {
                    Guid? userID = null;
                    Guid parsedUserID;
                    if (Guid.TryParse(HttpContext?.User?.Claims.SingleOrDefault(x => x.Type == "userid")?.Value, out parsedUserID))
                        userID = parsedUserID;

                    var result = await BudgetPromotionService.ImportBudgetPromotionAsync(projectID, input, userID);
                    await tran.CommitAsync();
                    return await _httpResultHelper.SuccessCustomResult(result, BudgetPromotionService.logModel);
                }
                catch (Exception ex)
                {
                    _logger.LogError(message: string.Join(" : ", "ImportBudgetPromotionAsync", ex?.InnerException?.Message ?? ex?.Message));
                    await tran.RollbackAsync();
                    throw ex;
                }
            }
        }
        /// <summary>
        /// Export BudgetPromotion
        /// </summary>
        /// <param name="projectID"></param>s
        /// <returns></returns>
        [HttpGet("{projectID}/BudgetPromotions/Export")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<FileDTO>))]
        public async Task<IActionResult> ExportExcelBudgetPromotionAsync([FromRoute] Guid projectID, CancellationToken cancellationToken = default)
        {
            try
            {
                var result = await BudgetPromotionService.ExportExcelBudgetPromotionAsync(projectID);
                return await _httpResultHelper.SuccessCustomResult(result, BudgetPromotionService.logModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(message: string.Join(" : ", "ExportExcelBudgetPromotionAsync", ex?.InnerException?.Message ?? ex?.Message));
                throw ex;
            }
        }


    }
}
