using Database.Models.MST;
using System;
using System.Collections.Generic;
using System.Text;

namespace Base.DTOs.MST
{
    public class AgentDropdownDTO
    {
        public Guid Id { get; set; }
        /// <summary>
        /// ชื่อ Agency ภาษาไทย (TH)
        /// </summary>
        public string NameTH { get; set; }
        /// <summary>
        /// ชื่อ Agency อังกฤษ (EN)
        /// </summary>
        public string NameEN { get; set; }
        /// <summary>
        /// รหัส ผู้สร้างข้อมูล
        /// </summary>
        public string Create_by { get; set; }
        /// <summary>
        /// วันที่ สร้างข้อมูล
        /// </summary>
        public DateTime? Create_date { get; set; }
        /// <summary>
        /// รหัส ผู้แก้ไขข้อมูล
        /// </summary>
        public string Modify_by { get; set; }
        /// <summary>
        /// วันที่ แก้ไขข้อมูล
        /// </summary>
        public DateTime? Modify_date { get; set; }


        public static AgentDropdownDTO CreateFromModel(Agent model)
        {
            if (model != null)
            {
                var result = new AgentDropdownDTO()
                {
                    Id = model.ID,
                    NameTH = model.NameTH,
                    NameEN = model.NameEN
                };
                return result;
            }
            else
            {
                return null;
            }
        }

        public static AgentDropdownDTO CreateFromData(Guid? ID, string Name)
        {
            if (ID.HasValue  && !string.IsNullOrEmpty(Name))
            {
                var result = new AgentDropdownDTO()
                {
                    Id = ID.Value,
                    NameTH = Name,
                    NameEN = Name
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
