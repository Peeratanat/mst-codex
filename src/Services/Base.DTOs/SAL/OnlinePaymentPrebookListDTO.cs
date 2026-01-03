using Base.DTOs.MST;
using Base.DTOs.SAL.Sortings;
using Database.Models;
using Database.Models.CTM;
using Database.Models.FIN;
using Database.Models.MST;
using Database.Models.PRJ;
using Database.Models.SAL;
using Microsoft.EntityFrameworkCore;
using System;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace Base.DTOs.SAL
{
    /// <summary>
    /// รายการ OnlinePayment Prebook
    /// </summary>
    public class OnlinePaymentPrebookListDTO
    {
        /// <summary>
        /// ID ของ OnlinePaymentPrebook
        /// </summary>
        public Guid? Id { get; set; }
        public Guid? QuotationID { get; set; }
        public string QuotationNo { get; set; }
        public DateTime? PaymentDate { get; set; }
        public string PaymentChannel { get; set; }
        public string CustomerName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string ProjectName { get; set; }
        public string UnitNo { get; set; }
        public string ContactNo { get; set; }
        public MasterCenterDropdownDTO UnitStatus { get; set; }  
        public decimal? TotalAmount { get; set; }
        public string PaymentStatus { get; set; }
        public DateTime? ExpireDateTime { get; set; }
        public string BookingNo { get; set; }
        public string SourceCardMasking { get; set; }
        public string PaymentType { get; set; }
        public string Remark { get; set; }
        public string CancelRemark { get; set; }
        public string SourceBrand { get; set; }
        public string SourceIssuerBank { get; set; }

        public static  OnlinePaymentPrebookListDTO CreateFromQueryResult(OnlinePaymentQueryResult model , DatabaseContext db)
        {
            if (model != null)
            {
                OnlinePaymentPrebookListDTO result = new OnlinePaymentPrebookListDTO()
                { 
                    Id = model.OnlinePaymentPrebook.ID,
                    QuotationID = model.Quotation?.ID ,
                    QuotationNo = model.Quotation?.IsDeleted == false ?  model.Quotation?.QuotationNo : "",
                    PaymentDate = model.OnlinePaymentPrebook.PaymentDate,
                    PaymentChannel = model.OnlinePaymentPrebook.PaymentChannel,
                    CustomerName = model.CustomerName,
                    PhoneNumber = model.OnlinePaymentPrebook.PhoneNumber,
                    Email = model.OnlinePaymentPrebook.Email,
                    ProjectName = model.ProjectName,
                    UnitNo = model.UnitNo,
                    ContactNo = model.ContactNo,
                    UnitStatus = MasterCenterDropdownDTO.CreateFromModel(model.UnitStatus),
                    TotalAmount = model.OnlinePaymentPrebook.TotalAmount,
                    ExpireDateTime = !string.IsNullOrEmpty( model.OnlinePaymentHistory?.Expire_Time) ?  DateTime.ParseExact(model.OnlinePaymentHistory.Expire_Time , "yyyyMMddHHmmss", CultureInfo.GetCultureInfo("en-US")) : DateTime.Now ,
                    PaymentStatus = model.OnlinePaymentHistory?.Payment_Status,
                    BookingNo = model.Booking?.BookingNo, 
                    SourceCardMasking = model.OnlinePaymentHistory?.SourceCardMasking,
                    PaymentType = "card".Equals(model.OnlinePaymentHistory?.SourceObject ) ? "บัตรเครดิต": ("qr".Equals(model.OnlinePaymentHistory?.SourceObject)  ? "QR Code" : ""),
                    Remark = model.OnlinePaymentPrebook.Remark,
                    CancelRemark = model.OnlinePaymentPrebook.CancelRemark,
                    SourceBrand = "card".Equals(model.OnlinePaymentHistory?.SourceObject) ? model.OnlinePaymentHistory?.SourceBrand : "" ,
                    SourceIssuerBank = model.OnlinePaymentHistory?.SourceIssuerBank
                };
                if (model.OnlinePaymentPrebook?.IsCancel ?? false)
                {
                    result.PaymentStatus = "ยึด";
                }
                if(!(model.OnlinePaymentPrebook?.IsCancel ?? false))
                {
                    var quotationDelete =  db.Quotations.IgnoreQueryFilters().Where(x => x.RefOnlinePaymentPrebookID == model.OnlinePaymentPrebook.ID).OrderByDescending(x=>x.Created).FirstOrDefault();
                    if(quotationDelete != null)
                    {
                        var bookingCancel =  db.Bookings.Where(x => x.IsCancelled == true && x.QuotationID == quotationDelete.ID).OrderByDescending(x=>x.Created).FirstOrDefault();
                        if (bookingCancel != null )
                        {
                            result.QuotationID = null;
                            if (bookingCancel.CancelType == BookingCancelType.Cancel)
                            {
                                result.BookingNo = bookingCancel.BookingNo + " (ยกเลิกจอง)";
                            }
                            else if (bookingCancel.CancelType == BookingCancelType.CancelByChangeUnit)
                            {
                                result.BookingNo = bookingCancel.BookingNo + " (ย้ายแปลง)";
                            }
                            else if (bookingCancel.CancelType == BookingCancelType.CancelByCancelContract)
                            {
                                result.BookingNo = bookingCancel.BookingNo + " (ยกเลิกสัญญา)";
                            }
                        }
                    }
                    
                }
                if(model.Booking?.ID != null && string.IsNullOrEmpty( model.Booking?.BookingNo) && model.Booking?.IsCancelled == false)
                {
                    result.BookingNo =  "รอยืนยันจอง";
                }
                return result;
            }
            else
            {
                return null;
            }
        }
        public static void SortBy(OnlinePaymentPrebookListSortByParam sortByParam, ref IQueryable<OnlinePaymentQueryResult> query)
        {
            if (sortByParam.SortBy != null)
            {
                switch (sortByParam.SortBy.Value)
                {
                    case OnlinePaymentPerbookListSortBy.PaymentDate:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.OnlinePaymentPrebook.PaymentDate);
                        else query = query.OrderByDescending(o => o.OnlinePaymentPrebook.PaymentDate);
                        break;
                    case OnlinePaymentPerbookListSortBy.PaymentChannel:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.OnlinePaymentPrebook.PaymentChannel);
                        else query = query.OrderByDescending(o => o.OnlinePaymentPrebook.PaymentChannel);
                        break;
                    case OnlinePaymentPerbookListSortBy.CustomerName:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.OnlinePaymentPrebook.TitleName).ThenBy(o=> o.OnlinePaymentPrebook.FirstName).ThenBy(o=> o.OnlinePaymentPrebook.LastName);
                        else query = query.OrderByDescending(o => o.OnlinePaymentPrebook.TitleName).ThenByDescending(o => o.OnlinePaymentPrebook.FirstName).ThenByDescending(o => o.OnlinePaymentPrebook.LastName);
                        break;
                    case OnlinePaymentPerbookListSortBy.PhoneNumber:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.OnlinePaymentPrebook.PhoneNumber);
                        else query = query.OrderByDescending(o => o.OnlinePaymentPrebook.PhoneNumber);
                        break;
                    case OnlinePaymentPerbookListSortBy.ProjectName:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.Project.ProjectNo ).ThenBy(o=> o.Project.ProjectNameTH);
                        else query = query.OrderByDescending(o => o.Project.ProjectNo).ThenByDescending(o => o.Project.ProjectNameTH);
                        break;
                    case OnlinePaymentPerbookListSortBy.UnitNo:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.Unit.UnitNo);
                        else query = query.OrderByDescending(o => o.Unit.UnitNo);
                        break;
                    case OnlinePaymentPerbookListSortBy.UnitStatus:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.UnitStatus.Name);
                        else query = query.OrderByDescending(o => o.UnitStatus.Name);
                        break;
                    case OnlinePaymentPerbookListSortBy.TotalAmount:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.OnlinePaymentPrebook.TotalAmount);
                        else query = query.OrderByDescending(o => o.OnlinePaymentPrebook.TotalAmount);
                        break;
                    case OnlinePaymentPerbookListSortBy.Email:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.OnlinePaymentPrebook.Email);
                        else query = query.OrderByDescending(o => o.OnlinePaymentPrebook.Email);
                        break;
                    case OnlinePaymentPerbookListSortBy.QuotationNo:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.Quotation.QuotationNo);
                        else query = query.OrderByDescending(o => o.Quotation.QuotationNo);
                        break;
                    case OnlinePaymentPerbookListSortBy.ContractNo:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.ContactNo);
                        else query = query.OrderByDescending(o => o.ContactNo);
                        break;
                    case OnlinePaymentPerbookListSortBy.PaymentStatus:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.OnlinePaymentHistory.Payment_Status);
                        else query = query.OrderByDescending(o => o.OnlinePaymentHistory.Payment_Status);
                        break;
                    case OnlinePaymentPerbookListSortBy.BookingNo:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.Booking.BookingNo);
                        else query = query.OrderByDescending(o => o.Booking.BookingNo);
                        break;
                    case OnlinePaymentPerbookListSortBy.SourceCardMasking:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.OnlinePaymentHistory.SourceCardMasking);
                        else query = query.OrderByDescending(o => o.OnlinePaymentHistory.SourceCardMasking);
                        break;
                    case OnlinePaymentPerbookListSortBy.PaymentType:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.OnlinePaymentHistory.SourceObject);
                        else query = query.OrderByDescending(o => o.OnlinePaymentHistory.SourceObject);
                        break;
                    case OnlinePaymentPerbookListSortBy.SourceBrand:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.OnlinePaymentHistory.SourceBrand);
                        else query = query.OrderByDescending(o => o.OnlinePaymentHistory.SourceBrand);
                        break;
                    case OnlinePaymentPerbookListSortBy.SourceIssuerBank:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.OnlinePaymentHistory.SourceIssuerBank);
                        else query = query.OrderByDescending(o => o.OnlinePaymentHistory.SourceIssuerBank);
                        break;
                    default:
                        query = query.OrderByDescending(o => o.OnlinePaymentPrebook.Created);
                        break;
                }
            }
            else
            {
                query = query.OrderByDescending(o => o.OnlinePaymentPrebook.Created);
            }
        }
    }

    public class OnlinePaymentQueryResult
    {
        public Quotation Quotation { get; set; }
        public OnlinePaymentPrebook OnlinePaymentPrebook { get; set; }
        public Booking Booking { get; set; }
        public MasterCenter UnitStatus { get; set; }
        public Project Project { get; set; }
        public Unit Unit { get; set; }
        public string CustomerName { get; set; }
        public string ProjectName { get; set; }
        public string UnitNo { get; set; }
        public string ContactNo { get; set; }
        //public Contact Contact { get; set; }
        public OnlinePaymentHistory OnlinePaymentHistory { get; set;}
    }
}
