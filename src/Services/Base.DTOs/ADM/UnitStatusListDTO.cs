using Database.Models;
using Database.Models.DbQueries.ADM;
using System;
using System.Collections.Generic;
using System.Text;

namespace Base.DTOs.ADM
{
    public class UnitStatusListDTO : BaseDTO
    {
        public string ProjectNo { get; set; }
        public string UnitNo { get; set; }
        public Guid? BookingID { get; set; }
        public string BookingNo { get; set; }
        public DateTime? BookingDate { get; set; }
        public DateTime? BookingApproveDate { get; set; }
        public DateTime? BookingCancelDate { get; set; }
        public Guid? AgreementID { get; set; }
        public string AgreementNo { get; set; }
        public DateTime? ContractDate { get; set; }
        public DateTime? ContractApproveDate { get; set; }
        public DateTime? ContractCancelDate { get; set; }
        public Guid? TransferID { get; set; }
        public string TransferNo { get; set; }
        public DateTime? TransferDate { get; set; }
        public DateTime? ActualTransferDate { get; set; }
        public Guid? TransferPromotionID { get; set; }
        public string TransferPromotionNo { get; set; }
        public DateTime? TransferPromotionDate { get; set; }
        public DateTime? TransferPromotionApproveDate { get; set; }
        public string StatusID { get; set; }
        public string ProjectName { get; set; }
        public string CustomerName { get; set; }
        public string StatusName { get; set; }
        public string ModelID { get; set; }
        public string ModelName { get; set; }

        public static UnitStatusListDTO CreateFromQuery(dbqUnitStatusList model, DatabaseContext db)
        {
            if (model != null)
            {
                UnitStatusListDTO result = new UnitStatusListDTO();

                result.Id = model.UnitID;
                result.ProjectNo = model.ProjectNo;
                result.UnitNo = model.UnitNo;
                result.BookingID = model.BookingID;
                result.BookingNo = model.BookingNo;
                result.BookingDate = model.BookingDate;
                result.BookingApproveDate = model.BookingApproveDate;
                result.BookingCancelDate = model.BookingCancelDate;
                result.AgreementID = model.AgreementID;
                result.AgreementNo = model.AgreementNo;
                result.ContractDate = model.ContractDate;
                result.ContractApproveDate = model.ContractApproveDate;
                result.ContractCancelDate = model.ContractCancelDate;
                result.TransferID = model.TransferID;
                result.TransferNo = model.TransferNo;
                result.TransferDate = model.TransferDate;
                result.ActualTransferDate = model.ActualTransferDate;
                result.TransferPromotionID = model.TransferPromotionID;
                result.TransferPromotionNo = model.TransferPromotionNo;
                result.TransferPromotionDate = model.TransferPromotionDate;
                result.TransferPromotionApproveDate = model.TransferPromotionApproveDate;
                result.StatusID = model.StatusID;
                result.ProjectName = model.ProjectName;
                result.CustomerName = model.CustomerName;
                result.StatusName = model.StatusName;
                result.ModelID = model.ModelID;
                result.ModelName = model.ModelName;

                return result;
            }
            else
            {
                return null;
            }
        }
    }
}
