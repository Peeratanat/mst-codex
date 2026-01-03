using Database.Models.SAL;
using ErrorHandling;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using models = Database.Models;

namespace Base.DTOs.SAL
{
    /// <summary>
    /// ข้อมูลเจ้าของใบขจอง / สัญญา
    /// </summary>
    public class DocumentOwnerDTO
    {
        [Description("UnitID")]
        public Guid? UnitID { get; set; }

        [Description("BookingID")]
        public Guid? BookingID { get; set; }

        [Description("BookingNo")]
        public string BookingNo { get; set; }


        [Description("AgreementID")]
        public Guid? AgreementID { get; set; }

        [Description("AgreementNo")]
        public string AgreementNo { get; set; }


        [Description("IsThaiNationality")]
        public bool IsThaiNationality { get; set; }

        [Description("เลข contact")]
        public string ContactNo { get; set; }

        //[Description("คำนำหน้า (TH)")]
        //public string TitleTH { get; set; }

        //[Description("ชื่อ (TH)")]
        //public string FirstNameTH { get; set; }

        //[Description("ชื่อกลาง (TH)")]
        //public string MiddleNameTH { get; set; }

        //[Description("นามสกุล (TH)")]
        //public string LastNameTH { get; set; }

        //[Description("คำนำหน้า (EN)")]
        //public string TitleEN { get; set; }

        //[Description("ชื่อ (EN)")]
        //public string FirstNameEN { get; set; }

        //[Description("ชื่อกลาง (EN)")]
        //public string MiddleNameEN { get; set; }

        //[Description("นามสกุล (EN)")]
        //public string LastNameEN { get; set; }

        [Description("แหล่งที่มา")]
        public string SourceName { get; set; }

        [Description("ชื่อที่ใช้แสดง")]
        public string DisplayName { get; set; }

        public static DocumentOwnerDTO CreateFromQueryResult(DocumentOwnerQueryResult model)
        {
            if (model != null)
            {
                var result = new DocumentOwnerDTO();

                result.UnitID = model.Booking?.UnitID;

                result.BookingID = model.Booking?.ID;
                result.BookingNo = model.Booking?.BookingNo;
                result.AgreementID = model.Agreement?.ID;
                result.AgreementNo = model.Agreement?.AgreementNo;

                if (model.AgreementOwner != null)
                {
                    result.SourceName = nameof(model.AgreementOwner);
                    result.ContactNo = model.AgreementOwner.ContactNo;

                    result.IsThaiNationality = model.AgreementOwner.IsThaiNationality;

                    result.DisplayName = (model.AgreementOwner.IsThaiNationality) ? model.AgreementOwner.FullnameTH : model.AgreementOwner.FullnameEN;

                    //result.TitleEN = model.AgreementOwner?.ContactTitleEN?.NameEN;
                    //result.FirstNameEN = model.AgreementOwner?.FirstNameEN;
                    //result.MiddleNameEN = model.AgreementOwner?.MiddleNameEN;
                    //result.LastNameEN = model.AgreementOwner?.LastNameEN;

                    //result.TitleTH = model.AgreementOwner?.ContactTitleTH?.Name;
                    //result.FirstNameTH = model.AgreementOwner?.FirstNameTH;
                    //result.MiddleNameTH = model.AgreementOwner?.MiddleNameTH;
                    //result.LastNameTH = model.AgreementOwner?.LastNameTH;

                    //if (model.AgreementOwner.IsThaiNationality)
                    //    result.DisplayName = (!string.IsNullOrEmpty(result.TitleTH) ? result.TitleTH + " " : "")
                    //        + (!string.IsNullOrEmpty(result.FirstNameTH) ? result.FirstNameTH + " " : "")
                    //        + (!string.IsNullOrEmpty(result.MiddleNameTH) ? result.MiddleNameTH + " " : "")
                    //        + (!string.IsNullOrEmpty(result.LastNameTH) ? result.LastNameTH + " " : "");
                    //else
                    //    result.DisplayName = (!string.IsNullOrEmpty(result.TitleEN) ? result.TitleEN + " " : "")
                    //        + (!string.IsNullOrEmpty(result.FirstNameEN) ? result.FirstNameEN + " " : "")
                    //        + (!string.IsNullOrEmpty(result.MiddleNameEN) ? result.MiddleNameEN + " " : "")
                    //        + (!string.IsNullOrEmpty(result.LastNameEN) ? result.LastNameEN + " " : "");
                }
                else if (model.BookingOwner != null)
                {
                    result.SourceName = nameof(model.BookingOwner);
                    result.ContactNo = model.BookingOwner.ContactNo;

                    result.IsThaiNationality = (model.BookingOwner.IsThaiNationality);

                    result.DisplayName = (model.BookingOwner.IsThaiNationality) ? model.BookingOwner.FullnameTH : model.BookingOwner.FullnameEN;

                    //result.TitleEN = model.BookingOwner?.ContactTitleEN?.NameEN;
                    //result.FirstNameEN = model.BookingOwner?.FirstNameEN;
                    //result.MiddleNameEN = model.BookingOwner?.MiddleNameEN;
                    //result.LastNameEN = model.BookingOwner?.LastNameEN;

                    //result.TitleTH = model.BookingOwner?.ContactTitleTH?.Name;
                    //result.FirstNameTH = model.BookingOwner?.FirstNameTH;
                    //result.MiddleNameTH = model.BookingOwner?.MiddleNameTH;
                    //result.LastNameTH = model.BookingOwner?.LastNameTH;

                    //if (model.BookingOwner.IsThaiNationality)
                    //    result.DisplayName = (!string.IsNullOrEmpty(result.TitleTH) ? result.TitleTH + " " : "")
                    //        + (!string.IsNullOrEmpty(result.FirstNameTH) ? result.FirstNameTH + " " : "")
                    //        + (!string.IsNullOrEmpty(result.MiddleNameTH) ? result.MiddleNameTH + " " : "")
                    //        + (!string.IsNullOrEmpty(result.LastNameTH) ? result.LastNameTH + " " : "");
                    //else
                    //    result.DisplayName = (!string.IsNullOrEmpty(result.TitleEN) ? result.TitleEN + " " : "")
                    //        + (!string.IsNullOrEmpty(result.FirstNameEN) ? result.FirstNameEN + " " : "")
                    //        + (!string.IsNullOrEmpty(result.MiddleNameEN) ? result.MiddleNameEN + " " : "")
                    //        + (!string.IsNullOrEmpty(result.LastNameEN) ? result.LastNameEN + " " : "");
                }

                return result;
            }
            else
            {
                return null;
            }
        }
    }

    public class DocumentOwnerQueryResult
    {
        public Booking Booking { get; set; }
        public BookingOwner BookingOwner { get; set; }

        public Agreement Agreement { get; set; }
        public AgreementOwner AgreementOwner { get; set; }
    }
}
