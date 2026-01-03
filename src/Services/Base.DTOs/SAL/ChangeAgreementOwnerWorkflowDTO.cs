using Database.Models;
using Database.Models.SAL;
using Database.Models.USR;
using Database.Models.MST;
using ErrorHandling;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using models = Database.Models;
using Base.DTOs.MST;
using Base.DTOs.USR;
using Database.Models.MasterKeys;

namespace Base.DTOs.SAL
{
    public class ChangeAgreementOwnerWorkflowDTO : BaseDTO
    {
        /// <summary>
        /// เลขที่สํญญา
        /// </summary>
        public AgreementDTO Agreement { get; set; }

        /// <summary>
        /// วันที่นัดทำรายการ
        /// </summary>
        public DateTime? AppointmentDate { get; set; }


        ///// <summary>
        ///// วันที่ทำรายการ
        ///// </summary>
        //public DateTime? Date { get; set; }

        /// <summary>
        /// วันที่นัดโอนใหม่
        /// </summary>
        public DateTime? NewTransferOwnershipDate { get; set; }


        /// <summary>
        /// เบี้ยปรับ/ค่าธรรมเนียม
        /// </summary>
        public decimal Fee { get; set; }

        /// <summary>
        /// ชนิดของการเปลี่ยนแปลงชื่อผู้ทำสัญญา
        /// </summary>
        public MasterCenterDropdownDTO ChangeAgreementOwnerType { get; set; }

        /// <summary>
        /// ตำแหน่งของผู้อนุมัติตั้งเรื่อง
        /// </summary>
        public Role RequestApproverRole { get; set; }
        /// <summary>
        /// ผู้อนุมัติตั้งเรื่อง
        /// </summary>
        public UserListDTO RequestApproverUser { get; set; }
        /// <summary>
        /// วันที่อนุมัติตั้งเรื่อง
        /// </summary>
        public DateTime? RequestApprovedDate { get; set; }
        /// <summary>
        /// สถานะอนุมัติตั้งเรื่อง
        /// </summary>
        public bool? IsRequestApproved { get; set; }
        /// <summary>
        /// เหตุผลที่ไม่อนุมัติตั้งเรื่อง
        /// </summary>
        public string RequestRejectComment { get; set; }

        /// <summary>
        /// ตำแหน่งของผู้อนุมัติ
        /// </summary>
        public Role ApproverRole { get; set; }
        /// <summary>
        /// ผู้อนุมัติ
        /// </summary>
        public UserListDTO ApproverUser { get; set; }
        /// <summary>
        /// วันที่อนุมัติ
        /// </summary>
        public DateTime? ApprovedDate { get; set; }
        /// <summary>
        /// สถานะอนุมัติ
        /// </summary>
        public bool? IsApproved { get; set; }
        /// <summary>
        /// เหตุผลที่ไม่อนุมัติ
        /// </summary>
        public string RejectComment { get; set; }
        /// <summary>
        /// สถานะอนุมัติพิมพ์เอกสาร
        /// </summary>
        public bool? IsPrintApproved { get; set; }
        /// <summary>
        /// วันที่อนุมัติพิมพ์เอกสาร
        /// </summary>
        public DateTime? PrintApprovedDate { get; set; }
        /// <summary>
        /// ผู้อนุมัติพิมพ์เอกสาร
        /// </summary>
        public UserListDTO PrintApprovedBy { get; set; }

        /// <summary>
        /// เหตุผลที่ไม่เก็บเบี้ยปรับ/ค่าธรรมเนียม
        /// </summary>
        public string NoFeeComment { get; set; }

        /// <summary>
        /// รหัสพนักงานตั้งเรื่อง
        /// </summary>
        public USR.UserListDTO SaleUser { get; set; }

        /// <summary>
        /// สถานะของการเปลี่ยนแปลงชื่อผู้ทำสัญญา
        /// </summary>
        public MasterCenterDropdownDTO ChangeAgreementOwnerStatus { get; set; }

        /// <summary>
        /// วันที่ตั้งเรื่อง
        /// </summary>
        public DateTime? CreateDate { get; set; }

        /// <summary>
        /// รายละเอียดชื่่อ
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// ค่าธรรมเนียมเบี้ยปรับ
        /// </summary>
        //public decimal? ChangeNameFee { get; set; }

        /// <summary>
        /// ชื่อ Agency
        /// </summary>
        public AgentDropdownDTO Agent { get; set; }

        /// <summary>
        /// เชคว่า LC ตั้งเรื่องหรือยัง
        /// </summary>
        public bool IsHaveID { get; set; }

        /// <summary>
        /// เชคว่า AG Appove หรือยัง
        /// </summary>
        public bool AGChang { get; set; }

