using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Base.DTOs.PRJ;
using Base.DTOs.PRM;
using Database.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PagingExtensions;
using MST_Promotion.Params.Filters;
using MST_Promotion.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Common.Helper.HttpResultHelper;
using System.Net;
using Base.DTOs.Common;

namespace MST_Promotion.API.Controllers
{
#if !DEBUG
    [Authorize]
#endif
    [Route(HelperStatic.BaseAPIRoute)]
    [ApiController]
    public class MasterPreSalePromotionsController : BaseController
    {
        private readonly IMasterPreSalePromotionService MasterPreSalePromotionService;
        private readonly ILogger<MasterPreSalePromotionsController> _logger;
        private readonly DatabaseContext DB;
        private readonly IHttpResultHelper _httpResultHelper;


        public MasterPreSalePromotionsController(IMasterPreSalePromotionService masterPreSalePromotionService, ILogger<MasterPreSalePromotionsController> logger, DatabaseContext db, IHttpResultHelper httpResultHelper)
        {
            this.MasterPreSalePromotionService = masterPreSalePromotionService;
            this._logger = logger;
            this.DB = db;
            _httpResultHelper = httpResultHelper;
        }

        /// <summary>
        /// สร้าง Master โปรก่อนขาย
        /// </summary>
        /// <returns>The master presale promotion.</returns>
        /// <param name="input">Input.</param>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ResponseModel<MasterPreSalePromotionDTO>))]
        public async Task<IActionResult> CreateMasterPreSalePromotionAsync([FromBody] MasterPreSalePromotionDTO input)
        {
            using (var tran = await DB.Database.BeginTransactionAsync())
            {
                try
                {
                    var result = await MasterPreSalePromotionService.CreateMasterPreSalePromotionAsync(input);
                    await tran.CommitAsync();
                    return await _httpResultHelper.SuccessCustomResult(result, MasterPreSalePromotionService.logModel, HttpStatusCode.Created);
                }
                catch (Exception ex)
                {
                    _logger.LogError(message: string.Join(" : ", "CreateMasterPreSalePromotionAsync", ex?.InnerException?.Message ?? ex?.Message));
                    await tran.RollbackAsync();
                    throw ex;
                }
            }
        }

