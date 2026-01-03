using Database.Models.MST;
using System;
namespace Base.DTOs.MST
{
    public class BankBranchBOTDropdownDTO : BaseDTO
    {

        /// <summary>
        /// ชื่อสาขา
        /// </summary>
        public string BankBranchName { get; set; }

        /// <summary>
        /// BankCode
        /// </summary>
        public string BankCode { get; set; }

        /// <summary>
        /// ชื่อสาขา
        /// </summary>
        public string BankBranchCode { get; set; }

        /// <summary>
        /// ชื่อสาขา EN
        /// </summary>
        public string BankBranchNameEN { get; set; }

        public static BankBranchBOTDropdownDTO CreateFromModel(BankBranchBOT model)
        {
            if (model != null)
            {
                var result = new BankBranchBOTDropdownDTO()
                {
                    Id = model.ID,
                    BankCode = model.BankCode,
                    BankBranchCode = model.BankBranchCode,
                    BankBranchName = model.BankBranchName,
                    BankBranchNameEN = model.BankBranchNameEN
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
