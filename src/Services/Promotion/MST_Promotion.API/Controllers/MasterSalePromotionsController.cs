using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Base.DTOs.MST;
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
using Base.DTOs.Common;

namespace MST_Promotion.API.Controllers
{

#if !DEBUG
    [Authorize]
#endif
    [Route(HelperStatic.BaseAPIRoute)]
    [ApiController]
    public class MasterSalePromotionsController : BaseController
    {
        private readonly IMasterSalePromotionService MasterSalePromotionService;
        private readonly IHttpResultHelper _httpResultHelper;
        private readonly ILogger<MasterSalePromotionsController> _logger;
        private readonly DatabaseContext DB;

        public MasterSalePromotionsController(IMasterSalePromotionService masterSalePromotionService, ILogger<MasterSalePromotionsController> logger, DatabaseContext db, IHttpResultHelper httpResultHelper)
        {
            this.MasterSalePromotionService = masterSalePromotionService;
            this._logger = logger;
            this.DB = db;
            _httpResultHelper = httpResultHelper;
        }

        /// <summary>
        /// สร้าง Master โปรขาย
        /// </summary>
        /// <returns>The master booking promotion.</returns>
        /// <param name="input">Input.</param>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<MasterSalePromotionDTO>))]
        public async Task<IActionResult> CreateMasterSalePromotionAsync([FromBody] MasterSalePromotionDTO input)
        {
            using (var tran = await DB.Database.BeginTransactionAsync())
            {
                try
                {
                    var result = await MasterSalePromotionService.CreateMasterSalePromotionAsync(input);
                    await tran.CommitAsync();
                    return await _httpResultHelper.SuccessCustomResult(result, MasterSalePromotionService.logModel);
                }
                catch (Exception ex)
                {
                    _logger.LogError(message: string.Join(" : ", "CreateMasterSalePromotionAsync", ex?.InnerException?.Message ?? ex?.Message));
                    await tran.RollbackAsync();
                    throw ex;
                }
            }
        }

