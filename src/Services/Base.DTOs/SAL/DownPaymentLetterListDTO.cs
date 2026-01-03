using Database.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Database.Models.DbQueries.SAL;
using System.Linq;

namespace Base.DTOs.SAL
{
    public class DownPaymentLetterListDTO : BaseDTO
    {
        /// <summary>
        /// เลือก
        /// </summary>
        public bool IsSelected { get; set; }
        /// <summary>
        /// สัญญา
        /// </summary>
        public AgreementDropdownDTO Agreement { get; set; }
        /// <summary>
        /// สถานะตั้งเรื่อง
        /// </summary>
        public string ChangeAgreementOwnerType { get; set; }
        /// <summary>
        /// ปะเภทโครงการ
        /// </summary>
        public string ProjectType { get; set; }
        /// <summary>
        /// รหัสโครงการ
        /// </summary>
        public string ProjectNo { get; set; }
        /// <summary>
        /// โครงการ
        /// </summary>
        public string ProjectNameTH { get; set; }
        /// <summary>
        /// โครงการ
        /// </summary>
        public PRJ.ProjectDropdownDTO Project { get; set; }
        /// <summary>
        /// เลขที่แปลง
        /// </summary>
        public string UnitNo { get; set; }
        /// <summary>
        /// เลขที่แปลง
        /// </summary>
        public Guid? UnitID { get; set; }
        /// <summary>
        /// ชื่อ-นามสกุล
        /// </summary>
        public string DisplayName { get; set; }
        /// <summary>
        /// จำนวนที่ค้างชำระ
        /// </summary>
        public int CountDueAmount { get; set; }
        /// <summary>
        /// ยอดเงินที่ค้างชำระ
        /// </summary>
        public decimal SumDueAmount { get; set; }
        /// <summary>
        /// วันที่รับชำระล่าสุด
        /// </summary>
        public DateTime? LastReceiveDate { get; set; }
        /// <summary>
        /// รูปแบบจดหมายที่ต้องออก
        /// </summary>
        public string NowDownPaymentLetterType { get; set; }
        /// <summary>
        /// รูปแบบจดหมายที่ต้องออก_ID
        /// </summary>
        public Guid? NowDownPaymentLetterID { get; set; }
        /// <summary>
        /// รูปแบบจดหมายที่ต้องออก_Name
        /// </summary>
        public string NowDownPaymentLetterName { get; set; }
        /// <summary>
        /// รูปแบบจดหมายล่าสุด
        /// </summary>
        public string LastDownPaymentLetterType { get; set; }
        /// <summary>
        /// รูปแบบจดหมายล่าสุด ID
        /// </summary>
        public Guid? LastDownPaymentLetterTypeID { get; set; }
        /// <summary>
        /// รูปแบบจดหมายล่าสุด  Name
        /// </summary>
        public string LastDownPaymentLetterTypeName { get; set; }
        /// <summary>
        /// วันที่ตอบรับ/ตีกลับ
        /// </summary>
        public DateTime? LastResponseDate { get; set; }
        /// <summary>
        /// สถานะตอบรับ/ตีกลับ
        /// </summary>
        public string LastLetterStatusName { get; set; }
        /// <summary>
        /// เกิน 12.5% หรือไม่
        /// /// </summary>
        public bool? IsOverTwelvePointFivePercent { get; set; }
        /// <summary>
        /// ประภทที่อยู่ให้ส่งจดหมาย
        /// /// </summary>
        public Guid? ContactAddressTypeID { get; set; }
        /// <summary>
        /// วันที่ออกจดหมาย
        /// /// </summary>
        public DateTime DownPaymentLetterDate { get; set; }
        /// <summary>
        /// งวดที่ค้างงวดแรก
        /// /// </summary>
        public int RemainDownPeriodStart { get; set; }
        /// <summary>
        /// งวดที่ค้างงวดสุดท้าย
        /// /// </summary>
        public int RemainDownPeriodEnd { get; set; }
        /// <summary>
        /// จำนวนครั้งที่ออกจดหมาย CTM2
        /// /// </summary>
        public int CTM2Amount { get; set; }
        /// <summary>
        /// รูปแบบจดหมายนัดโอนล่าสุด
        /// </summary>
        public string LastTransferLetterType { get; set; }
        /// <summary>
        /// รูปแบบจดหมายนัดโอนล่าสุด ID
        /// </summary>
        public Guid? LastTransferLetterTypeID { get; set; }
        /// <summary>
        /// รูปแบบจดหมายนัดโอนล่าสุด  Name
        /// </summary>
        public string LastTransferLetterTypeName { get; set; }
        /// <summary>
        /// รหัสไปรษณีย์
        /// </summary>
        public string PostalCode { get; set; }
        /// <summary>
        /// ประเทศ
        /// </summary>
        public string CountryNameTH { get; set; }
        /// <summary>
        /// เลขที่ Tracking รหัสไปรษณีย์
        /// </summary>
        public string PostTrackingNo { get; set; }
        /// <summary>
        ///รหัส Contact
        /// </summary>
        public Guid ContactID { get; set; }
        /// <summary>
        ///รหัสจดหมายล่าสุด
        /// </summary>
        public Guid? LastDownPaymentLetterID { get; set; }
        /// <summary>
        ///สถานะรอยกเลิก
        /// </summary>
        public string WaitForCancel { get; set; }  
        /// <summary>
        ///สามารถออกจดหมายได้
        /// </summary>
        public bool? CanCreateLetter { get; set; }
        /// <summary>
        ///จำนวนงวด
        /// </summary>
        public int? InstallmentAmount { get; set; }
        /// <summary>
        ///จำนวนครั้งที่ออกจดหมาย CTM2 หลังฉบับยกเลิกล่าสุด
        /// </summary>
        public int? CTM2Amount_2 { get; set; }
        /// <summary>
        ///วันที่จดหมายฉบับยกเลิกล่าสุด
        /// </summary>
        public DateTime? LastCancelDownPaymentLetterDate { get; set; }
        /// <summary>
        ///รหัส Contact ทั้งหมด
        /// </summary>
        public string AllContactID { get; set; }
        /// <summary>
        ///สถานะตั้งเรื่อง (ย้ายแปลง,ยกเลิกสัญญา)
        /// </summary>
        public string AgreementStatusName { get; set; }
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
        ///แจ้งยกเลิกสัญญาไปแล้วและมีการชำระเงินเข้ามา แสดง Highlight (#F6ECBF)
        /// </summary>
        public bool? IsHaveLastReceiveDate { get; set; }

