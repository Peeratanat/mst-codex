using Database.Models;
using Microsoft.AspNetCore.Mvc;
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
    public class AgreementsController : BaseController
    {
        private IAgreementService AgreementService;
        private readonly DatabaseContext DB;
        private readonly ILogger<AgreementsController> _logger;
        private readonly IHttpResultHelper _httpResultHelper;

        public AgreementsController(
            DatabaseContext db,
            ILogger<AgreementsController> logger,
            IHttpResultHelper httpResultHelper,
            IAgreementService agreementService)
        {
            DB = db;
            _logger = logger;
            _httpResultHelper = httpResultHelper;
            AgreementService = agreementService;
        }
        /// <summary>
        /// เอกสารสัญญาในโครงการ
        /// </summary>
        [HttpGet("{projectID}/Agreement")]
        [ProducesResponseType(typeof(ResponseModel<AgreementDTO>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAgreementAsync([FromRoute] Guid projectID, CancellationToken cancellationToken = default)
        {
            var result = await AgreementService.GetAgreementAsync(projectID, cancellationToken);
            return await _httpResultHelper.SuccessCustomResult(result, AgreementService.logModel);
        }
        /// <summary>
        /// แก้ไขเอกสารสัญญาในโครงการ
        /// </summary>
        [HttpPut("{projectID}/Agreement/{id}")]
        [ProducesResponseType(typeof(ResponseModel<AgreementDTO>), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateAgreementAsync([FromRoute] Guid projectID, [FromRoute] Guid id, [FromBody] AgreementDTO input)
        {
            using var tran = await DB.Database.BeginTransactionAsync();
            try
            {
                var result = await AgreementService.UpdateAgreementAsync(projectID, id, input);
                await tran.CommitAsync();
                return await _httpResultHelper.SuccessCustomResult(result, AgreementService.logModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(message: string.Join(" : ", "UpdateAgreementAsync", ex?.InnerException?.Message ?? ex?.Message));
                await tran.RollbackAsync();
                throw ex;
            }
        }


    }
}