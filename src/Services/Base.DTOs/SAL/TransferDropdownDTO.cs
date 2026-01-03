using Database.Models.SAL;
using System;
using System.Collections.Generic;
using System.Text;

namespace Base.DTOs.SAL
{
    public class TransferDropdownDTO : BaseDTO
    {
        /// <summary>
        /// เลขที่โอน
        /// </summary>
        public string TransferNo { get; set; }
        /// <summary>
        /// วันที่นัดโอน
        /// </summary>
        public DateTime? ScheduleTransferDate { get; set; }
        /// <summary>
        /// ใบจอง
        /// </summary>
        public AgreementDropdownDTO Agreement { get; set; }

        public static TransferDropdownDTO CreateFromModel(Transfer model)
        {
            if (model != null)
            {
                var result = new TransferDropdownDTO()
                {
                    Id = model.ID,
                    Updated = model.Updated,
                    UpdatedBy = model.UpdatedBy?.DisplayName,
                    TransferNo = model.TransferNo,
                    ScheduleTransferDate = model.ScheduleTransferDate,
                    Agreement = AgreementDropdownDTO.CreateFromModel(model.Agreement)
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
