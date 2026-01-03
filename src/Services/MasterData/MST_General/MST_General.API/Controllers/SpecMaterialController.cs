using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Base.DTOs;
using Base.DTOs.CMS;
using Base.DTOs.MST;
using Base.DTOs.PRJ;
using Commission.Services;
using Database.Models;
using MST_General.Params.Filters;
using MST_General.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PagingExtensions;
using Common.Helper.HttpResultHelper;
using System.ComponentModel.DataAnnotations;
using Base.DTOs.Common;
using Report.Integration;

namespace MST_General.API.Controllers
{
#if !DEBUG
    [Authorize]
#endif
    [Route(HelperStatic.BaseAPIRoute)]
    [ApiController]
    public class SpecMaterialController : BaseController
    {
        private ISpecMaterialService SpecMaterialService;
        private readonly DatabaseContext DB;
        private readonly ILogger<SpecMaterialController> _logger;
        private readonly IHttpResultHelper _httpResultHelper;

        public SpecMaterialController(ISpecMaterialService specMateriaService, DatabaseContext db, ILogger<SpecMaterialController> logger, IHttpResultHelper httpResultHelper)
        {
            SpecMaterialService = specMateriaService;
            DB = db;
            _logger = logger;
            _httpResultHelper = httpResultHelper;
        }


