using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Base.DTOs.FIN
{
    public class DirectCreditDebitExportListDTO : BaseDTO
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
        /// รอบการตัดเงิน วันที่ 1 หรือ 15
        /// </summary>
        public DateTime PeriodDate { get; set; }

        /// <summary>
        /// วันที่ตัดเงิน
        /// </summary>
        public DateTime ReceiveDate { get; set; }

        public List<DirectCreditDebitExportDetailDTO> DirectCreditDebitExportDetail { get; set; }
    }
}
