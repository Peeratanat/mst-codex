using Base.DTOs.MST;
using Base.DTOs.PRJ;
using Base.DTOs.SAL.Sortings;
using Database.Models;
using Database.Models.LET;
using Database.Models.MasterKeys;
using Database.Models.SAL;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Database.Models.CTM;
using Database.Models.DbQueries.SAL;

namespace Base.DTOs.SAL
{
    public class TransferLetterDTO : BaseDTO
    {
        /// <summary>
        /// โครงการ
        /// </summary>
        public PRJ.ProjectDropdownDTO Project { get; set; }

        /// <summary>
        /// แปลง
        ///  Project/api/Projects/{projectID}/Units/DropdownList
        /// </summary>
        public PRJ.UnitDropdownDTO Unit { get; set; }


        /// <summary>
        /// สัญญา
        /// </summary>
        public AgreementDropdownDTO Agreement { get; set; }

        /// <summary>
        /// ผู้ทำสัญญา
        /// </summary>
        public string FullName { get; set; }
        //public AgreementOwnerDropdownDTO AgreementOwner { get; set; }

        /// <summary>
        /// สถานะตั้งเรื่อง 
        /// </summary>
        public string UnitWorkFlowStutus { get; set; }

        /// <summary>
        /// จำนวนงวดที่ค้าง 
        /// </summary>
        //public int RemainDownPeriodCount { get; set; }

        /// <summary>
        /// งวดดาวน์เริ่มต้นที่ค้าง 
        /// </summary>
        //public int RemainDownPeriodStart { get; set; }

        /// <summary>
        /// งวดดาวน์สิ้นสุดที่ค้าง 
        /// </summary>
        //public int RemainDownPeriodEnd { get; set; }

        /// <summary>
        /// เลขที่จดหมาย 
        /// </summary>
        public string TransferLetterNo { get; set; }

        /// <summary>
        /// จำนวนเงินที่ค้าง 
        /// </summary>
        //public decimal RemainDownTotalAmount { get; set; }

        /// <summary>
        /// ประเภทจดหมาย 
        /// </summary>
        public MST.MasterCenterDTO TransferLetterType { get; set; }

        /// <summary>
        /// สถานะตอบรับ
        /// </summary>
        public MST.MasterCenterDTO LetterStatus { get; set; }

        /// <summary>
        /// วันที่ตอบรับหรือตีกลับ 
        /// </summary>
        public DateTime? ResponseDate { get; set; }

        /// <summary>
        /// ประเภทเหตุผล
        /// </summary>
        public MST.MasterCenterDTO LetterReasonResponse { get; set; }

        /// <summary>
        /// วันที่ออกจดหมาย 
        /// </summary>
        public DateTime TransferLetterDate { get; set; }

        /// <summary>
        /// เลขที่พัสดุ 
        /// </summary>
        public MST.PostTrackingDTO PostTracking { get; set; }

        /// <summary>
        /// แสดงสัญลักษณ์แปลงที่ยังไม่มีข้อมูลตอบรับ 
        /// ระบบมีหน้าจอแสดงสัญลักษณ์ที่จดหมายที่ยังไม่มีข้อมูลการตอบรับ เกิน 14 วัน นับจากวันที่ออกจดหมาย (รวมวันเสาร์-อาทิตย์)
        /// </summary>
        public bool? IsNotResponse { get; set; }

        /// <summary>
        /// ที่อยู่ทีออกจดหมาย 
        /// </summary>
        public MasterCenterDTO ContactAddressType { get; set; }

        /// <summary>
        /// เกิน 12.5% หรือไหม
        /// </summary>
        //public bool? IsOverTwelvePointFivePercent { get; set; }

        /// <summary>
        ///หมายเหตุ
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// วันที่นัดโอนในสัญญา
        /// /// </summary>
        public DateTime? TransferOwnershipDate { get; set; }

        /// <summary>
        /// วันที่นัดโอนในจดหมาย
        /// /// </summary>
        public DateTime? AppointmentTransferDate { get; set; }

