using Database.Models.PRJ;
using System;
using System.Collections.Generic;
using System.Text;

namespace Base.DTOs.PRJ
{
    public class FloorDropdownDTO
    {
        /// <summary>
        /// Identity FloorID
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// ชื่อชั้น (TH)
        /// </summary>
        public string NameTH { get; set; }
        /// <summary>
        /// ชื่อชั้น (EN)
        /// </summary>
        public string NameEN { get; set; }
        public static FloorDropdownDTO CreateFromModel(Floor model)
        {
            if (model != null)
            {
                var result = new FloorDropdownDTO
                {
                    Id= model.ID,
                    NameTH = model.NameTH,
                    NameEN= model.NameEN
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
