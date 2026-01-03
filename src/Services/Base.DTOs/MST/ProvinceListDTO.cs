using Database.Models.MST;
using System;
using System.Collections.Generic;
using System.Text;

namespace Base.DTOs.MST
{
    public class ProvinceListDTO
    {
        public Guid? Id { get; set; }
        /// <summary>
        /// จังหวัด ภาษาไทย
        /// </summary>
        public string NameTH { get; set; }
        /// <summary>
        /// จังหวัด ภาษาอังกฤษ
        /// </summary>
        public string NameEN { get; set; }

        public static ProvinceListDTO CreateFromModel(Province model)
        {
            if (model != null)
            {
                ProvinceListDTO result = new ProvinceListDTO()
                {
                    Id = model.ID,
                    NameTH = model.NameTH,
                    NameEN = model.NameEN,
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
