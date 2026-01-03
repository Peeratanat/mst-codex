using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Base.DTOs.Common;
using Base.DTOs.CTM;
using Base.DTOs.MST;
using Base.DTOs.PRJ;
using Common.Helper.HttpResultHelper;
using CTM_CustomerConsent.Params.Filters;
using CTM_CustomerConsent.Services.CustomerConsentService;
using Database.Models;
using Microsoft.AspNetCore.Mvc;
using MST_General.Services;
using PagingExtensions;
using PRJ_Project.Services;

namespace CTM_CustomerConsent.API.Controllers
{
    [Route(HelperStatic.BaseAPIRoute)]
    [ApiController]
    public class CustomerConsentController : BaseController
    {
        private readonly DatabaseContext DB;
        private readonly IHttpResultHelper _httpResultHelper;
        private readonly ICustomerConsentService CustomerConsentService;
        private readonly IDropdownService DropdownService;
        private readonly IMasterCenterService MasterCenterService;



        public CustomerConsentController(DatabaseContext db, ICustomerConsentService customerConsentService, IDropdownService dropdownService, IMasterCenterService masterCenterService, IHttpResultHelper httpResultHelper)
        {
            this.DB = db;
            this.CustomerConsentService = customerConsentService;
            DropdownService = dropdownService;
            MasterCenterService = masterCenterService;
            _httpResultHelper = httpResultHelper;
        }

        /// <summary>
        /// Get List ข้อมูล Consent
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="pageParam"></param>
        /// <param name="sortByParam"></param>
        /// <returns>List LeadListDTO</returns>
        [HttpGet]
        [PagingResponseHeaders]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<List<ConsentListDTO>>))]
        public async Task<IActionResult> GetConSentList([FromQuery] ConsentFilter filter, [FromQuery] PageParam pageParam, [FromQuery] ConsentListSortByParam sortByParam, CancellationToken cancellationToken = default)
        {
            var result = await CustomerConsentService.GetConSentListAsync(filter, pageParam, sortByParam, cancellationToken);
            AddPagingResponse(result.PageOutput);
            return await _httpResultHelper.SuccessCustomResult(result.Consent, CustomerConsentService.logModel);
        }


        /// <summary>
        /// แก้ข้อมูล onsent
        /// </summary> 
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("UpdateCustomerConsentAsync")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateCustomerConsentAsync([FromBody] UpdateCustomerConsentDTO input)
        {
            using (var tran = await DB.Database.BeginTransactionAsync())
            {
                try
                {
                    await CustomerConsentService.UpdateCustomerConsentAsync(input);
                    await tran.CommitAsync();
                    return await _httpResultHelper.SuccessCustomResult(null, CustomerConsentService.logModel);
                }
                catch (Exception ex)
                {
                    await tran.RollbackAsync();
                    throw ex;
                }
            }
        }


        /// <summary>
        /// ลิสข้อมูลพื้นฐานทั่วไป Dropdown
        /// </summary>
        /// <param name="masterCenterGroupKey"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpGet("DropdownList")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<List<MasterCenterDropdownDTO>>))]
        public async Task<IActionResult> GetMasterCenterDropdownList([FromQuery] string masterCenterGroupKey, [FromQuery] string name, CancellationToken cancellationToken = default)
        {
            var results = await MasterCenterService.GetMasterCenterDropdownListAsync(masterCenterGroupKey, name, null, null, cancellationToken);
            return await _httpResultHelper.SuccessCustomResult(results, MasterCenterService.logModel);
        }


        [HttpGet("Find")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<MasterCenterDropdownDTO>))]
        public async Task<IActionResult> GetFindMasterCenterDropdownItem([FromQuery] string masterCenterGroupKey, [FromQuery] string key, CancellationToken cancellationToken = default)
        {
            var result = await MasterCenterService.GetFindMasterCenterDropdownItemAsync(masterCenterGroupKey, key, cancellationToken);
            return await _httpResultHelper.SuccessCustomResult(result, MasterCenterService.logModel);
        }



        /// <summary>
        /// ลิสข้อมูลโครงการ Dropdown
        /// </summary>
        /// <param name="name"></param>
        /// <param name="companyID"></param>
        /// <param name="isActive"></param>
        /// <param name="isRepurchase"></param>
        /// <param name="projectStatusKey"></param>
        /// <returns></returns>
        [HttpGet("ProjectDropdownList")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<List<ProjectDropdownDTO>>))]
        public async Task<IActionResult> GetProjectDropdown([FromQuery] string name, [FromQuery] Guid? companyID, [FromQuery] bool isActive = true, [FromQuery] bool isRepurchase = false, [FromQuery] string projectStatusKey = null, CancellationToken cancellationToken = default)
        {

            Guid? userID = null;
            var result = await DropdownService.GetProjectDropdownListAsync(name, companyID, isActive, isRepurchase, projectStatusKey, userID, cancellationToken);
            return await _httpResultHelper.SuccessCustomResult(result, DropdownService.logModel);
        }

    }


}