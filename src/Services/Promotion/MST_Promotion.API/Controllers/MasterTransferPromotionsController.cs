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
using MST_Promotion.API;
using MST_Promotion.API.Controllers;
using Common.Helper.HttpResultHelper;
using Base.DTOs.Common;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MST_MST_Promotion.API.Controllers
{
#if !DEBUG
    [Authorize]
#endif
    [Route(HelperStatic.BaseAPIRoute)]
    [ApiController]
    public class MasterTransferPromotionsController : BaseController
    {
        private readonly IMasterTransferPromotionService MasterTransferPromotionService;
        private readonly IHttpResultHelper _httpResultHelper;
        private readonly ILogger<MasterTransferPromotionsController> _logger;
        private readonly DatabaseContext DB;

        public MasterTransferPromotionsController(IMasterTransferPromotionService masterTransferPromotionService, ILogger<MasterTransferPromotionsController> logger, DatabaseContext db, IHttpResultHelper httpResultHelper)
        {
            this.MasterTransferPromotionService = masterTransferPromotionService;
            _logger = logger;
            this.DB = db;
            _httpResultHelper = httpResultHelper;
        }

        /// <summary>
        /// สร้าง Master โปรโอน
        /// </summary>
        /// <returns>The master transfer promotion.</returns>
        /// <param name="input">Input.</param>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<MasterTransferPromotionDTO>))]
        public async Task<IActionResult> CreateMasterTransferPromotionAsync([FromBody] MasterTransferPromotionDTO input)
        {
            using (var tran = await DB.Database.BeginTransactionAsync())
            {
                try
                {
                    var result = await MasterTransferPromotionService.CreateMasterTransferPromotionAsync(input);
                    await tran.CommitAsync();
                    return await _httpResultHelper.SuccessCustomResult(result, MasterTransferPromotionService.logModel);
                }
                catch (Exception ex)
                {
                    _logger.LogError(message: string.Join(" : ", "CreateMasterTransferPromotionAsync", ex?.InnerException?.Message ?? ex?.Message));
                    await tran.RollbackAsync();
                    throw ex;
                }
            }
        }

        /// <summary>
        /// ลิส Master โปรโอน
        /// </summary>
        /// <returns>The master transfer promotion list.</returns>
        /// <param name="filter">Filter.</param>
        /// <param name="pageParam">Page parameter.</param>
        /// <param name="sortByParam">Sort by parameter.</param>
        [HttpGet]
        [PagingResponseHeaders]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<List<MasterTransferPromotionDTO>>))]
        public async Task<IActionResult> GetMasterTransferPromotionList([FromQuery] MasterTransferPromotionListFilter filter,
            [FromQuery] PageParam pageParam,
            [FromQuery] MasterTransferPromotionSortByParam sortByParam, CancellationToken cancellationToken = default)
        {
            var result = await MasterTransferPromotionService.GetMasterTransferPromotionListAsync(filter, pageParam, sortByParam, cancellationToken);
            AddPagingResponse(result.PageOutput);
            return await _httpResultHelper.SuccessCustomResult(result.MasterTransferPromotionDTOs, MasterTransferPromotionService.logModel);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<MasterTransferPromotionDTO>))]
        public async Task<IActionResult> GetMasterTransferPromotionDetail([FromRoute] Guid id, CancellationToken cancellationToken = default)
        {
            var result = await MasterTransferPromotionService.GetMasterTransferPromotionDetailAsync(id, cancellationToken);
            return await _httpResultHelper.SuccessCustomResult(result, MasterTransferPromotionService.logModel);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<MasterTransferPromotionDTO>))]
        public async Task<IActionResult> UpdateMasterTransferPromotionAsync([FromRoute] Guid id, [FromBody] MasterTransferPromotionDTO input)
        {
            using (var tran = await DB.Database.BeginTransactionAsync())
            {
                try
                {
                    var result = await MasterTransferPromotionService.UpdateMasterTransferPromotionAsync(id, input);
                    await tran.CommitAsync();
                    return await _httpResultHelper.SuccessCustomResult(result, MasterTransferPromotionService.logModel);
                }
                catch (Exception ex)
                {
                    _logger.LogError(message: string.Join(" : ", "UpdateMasterTransferPromotionAsync", ex?.InnerException?.Message ?? ex?.Message));
                    await tran.RollbackAsync();
                    throw ex;
                }
            }
        }

        /// <summary>
        /// ลบโปรโอน
        /// </summary>
        /// <returns>The master transfer promotion.</returns>
        /// <param name="id">Identifier.</param>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMasterTransferPromotionAsync([FromRoute] Guid id)
        {
            using (var tran = await DB.Database.BeginTransactionAsync())
            {
                try
                {
                    await MasterTransferPromotionService.DeleteMasterTransferPromotionAsync(id);
                    await tran.CommitAsync();
                    return Ok();
                }
                catch (Exception ex)
                {
                    _logger.LogError(message: string.Join(" : ", "DeleteMasterTransferPromotionAsync", ex?.InnerException?.Message ?? ex?.Message));
                    await tran.RollbackAsync();
                    throw ex;
                }
            }
        }

        /// <summary>
        /// ดึงรายการ Master sรโปรโอน
        /// </summary>
        /// <returns>The master transfer promotion list.</returns>
        /// <param name="pageParam">Page parameter.</param>
        /// <param name="sortByParam">Sort by parameter.</param>
        [HttpGet("{id}/Items")]
        [PagingResponseHeaders]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<List<MasterTransferPromotionItemDTO>>))]
        public async Task<IActionResult> GetMasterTransferPromotionItemList([FromRoute] Guid id,
            [FromQuery] PageParam pageParam,
            [FromQuery] MasterTransferPromotionItemSortByParam sortByParam, CancellationToken cancellationToken = default)
        {
            var result = await MasterTransferPromotionService.GetMasterTransferPromotionItemListAsync(id, pageParam, sortByParam, cancellationToken);
            AddPagingResponse(result.PageOutput);
            return await _httpResultHelper.SuccessCustomResult(result.MasterTransferPromotionItemDTOs, MasterTransferPromotionService.logModel);
        }

        /// <summary>
        /// แก้ไขรายการ Master โปรโอน
        /// </summary>
        /// <returns>The master transfer promotion list.</returns>
        /// <param name="inputs">Inputs.</param>
        [HttpPut("{id}/Items")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<List<MasterTransferPromotionItemDTO>>))]
        public async Task<IActionResult> UpdateMasterTransferPromotionItemListAsync([FromRoute] Guid id, [FromBody] List<MasterTransferPromotionItemDTO> inputs)
        {
            using (var tran = await DB.Database.BeginTransactionAsync())
            {
                try
                {
                    var results = await MasterTransferPromotionService.UpdateMasterTransferPromotionItemListAsync(id, inputs);
                    await tran.CommitAsync();
                    return await _httpResultHelper.SuccessCustomResult(results, MasterTransferPromotionService.logModel);

                }
                catch (Exception ex)
                {
                    _logger.LogError(message: string.Join(" : ", "UpdateMasterTransferPromotionItemListAsync", ex?.InnerException?.Message ?? ex?.Message));
                    await tran.RollbackAsync();
                    throw ex;
                }
            }
        }

        /// <summary>
        /// แก้ไขรายการ Master โปรโอน (ทีละรายการ)
        /// </summary>
        /// <returns>The master transfer promotion item.</returns>
        /// <param name="id">Identifier.</param>
        /// <param name="masterTransferPromotionItemID">Master transfer promotion item identifier.</param>
        /// <param name="input">Input.</param>
        [HttpPut("{id}/Items/{masterTransferPromotionItemID}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<MasterTransferPromotionItemDTO>))]
        public async Task<IActionResult> UpdateMasterTransferPromotionItemAsync([FromRoute] Guid id, [FromRoute] Guid masterTransferPromotionItemID, [FromBody] MasterTransferPromotionItemDTO input)
        {
            using (var tran = await DB.Database.BeginTransactionAsync())
            {
                try
                {
                    var results = await MasterTransferPromotionService.UpdateMasterTransferPromotionItemAsync(id, masterTransferPromotionItemID, input);
                    await tran.CommitAsync();
                    return await _httpResultHelper.SuccessCustomResult(results, MasterTransferPromotionService.logModel);

                }
                catch (Exception ex)
                {
                    _logger.LogError(message: string.Join(" : ", "UpdateMasterTransferPromotionItemAsync", ex?.InnerException?.Message ?? ex?.Message));
                    await tran.RollbackAsync();
                    throw ex;
                }
            }
        }

        /// <summary>
        /// ลบรายการ Master โปรโอน
        /// </summary>
        /// <returns>The master transfer promotion item.</returns>
        /// <param name="id">Identifier.</param>
        /// <param name="masterTransferPromotionItemID">Master transfer promotion identifier.</param>
        [HttpDelete("{id}/Items/{masterTransferPromotionItemID}")]
        public async Task<IActionResult> DeleteMasterTransferPromotionItemAsync([FromRoute] Guid id, [FromRoute] Guid masterTransferPromotionItemID)
        {
            using (var tran = await DB.Database.BeginTransactionAsync())
            {
                try
                {
                    await MasterTransferPromotionService.DeleteMasterTransferPromotionItemAsync(masterTransferPromotionItemID);
                    await tran.CommitAsync();
                    return Ok();
                }
                catch (Exception ex)
                {
                    _logger.LogError(message: string.Join(" : ", "DeleteMasterTransferPromotionItemAsync", ex?.InnerException?.Message ?? ex?.Message));
                    await tran.RollbackAsync();
                    throw ex;
                }
            }
        }

        /// <summary>
        /// สร้างรายการ Master โปรโอน โดยใช้ Material ที่ดึงจาก SAP
        /// </summary>
        /// <returns>The master transfer promotion items.</returns>
        /// <param name="id">Identifier.</param>
        /// <param name="inputs">Inputs.</param>
        [HttpPost("{id}/Items")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<List<MasterTransferPromotionItemDTO>>))]
        public async Task<IActionResult> CreateMasterTransferPromotionItemFromMaterialAsync([FromRoute] Guid id, [FromBody] List<PromotionMaterialDTO> inputs)
        {
            using (var tran = await DB.Database.BeginTransactionAsync())
            {
                try
                {
                    var results = await MasterTransferPromotionService.CreateMasterTransferPromotionItemFromMaterialAsync(id, inputs);
                    await tran.CommitAsync();
                    return await _httpResultHelper.SuccessCustomResult(results, MasterTransferPromotionService.logModel);

                }
                catch (Exception ex)
                {
                    _logger.LogError(message: string.Join(" : ", "CreateMasterTransferPromotionItemFromMaterialAsync", ex?.InnerException?.Message ?? ex?.Message));
                    await tran.RollbackAsync();
                    throw ex;
                }
            }
        }

        /// <summary>
        /// สร้างรายการย่อย Master โปรโอน โดยใช้ Material ที่ดึงจาก SAP
        /// </summary>
        /// <returns>The master transfer promotion items.</returns>
        /// <param name="id">Identifier.</param>
        /// <param name="inputs">Inputs.</param>
        [HttpPost("{id}/Items/{masterTransferPromotionItemID}/SubItems")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<List<MasterTransferPromotionItemDTO>>))]
        public async Task<IActionResult> CreateSubMasterTransferPromotionItemFromMaterialAsync([FromRoute] Guid id, [FromRoute] Guid masterTransferPromotionItemID, [FromBody] List<PromotionMaterialDTO> inputs)
        {
            using (var tran = await DB.Database.BeginTransactionAsync())
            {
                try
                {
                    var results = await MasterTransferPromotionService.CreateSubMasterTransferPromotionItemFromMaterialAsync(id, masterTransferPromotionItemID, inputs);
                    await tran.CommitAsync();
                    return await _httpResultHelper.SuccessCustomResult(results, MasterTransferPromotionService.logModel);

                }
                catch (Exception ex)
                {
                    _logger.LogError(message: string.Join(" : ", "CreateSubMasterTransferPromotionItemFromMaterialAsync", ex?.InnerException?.Message ?? ex?.Message));
                    await tran.RollbackAsync();
                    throw ex;
                }
            }
        }

        /// <summary>
        /// ดึงแบบบ้านที่ผูกไว้กับรายการ Master โปรโอน
        /// </summary>
        /// <returns>The models of master transfer promotion item.</returns>
        /// <param name="id">Identifier.</param>
        /// <param name="masterTransferPromotionItemID">Master transfer promotion item identifier.</param>
        [HttpGet("{id}/Items/{masterTransferPromotionItemID}/Models")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<List<ModelListDTO>>))]
        public async Task<IActionResult> GetModelsOfMasterTransferPromotionItem([FromRoute] Guid id, [FromRoute] Guid masterTransferPromotionItemID)
        {
            var results = await MasterTransferPromotionService.GetMasterTransferPromotionItemModelListAsync(masterTransferPromotionItemID);
            return await _httpResultHelper.SuccessCustomResult(results, MasterTransferPromotionService.logModel);
        }

        /// <summary>
        /// ระบุแบบบ้าน
        /// </summary>
        /// <returns>The models to master transfer promotion item.</returns>
        /// <param name="id">Identifier.</param>
        /// <param name="masterTransferPromotionID">Master transfer promotion identifier.</param>
        /// <param name="inputs">Inputs.</param>
        [HttpPost("{id}/Items/{masterTransferPromotionItemID}/Models/Save")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<List<ModelListDTO>>))]
        public async Task<IActionResult> AddMasterTransferPromotionItemModelListAsync([FromRoute] Guid id, [FromRoute] Guid masterTransferPromotionItemID,
            [FromBody] List<ModelListDTO> inputs)
        {
            using (var tran = await DB.Database.BeginTransactionAsync())
            {
                try
                {
                    await MasterTransferPromotionService.AddMasterTransferPromotionItemModelListAsync(masterTransferPromotionItemID, inputs);
                    await tran.CommitAsync();
                    return Ok();
                }
                catch (Exception ex)
                {
                    _logger.LogError(message: string.Join(" : ", "AddMasterTransferPromotionItemModelListAsync", ex?.InnerException?.Message ?? ex?.Message));
                    await tran.RollbackAsync();
                    throw ex;
                }
            }
        }

        /// <summary>
        /// ดึงรายการที่ไม่ต้องจัดซื้อ
        /// </summary>
        /// <returns>The master transfer promotion free item list async.</returns>
        /// <param name="id">Identifier.</param>
        /// <param name="pageParam">Page parameter.</param>
        /// <param name="sortByParam">Sort by parameter.</param>
        [HttpGet("{id}/FreeItems")]
        [PagingResponseHeaders]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<List<MasterTransferPromotionFreeItemDTO>>))]
        public async Task<IActionResult> GetMasterTransferPromotionFreeItemListAsync([FromRoute] Guid id,
            [FromQuery] PageParam pageParam,
            [FromQuery] MasterTransferPromotionFreeItemSortByParam sortByParam, CancellationToken cancellationToken = default)
        {
            var result = await MasterTransferPromotionService.GetMasterTransferPromotionFreeItemListAsync(id, pageParam, sortByParam, cancellationToken);
            AddPagingResponse(result.PageOutput);
            return await _httpResultHelper.SuccessCustomResult(result.MasterTransferPromotionFreeItemDTOs, MasterTransferPromotionService.logModel);
        }

        /// <summary>
        /// สร้างรายการที่ไม่ต้องจัดซืื้อ
        /// </summary>
        /// <returns>The master transfer promotion free item.</returns>
        /// <param name="id">Identifier.</param>
        /// <param name="input">Input.</param>
        [HttpPost("{id}/FreeItems")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<MasterTransferPromotionFreeItemDTO>))]
        public async Task<IActionResult> CreateMasterTransferPromotionFreeItemAsync([FromRoute] Guid id,
            [FromBody] MasterTransferPromotionFreeItemDTO input)
        {
            using (var tran = await DB.Database.BeginTransactionAsync())
            {
                try
                {
                    await MasterTransferPromotionService.CreateMasterTransferPromotionFreeItemAsync(id, input);
                    await tran.CommitAsync();
                    return Ok();
                }
                catch (Exception ex)
                {
                    _logger.LogError(message: string.Join(" : ", "CreateMasterTransferPromotionFreeItemAsync", ex?.InnerException?.Message ?? ex?.Message));
                    await tran.RollbackAsync();
                    throw ex;
                }
            }
        }

        /// <summary>
        /// แก้ไขรายการที่ไม่ต้องจัดซื้อ
        /// </summary>
        /// <returns>The master transfer promotion free item list.</returns>
        /// <param name="id">Identifier.</param>
        /// <param name="masterTransferPromotionFreeItemID">Master transfer promotion free item identifier.</param>
        /// <param name="inputs">Inputs.</param>
        [HttpPut("{id}/FreeItems")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<List<MasterTransferPromotionFreeItemDTO>>))]
        public async Task<IActionResult> UpdateMasterTransferPromotionFreeItemListAsync([FromRoute] Guid id,
            [FromBody] List<MasterTransferPromotionFreeItemDTO> inputs)
        {
            using (var tran = await DB.Database.BeginTransactionAsync())
            {
                try
                {
                    await MasterTransferPromotionService.UpdateMasterTransferPromotionFreeItemListAsync(id, inputs);
                    await tran.CommitAsync();
                    return Ok();
                }
                catch (Exception ex)
                {
                    _logger.LogError(message: string.Join(" : ", "UpdateMasterTransferPromotionFreeItemListAsync", ex?.InnerException?.Message ?? ex?.Message));
                    await tran.RollbackAsync();
                    throw ex;
                }
            }
        }

        /// <summary>
        /// แก้ไขรายการที่ไม่ต้องจัดซื้อ (ทีละรายการ)
        /// </summary>
        /// <returns>The master Transfer promotion free item.</returns>
        /// <param name="id">Identifier.</param>
        /// <param name="masterTransferPromotionFreeItemID">Master Transfer promotion free item identifier.</param>
        /// <param name="input">Input.</param>
        [HttpPut("{id}/FreeItems/{masterTransferPromotionFreeItemID}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<MasterTransferPromotionFreeItemDTO>))]
        public async Task<IActionResult> UpdateMasterTransferPromotionFreeItemAsync([FromRoute] Guid id, [FromRoute] Guid masterTransferPromotionFreeItemID,
            [FromBody] MasterTransferPromotionFreeItemDTO input)
        {
            using (var tran = await DB.Database.BeginTransactionAsync())
            {
                try
                {
                    await MasterTransferPromotionService.UpdateMasterTransferPromotionFreeItemAsync(id, masterTransferPromotionFreeItemID, input);
                    await tran.CommitAsync();
                    return Ok();
                }
                catch (Exception ex)
                {
                    _logger.LogError(message: string.Join(" : ", "UpdateMasterTransferPromotionFreeItemAsync", ex?.InnerException?.Message ?? ex?.Message));
                    await tran.RollbackAsync();
                    throw ex;
                }
            }
        }

        /// <summary>
        /// ลบรายการที่ไม่ต้องจัดซื้อ
        /// </summary>
        /// <returns>The master transfer promotion free item.</returns>
        /// <param name="id">Identifier.</param>
        /// <param name="masterTransferPromotionFreeItemID">Master transfer promotion free item identifier.</param>
        [HttpDelete("{id}/FreeItems/{masterTransferPromotionFreeItemID}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<List<MasterTransferPromotionFreeItemDTO>>))]
        public async Task<IActionResult> DeleteMasterTransferPromotionFreeItemAsync([FromRoute] Guid id,
            [FromRoute] Guid masterTransferPromotionFreeItemID)
        {
            using (var tran = await DB.Database.BeginTransactionAsync())
            {
                try
                {
                    await MasterTransferPromotionService.DeleteMasterTransferPromotionFreeItemAsync(masterTransferPromotionFreeItemID);
                    await tran.CommitAsync();
                    return Ok();
                }
                catch (Exception ex)
                {
                    _logger.LogError(message: string.Join(" : ", "DeleteMasterTransferPromotionFreeItemAsync", ex?.InnerException?.Message ?? ex?.Message));
                    await tran.RollbackAsync();
                    throw ex;
                }
            }
        }

        /// <summary>
        /// ดึงแบบบ้านของรายการที่ไม่ต้องจัดซื้อ
        /// </summary>
        /// <returns>The models of master transfer promotion free item.</returns>
        /// <param name="id">Identifier.</param>
        /// <param name="masterTransferPromotionFreeItemID">Master transfer promotion free item identifier.</param>
        [HttpGet("{id}/FreeItems/{masterTransferPromotionFreeItemID}/Models")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<List<ModelListDTO>>))]
        public async Task<IActionResult> GetModelsOfMasterTransferPromotionFreeItem([FromRoute] Guid id, [FromRoute] Guid masterTransferPromotionFreeItemID, CancellationToken cancellationToken = default)
        {
            var results = await MasterTransferPromotionService.GetMasterTransferPromotionFreeItemModelListAsync(masterTransferPromotionFreeItemID, cancellationToken);

            return await _httpResultHelper.SuccessCustomResult(results, MasterTransferPromotionService.logModel);

        }

        /// <summary>
        /// ระบุแบบบ้านให้กับรายการที่ไม่ต้องจัดซื้อ
        /// </summary>
        /// <returns>The master transfer promotion free item model list async.</returns>
        /// <param name="id">Identifier.</param>
        /// <param name="masterTransferPromotionFreeItemID">Master transfer promotion free item identifier.</param>
        /// <param name="inputs">Inputs.</param>
        [HttpPost("{id}/FreeItems/{masterTransferPromotionFreeItemID}/Models/Save")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<List<ModelListDTO>>))]
        public async Task<IActionResult> AddMasterTransferPromotionFreeItemModelListAsync([FromRoute] Guid id, [FromRoute] Guid masterTransferPromotionFreeItemID,
            [FromBody] List<ModelListDTO> inputs)
        {
            using (var tran = await DB.Database.BeginTransactionAsync())
            {
                try
                {
                    await MasterTransferPromotionService.AddMasterTransferPromotionFreeItemModelListAsync(masterTransferPromotionFreeItemID, inputs);
                    await tran.CommitAsync();
                    return Ok();
                }
                catch (Exception ex)
                {
                    _logger.LogError(message: string.Join(" : ", "AddMasterTransferPromotionFreeItemModelListAsync", ex?.InnerException?.Message ?? ex?.Message));
                    await tran.RollbackAsync();
                    throw ex;
                }
            }
        }

        /// <summary>
        /// ดึงรายการโปรค่าธรรมเนียมรูดบัตร
        /// </summary>
        /// <returns>The master transfer credit card item async.</returns>
        /// <param name="id">Identifier.</param>
        /// <param name="pageParam">Page parameter.</param>
        /// <param name="sortByParam">Sort by parameter.</param>
        [HttpGet("{id}/CreditCardItems")]
        [PagingResponseHeaders]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<List<MasterTransferCreditCardItemDTO>>))]
        public async Task<IActionResult> GetMasterTransferCreditCardItemAsync([FromRoute] Guid id,
            [FromQuery] PageParam pageParam,
            [FromQuery] MasterTransferCreditCardItemSortByParam sortByParam, CancellationToken cancellationToken = default)
        {
            var result = await MasterTransferPromotionService.GetMasterTransferCreditCardItemAsync(id, pageParam, sortByParam, cancellationToken);

            AddPagingResponse(result.PageOutput);
            return await _httpResultHelper.SuccessCustomResult(result.MasterTransferCreditCardItemDTOs, MasterTransferPromotionService.logModel);

        }

        /// <summary>
        /// สร้างรายการโปรค่าธรรมเนียมรูดบัตร
        /// </summary>
        /// <returns>The master transfer credit card item list async.</returns>
        /// <param name="id">Identifier.</param>
        /// <param name="inputs">Master ค่าธรรมเนียมรูดบัตร</param>
        [HttpPost("{id}/CreditCardItems")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<List<MasterTransferCreditCardItemDTO>>))]
        public async Task<IActionResult> CreateMasterTransferCreditCardItemsAsync([FromRoute] Guid id,
            [FromBody] List<PromotionMaterialDTO> inputs)
        {
            using (var tran = await DB.Database.BeginTransactionAsync())
            {
                try
                {
                    await MasterTransferPromotionService.CreateMasterTransferCreditCardItemsAsync(id, inputs);
                    await tran.CommitAsync();
                    return Ok();
                }
                catch (Exception ex)
                {
                    _logger.LogError(message: string.Join(" : ", "CreateMasterTransferCreditCardItemsAsync", ex?.InnerException?.Message ?? ex?.Message));
                    await tran.RollbackAsync();
                    throw ex;
                }
            }
        }

        /// <summary>
        /// แก้ไขรายการโปรค่าธรรมเนียมรูดบัตร
        /// </summary>
        /// <returns>The master transfer credit card item list async.</returns>
        /// <param name="id">Identifier.</param>
        /// <param name="inputs">Inputs.</param>
        [HttpPut("{id}/CreditCardItems")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<List<MasterTransferCreditCardItemDTO>>))]
        public async Task<IActionResult> UpdateMasterTransferCreditCardItemListAsync([FromRoute] Guid id,
            [FromBody] List<MasterTransferCreditCardItemDTO> inputs)
        {
            using (var tran = await DB.Database.BeginTransactionAsync())
            {
                try
                {
                    await MasterTransferPromotionService.UpdateMasterTransferCreditCardItemListAsync(id, inputs);
                    await tran.CommitAsync();
                    return Ok();
                }
                catch (Exception ex)
                {
                    _logger.LogError(message: string.Join(" : ", "UpdateMasterTransferCreditCardItemListAsync", ex?.InnerException?.Message ?? ex?.Message));
                    await tran.RollbackAsync();
                    throw ex;
                }
            }
        }

        /// <summary>
        /// แก้ไขรายการโปรค่าธรรมเนียมรูดบัตร (ทีละรายการ)
        /// </summary>
        /// <returns>The master Transfer credit card item list async.</returns>
        /// <param name="id">Identifier.</param>
        /// <param name="masterTransferCreditCardItemID">Master Transfer credit card item identifier.</param>
        /// <param name="input">Input.</param>
        [HttpPut("{id}/CreditCardItems/{masterTransferCreditCardItemID}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<MasterTransferCreditCardItemDTO>))]
        public async Task<IActionResult> UpdateMasterTransferCreditCardItemAsync([FromRoute] Guid id, [FromRoute] Guid masterTransferCreditCardItemID,
            [FromBody] MasterTransferCreditCardItemDTO input)
        {
            using (var tran = await DB.Database.BeginTransactionAsync())
            {
                try
                {
                    await MasterTransferPromotionService.UpdateMasterTransferCreditCardItemAsync(id, masterTransferCreditCardItemID, input);
                    await tran.CommitAsync();
                    return Ok();
                }
                catch (Exception ex)
                {
                    _logger.LogError(message: string.Join(" : ", "UpdateMasterTransferCreditCardItemAsync", ex?.InnerException?.Message ?? ex?.Message));
                    await tran.RollbackAsync();
                    throw ex;
                }
            }
        }

        /// <summary>
        /// ลบรายการโปรค่าธรรมเนียมรูดบัตร
        /// </summary>
        /// <returns>The master transfer credit card item list async.</returns>
        /// <param name="id">Identifier.</param>
        /// <param name="masterTransferCreditCardItemID">Master transfer credit card item identifier.</param>
        [HttpDelete("{id}/CreditCardItems/{masterTransferCreditCardItemID}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<List<MasterTransferCreditCardItemDTO>>))]
        public async Task<IActionResult> DeleteMasterTransferCreditCardItemAsync([FromRoute] Guid id,
            [FromRoute] Guid masterTransferCreditCardItemID)
        {
            using (var tran = await DB.Database.BeginTransactionAsync())
            {
                try
                {
                    await MasterTransferPromotionService.DeleteMasterTransferCreditCardItemAsync(masterTransferCreditCardItemID);
                    await tran.CommitAsync();
                    return Ok();
                }
                catch (Exception ex)
                {
                    _logger.LogError(message: string.Join(" : ", "DeleteMasterTransferCreditCardItemAsync", ex?.InnerException?.Message ?? ex?.Message));
                    await tran.RollbackAsync();
                    throw ex;
                }
            }
        }

        /// <summary>
        /// Clone โปรโอน
        /// </summary>
        /// <returns>The master transfer promotion async.</returns>
        /// <param name="id">Identifier.</param>
        [HttpPost("{id}/Clone")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<MasterTransferPromotionDTO>))]
        public async Task<IActionResult> CloneMasterTransferPromotionAsync([FromRoute] Guid id)
        {
            using (var tran = await DB.Database.BeginTransactionAsync())
            {
                try
                {
                    var result = await MasterTransferPromotionService.CloneMasterTransferPromotionAsync(id);
                    await tran.CommitAsync();
                    return await _httpResultHelper.SuccessCustomResult(result, MasterTransferPromotionService.logModel);
                }
                catch (Exception ex)
                {
                    _logger.LogError(message: string.Join(" : ", "CloneMasterTransferPromotionAsync", ex?.InnerException?.Message ?? ex?.Message));
                    await tran.RollbackAsync();
                    throw ex;
                }
            }
        }

        /// <summary>
        /// ดึงจำนวน Item ที่ Clone ได้หรือ Expired ไปแล้ว
        /// </summary>
        /// <returns>The clone master transfer promotion confirm async.</returns>
        /// <param name="id">Identifier.</param>
        [HttpGet("{id}/CloneConfirmPopup")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<CloneMasterPromotionConfirmDTO>))]
        public async Task<IActionResult> GetCloneMasterTransferPromotionConfirmAsync([FromRoute] Guid id, CancellationToken cancellationToken = default)
        {
            var result = await MasterTransferPromotionService.GetCloneMasterTransferPromotionConfirmAsync(id, cancellationToken);
            return await _httpResultHelper.SuccessCustomResult(result, MasterTransferPromotionService.logModel);
        }

    }
}