        /// <summary>
        /// ลิส Master โปรขาย
        /// </summary>
        /// <returns>The master booking promotion list.</returns>
        /// <param name="filter">Filter.</param>
        /// <param name="pageParam">Page parameter.</param>
        /// <param name="sortByParam">Sort by parameter.</param>
        [HttpGet]
        [PagingResponseHeaders]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<List<MasterSalePromotionDTO>>))]
        public async Task<IActionResult> GetMasterSalePromotionList([FromQuery] MasterSalePromotionListFilter filter,
            [FromQuery] PageParam pageParam,
            [FromQuery] MasterSalePromotionSortByParam sortByParam, CancellationToken cancellationToken = default)
        {

            var result = await MasterSalePromotionService.GetMasterSalePromotionListAsync(filter, pageParam, sortByParam, cancellationToken);
            AddPagingResponse(result.PageOutput);
            return await _httpResultHelper.SuccessCustomResult(result.MasterSalePromotionDTOs, MasterSalePromotionService.logModel);

        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<MasterSalePromotionDTO>))]
        public async Task<IActionResult> GetMasterSalePromotionDetail([FromRoute] Guid id, CancellationToken cancellationToken = default)
        {
            var result = await MasterSalePromotionService.GetMasterSalePromotionDetailAsync(id, cancellationToken);
            return await _httpResultHelper.SuccessCustomResult(result, MasterSalePromotionService.logModel);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<MasterSalePromotionDTO>))]
        public async Task<IActionResult> UpdateMasterSalePromotionAsync([FromRoute] Guid id, [FromBody] MasterSalePromotionDTO input)
        {
            using (var tran = await DB.Database.BeginTransactionAsync())
            {
                try
                {
                    var result = await MasterSalePromotionService.UpdateMasterSalePromotionAsync(id, input);
                    await tran.CommitAsync();
                    return await _httpResultHelper.SuccessCustomResult(result, MasterSalePromotionService.logModel);
                }
                catch (Exception ex)
                {
                    _logger.LogError(message: string.Join(" : ", "UpdateMasterSalePromotionAsync", ex?.InnerException?.Message ?? ex?.Message));
                    await tran.RollbackAsync();
                    throw ex;
                }
            }
        }

        [HttpPut("{id}/ItemsCheck/")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<MasterSalePromotionDTO>))]
        public async Task<IActionResult> CheckActiveMasterSalePromotionAsync([FromRoute] Guid id, [FromBody] MasterSalePromotionDTO input)
        {
            using (var tran = await DB.Database.BeginTransactionAsync())
            {
                try
                {
                    var result = await MasterSalePromotionService.CheckActiveMasterSalePromotionAsync(id, input);
                    await tran.CommitAsync();
                    return await _httpResultHelper.SuccessCustomResult(result, MasterSalePromotionService.logModel);
                }
                catch (Exception ex)
                {
                    _logger.LogError(message: string.Join(" : ", "CheckActiveMasterSalePromotionAsync", ex?.InnerException?.Message ?? ex?.Message));
                    await tran.RollbackAsync();
                    throw ex;
                }
            }
        }

        /// <summary>
        /// ลบโปรขาย
        /// </summary>
        /// <returns>The master booking promotion.</returns>
        /// <param name="id">Identifier.</param>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMasterSalePromotionAsync([FromRoute] Guid id)
        {
            using (var tran = await DB.Database.BeginTransactionAsync())
            {
                try
                {
                    await MasterSalePromotionService.DeleteMasterSalePromotionAsync(id);
                    await tran.CommitAsync();
                    return Ok();
                }
                catch (Exception ex)
                {
                    _logger.LogError(message: string.Join(" : ", "DeleteMasterSalePromotionAsync", ex?.InnerException?.Message ?? ex?.Message));
                    await tran.RollbackAsync();
                    throw ex;
                }
            }
        }

        /// <summary>
        /// ดึงรายการ Master sรโปรขาย
        /// </summary>
        /// <returns>The master booking promotion list.</returns>
        /// <param name="pageParam">Page parameter.</param>
        /// <param name="sortByParam">Sort by parameter.</param>
        [HttpGet("{id}/Items")]
        [PagingResponseHeaders]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<List<MasterSalePromotionItemDTO>>))]
        public async Task<IActionResult> GetMasterSalePromotionItemList([FromRoute] Guid id,
            [FromQuery] PageParam pageParam,
            [FromQuery] MasterSalePromotionItemSortByParam sortByParam, CancellationToken cancellationToken = default)
        {

            var result = await MasterSalePromotionService.GetMasterSalePromotionItemListAsync(id, pageParam, sortByParam, cancellationToken);
            AddPagingResponse(result.PageOutput);
            return await _httpResultHelper.SuccessCustomResult(result.MasterSalePromotionItemDTOs, MasterSalePromotionService.logModel);
        }

        /// <summary>
        /// แก้ไขรายการ Master โปรขาย
        /// </summary>
        /// <returns>The master booking promotion list.</returns>
        /// <param name="inputs">Inputs.</param>
        [HttpPut("{id}/Items/")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<List<MasterSalePromotionItemDTO>>))]
        public async Task<IActionResult> UpdateMasterSalePromotionItemListAsync([FromRoute] Guid id, [FromBody] List<MasterSalePromotionItemDTO> inputs)
        {
            using (var tran = await DB.Database.BeginTransactionAsync())
            {
                try
                {
                    var results = await MasterSalePromotionService.UpdateMasterSalePromotionItemListAsync(id, inputs);
                    await tran.CommitAsync();
                    return await _httpResultHelper.SuccessCustomResult(results, MasterSalePromotionService.logModel);
                }
                catch (Exception ex)
                {
                    _logger.LogError(message: string.Join(" : ", "UpdateMasterSalePromotionItemListAsync", ex?.InnerException?.Message ?? ex?.Message));
                    await tran.RollbackAsync();
                    throw ex;
                }
            }
        }

        /// <summary>
        /// แก้ไขรายการ Master โปรขาย (ทีละรายการ)
        /// </summary>
        /// <returns>The master booking promotion item.</returns>
        /// <param name="id">Identifier.</param>
        /// <param name="masterSalePromotionItemID">Master booking promotion item identifier.</param>
        /// <param name="input">Input.</param>
        [HttpPut("{id}/Items/{masterSalePromotionItemID}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<MasterSalePromotionItemDTO>))]
        public async Task<IActionResult> UpdateMasterSalePromotionItemAsync([FromRoute] Guid id, [FromRoute] Guid masterSalePromotionItemID, [FromBody] MasterSalePromotionItemDTO input)
        {
            using (var tran = await DB.Database.BeginTransactionAsync())
            {
                try
                {
                    var results = await MasterSalePromotionService.UpdateMasterSalePromotionItemAsync(id, masterSalePromotionItemID, input);
                    await tran.CommitAsync();
                    return await _httpResultHelper.SuccessCustomResult(results, MasterSalePromotionService.logModel);

                }
                catch (Exception ex)
                {
                    _logger.LogError(message: string.Join(" : ", "UpdateMasterSalePromotionItemAsync", ex?.InnerException?.Message ?? ex?.Message));
                    await tran.RollbackAsync();
                    throw ex;
                }
            }
        }

        /// <summary>
        /// ลบรายการ Master โปรขาย
        /// </summary>
        /// <returns>The master booking promotion item.</returns>
        /// <param name="id">Identifier.</param>
        /// <param name="masterSalePromotionItemID">Master booking promotion identifier.</param>
        [HttpDelete("{id}/Items/{masterSalePromotionItemID}")]
        public async Task<IActionResult> DeleteMasterSalePromotionItemAsync([FromRoute] Guid id, [FromRoute] Guid masterSalePromotionItemID)
        {
            using (var tran = await DB.Database.BeginTransactionAsync())
            {
                try
                {
                    await MasterSalePromotionService.DeleteMasterSalePromotionItemAsync(masterSalePromotionItemID);
                    await tran.CommitAsync();
                    return Ok();
                }
                catch (Exception ex)
                {
                    _logger.LogError(message: string.Join(" : ", "DeleteMasterSalePromotionItemAsync", ex?.InnerException?.Message ?? ex?.Message));
                    await tran.RollbackAsync();
                    throw ex;
                }
            }
        }

        /// <summary>
        /// สร้างรายการ Master โปรขาย โดยใช้ Material ที่ดึงจาก SAP
        /// </summary>
        /// <returns>The master booking promotion items.</returns>
        /// <param name="id">Identifier.</param>
        /// <param name="inputs">Inputs.</param>
        [HttpPost("{id}/Items")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<List<MasterSalePromotionItemDTO>>))]
        public async Task<IActionResult> CreateMasterSalePromotionItemFromMaterialAsync([FromRoute] Guid id, [FromBody] List<PromotionMaterialDTO> inputs)
        {
            using (var tran = await DB.Database.BeginTransactionAsync())
            {
                try
                {
                    var results = await MasterSalePromotionService.CreateMasterSalePromotionItemFromMaterialAsync(id, inputs);
                    await tran.CommitAsync();
                    return await _httpResultHelper.SuccessCustomResult(results, MasterSalePromotionService.logModel);

                }
                catch (Exception ex)
                {
                    _logger.LogError(message: string.Join(" : ", "CreateMasterSalePromotionItemFromMaterialAsync", ex?.InnerException?.Message ?? ex?.Message));
                    await tran.RollbackAsync();
                    throw ex;
                }
            }
        }

        /// <summary>
        /// สร้างรายการย่อย Master โปรขาย โดยใช้ Material ที่ดึงจาก SAP
        /// </summary>
        /// <returns>The master booking promotion items.</returns>
        /// <param name="id">Identifier.</param>
        /// <param name="inputs">Inputs.</param>
        [HttpPost("{id}/Items/{masterSalePromotionItemID}/SubItems")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<List<MasterSalePromotionItemDTO>>))]
        public async Task<IActionResult> CreateSubMasterSalePromotionItemFromMaterialAsync([FromRoute] Guid id, [FromRoute] Guid masterSalePromotionItemID, [FromBody] List<PromotionMaterialDTO> inputs)
        {
            using (var tran = await DB.Database.BeginTransactionAsync())
            {
                try
                {
                    var results = await MasterSalePromotionService.CreateSubMasterSalePromotionItemFromMaterialAsync(id, masterSalePromotionItemID, inputs);
                    await tran.CommitAsync();
                    return await _httpResultHelper.SuccessCustomResult(results, MasterSalePromotionService.logModel);

                }
                catch (Exception ex)
                {
                    _logger.LogError(message: string.Join(" : ", "CreateSubMasterSalePromotionItemFromMaterialAsync", ex?.InnerException?.Message ?? ex?.Message));
                    await tran.RollbackAsync();
                    throw ex;
                }
            }
        }

        /// <summary>
        /// ดึงแบบบ้านที่ผูกไว้กับรายการ Master โปรขาย
        /// </summary>
        /// <returns>The models of master booking promotion item.</returns>
        /// <param name="id">Identifier.</param>
        /// <param name="masterSalePromotionItemID">Master booking promotion item identifier.</param>
        [HttpGet("{id}/Items/{masterSalePromotionItemID}/Models")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<List<ModelListDTO>>))]
        public async Task<IActionResult> GetModelsOfMasterSalePromotionItem([FromRoute] Guid id, [FromRoute] Guid masterSalePromotionItemID, CancellationToken cancellationToken = default)
        {
            var results = await MasterSalePromotionService.GetMasterSalePromotionItemModelListAsync(masterSalePromotionItemID, cancellationToken);
            return await _httpResultHelper.SuccessCustomResult(results, MasterSalePromotionService.logModel);
        }

        /// <summary>
        /// ระบุแบบบ้าน
        /// </summary>
        /// <returns>The models to master booking promotion item.</returns>
        /// <param name="id">Identifier.</param>
        /// <param name="masterSalePromotionID">Master booking promotion identifier.</param>
        /// <param name="inputs">Inputs.</param>
        [HttpPost("{id}/Items/{masterSalePromotionItemID}/Models/Save")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<List<ModelListDTO>>))]
        public async Task<IActionResult> AddMasterSalePromotionItemModelListAsync([FromRoute] Guid id, [FromRoute] Guid masterSalePromotionItemID,
            [FromBody] List<ModelListDTO> inputs)
        {
            using (var tran = await DB.Database.BeginTransactionAsync())
            {
                try
                {
                    await MasterSalePromotionService.AddMasterSalePromotionItemModelListAsync(masterSalePromotionItemID, inputs);
                    await tran.CommitAsync();
                    return Ok();
                }
                catch (Exception ex)
                {
                    _logger.LogError(message: string.Join(" : ", "AddMasterSalePromotionItemModelListAsync", ex?.InnerException?.Message ?? ex?.Message));
                    await tran.RollbackAsync();
                    throw ex;
                }
            }
        }

        /// <summary>
        /// ดึงรายการที่ไม่ต้องจัดซื้อ
        /// </summary>
        /// <returns>The master booking promotion free item list async.</returns>
        /// <param name="id">Identifier.</param>
        /// <param name="pageParam">Page parameter.</param>
        /// <param name="sortByParam">Sort by parameter.</param>
        [HttpGet("{id}/FreeItems")]
        [PagingResponseHeaders]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<List<MasterSalePromotionFreeItemDTO>>))]
        public async Task<IActionResult> GetMasterSalePromotionFreeItemListAsync([FromRoute] Guid id,
            [FromQuery] PageParam pageParam,
            [FromQuery] MasterSalePromotionFreeItemSortByParam sortByParam, CancellationToken cancellationToken = default)
        {
            var result = await MasterSalePromotionService.GetMasterSalePromotionFreeItemListAsync(id, pageParam, sortByParam, cancellationToken);
            AddPagingResponse(result.PageOutput);
            return await _httpResultHelper.SuccessCustomResult(result.MasterSalePromotionFreeItemDTOs, MasterSalePromotionService.logModel);
        }

        /// <summary>
        /// สร้างรายการที่ไม่ต้องจัดซืื้อ
        /// </summary>
        /// <returns>The master booking promotion free item.</returns>
        /// <param name="id">Identifier.</param>
        /// <param name="input">Input.</param>
        [HttpPost("{id}/FreeItems")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<MasterSalePromotionFreeItemDTO>))]
        public async Task<IActionResult> CreateMasterSalePromotionFreeItemAsync([FromRoute] Guid id,
            [FromBody] MasterSalePromotionFreeItemDTO input)
        {
            using (var tran = await DB.Database.BeginTransactionAsync())
            {
                try
                {
                    await MasterSalePromotionService.CreateMasterSalePromotionFreeItemAsync(id, input);
                    await tran.CommitAsync();
                    return Ok();
                }
                catch (Exception ex)
                {
                    _logger.LogError(message: string.Join(" : ", "CreateMasterSalePromotionFreeItemAsync", ex?.InnerException?.Message ?? ex?.Message));
                    await tran.RollbackAsync();
                    throw ex;
                }
            }
        }

        /// <summary>
        /// แก้ไขรายการที่ไม่ต้องจัดซื้อ
        /// </summary>
        /// <returns>The master booking promotion free item list.</returns>
        /// <param name="id">Identifier.</param>
        /// <param name="masterSalePromotionFreeItemID">Master booking promotion free item identifier.</param>
        /// <param name="inputs">Inputs.</param>
        [HttpPut("{id}/FreeItems")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<List<MasterSalePromotionFreeItemDTO>>))]
        public async Task<IActionResult> UpdateMasterSalePromotionFreeItemListAsync([FromRoute] Guid id,
            [FromBody] List<MasterSalePromotionFreeItemDTO> inputs)
        {
            using (var tran = await DB.Database.BeginTransactionAsync())
            {
                try
                {
                    await MasterSalePromotionService.UpdateMasterSalePromotionFreeItemListAsync(id, inputs);
                    await tran.CommitAsync();
                    return Ok();
                }
                catch (Exception ex)
                {
                    _logger.LogError(message: string.Join(" : ", "UpdateMasterSalePromotionFreeItemListAsync", ex?.InnerException?.Message ?? ex?.Message));
                    await tran.RollbackAsync();
                    throw ex;
                }
            }
        }

        /// <summary>
        /// แก้ไขรายการที่ไม่ต้องจัดซื้อ (ทีละรายการ)
        /// </summary>
        /// <returns>The master booking promotion free item.</returns>
        /// <param name="id">Identifier.</param>
        /// <param name="masterSalePromotionFreeItemID">Master booking promotion free item identifier.</param>
        /// <param name="input">Input.</param>
        [HttpPut("{id}/FreeItems/{masterSalePromotionFreeItemID}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<MasterSalePromotionFreeItemDTO>))]
        public async Task<IActionResult> UpdateMasterSalePromotionFreeItemAsync([FromRoute] Guid id, [FromRoute] Guid masterSalePromotionFreeItemID,
            [FromBody] MasterSalePromotionFreeItemDTO input)
        {
            using (var tran = await DB.Database.BeginTransactionAsync())
            {
                try
                {
                    await MasterSalePromotionService.UpdateMasterSalePromotionFreeItemAsync(id, masterSalePromotionFreeItemID, input);
                    await tran.CommitAsync();
                    return Ok();
                }
                catch (Exception ex)
                {
                    _logger.LogError(message: string.Join(" : ", "UpdateMasterSalePromotionFreeItemAsync", ex?.InnerException?.Message ?? ex?.Message));
                    await tran.RollbackAsync();
                    throw ex;
                }
            }
        }

        /// <summary>
        /// ลบรายการที่ไม่ต้องจัดซื้อ
        /// </summary>
        /// <returns>The master booking promotion free item.</returns>
        /// <param name="id">Identifier.</param>
        /// <param name="masterSalePromotionFreeItemID">Master booking promotion free item identifier.</param>
        [HttpDelete("{id}/FreeItems/{masterSalePromotionFreeItemID}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<List<MasterSalePromotionFreeItemDTO>>))]
        public async Task<IActionResult> DeleteMasterSalePromotionFreeItemAsync([FromRoute] Guid id,
            [FromRoute] Guid masterSalePromotionFreeItemID)
        {
            using (var tran = await DB.Database.BeginTransactionAsync())
            {
                try
                {
                    await MasterSalePromotionService.DeleteMasterSalePromotionFreeItemAsync(masterSalePromotionFreeItemID);
                    await tran.CommitAsync();
                    return Ok();
                }
                catch (Exception ex)
                {
                    _logger.LogError(message: string.Join(" : ", "DeleteMasterSalePromotionFreeItemAsync", ex?.InnerException?.Message ?? ex?.Message));
                    await tran.RollbackAsync();
                    throw ex;
                }
            }
        }

        /// <summary>
        /// ดึงแบบบ้านของรายการที่ไม่ต้องจัดซื้อ
        /// </summary>
        /// <returns>The models of master booking promotion free item.</returns>
        /// <param name="id">Identifier.</param>
        /// <param name="masterSalePromotionFreeItemID">Master booking promotion free item identifier.</param>
        [HttpGet("{id}/FreeItems/{masterSalePromotionFreeItemID}/Models")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<List<ModelListDTO>>))]
        public async Task<IActionResult> GetModelsOfMasterSalePromotionFreeItem([FromRoute] Guid id, [FromRoute] Guid masterSalePromotionFreeItemID, CancellationToken cancellationToken = default)
        {
            var results = await MasterSalePromotionService.GetMasterSalePromotionFreeItemModelListAsync(masterSalePromotionFreeItemID, cancellationToken);
            return await _httpResultHelper.SuccessCustomResult(results, MasterSalePromotionService.logModel);
        }

        /// <summary>
        /// ระบุแบบบ้านให้กับรายการที่ไม่ต้องจัดซื้อ
        /// </summary>
        /// <returns>The master booking promotion free item model list async.</returns>
        /// <param name="id">Identifier.</param>
        /// <param name="masterSalePromotionFreeItemID">Master booking promotion free item identifier.</param>
        /// <param name="inputs">Inputs.</param>
        [HttpPost("{id}/FreeItems/{masterSalePromotionFreeItemID}/Models/Save")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<List<ModelListDTO>>))]
        public async Task<IActionResult> AddMasterSalePromotionFreeItemModelListAsync([FromRoute] Guid id, [FromRoute] Guid masterSalePromotionFreeItemID,
            [FromBody] List<ModelListDTO> inputs)
        {
            using (var tran = await DB.Database.BeginTransactionAsync())
            {
                try
                {
                    await MasterSalePromotionService.AddMasterSalePromotionFreeItemModelListAsync(masterSalePromotionFreeItemID, inputs);
                    await tran.CommitAsync();
                    return Ok();
                }
                catch (Exception ex)
                {
                    _logger.LogError(message: string.Join(" : ", "AddMasterSalePromotionFreeItemModelListAsync", ex?.InnerException?.Message ?? ex?.Message));
                    await tran.RollbackAsync();
                    throw ex;
                }
            }
        }

        /// <summary>
        /// ดึงรายการโปรค่าธรรมเนียมรูดบัตร
        /// </summary>
        /// <returns>The master booking credit card item async.</returns>
        /// <param name="id">Identifier.</param>
        /// <param name="pageParam">Page parameter.</param>
        /// <param name="sortByParam">Sort by parameter.</param>
        [HttpGet("{id}/CreditCardItems")]
        [PagingResponseHeaders]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<List<MasterSalePromotionCreditCardItemDTO>>))]
        public async Task<IActionResult> GetMasterBookingCreditCardItemAsync([FromRoute] Guid id,
            [FromQuery] PageParam pageParam,
            [FromQuery] MasterSalePromotionCreditCardItemSortByParam sortByParam, CancellationToken cancellationToken = default)
        {
            var result = await MasterSalePromotionService.GetMasterSalePromotionCreditCardItemAsync(id, pageParam, sortByParam, cancellationToken);
            AddPagingResponse(result.PageOutput);
            return await _httpResultHelper.SuccessCustomResult(result.MasterSalePromotionCreditCardItemDTOs, MasterSalePromotionService.logModel);
        }

        /// <summary>
        /// สร้างรายการโปรค่าธรรมเนียมรูดบัตร
        /// </summary>
        /// <returns>The master booking credit card item list async.</returns>
        /// <param name="id">Identifier.</param>
        /// <param name="inputs">Master ค่าธรรมเนียมรูดบัตร</param>
        [HttpPost("{id}/CreditCardItems")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<List<MasterSalePromotionCreditCardItemDTO>>))]
        public async Task<IActionResult> CreateMasterSalePromotionCreditCardItemsAsync([FromRoute] Guid id,
            [FromBody] List<EDCFeeDTO> inputs)
        {
            using (var tran = await DB.Database.BeginTransactionAsync())
            {
                try
                {
                    await MasterSalePromotionService.CreateMasterSalePromotionCreditCardItemsAsync(id, inputs);
                    await tran.CommitAsync();
                    return Ok();
                }
                catch (Exception ex)
                {
                    _logger.LogError(message: string.Join(" : ", "CreateMasterSalePromotionCreditCardItemsAsync", ex?.InnerException?.Message ?? ex?.Message));
                    await tran.RollbackAsync();
                    throw ex;
                }
            }
        }

        /// <summary>
        /// สร้างรายการโปรค่าธรรมเนียมรูดบัตร From PromotionMaterialItem
        /// </summary>
        /// <returns>The master booking credit card item list async.</returns>
        /// <param name="id">Identifier.</param>
        /// <param name="inputs">Master ค่าธรรมเนียมรูดบัตร</param>
        [HttpPost("{id}/CreditCardItemsFromPromotionMaterial")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<List<MasterSalePromotionCreditCardItemDTO>>))]
        public async Task<IActionResult> CreditCardItemsFromPromotionMaterialListAsync([FromRoute] Guid id,
            [FromBody] List<PromotionMaterialDTO> inputs)
        {
            using (var tran = await DB.Database.BeginTransactionAsync())
            {
                try
                {
                    await MasterSalePromotionService.CreditCardItemsFromPromotionMaterialListAsync(id, inputs);
                    await tran.CommitAsync();
                    return Ok();
                }
                catch (Exception ex)
                {
                    _logger.LogError(message: string.Join(" : ", "CreditCardItemsFromPromotionMaterialListAsync", ex?.InnerException?.Message ?? ex?.Message));
                    await tran.RollbackAsync();
                    throw ex;
                }
            }
        }

        /// <summary>
        /// แก้ไขรายการโปรค่าธรรมเนียมรูดบัตร
        /// </summary>
        /// <returns>The master booking credit card item list async.</returns>
        /// <param name="id">Identifier.</param>
        /// <param name="inputs">Inputs.</param>
        [HttpPut("{id}/CreditCardItems")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<List<MasterSalePromotionCreditCardItemDTO>>))]
        public async Task<IActionResult> UpdateMasterSalePromotionCreditCardItemListAsync([FromRoute] Guid id,
            [FromBody] List<MasterSalePromotionCreditCardItemDTO> inputs)
        {
            using (var tran = await DB.Database.BeginTransactionAsync())
            {
                try
                {
                    await MasterSalePromotionService.UpdateMasterSalePromotionCreditCardItemListAsync(id, inputs);
                    await tran.CommitAsync();
                    return Ok();
                }
                catch (Exception ex)
                {
                    _logger.LogError(message: string.Join(" : ", "UpdateMasterSalePromotionCreditCardItemListAsync", ex?.InnerException?.Message ?? ex?.Message));
                    await tran.RollbackAsync();
                    throw ex;
                }
            }
        }

        /// <summary>
        /// แก้ไขรายการโปรค่าธรรมเนียมรูดบัตร (ทีละรายการ)
        /// </summary>
        /// <returns>The master booking credit card item list async.</returns>
        /// <param name="id">Identifier.</param>
        /// <param name="masterBookingCreditCardItemID">Master booking credit card item identifier.</param>
        /// <param name="input">Input.</param>
        [HttpPut("{id}/CreditCardItems/{masterBookingCreditCardItemID}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<MasterSalePromotionCreditCardItemDTO>))]
        public async Task<IActionResult> UpdateMasterSalePromotionCreditCardItemAsync([FromRoute] Guid id, [FromRoute] Guid masterBookingCreditCardItemID,
            [FromBody] MasterSalePromotionCreditCardItemDTO input)
        {
            using (var tran = await DB.Database.BeginTransactionAsync())
            {
                try
                {
                    await MasterSalePromotionService.UpdateMasterSalePromotionCreditCardItemAsync(id, masterBookingCreditCardItemID, input);
                    await tran.CommitAsync();
                    return Ok();
                }
                catch (Exception ex)
                {
                    _logger.LogError(message: string.Join(" : ", "UpdateMasterSalePromotionCreditCardItemAsync", ex?.InnerException?.Message ?? ex?.Message));
                    await tran.RollbackAsync();
                    throw ex;
                }
            }
        }

        /// <summary>
        /// ลบรายการโปรค่าธรรมเนียมรูดบัตร
        /// </summary>
        /// <returns>The master booking credit card item list async.</returns>
        /// <param name="id">Identifier.</param>
        /// <param name="masterBookingCreditCardItemID">Master booking credit card item identifier.</param>
        [HttpDelete("{id}/CreditCardItems/{masterBookingCreditCardItemID}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<List<MasterSalePromotionCreditCardItemDTO>>))]
        public async Task<IActionResult> DeleteMasterSalePromotionCreditCardItemAsync([FromRoute] Guid id,
            [FromRoute] Guid masterBookingCreditCardItemID)
        {
            using (var tran = await DB.Database.BeginTransactionAsync())
            {
                try
                {
                    await MasterSalePromotionService.DeleteMasterSalePromotionCreditCardItemAsync(masterBookingCreditCardItemID);
                    await tran.CommitAsync();
                    return Ok();
                }
                catch (Exception ex)
                {
                    _logger.LogError(message: string.Join(" : ", "DeleteMasterSalePromotionCreditCardItemAsync", ex?.InnerException?.Message ?? ex?.Message));
                    await tran.RollbackAsync();
                    throw ex;
                }
            }
        }

        /// <summary>
        /// Clone โปรขาย
        /// </summary>
        /// <returns>The master booking promotion async.</returns>
        /// <param name="id">Identifier.</param>
        [HttpPost("{id}/Clone")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<MasterSalePromotionDTO>))]
        public async Task<IActionResult> CloneMasterSalePromotionAsync([FromRoute] Guid id)
        {
            using (var tran = await DB.Database.BeginTransactionAsync())
            {
                try
                {
                    var result = await MasterSalePromotionService.CloneMasterSalePromotionAsync(id);
                    await tran.CommitAsync();
                    return await _httpResultHelper.SuccessCustomResult(result, MasterSalePromotionService.logModel);
                }
                catch (Exception ex)
                {
                    _logger.LogError(message: string.Join(" : ", "CloneMasterSalePromotionAsync", ex?.InnerException?.Message ?? ex?.Message));
                    await tran.RollbackAsync();
                    throw ex;
                }
            }
        }

        /// <summary>
        /// ดึงจำนวน Item ที่ Clone ได้หรือ Expired ไปแล้ว
        /// </summary>
        /// <returns>The clone master booking promotion confirm async.</returns>
        /// <param name="id">Identifier.</param>
        [HttpGet("{id}/CloneConfirmPopup")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<CloneMasterPromotionConfirmDTO>))]
        public async Task<IActionResult> GetCloneMasterSalePromotionConfirmAsync([FromRoute] Guid id, CancellationToken cancellationToken = default)
        {
            var result = await MasterSalePromotionService.GetCloneMasterSalePromotionConfirmAsync(id, cancellationToken);
            return await _httpResultHelper.SuccessCustomResult(result, MasterSalePromotionService.logModel);
        }

    }
}
