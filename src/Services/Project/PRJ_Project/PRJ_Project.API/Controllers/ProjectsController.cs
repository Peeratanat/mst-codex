using Base.DTOs.Common;
using Base.DTOs.MST;
using Base.DTOs.PRJ;
using Common.Helper.HttpResultHelper;
using Database.Models;
using Microsoft.AspNetCore.Mvc;
using PagingExtensions;
using PRJ_Project.Params.Filters;
using PRJ_Project.Services;
using Report.Integration;
using Microsoft.AspNetCore.Authorization;
using System.Net;

namespace PRJ_Project.API.Controllers
{
#if !DEBUG
     [Authorize(Policy = "MultiSchemePolicy")]
#endif
    [Route(HelperStatic.BaseAPIRouteController)]
    [ApiController]
    public class ProjectsController : BaseController
    {
        private IProjectService ProjectService;
        private readonly DatabaseContext DB;
        private readonly ILogger<ProjectsController> _logger;
        private readonly IHttpResultHelper _httpResultHelper;

        public ProjectsController(
            IProjectService projectService,
            DatabaseContext db,
            ILogger<ProjectsController> logger,
            IHttpResultHelper httpResultHelper)
        {
            ProjectService = projectService;
            DB = db;
            _logger = logger;
            _httpResultHelper = httpResultHelper;
        }
        /// <summary>
        /// ดึงข้อมูลสถานะของแต่ละ Tab ในหน้าข้อมูลโครงการ
        /// </summary>
        /// <param name="projectID"></param>
        /// <returns></returns>
        [HttpGet("{projectID}/DataStatus")]
        [ProducesResponseType(typeof(ResponseModel<ProjectDataStatusDTO>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetProjectDataStatusAsync([FromRoute] Guid projectID, CancellationToken cancellationToken = default)
        {
            var result = await ProjectService.GetProjectDataStatusAsync(projectID, cancellationToken);
            return await _httpResultHelper.SuccessCustomResult(result, ProjectService.logModel);
        }

        /// <summary>
        /// ลิสข้อมูลโครงการ
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="pageParam"></param>
        /// <param name="sortByParam"></param>
        /// <returns></returns>
        [HttpGet]
        [PagingResponseHeaders]
        [ProducesResponseType(typeof(ResponseModel<List<ProjectDTO>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetProjectListAsync([FromQuery] ProjectsFilter filter, [FromQuery] PageParam pageParam, [FromQuery] ProjectSortByParam sortByParam, CancellationToken cancellationToken = default)
        {
            try
            {
                Guid? userID = null;
                Guid parsedUserID;
                if (Guid.TryParse(HttpContext?.User?.Claims.SingleOrDefault(x => x.Type == "userid")?.Value, out parsedUserID))
                {
                    userID = parsedUserID;
                }

                filter.UserID = userID;

                var result = await ProjectService.GetProjectListAsync(filter, pageParam, sortByParam, cancellationToken);

                AddPagingResponse(result.PageOutput);

                return await _httpResultHelper.SuccessCustomResult(result.Projects, ProjectService.logModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(message: string.Join(" : ", "GetProjectListAsync", ex?.InnerException?.Message ?? ex?.Message));
                throw ex;
            }
        }

        [HttpGet("{id}")]

        [ProducesResponseType(typeof(ResponseModel<ProjectDTO>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetProjectAsync([FromRoute] Guid id, CancellationToken cancellationToken = default)
        {
            var result = await ProjectService.GetProjectAsync(id, cancellationToken);
            return await _httpResultHelper.SuccessCustomResult(result, ProjectService.logModel);
        }

        /// <summary>
        /// ดึงข้อมูลจำนวนโครงการในสถานะต่างๆ
        /// </summary>
        /// <returns></returns>
        [HttpGet("Count")]
        [ProducesResponseType(typeof(ResponseModel<ProjectCountDTO>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetProjectCountAsync(CancellationToken cancellationToken = default)
        {
            var result = await ProjectService.GetProjectCountAsync(cancellationToken);
            return await _httpResultHelper.SuccessCustomResult(result, ProjectService.logModel);
        }
        /// <summary>
        /// สร้างข้อมูลโครงการ
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(ResponseModel<ProjectDTO>), StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateProjectAsync([FromBody] ProjectDTO input)
        {
            using (var tran = await DB.Database.BeginTransactionAsync())
            {
                try
                {
                    var result = await ProjectService.CreateProjectAsync(input);
                    await tran.CommitAsync();
                    return await _httpResultHelper.SuccessCustomResult(result, ProjectService.logModel, HttpStatusCode.Created);

                }
                catch (Exception ex)
                {
                    _logger.LogError(message: string.Join(" : ", "CreateProjectAsync", ex?.InnerException?.Message ?? ex?.Message));
                    await tran.RollbackAsync();
                    throw ex;
                }
            }
        }
        /// <summary>
        /// ลบโครงการ
        /// </summary>
        [HttpDelete("{projectID}")]
        public async Task<IActionResult> DeleteProjectAsync([FromRoute] Guid projectID, [FromQuery] string reason)
        {
            using (var tran = await DB.Database.BeginTransactionAsync())
            {
                try
                {
                    await ProjectService.DeleteProjectAsync(projectID, reason);
                    await tran.CommitAsync();
                    return await _httpResultHelper.SuccessCustomResult(null, ProjectService.logModel);

                }
                catch (Exception ex)
                {
                    _logger.LogError(message: string.Join(" : ", "DeleteProjectAsync", ex?.InnerException?.Message ?? ex?.Message));
                    await tran.RollbackAsync();
                    throw ex;
                }
            }
        }

        /// <summary>
        /// ดึง url Template ใบจอง
        /// </summary>
        /// <param name="projectID"></param>
        /// <returns></returns>
        [HttpGet("{projectID}/ExportBookingTemplateUrl")]

        [ProducesResponseType(typeof(ResponseModel<ReportResult>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetExportBookingTemplateUrlAsync([FromRoute] Guid projectID, CancellationToken cancellationToken = default)
        {
            var result = await ProjectService.GetExportBookingTemplateUrlAsync(projectID, cancellationToken);
            return await _httpResultHelper.SuccessCustomResult(result, ProjectService.logModel);
        }

        /// <summary>
        /// ดึง url Template สัญญา
        /// </summary>
        /// <param name="projectID"></param>
        /// <returns></returns>
        [HttpGet("{projectID}/ExportAgreementTemplateUrl")]
        [ProducesResponseType(typeof(ResponseModel<ReportResult>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetExportAgreementTemplateUrlAsync([FromRoute] Guid projectID, CancellationToken cancellationToken = default)
        {
            var result = await ProjectService.GetExportAgreementTemplateUrlAsync(projectID, cancellationToken);
            return await _httpResultHelper.SuccessCustomResult(result, ProjectService.logModel);
        }


        /// <summary>
        /// อัพเดทสถานะโครงการ projectStatus
        /// </summary>
        /// <param name="id"></param>
        /// <param name="projectStatus"></param>
        /// <returns></returns>
        [HttpPut("{id}/ProjectStatus")]
        public async Task<IActionResult> UpdateProjectStatus([FromRoute] Guid id, [FromBody] MasterCenterDropdownDTO projectStatus)
        {
            using (var tran = await DB.Database.BeginTransactionAsync())
            {
                try
                {
                    await ProjectService.UpdateProjectStatus(id, projectStatus);
                    await tran.CommitAsync();
                    return await _httpResultHelper.SuccessCustomResult(null, ProjectService.logModel);

                }
                catch (Exception ex)
                {
                    _logger.LogError(message: string.Join(" : ", "UpdateProjectStatus", ex?.InnerException?.Message ?? ex?.Message));
                    await tran.RollbackAsync();
                    throw ex;
                }
            }
        }

        /// <summary>
        /// อัพเดทสถานะโครงการ projectStatusManual
        /// </summary>
        /// <param name="projectNo"></param>
        /// <param name="projectStatusKey"></param>
        /// <returns></returns>
        [HttpPost("ProjectStatusM")]
        [ProducesResponseType(typeof(ResponseModel<ProjectDTO>), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateProjectStatusM([FromQuery] string projectNo, [FromQuery] string projectStatusKey)
        {
            using (var tran = await DB.Database.BeginTransactionAsync())
            {
                try
                {
                    var result = await ProjectService.UpdateProjectStatusM(projectNo, projectStatusKey);
                    await tran.CommitAsync();
                    return await _httpResultHelper.SuccessCustomResult(result, ProjectService.logModel);

                }
                catch (Exception ex)
                {
                    _logger.LogError(message: string.Join(" : ", "UpdateProjectStatusM", ex?.InnerException?.Message ?? ex?.Message));
                    await tran.RollbackAsync();
                    throw ex;
                }
            }
        }

        /// <summary>
        /// Save Default Project
        /// </summary>
        [HttpPut]
        public async Task<IActionResult> SaveUserDefaultProject([FromBody] ProfileUserDTO input)
        {
            using (var tran = await DB.Database.BeginTransactionAsync())
            {
                try
                {
                    Guid parsedUserID;
                    Guid? userID = null;
                    if (Guid.TryParse(HttpContext?.User?.Claims.SingleOrDefault(x => x.Type == "userid")?.Value, out parsedUserID))
                    {
                        userID = parsedUserID;
                    }

                    await ProjectService.SaveUserDefaultProject(input, userID);
                    await tran.CommitAsync();
                    return await _httpResultHelper.SuccessCustomResult(null, ProjectService.logModel);

                }
                catch (Exception ex)
                {
                    _logger.LogError(message: string.Join(" : ", "SaveUserDefaultProject", ex?.InnerException?.Message ?? ex?.Message));
                    await tran.RollbackAsync();
                    throw ex;
                }
            }
        }
        /// <summary>
        /// Get Default Project
        /// </summary>
        /// <returns></returns>
        [HttpGet("DefaultProject")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<ProfileUserDTO>))]
        public async Task<IActionResult> GetDefaultProjectAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                Guid parsedUserID;
                Guid? userID = null;
                if (Guid.TryParse(HttpContext?.User?.Claims.SingleOrDefault(x => x.Type == "userid")?.Value, out parsedUserID))
                {
                    userID = parsedUserID;
                }
                var result = await ProjectService.GetDefaultProjectAsync(userID, cancellationToken);

                return await _httpResultHelper.SuccessCustomResult(result, ProjectService.logModel);

            }
            catch (Exception ex)
            {
                _logger.LogError(message: string.Join(" : ", "GetDefaultProjectAsync", ex?.InnerException?.Message ?? ex?.Message));
                throw ex;
            }
        }

        /// <summary>
        /// Get Default Project
        /// </summary>
        /// <param name="projectID"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("ProjectEvent")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<bool>))]
        public async Task<IActionResult> UpdateProjectEventAsync([FromQuery] Guid projectID, [FromQuery] bool input)
        {
            using (var tran = await DB.Database.BeginTransactionAsync())
            {
                try
                {
                    var result = await ProjectService.UpdateProjectEvent(projectID, input);
                    await tran.CommitAsync();
                    return await _httpResultHelper.SuccessCustomResult(result, ProjectService.logModel);

                }
                catch (Exception ex)
                {
                    _logger.LogError(message: string.Join(" : ", "UpdateProjectEvent", ex?.InnerException?.Message ?? ex?.Message));
                    await tran.RollbackAsync();
                    throw ex;
                }
            }
        }

    }
}