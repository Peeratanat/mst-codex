using System.Net;
using Base.DTOs;
using Database.Models;
using Microsoft.AspNetCore.Mvc;
using PagingExtensions;
using Common.Helper.HttpResultHelper;
using Base.DTOs.Common;
using PRJ_Project.Services;
using Base.DTOs.PRJ;
using Microsoft.AspNetCore.Authorization;

namespace PRJ_Project.API.Controllers
{
#if !DEBUG
     [Authorize(Policy = "MultiSchemePolicy")]
#endif
    [Route(HelperStatic.BaseAPIRouteController)]
    [ApiController]
    public class AddressesController : BaseController
    {
        private IAddressService ProjectAddressService;
        private readonly DatabaseContext DB;
        private readonly ILogger<AddressesController> _logger;
        private readonly IHttpResultHelper _httpResultHelper;

        public AddressesController(
            IAddressService projectService,
            DatabaseContext db,
            ILogger<AddressesController> logger,
            IHttpResultHelper httpResultHelper)
        {
            ProjectAddressService = projectService;
            DB = db;
            _logger = logger;
            _httpResultHelper = httpResultHelper;
        }

        /// <summary>
        /// ลิสข้อมูลที่ตั้ง Dropdown
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpGet("{projectID}/Addresses/DropdownList")]
        [ProducesResponseType(typeof(ResponseModel<List<ProjectAddressListDTO>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetProjectAddressDropdownListAsync([FromRoute] Guid projectID,
         [FromQuery] string name,
         [FromQuery] string projectAddressTypeKey,
         CancellationToken cancellationToken = default)
        {
            var result = await ProjectAddressService.GetProjectAddressDropdownListAsync(projectID, name, projectAddressTypeKey, cancellationToken);
            return await _httpResultHelper.SuccessCustomResult(result, ProjectAddressService.logModel);
        }
        /// <summary>
        /// ลิส ข้อมูลที่ตั้งโครงการ
        /// </summary>
        /// <param name="projectID"></param>
        /// <param name="pageParam"></param>
        /// <param name="sortByParam"></param>
        /// <returns></returns>
        [HttpGet("{projectID}/Addresses")]
        [PagingResponseHeaders]
        [ProducesResponseType(typeof(ResponseModel<List<ProjectAddressListDTO>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetProjectAddressListAsync([FromRoute] Guid projectID,
            [FromQuery] PageParam pageParam,
            [FromQuery] SortByParam sortByParam,
            CancellationToken cancellationToken = default)
        {
            var result = await ProjectAddressService.GetProjectAddressListAsync(projectID, pageParam, sortByParam, cancellationToken);
            AddPagingResponse(result.PageOutput);
            return await _httpResultHelper.SuccessCustomResult(result.ProjectAddresses, ProjectAddressService.logModel);
        }
        /// <summary>
        /// ข้อมูลที่ตั้งโครงการ
        /// </summary>
        /// <param name="projectID"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{projectID}/Addresses/{id}")]
        [ProducesResponseType(typeof(ResponseModel<ProjectAddressDTO>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetProjectAddressAsync(
         [FromRoute] Guid id,
         CancellationToken cancellationToken = default)
        {
            var result = await ProjectAddressService.GetProjectAddressAsync(id, cancellationToken);
            return await _httpResultHelper.SuccessCustomResult(result, ProjectAddressService.logModel);
        }
        /// <summary>
        /// สร้าง ข้อมูลที่ตั้งโครงการ
        /// </summary>
        /// <param name="projectID"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("{projectID}/Addresses")]
        [ProducesResponseType(typeof(ResponseModel<ProjectAddressDTO>), StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateProjectAddressAsync([FromRoute] Guid projectID, [FromBody] ProjectAddressDTO input)
        {
            using var tran = await DB.Database.BeginTransactionAsync();
            try
            {
                var result = await ProjectAddressService.CreateProjectAddressAsync(projectID, input);
                await tran.CommitAsync();
                return await _httpResultHelper.SuccessCustomResult(result, ProjectAddressService.logModel, HttpStatusCode.Created);
            }
            catch (Exception ex)
            {
                _logger.LogError(message: string.Join(" : ", "CreateProjectAddressAsync", ex?.InnerException?.Message ?? ex?.Message));
                await tran.RollbackAsync();
                throw ex;
            }
        }
        /// <summary>
        /// แก้ไข ข้อมูลที่ตั้งโครงการ
        /// </summary>
        /// <param name="projectID"></param>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut("{projectID}/Addresses/{id}")]
        [ProducesResponseType(200, Type = typeof(ProjectAddressDTO))]
        public async Task<IActionResult> UpdateProjectAddressAsync([FromRoute] Guid projectID, [FromRoute] Guid id, [FromBody] ProjectAddressDTO input)
        {
            using var tran = await DB.Database.BeginTransactionAsync();
            try
            {
                var result = await ProjectAddressService.UpdateProjectAddressAsync(projectID, id, input);
                await tran.CommitAsync();
                return await _httpResultHelper.SuccessCustomResult(result, ProjectAddressService.logModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(message: string.Join(" : ", "UpdateProjectAddressAsync", ex?.InnerException?.Message ?? ex?.Message));
                await tran.RollbackAsync();
                throw ex;
            }
        }
        /// <summary>
        /// ลบ ข้อมูลที่ตั้งโครงการ
        /// </summary>
        /// <param name="projectID"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{projectID}/Addresses/{id}")]
        public async Task<IActionResult> DeleteProjectAddressAsync([FromRoute] Guid projectID, [FromRoute] Guid id)
        {
            using var tran = await DB.Database.BeginTransactionAsync();
            try
            {
                await ProjectAddressService.DeleteProjectAddressAsync(id);
                await tran.CommitAsync();
                return await _httpResultHelper.SuccessCustomResult(null, ProjectAddressService.logModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(message: string.Join(" : ", "DeleteProjectAddressAsync", ex?.InnerException?.Message ?? ex?.Message));
                await tran.RollbackAsync();
                throw ex;
            }
        }


    }
}