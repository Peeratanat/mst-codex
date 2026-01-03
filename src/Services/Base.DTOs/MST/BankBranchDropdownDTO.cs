using Database.Models.MST;
using System;
namespace Base.DTOs.MST
{
    public class BankBranchDropdownDTO : BaseDTO
    {
        /// <summary>
        /// ชื่อสาขา
        /// </summary>
        public string Name { get; set; }
       

        public static BankBranchDropdownDTO CreateFromModel(BankBranch model)
        {
            if (model != null)
            {
                var result = new BankBranchDropdownDTO()
                {
                    Id = model.ID,
                    Name = model.Name
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
