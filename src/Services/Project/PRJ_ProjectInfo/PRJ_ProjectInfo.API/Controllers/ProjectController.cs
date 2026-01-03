using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using PRJ_ProjectInfo.Services;
using Common.Helper.HttpResultHelper;
using PRJ_ProjectInfo.Params.Outputs;
using Database.Models.PRJ;
using PagingExtensions;
using Base.DTOs.Common;
using Base.DTOs.PRJ;
using Database.Models;


namespace PRJ_ProjectInfo.API.Controllers
{
#if !DEBUG
    [Authorize]
#endif
    [Route(HelperStatic.BaseAPIRoute)]
    [ApiController]
    public class ProjectController : BaseController
    {
        private readonly DatabaseContext DB;
        private readonly IProjectService _ProjectService;
        private readonly ILogger<ProjectController> _logger;
        private readonly IProjectInfoService IProjectInfoService;
        public bool CheckVerifyHeader = true;

        private readonly IHttpResultHelper _httpResultHelper;

        public ProjectController(IProjectService projectService, IHttpResultHelper httpResultHelper, IProjectInfoService iProjectInfoService, DatabaseContext dB, ILogger<ProjectController> logger)
        {
            _ProjectService = projectService;
            _httpResultHelper = httpResultHelper;
            IProjectInfoService = iProjectInfoService;
            DB = dB;
            _logger = logger;
        }

