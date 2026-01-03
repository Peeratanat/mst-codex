using Database.Models.MST;
using System;


namespace Base.DTOs.FIN
{
    public class BankAccNameDTO : BaseDTO
    {
        /// <summary>
        /// ชื่อธนาคารภาษาไทย
        /// </summary>
        public string NameTH { get; set; }
        /// <summary>
        /// ชื่อธนาคาร Alias + BankAccountNo
        /// </summary>
        public string BankAccountName { get; set; }
        /// <summary>
        /// ID Bank Account
        /// </summary>
        public Guid BankAccountID { get; set; }

        public static BankAccNameDTO CreateFromModel(BankAccount model)
        {
            if (model != null)
            {
                var result = new BankAccNameDTO()
                {
                    Id = model.Bank?.ID, ///// Bank ID
                    NameTH = model.Bank?.NameTH,
                    BankAccountName = model.Bank?.Alias + " " + model.BankAccountNo,
                    BankAccountID = model.ID
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
