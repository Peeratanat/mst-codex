using System;
using Base.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PagingExtensions;

namespace MST_General.API.Controllers
{
    [ProducesResponseType(500, Type = typeof(ErrorResponse))]
    public class BaseController : ControllerBase
    {
        protected void AddPagingResponse(PageOutput output)
        {
            if (output != null)
            {
                Response.Headers.Append("Access-Control-Expose-Headers", "X-Paging-PageNo, X-Paging-PageSize, X-Paging-PageCount, X-Paging-TotalRecordCount");
                Response.Headers.Append("X-Paging-PageNo", output.Page.ToString());
                Response.Headers.Append("X-Paging-PageSize", output.PageSize.ToString());
                Response.Headers.Append("X-Paging-PageCount", output.PageCount.ToString());
                Response.Headers.Append("X-Paging-TotalRecordCount", output.RecordCount.ToString());
            }
        }

        protected Guid? GetUserId()
        {
            var userIdClaim = HttpContext?.User?.Claims.FirstOrDefault(x => x.Type == "userid")?.Value;
            return Guid.TryParse(userIdClaim, out var userID) ? userID : null;
        }
    }
}
