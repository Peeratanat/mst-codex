using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Base.DTOs;
using Base.DTOs.MST;
using Base.DTOs.SAL;
using Base.DTOs.SAL.Sortings;
using Database.Models;
using Database.Models.MST;
using MST_Lg.Params.Filters;
using MST_Lg.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PagingExtensions;
using Base.DTOs.Common;
using Common.Helper.HttpResultHelper;

namespace MST_Lg.API.Controllers
{
#if !DEBUG
    [Authorize]
#endif
    [Route(HelperStatic.BaseAPIRoute)]
    [ApiController]
    public class LetterOfGuaranteeController : BaseController
    {
        private ILetterOfGuaranteeService LetterOfGuaranteeService;
        private readonly IHttpResultHelper _httpResultHelper;
        private readonly ILogger<LetterOfGuaranteeController> _logger;
        private readonly DatabaseContext DB;

        public LetterOfGuaranteeController(ILetterOfGuaranteeService letterOfGuaranteeService, DatabaseContext db, IHttpResultHelper httpResultHelper, ILogger<LetterOfGuaranteeController> logger)
        {
            this.LetterOfGuaranteeService = letterOfGuaranteeService;
            this.DB = db;
            _httpResultHelper = httpResultHelper;
            _logger = logger;
        }

        /// <summary>
        /// ลิสของข้อมูลนิติบุคคล
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="pageParam"></param>
        /// <param name="sortByParam"></param>
        /// <returns></returns>
        [HttpGet("GetLetterOfGuaranteeList")]
        [PagingResponseHeaders]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<List<LetterOfGuaranteeDTO>>))]
        public async Task<IActionResult> GetLetterOfGuaranteeAsync([FromQuery] LetterOfGuaranteeFilter filter, [FromQuery] PageParam pageParam, [FromQuery] LetterOfGuaranteeSortByParam sortByParam, CancellationToken cancellationToken = default)
        {
            var result = await LetterOfGuaranteeService.GetLetterOfGuaranteeAsync(filter, pageParam, sortByParam, cancellationToken);

            AddPagingResponse(result.PageOutput);
            return await _httpResultHelper.SuccessCustomResult(result.LetterOfGuarantee, LetterOfGuaranteeService.logModel);
        }

        /// <summary>
        /// เพิ่มหนังสือสัญญา
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("AddLetterOfGuarantee")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<LetterOfGuaranteeDTO>))]
        public async Task<IActionResult> AddLetterOfGuaranteeAsync([FromBody] LetterOfGuaranteeDTO input)
        {
            using (var tran = await DB.Database.BeginTransactionAsync())
            {
                try
                {
                    var result = await LetterOfGuaranteeService.AddLetterOfGuaranteeAsync(input);
                    await tran.CommitAsync(); 
                    return await _httpResultHelper.SuccessCustomResult(result, LetterOfGuaranteeService.logModel);
                }
                catch (Exception ex)
                {
                    _logger.LogError(message: string.Join(" : ", "AddLetterOfGuaranteeAsync", ex?.InnerException?.Message ?? ex?.Message));
                    await tran.RollbackAsync();
                    throw ex;
                }
            }
        }

        /// <summary>
        /// แก้ไขหนังสือสัญญา
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("EditLetterOfGuarantee")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<LetterOfGuaranteeDTO>))]
        public async Task<IActionResult> EditLetterOfGuaranteeAsync([FromBody] LetterOfGuaranteeDTO input)
        {
            using (var tran = await DB.Database.BeginTransactionAsync())
            {
                try
                {
                    Guid? userID = null;
                    Guid parsedUserID;
                    if (Guid.TryParse(HttpContext?.User?.Claims.SingleOrDefault(x => x.Type == "userid")?.Value, out parsedUserID))
                    {
                        userID = parsedUserID;
                    }
                    var result = await LetterOfGuaranteeService.EditLetterOfGuaranteeAsync(input, userID);
                    await tran.CommitAsync();
                    return await _httpResultHelper.SuccessCustomResult(result, LetterOfGuaranteeService.logModel);
                }
                catch (Exception ex)
                {
                    _logger.LogError(message: string.Join(" : ", "EditLetterOfGuaranteeAsync", ex?.InnerException?.Message ?? ex?.Message));
                    await tran.RollbackAsync();
                    throw ex;
                }
            }
        }

        /// <summary>
        /// ลบหนังสือสัญญา
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("DeleteLetterOfGuarantee")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<bool>))]
        public async Task<IActionResult> DeleteLetterOfGuaranteeAsync([FromBody] LetterOfGuaranteeDTO input)
        {
            using (var tran = await DB.Database.BeginTransactionAsync())
            {
                try
                {
                    var result = await LetterOfGuaranteeService.DeleteLetterOfGuaranteeAsync(input);
                    await tran.CommitAsync();
                    return await _httpResultHelper.SuccessCustomResult(result, LetterOfGuaranteeService.logModel);
                }
                catch (Exception ex)
                {
                    _logger.LogError(message: string.Join(" : ", "DeleteLetterOfGuaranteeAsync", ex?.InnerException?.Message ?? ex?.Message));
                    await tran.RollbackAsync();
                    throw ex;
                }
            }
        }

        /// <summary>
        /// ยกเลิกหนังสือสัญญา
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("CancelLetterOfGuarantee")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<LetterOfGuaranteeDTO>))]
        public async Task<IActionResult> CancelLetterOfGuaranteeAsync([FromBody] LetterOfGuaranteeDTO input)
        {
            using (var tran = await DB.Database.BeginTransactionAsync())
            {
                try
                {
                    Guid? userID = null;
                    Guid parsedUserID;
                    if (Guid.TryParse(HttpContext?.User?.Claims.SingleOrDefault(x => x.Type == "userid")?.Value, out parsedUserID))
                    {
                        userID = parsedUserID;
                    }
                    var result = await LetterOfGuaranteeService.CancelLetterOfGuaranteeAsync(input, userID);
                    await tran.CommitAsync();
                    return await _httpResultHelper.SuccessCustomResult(result, LetterOfGuaranteeService.logModel);
                }
                catch (Exception ex)
                {
                    _logger.LogError(message: string.Join(" : ", "CancelLetterOfGuaranteeAsync", ex?.InnerException?.Message ?? ex?.Message));
                    await tran.RollbackAsync();
                    throw ex;
                }
            }
        }


        /// <summary>
        /// ยกเลิก ยกเลิกหนังสือสัญญา
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("CancelCancelLetterOfGuarantee")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<LetterOfGuaranteeDTO>))]
        public async Task<IActionResult> CancelCancelLetterOfGuaranteeAsync([FromBody] LetterOfGuaranteeDTO input)
        {
            using (var tran = await DB.Database.BeginTransactionAsync())
            {
                try
                {
                    Guid? userID = null;
                    Guid parsedUserID;
                    if (Guid.TryParse(HttpContext?.User?.Claims.SingleOrDefault(x => x.Type == "userid")?.Value, out parsedUserID))
                    {
                        userID = parsedUserID;
                    }
                    var result = await LetterOfGuaranteeService.CancelCancelLetterOfGuaranteeAsync(input, userID);
                    await tran.CommitAsync();
                    return await _httpResultHelper.SuccessCustomResult(result, LetterOfGuaranteeService.logModel);
                }
                catch (Exception ex)
                {
                    _logger.LogError(message: string.Join(" : ", "CancelCancelLetterOfGuaranteeAsync", ex?.InnerException?.Message ?? ex?.Message));
                    await tran.RollbackAsync();
                    throw ex;
                }
            }
        }

        /// <summary>
        /// ดูรายการเอกสารที่อัพโหลด
        /// </summary>
        /// <param name="LetterGuaranteeID"></param>
        /// <returns>List of AgreementFileDTO</returns>
        [HttpGet("GetLetterGuaranteeFileListAsync")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<List<LetterGuaranteeFileDTO>>))]
        public async Task<IActionResult> GetLetterGuaranteeFileListAsync([FromQuery] Guid LetterGuaranteeID, CancellationToken cancellationToken = default)
        {

            var result = await LetterOfGuaranteeService.GetLetterGuaranteeFileListAsync(LetterGuaranteeID, cancellationToken);
            return await _httpResultHelper.SuccessCustomResult(result, LetterOfGuaranteeService.logModel);
        }

        /// <summary>
        /// ลบเอกสารที่อัพโหลด
        /// </summary>
        /// <param name="id"></param>
        /// <returns>List of AgreementFileDTO</returns>
        [HttpPost("DeleteLetterGuaranteeFile")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<string>))]
        public async Task<IActionResult> DeleteLetterGuaranteeFileAsync([FromQuery] Guid id)
        {
            using (var tran = await DB.Database.BeginTransactionAsync())
            {
                try
                {
                    var result = await LetterOfGuaranteeService.DeleteLetterGuaranteeFileAsync(id);
                    await tran.CommitAsync();
                    return await _httpResultHelper.SuccessCustomResult(result, LetterOfGuaranteeService.logModel);
                }
                catch (Exception ex)
                {
                    _logger.LogError(message: string.Join(" : ", "DeleteLetterGuaranteeFileAsync", ex?.InnerException?.Message ?? ex?.Message));
                    await tran.RollbackAsync();
                    throw;
                }
            }
        }

        /// <summary>
        /// Move File
        /// </summary>
        /// <returns>List of AgreementFileDTO</returns>
        [HttpPost("MoveFile")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<string>))]
        public async Task<IActionResult> MoveFile()
        {
            using (var tran = await DB.Database.BeginTransactionAsync())
            {
                try
                {
                    var result = await LetterOfGuaranteeService.MoveFile();
                    await tran.CommitAsync();
                    return await _httpResultHelper.SuccessCustomResult(result, LetterOfGuaranteeService.logModel);
                }
                catch (Exception ex)
                {
                    _logger.LogError(message: string.Join(" : ", "MoveFile", ex?.InnerException?.Message ?? ex?.Message));
                    await tran.RollbackAsync();
                    throw;
                }
            }
        }


        /// <summary>
        /// ลบเอกสารที่อัพโหลด
        /// </summary>
        /// <param name="id"></param>
        /// <returns>List of AgreementFileDTO</returns>
        [HttpPost("AddLGGuarantor")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<string>))]
        public async Task<IActionResult> AddLGGuarantor([FromBody] MasterCenterDTO input)
        {
            using (var tran = await DB.Database.BeginTransactionAsync())
            {
                try
                {
                    var result = await LetterOfGuaranteeService.AddLGGuarantor(input);
                    await tran.CommitAsync();
                    return await _httpResultHelper.SuccessCustomResult(result, LetterOfGuaranteeService.logModel);
                }
                catch (Exception ex)
                {
                    _logger.LogError(message: string.Join(" : ", "AddLGGuarantor", ex?.InnerException?.Message ?? ex?.Message));
                    await tran.RollbackAsync();
                    throw;
                }
            }
        }
    }
}