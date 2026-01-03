using PagingExtensions;
using System;

namespace Base.DTOs
{
    public class BaseDTOFromQuery
    {
        public Guid? Id { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? Updated { get; set; }

        public int? Page { get; set; }
        public int? PageSize { get; set; }
        public int? PageCount { get; set; }
        public int? RecordCount { get; set; }
    }

}
