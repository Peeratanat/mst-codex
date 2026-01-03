using Base.DTOs.SAL.Sortings;
using Database.Models;
using System;
using System.Linq;
using models = Database.Models;

namespace Base.DTOs.SAL
{
    /// <summary>
    /// รายการ Quotation
    /// UI: https://projects.invisionapp.com/d/?origin=v7#/console/17482068/362366221/preview
    /// Model: Quotation
    /// </summary>
    public class QuotationPreBookListDTO
    {
        /// <summary>
        /// ID ของ Quotation
        /// </summary>
        public Guid? Id { get; set; }
        /// <summary>
        /// เลขที่ใบเสนอราคา
        /// </summary>
        public string QuotationNo { get; set; }
        /// <summary>
        /// โครงการ
        /// Project/api/Projects/DropdownList
        /// </summary>
        public PRJ.ProjectDropdownDTO Project { get; set; }
        /// <summary>
        /// แปลง
        /// </summary>
        public PRJ.UnitDropdownDTO Unit { get; set; }
        /// <summary>
        /// วันที่ออก
        /// </summary>
        public DateTime? IssueDate { get; set; }
        /// <summary>
        /// สถานะ
        /// Master/api/MasterCenters?masterCenterGroupKey=QuotationStatus
        /// </summary>
        public MST.MasterCenterDropdownDTO QuotationStatus { get; set; }
        /// <summary>
        /// ออกโดย
        /// Identity/api/Users/DropdownList
        /// </summary>
        public USR.UserListDTO CreatedBy { get; set; }
        /// <summary>
        /// สามารถลบได้หรือไม่
        /// </summary>
        public bool? CanDelete { get; set; }

        /// <summary>
        /// เลขที่ใบรับเงินชั่วคราว
        /// </summary>
        public string ReceiptPreBookNo { get; set; }

        /// <summary>
        /// Id เลขที่ใบรับเงินชั่วคราว
        /// </summary>
        public Guid? ReceiptPreBookId { get; set; }

        /// <summary>
        /// PaymentPreBook
        /// </summary>
        public FIN.PaymentPreBookFormDTO PaymentPreBook { get; set; }
        public DateTime? ReceiveDate { get; set; }

        public bool? IsDeleted { get; set; }

        public bool IsChangeUnit { get; set; }
        public bool IsCancelRealBook { get; set; }
        public bool IsCancelAgreement { get; set; }

        public static QuotationPreBookListDTO CreatePreBookFromQueryResult(QuotationPreBookQueryResult model)
        {
            if (model != null)
            {
                QuotationPreBookListDTO result = new QuotationPreBookListDTO()
                {
                    Id = model.Quotation.ID,
                    QuotationNo = model.Quotation.QuotationNo,
                    Project = PRJ.ProjectDropdownDTO.CreateFromModel(model.Project),
                    Unit = PRJ.UnitDropdownDTO.CreateFromModel(model.Unit),
                    ReceiptPreBookNo = model.PaymentPrebook != null ? model.PaymentPrebook?.ReceiptPrebookNo : null,
                    PaymentPreBook = FIN.PaymentPreBookFormDTO.CreateFromPaymentPrebookDataAsync(model),
                    IssueDate = model.Quotation.IssueDate,
                    QuotationStatus = MST.MasterCenterDropdownDTO.CreateFromModel(model.QuotationStatus),
                    CreatedBy = USR.UserListDTO.CreateFromModel(model.Quotation.CreatedBy),
                    ReceiptPreBookId = model.PaymentPrebook != null ? model.PaymentPrebook?.ID : null,
                    ReceiveDate = model.PaymentPrebook.ReceiveDate,
                    IsDeleted = model.PaymentPrebook.IsDeleted,
                    IsChangeUnit = model.PaymentPrebook.IsChangeUnit,
                    IsCancelRealBook = model.PaymentPrebook.IsCancelRealBook,
                    IsCancelAgreement = model.PaymentPrebook.IsCancelAgreement
                };
                return result;
            }
            else
            {
                return null;
            }
        }
        public static void SortBy(QuotationListSortByParam sortByParam, ref IQueryable<QuotationPreBookQueryResult> query)
        {
            if (sortByParam.SortBy != null)
            {
                switch (sortByParam.SortBy.Value)
                {
                    case QuotationListSortBy.QuotationNo:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.Quotation.QuotationNo);
                        else query = query.OrderByDescending(o => o.Quotation.QuotationNo);
                        break;
                    case QuotationListSortBy.Project:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.Project.ProjectNo);
                        else query = query.OrderByDescending(o => o.Project.ProjectNo);
                        break;
                    case QuotationListSortBy.UnitNo:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.Unit.UnitNo);
                        else query = query.OrderByDescending(o => o.Unit.UnitNo);
                        break;
                    case QuotationListSortBy.HouseNo:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.Unit.HouseNo);
                        else query = query.OrderByDescending(o => o.Unit.HouseNo);
                        break;
                    case QuotationListSortBy.IssueDate:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.Quotation.IssueDate);
                        else query = query.OrderByDescending(o => o.Quotation.IssueDate);
                        break;
                    case QuotationListSortBy.QuotationStatus:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.QuotationStatus.Name);
                        else query = query.OrderByDescending(o => o.QuotationStatus.Name);
                        break;
                    case QuotationListSortBy.CreatedBy:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.Quotation.Created);
                        else query = query.OrderByDescending(o => o.Quotation.Created);
                        break;
                    default:
                        query = query.OrderByDescending(o => o.Quotation.Created);
                        break;
                }
            }
            else
            {
                query = query.OrderByDescending(o => o.Quotation.Created);
            }
        }
    }

    public class QuotationPreBookQueryResult
    {
        public models.SAL.Quotation Quotation { get; set; }
        public models.FIN.PaymentPrebook PaymentPrebook { get; set; }
        public models.FIN.PaymentMethodPrebook PaymentMethodPrebook { get; set; }
        public models.PRJ.Project Project { get; set; }
        public models.PRJ.Unit Unit { get; set; }
        public models.MST.MasterCenter QuotationStatus { get; set; }
        public models.USR.User User { get; set; }

        public models.MST.MasterCenter CreditCardPaymentType { get; set; }

        public models.MST.MasterCenter CreditCardType { get; set; }
        public models.MST.Bank EDCBank { get; set; }
        public models.MST.Bank Bank { get; set; }
        public models.USR.User FeeConfirmByUser { get; set; }
        public models.MST.MasterCenter PaymentMethodType { get; set; }
        public models.MST.Company PayToCompany { get; set; }
        public models.USR.User UpdatedBy { get; set; }
    }

}
