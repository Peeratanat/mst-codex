using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PagingExtensions;

namespace Base.DTOs.Common
{
    public enum Ascending //***
    {
        ASC = 1,
        DESC = 2,
    }
 

    public class PageOutput
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int PageCount { get; set; }
        public int RecordCount { get; set; }
    }

    public class SortMapping<T>
    {
        public T SortBy { get; set; }
        public Ascending? Ascending { get; set; }
    }

    public class PageInput
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public string TableName { get; set; }
        public List<PageSqlParamter> Parameter { get; set; }
    }

    public class PageSqlParamter
    {
        public string Text { get; set; }
        public string Value { get; set; }
        public string Expression { get; set; }
    }

    public class Paging
    {
        public SortByParam SortByParam { get; set; }
        public PageParam PageParam { get; set; }

        public Paging()
        {
            SortByParam = new SortByParam();
            PageParam = new PageParam();
        }
    }
}
