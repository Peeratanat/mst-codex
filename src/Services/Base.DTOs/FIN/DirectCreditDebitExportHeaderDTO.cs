using Base.DTOs.MST;
using Database.Models;
using Database.Models.FIN;
using Database.Models.MST;
using Database.Models.PRJ;
using Database.Models.SAL;
using ErrorHandling;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
namespace Base.DTOs.FIN
{
    public class DirectCreditDebitExportHeaderDTO : BaseDTO
    {
        /// <summary>
        /// ชนิดของแบบฟอร์ม Direct Debit/Credit
        /// </summary>
        public MST.MasterCenterDropdownDTO DirectFormType { get; set; }

        /// <summary>
        /// บริษัท
        /// </summary>
        public MST.CompanyDropdownDTO Company { get; set; }

        /// <summary>
        /// ธนาคาร
        /// </summary>
        public MST.BankDropdownDTO Bank { get; set; }
        /// <summary>
        /// บัญชีธนาคาร
        /// </summary>
        public MST.BankAccountDropdownDTO BankAccount { get; set; }

        /// <summary>
        /// รอบการตัดเงิน วันที่ 1 หรือ 15
        /// </summary>
        public DateTime? PeriodDate { get; set; }

        /// <summary>
        /// วันที่ตัดเงิน
        /// </summary>
        public DateTime? ReceiveDate { get; set; }

        /// <summary>
        /// รวมจำนวนรายการใน Textfile ที่จะตัดเงินลูกค้า
        /// </summary>
        public int? TotalRecord { get; set; }

        /// <summary>
        /// จำนวนรายการ Error
        /// </summary>
        public int? TotalErrorRecord { get; set; }

        /// <summary>
        /// รวมจำนวนเงินใน Textfile ที่จะตัดเงินลูกค้า
        /// </summary>
        public decimal TotalAmount { get; set; }

        /// <summary>
        /// วันที่ Import Textfile ที่ได้จากธนาคาร
        /// </summary>
        public DateTime? ImportDate { get; set; }
        /// <summary>
        /// วันที่ Export Textfile
        /// </summary>
        public DateTime? ExportDate { get; set; }

        public string BacthID { get; set; }

        public DirectCreditDebitExportDetailDTO DirectCreditDebitExportDetail { get; set; }
        public string ImportFileName { get; set; }

        public class PaymentUnitPriceItemBooking
        {
            public Guid BookingID { get; set; }
            public List<PaymentUnitPriceItemDTO> PaymentUnitPriceItemDTO { get; set; }
        }

        public class DirectCreditDebitApprovalFormExportQueryResult
        {
            public DirectCreditDebitApprovalForm DirectCreditDebitApprovalForm { get; set; }
            public Agreement Agreement { get; set; }
            public Unit Unit { get; set; }
            public Project Project { get; set; }
            public BankAccount BankAccount { get; set; }
            //public UnitPrice UnitPrice { get; set; }
            //public UnitPriceItem UnitPriceItem { get; set; } // kim
            //public UnitPriceInstallment UnitPriceInstallment { get; set; }
            public Bank Bank { get; set; }
            public Transfer Transfer { get; set; }
        }

        public class DirectCreditDebitExportHeaderQueryResult
        {
            public DirectCreditDebitExportHeader DirectCreditDebitExportHeader { get; set; }
            public MasterCenter DirectFormType { get; set; }
            public Company Company { get; set; }
            public Bank Bank { get; set; }
            public BankAccount BankAccount { get; set; }
            public DirectCreditDebitExportDetail DirectCreditDebitExportDetail { get; set; }
            public DirectCreditDebitApprovalForm DirectCreditDebitApprovalForm { get; set; }
            public string BacthID { get; set; }
            public Booking Booking { get; set; }
            public Agreement Agreement { get; set; }
            public Transfer Transfer { get; set; }
            public listBatch BatchStatus { get; set; }
            public PaymentMethod PaymentMethod { get; set; }
            public Payment Payment { get; set; }
            public MasterCenter TranCode { get; set; }
            public Unit Unit  { get; set; }
            public Project Project { get; set; }
            public UnitPriceInstallment UnitPriceInstallment { get; set; }

        }
        //public class DirectCreditDebitExportImportQueryResult
        //{

