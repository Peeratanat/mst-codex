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
using Microsoft.AspNetCore.Mvc;
using PagingExtensions;
using PRJ_Unit.API;
using PRJ_Unit.API.Controllers;
using PRJ_Unit.Params.Filters;
using PRJ_Unit.Services;

namespace PRJ_Unit.API.Controllers
{
//#if !DEBUG
    [Authorize]
//#endif
    [Route(HelperStatic.BaseAPIRouteController)]
    [ApiController]
    public class UnitsController : BaseController
    {
        private readonly DatabaseContext DB;
        private readonly ILogger<UnitsController> _logger;
        private readonly IUnitService UnitService;
        private readonly IHttpResultHelper _httpResultHelper;

        public UnitsController(DatabaseContext dB, ILogger<UnitsController> logger, IWaiveQCService waiveQCService, IHttpResultHelper httpResultHelper, IUnitService unitService)
        {
            DB = dB;
            _logger = logger;
            _httpResultHelper = httpResultHelper;
            UnitService = unitService;
        }
        /// <summary>
        /// ลิสข้อมูล unit Position
        /// </summary>
        /// <param name="ProjectNo"></param>
        /// <returns></returns>
        [HttpGet("GetUnitPosition")]
        [ProducesResponseType(typeof(ResponseModel<List<UnitDropdownDTO>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetUnitPosition([FromQuery] string ProjectNo, CancellationToken cancellationToken = default)
        {
            Guid? userID = null;
            Guid parsedUserID;
            if (Guid.TryParse(HttpContext?.User?.Claims.SingleOrDefault(x => x.Type == "userid")?.Value, out parsedUserID))
                userID = parsedUserID;
            var result = await UnitService.GetUnitPosition(ProjectNo, cancellationToken);
            return await _httpResultHelper.SuccessCustomResult(result, UnitService.logModel);

        }


        /// <summary>
        /// GetUnitMasterPlanDetail
        /// </summary>
        /// <param name="UnitID"></param>
        /// <returns></returns>
        [HttpGet("GetUnitMasterPlanDetail")]
        [ProducesResponseType(typeof(ResponseModel<UnitMasterPlanDTO>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetUnitMasterPlanDetail([FromQuery] Guid UnitID, CancellationToken cancellationToken = default)
        {

            Guid? userID = null;
            Guid parsedUserID;
            if (Guid.TryParse(HttpContext?.User?.Claims.SingleOrDefault(x => x.Type == "userid")?.Value, out parsedUserID))
                userID = parsedUserID;

            var result = await UnitService.GetUnitMasterPlanDetail(UnitID, cancellationToken);

            return await _httpResultHelper.SuccessCustomResult(result, UnitService.logModel);

        }
        /// <summary>
        /// ลิส ข้อมูลแปลง Dropdown
        /// </summary>
        /// <param name="projectID"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpGet("{projectID}/Units/DropdownList")]
        [ProducesResponseType(typeof(ResponseModel<List<UnitDropdownDTO>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetProjectUnitDropdownList([FromRoute] Guid projectID,
            [FromQuery] Guid? towerID = null,
            [FromQuery] Guid? floorID = null,
            [FromQuery] string name = null,
            [FromQuery] string unitStatusKey = null,
            [FromQuery] bool? allUnit = null, CancellationToken cancellationToken = default)
        {

            var result = await UnitService.GetUnitDropdownListAsync(projectID, towerID, floorID, name, unitStatusKey, allUnit, cancellationToken);
            return await _httpResultHelper.SuccessCustomResult(result, UnitService.logModel);
        }

        /// <summary>
        /// ลิส ข้อมูลแปลง Dropdown quotation
        /// </summary>
        /// <param name="projectID"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpGet("{projectID}/Units/QuotationDropdownList")]
        [ProducesResponseType(typeof(ResponseModel<List<UnitDropdownDTO>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetUnitQuotationDropdownList([FromRoute] Guid projectID,
            [FromQuery] Guid? towerID = null,
            [FromQuery] Guid? floorID = null,
            [FromQuery] string name = null,
            [FromQuery] string unitStatusKey = null,
            [FromQuery] bool? allUnit = null,
            CancellationToken cancellationToken = default)
        {

            var result = await UnitService.GetUnitQuotationDropdownListAsync(projectID, towerID, floorID, name, unitStatusKey, allUnit, cancellationToken);
            return await _httpResultHelper.SuccessCustomResult(result, UnitService.logModel);
        }

        /// <summary>
        /// ลิส ข้อมูลแปลง Dropdown
        /// </summary>
        /// <param name="projectID"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpGet("{projectID}/Units/DropdownListSellPrice")]
        [ProducesResponseType(typeof(ResponseModel<List<UnitDropdownSellPriceDTO>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetProjectUnitDropdownWithSellPriceList([FromRoute] Guid projectID,
            [FromQuery] string name,
            [FromQuery] string unitStatusKey = null,
            CancellationToken cancellationToken = default)
        {
            var result = await UnitService.GetUnitDropdownWithSellPriceListAsync(projectID, name, unitStatusKey, cancellationToken);
            return await _httpResultHelper.SuccessCustomResult(result, UnitService.logModel);
        }
        /// <summary>
        /// ลิส ข้อมูลแปลง
        /// </summary>
        /// <param name="projectID"></param>
        /// <param name="unitFilter"></param>
        /// <param name="pageParam"></param>
        /// <param name="sortByParam"></param>
        /// <returns></returns>
        [HttpGet("{projectID}/Units")]
        [PagingResponseHeaders]
        [ProducesResponseType(typeof(ResponseModel<List<UnitDTO>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetUnitListAsync([FromRoute] Guid projectID,
            [FromQuery] UnitFilter unitFilter,
            [FromQuery] PageParam pageParam,
            [FromQuery] UnitListSortByParam sortByParam,
            CancellationToken cancellationToken = default)
        {
            var result = await UnitService.GetUnitListAsync(projectID, unitFilter, pageParam, sortByParam, cancellationToken);
            AddPagingResponse(result.PageOutput);
            return await _httpResultHelper.SuccessCustomResult(result.Units, UnitService.logModel);

        }
        /// <summary>
        /// ข้อมูลรายละเอียดแปลง
        /// </summary>
        /// <param name="projectID"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{projectID}/Units/{id}")]
        [ProducesResponseType(typeof(ResponseModel<UnitDTO>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetUnitAsync([FromRoute] Guid projectID,
        [FromRoute] Guid id,
            CancellationToken cancellationToken = default)
        {

            var result = await UnitService.GetUnitAsync(projectID, id, cancellationToken);
            return await _httpResultHelper.SuccessCustomResult(result, UnitService.logModel);
        }
        /// <summary>
        /// ข้อมูลแปลงทั่วไป
        /// </summary>
        /// <param name="projectID"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{projectID}/Units/{id}/Info")]
        [ProducesResponseType(typeof(ResponseModel<UnitInfoDTO>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetUnitInfoAsync([FromRoute] Guid projectID, [FromRoute] Guid id,
            CancellationToken cancellationToken = default)
        {

            var result = await UnitService.GetUnitInfoAsync(projectID, id, cancellationToken);
            return await _httpResultHelper.SuccessCustomResult(result, UnitService.logModel);
        }
        /// <summary>
        /// สร้าง ข้อมูลแปลง
        /// </summary>
        /// <param name="projectID"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("{projectID}/Units")]
        [ProducesResponseType(typeof(ResponseModel<UnitDTO>), StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateUnitAsync([FromRoute] Guid projectID, [FromBody] UnitDTO input)
        {
            using (var tran = await DB.Database.BeginTransactionAsync())
            {
                try
                {
                    var result = await UnitService.CreateUnitAsync(projectID, input);
                    await tran.CommitAsync();
                    return await _httpResultHelper.SuccessCustomResult(result, UnitService.logModel, HttpStatusCode.Created);
                }
                catch (Exception ex)
                {
                    _logger.LogError(message: string.Join(" : ", "CreateUnitAsync", ex?.InnerException?.Message ?? ex?.Message));
                    await tran.RollbackAsync();
                    throw ex;
                }
            }
        }
        /// <summary>
        /// แก้ไข ข้อมูลแปลง
        /// </summary>
        /// <param name="projectID"></param>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut("{projectID}/Units/{id}")]
        [ProducesResponseType(typeof(ResponseModel<UnitDTO>), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateUnitAsync([FromRoute] Guid projectID, [FromRoute] Guid id, [FromBody] UnitDTO input)
        {
            using (var tran = await DB.Database.BeginTransactionAsync())
            {
                try
                {

                    Guid? userID = null;
                    //Guid? userID = Guid.Parse("DBBC7F68-4EA8-4C14-9F5E-72D390A5F287");
                    Guid parsedUserID;
                    if (Guid.TryParse(HttpContext?.User?.Claims.SingleOrDefault(x => x.Type == "userid")?.Value, out parsedUserID))
                    {
                        userID = parsedUserID;
                    }

                    var result = await UnitService.UpdateUnitAsync(projectID, id, input, userID);
                    await tran.CommitAsync();
                    return await _httpResultHelper.SuccessCustomResult(result, UnitService.logModel);
                }
                catch (Exception ex)
                {
                    _logger.LogError(message: string.Join(" : ", "UpdateUnitAsync", ex?.InnerException?.Message ?? ex?.Message));
                    await tran.RollbackAsync();
                    throw ex;
                }
            }
        }
        /// <summary>
        /// ลบ ข้อมูลแปลง
        /// </summary>
        /// <param name="projectID"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{projectID}/Units/{id}")]
        public async Task<IActionResult> DeleteUnitAsync([FromRoute] Guid projectID, [FromRoute] Guid id)
        {
            using (var tran = await DB.Database.BeginTransactionAsync())
            {
                try
                {
                    var result = await UnitService.DeleteUnitAsync(projectID, id);
                    await tran.CommitAsync();
                    return await _httpResultHelper.SuccessCustomResult(result, UnitService.logModel);
                }
                catch (Exception ex)
                {
                    _logger.LogError(message: string.Join(" : ", "DeleteUnitAsync", ex?.InnerException?.Message ?? ex?.Message));
                    await tran.RollbackAsync();
                    throw ex;
                }
            }
        }

        /// <summary>
        /// Import Unit Excel (ตั้งต้น)
        /// </summary>
        /// <param name="projectID"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("{projectID}/Units/InitialImport")]
        [ProducesResponseType(typeof(ResponseModel<UnitInitialExcelDTO>), StatusCodes.Status200OK)]
        public async Task<IActionResult> ImportUnitInitialAsync([FromRoute] Guid projectID, [FromBody] FileDTO input)
        {
            using (var tran = await DB.Database.BeginTransactionAsync())
            {
                try
                {
                    Guid? userID = null;
                    Guid parsedUserID;
                    if (Guid.TryParse(HttpContext?.User?.Claims.SingleOrDefault(x => x.Type == "userid")?.Value, out parsedUserID))
                    {
                        userID = parsedUserID;
                    }
                    var result = await UnitService.ImportUnitInitialAsync(projectID, input, userID);
                    await tran.CommitAsync();
                    return await _httpResultHelper.SuccessCustomResult(result, UnitService.logModel);
                }
                catch (Exception ex)
                {
                    _logger.LogError(message: string.Join(" : ", "ImportUnitInitialAsync", ex?.InnerException?.Message ?? ex?.Message));
                    await tran.RollbackAsync();
                    throw ex;
                }
            }
        }

        /// <summary>
        /// Import Unit Excel (ทั่วไป)
        /// </summary>
        /// <param name="projectID"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("{projectID}/Units/GeneralImport")]
        [ProducesResponseType(typeof(ResponseModel<UnitGeneralExcelDTO>), StatusCodes.Status200OK)]
        public async Task<IActionResult> ImportUnitGeneralAsync([FromRoute] Guid projectID, [FromBody] FileDTO input)
        {
            using (var tran = await DB.Database.BeginTransactionAsync())
            {
                try
                {
                    Guid? userID = null;
                    Guid parsedUserID;
                    if (Guid.TryParse(HttpContext?.User?.Claims.SingleOrDefault(x => x.Type == "userid")?.Value, out parsedUserID))
                    {
                        userID = parsedUserID;
                    }
                    var result = await UnitService.ImportUnitGeneralAsync(projectID, input, userID);
                    await tran.CommitAsync();
                    return await _httpResultHelper.SuccessCustomResult(result, UnitService.logModel);
                }
                catch (Exception ex)
                {
                    _logger.LogError(message: string.Join(" : ", "ImportUnitGeneralAsync", ex?.InnerException?.Message ?? ex?.Message));
                    await tran.RollbackAsync();
                    throw ex;
                }
            }
        }
        /// <summary>
        /// Import Excel (พิ้นที่รั้ว)
        /// </summary>
        /// <param name="projectID"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("{projectID}/Units/FenceAreaImport")]
        [ProducesResponseType(typeof(ResponseModel<UnitFenceAreaExcelDTO>), StatusCodes.Status200OK)]
        public async Task<IActionResult> ImportUnitFenceAreaAsync([FromRoute] Guid projectID, [FromBody] FileDTO input)
        {
            using (var tran = await DB.Database.BeginTransactionAsync())
            {
                try
                {
                    var result = await UnitService.ImportUnitFenceAreaAsync(projectID, input);
                    await tran.CommitAsync();
                    return await _httpResultHelper.SuccessCustomResult(result, UnitService.logModel);
                }
                catch (Exception ex)
                {
                    _logger.LogError(message: string.Join(" : ", "ImportUnitFenceAreaAsync", ex?.InnerException?.Message ?? ex?.Message));
                    await tran.RollbackAsync();
                    throw ex;
                }
            }
        }

        /// <summary>
        /// Export excel (ตั้งต้น)
        /// </summary>
        /// <param name="projectID"></param>
        /// <returns></returns>
        [HttpGet("{projectID}/Units/InitialExport")]
        [ProducesResponseType(typeof(ResponseModel<FileDTO>), StatusCodes.Status200OK)]
        public async Task<IActionResult> ExportExcelUnitInitialAsync([FromRoute] Guid projectID)
        {
            try
            {
                var result = await UnitService.ExportExcelUnitInitialAsync(projectID);

                return await _httpResultHelper.SuccessCustomResult(result, UnitService.logModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(message: string.Join(" : ", "ExportExcelUnitInitialAsync", ex?.InnerException?.Message ?? ex?.Message));
                throw ex;
            }
        }

        /// <summary>
        /// Export excel (ทั่วไป)
        /// </summary>
        /// <param name="projectID"></param>
        /// <returns></returns>
        [HttpGet("{projectID}/Units/GeneralExport")]
        [ProducesResponseType(typeof(ResponseModel<FileDTO>), StatusCodes.Status200OK)]
        public async Task<IActionResult> ExportExcelUnitGeneralAsync([FromRoute] Guid projectID)
        {
            try
            {
                var result = await UnitService.ExportExcelUnitGeneralAsync(projectID);

                return await _httpResultHelper.SuccessCustomResult(result, UnitService.logModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(message: string.Join(" : ", "ExportExcelUnitGeneralAsync", ex?.InnerException?.Message ?? ex?.Message));
                throw ex;
            }
        }

        /// <summary>
        /// Export excel (พื้นที่รั้ว)
        /// </summary>
        /// <param name="projectID"></param>
        /// <returns></returns>
        [HttpGet("{projectID}/Units/FenceAreaExport")]
        [ProducesResponseType(typeof(ResponseModel<FileDTO>), StatusCodes.Status200OK)]
        public async Task<IActionResult> ExportExcelUnitFenceAreaAsync([FromRoute] Guid projectID)
        {
            try
            {
                var result = await UnitService.ExportExcelUnitFenceAreaAsync(projectID);

                return await _httpResultHelper.SuccessCustomResult(result, UnitService.logModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(message: string.Join(" : ", "ExportExcelUnitFenceAreaAsync", ex?.InnerException?.Message ?? ex?.Message));
                throw ex;
            }
        }

        [HttpPut("{projectID}/ClearPointUnit/")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<bool>))]
        public async Task<IActionResult> ClearPointUnitAsync([FromRoute] Guid projectID)
        {
            using (var tran = await DB.Database.BeginTransactionAsync())
            {
                try
                {
                    var result = await UnitService.ClearPointUnitAsync(projectID);
                    tran.Commit();
                    return Ok(result);
                }
                catch (Exception ex)
                {
                    await tran.RollbackAsync();
                    throw ex;
                }
            }
        }
        [HttpGet("GetUnitDefectDropdown")]
        [ProducesResponseType(typeof(ResponseModel<List<UnitDropdownDefectDTO>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetUnitDefectDropdown([FromQuery] Guid projectId, [FromQuery] string KeySearch, CancellationToken cancellationToken = default)
        {
            try
            {
                Guid? userID = null;
                Guid parsedUserID;
                if (Guid.TryParse(HttpContext?.User?.Claims.Where(x => x.Type == "userid").Select(o => o.Value).SingleOrDefault(), out parsedUserID))
                    userID = parsedUserID;
                var result = await UnitService.GetUnitDropdownDefectListAsync(projectId, KeySearch, cancellationToken);

                return await _httpResultHelper.SuccessCustomResult(result, UnitService.logModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(message: string.Join(" : ", "ExportExcelUnitFenceAreaAsync", ex?.InnerException?.Message ?? ex?.Message));
                throw ex;
            }
        }
    }
}
