using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Base.DTOs;
using Base.DTOs.PRJ;
using Database.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PagingExtensions;
using MST_Unitmeter.Services;
using Base.DTOs.Common;
using Common.Helper.HttpResultHelper;
using MST_Unitmeter.Params.Filters;

namespace MST_Unitmeter.API.Controllers
{
#if !DEBUG
    [Authorize]
#endif
    [Route(HelperStatic.BaseAPIRoute)]
    [ApiController]
    public class UnitMetersController : BaseController
    {
        private readonly DatabaseContext DB;
        private readonly ILogger<UnitMetersController> _logger;
        private readonly IHttpResultHelper _httpResultHelper;
        private readonly IUnitMeterService UnitMeterService;
        public UnitMetersController(DatabaseContext db, ILogger<UnitMetersController> logger, IHttpResultHelper httpResultHelper, IUnitMeterService unitMeterService)
        {
            DB = db;
            _logger = logger;
            _httpResultHelper = httpResultHelper;
            UnitMeterService = unitMeterService;
        }
        /// <summary>
        /// ลิสราคา มิเตอร์น้ำ
        /// </summary>
        /// <param name="unitID"></param>
        /// <returns></returns>
        [HttpGet("{unitID}/WaterMeterPriceDropdownList")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<List<WaterMeterPriceDropdownDTO>>))]
        public async Task<IActionResult> GetWaterMeterPriceDropdownListAsync([FromRoute] Guid unitID, CancellationToken cancellationToken = default)
        {
            var result = await UnitMeterService.GetWaterMeterPriceDropdownListAsync(unitID, cancellationToken);
            return await _httpResultHelper.SuccessCustomResult(result, UnitMeterService.logModel);
        }
        /// <summary>
        /// ลิสราคา มิเตอร์ไฟ
        /// </summary>
        /// <param name="unitID"></param>
        /// <returns></returns>
        [HttpGet("{unitID}/ElectricMeterPriceDropdownList")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<List<ElectricMeterPriceDropdownDTO>>))]
        public async Task<IActionResult> GetElectricMeterPriceDropdownListAsync([FromRoute] Guid unitID, CancellationToken cancellationToken = default)
        {
            var result = await UnitMeterService.GetElectricMeterPriceDropdownListAsync(unitID, cancellationToken);
            return await _httpResultHelper.SuccessCustomResult(result, UnitMeterService.logModel);
        }
        /// <summary>
        /// ลิสข้อมูล UnitMeter
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="pageParam"></param>
        /// <param name="sortByParam"></param>
        /// <returns></returns>
        [HttpGet]
        [PagingResponseHeaders]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<List<ProjectUnitMeterListDTO>>))]
        public async Task<IActionResult> GetUnitMeterListAsync([FromQuery] UnitMeterFilter filter, [FromQuery] PageParam pageParam, [FromQuery] UnitMeterListSortByParam sortByParam, CancellationToken cancellationToken = default)
        {
            var result = await UnitMeterService.GetUnitMeterListAsync(filter, pageParam, sortByParam, cancellationToken);
            AddPagingResponse(result.PageOutput);
            return await _httpResultHelper.SuccessCustomResult(result.ProjectUnitMeterLists, UnitMeterService.logModel);
        }
        /// <summary>
        /// ข้อมูล UnitMeter
        /// </summary>
        /// <param name="unitID"></param>
        /// <returns></returns>
        [HttpGet("{unitID}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<UnitMeterDTO>))]
        public async Task<IActionResult> GetUnitMeterAsync([FromRoute] Guid unitID, CancellationToken cancellationToken = default)
        {

            var result = await UnitMeterService.GetUnitMeterAsync(unitID, cancellationToken);
            return await _httpResultHelper.SuccessCustomResult(result, UnitMeterService.logModel);
        }
        /// <summary>
        /// ลบ (Reset field Unitmeter)
        /// </summary>
        /// <param name="unitID"></param>
        /// <returns></returns>
        [HttpDelete("{unitID}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<UnitMeterDTO>))]
        public async Task<IActionResult> DeleteUnitMeterAsync([FromRoute] Guid unitID)
        {
            using (var tran = await DB.Database.BeginTransactionAsync())
            {
                try
                {
                    await UnitMeterService.DeleteUnitMeterAsync(unitID);
                    await tran.CommitAsync();
                    return Ok();
                }
                catch (Exception ex)
                {
                    _logger.LogError(message: string.Join(" : ", "DeleteUnitMeterAsync", ex?.InnerException?.Message ?? ex?.Message));
                    await tran.RollbackAsync();
                    throw ex;
                }
            }
        }
        /// <summary>
        /// แก้ไข ข้อมูล UnitMeter
        /// </summary>
        /// <param name="unitID"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut("{unitID}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<UnitMeterDTO>))]
        public async Task<IActionResult> UpdateUnitMeterAsync([FromRoute] Guid unitID, [FromBody] UnitMeterDTO input)
        {
            using var tran = await DB.Database.BeginTransactionAsync();
            try
            {
                Guid? userID = null;
                Guid parsedUserID;
                if (Guid.TryParse(HttpContext?.User?.Claims.SingleOrDefault(x => x.Type == "userid")?.Value, out parsedUserID))
                {
                    userID = parsedUserID;
                }

                var result = await UnitMeterService.UpdateUnitMeterAsync(unitID, input, userID);
                await tran.CommitAsync();
                return await _httpResultHelper.SuccessCustomResult(result, UnitMeterService.logModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(message: string.Join(" : ", "UpdateUnitMeterAsync", ex?.InnerException?.Message ?? ex?.Message));
                await tran.RollbackAsync();
                throw ex;
            }
        }
        /// <summary>
        /// Import เลขมิเตอร์
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("UnitMeter/Import")]
        public async Task<IActionResult> ImportUnitMeterExcelAsync([FromBody] FileDTO input)
        {
            using (var tran = await DB.Database.BeginTransactionAsync())
            {
                try
                {
                    var result = await UnitMeterService.ImportUnitMeterExcelAsync(input);
                    await tran.CommitAsync();
                    return await _httpResultHelper.SuccessCustomResult(result, UnitMeterService.logModel);
                }
                catch (Exception ex)
                {
                    _logger.LogError(message: string.Join(" : ", "ImportUnitMeterExcelAsync", ex?.InnerException?.Message ?? ex?.Message));
                    await tran.RollbackAsync();
                    throw ex;
                }
            }
        }
        /// <summary>
        /// Export เลขมิเตอร์
        /// </summary>
        /// <returns></returns>
        [HttpGet("UnitMeter/Export")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<FileDTO>))]
        public async Task<IActionResult> ExportUnitMeterExcelAsync([FromQuery] UnitMeterFilter filter, [FromQuery] UnitMeterListSortByParam sortByParam, CancellationToken cancellationToken = default)
        {
            try
            {
                var result = await UnitMeterService.ExportUnitMeterExcelAsync(filter, sortByParam, cancellationToken);
                return await _httpResultHelper.SuccessCustomResult(result, UnitMeterService.logModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(message: string.Join(" : ", "ExportUnitMeterExcelAsync", ex?.InnerException?.Message ?? ex?.Message));
                throw ex;
            }
        }
        /// <summary>
        /// Import สถานะมิเตอร์
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("UnitMeterStatus/Import")]
        public async Task<IActionResult> ImportUnitMeterStatusExcelAsync([FromBody] FileDTO input)
        {
            using (var tran = await DB.Database.BeginTransactionAsync())
            {
                try
                {
                    var result = await UnitMeterService.ImportUnitMeterStatusExcelAsync(input);
                    await tran.CommitAsync();
                    return await _httpResultHelper.SuccessCustomResult(result, UnitMeterService.logModel);
                }
                catch (Exception ex)
                {
                    _logger.LogError(message: string.Join(" : ", "ImportUnitMeterStatusExcelAsync", ex?.InnerException?.Message ?? ex?.Message));
                    await tran.RollbackAsync();
                    throw ex;
                }
            }
        }
        /// <summary>
        /// Export สถานะมิเตอร์
        /// </summary>
        /// <returns></returns>
        [HttpGet("UnitMeterStatus/Export")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<FileDTO>))]
        public async Task<IActionResult> ExportUnitMeterStatusExcelAsync([FromQuery] UnitMeterFilter filter, [FromQuery] UnitMeterListSortByParam sortByParam, CancellationToken cancellationToken = default)
        {
            try
            {
                var result = await UnitMeterService.ExportUnitMeterStatusExcelAsync(filter, sortByParam, cancellationToken);
                return await _httpResultHelper.SuccessCustomResult(result, UnitMeterService.logModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(message: string.Join(" : ", "ExportUnitMeterStatusExcelAsync", ex?.InnerException?.Message ?? ex?.Message));
                throw ex;
            }
        }
    }
}