        /// <summary>
        /// ลิสข้อมูล Material collection
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="pageParam"></param>
        /// <param name="sortByParam"></param>
        /// <returns></returns>
        [HttpGet("GetSpecMaterialList")]
        [PagingResponseHeaders]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<List<SpecMaterialCollectionDTO>>))]
        public async Task<IActionResult> GetSpecMaterialCollectionListAsync([FromQuery] SpecMaterialCollectionFilter filter, [FromQuery] PageParam pageParam, [FromQuery] SpecMaterialCollectionSortByParam sortByParam, CancellationToken cancellationToken = default)
        {
            var result = await SpecMaterialService.GetSpecMaterialCollectionListAsync(filter, pageParam, sortByParam, cancellationToken);
            AddPagingResponse(result.PageOutput);
            return await _httpResultHelper.SuccessCustomResult(result.SpecMaterialCollection, SpecMaterialService.logModel);
        }

        /// <summary>
        /// get Material collection detail
        /// </summary>
        /// <param name="id"></param>
        /// <param name="filter"></param>
        /// <param name="pageParam"></param>
        /// <param name="sortByParam"></param>
        /// <returns></returns>
        [HttpGet("GetSpecMaterialDetail")]
        [PagingResponseHeaders]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<List<SpecMaterialCollectionDetailDTO>>))]
        public async Task<IActionResult> GetSpecMaterialCollectionDetailAsync([FromQuery] Guid id, [FromQuery] SpecMaterialCollectionDetailFilter filter, [FromQuery] PageParam pageParam, [FromQuery] SpecMaterialCollectionDetailSortByParam sortByParam, CancellationToken cancellationToken = default)
        {
            var result = await SpecMaterialService.GetSpecMaterialCollectionDetailAsync(id, filter, pageParam, sortByParam, cancellationToken);
            AddPagingResponse(result.PageOutput);
            return await _httpResultHelper.SuccessCustomResult(result.SpecMaterialCollectionDetail, SpecMaterialService.logModel);
        }

        /// <summary>
        /// get unit model by project
        /// </summary>
        /// <param name="projectid"></param>
        /// <returns></returns>
        [HttpGet("GetUnitModelBProject")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<List<ModelDropdownDTO>>))]
        public async Task<IActionResult> GetUnitModelByProjectAsync([FromQuery] Guid projectid,Guid collectionID, CancellationToken cancellationToken = default)
        {
            var result = await SpecMaterialService.GetUnitModelByProjectAsync(projectid,collectionID, cancellationToken);
            return await _httpResultHelper.SuccessCustomResult(result, SpecMaterialService.logModel);
        }


        /// <summary>
        /// ลบ Material collection
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="IsActive"></param>
        /// <param name="Name"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("EditSpecMaterialCollection")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<SpecMaterialCollectionDTO>))]
        public async Task<IActionResult> EditSpecMaterialCollectionAsync([FromQuery] Guid ID, [FromQuery] Guid projectID, [FromQuery] bool IsActive, [FromQuery] string Name, [FromBody] List<SpecMaterialCollectionDetailDTO> model)
        {

            using var tran = await DB.Database.BeginTransactionAsync();
            try
            {
                var result = await SpecMaterialService.EditSpecMaterialCollectionAsync(ID, projectID, IsActive, Name, model);
                await tran.CommitAsync();
                return await _httpResultHelper.SuccessCustomResult(result, SpecMaterialService.logModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(message: string.Join(" : ", "EditSpecMaterialCollectionAsync", ex?.InnerException?.Message ?? ex?.Message));
                await tran.RollbackAsync();
                throw;
            }
        }

        /// <summary>
        /// ลบ Material Item
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        [HttpPost("DeleteSpecMaterialItem")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<bool>))]
        public async Task<IActionResult> DeleteSpecMaterialItemAsync([FromQuery] Guid ID)
        {
            try
            {
                await SpecMaterialService.DeleteSpecMaterialItemAsync(ID);
                return await _httpResultHelper.SuccessCustomResult(null, SpecMaterialService.logModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(message: string.Join(" : ", "DeleteSpecMaterialItemAsync", ex?.InnerException?.Message ?? ex?.Message));
                throw;
            }
        }

        /// <summary>
        /// get Material collection detail
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="pageParam"></param>
        /// <param name="sortByParam"></param>
        /// <returns></returns>
        [HttpGet("GetAllSpecMaterialItems")]
        [PagingResponseHeaders]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<List<SpecMaterialCollectionDTO>>))]
        public async Task<IActionResult> GetAllSpecMaterialCollectionItemsAsync([FromQuery] SpecMaterialCollectionDetailFilter filter, [FromQuery] PageParam pageParam, [FromQuery] SpecMaterialCollectionDetailSortByParam sortByParam, CancellationToken cancellationToken = default)
        {
            var result = await SpecMaterialService.GetAllSpecMaterialCollectionItemsAsync(filter, pageParam, sortByParam, cancellationToken);
            AddPagingResponse(result.PageOutput);
            return await _httpResultHelper.SuccessCustomResult(result.SpecMaterialCollectionDetail, SpecMaterialService.logModel);
        }



        /// <summary>
        /// add Material item
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("AddSpecMaterialItems")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<SpecMaterialItemDTO>))]
        public async Task<IActionResult> AddSpecMaterialItemsAsync([FromBody] SpecMaterialCollectionDTO input)
        {
            using var tran = await DB.Database.BeginTransactionAsync();
            try
            {
                var result = await SpecMaterialService.AddSpecMaterialItemsAsync(input);
                return await _httpResultHelper.SuccessCustomResult(result, SpecMaterialService.logModel);
            }
            catch (Exception ex)
            {
                await tran.RollbackAsync();
                _logger.LogError(message: string.Join(" : ", "AddSpecMaterialItemsAsync", ex?.InnerException?.Message ?? ex?.Message));
                throw ex;
            }
        }

        /// <summary>
        /// Edit Material item
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("EditSpecMaterialItems")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<SpecMaterialItemDTO>))]
        public async Task<IActionResult> EditSpecMaterialItemsAsync([FromBody] SpecMaterialCollectionDTO input)
        {
            using var tran = await DB.Database.BeginTransactionAsync();
            try
            {
                var result = await SpecMaterialService.EditSpecMaterialItemsAsync(input);
                return await _httpResultHelper.SuccessCustomResult(result, SpecMaterialService.logModel);
            }
            catch (Exception ex)
            {
                await tran.RollbackAsync();
                _logger.LogError(message: string.Join(" : ", "EditSpecMaterialItemsAsync", ex?.InnerException?.Message ?? ex?.Message));
                throw ex;
            }
        }

        /// <summary>
        /// get Material item by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("GetSpecMaterialItemById")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<SpecMaterialItemDTO>))]
        public async Task<IActionResult> GetSpecMaterialItemByIdAsync([FromQuery] Guid id, CancellationToken cancellationToken = default)
        {
            var result = await SpecMaterialService.GetSpecMaterialItemByIdAsync(id, cancellationToken);
            return await _httpResultHelper.SuccessCustomResult(result, SpecMaterialService.logModel);
        }

        /// <summary>
        /// get Material item by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("GetSpecMaterialDetailByItemId")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<SpecMaterialCollectionDetailDTO>))]
        public async Task<IActionResult> GetSpecMaterialDetailByItemIdAsync([FromQuery] Guid id, CancellationToken cancellationToken = default)
        {
            var result = await SpecMaterialService.GetSpecMaterialDetailByItemIdAsync(id, cancellationToken);
            return await _httpResultHelper.SuccessCustomResult(result, SpecMaterialService.logModel);
        }



        /// <summary>
        /// ลบ Material collection
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        [HttpPost("DeleteSpecMaterialCollection")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<bool>))]
        public async Task<IActionResult> DeleteSpecMaterialCollectionAsync([FromQuery] Guid ID)
        {
            using var tran = await DB.Database.BeginTransactionAsync();
            try
            {
                await SpecMaterialService.DeleteSpecMaterialCollectionAsync(ID);
                await tran.CommitAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                await tran.RollbackAsync();
                _logger.LogError(message: string.Join(" : ", "DeleteSpecMaterialCollectionAsync", ex?.InnerException?.Message ?? ex?.Message));
                throw ex;
            }
        }


        [HttpGet("ExportTemplateSpecMaterial")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<FileDTO>))]
        public async Task<IActionResult> ExportTemplateSpecMaterialAsync([FromQuery] Guid ProjectID)
        {
            var result = await SpecMaterialService.ExportTemplateSpecMaterialAsync(ProjectID);
            return await _httpResultHelper.SuccessCustomResult(result, SpecMaterialService.logModel);
        }

        [HttpGet("ExportSpecMaterialDetail")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<FileDTO>))]
        public async Task<IActionResult> ExportSpecMaterialDetailAsync([Required][FromQuery] Guid SpecMaterialCollectionID)
        {
            var result = await SpecMaterialService.ExportSpecMaterialDetailAsync(SpecMaterialCollectionID);
            if (result is null)
            {
                return await _httpResultHelper.SuccessCustomResult(result, SpecMaterialService.logModel, HttpStatusCode.NoContent);
            }
            else
            {
                return await _httpResultHelper.SuccessCustomResult(result, SpecMaterialService.logModel);
            }
        }


        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<SpecMaterialExcelDTO>))]
        [HttpPost("Import")]
        public async Task<IActionResult> ImportSpecMaterialExcel([FromQuery] Guid ProjectID, [FromBody] FileDTO input)
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

                var result = await SpecMaterialService.ImportSpecMaterialExcel(ProjectID, input, userID);
                await tran.CommitAsync();
                return await _httpResultHelper.SuccessCustomResult(result, SpecMaterialService.logModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(message: string.Join(" : ", "ImportSpecMaterialExcel", ex?.InnerException?.Message ?? ex?.Message));
                await tran.RollbackAsync();
                throw ex;
            }
        }


        /// <summary>
        /// Export Agreement Print Form Template
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="IsEN"></param>
        [HttpGet("ExportSpecMaterialTMPPrintFormUrl")]
        [ProducesResponseType(200, Type = typeof(ResponseModel<ReportResult>))]
        public async Task<IActionResult> ExportSpecMaterialTMPPrintFormUrlAsync([FromQuery] Guid ID, [FromQuery] bool IsEN)
        {
            var result = await SpecMaterialService.ExportSpecMaterialTMPPrintFormUrlAsync(ID, IsEN);
            return await _httpResultHelper.SuccessCustomResult(result, SpecMaterialService.logModel);
        }

        /// <summary>
        /// add Material item
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("AddErrorRecord")]
        [ProducesResponseType(200, Type = typeof(ResponseModel<bool>))]
        public async Task<IActionResult> AddErrorRecordAsync([FromBody] List<ImportError> input)
        {
            using var tran = await DB.Database.BeginTransactionAsync();
            try
            {
                var result = await SpecMaterialService.AddErrorRecordAsync(input);
                await tran.CommitAsync();
                return await _httpResultHelper.SuccessCustomResult(result, SpecMaterialService.logModel);

            }
            catch (Exception ex)
            {
                _logger.LogError(message: string.Join(" : ", "AddErrorRecordAsync", ex?.InnerException?.Message ?? ex?.Message));
                await tran.RollbackAsync();
                throw ex;
            }
        }



        /// <summary>
        /// Export Agreement Print Form Template
        /// </summary>
        /// <param name="agreementID"></param>
        [HttpPost("UpdateCollectionAgreement")]
        [ProducesResponseType(200, Type = typeof(bool))]
        public async Task<IActionResult> UpdateCollectionAgreement([FromQuery] Guid agreementID)
        {
            using (var tran =  await DB.Database.BeginTransactionAsync())
            {
                try
                {

                    var result = await SpecMaterialService.UpdateCollectionAgreementAsync(agreementID);
                    await tran.CommitAsync();
                    return await _httpResultHelper.SuccessCustomResult(result, SpecMaterialService.logModel);

                }
                catch (Exception ex)
                {
                    _logger.LogError(message: string.Join(" : ", "UpdateCollectionAgreement", ex?.InnerException?.Message ?? ex?.Message));
                    await tran.RollbackAsync();
                    throw;
                }
            }
        }


        /// <summary>
        /// Export Agreement Print Form
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="IsEN"></param>
        [HttpGet("ExportSpecMaterialPrintFormUrl")]
        [ProducesResponseType(200, Type = typeof(ReportResult))]
        public async Task<IActionResult> ExportSpecMaterialPrintFormUrlAsync([FromQuery] Guid ID, [FromQuery] bool IsEN)
        {
            try
            {

                var result = await SpecMaterialService.ExportSpecMaterialPrintFormUrlAsync(ID, IsEN);
                return await _httpResultHelper.SuccessCustomResult(result , SpecMaterialService.logModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(message: string.Join(" : ", "UpdateCollectionAgreement", ex?.InnerException?.Message ?? ex?.Message));
                throw;
            }
        }

    }
}