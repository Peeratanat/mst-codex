using Database.Models.MST;
using System;
using System.Collections.Generic;
using System.Text;

namespace Base.DTOs.MST
{
    public class DistrictListDTO
    {
        public Guid? Id { get; set; }
        /// <summary>
        /// ชื่ออำเภอ ภาษาไทย
        /// </summary>
        public string NameTH { get; set; }
        /// <summary>
        /// ชื่ออำเภอ ภาษาอังกฤษ
        /// </summary>
        public string NameEN { get; set; }

        public static DistrictListDTO CreateFromModel(District model)
        {
            if (model != null)
            {
                var result = new DistrictListDTO
                { 
                    Id=model.ID,
                    NameTH=model.NameTH,
                    NameEN=model.NameEN
                };
                return result;
            }
            else
                return null;
        }
    }

}
