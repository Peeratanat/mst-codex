using Base.DTOs.MST;
using Base.DTOs.SAL.Sortings;
using Database.Models;
using Database.Models.MasterKeys;
using Database.Models.SAL;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Database.Models.PRJ;
using Database.Models.MST;
using Database.Models.USR;
using System.Threading.Tasks;

namespace Base.DTOs.SAL
{
    /// <summary>
    /// Memo คืนเงินลูกค้า
    /// </summary>
    public class RefundMemoListDTO : BaseDTO
    {
        /// <summary>
        /// โครงการ
        /// </summary>
        public PRJ.ProjectDropdownDTO Project { get; set; }
        /// <summary>
        /// แปลง
        /// </summary>
        public PRJ.UnitDropdownDTO Unit { get; set; }
        ///<summary>
        ///วันที่โอนจริง
        ///</summary>
        public DateTime? ActualTransferDate { get; set; }
        /// <summary>
        /// ชื่อลูกค้า
        /// </summary>
        public TransferOwnerDropdownDTO TransferOwner { get; set; }
        /// <summary>
        /// โอนเงินคืนลูกค้า
        /// </summary>
        public bool IsRefundCustomer { get; set; }
        /// <summary>
        /// โอนเงินคืนนิติ
        /// </summary>
        public bool IsRefundLegalEntity { get; set; }
        /// <summary>
        /// โอนเงินคืนลูกค้า (สั่งจ่ายนิติ) 
        /// </summary>
        public bool IsRefundCustomerByLegalEntity { get; set; }
        /// <summary>
        /// วันที่นัดชำระ (คืนลูกค้า)
        /// </summary>
        public DateTime? RefundDueDate { get; set; }

        /// <summary>
        /// ยกยอดไปนิติบุคคล
        /// </summary>
        public bool? IsAPBalanceTransfer { get; set; }

        /// <summary>
        /// รวมเงินทอน AP
        /// </summary>
        public decimal? APChangeAmount { get; set; }

        /// <summary>
        /// รวมเงินทอน  (นิติบุคคล)
        /// </summary>
        public decimal? LegalEntityChangeAmount { get; set; }

        //public static RefundMemoListDTO CreateFromModel(RefundMemo model, DatabaseContext DB)
        //{
        //    try
        //    {
        //        if (model != null)
        //        {
        //            var owner = DB.TransferOwners.Include(o => o.MarriageStatus).Where(o => o.TransferID == model.TransferID && o.Order == 1).FirstOrDefault();

        //            var result = new RefundMemoListDTO()
        //            {
        //                Id = model.ID,
        //                Project = PRJ.ProjectDropdownDTO.CreateFromModel(model.Transfer.Project),
        //                Unit = PRJ.UnitDropdownDTO.CreateFromModel(model.Transfer.Unit),
        //                ActualTransferDate = model.Transfer.ActualTransferDate,
        //                TransferOwner = TransferOwnerDropdownDTO.CreateFromModel(owner),
        //                IsRefundCustomer = model.RefundMemoType.Key == "1",
        //                IsRefundLegalEntity = model.RefundMemoType.Key == "2",
        //                IsRefundCustomerByLegalEntity = model.RefundMemoType.Key == "3",
        //                RefundDueDate = model.RefundDueDate
        //            };

        //            return result;
        //        }
        //        else
        //        {
        //            return null;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        var x = ex.Message;
        //        return null;
        //    }
        //}

        public static RefundMemoListDTO CreateFromQueryResult(RefundMemoQueryResult model, DatabaseContext DB)
        {
            if (model != null)
            {
                var result = new RefundMemoListDTO()
                {
                    Id = model.Transfer.ID,
                    Project = PRJ.ProjectDropdownDTO.CreateFromModel(model.Transfer.Project),
                    Unit = PRJ.UnitDropdownDTO.CreateFromModel(model.Transfer.Unit),
                    ActualTransferDate = model.Transfer.ActualTransferDate,
                    TransferOwner = TransferOwnerDropdownDTO.CreateFromModel(model.TransferOwner),
                    IsRefundCustomer = model.IsRefundCustomer,
                    IsRefundLegalEntity = model.IsRefundLegalEntity,
                    IsRefundCustomerByLegalEntity = model.IsRefundCustomerByLegalEntity,
                    RefundDueDate = model.RefundDueDate,
                    IsAPBalanceTransfer = model.IsAPBalanceTransfer,
                    APChangeAmount = (model.APChangeAmount ?? 0),
                    LegalEntityChangeAmount = (model.LegalEntityChangeAmount ?? 0)
                };

                return result;
            }
            else
            {
                return null;
            }
        }

