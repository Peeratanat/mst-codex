using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Base.DTOs.Common;
using Base.DTOs.PRJ;
using Base.DTOs.SAL;
using Common.Helper.HttpResultHelper;
using Database.Models;
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
    public class HomeInspectionController : BaseController
    {
        private IHomeInspectionService HomeInspectionService;
        private readonly IHttpResultHelper _httpResultHelper;
        private readonly ILogger<HomeInspectionController> _logger;

        public HomeInspectionController(IHomeInspectionService homeInspectionService, IHttpResultHelper httpResultHelper, ILogger<HomeInspectionController> logger)
        {
            this.HomeInspectionService = homeInspectionService;
            _httpResultHelper = httpResultHelper;
            _logger = logger;
        }

        //[HttpGet]
        //[PagingResponseHeaders]
        //[ProducesResponseType(200, Type = typeof(ResponseModel<List<UnitInfoListDTO>>))]
        //public async Task<IActionResult> GetUnitInfoListAsync([FromQuery] UnitInfoListFilter filter, [FromQuery] PageParam pageParam, [FromQuery] UnitInfoListSortByParam sortByParam, CancellationToken cancellationToken = default)
        //{
        //    var result = await UnitInfoService.GetUnitInfoListAsync(filter, pageParam, sortByParam,cancellationToken);
        //    AddPagingResponse(result.PageOutput);
        //    return await _httpResultHelper.SuccessCustomResult(result.Units, UnitInfoService.logModel);
        //}

        /// <summary>
        /// getPublicAppointmentCalendar
        /// </summary>
        /// <param name="body"></param>
        /// <returns></returns>
        [HttpGet("GetPublicAppointmentCalendar")]
        [ProducesResponseType(200, Type = typeof(ResponseModel<List<PublicAppointmentDTO>>))]
        public async Task<IActionResult> GetPublicAppointmentCalendar([FromQuery] PublicAppointmentFilter filter, CancellationToken cancellationToken = default)
        {
            Guid? userID = null;// Guid.Parse("ADB42DC3-8767-451D-AEC8-12E7FBF62F10");
#if DEBUG
            userID = Guid.Parse("ADB42DC3-8767-451D-AEC8-12E7FBF62F10");
#endif
            Guid parsedUserID;
            if (Guid.TryParse(HttpContext?.User?.Claims.Where(x => x.Type == "userid").Select(o => o.Value).SingleOrDefault(), out parsedUserID))
                userID = parsedUserID;

            var result = await HomeInspectionService.GetPublicAppointmentCalendar(filter, userID, cancellationToken);

            return await _httpResultHelper.SuccessCustomResult(result, HomeInspectionService.logModel);
        }
        /// <summary>
        /// CreateAppointment
        /// </summary>
        /// <param name="body"></param>
        /// <returns></returns>
        [HttpPost("CreateAppointment")]
        [ProducesResponseType(200, Type = typeof(ResponseModel<bool?>))]
        public async Task<IActionResult> CreateAppointment([FromBody] CreateAppointmentDTO body, CancellationToken cancellationToken = default)
        {
            try
            {
                Guid? userID = null;// Guid.Parse("ADB42DC3-8767-451D-AEC8-12E7FBF62F10");

                Guid parsedUserID;
                if (Guid.TryParse(HttpContext?.User?.Claims.Where(x => x.Type == "userid").Select(o => o.Value).SingleOrDefault(), out parsedUserID))
                    userID = parsedUserID;
#if DEBUG
                userID = Guid.Parse("ADB42DC3-8767-451D-AEC8-12E7FBF62F10");
#endif
                var result = await HomeInspectionService.CreateAppointment(body, userID ,  cancellationToken );

                return await _httpResultHelper.SuccessCustomResult(result, HomeInspectionService.logModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(message: string.Join(" : ", "CreateAppointment", ex?.InnerException?.Message ?? ex?.Message)); 
                throw;
            }
        }
        /// <summary>
        /// getPublicAppointmentCalendar
        /// </summary>
        /// <param name="body"></param>
        /// <returns></returns>
        [HttpPost("UpdateAppointment")]
        [ProducesResponseType(200, Type = typeof(ResponseModel<bool?>))]
        public async Task<IActionResult> UpdateAppointment([FromBody] CreateAppointmentDTO body, CancellationToken cancellationToken = default)
        {
            try
            {
                Guid? userID = null;// Guid.Parse("ADB42DC3-8767-451D-AEC8-12E7FBF62F10");
 
                Guid parsedUserID;
                if (Guid.TryParse(HttpContext?.User?.Claims.Where(x => x.Type == "userid").Select(o => o.Value).SingleOrDefault(), out parsedUserID))
                    userID = parsedUserID;
#if DEBUG
                userID = Guid.Parse("ADB42DC3-8767-451D-AEC8-12E7FBF62F10");
#endif
                var result = await HomeInspectionService.UpdateAppointment(body, userID , cancellationToken);

                return await _httpResultHelper.SuccessCustomResult(result, HomeInspectionService.logModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(message: string.Join(" : ", "UpdateAppointment", ex?.InnerException?.Message ?? ex?.Message));
                throw;
            }
        }
        /// <summary>
        /// getPublicAppointmentCalendar
        /// </summary>
        /// <param name="body"></param>
        /// <returns></returns>
        [HttpGet("GetPublicAppointment")]
        [ProducesResponseType(200, Type = typeof(ResponseModel<List<PublicAppointmentListDTO>>))]
        public async Task<IActionResult> GetPublicAppointment([FromQuery] PublicAppointmentFilter filter, CancellationToken cancellationToken = default)
        { 
                Guid? userID = null;// Guid.Parse("960F48E3-D8CF-4DD2-852A-CB463A0F2E49");
                Guid parsedUserID;
                if (Guid.TryParse(HttpContext?.User?.Claims.Where(x => x.Type == "userid").Select(o => o.Value).SingleOrDefault(), out parsedUserID))
                    userID = parsedUserID;
#if DEBUG
                userID = Guid.Parse("ADB42DC3-8767-451D-AEC8-12E7FBF62F10");
#endif
                var result = await HomeInspectionService.GetPublicAppointment(filter, userID, cancellationToken);

                return await _httpResultHelper.SuccessCustomResult(result, HomeInspectionService.logModel);
             
        }
        /// <summary>
        /// getPublicAppointmentCalendar
        /// </summary>
        /// <param name="body"></param>
        /// <returns></returns>
        [HttpPost("DeleteAppointment")]
        [ProducesResponseType(200, Type = typeof(ResponseModel<bool?>))]
        public async Task<IActionResult> DeleteAppointment([FromBody] DeleteDefecttrackingInput body, CancellationToken cancellationToken = default)
        {
            try
            {
                Guid? userID = null;// Guid.Parse("960F48E3-D8CF-4DD2-852A-CB463A0F2E49");
                Guid parsedUserID;
                if (Guid.TryParse(HttpContext?.User?.Claims.Where(x => x.Type == "userid").Select(o => o.Value).SingleOrDefault(), out parsedUserID))
                    userID = parsedUserID;

                var result = await HomeInspectionService.DeleteAppointment(body, userID, cancellationToken);

                return await _httpResultHelper.SuccessCustomResult(result, HomeInspectionService.logModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(message: string.Join(" : ", "UpdateAppointment", ex?.InnerException?.Message ?? ex?.Message));
                throw;
            }
        }
        [HttpGet("GetSEByProjectDropdown")]
        [ProducesResponseType(200, Type = typeof(ResponseModel<List<SeDTO>>))]
        public async Task<IActionResult> GetSEByProjectDropdown([FromQuery] string projectCode, CancellationToken cancellationToken = default)
        {
 
            var result = await HomeInspectionService.GetSEByProjectDropdown(projectCode );

            return await _httpResultHelper.SuccessCustomResult(result, HomeInspectionService.logModel);

        }
        [HttpGet("GetInspectionType")]
        [ProducesResponseType(200, Type = typeof(ResponseModel<List<InspectionTypeDTO>>))]
        public async Task<IActionResult> GetInspectionType( CancellationToken cancellationToken = default)
        {
 
            var result = await HomeInspectionService.GetMasterInspectionType( );

            return await _httpResultHelper.SuccessCustomResult(result, HomeInspectionService.logModel);

        }
    }
}
