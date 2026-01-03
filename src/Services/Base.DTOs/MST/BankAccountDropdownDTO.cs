using System;
using System.ComponentModel;
using System.Linq;
using Database.Models.MST;

namespace Base.DTOs.MST
{
    public class BankAccountDropdownDTO : BaseDTO
    {
        public string DisplayName { get; set; }
        /// <summary>
        /// ธนาคาร
        /// Master/api/Banks/DropdownList
        /// </summary>
        [Description("ธนาคาร")]
        public BankDropdownDTO Bank { get; set; }

        /// <summary>
        /// เลขที่บัญชี
        /// </summary>
        [Description("เลขที่บัญชี")]
        public string BankAccountNo { get; set; }

        /// <summary>
        /// ประเภทบัญชี
        /// masterdata/api/MasterCenters/DropdownList?masterCenterGroupKey=BankAccountType
        /// </summary>
        [Description("ประเภทบัญชี")]
        public MST.MasterCenterDropdownDTO BankAccountType { get; set; }

        [Description("ชื่อย่อ")]
        public string PrefixName { get; set; }

        public static void SortBy(BankAccountDropdownSortByParam sortByParam, ref IQueryable<BankAccount> query)
        {
            if (sortByParam.SortBy != null)
            {
                switch (sortByParam.SortBy.Value)
                {
                    case BankAccountDropdownSortBy.BankAccountNo:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.BankAccountNo);
                        else query = query.OrderByDescending(o => o.BankAccountNo);
                        break;
                    case BankAccountDropdownSortBy.BankAccountType:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.BankAccountType.Name);
                        else query = query.OrderByDescending(o => o.BankAccountType.Name);
                        break;
                    case BankAccountDropdownSortBy.DisplayName:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.Bank.Alias);
                        else query = query.OrderByDescending(o => o.Bank.Alias);
                        break;
                    case BankAccountDropdownSortBy.PrefixName:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.DisplayName);
                        else query = query.OrderByDescending(o => o.DisplayName);
                        break;
                    default:
                        query = query.OrderBy(o => o.Bank.Alias).ThenBy(x => x.BankAccountNo);
                        break;
                }
            }
            else
            {
                query = query.OrderBy(o => o.Bank.Alias).ThenBy(x=>x.BankAccountNo);
            }
        }

        public static BankAccountDropdownDTO CreateFromModel(BankAccount model)
        {
            if (model != null)
            {
                BankAccountDropdownDTO result = new BankAccountDropdownDTO();

                result.Id = model.ID;
                result.Bank = BankDropdownDTO.CreateFromModel(model.Bank);
                result.BankAccountNo = model.BankAccountNo;
                result.BankAccountType = MasterCenterDropdownDTO.CreateFromModel(model.BankAccountType);
                string DisplayNameBank = result.Bank?.Alias + " " + result.BankAccountType?.Name + " " + result.BankAccountNo;
                result.DisplayName = !string.IsNullOrEmpty(model.DisplayName) ? model.DisplayName : DisplayNameBank;
                result.PrefixName = result.DisplayName + " " + result.BankAccountType?.Name + " " + result.BankAccountNo;

                return result;
            }
            else
            {
                return null;
            }
        }

    }
}
