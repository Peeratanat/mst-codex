
using Base.DTOs.Common;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace Common.Extensions.HeaderUril
{
    public interface IHeadersUtils
    {
        Task<string> GetBodyRequest();
        string GetQueryString();
        HttpRequest GetHttpRequest();
        Guid? GetUserID();
        string GetUserEmcode();
        void SetUserData(string UserID, string Empcode);
        string GeturlPath();
        string GetMethod();
        void SetMethod(string Method);
        void SetPagingHeader(PageOutput output);
        string CurrentToken { get; }
        string EmpCode { get; }
        string ControllerName { get; }
        string ActionName { get; }
        string UserID { get; }
        void VerifyHeaderEQN();
    }
}
