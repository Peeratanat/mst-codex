using Base.DTOs.Common;
using Base.DTOs.SAL;
using Common.Helper.HttpResultHelper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PagingExtensions;
using PRJ_UnitInfos.Params.Filters;
using PRJ_UnitInfos.Params.Outputs;
using PRJ_UnitInfos.Services;

namespace PRJ_UnitInfos.API.Controllers
{
#if !DEBUG
    [Authorize]
#endif
    [Route(HelperStatic.BaseAPIRoute)]
    [ApiController]
    public class UnitInfosController : BaseController
    {
        private IUnitInfoService UnitInfoService;
        private readonly IHttpResultHelper _httpResultHelper;

        public UnitInfosController(IUnitInfoService unitInfoService, IHttpResultHelper httpResultHelper)
        {
            this.UnitInfoService = unitInfoService;
            _httpResultHelper = httpResultHelper;
        }

        /// <summary>
        /// ดึงรายการสถานะแปลง
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="pageParam"></param>
        /// <param name="sortByParam"></param>
        /// <returns></returns>
        [HttpGet]
        [PagingResponseHeaders]
        [ProducesResponseType(200, Type = typeof(ResponseModel<List<UnitInfoListDTO>>))]
        public async Task<IActionResult> GetUnitInfoListAsync([FromQuery] UnitInfoListFilter filter, [FromQuery] PageParam pageParam, [FromQuery] UnitInfoListSortByParam sortByParam, CancellationToken cancellationToken = default)
        {
            var result = await UnitInfoService.GetUnitInfoListAsync(filter, pageParam, sortByParam,cancellationToken);
            AddPagingResponse(result.PageOutput);
            return await _httpResultHelper.SuccessCustomResult(result.Units, UnitInfoService.logModel);
        }
        /// <summary>
        /// ดึงรายการสถานะแปลงจาก contact
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="pageParam"></param>
        /// <param name="sortByParam"></param>
        /// <returns></returns>
        [HttpGet("Contact")]
        [PagingResponseHeaders]
        [ProducesResponseType(200, Type = typeof(ResponseModel<UnitInfoListPagingByContact>))]
        public async Task<IActionResult> GetUnitInfoListByContactAsync([FromQuery] UnitInfoListFilterByContact filter, [FromQuery] PageParam pageParam, [FromQuery] UnitInfoListSortByParam sortByParam, CancellationToken cancellationToken = default)
        {
            var result = await UnitInfoService.GetUnitInfoListByContactAsync(filter, pageParam, sortByParam,cancellationToken);
            AddPagingResponse(result.PageOutput);
            return await _httpResultHelper.SuccessCustomResult(result.Units, UnitInfoService.logModel);
        }

        /// <summary>
        /// ดึงรายละเอียดแปลงของหน้าสถานะแปลง
        /// </summary>
        /// <param name="unitID"></param>
        /// <returns></returns>
        [HttpGet("{unitID}")]
        [ProducesResponseType(200, Type = typeof(ResponseModel<UnitInfoDTO>))]
        public async Task<IActionResult> GetUnitInfoAsync([FromRoute] Guid unitID, CancellationToken cancellationToken = default)
        {
            var result = await UnitInfoService.GetUnitInfoAsync(unitID,cancellationToken);
            return await _httpResultHelper.SuccessCustomResult(result, UnitInfoService.logModel);
        }

        /// <summary>
        /// ดึงรายละเอียดแปลงของหน้าสถานะแปลง สำหรับหน้าจ่ายเงิน
        /// </summary>
        /// <param name="unitID"></param>
        /// <returns></returns>
        [HttpGet("ForPayment/{unitID}")]
        [ProducesResponseType(200, Type = typeof(ResponseModel<UnitInfoDTO>))]
        public async Task<IActionResult> GetUnitInfoForPaymentAsync([FromRoute] Guid unitID, CancellationToken cancellationToken = default)
        {
            var result = await UnitInfoService.GetUnitInfoForPaymentAsync(unitID,cancellationToken);
            return await _httpResultHelper.SuccessCustomResult(result, UnitInfoService.logModel);

        }

