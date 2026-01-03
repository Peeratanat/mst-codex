using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using models = Database.Models;

namespace Base.DTOs.CTM
{
    public class LeadActivityStatusDTO
    {
        /// <summary>
        /// รหัสของ Activity Status
        /// </summary>
        public Guid? Id { get; set; }
        /// <summary>
        /// รายละเอียดของ Status เช่น ลูกค้าไม่รับสาย
        /// </summary>
        public string StatusDescription { get; set; }
        /// <summary>
        /// ประเภทของ Activity Status
        /// masterdata/api/MasterCenters/DropdownList?masterCenterGroupKey=LeadActivityStatusType
        /// </summary>
        public MST.MasterCenterDropdownDTO LeadActivityStatusType { get; set; }
        /// <summary>
        /// FollowUp หรือ Disqualify
        /// masterdata/api/MasterCenters/DropdownList?masterCenterGroupKey=LeadActivityFollowUpType
        /// </summary>
        public MST.MasterCenterDropdownDTO LeadActivityFollowUpType { get; set; }

        public static LeadActivityStatusDTO CreateFromModel(models.CTM.LeadActivityStatus model)
        {
            if(model != null)
            {
                var result = new LeadActivityStatusDTO
                {
                    Id = model.ID,
                    StatusDescription = model.Description,
                    LeadActivityStatusType = MST.MasterCenterDropdownDTO.CreateFromModel(model.LeadActivityStatusType),
                    LeadActivityFollowUpType = MST.MasterCenterDropdownDTO.CreateFromModel(model.LeadActivityFollowUpType)
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
