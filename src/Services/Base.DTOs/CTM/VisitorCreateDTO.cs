using System;
using System.Collections.Generic;
using System.Text;
using models = Database.Models;

namespace Base.DTOs.CTM
{
    public class VisitorCreateDTO
    {
        /// <summary>
        /// รหัสโครงการ (branch_code)
        /// </summary>
        public string ProjectNo { get; set; }
        /// <summary>
        /// เลขที่บัตรประชาชน (personalid)
        /// </summary>
        public string CitizenIdentityNo { get; set; }
        /// <summary>
        /// คำนำหน้า (ภาษาไทย) (titlename)
        /// </summary>
        public string TitleTH { get; set; }
        /// <summary>
        /// ชื่อจริง (ภาษาไทย) (firstname)
        /// </summary>
        public string FirstNameTH { get; set; }
        /// <summary>
        /// นามสกุล (ภาษาไทย) (lastname)
        /// </summary>
        public string LastNameTH { get; set; }
        /// <summary>
        /// คำนำหน้า (ภาษาอังกฤษ) (ENPrefix)
        /// </summary>
        public string TitleEN { get; set; }
        /// <summary>
        /// ชื่อจริง (ภาษาอังกฤษ) (ENFirstname)
        /// </summary>
        public string FirstNameEN { get; set; }
        /// <summary>
        /// นามสกุล (ภาษาอังกฤษ) (ENLastName)
        /// </summary>
        public string LastNameEN { get; set; }
        /// <summary>
        /// เพศ (gender)
        /// </summary>
        public string Gender { get; set; }
        /// <summary>
        /// วันเกิด (dateofbirth)
        /// </summary>
        public DateTime? BirthDate { get; set; }
        /// <summary>
        /// กรุ๊ปเลือด (bloodtype)
        /// </summary>
        public string BloodType { get; set; }
        /// <summary>
        /// บ้านเลขที่ (address_houseid)
        /// </summary>
        public string HouseNo { get; set; }
        /// <summary>
        /// หมู่ที่ (address_village)
        /// </summary>
        public string Moo { get; set; }
        /// <summary>
        /// หมู่บ้าน/อาคาร (ทีอยู่) (address_fullformated)
        /// </summary>
        public string Village { get; set; }
        /// <summary>
        /// ซอย (address_lane)
        /// </summary>
        public string Soi { get; set; }
        /// <summary>
        /// ถนน (address_road)
        /// </summary>
        public string Road { get; set; }
        /// <summary>
        /// รหัสไปรษณีย์ (address_postcode)
        /// </summary>
        public string PostalCode { get; set; }
        /// <summary>
        /// ประเทศ (address_country)
        /// </summary>
        public string Country { get; set; }
        /// <summary>
        /// จังหวัด (address_province)
        /// </summary>
        public string Province { get; set; }
        /// <summary>
        /// เขต/อำเภอ (address_district)
        /// </summary>
        public string District { get; set; }
        /// <summary>
        /// แขวง/ตำบล (address_subdistrict)
        /// </summary>
        public string SubDistrict { get; set; }
        /// <summary>
        /// สัญชาติ (address_nationality)
        /// </summary>
        public string National { get; set; }
        /// <summary>
        /// สถานที่ออกบัตร (Issuer)
        /// </summary>
        public string Issue { get; set; }
        /// <summary>
        /// วันที่ออกบัตร (CardIssueDate)
        /// </summary>
        public DateTime? IssueDate { get; set; }
        /// <summary>
        /// วันที่บัตรหมดอายุ (CardExpireDate)
        /// </summary>
        public DateTime? IssueExpireDate { get; set; }
        /// <summary>
        /// วันที่เข้าโครงการ (in_date)
        /// </summary>
        public DateTime? VisitDateIn { get; set; }
        /// <summary>
        /// วันที่ออกโครงการ (out_date)
        /// </summary>
        public DateTime? VisitDateOut { get; set; }
        /// <summary>
        /// สถานะ/ประเภทบุคคลที่เข้าโครงการ (personal_visittype)
        /// </summary>
        public string ContactStatusKey { get; set; }
        /// <summary>
        /// ชนิดของบัตรที่ใช้ยืนยันเข้าโครงการ (personal_visitcardtype)
        /// </summary>
        public string PersonalVisitCardTypeKey { get; set; }
        /// <summary>
        /// ข้อมลูรูปถ่ายจากกล้อง (personal_visitcardimage)
        /// </summary>
        public string PersonalVisitImageFromCard { get; set; }
        /// <summary>
        /// ข้อมลูรูปถ่ายจากกล้อง (ชื่อไฟล์)
        /// </summary>
        public string PersonalVisitImageName { get; set; }
        /// <summary>
        /// รูปแบบการเยียมชมโครงการ (customer_visitvia)
        /// </summary>
        public string VisitByKey { get; set; }
        /// <summary>
        /// ประเภทรถยนต์ (vehicle_type)
        /// </summary>
        public string VehicleKey { get; set; }
        /// <summary>
        /// เลขทะเบียนรถ ("vehicle_regist)
        /// </summary>
        public string VehicleRegistrationNo { get; set; }
        /// <summary>
        /// สีรถ (vehicle_color)
        /// </summary>
        public string VehicleColor { get; set; }
        /// <summary>
        /// ยี่ห้อรถ (vehicle_brand)
        /// </summary>
        public string VehicleBrand { get; set; }
        /// <summary>
        /// Transaction ID จากเครื่องรูดบัตร (trans_id)
        /// </summary>
        public string VisitKioskTransactionID { get; set; }
        /// <summary>
        /// Device ID ของเครื่องรูดบัตร (deviceid)
        /// </summary>
        public string VisitKioskDeviceID { get; set; }
        /// <summary>
        /// เลขรับรายการเยี่ยมชมโครงการ (รหัสรันนิงบัตรจาก offline) (printrunning_label)
        /// </summary>
        public string VisitorRunning { get; set; }
        /// <summary>
        /// เบอร์โทรศัพท์
        /// </summary>
        public string PhoneNumber { get; set; }

