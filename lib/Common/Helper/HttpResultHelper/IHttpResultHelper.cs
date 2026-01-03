
using Base.DTOs.Common;
using Common.Helper.Logging; 
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Common.Helper.HttpResultHelper
{
    public interface IHttpResultHelper
    {
        Task<IActionResult> SuccessCustomResult(dynamic data, LogModel logModel, HttpStatusCode httpStatusCode = HttpStatusCode.OK);
        Task<IActionResult> ExceptionCustomResult(HttpStatusCode statuscode, ResponseModel data, LogModel logModel);
    }
}
