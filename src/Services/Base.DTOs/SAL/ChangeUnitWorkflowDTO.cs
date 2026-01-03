using Database.Models;
using Database.Models.PRM;
using Database.Models.SAL;
using FileStorage;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.DTOs.SAL
{
    public class ChangeUnitWorkflowDTO : BaseDTO
    {
        /// <summary>
        /// โครงการ
        /// </summary>
        public PRJ.ProjectDTO Project { get; set; }
     
        /// <summary>
        /// แปลง
        /// </summary>
        public PRJ.UnitDTO Unit { get; set; }

        /// <summary>
        /// แปลงเดิม (จากสัญญา)
        /// </summary>
        public AgreementDTO FromAgreement { get; set; }

        /// <summary>
        /// แปลงใหม่ (ไปที่สัญญา)
        /// </summary>
        public AgreementDTO ToAgreement { get; set; }

        /// <summary>
        /// ชื่อลูกค้า
        /// </summary>
        public AgreementOwnerDropdownDTO AgreementOwner { get; set; }

        ///<summary>
        ///วันที่ตั้งเรื่อง
        ///</summary>
        public DateTime? ChangeUnitWorkflowDate { get; set; }

        ///<summary>
        ///วันที่อนุมัติ
        ///</summary>
        public DateTime? ApprovedDate { get; set; }

        ///<summary>
        ///สถานะอนุมัติ
        ///</summary>
        public bool? IsApproved { get; set; }

        public async static Task<List<ChangeUnitWorkflowDTO>> CreateFromModelAsync(IEnumerable<ChangeUnitWorkflow> model, FileHelper fileHelper, DatabaseContext DB)
        {
            if (model != null)
            {
                List<ChangeUnitWorkflowDTO> result = new List<ChangeUnitWorkflowDTO>();

                foreach (var item in model)
                {
                    var mainOwner = await DB.AgreementOwners.Where(o => o.AgreementID == item.FromAgreementID && o.IsMainOwner).FirstOrDefaultAsync();
                    ChangeUnitWorkflowDTO data = new ChangeUnitWorkflowDTO()
                    {
                        Id = item.ID,
                        ApprovedDate = item.ApprovedDate,
                        FromAgreement = await AgreementDTO.CreateFromModelAsync(item.FromAgreement, fileHelper, DB),
                        ToAgreement = await AgreementDTO.CreateFromModelAsync(item.ToAgreement, fileHelper, DB),
                        ChangeUnitWorkflowDate = item.Created,
                        IsApproved = item.IsApproved,
                        AgreementOwner = AgreementOwnerDropdownDTO.CreateFromModel(mainOwner)
                    };
                    result.Add(data);
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
