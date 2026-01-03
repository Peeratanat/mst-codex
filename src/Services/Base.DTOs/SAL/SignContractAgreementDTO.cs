using Base.DTOs.USR;
using Database.Models;
using Database.Models.MasterKeys;
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
    public class SignContractAgreementDTO
    {
        /// <summary>
        /// สัญญา
        /// </summary>
        public AgreementDropdownDTO Agreement { get; set; }

        /// <summary>
        /// วันที่ขออนุมัติ
        /// </summary>
        public DateTime? SignContractRequestDate { get; set; }

        /// <summary>
        /// ผู้ขออนุมัติ
        /// </summary>
        public USR.UserDTO SignContractRequestUser { get; set; }

        /// <summary>
        /// หมายเหตุขออนุมัติ
        /// </summary>
        public string SignContractRequestRemark { get; set; }


        /// <summary>
        /// วันที่อนุมัติ
        /// </summary>
        public DateTime? SignContractApproveDate { get; set; }

        /// <summary>
        /// ผู้อนุมัติ
        /// </summary>
        public USR.UserDTO SignContractApproveUser { get; set; }

        /// <summary>
        /// สถานะปัจจุบัน
        /// </summary>
        public string CurrentStatus { get; set; }

        /// <summary>
        /// สามารถ Cancel Approve
        /// </summary>
        public bool IsCanCancelApprove { get; set;}
        public bool? IsCombineUnit { get; set;}

        /// <summary>
        /// หมายเหตุอนุมัติ
        /// </summary>
        public string SignContractApproveRemark { get; set; }

        public static async Task<SignContractAgreementDTO> CreateFromModelAsync(Agreement model, DatabaseContext DB)
        {
            if (model != null)
            {

                var signContractApprove = await DB.SignContractWorkflows
                    .Include(e => e.SignContractAction)
                    .Where(e => e.AgreementID == model.ID && e.SignContractAction.Key == SignContractActionKeys.ApproveSignContract)
                    .Include(e => e.ActionBy)
                    .OrderByDescending(e => e.ActionDate)
                    .FirstOrDefaultAsync();

                var cancelSignContractApprove = await DB.SignContractWorkflows
                    .Include(e => e.SignContractAction)
                    .Where(e => e.AgreementID == model.ID && e.SignContractAction.Key == SignContractActionKeys.CancelApproveSignContract)
                    .Include(e => e.ActionBy)
                    .OrderByDescending(e => e.ActionDate)
                    .FirstOrDefaultAsync();

                var signContractRequest = await DB.SignContractWorkflows
                    .Include(e => e.SignContractAction)
                    .Where(e => e.AgreementID == model.ID && e.SignContractAction.Key == SignContractActionKeys.SignContract)
                    .OrderByDescending(e => e.ActionDate)
                    .FirstOrDefaultAsync();

                var currentSignContractWorkflow = await DB.SignContractWorkflows
                    .Include(e => e.SignContractAction)
                    .Where(e => e.AgreementID == model.ID)
                    .OrderByDescending(e => e.Created)
                    .FirstOrDefaultAsync();

                var combineUnit = await DB.CombineUnits.Include(x=>x.CombineStatus).Where(x =>!x.CombineStatus.Key.Equals(MasterCombineStatusKeys.Reject) && ( x.UnitID == model.UnitID || x.UnitIDCombine == model.UnitID)).FirstOrDefaultAsync();
                
                SignContractAgreementDTO result = new SignContractAgreementDTO();

                result.Agreement = AgreementDropdownDTO.CreateFromModel(model);
                result.SignContractRequestDate = model.SignContractRequestDate;
                result.SignContractRequestUser = model.SignContractRequestUser == null ? null : UserDTO.CreateFromModel(model.SignContractRequestUser);
                result.SignContractRequestRemark = signContractRequest?.Remark;
                if (combineUnit != null)
                {
                    result.IsCombineUnit = true; 
                } 
                if (cancelSignContractApprove != null)
                {
                    if (signContractApprove.ActionDate > cancelSignContractApprove.ActionDate)
                    {
                        result.SignContractApproveDate = signContractApprove?.ActionDate;
                        result.SignContractApproveUser = signContractApprove?.ActionBy == null ? null : UserDTO.CreateFromModel(signContractApprove.ActionBy);
                        result.SignContractApproveRemark = signContractApprove?.Remark;
                    }
                    else
                    {
                        result.SignContractApproveDate = null;
                        result.SignContractApproveUser = null;
                        result.SignContractApproveRemark = cancelSignContractApprove?.Remark;
                    }
                }
                else
                {
                    result.SignContractApproveDate = signContractApprove?.ActionDate;
                    result.SignContractApproveUser = signContractApprove?.ActionBy == null ? null : UserDTO.CreateFromModel(signContractApprove.ActionBy);
                    result.SignContractApproveRemark = signContractApprove?.Remark;
                }

                result.CurrentStatus = currentSignContractWorkflow?.SignContractAction?.Key;
                result.IsCanCancelApprove = model.SignContractApprovedDate.HasValue ? 
                    (model.SignContractApprovedDate.Value.Day > 5 && (DateTime.Now <= new DateTime(model.SignContractApprovedDate.Value.Year, model.SignContractApprovedDate.Value.Month, 5).AddMonths(1))) || 
                    (model.SignContractApprovedDate.Value.Day <= 5 && (DateTime.Now <= new DateTime(model.SignContractApprovedDate.Value.Year, model.SignContractApprovedDate.Value.Month,5)))
                    : false;


                return result;
            }
            else
            {
                return null;
            }
        }
    }
}
