using Base.DTOs.MST;
using Base.DTOs.PRJ;
using Database.Models;
using Database.Models.DbQueries.FIN;
using Database.Models.FIN;
using Database.Models.PRJ;
using Database.Models.SAL;
using Database.Models.USR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Base.DTOs.FIN
{

    public class UnknownPaymentDetailDTO : BaseDTO
    {
        [Description("ID เงินโอนไม่ทราบผู้โอน")]
        public Guid? UnknownPaymentID { get; set; }

        /// <summary>
        /// เลขที่ PI
        /// </summary>
        [Description("เลขที่ PI")]
        public string UnknownPaymentCode { get; set; }

        /// <summary>
        /// วันที่เงินเข้า
        /// </summary>
        [Description("วันที่เงินเข้า")]
        public DateTime? ReceiveDate { get; set; }

        /// <summary>
        /// บริษัท
        /// </summary>
        [Description("บริษัท")]
        public CompanyDropdownDTO Company { get; set; }

        /// <summary>
        /// โครงการ
        /// </summary>
        public ProjectDTO Project { get; set; }

        /// <summary>
        /// แปลง
        /// </summary>
        public UnitDTO Unit { get; set; }

        /// <summary>
        /// บัญชีธนาคาร filter ตามข้อมูลบริษัท
        /// </summary>
        [Description("บัญชีธนาคาร")]
        public BankAccountDropdownDTO BankAccount { get; set; }

        /// <summary>
        /// เงินตั้งพัก
        /// </summary>
        [Description("เงินตั้งพัก")]
        public decimal UnknownAmount { get; set; }

        /// <summary>
        /// เงินกลับรายการ
        /// </summary>
        [Description("เงินกลับรายการ")]
        public decimal ReverseAmount { get; set; }

        /// <summary>
        /// เงินคงเหลือ
        /// </summary>
        [Description("เงินคงเหลือ")]
        public decimal BalanceAmount { get; set; }

        public List<UnknownPaymentReverseDTO> UnknownPaymentReverseList { get; set; }

        public Guid? BookingID { get; set; }

        public string PaymentMethodType { get; set; }

        public string Number { get; set; }
        public static async Task<UnknownPaymentDetailDTO> CreateFromQueryResultAsync(UnknownPaymentQueryResult model, DatabaseContext DB)
        {
            if (model != null)
            {
                UnknownPaymentDetailDTO result = new UnknownPaymentDetailDTO()
                {
                    UnknownPaymentID = model.UnknownPayment.ID,
                    UnknownPaymentCode = model.UnknownPayment.UnknownPaymentCode,
                    ReceiveDate = model.UnknownPayment.ReceiveDate,
                    Company = CompanyDropdownDTO.CreateFromModel(model.UnknownPayment?.Company),

                    Project = ProjectDTO.CreateFromModel(model.UnknownPayment?.Booking?.Project),
                    Unit = UnitDTO.CreateFromModel(model.UnknownPayment?.Booking?.Unit),

                    BankAccount = BankAccountDropdownDTO.CreateFromModel(model.UnknownPayment?.BankAccount),
                    UnknownAmount = model.UnknownPayment.Amount,
                    BookingID = model.UnknownPayment.BookingID,
                    PaymentMethodType = model.UnknownPayment.PaymentMethodType.Name,
                    Number = !string.IsNullOrEmpty(model.UnknownPayment.Number) ? model.UnknownPayment.Number : model.UnknownPayment.IR
                };

                var ReversedAmount = await DB.PaymentMethods.Include(x=>x.Payment)
                    .Where(o => o.UnknownPaymentID == model.UnknownPayment.ID && o.IsDeleted == false
                    &&  o.Payment.IsCancel == false
                    ).Select(o => o.PayAmount).SumAsync();

                result.ReverseAmount = ReversedAmount;

                result.BalanceAmount = result.UnknownAmount - ReversedAmount;

                var detailQuery = from o in DB.PaymentMethods
                                         .Include(o => o.Payment)
                                             .ThenInclude(o => o.Booking)
                                                 .ThenInclude(o => o.Unit)
                                                     .ThenInclude(o => o.Project)
                                     .Include(o => o.CreatedBy)

                                  join ag in DB.Agreements on o.Payment.Booking.ID equals ag.BookingID into agData
                                  from agModel in agData.DefaultIfEmpty()

                                  join rth in DB.ReceiptTempHeaders on o.Payment.ID equals rth.PaymentID into rthData
                                  from rthModel in rthData.DefaultIfEmpty()

                                  select new UnknownPaymentReverseQueryResult
                                  {
                                      PaymentMethod = o,
                                      Payment = o.Payment,
                                      Booking = o.Payment.Booking,
                                      Project = o.Payment.Booking.Project,
                                      Unit = o.Payment.Booking.Unit,
                                      Agreement = agModel ?? new Agreement(),

                                      ReceiptTempHeader = rthModel ?? new ReceiptTempHeader(),
                                  };

                detailQuery = detailQuery.Where(o => o.PaymentMethod.UnknownPaymentID == result.UnknownPaymentID && o.Payment.IsCancel == false);

                var detailData = detailQuery.ToList();

                result.UnknownPaymentReverseList = new List<UnknownPaymentReverseDTO>();

                var unknownPaymentReverseList = detailData
                    .Select(o => new UnknownPaymentReverseDTO
                    {
                        Id = o.PaymentMethod.ID,
                        ReferentNo = (o.Agreement.AgreementNo ?? "") == "" ? o.Booking.BookingNo : o.Agreement.AgreementNo,
                        ReverseProject = ProjectDTO.CreateFromModel(o.Project),
                        ReverseUnit = UnitDTO.CreateFromModel(o.Unit),
                        ReceiptTempHeaderID = o.ReceiptTempHeader.ID,
                        ReverseDate = o.Payment.ReceiveDate,
                        ReverseAmount = o.PaymentMethod.PayAmount,
                        ReceiptTempNo = o.ReceiptTempHeader.ReceiptTempNo,
                        BookingID = o.Booking.ID,
                        RVNumber = o.Payment.PostGLDocumentNo
                    }).ToList() ?? new List<UnknownPaymentReverseDTO>();

                result.UnknownPaymentReverseList = unknownPaymentReverseList;

                return result;
            }
            else
            {
                return null;
            }
        }

    }

    public class UnknownPaymentReverseDTO : BaseDTO
    {
        /// <summary>
        /// เลขที่จอง/สัญญา
        /// </summary>
        [Description("เลขที่จอง/สัญญา")]
        public string ReferentNo { get; set; }

        /// <summary>
        /// โครงการ
        /// </summary>
        public ProjectDTO ReverseProject { get; set; }

        /// <summary>
        /// แปลง
        /// </summary>
        public UnitDTO ReverseUnit { get; set; }

        /// <summary>
        /// วันที่กลับรายการ
        /// </summary>
        [Description("วันที่กลับรายการ")]
        public DateTime? ReverseDate { get; set; }

        /// <summary>
        /// จำนวนเงินที่กลับรายการ
        /// </summary>
        [Description("จำนวนเงินที่กลับรายการ")]
        public decimal ReverseAmount { get; set; }

        /// <summary>
        /// เลขที่ใบเสร็จ
        /// </summary>
        [Description("เลขที่ใบเสร็จ")]
        public string ReceiptTempNo { get; set; }

        /// <summary>
        /// เลขที่ RVNumber
        /// </summary>
        [Description("เลขที่ RVNumber")]
        public string RVNumber { get; set; }

        /// <summary>
        /// หมายเหตุยกเลิก
        /// </summary>
        [Description("หมายเหตุยกเลิก")]
        public string CancelRemark { get; set; }

        /// <summary>
        /// ReceiptTempHeaderID
        /// </summary>
        public Guid? ReceiptTempHeaderID { get; set; }

        public Guid? BookingID { get; set; }
        public Guid? PaymentID { get; set; }

        /// <summary>
        /// ผู้กลับรายการ
        /// </summary>
        [Description("ผู้กลับรายการ")]
        public string ReverseBy { get; set; }

        public static async Task<UnknownPaymentReverseDTO> CreateFromQuerySPAsync(dbqSPUnknownPaymentList model )
        {
            if (model != null)
            {
                UnknownPaymentReverseDTO result = new UnknownPaymentReverseDTO()
                {
                    ReverseProject = new ProjectDTO { Id = model.ProjectID, ProjectNo = model.ProjectNo, ProjectNameTH = model.ProjectNo + "-" +model.ProjectName },
                    ReverseUnit = new UnitDTO { Id = model.UnitID ?? Guid.Empty, UnitNo = model.UnitNo },
                    ReverseAmount = model.ReverseAmount ?? 0,
                    ReverseBy = model.ReverseName,
                    ReverseDate = model.ReverseDate,
                    RVNumber = model.RVDocumentNo,
                    PaymentID = model.PaymentID
                }; 
                return result;
            }
            else
            {
                return null;
            }
        }
    }

    public class UnknownPaymentReverseQueryResult
    {
        //public PaymentUnknownPayment PaymentUnknownPayment { get; set; }
        public User UserReverse { get; set; }

        public PaymentMethod PaymentMethod { get; set; }
        public Payment Payment { get; set; }

        public Booking Booking { get; set; }
        public Project Project { get; set; }
        public Unit Unit { get; set; }
        public Agreement Agreement { get; set; }

        public ReceiptTempHeader ReceiptTempHeader { get; set; }
    }

}