        public void ToModel(ref models.CTM.Visitor model)
        {
            model.VisitorRunning = this.VisitorRunning;
            model.VisitDateIn = this.VisitDateIn;
            model.VisitDateOut = this.VisitDateOut;
            model.TitleTH = this.TitleTH;
            model.FirstNameTH = this.FirstNameTH;
            model.LastNameTH = this.LastNameTH;
            model.TitleEN = this.TitleEN;
            model.FirstNameEN = this.FirstNameEN;
            model.LastNameEN = this.LastNameEN;
            model.Gender = this.Gender;
            model.BirthDate = this.BirthDate;
            model.BloodType = this.BloodType;
            model.HouseNo = this.HouseNo;
            model.Moo = this.Moo;
            model.Village = this.Village;
            model.Soi = this.Soi;
            model.Road = this.Road;
            model.PostalCode = this.PostalCode;
            model.Country = this.Country;
            model.Province = this.Province;
            model.District = this.District;
            model.SubDistrict = this.SubDistrict;
            model.National = this.National;
            model.Issue = this.Issue;
            model.IssueDate = this.IssueDate;
            model.IssueExpireDate = this.IssueExpireDate;
            model.PhoneNumber = this.PhoneNumber;
            model.VehicleRegistrationNo = this.VehicleRegistrationNo;
            model.VehicleColor = this.VehicleColor;
            model.VehicleBrand = this.VehicleBrand;
            model.VisitKioskTransactionID = this.VisitKioskTransactionID;
            model.VisitKioskDeviceID = this.VisitKioskDeviceID;
            model.CitizenIdentityNo = this.CitizenIdentityNo;
            model.PersonalVisitImageFromCard = this.PersonalVisitImageFromCard;
            model.IDCardImage = this.PersonalVisitImageName;
        }
    }
}