        /// <summary>
        /// ข้อมูลโครงการ
        /// </summary>
        /// <returns>The project info.</returns>
        /// <param name="projectID">Project identifier.</param>
        [HttpGet("{projectID}/Info")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<ProjectInfoDTO>))]
        public async Task<IActionResult> GetProjectInfo([FromRoute] Guid projectID, CancellationToken cancellationToken = default)
        {
            var result = await IProjectInfoService.GetProjectInfoAsync(projectID, cancellationToken);
            return await _httpResultHelper.SuccessCustomResult(result, IProjectInfoService.logModel);
        }
        /// <summary>
        /// แก้ไขข้อมูลโครงการ
        /// </summary>
        [HttpPut("{projectID}/Info")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<ProjectInfoDTO>))]
        public async Task<IActionResult> EditProjectInfo([FromRoute] Guid projectID, [FromBody] ProjectInfoDTO input)
        {
            using (var tran = await DB.Database.BeginTransactionAsync())
            {
                try
                {
                    Guid? userID = null;
                    Guid parsedUserID;
                    if (Guid.TryParse(HttpContext?.User?.Claims.SingleOrDefault(x => x.Type == "userid")?.Value, out parsedUserID))
                        userID = parsedUserID;

                    var result = await IProjectInfoService.UpdateProjectInfoAsync(projectID, input, userID);
                    await tran.CommitAsync();
                    return await _httpResultHelper.SuccessCustomResult(result, IProjectInfoService.logModel);
                }
                catch (Exception ex)
                {
                    _logger.LogError(message: string.Join(" : ", "UpdateLockFloorAsync", ex?.InnerException?.Message ?? ex?.Message));
                    await tran.RollbackAsync();
                    throw ex;
                }
            }

        }

        [HttpGet]
        [Route("GetProjectInformationList")]
        [SwaggerOperation(Summary = "เรียกดูข้อมูล Project", Description = "")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<ProjectInformationPaging.Result>))]
        public async Task<IActionResult> GetProjectInformationListAsync([FromQuery] ProjectInformationPaging.Filter filter, [FromQuery] ProjectInformationPaging.SortByParam sortByParam, [FromQuery] PageParam pageParam, CancellationToken cancellationToken = default)
        {
            var _data = await _ProjectService.GetProjectInformationListAsync(filter, sortByParam, pageParam, cancellationToken);
            AddPagingResponse(_data.PageResult);
            return await _httpResultHelper.SuccessCustomResult(_data.DataResult, _ProjectService.logModel);
        }

        [HttpGet]
        [Route("GetProjectInformationList_HealthCheck")]
        [SwaggerOperation(Summary = "เรียกดูข้อมูล Project (HealthCheck)", Description = "")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<ProjectInformationPaging.Result>))]
        public async Task<IActionResult> GetProjectInformationList_HealthCheck(CancellationToken cancellationToken = default)
        {

            ProjectInformationPaging.Filter filter = new();
            ProjectInformationPaging.SortByParam sortByParam = new();
            PageParam pageParam = new() { Page = 1, PageSize = 10 };
            var _data = await _ProjectService.GetProjectInformationListAsync(filter, sortByParam, pageParam, cancellationToken);
            AddPagingResponse(_data.PageResult);
            return await _httpResultHelper.SuccessCustomResult(_data.DataResult, _ProjectService.logModel);
        }


        [HttpGet]
        [Route("GetProjectInformationDetail")]
        [SwaggerOperation(Summary = "เรียกดูข้อมูล Project", Description = "")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<ProjectInformationModel.ResultProjectInformation>))]
        public async Task<IActionResult> GetProjectInformationDetailAsync([FromQuery] Guid ProjectID, CancellationToken cancellationToken = default)
        {
            var _data = await _ProjectService.GetProjectInformationDetailAsync(ProjectID, cancellationToken);
            return await _httpResultHelper.SuccessCustomResult(_data, _ProjectService.logModel);
        }



        [HttpGet]
        [Route("DropdownList/ProjectBrand")]
        [SwaggerOperation(Summary = "ProjectBrandDropdownList", Description = "")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<List<DropdownListModel>>))]
        public async Task<IActionResult> GetProjectBrandDDLAsync([FromQuery] string TestSearch, CancellationToken cancellationToken = default)
        {
            var _data = await _ProjectService.GetProjectBrandDDLAsync(null, TestSearch, cancellationToken);
            return await _httpResultHelper.SuccessCustomResult(_data, _ProjectService.logModel);
        }


        [HttpGet]
        [Route("DropdownList/ProjectType")]
        [SwaggerOperation(Summary = "ProjectBrandDropdownList", Description = "")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<List<DropdownListModel>>))]
        public async Task<IActionResult> GetProjectTypeDDL([FromQuery] string TestSearch)
        {
            var _data = _ProjectService.GetProjectTypeDDL(TestSearch);
            return await _httpResultHelper.SuccessCustomResult(_data, _ProjectService.logModel);
        }


        [HttpGet]
        [Route("DropdownList/ProjectStatus")]
        [SwaggerOperation(Summary = "ProjectStatusDropdownList", Description = "")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<List<DropdownListModel>>))]
        public async Task<IActionResult> GetProjectStatusDDLAsync([FromQuery] string TestSearch, CancellationToken cancellationToken = default)
        {
            var _data = await _ProjectService.GetProjectStatusDDLAsync(TestSearch, cancellationToken);
            return await _httpResultHelper.SuccessCustomResult(_data, _ProjectService.logModel);
        }


        [HttpGet]
        [Route("DropdownList/ProjectZone")]
        [SwaggerOperation(Summary = "ProjectZoneDropdownList", Description = "")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<List<DropdownListModel>>))]
        public async Task<IActionResult> GetProjectZoneDDLAsync([FromQuery] string TestSearch, CancellationToken cancellationToken = default)
        {
            var _data = await _ProjectService.GetProjectZoneDDLAsync(TestSearch, cancellationToken);
            return await _httpResultHelper.SuccessCustomResult(_data, _ProjectService.logModel);
        }

        [HttpPost]
        [Route("UpdateAdminDescription")]
        [SwaggerOperation(Summary = "UpdateAdminDescription", Description = "")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<bool>))]
        public async Task<IActionResult> UpdateProjectAdminDescriptionAsync([FromBody] UpdateProjectInfoModel Input)
        {
            var result = await _ProjectService.UpdateProjectAdminDescriptionAsync(Input);
            return await _httpResultHelper.SuccessCustomResult(result, _ProjectService.logModel);
        }
    }
}