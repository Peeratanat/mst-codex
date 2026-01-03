using System;
using System.Linq;
using models = Database.Models;

namespace Base.DTOs.CTM
{
    public class VisitorHistoryDTO
    {
        /// <summary>
        /// โครงการ
        /// </summary>
        public PRJ.ProjectDTO Project { get; set; }
        /// <summary>
        /// วันที่เข้าโครงการ
        /// </summary>
        public DateTime? VisitDateIn { get; set; }
        /// <summary>
        /// วันที่ออกโครงการ
        /// </summary>
        public DateTime? VisitDateOut { get; set; }
        /// <summary>
        /// สถานะ Opportunity (โอกาสการขาย)
        /// masterdata/api/MasterCenters/DropdownList?masterCenterGroupKey=SalesOpportunity
        /// </summary>
        public MST.MasterCenterDropdownDTO SalesOpportunity { get; set; }

        public static VisitorHistoryDTO CreateFromQueryResult(VisitorHistoryQueryResult queryResult)
        {
            if (queryResult != null)
            {
                var result = new VisitorHistoryDTO()
                {
                    Project = PRJ.ProjectDTO.CreateFromModel(queryResult.Project),
                    VisitDateIn = queryResult.Visitor.VisitDateIn,
                    VisitDateOut = queryResult.Visitor.VisitDateOut,
                    SalesOpportunity = MST.MasterCenterDropdownDTO.CreateFromModel(queryResult.SalesOpportunity)
                };
                return result;
            }
            else
            {
                return null;
            }
        }

        public static void SortBy(VisitorHistoryListSortByParam sortByParam, ref IQueryable<VisitorHistoryQueryResult> query)
        {
            switch (sortByParam.SortBy)
            {
                case VisitorHistoryListSortBy.Project:
                    if (sortByParam.Ascending) query = query.OrderBy(o => o.Project.ProjectNo);
                    else query = query.OrderByDescending(o => o.Project.ProjectNo);
                    break;
                case VisitorHistoryListSortBy.VisitDateIn:
                    if (sortByParam.Ascending) query = query.OrderBy(o => o.Visitor.VisitDateIn);
                    else query = query.OrderByDescending(o => o.Visitor.VisitDateIn);
                    break;
                case VisitorHistoryListSortBy.VisitDateOut:
                    if (sortByParam.Ascending) query = query.OrderBy(o => o.Visitor.VisitDateOut);
                    else query = query.OrderByDescending(o => o.Visitor.VisitDateOut);
                    break;
                case VisitorHistoryListSortBy.SalesOpportunity:
                    if (sortByParam.Ascending) query = query.OrderBy(o => o.SalesOpportunity.Name);
                    else query = query.OrderByDescending(o => o.SalesOpportunity.Name);
                    break;
                default:
                    break;
            }
        }

    }

    public class VisitorHistoryQueryResult
    {
        public models.PRJ.Project Project { get; set; }
        public models.CTM.Visitor Visitor { get; set; }
        public models.MST.MasterCenter SalesOpportunity { get; set; }
    }
}
