using Database.Models.MST;
using System;
using System.Collections.Generic;
using System.Text;

namespace Base.DTOs.MST
{
    public class LandOfficeListDTO
    {
        public Guid? Id { get; set; }
        /// <summary>
        /// ชื่อสำนักงานที่ดิน ภาษาไทย
        /// </summary>
        public string NameTH { get; set; }
        /// <summary>
        /// ชื่อสำนักงานที่ดิน ภาษาอังกฤษ
        /// </summary>
        public string NameEN { get; set; }

        public static LandOfficeListDTO CreateFromModel(LandOffice model)
        {
            if (model != null)
            {
                var result = new LandOfficeListDTO()
                {
                    Id = model.ID,
                    NameEN = model.NameEN,
                    NameTH = model.NameTH,
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
