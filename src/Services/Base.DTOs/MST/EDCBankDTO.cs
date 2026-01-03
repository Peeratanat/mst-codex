using Database.Models.FIN;
using Database.Models.MST;
using Database.Models.USR;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Base.DTOs.MST
{
    /// <summary>
    /// ธนาคารเครื่องรูดบัตร
    /// ไม่มี Model โดยตรง (Group By Bank จาก EDC)
    /// </summary>
    public class EDCBankDTO
    {
        /// <summary>
        /// ธนาคาร
        /// Master/api/Banks/DropdownList
        /// </summary>
        public BankDropdownDTO Bank { get; set; }
        /// <summary>
        /// แก้ไขล่าสุด
        /// </summary>
        public DateTime? Updated { get; set; }
        /// <summary>
        /// แก้ไขโดย
        /// </summary>
        public string UpdatedBy { get; set; }

        public static EDCBankDTO CreateFromModel(EDC model)
        {
            if (model != null)
            {
                var result = new EDCBankDTO()
                {
                    Bank = BankDropdownDTO.CreateFromModel(model.Bank),
                    Updated = model.Updated,
                    UpdatedBy = model.UpdatedBy?.DisplayName
                };
                return result;
            }
            else
            {
                return null;
            }
        }
        public static EDCBankDTO CreateFromQueryResult(EDCBankQueryResult model)
        {
            if (model != null)
            {
                var result = new EDCBankDTO()
                {
                    Bank = BankDropdownDTO.CreateFromModel(model.Bank),
                    Updated = model.Updated,
                    UpdatedBy = model.UpdatedBy?.DisplayName
                };

                return result;
            }
            else
            {
                return null;
            }
        }

        public static void SortBy(EDCBankSortByParam sortByParam, ref IQueryable<EDCBankQueryResult> query)
        {
            if (sortByParam.SortBy != null)
            {
                switch (sortByParam.SortBy.Value)
                {
                    case EDCBankSortBy.Bank:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.Bank.NameTH);
                        else query = query.OrderByDescending(o => o.Bank.NameTH);
                        break;
                    case EDCBankSortBy.Updated:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.EDC.Updated);
                        else query = query.OrderByDescending(o => o.EDC.Updated);
                        break;
                    case EDCBankSortBy.UpdatedBy:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.EDC.UpdatedBy.DisplayName);
                        else query = query.OrderByDescending(o => o.EDC.UpdatedBy.DisplayName);
                        break;
                    default:
                        query = query.OrderBy(o => o.Bank.NameTH);
                        break;
                }
            }
            else
            {
                query = query.OrderBy(o => o.Bank.NameTH);
            }
        }

        public static void SortBy(EDCBankSortByParam sortByParam, ref List<EDCBankQueryResult> query)
        {
            if (sortByParam.SortBy != null)
            {
                switch (sortByParam.SortBy.Value)
                {
                    case EDCBankSortBy.Bank:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.Bank?.NameTH).ToList();
                        else query = query.OrderByDescending(o => o.Bank?.NameTH).ToList();
                        break;
                    case EDCBankSortBy.Updated:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.Updated).ToList();
                        else query = query.OrderByDescending(o => o.Updated).ToList();
                        break;
                    case EDCBankSortBy.UpdatedBy:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.UpdatedBy?.DisplayName).ToList();
                        else query = query.OrderByDescending(o => o.UpdatedBy?.DisplayName).ToList();
                        break;
                    default:
                        query = query.OrderBy(o => o.Bank?.NameTH).ToList();
                        break;
                }
            }
            else
            {
                query = query.OrderBy(o => o.Bank.NameTH).ToList();
            }
        }
    }

    public class EDCBankQueryResult
    {
        public EDC EDC { get; set; }
        public Bank Bank { get; set; }

        public EDCFee EDCFee { get; set; }
        public DateTime? Updated { get; set; }
        public User UpdatedBy { get; set; }
    }
}
