using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Base.DTOs.USR;
using Database.Models;
using Auth_User.Params.Filters;
using Auth_User.Services;
using Microsoft.AspNetCore.Mvc;
using PagingExtensions;
using Microsoft.AspNetCore.Authorization;
using Common.Helper.HttpResultHelper;
using Database.Models.USR;
using System.Net;
using System.Collections;
using System.Reflection.Metadata;
using Auth;
using ErrorHandling;
using Base.DTOs.Common;

namespace Auth_User.API.Controllers
{


     [Authorize]


    [Route(HelperStatic.BaseAPIRoute)]
    [ApiController]
    public class UsersController : BaseController
    {
        private readonly DatabaseContext DB;
        private readonly IUserService UserService;
        private readonly ILogger<UsersController> Logger;
        private readonly IHttpResultHelper _httpResultHelper;

        public UsersController(DatabaseContext db, IUserService userService, ILogger<UsersController> logger, IHttpResultHelper httpResultHelper)
        {
            this.DB = db;
            this.UserService = userService;
            Logger = logger;
            _httpResultHelper = httpResultHelper;
        }

        /// <summary>
        /// Get ข้อมูล List User
        /// </summary>
        /// <returns>UserListDTO</returns>
        [HttpGet]
        [PagingResponseHeaders]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<UserListDTO>))]
        public async Task<IActionResult> GetUserListAsync([FromQuery] UserFilter filter, [FromQuery] PageParam pageParam, [FromQuery] UserListSortByParam sortByParam, CancellationToken cancellationToken = default)
        {
            var result = await UserService.GetUserListAsync(filter, pageParam, sortByParam, cancellationToken);
            AddPagingResponse(result.PageOutput);
            return await _httpResultHelper.SuccessCustomResult(result.Users, UserService.logModel);
        }

        /// <summary>
        /// Get ข้อมูล List Userที่เคยประจำโครงการ
        /// </summary>
        /// <returns>UserListDTO</returns>
        [HttpGet("UserForProject")]
        [PagingResponseHeaders]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<UserListDTO>))]
        public async Task<IActionResult> GetUserForProjectListAsync([FromQuery] UserFilter filter, [FromQuery] PageParam pageParam, [FromQuery] UserListSortByParam sortByParam, CancellationToken cancellationToken = default)
        {
            if (filter.AuthorizeProjectIDs is null)
            {
                return await _httpResultHelper.ExceptionCustomResult(HttpStatusCode.NotFound, new Base.DTOs.Common.ResponseModel<ErrorResponse>
                {
                    Message = "Request parameter AuthorizeProjectIDs"
                }, UserService.logModel);
            }

            var result = await UserService.GetUserForProjectListAsync(filter, pageParam, sortByParam, cancellationToken);
            AddPagingResponse(result.PageOutput);
            return await _httpResultHelper.SuccessCustomResult(result.Users, UserService.logModel);
        }

        /// <summary>
        /// Get ข้อมูล Dropdown List User
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpGet("DropdownList")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<UserListDTO>))]
        public async Task<IActionResult> GetUserDropdownListAsync([FromQuery] string name, CancellationToken cancellationToken = default)
        {
            var results = await UserService.GetUserDropdownListAsync(name, cancellationToken);
            return await _httpResultHelper.SuccessCustomResult(results, UserService.logModel);
        }


        [HttpPost("SyncUserFromAuth")]
        public async Task<IActionResult> SyncUserFromAuthAsync(CancellationToken cancellationToken = default)
        {
            using (var tran = await DB.Database.BeginTransactionAsync())
            {
                try
                {
                    await UserService.SyncUserFromAuthAsync(cancellationToken);

                    await tran.CommitAsync();
                    return await _httpResultHelper.SuccessCustomResult("OK", UserService.logModel);
                }
                catch (Exception ex)
                {
                    await tran.RollbackAsync();
                    throw ex;
                }
            }
        }


        /// <summary>
        ///  เจ้าของบัตร K-Cash Card
        /// </summary>
        /// <param name="projectID">projectID</param>
        /// <param name="textFilter">textFilter</param>
        /// <returns></returns>
        [HttpGet("GetKCashCardUserByProject")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<List<UserListDTO>>))]
        public async Task<IActionResult> GetKCashCardUserByProjectAsync([FromQuery] Guid projectID, [FromQuery] string textFilter, CancellationToken cancellationToken = default)
        {
            var result = await UserService.GetKCashCardUserByProjectAsync(projectID, textFilter, cancellationToken);
            return await _httpResultHelper.SuccessCustomResult(result, UserService.logModel);
        }

        [HttpGet("GetUserAsync")]
        [PagingResponseHeaders]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<UserListDTO>))]
        public async Task<IActionResult> GetUserAsync([FromQuery] Guid id, CancellationToken cancellationToken = default)
        {
            var result = await UserService.GetUserAsync(id, cancellationToken);
            return await _httpResultHelper.SuccessCustomResult(result, UserService.logModel);
        }
        [HttpGet("GetUserAppPermission")]
        [PagingResponseHeaders]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(JsonWebToken))]
        public async Task<IActionResult> GetUserAppPermissionAsync(CancellationToken cancellationToken = default)
        {
            using (var tran = await DB.Database.BeginTransactionAsync())
            {
                try
                {
                    Guid userID = Guid.Empty;
                    Guid parsedUserID;
                    if (Guid.TryParse(HttpContext?.User?.Claims.SingleOrDefault(x => x.Type == "userid")?.Value, out parsedUserID))
                    {
                        userID = parsedUserID;
                    }

                    var result = await UserService.GetUserAppPermissionAsync(userID, cancellationToken);
                    await tran.CommitAsync();
                    return await _httpResultHelper.SuccessCustomResult(result, UserService.logModel);
                }
                catch (Exception ex)
                {
                    Logger.LogError(message: string.Join(" : ", "GetUserAppPermissionAsync", ex?.InnerException?.Message ?? ex?.Message));
                    await tran.RollbackAsync();
                    throw;
                }
            }

        }

        [HttpGet("SyncUserRoleByEmpCode")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        public async Task<IActionResult> SyncUserRoleByEmpCode([FromQuery] string EmpCode, CancellationToken cancellationToken = default)
        {
            var results = await UserService.SyncUserRoleByEmpCodeAsync(EmpCode, cancellationToken);
            return await _httpResultHelper.SuccessCustomResult(true, UserService.logModel);
        }

        /// <summary>
        /// ดึงข้อมูลพนักงายขานประจำโครงการ
        /// </summary>
        /// <param name="projectID"></param>
        /// <param name="ignoreQueryFilters"></param>
        /// <param name="saleUserFullName"></param>
        /// <returns></returns>
        [HttpGet("GetSaleUserProject")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<UserListDTO>))]
        public async Task<IActionResult> GetSaleUserProject([FromQuery] Guid projectID, [FromQuery] bool ignoreQueryFilters, [FromQuery] string saleUserFullName, CancellationToken cancellationToken = default)
        {
            //ยุบไปเรียก inzone อันเก่าเขียนเหมือนกัน
            var result = await UserService.GetSaleUserProjectInZoneAsync(projectID, ignoreQueryFilters, saleUserFullName, cancellationToken);
            return await _httpResultHelper.SuccessCustomResult(result, UserService.logModel);
        }

        /// <summary>
        /// ดึงข้อมูลพนักงายขานประจำโครงการ ตาม Zone
        /// </summary>
        /// <param name="projectID"></param>
        /// <param name="ignoreQueryFilters"></param>
        /// <param name="saleUserFullName"></param>
        /// <returns></returns>
        [HttpGet("GetSaleUserProjectInZone")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<UserListDTO>))]
        public async Task<IActionResult> GetSaleUserProjectInZoneAsync([FromQuery] Guid projectID, [FromQuery] bool ignoreQueryFilters, [FromQuery] string saleUserFullName, CancellationToken cancellationToken = default)
        {
            var result = await UserService.GetSaleUserProjectInZoneAsync(projectID, ignoreQueryFilters, saleUserFullName, cancellationToken);
            return await _httpResultHelper.SuccessCustomResult(result, UserService.logModel);
        }



        [HttpGet("GetPCardUserByProject")]
        [ProducesResponseType(200, Type = typeof(List<UserListDTO>))]
        public async Task<IActionResult> GetPCardUserByProjectAsync([FromQuery] Guid transferId)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    var result = await UserService.GetPCardUserByProjectAsync(transferId);
                    tran.Commit();
                    return await _httpResultHelper.SuccessCustomResult(result, UserService.logModel);
                }
                catch (Exception ex)
                {
                    Logger.LogError(message: string.Join(" : ", "GetPCardUserByProjectAsync", ex?.InnerException?.Message ?? ex?.Message));
                    await tran.RollbackAsync();
                    throw;
                }
            }
        }
    }
}
