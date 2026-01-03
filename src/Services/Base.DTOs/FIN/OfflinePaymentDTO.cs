using Database.Models.MST;
using System;
using System.ComponentModel;
using System.Linq;
using Database.Models.FIN;
using Base.DTOs.MST; 
using Base.DTOs.USR;
using Database.Models;
using Base.DTOs.SAL;
using Database.Models.MasterKeys;

namespace Base.DTOs.FIN
{
    public class OfflinePaymentDTO : BaseDTO
    {
        /// <summary>
        /// Link ข้อมูลจอง
        /// </summary>
        [Description("Link ข้อมูลจอง")]
        public SAL.BookingDTO Booking { get; set; }

        /// <summary>
        /// จำนวนเงิน
        /// </summary>
        [Description("จำนวนเงิน")]
        public decimal PayAmount { get; set; }

        /// <summary>
        /// วันที่รับเงิน
        /// </summary>
        [Description("วันที่รับเงิน")]
        public DateTime? ReceiveDate { get; set; }

        /// <summary>
        /// ชื่อผู้รับเงิน
        /// </summary>
        [Description("ชื่อผู้รับเงิน")]
        public string CreateBy { get; set; }

        /// <summary>
        /// เลขที่ใบเสร็จรับเงินชั่วคราว
        /// </summary>
        [Description("เลขที่ใบเสร็จรับเงินชั่วคราว")]
        public string TempReceiptNo { get; set; }

        /// <summary>
        /// เลขที่ใบเสร็จรับเงิน
        /// </summary>
        [Description("เลขที่ใบเสร็จรับเงิน")]
        public string ReceiptNo { get; set; }

        /// <summary>
        /// สถานะ
        /// </summary>
        [Description("สถานะ")]
        public MST.MasterCenterDropdownDTO OfflinePaymentStatusMaster { get; set; }

        /// <summary>
        /// วันที่ยืนยัน
        /// </summary>
        [Description("วันที่ยืนยัน")]
        public DateTime? ConfirmedDate { get; set; }

        /// <summary>
        /// ผู้ยืนยัน
        /// </summary>
        [Description("ผู้ยืนยัน")]
        public USR.UserListDTO ConfirmedBy { get; set; }

        /// <summary>
        /// รายการรับชำระ
        /// </summary>
        [Description("รายการรับชำระ")]
        public MasterPriceItemDTO PaymentTypeDetail { get; set; }

        /// <summary>
        /// ชื่อลูกค้า
        /// </summary>
        [Description("ชื่อลูกค้า")]
        public string PaymentName { get; set; }

        /// <summary>
        /// ชื่อลูกค้า
        /// </summary>
        [Description("รหัสลูกค้า")]
        public string PaymentCode { get; set; }

        /// <summary>
        /// เหตุผลในการยกเลิก
        /// </summary>
        [Description("เหตุผลในการยกเลิก")]
        public string CancelRemark { get; set; }

