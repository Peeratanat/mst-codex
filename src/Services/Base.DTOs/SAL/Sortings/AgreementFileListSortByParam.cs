using System;
using System.Collections.Generic;
using System.Text;

namespace Base.DTOs.SAL.Sortings
{
    public class AgreementFileListSortByParam
    {
        public AgreementFileListSortBy? SortBy { get; set; }
        public bool Ascending { get; set; }
    }

    public enum AgreementFileListSortBy
    {
        ProjectNo,
        DocType,
        UploadDate
    }
}
