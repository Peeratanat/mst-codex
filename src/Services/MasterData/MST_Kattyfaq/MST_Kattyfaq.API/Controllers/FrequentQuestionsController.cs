using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Base.DTOs;
using Base.DTOs.MST;
using Database.Models;
using MST_Kattyfaq.Params.Filters;
using MST_Kattyfaq.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PagingExtensions;
using Common.Helper.HttpResultHelper;


namespace MST_Kattyfaq.API.Controllers
{
#if !DEBUG
    [Authorize]
#endif
    [Route(HelperStatic.BaseAPIRoute)]
    [ApiController]
    public class FrequentQuestionsController : BaseController
    {
        private IFrequentQuestionsService FrequentQuestionsService;
        private readonly DatabaseContext DB;
        private readonly IHttpResultHelper _httpResultHelper;
        private readonly ILogger<FrequentQuestionsController> _logger;
        public FrequentQuestionsController(DatabaseContext db, IFrequentQuestionsService frequentQuestionsService, IHttpResultHelper httpResultHelper, ILogger<FrequentQuestionsController> logger)
        {
            FrequentQuestionsService = frequentQuestionsService;
            DB = db;
            _httpResultHelper = httpResultHelper;
            _logger = logger;
        }


        /// <summary>
        /// ลิสของข้อมูลคำถาม
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="pageParam"></param>
        /// <param name="sortByParam"></param>
        /// <returns></returns>
        [HttpGet("GetQuestionFAQList")]
        [PagingResponseHeaders]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<QuestionFAQDTO>))]
        public async Task<IActionResult> GetQuestionFAQListAsync([FromQuery] QuestopnFAQFilter filter, [FromQuery] PageParam pageParam, [FromQuery] QuestionFAQSortByParam sortByParam, CancellationToken cancellationToken = default)
        {
            var result = await FrequentQuestionsService.GetQuestionFAQListAsync(filter, pageParam, sortByParam, cancellationToken);
            AddPagingResponse(result.PageOutput);
            return await _httpResultHelper.SuccessCustomResult(result.FrequentQuestions, FrequentQuestionsService.logModel);
        }


        /// <summary>
        /// เพิ่ม question and answer
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("AddQuestion")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(QuestionFAQDTO))]
        public async Task<IActionResult> AddQuestionAsync([FromBody] QuestionFAQDTO input)
        {
            using (var tran = await DB.Database.BeginTransactionAsync())
            {
                try
                {
                    var result = await FrequentQuestionsService.AddQuestionAsync(input);
                    await tran.CommitAsync();
                    return await _httpResultHelper.SuccessCustomResult(result, FrequentQuestionsService.logModel,HttpStatusCode.Created);
                }
                catch (Exception ex)
                {
                    _logger.LogError(message: string.Join(" : ", "AddQuestionAsync", ex?.InnerException?.Message ?? ex?.Message));
                    await tran.RollbackAsync();
                    throw ex;
                }
            }
        }

        /// <summary>
        /// แก้ไข question and answer
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("EditQuestion")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(QuestionFAQDTO))]
        public async Task<IActionResult> EditQuestionAsync([FromBody] QuestionFAQDTO input)
        {
            using (var tran = await DB.Database.BeginTransactionAsync())
            {
                try
                {
                    var result = await FrequentQuestionsService.EditQuestionAsync(input);
                    await tran.CommitAsync();
                    return await _httpResultHelper.SuccessCustomResult(result, FrequentQuestionsService.logModel);
                }
                catch (Exception ex)
                {
                    _logger.LogError(message: string.Join(" : ", "EditQuestionAsync", ex?.InnerException?.Message ?? ex?.Message));
                    await tran.RollbackAsync();
                    throw ex;
                }
            }
        }

        /// <summary>
        /// Export question and answer
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("ExportQuestion")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(FileDTO))]
        public async Task<IActionResult> ExportQuestion([FromBody] List<Guid> input, CancellationToken cancellationToken = default)
        {
            var result = await FrequentQuestionsService.GenTextFileQuestionAsync(input, cancellationToken);
            return await _httpResultHelper.SuccessCustomResult(result, FrequentQuestionsService.logModel);
        }


        /// <summary>
        /// ลบ question and answer
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("DeleteQuestion")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        public async Task<IActionResult> DeleteQuestionAsync([FromBody] QuestionFAQDTO input)
        {
            using (var tran = await DB.Database.BeginTransactionAsync())
            {
                try
                {
                    await FrequentQuestionsService.DeleteQuestionAsync(input);
                    await tran.CommitAsync();
                    return await _httpResultHelper.SuccessCustomResult(null, FrequentQuestionsService.logModel);
                }
                catch (Exception ex)
                {
                    _logger.LogError(message: string.Join(" : ", "DeleteQuestionAsync", ex?.InnerException?.Message ?? ex?.Message));
                    await tran.RollbackAsync();
                    throw ex;
                }
            }
        }

    }
}