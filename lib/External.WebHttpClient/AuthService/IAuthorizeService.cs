using Base.DTOs;
using Base.DTOs.Auth;
using Common.Helper.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace External.WebHttpClient
{
    public interface IAuthorizeService
    {
        LogModel logModel { get; set; }

        //Task<List<UserRoleResultReturn>> GetUserRoleAsync(string employeeNo);
        Task<AuthorizeDataResp> LoginFromAuthByTokenAsnc(string uToken);
        Task<AuthorizeDataResp> LoginAsnc(LoginParam data);
        //Task<string> AddMasterCenterAsync(AddMasterDataReq data);
        Task<IntrospecDTO> GetIntroSpect(IntrospecReqDTO data);
        
    }
}
