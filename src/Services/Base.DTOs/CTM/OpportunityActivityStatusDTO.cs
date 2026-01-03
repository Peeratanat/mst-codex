using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using models = Database.Models;

namespace Base.DTOs.CTM
{
    public class OpportunityActivityStatusDTO
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
        /// ลำดับ
        /// </summary>
        public int Order { get; set; }
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
        /// <summary>
        /// ชนิดของ Status (ActivityResult = 0, ActivityEnd = 1)
        /// masterdata/api/MasterCenters/DropdownList?masterCenterGroupKey=WalkActivityStatusType
        /// </summary>
        public MST.MasterCenterDropdownDTO WalkActivityStatusType { get; set; }

        public static OpportunityActivityStatusDTO CreateFromModel(models.CTM.OpportunityActivityStatus model, List<models.CTM.OpportunityActivityResult> activityResults = null)
        {
            if (model != null)
            {
                var result = new OpportunityActivityStatusDTO
                {
                    Id = model.ID,
                    StatusDescription = model.Description,
                    Code = model.Code,
                    Order = model.Order,
                    WalkActivityStatusType = MST.MasterCenterDropdownDTO.CreateFromModel(model.WalkActivityStatusType),
                };
                if (activityResults != null)
                {
                    if(activityResults.Count > 0)
                    {
                        var activityResult = activityResults.Where(o => o.StatusID == model.ID).FirstOrDefault();
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
    }
}