        //    public DirectCreditDebitExportDetail DirectCreditDebitExportDetail { get; set; }
        //    //public DirectCreditDebitApprovalForm DirectCreditDebitApprovalForm { get; set; }
        //    public Agreement Agreement { get; set; }
        //    public Transfer Transfer { get; set; }
        //    public listBatch BatchStatus { get; set; }
        //}
        public class listBatch
        {

            public string BatchID { get; set; }
            public string TransCode { get; set; }
        }
        
        public static void SortBy(DirectCreditDebitExportHeaderSortByParam sortByParam, ref IQueryable<DirectCreditDebitExportHeaderQueryResult> query)
        {
            if (query != null)
            {
                if (sortByParam.SortBy != null)
                {
                    switch (sortByParam.SortBy.Value)
                    {
                        case DirectCreditDebitExportHeaderSortBy.BatchID:
                            if (sortByParam.Ascending) query = query.OrderBy(o => o.BacthID);
                            else query = query.OrderByDescending(o => o.BacthID);
                            break;
                        case DirectCreditDebitExportHeaderSortBy.Bank:
                            if (sortByParam.Ascending) query = query.OrderBy(o => o.Bank.Alias);
                            else query = query.OrderByDescending(o => o.Bank.Alias);
                            break;
                        case DirectCreditDebitExportHeaderSortBy.BankAccount:
                            if (sortByParam.Ascending) query = query.OrderBy(o => o.BankAccount.BankAccountNo);
                            else query = query.OrderByDescending(o => o.BankAccount.BankAccountNo);
                            break;
                        case DirectCreditDebitExportHeaderSortBy.Company:
                            if (sortByParam.Ascending) query = query.OrderBy(o => o.Company.Code);
                            else query = query.OrderByDescending(o => o.Company.Code);
                            break;
                        case DirectCreditDebitExportHeaderSortBy.PeriodDate:
                            if (sortByParam.Ascending) query = query.OrderBy(o => o.DirectCreditDebitExportHeader.PeriodDate);
                            else query = query.OrderByDescending(o => o.DirectCreditDebitExportHeader.PeriodDate);
                            break;
                        case DirectCreditDebitExportHeaderSortBy.ReceiveDate:
                            if (sortByParam.Ascending) query = query.OrderBy(o => o.DirectCreditDebitExportHeader.ReceiveDate);
                            else query = query.OrderByDescending(o => o.DirectCreditDebitExportHeader.ReceiveDate);
                            break;
                        case DirectCreditDebitExportHeaderSortBy.DirectCreditDebitType:
                            if (sortByParam.Ascending) query = query.OrderBy(o => o.DirectCreditDebitExportHeader.DirectFormType.Name);
                            else query = query.OrderByDescending(o => o.DirectCreditDebitExportHeader.DirectFormType.Name);
                            break;
                        case DirectCreditDebitExportHeaderSortBy.TotalRecord:
                            if (sortByParam.Ascending) query = query.OrderBy(o => o.DirectCreditDebitExportHeader.TotalRecord);
                            else query = query.OrderByDescending(o => o.DirectCreditDebitExportHeader.TotalRecord);
                            break;
                        case DirectCreditDebitExportHeaderSortBy.TotalErrorRecord:
                            if (sortByParam.Ascending) query = query.OrderBy(o => o.DirectCreditDebitExportHeader.TotalErrorRecord);
                            else query = query.OrderByDescending(o => o.DirectCreditDebitExportHeader.TotalErrorRecord);
                            break;
                        case DirectCreditDebitExportHeaderSortBy.TotalAmount:
                            if (sortByParam.Ascending) query = query.OrderBy(o => o.DirectCreditDebitExportHeader.TotalAmount);
                            else query = query.OrderByDescending(o => o.DirectCreditDebitExportHeader.TotalAmount);
                            break;

                        case DirectCreditDebitExportHeaderSortBy.ImportDate:
                            if (sortByParam.Ascending) query = query.OrderBy(o => o.DirectCreditDebitExportHeader.ImportDate);
                            else query = query.OrderByDescending(o => o.DirectCreditDebitExportHeader.ImportDate);
                            break;

                        case DirectCreditDebitExportHeaderSortBy.ExportDate:
                            if (sortByParam.Ascending) query = query.OrderBy(o => o.DirectCreditDebitExportHeader.Created);
                            else query = query.OrderByDescending(o => o.DirectCreditDebitExportHeader.Created);
                            break;

                        case DirectCreditDebitExportHeaderSortBy.ImportBy:
                            if (sortByParam.Ascending) query = query.OrderBy(o => o.DirectCreditDebitExportHeader.UpdatedBy.DisplayName);
                            else query = query.OrderByDescending(o => o.DirectCreditDebitExportHeader.UpdatedBy.DisplayName);
                            break;

                        default:
                            query = query.OrderBy(o => o.DirectCreditDebitExportHeader.ImportDate);
                            break;
                    }
                }
                else
                {
                    query = query.OrderByDescending(o => o.DirectCreditDebitExportHeader.Created);
                }
            }
        }

