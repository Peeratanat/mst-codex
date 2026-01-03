using System.Net;
using Database.Models;
using Microsoft.AspNetCore.Mvc;
using PagingExtensions;
using Common.Helper.HttpResultHelper;
using Base.DTOs.Common;
using PRJ_Project.Services;
using Base.DTOs.PRJ;
using PRJ_Project.Params.Filters;
using Microsoft.AspNetCore.Authorization;
namespace PRJ_Project.API.Controllers
{
#if !DEBUG
     [Authorize(Policy = "MultiSchemePolicy")]
#endif
    [Route(HelperStatic.BaseAPIRouteController)]
    [ApiController]
    public class ModelsController : BaseController
    {
        private IModelService ModelService;
        private readonly DatabaseContext DB;
        private readonly ILogger<ModelsController> _logger;
        private readonly IHttpResultHelper _httpResultHelper;

        public ModelsController(
            DatabaseContext db,
            ILogger<ModelsController> logger,
            IHttpResultHelper httpResultHelper,
            IModelService modelService)
        {
            DB = db;
            _logger = logger;
            _httpResultHelper = httpResultHelper;
            ModelService = modelService;
        }
        /// <summary>
        /// ลิสข้อมูลแบบบ้าน dropdown
        /// </summary>
        /// <param name="projectID"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpGet("{projectID}/Models/DropdownList")]
        [ProducesResponseType(typeof(ResponseModel<List<ModelDropdownDTO>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetModelDropdownListAsync([FromRoute] Guid projectID, [FromQuery] string name, CancellationToken cancellationToken = default)
        {
            var result = await ModelService.GetModelDropdownListAsync(projectID, name, cancellationToken);
            return await _httpResultHelper.SuccessCustomResult(result, ModelService.logModel);
        }
        /// <summary>
        /// ลิสข้อมูลแบบบ้าน
        /// </summary>
        /// <param name="projectID"></param>
        /// <param name="filter"></param>
        /// <param name="pageParam"></param>
        /// <param name="sortByParam"></param>
        /// <returns></returns>
        [HttpGet("{projectID}/Models")]
        [PagingResponseHeaders]
        [ProducesResponseType(typeof(ResponseModel<List<ModelListDTO>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetModelListAsync([FromRoute] Guid projectID,
         [FromQuery] ModelsFilter filter,
         [FromQuery] PageParam pageParam,
         [FromQuery] ModelListSortByParam sortByParam, CancellationToken cancellationToken = default)
        {
            var result = await ModelService.GetModelListAsync(projectID, filter, pageParam, sortByParam, cancellationToken);
            AddPagingResponse(result.PageOutput);
            return await _httpResultHelper.SuccessCustomResult(result.Models, ModelService.logModel);
        }
        /// <summary>
        /// ลิสข้อมูลแบบบ้านทั้งหมด
        /// </summary>
        /// <param name="projectID"></param>
        /// <param name="filter"></param>
        /// <param name="pageParam"></param>
        /// <param name="sortByParam"></param>
        /// <returns></returns>
        [HttpGet("{projectID}/Models/All")]
        [PagingResponseHeaders]
        [ProducesResponseType(typeof(ResponseModel<List<ModelListDTO>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetModelListAllAsync([FromRoute] Guid projectID, [FromQuery] ModelsFilter filter, [FromQuery] PageParam pageParam, [FromQuery] ModelListSortByParam sortByParam, CancellationToken cancellationToken = default)
        {
            var result = await ModelService.GetModelListAllAsync(projectID, filter, pageParam, sortByParam, cancellationToken);
            AddPagingResponse(result.PageOutput);
            return await _httpResultHelper.SuccessCustomResult(result.Models, ModelService.logModel);

        }
        /// <summary>
        /// ข้อมูลแบบบ้าน
        /// </summary>
        /// <param name="projectID"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{projectID}/Models/{id}")]
        [ProducesResponseType(typeof(ResponseModel<ModelDTO>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetModelAsync([FromRoute] Guid projectID, [FromRoute] Guid id, CancellationToken cancellationToken = default)
        {
            var result = await ModelService.GetModelAsync(projectID, id, cancellationToken);
            return await _httpResultHelper.SuccessCustomResult(result, ModelService.logModel);
        }
        /// <summary>
        /// สร้างข้อมูลแบบบ้าน
        /// </summary>
        /// <param name="projectID"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("{projectID}/Models")]
        [ProducesResponseType(typeof(ResponseModel<ModelDTO>), StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateModelAsync([FromRoute] Guid projectID, [FromBody] ModelDTO input)
        {
            using (var tran = await DB.Database.BeginTransactionAsync())
            {
                try
                {
                    var result = await ModelService.CreateModelAsync(projectID, input);
                    await tran.CommitAsync();
                    return await _httpResultHelper.SuccessCustomResult(result, ModelService.logModel,HttpStatusCode.Created);
                }
                catch (Exception ex)
                {
                    _logger.LogError(message: string.Join(" : ", "CreateModelAsync", ex?.InnerException?.Message ?? ex?.Message));
                    await tran.RollbackAsync();
                    throw ex;
                }
            }
        }

        /// <summary>
        /// แก้ไขข้อมูลแบบบ้าน
        /// </summary>
        /// <param name="projectID"></param>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut("{projectID}/Models/{id}")]
        [ProducesResponseType(typeof(ResponseModel<ModelDTO>), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateModelAsync([FromRoute] Guid projectID, [FromRoute] Guid id, [FromBody] ModelDTO input)
        {
            using (var tran = await DB.Database.BeginTransactionAsync())
            {
                try
                {
                    var result = await ModelService.UpdateModelAsync(projectID, id, input);
                    await tran.CommitAsync();
                    return await _httpResultHelper.SuccessCustomResult(result, ModelService.logModel);
                }
                catch (Exception ex)
                {
                    _logger.LogError(message: string.Join(" : ", "UpdateModelAsync", ex?.InnerException?.Message ?? ex?.Message));
                    await tran.RollbackAsync();
                    throw ex;
                }
            }
        }
        /// <summary>
        /// แก้ไขข้อมูลแบบบ้าน List
        /// </summary>
        /// <param name="projectID"></param>
        /// <param name="inputs"></param>
        /// <returns></returns>
        [HttpPut("{projectID}/Models/All")]
        [ProducesResponseType(typeof(ResponseModel<ModelDTO>), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateModelListAsync([FromRoute] Guid projectID, [FromBody] ModelInput inputs)
        {
            using (var tran = await DB.Database.BeginTransactionAsync())
            {
                try
                {
                    var result = await ModelService.UpdateModelListAsync(projectID, inputs.Items, inputs.ModelId);
                    await tran.CommitAsync();
                    return await _httpResultHelper.SuccessCustomResult(result, ModelService.logModel);
                }
                catch (Exception ex)
                {
                    _logger.LogError(message: string.Join(" : ", "UpdateModelListAsync", ex?.InnerException?.Message ?? ex?.Message));
                    await tran.RollbackAsync();
                    throw ex;
                }
            }
        }
        /// <summary>
        /// ลบข้อมูลแบบบ้าน
        /// </summary>
        /// <param name="projectID"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{projectID}/Models/{id}")]
        public async Task<IActionResult> DeleteModelAsync([FromRoute] Guid projectID, [FromRoute] Guid id)
        {
            using (var tran = await DB.Database.BeginTransactionAsync())
            {
                try
                {
                    var result = await ModelService.DeleteModelAsync(projectID, id);
                    await tran.CommitAsync();
                    return Ok();
                }
                catch (Exception ex)
                {
                    _logger.LogError(message: string.Join(" : ", "DeleteModelAsync", ex?.InnerException?.Message ?? ex?.Message));
                    await tran.RollbackAsync();
                    throw ex;
                }
            }
        }


    }
}