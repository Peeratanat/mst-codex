using Base.DTOs.MST;
using Base.DTOs.USR;
using Database.Models;
using Database.Models.SAL;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Base.DTOs.SAL
{
    public class SignContractWorkflowDTO
    {
        /// <summary>
        /// เลขที่สัญญา
        /// </summary>
        public AgreementDropdownDTO Agreement { get; set; }

        /// <summary>
        /// การทำรายการอนุมัติ Sign Contract
        /// </summary>
        public MST.MasterCenterDropdownDTO SignContractAction { get; set; }

        /// <summary>
        /// วันที่ทำรายการ
        /// </summary>
        public DateTime? ActionDate { get; set; }

        /// <summary>
        /// Role ที่ทำรายการ
        /// </summary>
        //public Role ActionByRole { get; set; }

        /// <summary>
        /// ผู้ทำรายการ
        /// </summary>
        public USR.UserDTO ActionByUser { get; set; }

        /// <summary>
        /// หมายเหตุ
        /// </summary>
        public string Remark { get; set; }

        public static SignContractWorkflowDTO CreateFromModel(SignContractWorkflow model)
        {
            if (model != null)
            {   
                var result = new SignContractWorkflowDTO()
                {
                    Agreement = AgreementDropdownDTO.CreateFromModel(model.Agreement),
                    SignContractAction = MasterCenterDropdownDTO.CreateFromModel(model.SignContractAction),
                    ActionDate = model.ActionDate,
                    ActionByUser = UserDTO.CreateFromModel(model.ActionBy),
                    Remark = model.Remark
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
