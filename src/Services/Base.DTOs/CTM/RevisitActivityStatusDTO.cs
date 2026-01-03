using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using models = Database.Models;

namespace Base.DTOs.CTM
{
    public class RevisitActivityStatusDTO
    {
        /// <summary>
        /// ID ของ Activity Status
        /// </summary>
        public Guid? Id { get; set; }
        /// <summary>
        /// รหัส
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// รายละเอียดของ Status
        /// </summary>
        public string StatusDescription { get; set; }
        /// <summary>
        /// เหตุผลอื่นๆ
        /// </summary>
        public string OtherReason { get; set; }
        /// <summary>
        /// เลือกสถานะ/ไม่เลือกสถานะ
        /// </summary>
        public bool IsSelected { get; set; }

        public static RevisitActivityStatusDTO CreateFromModel(models.CTM.RevisitActivityStatus model, List<models.CTM.RevisitActivityResult> activityResults = null)
        {
            if (model != null)
            {
                var result = new RevisitActivityStatusDTO
                {
                    Id = model.ID,
                    StatusDescription = model.Description,
                    Code = model.Code
                };
                if (activityResults != null)
                {
                    if (activityResults.Count > 0)
                    {
                        var activityResult = activityResults.FirstOrDefault(o => o.StatusID == model.ID);
                        if (activityResult != null)
                        {
                            result.IsSelected = true;
                            result.OtherReason = activityResult.OtherReasons;
                        }
                    }
                }

                return result;
            }
            else
            {
                return null;
            }
        }

        public static RevisitActivityStatusDTO CreateListFromModel(models.CTM.RevisitActivityResult model)
        {
            if (model != null)
            {
                var result = new RevisitActivityStatusDTO
                {
                    Id = model.RevisitActivityStatus.ID,
                    StatusDescription = model.RevisitActivityStatus.Description,
                    Code = model.RevisitActivityStatus.Code,
                    IsSelected = true,
                    OtherReason = model.OtherReasons
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