        /// <summary>
        /// ลิส Master โปรก่อนขาย
        /// </summary>
        /// <returns>The master presale promotion list.</returns>
        /// <param name="filter">Filter.</param>
        /// <param name="pageParam">Page parameter.</param>
        /// <param name="sortByParam">Sort by parameter.</param>
        [HttpGet]
        [PagingResponseHeaders]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<List<MasterPreSalePromotionDTO>>))]
        public async Task<IActionResult> GetMasterPreSalePromotionListAsync([FromQuery] MasterPreSalePromotionListFilter filter,
            [FromQuery] PageParam pageParam,
            [FromQuery] MasterPreSalePromotionSortByParam sortByParam, CancellationToken cancellationToken = default)
        {
            var result = await MasterPreSalePromotionService.GetMasterPreSalePromotionListAsync(filter, pageParam, sortByParam, cancellationToken);
            AddPagingResponse(result.PageOutput);
            return await _httpResultHelper.SuccessCustomResult(result.MasterPreSalePromotionDTOs, MasterPreSalePromotionService.logModel);
        }

        [HttpGet("DropdownList")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<List<MasterPreSalePromotionDropdownDTO>>))]
        public async Task<IActionResult> GetMasterPreSalePromotionDropdownList([FromQuery] string promotionNo = null, [FromQuery] string name = null, CancellationToken cancellationToken = default)
        {
            var results = await MasterPreSalePromotionService.GetMasterPreSalePromotionDropdownAsync(promotionNo, name, cancellationToken);
            return await _httpResultHelper.SuccessCustomResult(results, MasterPreSalePromotionService.logModel);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<MasterPreSalePromotionDTO>))]
        public async Task<IActionResult> GetMasterPreSalePromotionDetail([FromRoute] Guid id, CancellationToken cancellationToken = default)
        {
            var result = await MasterPreSalePromotionService.GetMasterPreSalePromotionDetailAsync(id,cancellationToken);
            return await _httpResultHelper.SuccessCustomResult(result, MasterPreSalePromotionService.logModel);
        }

        /// <summary>
        /// ดึงโปรโมชั่นก่อนขายที่ Active ล่าสุดของโครงการนั้น
        /// </summary>
        /// <param name="projectID">รหัสโครงการ</param>
        /// <returns></returns>
        [HttpGet("Active")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<MasterPreSalePromotionDTO>))]
        public async Task<IActionResult> GetActiveMasterPreSalePromotionDetail([FromQuery] Guid projectID, CancellationToken cancellationToken = default)
        {
            var result = await MasterPreSalePromotionService.GetActiveMasterPreSalePromotionDetailAsync(projectID,cancellationToken);
            return await _httpResultHelper.SuccessCustomResult(result, MasterPreSalePromotionService.logModel);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<MasterPreSalePromotionDTO>))]
        public async Task<IActionResult> UpdateMasterPreSalePromotionAsync([FromRoute] Guid id, [FromBody] MasterPreSalePromotionDTO input)
        {
            using (var tran = await DB.Database.BeginTransactionAsync())
            {
                try
                {
                    var result = await MasterPreSalePromotionService.UpdateMasterPreSalePromotionAsync(id, input);
                    await tran.CommitAsync();
                    return await _httpResultHelper.SuccessCustomResult(result, MasterPreSalePromotionService.logModel);
                }
                catch (Exception ex)
                {
                    _logger.LogError(message: string.Join(" : ", "UpdateMasterPreSalePromotionAsync", ex?.InnerException?.Message ?? ex?.Message));
                    await tran.RollbackAsync();
                    throw ex;
                }
            }
        }

        /// <summary>
        /// ลบโปรก่อนขาย
        /// </summary>
        /// <returns>The master presale promotion.</returns>
        /// <param name="id">Identifier.</param>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMasterPreSalePromotionAsync([FromRoute] Guid id)
        {
            using (var tran = await DB.Database.BeginTransactionAsync())
            {
                try
                {
                    await MasterPreSalePromotionService.DeleteMasterPreSalePromotionAsync(id);
                    await tran.CommitAsync();
                    return Ok();
                }
                catch (Exception ex)
                {
                    _logger.LogError(message: string.Join(" : ", "DeleteMasterPreSalePromotionAsync", ex?.InnerException?.Message ?? ex?.Message));
                    await tran.RollbackAsync();
                    throw ex;
                }
            }
        }

        /// <summary>
        /// ดึงรายการ Master sรโปรก่อนขาย
        /// </summary>
        /// <returns>The master presale promotion list.</returns>
        /// <param name="pageParam">Page parameter.</param>
        /// <param name="sortByParam">Sort by parameter.</param>
        [HttpGet("{id}/Items")]
        [PagingResponseHeaders]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<List<MasterPreSalePromotionItemDTO>>))]
        public async Task<IActionResult> GetMasterPreSalePromotionItemList([FromRoute] Guid id,
            [FromQuery] PageParam pageParam,
            [FromQuery] MasterPreSalePromotionItemSortByParam sortByParam, CancellationToken cancellationToken = default)
        {
            var result = await MasterPreSalePromotionService.GetMasterPreSalePromotionItemListAsync(id, pageParam, sortByParam,cancellationToken);
            AddPagingResponse(result.PageOutput);
            return await _httpResultHelper.SuccessCustomResult(result.MasterPreSalePromotionItemDTOs, MasterPreSalePromotionService.logModel);
        }

        /// <summary>
        /// แก้ไขรายการ Master โปรก่อนขาย
        /// </summary>
        /// <returns>The master presale promotion list.</returns>
        /// <param name="inputs">Inputs.</param>
        [HttpPut("{id}/Items")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<List<MasterPreSalePromotionItemDTO>>))]
        public async Task<IActionResult> UpdateMasterPreSalePromotionItemListAsync([FromRoute] Guid id, [FromBody] List<MasterPreSalePromotionItemDTO> inputs)
        {
            using (var tran = await DB.Database.BeginTransactionAsync())
            {
                try
                {
                    var results = await MasterPreSalePromotionService.UpdateMasterPreSalePromotionItemListAsync(id, inputs);
                    await tran.CommitAsync();
                    return await _httpResultHelper.SuccessCustomResult(results, MasterPreSalePromotionService.logModel);
                }
                catch (Exception ex)
                {
                    _logger.LogError(message: string.Join(" : ", "UpdateMasterPreSalePromotionItemListAsync", ex?.InnerException?.Message ?? ex?.Message));
                    await tran.RollbackAsync();
                    throw ex;
                }
            }
        }

        /// <summary>
        /// แก้ไขรายการ Master โปรก่อนขาย (ทีละรายการ)
        /// </summary>
        /// <returns>The master preSale promotion item.</returns>
        /// <param name="id">Identifier.</param>
        /// <param name="masterPreSalePromotionItemID">Master preSale promotion item identifier.</param>
        /// <param name="input">Input.</param>
        [HttpPut("{id}/Items/{masterPreSalePromotionItemID}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<MasterPreSalePromotionItemDTO>))]
        public async Task<IActionResult> UpdateMasterPreSalePromotionItemAsync([FromRoute] Guid id, [FromRoute] Guid masterPreSalePromotionItemID, [FromBody] MasterPreSalePromotionItemDTO input)
        {
            using (var tran = await DB.Database.BeginTransactionAsync())
            {
                try
                {
                    var results = await MasterPreSalePromotionService.UpdateMasterPreSalePromotionItemAsync(id, masterPreSalePromotionItemID, input);
                    await tran.CommitAsync();
                    return await _httpResultHelper.SuccessCustomResult(results, MasterPreSalePromotionService.logModel);
                }
                catch (Exception ex)
                {
                    _logger.LogError(message: string.Join(" : ", "UpdateMasterPreSalePromotionItemAsync", ex?.InnerException?.Message ?? ex?.Message));
                    await tran.RollbackAsync();
                    throw ex;
                }
            }
        }

        /// <summary>
        /// ลบรายการ Master โปรก่อนขาย
        /// </summary>
        /// <returns>The master presale promotion item.</returns>
        /// <param name="id">Identifier.</param>
        /// <param name="masterPreSalePromotionItemID">Master presale promotion identifier.</param>
        [HttpDelete("{id}/Items/{masterPreSalePromotionItemID}")]
        public async Task<IActionResult> DeleteMasterPreSalePromotionItemAsync([FromRoute] Guid id, [FromRoute] Guid masterPreSalePromotionItemID)
        {
            using (var tran = await DB.Database.BeginTransactionAsync())
            {
                try
                {
                    await MasterPreSalePromotionService.DeleteMasterPreSalePromotionItemAsync(masterPreSalePromotionItemID);
                    await tran.CommitAsync();
                    return Ok();
                }
                catch (Exception ex)
                {
                    _logger.LogError(message: string.Join(" : ", "DeleteMasterPreSalePromotionItemAsync", ex?.InnerException?.Message ?? ex?.Message));
                    await tran.RollbackAsync();
                    throw ex;
                }
            }
        }

        /// <summary>
        /// สร้างรายการ Master โปรก่อนขาย โดยใช้ Material ที่ดึงจาก SAP
        /// </summary>
        /// <returns>The master presale promotion items.</returns>
        /// <param name="id">Identifier.</param>
        /// <param name="inputs">Inputs.</param>
        [HttpPost("{id}/Items")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<List<MasterPreSalePromotionItemDTO>>))]
        public async Task<IActionResult> CreateMasterPreSalePromotionItemFromMaterialAsync([FromRoute] Guid id, [FromBody] List<PromotionMaterialDTO> inputs)
        {
            using (var tran = await DB.Database.BeginTransactionAsync())
            {
                try
                {
                    var results = await MasterPreSalePromotionService.CreateMasterPreSalePromotionItemFromMaterialAsync(id, inputs);
                    await tran.CommitAsync();
                    return await _httpResultHelper.SuccessCustomResult(results, MasterPreSalePromotionService.logModel);
                }
                catch (Exception ex)
                {
                    _logger.LogError(message: string.Join(" : ", "CreateMasterPreSalePromotionItemFromMaterialAsync", ex?.InnerException?.Message ?? ex?.Message));
                    await tran.RollbackAsync();
                    throw ex;
                }
            }
        }

        /// <summary>
        /// สร้างรายการย่อย Master โปรก่อนขาย โดยใช้ Material ที่ดึงจาก SAP
        /// </summary>
        /// <returns>The master presale promotion items.</returns>
        /// <param name="id">Identifier.</param>
        /// <param name="inputs">Inputs.</param>
        [HttpPost("{id}/Items/{masterPreSalePromotionItemID}/SubItems")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<List<MasterPreSalePromotionItemDTO>>))]
        public async Task<IActionResult> CreateSubMasterPreSalePromotionItemFromMaterialAsync([FromRoute] Guid id, [FromRoute] Guid masterPreSalePromotionItemID, [FromBody] List<PromotionMaterialDTO> inputs)
        {
            using (var tran = await DB.Database.BeginTransactionAsync())
            {
                try
                {
                    await MasterPreSalePromotionService.CreateSubMasterPreSalePromotionItemFromMaterialAsync(id, masterPreSalePromotionItemID, inputs);
                    await tran.CommitAsync();
                    return Ok();
                }
                catch (Exception ex)
                {
                    _logger.LogError(message: string.Join(" : ", "CreateSubMasterPreSalePromotionItemFromMaterialAsync", ex?.InnerException?.Message ?? ex?.Message));
                    await tran.RollbackAsync();
                    throw ex;
                }
            }
        }

        /// <summary>
        /// ดึงแบบบ้านที่ผูกไว้กับรายการ Master โปรก่อนขาย
        /// </summary>
        /// <returns>The models of master presale promotion item.</returns>
        /// <param name="id">Identifier.</param>
        /// <param name="masterPreSalePromotionItemID">Master presale promotion item identifier.</param>
        [HttpGet("{id}/Items/{masterPreSalePromotionItemID}/Models")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<List<ModelListDTO>>))]
        public async Task<IActionResult> GetModelsOfMasterPreSalePromotionItem([FromRoute] Guid id, [FromRoute] Guid masterPreSalePromotionItemID)
        {
            var results = await MasterPreSalePromotionService.GetMasterPreSalePromotionItemModelListAsync(masterPreSalePromotionItemID);
            return await _httpResultHelper.SuccessCustomResult(results, MasterPreSalePromotionService.logModel);

        }

        /// <summary>
        /// ระบุแบบบ้าน
        /// </summary>
        /// <returns>The models to master presale promotion item.</returns>
        /// <param name="id">Identifier.</param>
        /// <param name="masterPreSalePromotionID">Master presale promotion identifier.</param>
        /// <param name="inputs">Inputs.</param>
        [HttpPost("{id}/Items/{masterPreSalePromotionItemID}/Models/Save")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<List<ModelListDTO>>))]
        public async Task<IActionResult> AddMasterPreSalePromotionItemModelListAsync([FromRoute] Guid id, [FromRoute] Guid masterPreSalePromotionItemID,
            [FromBody] List<ModelListDTO> inputs)
        {
            using (var tran = await DB.Database.BeginTransactionAsync())
            {
                try
                {
                    await MasterPreSalePromotionService.AddMasterPreSalePromotionItemModelListAsync(masterPreSalePromotionItemID, inputs);
                    await tran.CommitAsync();
                    return Ok();
                }
                catch (Exception ex)
                {
                    _logger.LogError(message: string.Join(" : ", "AddMasterPreSalePromotionItemModelListAsync", ex?.InnerException?.Message ?? ex?.Message));
                    await tran.RollbackAsync();
                    throw ex;
                }
            }
        }

        /// <summary>
        /// Clone โปรก่อนขาย
        /// </summary>
        /// <returns>The master pre sale promotion async.</returns>
        /// <param name="id">Identifier.</param>
        [HttpPost("{id}/Clone")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<MasterPreSalePromotionDTO>))]
        public async Task<IActionResult> CloneMasterPreSalePromotionAsync([FromRoute] Guid id)
        {
            using (var tran = await DB.Database.BeginTransactionAsync())
            {
                try
                {
                    var result = await MasterPreSalePromotionService.CloneMasterPreSalePromotionAsync(id);
                    await tran.CommitAsync();
                    return await _httpResultHelper.SuccessCustomResult(result, MasterPreSalePromotionService.logModel);

                }
                catch (Exception ex)
                {
                    _logger.LogError(message: string.Join(" : ", "CloneMasterPreSalePromotionAsync", ex?.InnerException?.Message ?? ex?.Message));
                    await tran.RollbackAsync();
                    throw ex;
                }
            }
        }

        /// <summary>
        /// ดึงจำนวน Item ที่ Clone ได้หรือ Expired ไปแล้ว
        /// </summary>
        /// <returns>The clone master pre sale promotion confirm async.</returns>
        /// <param name="id">Identifier.</param>
        [HttpGet("{id}/CloneConfirmPopup")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<CloneMasterPromotionConfirmDTO>))]
        public async Task<IActionResult> GetCloneMasterPreSalePromotionConfirmAsync([FromRoute] Guid id, CancellationToken cancellationToken = default)
        {
            var result = await MasterPreSalePromotionService.GetCloneMasterPreSalePromotionConfirmAsync(id,cancellationToken);
            return await _httpResultHelper.SuccessCustomResult(result, MasterPreSalePromotionService.logModel);

        }

    }
}
