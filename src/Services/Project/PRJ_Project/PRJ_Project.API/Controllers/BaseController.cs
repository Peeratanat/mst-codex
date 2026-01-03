using System;
using Base.DTOs;
using Microsoft.AspNetCore.Mvc;
using PagingExtensions;

namespace PRJ_Project.API.Controllers
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
    }
}