        /// <summary>
        /// เชคว่า ชำระเงินหรือยัง (แสดงปุ่มชำระเงิน)
        /// </summary>
        public bool IsPayment { get; set; }

        /// <summary>
        /// แสดงปุ่มบันทึกตั้งเรื่อง
        /// </summary>
        public bool IsSave { get; set; }
        /// <summary>
        /// แสดงปุ่มบันทึกข้อมูล(กรณี AG แก้ไข)
        /// </summary>
        public bool IsEdit { get; set; }
        /// <summary>
        /// แสดงปุ่มยกเลิกบันทึกตั้งเรื่อง
        /// </summary>
        public bool IsCancel { get; set; }
        /// <summary>
        /// แสดงปุ่มยกเลิกบันทึกคำขอ
        /// </summary>
        public bool IsCancelRequest { get; set; }
        /// <summary>
        /// แสดงปุ่มอนุมัติพิมพ์
        /// </summary>
        public bool IsApprovePrint { get; set; }
        /// <summary>
        /// แสดงปุ่มพิมพ์ใบบันทึก
        /// </summary>
        public bool IsPrint { get; set; }
        /// <summary>
        /// แสดงปุ่มพิมพ์ใบคำขอ
        /// </summary>
        public bool IsPrintRequest { get; set; }
        /// <summary>
        /// แสดงปุ่มเอกสาร
        /// </summary>
        public bool IsViewFile { get; set; }
        /// <summary>
        /// แสดงปุ่มอนุมัติตั้งเรื่อง
        /// </summary>
        public bool IsApprove { get; set; }
        /// <summary>
        /// แสดงปุ่มอนุมัติคำขอ
        /// </summary>
        public bool IsApproveRequest { get; set; }
        /// <summary>
        /// วันที่ปัจจุบัน
        /// </summary>
        public DateTime? DateNow { get; set; }
        /// <summary>
        /// Master/api/MasterCenters?masterCenterGroupKey=ProductType
        /// ประเภทของโครงการ  (แนวราบ, แนวสูง)
        /// </summary>
        public MST.MasterCenterDropdownDTO ProductType { get; set; }

