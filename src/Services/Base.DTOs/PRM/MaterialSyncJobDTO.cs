using System;
using System.Linq;
using Database.Models;
using Database.Models.PRM;

namespace Base.DTOs.PRM
{
    public class MaterialSyncJobDTO : BaseDTO
    {
        /// <summary>
        /// เลขที่ Job
        /// </summary>
        public string JobNo { get; set; }
        /// <summary>
        /// สถานะการทำงาน
        /// InProgress = 0,
        /// Completed = 1,
        /// Failed = 2,
        /// Waiting = 3,
        /// WaitingForResult = 4,
        /// Retrying = 5
        /// </summary>
        public BackgroundJobStatus Status { get; set; }
        /// <summary>
        /// Progress
        /// </summary>
        public double Progress { get; set; }
        /// <summary>
        /// Error Message จาก SAP
        /// </summary>
        public string ResponseMessage { get; set; }

        public static void SortBy(MaterialSyncJobSortByParam sortByParam, ref IQueryable<SAPMaterialSyncJob> query)
        {
            if (sortByParam.SortBy != null)
            {
                switch (sortByParam.SortBy.Value)
                {
                    case MaterialSyncJobSortBy.JobNo:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.JobNo);
                        else query = query.OrderByDescending(o => o.JobNo);
                        break;
                    case MaterialSyncJobSortBy.Status:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.Status);
                        else query = query.OrderByDescending(o => o.Status);
                        break;
                    case MaterialSyncJobSortBy.Updated:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.Updated);
                        else query = query.OrderByDescending(o => o.Updated);
                        break;
                    default:
                        query = query.OrderByDescending(o => o.Updated).ThenBy(o => o.JobNo);
                        break;
                }
            }
            else
            {
                query = query.OrderByDescending(o => o.Updated).ThenBy(o => o.JobNo);
            }
        }

        public static MaterialSyncJobDTO CreateFromModel(SAPMaterialSyncJob model)
        {
            if (model != null)
            {
                var result = new MaterialSyncJobDTO()
                {
                    Id = model.ID,
                    JobNo = model.JobNo,
                    Progress = model.Progress,
                    ResponseMessage = model.ResponseMessage,
                    Status = model.Status,
                    Updated = model.Updated
                };

                return result;
            }
            else
            {
                return null;
            }
        }

    }
}
