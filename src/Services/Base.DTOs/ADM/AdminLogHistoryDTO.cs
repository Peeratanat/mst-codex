using Base.DTOs.USR;
using Database.Models.ADM;
using System;
using System.ComponentModel;

namespace Base.DTOs.ADM
{
    public class AdminLogHistoryDTO : BaseDTO
    {
        [Description("MenuCode")]
        public string MenuCode { get; set; }

        [Description("MenuName")]
        public string MenuName { get; set; }

        [Description("MenuDisplayName")]
        public string MenuDisplayName { get; set; }

        [Description("วันที่แก้ไข")]
        public DateTime LogDate { get; set; }

        [Description("เลขจ๊อบอ้างอิง")]
        public string RequestNo { get; set; }

        [Description("หมายเหตุ")]
        public string Remark { get; set; }

        [Description("HistoryData")]
        public string HistoryData { get; set; }

        [Description("ข้อมูลอ้างอิง 1")]
        public string Ref1 { get; set; }

        [Description("ข้อมูลอ้างอิง 2")]
        public string Ref2 { get; set; }

        [Description("ข้อมูลอ้างอิง 3")]
        public string Ref3 { get; set; }
        [Description("ข้อมูลอ้างอิง 3")]
        public bool Isdelete { get; set; }

        public static AdminLogHistoryDTO CreateFromModel(AdminLogHistory model)
        {
            if (model != null)
            {
                AdminLogHistoryDTO result = new AdminLogHistoryDTO()
                {
                    Id = model.ID,
                    MenuCode = model.MenuCode,
                    MenuName = model.MenuName,
                    MenuDisplayName = $"{model.MenuCode} - {model.MenuName}",
                    RequestNo = model.RequestNo,
                    Remark = model.Remark,
                    HistoryData = model.HistoryData,
                    Ref1 = model.Ref1,
                    Ref2 = model.Ref2,
                    Ref3 = model.Ref3,
                    LogDate= model.LogDate,
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
