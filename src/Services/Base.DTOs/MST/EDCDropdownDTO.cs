using System;
using System.ComponentModel;
using Database.Models.MST;

namespace Base.DTOs.MST
{
    public class EDCDropdownDTO : BaseDTO
    {
        /// <summary>
        /// ธนาคาร
        /// Master/api/Banks/DropdownList
        /// </summary>
        [Description("ธนาคาร")]
        public BankDropdownDTO Bank { get; set; }
        /// <summary>
        /// รหัสเครื่องรูดบัตร
        /// </summary>
        [Description("รหัสเครื่องรูดบัตร")]
        public string Code { get; set; }

        public static EDCDropdownDTO CreateFromModel(EDC model)
        {
            if (model != null)
            {
                var result = new EDCDropdownDTO();
                result.Id = model.ID;
                result.Bank = BankDropdownDTO.CreateFromModel(model.Bank);
                result.Code = model.Code;

                return result;
            }
            else
            {
                return null;
            }
        }
    }
}
