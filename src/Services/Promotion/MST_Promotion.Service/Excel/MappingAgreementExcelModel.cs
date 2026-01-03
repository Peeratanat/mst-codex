using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace MST_Promotion.Services.Excel
{
    public class MappingAgreementExcelModel
    {
        public const int _oldAgreementNoIndex = 0;
        public const int _oldItemNoIndex = 1;
        public const int _oldMaterialCodeIndex = 2;
        public const int _newAgreementNoIndex = 3;
        public const int _newItemNoIndex = 4;
        public const int _newMaterialCodeIndex = 5;
        public const int _remarkIndex = 6;
        public const int _createDateIndex = 7;
        /// <summary>
        /// เลขที่ Agreement เดิม
        /// </summary>
        public string OldAgreementNo { get; set; }
        /// <summary>
        /// เลขที่ Item เดิม
        /// </summary>
        public string OldItemNo { get; set; }
        /// <summary>
        /// เลขที่ Material Code เดิม
        /// </summary>
        public string OldMaterialCode { get; set; }
        /// <summary>
        /// เลขที่ Agreement ใหม่
        /// </summary>
        public string NewAgreementNo { get; set; }
        /// <summary>
        /// เลขที่ Item ใหม่
        /// </summary>
        public string NewItemNo { get; set; }
        /// <summary>
        /// เลขที่ Material Code ใหม่่
        /// </summary>
        public string NewMaterialCode { get; set; }
        /// <summary>
        /// หมายเหตุ
        /// </summary>
        public string Remartk { get; set; }

        public static MappingAgreementExcelModel CreateFromDataRow(DataRow dr)
        {
            var result = new MappingAgreementExcelModel();
            result.OldAgreementNo = dr[_oldAgreementNoIndex]?.ToString();
            result.OldItemNo = dr[_oldItemNoIndex]?.ToString();
            result.OldMaterialCode = dr[_oldMaterialCodeIndex]?.ToString();
            result.NewAgreementNo = dr[_newAgreementNoIndex]?.ToString();
            result.NewItemNo = dr[_newItemNoIndex]?.ToString();
            result.NewMaterialCode = dr[_newMaterialCodeIndex]?.ToString();
            result.Remartk = dr[_remarkIndex]?.ToString();
            return result;
        }
    }
}
