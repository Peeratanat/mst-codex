using Database.Models.MST;
using System;
using System.Collections.Generic;
using System.Text;

namespace Base.DTOs.MST
{
    public class SubDistrictListDTO
    {
        public Guid? Id { get; set; }
        /// <summary>
        /// ชื่อตำบล ภาษาไทย
        /// </summary>
        public string NameTH { get; set; }
        /// <summary>
        /// ชื่อตำบลภาษาอังกฤษ
        /// </summary>
        public string NameEN { get; set; }
        /// <summary>
        /// รหัสไปรษณีย์
        /// </summary>
        public string PostalCode { get; set; }

        public static SubDistrictListDTO CreateFromModel(SubDistrict model)
        {
            if (model != null)
            {
                SubDistrictListDTO result = new SubDistrictListDTO()
                {
                    Id = model.ID,
                    NameTH = model.NameTH,
                    NameEN = model.NameEN,
                    PostalCode = model.PostalCode
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
