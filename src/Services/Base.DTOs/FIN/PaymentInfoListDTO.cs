using Database.Models.DbQueries.FIN;
using System;

namespace Base.DTOs.FIN
{
    public class PaymentInfoListDTO : BaseDTO
    {

        public Guid ProjectID { get; set; }

        public string ProjectNo { get; set; }
        public string ProjectName { get; set; }
        public Guid UnitID { get; set; }
        public string UnitNo { get; set; }
        public Guid? UnitStatusID { get; set; }
        public string UnitStatusKey { get; set; }

        public string HouseNo { get; set; }

        public string CustomerName { get; set; }

        public Guid? BookingID { get; set; }
        public string BookingNo { get; set; }
        public DateTime? BookingDate { get; set; }

        public Guid? AgreementID { get; set; }
        public string AgreementNo { get; set; }
        public DateTime? AgreementDate { get; set; }

        public Guid? TransferID { get; set; }
        public string TransferNo { get; set; }
        public DateTime? TransferDate { get; set; }

        public bool IsPreTransferPayment { get; set; }

        public static PaymentInfoListDTO CreateFromQueryResult(dbqSPPaymentInfoList model)
        {
            if (model != null)
            {
                var result = new PaymentInfoListDTO()
                {
                    ProjectID = model.ProjectID,
                    ProjectNo = model.ProjectNo,
                    ProjectName = model.ProjectName,
                    UnitID = model.UnitID,
                    UnitNo = model.UnitNo,
                    HouseNo = model.HouseNo,
                    UnitStatusID = model.UnitStatusID,
                    UnitStatusKey = model.UnitStatusKey,

                    BookingID = model.BookingID,
                    BookingNo = model.BookingNo,
                    BookingDate = model.BookingDate,

                    AgreementID = model.AgreementID,
                    AgreementNo = model.AgreementNo,
                    AgreementDate = model.AgreementDate,

                    TransferID = model.TransferID,
                    TransferNo = model.TransferNo,
                    TransferDate = model.TransferDate,

                    CustomerName = model.CustomerName,

                    IsPreTransferPayment = model.IsPreTransferPayment
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
