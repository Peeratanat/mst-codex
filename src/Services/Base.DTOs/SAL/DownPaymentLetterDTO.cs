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
    public class DownPaymentLetterDTO : BaseDTO
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
        public int RemainDownPeriodCount { get; set; }

        /// <summary>
        /// งวดดาวน์เริ่มต้นที่ค้าง 
        /// </summary>
        public int RemainDownPeriodStart { get; set; }

        /// <summary>
        /// งวดดาวน์สิ้นสุดที่ค้าง 
        /// </summary>
        public int RemainDownPeriodEnd { get; set; }

        /// <summary>
        /// เลขที่จดหมาย 
        /// </summary>
        public string DownPaymentLetterNo { get; set; }

        /// <summary>
        /// จำนวนเงินที่ค้าง 
        /// </summary>
        public decimal RemainDownTotalAmount { get; set; }

        /// <summary>
        /// ประเภทจดหมาย 
        /// </summary>
        public MST.MasterCenterDTO DownPaymentLetterType { get; set; }

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
        public DateTime DownPaymentLetterDate { get; set; }

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
        public bool? IsOverTwelvePointFivePercent { get; set; }

        /// <summary>
        ///หมายเหตุ
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        ///จำนวนงวด
        /// </summary>
        public int? InstallmentAmount { get; set; }

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

        /// <summary>
        /// วันที่สร้าง
        /// /// </summary>
        public DateTime? CreateDate { get; set; }

        ///จำนวนงวดที่ค้างในเอกสาร ฉ.2 หรือ ฉ.ยกเลิก
        public int? TotalPeriodOverDue { get; set; }
        ///จำนวนเงินที่ค้างในเอกสาร ฉ.2 หรือ ฉ.ยกเลิก
        public decimal? TotalAmountOverDue { get; set; }

        public static DownPaymentLetterDTO CreateFromQueryResult(DownPaymentLetterQueryResult model, DatabaseContext DB)
        {
            try
            {
                if (model != null)
                {
                    //var flowUnit = DB.ChangeUnitWorkflows
                    //                  .Where(o => o.FromAgreementID == model.DownPaymentLetter.AgreementID
                    //                          && (!o.IsApproved.HasValue || (o.IsApproved.HasValue && !o.IsApproved.Value))).FirstOrDefault();

                    var result = new DownPaymentLetterDTO()
                    {
                        Id = model.DownPaymentLetter.ID,
                        Project = ProjectDropdownDTO.CreateFromModel(model.DownPaymentLetter.Agreement.Project),
                        Unit = UnitDropdownDTO.CreateFromModel(model.DownPaymentLetter.Agreement.Unit),
                        Agreement = AgreementDropdownDTO.CreateFromModel(model.Agreement),
                        FullName = model.AgreementOwner.FullnameTH,
                        RemainDownPeriodCount = model.DownPaymentLetter.RemainDownPeriodCount,
                        RemainDownTotalAmount = model.DownPaymentLetter.RemainDownTotalAmount,
                        DownPaymentLetterType = MasterCenterDTO.CreateFromModel(model.DownPaymentLetter.DownPaymentLetterType),
                        PostTracking = PostTrackingDTO.CreateFromModel(model.DownPaymentLetter.PostTracking),
                        LetterStatus = MasterCenterDTO.CreateFromModel(model.DownPaymentLetter.LetterStatus),
                        ResponseDate = model.DownPaymentLetter.ResponseDate,
                        LetterReasonResponse = MasterCenterDTO.CreateFromModel(model.DownPaymentLetter.LetterReasonResponse),
                        DownPaymentLetterDate = model.DownPaymentLetter.DownPaymentLetterDate,
                        DownPaymentLetterNo = model.DownPaymentLetter.DownPaymentLetterNo,
                        InstallmentAmount = model.InstallmentAmount
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

        public static DownPaymentLetterDTO CreateFromQuery(dbqDownPaymentLetterHistory model, DatabaseContext DB)
        {
            if (model != null)
            {
                var modelProject = DB.Projects.Include(o => o.ProductType).Where(o => o.ID == model.ProjectID).FirstOrDefault();
                var modelUnit = DB.Units.Where(o => o.ID == model.UnitID).FirstOrDefault();
                var modelAgreement = DB.Agreements.Where(o => o.ID == model.AgreementID).FirstOrDefault();

                var result = new DownPaymentLetterDTO()
                {
                    Id = model.DownPaymentLetterID,
                    Project = ProjectDropdownDTO.CreateFromModel(modelProject),
                    Unit = UnitDropdownDTO.CreateFromModel(modelUnit),
                    Agreement = AgreementDropdownDTO.CreateFromModel(modelAgreement),
                    FullName = model.FullnameTH,
                    RemainDownPeriodCount = model.RemainDownPeriodCount,
                    RemainDownTotalAmount = model.RemainDownTotalAmount,
                    InstallmentAmount = model.InstallmentAmount,
                    DownPaymentLetterNo = model.DownPaymentLetterNo,
                    DownPaymentLetterType = new MasterCenterDTO()
                    {
                        Id = model.DownPaymentLetterTypeID,
                        Key = model.DownPaymentLetterTypeKey,
                        Name = model.DownPaymentLetterTypeName
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
                    DownPaymentLetterDate = model.DownPaymentLetterDate.Value,
                    MailConfirmCancelResponseDate = model.MailConfirmCancelResponseDate,
                    MailConfirmCancelSendDate = model.MailConfirmCancelSendDate,
                    MailConfirmCancelResponseTypeName = model.MailConfirmCancelResponseTypeName,
                    CanCreateMailConfirmCancel = model.CanCreateMailConfirmCancel,
                    CreateDate = model.CreateDate,
                    TotalAmountOverDue = model.TotalAmountOverDue,
                    TotalPeriodOverDue = model.TotalPeriodOverDue
                };

                return result;
            }
            else
            {
                return null;
            }
        }

        public static DownPaymentLetterDTO CreateFromModel(DownPaymentLetter model, DatabaseContext DB)
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

                    var result = new DownPaymentLetterDTO()
                    {
                        Id = model.ID,
                        Project = ProjectDropdownDTO.CreateFromModel(model.Agreement.Project),
                        Unit = UnitDropdownDTO.CreateFromModel(model.Agreement.Unit),
                        Agreement = AgreementDropdownDTO.CreateFromModel(model.Agreement),
                        FullName = owner.FullnameTH,
                        RemainDownPeriodCount = model.RemainDownPeriodCount,
                        RemainDownTotalAmount = model.RemainDownTotalAmount,
                        DownPaymentLetterType = MasterCenterDTO.CreateFromModel(model.DownPaymentLetterType),
                        PostTracking = PostTrackingDTO.CreateFromModel(model.PostTracking),
                        LetterStatus = MasterCenterDTO.CreateFromModel(model.LetterStatus),
                        ResponseDate = model.ResponseDate,
                        LetterReasonResponse = MasterCenterDTO.CreateFromModel(model.LetterReasonResponse),
                        DownPaymentLetterDate = model.DownPaymentLetterDate
                    };

                    //if (flowUnit != null)
                    //{
                    //    result.UnitWorkFlowStutus = "ย้ายแปลง";
                    //}
                    //else if (flowOwnerAgreement != null)
                    //{
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

        public static void SortBy(DownPaymentLetterListSortByParam sortByParam, ref IQueryable<DownPaymentLetterQueryResult> query)
        {
            IOrderedQueryable<DownPaymentLetterQueryResult> orderQuery;
            if (sortByParam.SortBy != null)
            {
                switch (sortByParam.SortBy.Value)
                {
                    case DownPaymentLetterListSortBy.UnitNo:
                        if (sortByParam.Ascending) orderQuery = query.OrderBy(o => o.DownPaymentLetter.Agreement.ProjectID).ThenBy(o => o.DownPaymentLetter.Agreement.Unit.UnitNo);
                        else orderQuery = query.OrderByDescending(o => o.DownPaymentLetter.Agreement.Unit.UnitNo);
                        break;
                    case DownPaymentLetterListSortBy.FullName:
                        if (sortByParam.Ascending) orderQuery = query.OrderBy(o => o.DownPaymentLetter.Agreement.ProjectID).ThenBy(o => o.AgreementOwner.FirstNameTH);
                        else orderQuery = query.OrderByDescending(o => o.AgreementOwner.FirstNameTH);
                        break;
                    case DownPaymentLetterListSortBy.RemainDownPeriodCount:
                        if (sortByParam.Ascending) orderQuery = query.OrderBy(o => o.DownPaymentLetter.Agreement.ProjectID).ThenBy(o => o.DownPaymentLetter.RemainDownPeriodCount);
                        else orderQuery = query.OrderByDescending(o => o.DownPaymentLetter.RemainDownPeriodCount);
                        break;
                    case DownPaymentLetterListSortBy.RemainDownTotalAmount:
                        if (sortByParam.Ascending) orderQuery = query.OrderBy(o => o.DownPaymentLetter.Agreement.ProjectID).ThenBy(o => o.DownPaymentLetter.RemainDownTotalAmount);
                        else orderQuery = query.OrderByDescending(o => o.DownPaymentLetter.RemainDownTotalAmount);
                        break;
                    case DownPaymentLetterListSortBy.DownPaymentLetterType:
                        if (sortByParam.Ascending) orderQuery = query.OrderBy(o => o.DownPaymentLetter.Agreement.ProjectID).ThenBy(o => o.DownPaymentLetter.DownPaymentLetterType.Key);
                        else orderQuery = query.OrderByDescending(o => o.DownPaymentLetter.DownPaymentLetterType.Key);
                        break;
                    default:
                        orderQuery = query.OrderBy(o => o.DownPaymentLetter.Agreement.ProjectID).ThenBy(o => o.DownPaymentLetter.Agreement.Unit.UnitNo);
                        break;
                }
            }
            else
            {
                orderQuery = query.OrderBy(o => o.DownPaymentLetter.Agreement.ProjectID).ThenBy(o => o.DownPaymentLetter.Agreement.Unit.UnitNo);
            }

            orderQuery.ThenBy(o => o.DownPaymentLetter.ID);
            query = orderQuery;
        }

        public class DownPaymentLetterQueryResult
        {
            public DownPaymentLetter DownPaymentLetter { get; set; }
            public Agreement Agreement { get; set; }
            public AgreementOwner AgreementOwner { get; set; }
            public ChangeAgreementOwnerWorkflow ChangeAgreementOwnerWorkflow { get; set; }
            public ContactAddress ContactAddress { get; set; }
            public int? InstallmentAmount { get; set; }
        }

        public void ToModel(ref DownPaymentLetter model)
        {
            model = model ?? new DownPaymentLetter();

            model.AgreementID = this.Agreement.Id.Value;
            model.RemainDownTotalAmount = this.RemainDownTotalAmount;
            model.RemainDownPeriodCount = this.RemainDownPeriodCount;
            model.RemainDownPeriodStart = this.RemainDownPeriodStart;
            model.RemainDownPeriodEnd = this.RemainDownPeriodEnd;
            model.DownPaymentLetterNo = this.DownPaymentLetterNo;
            model.ContactAddressTypeMasterCenterID = this.ContactAddressType.Id;
            model.DownPaymentLetterDate = this.DownPaymentLetterDate;
            model.DownPaymentLetterTypeMasterCenterID = this.DownPaymentLetterType.Id.Value;
            model.IsOverTwelvePointFivePercent = this.IsOverTwelvePointFivePercent;
            //model.LetterDate = this.LetterDate;
            //model.LetterStatusMasterCenterID = this.LetterStatusMasterCenterID;
            //model.LetterReasonResponseMasterCenterID = this.LetterReasonResponseMasterCenterID;
            //model.LetterTime = this.LetterTime;
            //model.Remark = this.Remark;
            //model.ResponseDate = this.ResponseDate;
            //model.PostTrackingNo = this.PostTrackingNo;
        }
    }
}
