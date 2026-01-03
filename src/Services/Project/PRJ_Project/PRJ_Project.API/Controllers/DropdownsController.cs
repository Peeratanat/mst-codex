using Base.DTOs.Common;
using Base.DTOs.PRJ;
using Common.Helper.HttpResultHelper;
using Database.Models;
using Microsoft.AspNetCore.Mvc;
using PRJ_Project.Services;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
namespace PRJ_Project.API.Controllers
{

#if !DEBUG
     [Authorize(Policy = "MultiSchemePolicy")]
#endif
    [Route(HelperStatic.BaseAPIRouteController)]
    [ApiController]
    public class DropdownsController : BaseController
    {
        private IDropdownService DropdownService;
        private readonly DatabaseContext DB;
        private readonly ILogger<DropdownsController> _logger;
        private readonly IHttpResultHelper _httpResultHelper;

        public DropdownsController(
            DatabaseContext db,
            ILogger<DropdownsController> logger,
            IHttpResultHelper httpResultHelper,
            IDropdownService dropdownService)
        {
            DB = db;
            _logger = logger;
            _httpResultHelper = httpResultHelper;
            DropdownService = dropdownService;
        }

        /// <summary>
        /// ลิสข้อมูลโครงการ Dropdown Walk Refer
        /// </summary>
        /// <param name="name"></param>
        /// <param name="companyID"></param>
        /// <param name="isActive"></param>
        /// <param name="projectStatusKey"></param>
        /// <returns></returns>
        [HttpGet("WalkRefer")]
        [ProducesResponseType(typeof(ResponseModel<List<ProjectDropdownDTO>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetProjectWalkReferDropdownListAsync([FromQuery] string name,
        [FromQuery] Guid? companyID,
        [FromQuery] bool isActive = true,
        [FromQuery] string projectStatusKey = null,
         CancellationToken cancellationToken = default)
        {
            var result = await DropdownService.GetProjectWalkReferDropdownListAsync(name, companyID, isActive, projectStatusKey, cancellationToken);
            return await _httpResultHelper.SuccessCustomResult(result, DropdownService.logModel);
        }


        /// <summary>
        /// ลิสข้อมูลโครงการ Dropdown สำหรับเบิกโฉนด
        /// </summary>
        /// <param name="name"></param>
        /// <param name="companyID"></param>
        /// <param name="isActive"></param>
        /// <param name="projectStatusKey"></param>
        /// <param name="productType"></param>
        /// <param name="landOfficeID"></param>
        /// <returns></returns>
        [HttpGet("TitledeedRequestFlow")]
        [ProducesResponseType(typeof(ResponseModel<List<ProjectDropdownDTO>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetProjectTitledeedRequestDropdownListAsync([FromQuery] string name,
        [FromQuery] Guid? companyID,
        [FromQuery] Guid? productType,
        [FromQuery] Guid? landOfficeID,
        [FromQuery] bool isActive = true,
        [FromQuery] string projectStatusKey = null,
         CancellationToken cancellationToken = default)
        {
            var result = await DropdownService.GetProjectTitledeedRequestDropdownListAsync(name, companyID, isActive, projectStatusKey, productType, landOfficeID, cancellationToken);
            return await _httpResultHelper.SuccessCustomResult(result, DropdownService.logModel);
        }

        /// <summary>
        /// ลิสข้อมูลโครงการ Dropdown ProjectByProductType
        /// </summary>
        /// <param name="name"></param>
        /// <param name="companyID"></param>
        /// <param name="isActive"></param>
        /// <param name="projectStatusKey"></param>
        /// <param name="productType"></param>
        /// <returns></returns>
        [HttpGet("ProjectByProductType")]
        [ProducesResponseType(typeof(ResponseModel<List<ProjectDropdownDTO>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetProjectByProductTypeDropdownListAsync([FromQuery] string name,
        [FromQuery] Guid? companyID,
        [FromQuery] Guid? productType,
        [FromQuery] bool isActive = true,
        [FromQuery] string projectStatusKey = null,
         CancellationToken cancellationToken = default)
        {
            var result = await DropdownService.GetProjectByProductTypeDropdownListAsync(name, companyID, isActive, projectStatusKey, productType, cancellationToken);
            return await _httpResultHelper.SuccessCustomResult(result, DropdownService.logModel);
        }



        /// <summary>
        /// ลิสข้อมูลโครงการ Dropdown ทุกสถานะ
        /// </summary>
        /// <param name="name"></param>
        /// <param name="companyID"></param>
        /// <returns></returns>
        [HttpGet("AllStatus")]
        [ProducesResponseType(typeof(ResponseModel<List<ProjectDropdownDTO>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetProjectAllStatusDropdownListAsync([FromQuery] string name,
        [FromQuery] Guid? companyID,
         CancellationToken cancellationToken = default)
        {
            Guid? userID = null;
            Guid parsedUserID;
            if (Guid.TryParse(HttpContext?.User?.Claims.SingleOrDefault(x => x.Type == "userid")?.Value, out parsedUserID))
                userID = parsedUserID;

            var result = await DropdownService.GetProjectAllStatusDropdownListAsync(name, companyID, userID, cancellationToken);
            return await _httpResultHelper.SuccessCustomResult(result, DropdownService.logModel);
        }


        /// <summary>
        /// ลิสข้อมูลโครงการ Dropdown ทุกสถานะ
        /// </summary>
        /// <param name="name"></param>
        /// <param name="companyID"></param>
        /// <returns></returns>
        [HttpGet("AllIsActive")]
        [ProducesResponseType(typeof(ResponseModel<List<ProjectDropdownDTO>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetProjectAllIsActiveDropdownListAsync([FromQuery] string name,
        [FromQuery] Guid? companyID,
         CancellationToken cancellationToken = default)
        {
            Guid? userID = null;
            Guid parsedUserID;
            if (Guid.TryParse(HttpContext?.User?.Claims.SingleOrDefault(x => x.Type == "userid")?.Value, out parsedUserID))
                userID = parsedUserID;
            _logger.LogInformation("Guid : " + userID);
            ClaimsPrincipal principal = HttpContext?.User as ClaimsPrincipal;
            if (null != principal)
            {
                foreach (Claim claim in principal.Claims)
                {
                    _logger.LogInformation("CLAIM TYPE: " + claim.Type + "; CLAIM VALUE: " + claim.Value + "</br>");
                }

            }
            var result = await DropdownService.GetProjectAllIsActiveDropdownListAsync(name, companyID, userID, cancellationToken);
            return await _httpResultHelper.SuccessCustomResult(result, DropdownService.logModel);
        }


        /// <summary>
        /// ลิสข้อมูลโครงการ Dropdown
        /// </summary>
        /// <param name="name"></param>
        /// <param name="companyID"></param>
        /// <param name="isActive"></param>
        /// <param name="ignoreRepurchase"></param>
        /// <param name="projectStatusKey"></param>
        /// <returns></returns>
        [HttpGet("Project")]
        [ProducesResponseType(typeof(ResponseModel<List<ProjectDropdownDTO>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetProjectDropdownListAsync([FromQuery] string name,
         [FromQuery] Guid? companyID,
         [FromQuery] bool isActive = true,
         [FromQuery] string projectStatusKey = null,
         [FromQuery] bool ignoreRepurchase = false,
         CancellationToken cancellationToken = default)
        {
            Guid? userID = null;
            Guid parsedUserID;
            if (Guid.TryParse(HttpContext?.User?.Claims.SingleOrDefault(x => x.Type == "userid")?.Value, out parsedUserID))
                userID = parsedUserID;

            var result = await DropdownService.GetProjectDropdownListAsync(name, companyID, isActive, ignoreRepurchase, projectStatusKey, userID, cancellationToken);
            return await _httpResultHelper.SuccessCustomResult(result, DropdownService.logModel);

        }
        /// <summary>
        /// ลิสข้อมูลโครงการ Dropdown ไม่เช็คสิทธิ์โครงการ
        /// </summary>
        /// <param name="name"></param>
        /// <param name="companyID"></param>
        /// <param name="isActive"></param>
        /// <param name="ignoreRepurchase"></param>
        /// <param name="projectStatusKey"></param>
        /// <returns></returns>
        [HttpGet("NonAutjProject")]
        [ProducesResponseType(typeof(ResponseModel<List<ProjectDropdownDTO>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetNonAuthProjectDropdownListAsync([FromQuery] string name,
         [FromQuery] Guid? companyID,
         [FromQuery] bool isActive = true,
         [FromQuery] string projectStatusKey = null,
         [FromQuery] bool ignoreRepurchase = false,
         CancellationToken cancellationToken = default)
        {
            var result = await DropdownService.GetProjectDropdownListAsync(name, companyID, isActive, ignoreRepurchase, projectStatusKey, null, cancellationToken);
            return await _httpResultHelper.SuccessCustomResult(result, DropdownService.logModel);

        }

    }
}