        public static void SortByDetail(DirectCreditDebitExportDetailSortByParam sortByParam, ref IQueryable<DirectCreditDebitExportHeaderQueryResult> query)
        {
            List<string> preferences = new List<string> { DirectCreditDebitExportDetailStatusKeys.Wait, 
                DirectCreditDebitExportDetailStatusKeys.Fail, 
                DirectCreditDebitExportDetailStatusKeys.Complete, 
                DirectCreditDebitExportDetailStatusKeys.TransferUnit,
                DirectCreditDebitExportDetailStatusKeys.Cancel};
            if (query != null)
            {
                if (sortByParam.SortBy != null)
                {
                    switch (sortByParam.SortBy.Value)
                    {
                        case DirectCreditDebitExportDetailSortBy.BatchID:
                            if (sortByParam.Ascending) query = query.OrderBy(item => preferences.IndexOf(item.DirectCreditDebitExportDetail.DirectCreditDebitExportDetailStatus.Key)).ThenBy(o => o.DirectCreditDebitExportDetail.DirectCreditDebitExportDetailStatus.Order).ThenBy(o => o.DirectCreditDebitExportDetail.BatchID);
                            else query = query.OrderBy(item => preferences.IndexOf(item.DirectCreditDebitExportDetail.DirectCreditDebitExportDetailStatus.Key)).ThenByDescending(o => o.DirectCreditDebitExportDetail.DirectCreditDebitExportDetailStatus.Order).ThenByDescending(o => o.DirectCreditDebitExportDetail.BatchID);
                            break;
                        case DirectCreditDebitExportDetailSortBy.Project:
                            if (sortByParam.Ascending) query = query.OrderBy(item => preferences.IndexOf(item.DirectCreditDebitExportDetail.DirectCreditDebitExportDetailStatus.Key)).ThenBy(o => o.DirectCreditDebitExportDetail.DirectCreditDebitExportDetailStatus.Order).ThenBy(o => o.Project.ProjectNo);
                            else query = query.OrderBy(item => preferences.IndexOf(item.DirectCreditDebitExportDetail.DirectCreditDebitExportDetailStatus.Key)).ThenByDescending(o => o.DirectCreditDebitExportDetail.DirectCreditDebitExportDetailStatus.Order).ThenByDescending(o => o.Project.ProjectNo);
                            break;
                        case DirectCreditDebitExportDetailSortBy.UnitNo:
                            if (sortByParam.Ascending) query = query.OrderBy(item => preferences.IndexOf(item.DirectCreditDebitExportDetail.DirectCreditDebitExportDetailStatus.Key)).ThenBy(o => o.DirectCreditDebitExportDetail.DirectCreditDebitExportDetailStatus.Order).ThenBy(o => o.Unit.UnitNo);
                            else query = query.OrderBy(item => preferences.IndexOf(item.DirectCreditDebitExportDetail.DirectCreditDebitExportDetailStatus.Key)).ThenByDescending(o => o.DirectCreditDebitExportDetail.DirectCreditDebitExportDetailStatus.Order).ThenByDescending(o => o.Unit.UnitNo);
                            break;
                        case DirectCreditDebitExportDetailSortBy.AccountNo:
                            if (sortByParam.Ascending) query = query.OrderBy(item => preferences.IndexOf(item.DirectCreditDebitExportDetail.DirectCreditDebitExportDetailStatus.Key)).ThenBy(o => o.DirectCreditDebitExportDetail.DirectCreditDebitExportDetailStatus.Order).ThenBy(o => o.DirectCreditDebitApprovalForm.AccountNO);
                            else query = query.OrderBy(item => preferences.IndexOf(item.DirectCreditDebitExportDetail.DirectCreditDebitExportDetailStatus.Key)).ThenByDescending(o => o.DirectCreditDebitExportDetail.DirectCreditDebitExportDetailStatus.Order).ThenByDescending(o => o.DirectCreditDebitApprovalForm.AccountNO);
                            break;
                        case DirectCreditDebitExportDetailSortBy.PeriodDate:
                            if (sortByParam.Ascending) query = query.OrderBy(item => preferences.IndexOf(item.DirectCreditDebitExportDetail.DirectCreditDebitExportDetailStatus.Key)).ThenBy(o => o.DirectCreditDebitExportDetail.DirectCreditDebitExportDetailStatus.Order).ThenBy(o => o.DirectCreditDebitApprovalForm.DirectPeriod);
                            else query = query.OrderBy(item => preferences.IndexOf(item.DirectCreditDebitExportDetail.DirectCreditDebitExportDetailStatus.Key)).ThenByDescending(o => o.DirectCreditDebitExportDetail.DirectCreditDebitExportDetailStatus.Order).ThenByDescending(o => o.DirectCreditDebitApprovalForm.DirectPeriod);
                            break;
                        case DirectCreditDebitExportDetailSortBy.DueDate:
                            if (sortByParam.Ascending) query = query.OrderBy(item => preferences.IndexOf(item.DirectCreditDebitExportDetail.DirectCreditDebitExportDetailStatus.Key)).ThenBy(o => o.DirectCreditDebitExportDetail.DirectCreditDebitExportDetailStatus.Order).ThenBy(o => o.DirectCreditDebitExportDetail.UnitPriceInstallment.DueDate);
                            else query = query.OrderBy(item => preferences.IndexOf(item.DirectCreditDebitExportDetail.DirectCreditDebitExportDetailStatus.Key)).ThenByDescending(o => o.DirectCreditDebitExportDetail.DirectCreditDebitExportDetailStatus.Order).ThenByDescending(o => o.DirectCreditDebitExportDetail.UnitPriceInstallment.DueDate);
                            break;
                        case DirectCreditDebitExportDetailSortBy.AgreementNo:
                            if (sortByParam.Ascending) query = query.OrderBy(item => preferences.IndexOf(item.DirectCreditDebitExportDetail.DirectCreditDebitExportDetailStatus.Key)).ThenBy(o => o.DirectCreditDebitExportDetail.DirectCreditDebitExportDetailStatus.Order).ThenBy(o => o.Agreement.AgreementNo);
                            else query = query.OrderBy(item => preferences.IndexOf(item.DirectCreditDebitExportDetail.DirectCreditDebitExportDetailStatus.Key)).ThenByDescending(o => o.DirectCreditDebitExportDetail.DirectCreditDebitExportDetailStatus.Order).ThenByDescending(o => o.Agreement.AgreementNo);
                            break;
                        case DirectCreditDebitExportDetailSortBy.CustomerName:
                            if (sortByParam.Ascending) query = query.OrderBy(item => preferences.IndexOf(item.DirectCreditDebitExportDetail.DirectCreditDebitExportDetailStatus.Key)).ThenBy(o => o.DirectCreditDebitExportDetail.DirectCreditDebitExportDetailStatus.Order).ThenBy(o => o.DirectCreditDebitApprovalForm.OwnerName);
                            else query = query.OrderBy(item => preferences.IndexOf(item.DirectCreditDebitExportDetail.DirectCreditDebitExportDetailStatus.Key)).ThenByDescending(o => o.DirectCreditDebitExportDetail.DirectCreditDebitExportDetailStatus.Order).ThenByDescending(o => o.DirectCreditDebitApprovalForm.OwnerName);
                            break;
                        case DirectCreditDebitExportDetailSortBy.ReceiveDate:
                            if (sortByParam.Ascending) query = query.OrderBy(item => preferences.IndexOf(item.DirectCreditDebitExportDetail.DirectCreditDebitExportDetailStatus.Key)).ThenBy(o => o.DirectCreditDebitExportHeader.ReceiveDate);
                            else query = query.OrderBy(item => preferences.IndexOf(item.DirectCreditDebitExportDetail.DirectCreditDebitExportDetailStatus.Key)).ThenByDescending(o => o.DirectCreditDebitExportHeader.ReceiveDate);
                            break;
                        case DirectCreditDebitExportDetailSortBy.Amount:
                            if (sortByParam.Ascending) query = query.OrderBy(item => preferences.IndexOf(item.DirectCreditDebitExportDetail.DirectCreditDebitExportDetailStatus.Key)).ThenBy(o => o.DirectCreditDebitExportDetail.DirectCreditDebitExportDetailStatus.Order).ThenBy(o => (o.DirectCreditDebitExportDetail.Amount));
                            else query = query.OrderBy(item => preferences.IndexOf(item.DirectCreditDebitExportDetail.DirectCreditDebitExportDetailStatus.Key)).ThenByDescending(o => o.DirectCreditDebitExportDetail.DirectCreditDebitExportDetailStatus.Order).ThenByDescending(o => (o.DirectCreditDebitExportDetail.Amount));
                            break;
                        case DirectCreditDebitExportDetailSortBy.DirectCreditDebitExportStatus:
                            if (sortByParam.Ascending) query = query.OrderBy(item => preferences.IndexOf(item.DirectCreditDebitExportDetail.DirectCreditDebitExportDetailStatus.Key)).ThenBy(o => o.DirectCreditDebitExportDetail.DirectCreditDebitExportDetailStatus.Order);
                            else query = query.OrderBy(item => preferences.IndexOf(item.DirectCreditDebitExportDetail.DirectCreditDebitExportDetailStatus.Key)).ThenByDescending(o => o.DirectCreditDebitExportDetail.DirectCreditDebitExportDetailStatus.Order);
                            break;
                        case DirectCreditDebitExportDetailSortBy.ReceiptTempNo:
                            if (sortByParam.Ascending) query = query.OrderBy(item => preferences.IndexOf(item.DirectCreditDebitExportDetail.DirectCreditDebitExportDetailStatus.Key)).ThenBy(o => o.Payment.ReceiptTempNo);
                            else query = query.OrderBy(item => preferences.IndexOf(item.DirectCreditDebitExportDetail.DirectCreditDebitExportDetailStatus.Key)).ThenByDescending(o => o.Payment.ReceiptTempNo);
                            break;
                        case DirectCreditDebitExportDetailSortBy.OldPeriod:
                            if (sortByParam.Ascending) query = query.OrderBy(item => preferences.IndexOf(item.DirectCreditDebitExportDetail.DirectCreditDebitExportDetailStatus.Key)).ThenBy(o => o.Payment.PaymentItemName);
                            else query = query.OrderBy(item => preferences.IndexOf(item.DirectCreditDebitExportDetail.DirectCreditDebitExportDetailStatus.Key)).ThenByDescending(o => o.Payment.PaymentItemName);
                            break;
                        case DirectCreditDebitExportDetailSortBy.Period:
                            if (sortByParam.Ascending) query = query.OrderBy(item => preferences.IndexOf(item.DirectCreditDebitExportDetail.DirectCreditDebitExportDetailStatus.Key)).ThenBy(o => o.DirectCreditDebitExportDetail.UnitPriceInstallment.Period);   
                            else query = query.OrderBy(item => preferences.IndexOf(item.DirectCreditDebitExportDetail.DirectCreditDebitExportDetailStatus.Key)).ThenByDescending(o => o.DirectCreditDebitExportDetail.UnitPriceInstallment.Period);
                            break;
                        case DirectCreditDebitExportDetailSortBy.TranCode:
                            if (sortByParam.Ascending) query = query.OrderBy(item => preferences.IndexOf(item.DirectCreditDebitExportDetail.DirectCreditDebitExportDetailStatus.Key)).ThenBy(o => o.TranCode.Key);
                            else query = query.OrderBy(item => preferences.IndexOf(item.DirectCreditDebitExportDetail.DirectCreditDebitExportDetailStatus.Key)).ThenByDescending(o => o.TranCode.Key);
                            break;

                        case DirectCreditDebitExportDetailSortBy.TranCodeRemark:
                            if (sortByParam.Ascending) query = query.OrderBy(item => preferences.IndexOf(item.DirectCreditDebitExportDetail.DirectCreditDebitExportDetailStatus.Key)).ThenBy(o => o.TranCode.Name);
                            else query = query.OrderBy(item => preferences.IndexOf(item.DirectCreditDebitExportDetail.DirectCreditDebitExportDetailStatus.Key)).ThenByDescending(o => o.TranCode.Name);
                            break;
                        default:
                            query = query.OrderBy(item => preferences.IndexOf(item.DirectCreditDebitExportDetail.DirectCreditDebitExportDetailStatus.Key)).ThenBy(o => o.Project.ProjectNo).ThenBy(o => o.Unit.UnitNo).ThenBy(o => o.UnitPriceInstallment.Period);
                            break;
                    }

                }
                else
                {
                    query = query.OrderBy(item => preferences.IndexOf(item.DirectCreditDebitExportDetail.DirectCreditDebitExportDetailStatus.Key)).ThenBy(o => o.Project.ProjectNo).ThenBy(o => o.Unit.UnitNo).ThenBy(o => o.UnitPriceInstallment.Period);
                }
            }
        }