        /// <summary>
        /// ดึงรายละเอียดโปรก่อนขาย
        /// </summary>
        /// <param name="unitID"></param>
        /// <returns></returns>
        [HttpGet("{unitID}/PreSalePromotions")]
        [ProducesResponseType(200, Type = typeof(ResponseModel<UnitInfoPreSalePromotionDTO>))]
        public async Task<IActionResult> GetUnitInfoPreSalePromotionAsync([FromRoute] Guid unitID, CancellationToken cancellationToken = default)
        {
            var result = await UnitInfoService.GetUnitInfoPreSalePromotionAsync(unitID,cancellationToken);
            return await _httpResultHelper.SuccessCustomResult(result, UnitInfoService.logModel);
        }

        /// <summary>
        /// ดึงรายละเอีลดโปรขาย
        /// </summary>
        /// <param name="unitID"></param>
        /// <returns></returns>
        [HttpGet("{unitID}/SalePromotions")]
        [ProducesResponseType(200, Type = typeof(ResponseModel<UnitInfoSalePromotionDTO>))]
        public async Task<IActionResult> GetUnitInfoSalePromotionAsync([FromRoute] Guid unitID, CancellationToken cancellationToken = default)
        {
            var result = await UnitInfoService.GetUnitInfoSalePromotionAsync(unitID,cancellationToken);
            return await _httpResultHelper.SuccessCustomResult(result, UnitInfoService.logModel);
        }

        /// <summary>
        /// ดึงรายการค่าใช้จ่าย
        /// </summary>
        /// <param name="unitID"></param>
        /// <returns></returns>
        [HttpGet("{unitID}/PromotionExpenses")]
        [ProducesResponseType(200, Type = typeof(ResponseModel<List<UnitInfoSalePromotionExpenseDTO>>))]
        public async Task<IActionResult> GetUnitInfoPromotionExpensesAsync([FromRoute] Guid unitID, CancellationToken cancellationToken = default)
        {
            var results = await UnitInfoService.GetUnitInfoPromotionExpensesAsync(unitID,cancellationToken);
            return await _httpResultHelper.SuccessCustomResult(results, UnitInfoService.logModel);
        }

        /// <summary>
        /// ดึงข้อมูลราคา
        /// </summary>
        /// <param name="unitID"></param>
        /// <returns></returns>
        [HttpGet("{unitID}/PriceLists")]
        [ProducesResponseType(200, Type = typeof(ResponseModel<UnitInfoPriceListDTO>))]
        public async Task<IActionResult> GetPriceListAsync([FromRoute] Guid unitID, CancellationToken cancellationToken = default)
        {
            var result = await UnitInfoService.GetPriceListAsync(unitID,cancellationToken);
            return await _httpResultHelper.SuccessCustomResult(result, UnitInfoService.logModel);
        }

        /// <summary>
        /// ดึงข้อมูลจำนวนแปลง
        /// </summary>
        /// <returns></returns>
        [HttpGet("Count")]
        [ProducesResponseType(200, Type = typeof(ResponseModel<UnitInfoStatusCountDTO>))]
        public async Task<IActionResult> GetUnitInfoCount([FromQuery] Guid? projectID, CancellationToken cancellationToken = default)
        {
            var result = await UnitInfoService.GetUnitInfoCountAsync(projectID,cancellationToken);
            return await _httpResultHelper.SuccessCustomResult(result, UnitInfoService.logModel);
        }

        /// <summary>
        /// เช็คแปลงว่า Prebook หรือไม่
        /// </summary>
        /// <returns></returns>
        [HttpGet("CheckPrebook")]
        public async Task<IActionResult> CheckPrebook([FromQuery] Guid? unitID)
        {
            var result = await UnitInfoService.CheckPrebookAsync(unitID);
            return await _httpResultHelper.SuccessCustomResult(result, UnitInfoService.logModel);
        }
    }
}