        public static ChangeAgreementOwnerWorkflowDTO CreateFromModelAsync(ChangeAgreementOwnerWorkflow model, DatabaseContext DB)
        {
            try
            {
                if (model != null)
                {
                    var result = new ChangeAgreementOwnerWorkflowDTO()
                    {
                        Id = model.ID,
                        AppointmentDate = model.AppointmentDate.HasValue ? model.AppointmentDate : DateTime.Now.Date,
                        NewTransferOwnershipDate = model.NewTransferOwnershipDate,
                        Fee = model.Fee,
                        ChangeAgreementOwnerType = MST.MasterCenterDropdownDTO.CreateFromModel(model.ChangeAgreementOwnerType),
                        RequestApproverRole = model.RequestApproverRole,
                        RequestApproverUser = USR.UserListDTO.CreateFromModel(model.RequestApproverUser),
                        RequestApprovedDate = model.RequestApprovedDate,
                        IsRequestApproved = model.IsRequestApproved,
                        RequestRejectComment = model.RequestRejectComment,
                        ApproverRole = model.ApproverRole,
                        ApproverUser = USR.UserListDTO.CreateFromModel(model.ApproverUser),
                        ApprovedDate = model.IsRequestApproved != false? model.ApprovedDate: model.RequestApprovedDate,
                        IsApproved = model.IsApproved,
                        RejectComment = model.RejectComment,
                        NoFeeComment = model.NoFeeComment,
                        SaleUser = USR.UserListDTO.CreateFromModel(model.SaleUser),
                        CreateDate = model.Created.HasValue ? model.Created : DateTime.Now.Date,
                        ChangeAgreementOwnerStatus = MST.MasterCenterDropdownDTO.CreateFromModel(model.ChangeAgreementOwnerStatus),
                        Agent = MST.AgentDropdownDTO.CreateFromModel(model.Agent)
                        //DateNow = DateTime.Now.Date,
                        //ProductType = MasterCenterDropdownDTO.CreateFromModel(model.Agreement.Project.ProductType)
                };

                    var detail = DB.ChangeAgreementOwnerWorkflowDetails
                            .IgnoreQueryFilters()
                            .Include(o => o.AgreementOwner).IgnoreQueryFilters()
                            .Where(o => o.ChangeAgreementOwnerWorkflowID == model.ID).ToList();

                    if (result.ChangeAgreementOwnerType != null)
                    {

                        if (result.ChangeAgreementOwnerType.Key == ChangeAgreementOwnerTypeKeys.AddNewOwner) //เพิ่มชื่อผู้ทำสัญญา
                        {

                            var name = string.Join(Environment.NewLine, detail.Where(o => o.ChangeAgreementOwnerInType == true).Select(o => o.AgreementOwner.FullnameTH).ToList());

                            result.Description = name;
                        }
                        else if (result.ChangeAgreementOwnerType.Key == ChangeAgreementOwnerTypeKeys.RemoveOwner) //สละชื่อผู้ทำสัญญา
                        {
                            var name = string.Join(Environment.NewLine, detail.Where(o => o.ChangeAgreementOwnerInType == false).Select(o => o.AgreementOwner?.FullnameTH).ToList());

                            result.Description = name;
                        }
                        else if (result.ChangeAgreementOwnerType.Key == ChangeAgreementOwnerTypeKeys.TransferOwner) //โอนสิทธิ์ผู้ทำสัญญา
                        {
                            var nameAdd = string.Join(Environment.NewLine, detail.Where(o => o.ChangeAgreementOwnerInType == true).Select(o => o.AgreementOwner?.FullnameTH).ToList());
                            var nameRemove = string.Join(Environment.NewLine, detail.Where(o => o.ChangeAgreementOwnerInType == false).Select(o => o.AgreementOwner?.FullnameTH).ToList());

                            result.Description = "ผู้โอน : " + nameRemove + Environment.NewLine;
                            result.Description += "ผู้รับโอน : " + nameAdd;


                        }
                        else if (result.ChangeAgreementOwnerType.Key == ChangeAgreementOwnerTypeKeys.ChangeMainOwner) //เปลี่ยนผู้ทำสัญญาหลัก
                        {
                            var nameAdd = string.Join(Environment.NewLine, detail.Where(o => o.ChangeAgreementOwnerInType == true).Select(o => o.AgreementOwner?.FullnameTH).ToList());
                            var nameRemove = string.Join(Environment.NewLine, detail.Where(o => o.ChangeAgreementOwnerInType == false).Select(o => o.AgreementOwner?.FullnameTH).ToList());

                            result.Description = "คนเดิม : " + nameRemove + Environment.NewLine;
                            result.Description += "คนใหม่ : " + nameAdd;
                        }
                    }
                    if (result.ApprovedDate == null)
                    {
                        result.IsHaveID = true;
                    }
                    else
                    {
                        result.IsHaveID = false;
                    }

                    return result;
                }
                else
                {
                    var result = new ChangeAgreementOwnerWorkflowDTO();
                    //result.ChangeAgreementOwnerType = new MasterCenterDropdownDTO();
                    //result.ChangeAgreementOwnerType.Key = "1";
                    result.AppointmentDate = DateTime.Now.Date;
                    result.CreateDate = DateTime.Now.Date;
                    result.IsHaveID = false;
                    return result;
                }
            }
            catch (Exception ex)
            {
                var x = ex;
                return null;
            }
        }

        public async Task ValidateAsync(DatabaseContext db)
        {
            ValidateException ex = new ValidateException();
            if (this.ChangeAgreementOwnerType == null)
            {
                var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                string desc = this.GetType().GetProperty(nameof(ChangeAgreementOwnerWorkflowDTO.ChangeAgreementOwnerType)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = errMsg.Message.Replace("[field]", desc);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }

            if (ex.HasError)
            {
                throw ex;
            }
        }

        public void ToModel(ref ChangeAgreementOwnerWorkflow model)
        {
            model.AppointmentDate = this.AppointmentDate;
            model.NewTransferOwnershipDate = this.NewTransferOwnershipDate;
            model.Fee = this.Fee;
            //model.ChangeAgreementOwnerType = this.ChangeAgreementOwnerType;
            model.ChangeAgreementOwnerTypeMasterCenterID = this.ChangeAgreementOwnerType?.Id;
            model.RequestApproverRoleID = this.RequestApproverRole?.ID;
            model.RequestApproverUserID = this.RequestApproverUser?.Id;
            model.RequestApprovedDate = this.RequestApprovedDate;
            model.IsRequestApproved = this.IsRequestApproved;
            model.RequestRejectComment = this.RequestRejectComment;
            model.ApproverRoleID = this.ApproverRole?.ID;
            model.ApproverUserID = this.ApproverUser?.Id;
            model.ApprovedDate = this.ApprovedDate;
            model.IsApproved = this.IsApproved;
            model.RejectComment = this.RejectComment;
            model.AgreementID = this.Agreement.Id;
            model.IsPrintApproved = this.IsPrintApproved;
            model.NoFeeComment = this.NoFeeComment;
            model.SaleUserID = this.SaleUser.Id;
            if (this.Agent?.NameTH!=null)
            { 
              model.AgentID = this.Agent?.Id;
            }
            //model.ChangeAgreementOwnerStatusMasterCenterID = this.ChangeAgreementOwnerStatus?.Id;
        }
    }
}