        public static DirectCreditDebitExportHeaderDTO CreateFromQueryResult(DirectCreditDebitExportHeaderQueryResult model, DatabaseContext DB)
        {
            if (model != null)
            {
                var oBacthID =  DB.DirectCreditDebitExportDetails.Where(x => x.DirectCreditDebitExportHeaderID == model.DirectCreditDebitExportHeader.ID).OrderBy(x => x.Seq).Select(x => x.BatchID).FirstOrDefault();
                var result = new DirectCreditDebitExportHeaderDTO()
                {
                    Bank = BankDropdownDTO.CreateFromModel(model.Bank),
                    BankAccount = BankAccountDropdownDTO.CreateFromModel(model.BankAccount),
                    Company = CompanyDropdownDTO.CreateFromModel(model.Company),
                    PeriodDate = model.DirectCreditDebitExportHeader.PeriodDate,
                    ReceiveDate = model.DirectCreditDebitExportHeader.ReceiveDate,
                    DirectFormType = MasterCenterDropdownDTO.CreateFromModel(model.DirectFormType),
                    TotalRecord = model.DirectCreditDebitExportHeader.TotalRecord,
                    TotalErrorRecord = model.DirectCreditDebitExportHeader.TotalErrorRecord,
                    TotalAmount = model.DirectCreditDebitExportHeader.TotalAmount,
                    ImportDate = model.DirectCreditDebitExportHeader.ImportDate,
                    Updated = model.DirectCreditDebitExportHeader.Updated,
                    Id = model.DirectCreditDebitExportHeader.ID,
                    BacthID = oBacthID,
                    ExportDate = model.DirectCreditDebitExportHeader.Created
                };
                return result;
            }
            else
            {
                return null;
            }
        }