        /// <summary>
        /// เวลานัดโอนในจดหมาย
        /// /// </summary>
        public DateTime? AppointmentTransferTime { get; set; }

        /// <summary>
        /// วันที่นัดโอนในบันทีกโอนสิทธิ์
        /// /// </summary>
        public DateTime? NewTransferOwnershipDate { get; set; }

        /// <summary>
        /// วันที่นัดโอนตามใบบันทึกเลื่อนโอน
        /// /// </summary>
        public DateTime? PostponeTransferDate { get; set; }

        /// <summary>
        /// วันที่ส่ง Email
        /// /// </summary>
        public DateTime? MailConfirmCancelSendDate { get; set; }

        /// <summary>
        /// วันที่ LCM ตอบรับ
        /// /// </summary>
        public DateTime? MailConfirmCancelResponseDate { get; set; }

        /// <summary>
        ///สถานะการตอบรับ
        /// </summary>
        public string MailConfirmCancelResponseTypeName { get; set; }

        /// <summary>
        ///สามารถออก Email แจ้ง LCM ออกจดหมายยกเลิก
        /// </summary>
        public bool? CanCreateMailConfirmCancel { get; set; }

        public static TransferLetterDTO CreateFromQueryResult(TransferLetterQueryResult model, DatabaseContext DB)
        {
            try
            {
                if (model != null)
                {
                    //var flowUnit = DB.ChangeUnitWorkflows
                    //                  .Where(o => o.FromAgreementID == model.TransferLetter.AgreementID
                    //                          && (!o.IsApproved.HasValue || (o.IsApproved.HasValue && !o.IsApproved.Value))).FirstOrDefault();

                    var result = new TransferLetterDTO()
                    {
                        Id = model.TransferLetter.ID,
                        Project = ProjectDropdownDTO.CreateFromModel(model.TransferLetter.Agreement.Project),
                        Unit = UnitDropdownDTO.CreateFromModel(model.TransferLetter.Agreement.Unit),
                        Agreement = AgreementDropdownDTO.CreateFromModel(model.Agreement),
                        FullName = model.AgreementOwner.FullnameTH,
                        TransferOwnershipDate = model.Agreement?.TransferOwnershipDate,
                        AppointmentTransferDate = model.TransferLetter?.AppointmentTransferDate,
                        NewTransferOwnershipDate = model.ChangeAgreementOwnerWorkflow?.NewTransferOwnershipDate,
                        TransferLetterType = MasterCenterDTO.CreateFromModel(model.TransferLetter.TransferLetterType),
                        PostTracking = PostTrackingDTO.CreateFromModel(model.TransferLetter.PostTracking),
                        LetterStatus = MasterCenterDTO.CreateFromModel(model.TransferLetter.LetterStatus),
                        ResponseDate = model.TransferLetter.ResponseDate,
                        LetterReasonResponse = MasterCenterDTO.CreateFromModel(model.TransferLetter.LetterReasonResponse),
                        TransferLetterDate = model.TransferLetter.TransferLetterDate,
                        PostponeTransferDate = model.PostponeTransfer?.PostponeTransferDate
                    };

                    //if (flowUnit != null)
                    //{
                    //    result.UnitWorkFlowStutus = "ย้ายแปลง";
                    //}
                    //else if (model.ChangeAgreementOwnerWorkflow != null)
                    //{
                    //    if (model.ChangeAgreementOwnerWorkflow.ChangeAgreementOwnerType.Key == "1")
                    //    {
                    //        result.UnitWorkFlowStutus = "เพิ่มชื่อ";
                    //    }
                    //    else if (model.ChangeAgreementOwnerWorkflow.ChangeAgreementOwnerType.Key == "2")
                    //    {
                    //        result.UnitWorkFlowStutus = "สละชื่อ";
                    //    }
                    //    else if (model.ChangeAgreementOwnerWorkflow.ChangeAgreementOwnerType.Key == "3")
                    //    {
                    //        result.UnitWorkFlowStutus = "โอนสิทธิ์";
                    //    }
                    //}

                    return result;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                var x = ex.Message;
                return null;
            }
        }

        public static TransferLetterDTO CreateFromQuery(dbqTransferLetterHistory model, DatabaseContext DB)
        {
            if (model != null)
            {
                var modelProject = DB.Projects.Include(o => o.ProductType).Where(o => o.ID == model.ProjectID).FirstOrDefault();
                var modelUnit = DB.Units.Where(o => o.ID == model.UnitID).FirstOrDefault();
                var modelAgreement = DB.Agreements.Where(o => o.ID == model.AgreementID).FirstOrDefault();

                var result = new TransferLetterDTO()
                {
                    Id = model.TransferLetterID,
                    Project = ProjectDropdownDTO.CreateFromModel(modelProject),
                    Unit = UnitDropdownDTO.CreateFromModel(modelUnit),
                    Agreement = AgreementDropdownDTO.CreateFromModel(modelAgreement),
                    FullName = model.FullnameTH,
                    TransferOwnershipDate = model.TransferOwnershipDate,
                    AppointmentTransferDate = model.AppointmentTransferDate,
                    NewTransferOwnershipDate = model.NewTransferOwnershipDate,
                    TransferLetterType = new MasterCenterDTO()
                    {
                        Id = model.TransferLetterTypeID,
                        Key = model.TransferLetterTypeKey,
                        Name = model.TransferLetterTypeName
                    },
                    PostTracking = new PostTrackingDTO()
                    {
                        Id = model.PostTrackingID,
                        PostTrackingNo = model.PostTrackingNo,
                        IsUsed = model.IsUsed
                    },
                    LetterStatus = new MasterCenterDTO()
                    {
                        Id = model.LetterStatusID,
                        Key = model.LetterStatusKey,
                        Name = model.LetterStatusName
                    },
                    LetterReasonResponse = new MasterCenterDTO()
                    {
                        Id = model.LetterReasonResponseID,
                        Key = model.LetterReasonResponseKey,
                        Name = model.LetterReasonResponseName,
                        NameEN = model.LetterReasonResponseNameEN,
                        FullName = model.LetterReasonResponseName + ((!string.IsNullOrEmpty(model.LetterReasonResponseNameEN)) ? "/" + model.LetterReasonResponseNameEN : ""),
                    },
                    ResponseDate = model.ResponseDate,
                    TransferLetterDate = model.TransferLetterDate.Value,
                    PostponeTransferDate = model.PostponeTransferDate,
                    MailConfirmCancelResponseDate = model.MailConfirmCancelResponseDate,
                    MailConfirmCancelSendDate = model.MailConfirmCancelSendDate,
                    MailConfirmCancelResponseTypeName = model.MailConfirmCancelResponseTypeName,
                    CanCreateMailConfirmCancel = model.CanCreateMailConfirmCancel
            };

                return result;
            }
            else
            {
                return null;
            }
        }

        public static TransferLetterDTO CreateFromModel(TransferLetter model, DatabaseContext DB)
        {
            try
            {
                if (model != null)
                {
                    var owner = DB.AgreementOwners.Where(o => o.AgreementID == model.AgreementID && o.IsMainOwner == true).FirstOrDefault();
                    //var flowUnit = DB.ChangeUnitWorkflows
                    //                  .Where(o => o.FromAgreementID == model.AgreementID
                    //                          && (!o.IsApproved.HasValue || (o.IsApproved.HasValue && !o.IsApproved.Value))).FirstOrDefault();
                    //var flowOwnerAgreement = DB.ChangeAgreementOwnerWorkflows
                    //                          .Include(o => o.ChangeAgreementOwnerType)
                    //                          .Where(o => o.AgreementID == model.AgreementID
                    //                                  && (!o.IsApproved.HasValue || (o.IsApproved.HasValue && !o.IsApproved.Value))).FirstOrDefault();

                    var result = new TransferLetterDTO()
                    {
                        Id = model.ID,
                        Project = ProjectDropdownDTO.CreateFromModel(model.Agreement.Project),
                        Unit = UnitDropdownDTO.CreateFromModel(model.Agreement.Unit),
                        Agreement = AgreementDropdownDTO.CreateFromModel(model.Agreement),
                        FullName = owner.FullnameTH,
                        TransferOwnershipDate = model.Agreement.TransferOwnershipDate,
                        AppointmentTransferDate = model.AppointmentTransferDate,
                        TransferLetterType = MasterCenterDTO.CreateFromModel(model.TransferLetterType),
                        PostTracking = PostTrackingDTO.CreateFromModel(model.PostTracking),
                        LetterStatus = MasterCenterDTO.CreateFromModel(model.LetterStatus),
                        ResponseDate = model.ResponseDate,
                        LetterReasonResponse = MasterCenterDTO.CreateFromModel(model.LetterReasonResponse),
                        TransferLetterDate = model.TransferLetterDate
                    };

                    //if (flowUnit != null)
                    //{
                    //    result.UnitWorkFlowStutus = "ย้ายแปลง";
                    //}
                    //else if (flowOwnerAgreement != null)
                    //{
                    //    result.NewTransferOwnershipDate = flowOwnerAgreement.NewTransferOwnershipDate;

                    //    if (flowOwnerAgreement.ChangeAgreementOwnerType.Key == "1")
                    //    {
                    //        result.UnitWorkFlowStutus = "เพิ่มชื่อ";
                    //    }
                    //    else if (flowOwnerAgreement.ChangeAgreementOwnerType.Key == "2")
                    //    {
                    //        result.UnitWorkFlowStutus = "สละชื่อ";
                    //    }
                    //    else if (flowOwnerAgreement.ChangeAgreementOwnerType.Key == "3")
                    //    {
                    //        result.UnitWorkFlowStutus = "โอนสิทธิ์";
                    //    }
                    //}

                    return result;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                var x = ex.Message;
                return null;
            }
        }

        public static void SortBy(TransferLetterListSortByParam sortByParam, ref IQueryable<TransferLetterQueryResult> query)
        {
            IOrderedQueryable<TransferLetterQueryResult> orderQuery;
            if (sortByParam.SortBy != null)
            {
                switch (sortByParam.SortBy.Value)
                {
                    case TransferLetterListSortBy.UnitNo:
                        if (sortByParam.Ascending) orderQuery = query.OrderBy(o => o.TransferLetter.Agreement.ProjectID).ThenBy(o => o.TransferLetter.Agreement.Unit.UnitNo);
                        else orderQuery = query.OrderByDescending(o => o.TransferLetter.Agreement.Unit.UnitNo);
                        break;
                    case TransferLetterListSortBy.FullName:
                        if (sortByParam.Ascending) orderQuery = query.OrderBy(o => o.TransferLetter.Agreement.ProjectID).ThenBy(o => o.AgreementOwner.FirstNameTH);
                        else orderQuery = query.OrderByDescending(o => o.AgreementOwner.FirstNameTH);
                        break;
                    case TransferLetterListSortBy.TransferOwnershipDate:
                        if (sortByParam.Ascending) orderQuery = query.OrderBy(o => o.TransferLetter.Agreement.ProjectID).ThenBy(o => o.Agreement.TransferOwnershipDate);
                        else orderQuery = query.OrderByDescending(o => o.Agreement.TransferOwnershipDate);
                        break;
                    case TransferLetterListSortBy.AppointmentTransferDate:
                        if (sortByParam.Ascending) orderQuery = query.OrderBy(o => o.TransferLetter.Agreement.ProjectID).ThenBy(o => o.TransferLetter.AppointmentTransferDate);
                        else orderQuery = query.OrderByDescending(o => o.TransferLetter.AppointmentTransferDate);
                        break;
                    case TransferLetterListSortBy.NewTransferOwnershipDate:
                        if (sortByParam.Ascending) orderQuery = query.OrderBy(o => o.TransferLetter.Agreement.ProjectID).ThenBy(o => o.ChangeAgreementOwnerWorkflow.NewTransferOwnershipDate);
                        else orderQuery = query.OrderByDescending(o => o.ChangeAgreementOwnerWorkflow.NewTransferOwnershipDate);
                        break;
                    case TransferLetterListSortBy.TransferLetterType:
                        if (sortByParam.Ascending) orderQuery = query.OrderBy(o => o.TransferLetter.Agreement.ProjectID).ThenBy(o => o.TransferLetter.TransferLetterType.Key);
                        else orderQuery = query.OrderByDescending(o => o.TransferLetter.TransferLetterType.Key);
                        break;
                    case TransferLetterListSortBy.PostponeTransferDate:
                        if (sortByParam.Ascending) orderQuery = query.OrderBy(o => o.TransferLetter.Agreement.ProjectID).ThenBy(o => o.PostponeTransfer.PostponeTransferDate);
                        else orderQuery = query.OrderByDescending(o => o.PostponeTransfer.PostponeTransferDate);
                        break;
                    case TransferLetterListSortBy.TransferLetterDate:
                        if (sortByParam.Ascending) orderQuery = query.OrderBy(o => o.TransferLetter.Agreement.ProjectID).ThenBy(o => o.TransferLetter.TransferLetterDate);
                        else orderQuery = query.OrderByDescending(o => o.TransferLetter.TransferLetterDate);
                        break;
                    case TransferLetterListSortBy.ResponseDate:
                        if (sortByParam.Ascending) orderQuery = query.OrderBy(o => o.TransferLetter.Agreement.ProjectID).ThenBy(o => o.TransferLetter.ResponseDate);
                        else orderQuery = query.OrderByDescending(o => o.TransferLetter.ResponseDate);
                        break;
                    default:
                        orderQuery = query.OrderBy(o => o.TransferLetter.Agreement.ProjectID).ThenBy(o => o.TransferLetter.Agreement.Unit.UnitNo);
                        break;
                }
            }
            else
            {
                orderQuery = query.OrderBy(o => o.TransferLetter.Agreement.ProjectID).ThenBy(o => o.TransferLetter.Agreement.Unit.UnitNo);
            }

            orderQuery.ThenBy(o => o.TransferLetter.ID);
            query = orderQuery;
        }

        public class TransferLetterQueryResult
        {
            public TransferLetter TransferLetter { get; set; }
            public Agreement Agreement { get; set; }
            public AgreementOwner AgreementOwner { get; set; }
            public ChangeAgreementOwnerWorkflow ChangeAgreementOwnerWorkflow { get; set; }
            public ContactAddress ContactAddress { get; set; }
            public PostponeTransfer PostponeTransfer { get; set; }
        }

        public void ToModel(ref TransferLetter model)
        {
            model = model ?? new TransferLetter();

            model.AgreementID = this.Agreement.Id.Value;
            //model.RemainDownTotalAmount = this.RemainDownTotalAmount;
            //model.RemainDownPeriodCount = this.RemainDownPeriodCount;
            //model.RemainDownPeriodStart = this.RemainDownPeriodStart;
            //model.RemainDownPeriodEnd = this.RemainDownPeriodEnd;
            model.TransferLetterNo = this.TransferLetterNo;
            model.ContactAddressTypeMasterCenterID = this.ContactAddressType.Id;
            model.TransferLetterDate = this.TransferLetterDate;
            model.TransferLetterTypeMasterCenterID = this.TransferLetterType.Id.Value;
            //model.IsOverTwelvePointFivePercent = this.IsOverTwelvePointFivePercent;
            //model.LetterDate = this.LetterDate;
            //model.LetterStatusMasterCenterID = this.LetterStatusMasterCenterID;
            //model.LetterReasonResponseMasterCenterID = this.LetterReasonResponseMasterCenterID;
            //model.LetterTime = this.LetterTime;
            //model.Remark = this.Remark;
            //model.ResponseDate = this.ResponseDate;
            //model.PostTrackingNo = this.PostTrackingNo;
            model.AppointmentTransferDate = this.AppointmentTransferDate;
            model.AppointmentTransferTime = this.AppointmentTransferTime;
        }
    }
}