        public static void SortBy(OfflinePaymentSortByParam sortByParam, ref IQueryable<OfflinePaymentQueryResult> query)
        {
            if (sortByParam.SortBy != null)
            {
                switch (sortByParam.SortBy.Value)
                {
                    case OfflinePaymentSortBy.ConfirmedByName:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.OfflinePaymentHeaders.ConfirmedBy.DisplayName);
                        else query = query.OrderByDescending(o => o.OfflinePaymentHeaders.ConfirmedBy.DisplayName);
                        break;
                    case OfflinePaymentSortBy.Project:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.ProjectName);
                        else query = query.OrderByDescending(o => o.ProjectName);
                        break;
                    case OfflinePaymentSortBy.UnitNo:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.OfflinePaymentHeaders.Booking.Unit.UnitNo);
                        else query = query.OrderByDescending(o => o.OfflinePaymentHeaders.Booking.Unit.UnitNo);
                        break;
                    case OfflinePaymentSortBy.CustomerName:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.OfflinePaymentHeaders.Contact.FirstNameTH).ThenBy(o => o.OfflinePaymentHeaders.Contact.LastNameTH);
                        else query = query.OrderByDescending(o => o.OfflinePaymentHeaders.Contact.FirstNameTH).ThenByDescending(o => o.OfflinePaymentHeaders.Contact.LastNameTH);
                        break;
                    case OfflinePaymentSortBy.PayAmount:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.OfflinePaymentHeaders.PayAmount);
                        else query = query.OrderByDescending(o => o.OfflinePaymentHeaders.PayAmount);
                        break;
                    case OfflinePaymentSortBy.OfflinePaymentItem:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.MasterPriceItems.Detail);
                        else query = query.OrderByDescending(o => o.MasterPriceItems.Detail);
                        break;
                    case OfflinePaymentSortBy.ReceiveDate:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.OfflinePaymentHeaders.ReceiveDate);
                        else query = query.OrderByDescending(o => o.OfflinePaymentHeaders.ReceiveDate);
                        break;
                    case OfflinePaymentSortBy.ReceiptByName:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.OfflinePaymentHeaders.CreatedByName);
                        else query = query.OrderByDescending(o => o.OfflinePaymentHeaders.CreatedByName);
                        break;
                    case OfflinePaymentSortBy.TempReceiptNo:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.OfflinePaymentHeaders.ReceiptNo);
                        else query = query.OrderByDescending(o => o.OfflinePaymentHeaders.ReceiptNo);
                        break;
                    case OfflinePaymentSortBy.ReceiptNo:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.ReceiptNo);
                        else query = query.OrderByDescending(o => o.ReceiptNo);
                        break;
                    case OfflinePaymentSortBy.OfflinePaymentStatus:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.OfflinePaymentHeaders.OfflinePaymentStatusMasterCenter.Name);
                        else query = query.OrderByDescending(o => o.OfflinePaymentHeaders.OfflinePaymentStatusMasterCenter.Name);
                        break;
                    case OfflinePaymentSortBy.ConfirmedDate:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.OfflinePaymentHeaders.ConfirmedDate);
                        else query = query.OrderByDescending(o => o.OfflinePaymentHeaders.ConfirmedDate);
                        break; 
                    default:
                        query = query.OrderByDescending(o => o.OfflinePaymentHeaders.ReceiveDate);
                        break;
                }
            }
            else
            {
                query = query.OrderByDescending(o => o.OfflinePaymentHeaders.ReceiveDate);
            }
        }
        public static OfflinePaymentDTO CreateFromModel(OfflinePaymentQueryResult model, DatabaseContext DB)
        {
            if (model != null)
            {
                OfflinePaymentDTO result = new OfflinePaymentDTO()
                {
                    Id = model.OfflinePaymentHeaders.ID,
                    Booking = new BookingDTO { Id=model.OfflinePaymentHeaders.BookingID}, 
                    PayAmount = model.OfflinePaymentHeaders.PayAmount,
                    PaymentTypeDetail = MasterPriceItemDTO.CreateFromModel(model.MasterPriceItems),
                    ReceiveDate = model.OfflinePaymentHeaders.ReceiveDate,
                    CreateBy = model.OfflinePaymentHeaders.CreatedByName,
                    TempReceiptNo = model.OfflinePaymentHeaders.ReceiptNo,
                    ReceiptNo = model.ReceiptNo,
                    OfflinePaymentStatusMaster = MasterCenterDropdownDTO.CreateFromModel(model.OfflinePaymentHeaders.OfflinePaymentStatusMasterCenter),
                    ConfirmedDate = model.OfflinePaymentHeaders.ConfirmedDate,
                    ConfirmedBy = UserListDTO.CreateFromModel(model.OfflinePaymentHeaders.ConfirmedBy),
                    CancelRemark = model.OfflinePaymentHeaders.CancelRemark,
                    Updated = model.OfflinePaymentHeaders.Updated,
                    UpdatedBy = model.OfflinePaymentHeaders.UpdatedBy?.DisplayName,
                    PaymentName = model.CustomerName,
                    PaymentCode = model.CustomerCode
                };
                result.Booking.Unit = PRJ.UnitDropdownDTO.CreateFromModel(model.OfflinePaymentHeaders.Booking.Unit);
                result.Booking.Project = PRJ.ProjectDropdownDTO.CreateFromModel(model.OfflinePaymentHeaders.Booking.Project);
                if (model.PaymentCancel)
                {
                    var OfflinePaymentStatus = DB.MasterCenters.Where(x => x.MasterCenterGroupKey.Equals(MasterCenterGroupKeys.OfflinePaymentStatus) && x.Key.Equals(OfflinePaymentStatusKeys.CancelReceipt)).FirstOrDefault();
                    result.OfflinePaymentStatusMaster = MasterCenterDropdownDTO.CreateFromModel(OfflinePaymentStatus);
                }

                return result;
            }
            else
            {
                return null;
            }
        }
    }
    public class OfflinePaymentQueryResult
    { 
        public OfflinePaymentHeader OfflinePaymentHeaders { get; set; } 
        public MasterPriceItem MasterPriceItems { get; set; } 
        public string PaymentTypeDetail { get; set; }
        public string ReceiptNo { get; set; }
        public string ProjectName { get; set; }
        public string CustomerName { get; set; }
        public string CustomerCode { get; set; }

        public bool PaymentCancel { get; set; }

    }
}