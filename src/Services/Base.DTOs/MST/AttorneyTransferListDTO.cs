using Database.Models.MST;
using System;
using System.Collections.Generic;
using System.Text;

namespace Base.DTOs.MST
{
    public class AttorneyTransferListDTO : BaseDTO
    {
        /// <summary>
        /// ชื่อ - นามสกุล
        /// </summary>
        public string Atty_FullName { get; set; }
        ///<summary>
        ///รหัสพนักงาน
        ///</summary>
        public string Atty_EmployeeCode { get; set; }

        public static AttorneyTransferListDTO CreateFromModel(AttorneyTransfer model)
        {
            if (model != null)
            {
                AttorneyTransferListDTO result = new AttorneyTransferListDTO()
                {
                    Id = model.ID,
                    Atty_FullName = model.Atty_FullName,
                    Atty_EmployeeCode = model.Atty_EmployeeCode,
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
