using Database.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Database.Models.DbQueries.SAL;
using System.Linq;

namespace Base.DTOs.SAL
{
    public class TransferLetterListDTO : BaseDTO
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
        /// รูปแบบจดหมายที่ต้องออก
        /// </summary>
        public string NowTransferLetterType { get; set; }
        /// <summary>
        /// รูปแบบจดหมายที่ต้องออก_ID
        /// </summary>
        public Guid? NowTransferLetterID { get; set; }
        /// <summary>
        /// รูปแบบจดหมายที่ต้องออก_Name
        /// </summary>
        public string NowTransferLetterName { get; set; }
        /// <summary>
        /// รูปแบบจดหมายล่าสุด
        /// </summary>
        public string LastTransferLetterType { get; set; }
        /// <summary>
        /// รูปแบบจดหมายล่าสุด ID
        /// </summary>
        public Guid? LastTransferLetterTypeID { get; set; }
        /// <summary>
        /// รูปแบบจดหมายล่าสุด  Name
        /// </summary>
        public string LastTransferLetterTypeName { get; set; }
        /// <summary>
        /// วันที่ตอบรับ/ตีกลับ
        /// </summary>
        public DateTime? LastResponseDate { get; set; }
        /// <summary>
        /// สถานะตอบรับ/ตีกลับ
        /// </summary>
        public string LastLetterStatusName { get; set; }
        /// <summary>
        /// ประภทที่อยู่ให้ส่งจดหมาย
        /// /// </summary>
        public Guid? ContactAddressTypeID { get; set; }
        /// <summary>
        /// วันที่ออกจดหมาย
        /// /// </summary>
        public DateTime? TransferLetterDate { get; set; }
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
        /// วันที่นัดโอนใหม่ตามใบบันทึก
        /// /// </summary>
        public DateTime? NewTransferOwnershipDate { get; set; }
        /// <summary>
        /// วันที่นัดโอนตามใบบันทึกเลื่อนโอน
        /// /// </summary>
        public DateTime? PostponeTransferDate { get; set; }

        /// <summary>
        /// รูปแบบจดหมายแจ้งชำระเงินดาวน์ล่าสุด
        /// </summary>
        public string LastDownPaymentLetterType { get; set; }
        /// <summary>
        /// รูปแบบจดหมายแจ้งชำระเงินดาวน์ล่าสุด ID
        /// </summary>
        public Guid? LastDownPaymentLetterTypeID { get; set; }
        /// <summary>
        /// รูปแบบจดหมายแจ้งชำระเงินดาวน์ล่าสุด  Name
        /// </summary>
        public string LastDownPaymentLetterTypeName { get; set; }

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
        public Guid? LastTransferLetterID { get; set; }
        /// <summary>
        ///สามารถออกจดหมายได้
        /// </summary>
        public bool? CanCreateLetter { get; set; }
        /// <summary>
        ///สถานะรอยกเลิก
        /// </summary>
        public string WaitForCancel { get; set; }
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
        public int? CountDueAmount { get; set; }


        public static TransferLetterListDTO CreateFromQuery(dbqTransferLetter model, DatabaseContext db)
        {
            if (model != null)
            {
                var modelAgreement = db.Agreements.Where(e => e.ID == model.AgreementID).FirstOrDefault();

                TransferLetterListDTO result = new TransferLetterListDTO();

                result.IsSelected = false;
                result.Agreement = AgreementDropdownDTO.CreateFromModel(modelAgreement);
                result.ChangeAgreementOwnerType = model.ChangeAgreementOwnerType;

                var prj = db.Projects.Where(e => e.ID == model.ProjectID).FirstOrDefault();

                result.Project = PRJ.ProjectDropdownDTO.CreateFromModel(prj);
                result.ProjectNo = model.ProjectNo;
                result.ProjectNameTH = model.ProjectNameTH;
                result.UnitNo = model.UnitNo;
                result.UnitID = model.UnitID;
                result.DisplayName = model.DisplayName;
                result.NowTransferLetterType = model.NowTransferLetterType;
                result.NowTransferLetterID = model.NowTransferLetterID;
                result.NowTransferLetterName = model.NowTransferLetterName;
                result.LastTransferLetterType = model.LastTransferLetterType;
                result.LastTransferLetterTypeID = model.LastTransferLetterTypeID;
                result.LastTransferLetterTypeName = model.LastTransferLetterTypeName;
                result.LastResponseDate = model.LastResponseDate;
                result.LastLetterStatusName = model.LastLetterStatusName;
                result.ProjectType = model.ProjectType;
                result.TransferOwnershipDate = model.TransferOwnershipDate;
                result.AppointmentTransferDate = model.AppointmentTransferDate;
                result.AppointmentTransferTime = model.AppointmentTransferTime;
                result.Id = model.AgreementID;
                result.LastDownPaymentLetterType = model.LastDownPaymentLetterType;
                result.LastDownPaymentLetterTypeID = model.LastDownPaymentLetterTypeID;
                result.LastDownPaymentLetterTypeName = model.LastDownPaymentLetterTypeName;
                result.PostalCode = model.PostalCode;
                result.CountryNameTH = model.CountryNameTH;
                result.PostTrackingNo = model.PostTrackingNo;
                result.NewTransferOwnershipDate = model.NewTransferOwnershipDate;
                result.PostponeTransferDate = model.PostponeTransferDate;
                result.ContactID = model.ContactID;
                result.LastTransferLetterID = model.LastTransferLetterID;
                result.CanCreateLetter = model.CanCreateLetter;
                result.TransferLetterDate = model.LastTransferLetterDate;
                result.WaitForCancel = ((model.WaitForCancel.HasValue && model.WaitForCancel.Value) ? "รอยกเลิกสัญญา" : "");
                result.AllContactID = model.AllContactID;
                result.AgreementStatusName = model.AgreementStatusName;
                result.MailConfirmCancelSendDate = model.MailConfirmCancelSendDate;
                result.MailConfirmCancelResponseDate = model.MailConfirmCancelResponseDate;
                result.MailConfirmCancelResponseTypeName = model.MailConfirmCancelResponseTypeName;
                result.CanCreateMailConfirmCancel = model.CanCreateMailConfirmCancel;
                result.CountDueAmount = model.CountDueAmount;
                return result;
            }
            else
            {
                return null;
            }
        }
    }
}