        public static DirectCreditDebitExportHeaderDTO CreateFromByIDQueryResult(DirectCreditDebitExportHeaderQueryResult model, DatabaseContext DB)
        {
            if (model != null)
            {
                var result = CreateFromQueryResult(model, DB);
                result.DirectCreditDebitExportDetail = new DirectCreditDebitExportDetailDTO();
                result.DirectCreditDebitExportDetail = DirectCreditDebitExportDetailDTO.CreateFromModel(model.DirectCreditDebitExportDetail, model.Agreement, model.Payment?.ReceiptTempNo, model.Payment?.PaymentItemName, model.TranCode?.Name, model.UnitPriceInstallment);
              
                return result;
            }
            else
            {
                return null;
            }
        }

        public static DirectCreditDebitExportHeaderDTO CreateFromDirectCreditDebitExportHeader(DirectCreditDebitExportHeader model)
        {
            if (model != null)
            { var result = new DirectCreditDebitExportHeaderDTO()
                {
                    Bank = BankDropdownDTO.CreateFromModel(model.BankAccount.Bank),
                    BankAccount = BankAccountDropdownDTO.CreateFromModel(model.BankAccount),
                    Company = CompanyDropdownDTO.CreateFromModel(model.BankAccount.Company),
                    PeriodDate = model.PeriodDate,
                    ReceiveDate = model.ReceiveDate,
                    DirectFormType = MasterCenterDropdownDTO.CreateFromModel(model.DirectFormType),
                    TotalRecord = model.TotalRecord,
                    TotalErrorRecord = model.TotalErrorRecord,
                    TotalAmount = model.TotalAmount,
                    ImportDate = model.ImportDate,
                    Updated = model.Updated,
                    Id = model.ID, 
                    ExportDate = model.Created
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