        public decimal? UnknownPaymentAmount { get; set; }
        public DateTime? LastDownPaymentLetterDate { get; set; }

        ///จำนวนงวดที่ค้างในเอกสาร ฉ.2 หรือ ฉ.ยกเลิก
        public int? TotalPeriodOverDue { get; set; }
        ///จำนวนเงินที่ค้างในเอกสาร ฉ.2 หรือ ฉ.ยกเลิก
        public decimal? TotalAmountOverDue { get; set; }

        /// <summary>
        ///จำนวนครั้งที่ออกจดหมาย CTM1 หลังฉบับยกเลิกล่าสุด
        /// </summary>
        public int? CTM1Amount_2 { get; set; }

        /// <summary>
        /// จำนวนครั้งที่ออกจดหมาย CTM1
        /// /// </summary>
        public int CTM1Amount { get; set; }

        public static DownPaymentLetterListDTO CreateFromQuery(dbqDownPaymentLetter model, DatabaseContext db)
        {
            if (model != null)
            {
                var modelAgreement = db.Agreements.Where(e => e.ID == model.AgreementID).FirstOrDefault();
                var modelProjectID = db.Projects.Where(e => e.ID == model.ProjectID).FirstOrDefault();

                DownPaymentLetterListDTO result = new DownPaymentLetterListDTO();

                result.IsSelected = false;
                result.Agreement = AgreementDropdownDTO.CreateFromModel(modelAgreement);
                result.ChangeAgreementOwnerType = model.ChangeAgreementOwnerType;
                result.Project = PRJ.ProjectDropdownDTO.CreateFromModel(modelProjectID);
                result.ProjectNo = model.ProjectNo;
                result.ProjectNameTH = model.ProjectNameTH;
                result.UnitNo = model.UnitNo;
                result.UnitID = model.UnitID;
                result.DisplayName = model.DisplayName;
                result.CountDueAmount = model.CountDueAmount ?? 0;
                result.SumDueAmount = model.SumDueAmount ?? 0;
                result.LastReceiveDate = model.LastReceiveDate;
                result.IsOverTwelvePointFivePercent = model.IsOverTwelvePointFivePercent;
                result.NowDownPaymentLetterType = model.NowDownPaymentLetterType;
                result.NowDownPaymentLetterID = model.NowDownPaymentLetterID;
                result.NowDownPaymentLetterName = model.NowDownPaymentLetterName;
                result.LastDownPaymentLetterType = model.LastDownPaymentLetterType;
                result.LastDownPaymentLetterTypeID = model.LastDownPaymentLetterTypeID;
                result.LastDownPaymentLetterTypeName = model.LastDownPaymentLetterTypeName;
                result.LastResponseDate = model.LastResponseDate;
                result.LastLetterStatusName = model.LastLetterStatusName;
                result.RemainDownPeriodStart = model.RemainDownPeriodStart ?? 0;
                result.RemainDownPeriodEnd = model.RemainDownPeriodEnd ?? 0;
                result.CTM2Amount = model.CTM2Amount ?? 0;
                result.ProjectType = model.ProjectType;
                result.Id = model.AgreementID;
                result.LastTransferLetterType = model.LastTransferLetterType;
                result.LastTransferLetterTypeID = model.LastTransferLetterTypeID;
                result.LastTransferLetterTypeName = model.LastTransferLetterTypeName;
                result.PostalCode = model.PostalCode;
                result.CountryNameTH = model.CountryNameTH;
                result.PostTrackingNo = model.PostTrackingNo;
                result.ContactID = model.ContactID;
                result.LastDownPaymentLetterID = model.LastDownPaymentLetterID;
                result.WaitForCancel = ((model.WaitForCancel.HasValue && model.WaitForCancel.Value) ? "รอยกเลิกสัญญา" : "");
                result.CanCreateLetter = model.CanCreateLetter;
                result.InstallmentAmount = model.InstallmentAmount;
                result.CTM2Amount_2 = model.CTM2Amount_2;
                result.LastCancelDownPaymentLetterDate = model.LastCancelDownPaymentLetterDate;
                result.AllContactID = model.AllContactID;
                result.AgreementStatusName = model.AgreementStatusName;
                result.MailConfirmCancelSendDate = model.MailConfirmCancelSendDate;
                result.MailConfirmCancelResponseDate = model.MailConfirmCancelResponseDate;
                result.MailConfirmCancelResponseTypeName = model.MailConfirmCancelResponseTypeName;
                result.CanCreateMailConfirmCancel = model.CanCreateMailConfirmCancel;
                result.IsHaveLastReceiveDate = model.IsHaveLastReceiveDate;
                result.UnknownPaymentAmount = model.UnknownPaymentAmount;
                result.LastDownPaymentLetterDate = model.LastDownPaymentLetterDate;

                return result;
            }
            else
            {
                return null;
            }
        }
    }
}