        public static void SortBy(RefundMemoListSortByParam sortByParam, ref IQueryable<RefundMemoQueryResult> query)
        {
            IOrderedQueryable<RefundMemoQueryResult> orderQuery;
            if (sortByParam.SortBy != null)
            {
                switch (sortByParam.SortBy.Value)
                {
                    case RefundMemoListSortBy.UnitNo:
                        if (sortByParam.Ascending) orderQuery = query.OrderBy(o => o.Transfer.ProjectID).ThenBy(o => o.Unit.UnitNo);
                        else orderQuery = query.OrderBy(o => o.Transfer.ProjectID).ThenByDescending(o => o.Unit.UnitNo);
                        break;
                    case RefundMemoListSortBy.FullName:
                        if (sortByParam.Ascending) orderQuery = query.OrderBy(o => o.Transfer.ProjectID).ThenBy(o => (o.TransferOwner.FirstNameTH));
                        else orderQuery = query.OrderBy(o => o.Transfer.ProjectID).ThenByDescending(o => (o.TransferOwner.FirstNameTH));
                        break;
                    case RefundMemoListSortBy.ActualTransferDate:
                        if (sortByParam.Ascending) orderQuery = query.OrderBy(o => o.Transfer.ProjectID).ThenBy(o => o.Transfer.ActualTransferDate);
                        else orderQuery = query.OrderBy(o => o.Transfer.ProjectID).ThenByDescending(o => o.Transfer.ActualTransferDate);
                        break;
                    case RefundMemoListSortBy.IsRefundCustomer:
                        if (sortByParam.Ascending) orderQuery = query.OrderBy(o => o.Transfer.ProjectID).ThenBy(o => o.IsRefundCustomer);
                        else orderQuery = query.OrderBy(o => o.Transfer.ProjectID).ThenByDescending(o => o.IsRefundCustomer);
                        break;
                    case RefundMemoListSortBy.IsRefundLegalEntity:
                        if (sortByParam.Ascending) orderQuery = query.OrderBy(o => o.Transfer.ProjectID).ThenBy(o => o.IsRefundLegalEntity);
                        else orderQuery = query.OrderBy(o => o.Transfer.ProjectID).ThenByDescending(o => o.IsRefundLegalEntity);
                        break;
                    case RefundMemoListSortBy.IsRefundCustomerByLegalEntity:
                        if (sortByParam.Ascending) orderQuery = query.OrderBy(o => o.Transfer.ProjectID).ThenBy(o => o.IsRefundCustomerByLegalEntity);
                        else orderQuery = query.OrderBy(o => o.Transfer.ProjectID).ThenByDescending(o => o.IsRefundCustomerByLegalEntity);
                        break;
                    case RefundMemoListSortBy.RefundDueDate:
                        if (sortByParam.Ascending) orderQuery = query.OrderBy(o => o.Transfer.ProjectID).ThenBy(o => o.RefundDueDate);
                        else orderQuery = query.OrderBy(o => o.Transfer.ProjectID).ThenByDescending(o => o.RefundDueDate);
                        break;
                    default:
                        orderQuery = query.OrderBy(o => o.Transfer.ProjectID).ThenByDescending(o => o.Unit.UnitNo);
                        break;
                }
            }
            else
            {
                orderQuery = query.OrderBy(o => o.Transfer.ProjectID).ThenBy(o => o.Transfer.ID);
            }

            orderQuery.ThenBy(o => o.Transfer.ID);
            query = orderQuery;
        }
    }
    public class RefundMemoQueryResult
    {
        public Transfer Transfer { get; set; }
        public TransferOwner TransferOwner { get; set; }
        public Project Project { get; set; }
        public Unit Unit { get; set; }
        public bool IsRefundCustomer { get; set; }
        public bool IsRefundLegalEntity { get; set; }
        public bool IsRefundCustomerByLegalEntity { get; set; }
        public DateTime? RefundDueDate { get; set; }

        public bool? IsAPBalanceTransfer { get; set; }
        public decimal? APChangeAmount { get; set; }
        public decimal? LegalEntityChangeAmount { get; set; }

    }
}
