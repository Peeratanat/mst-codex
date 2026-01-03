using Database.Models.PRJ;
using System;
using System.Collections.Generic;
using System.Text;

namespace Base.DTOs.PRJ
{
    public class TowerDropdownDTO
    {
        /// <summary>
        /// Identity Tower ID
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// ชื่อตึก
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// อาคารเลขที่ (TH)
        /// </summary>
        public string NoTH { get; set; }
        /// <summary>
        /// อาคารเลขที่ (EN)
        /// </summary>
        public string NoEN { get; set; }

        public static TowerDropdownDTO CreateFromModel(Tower model)
        {
            if (model != null)
            {
                var result = new TowerDropdownDTO
                {
                    Id = model.ID,
                    Code = model.TowerCode,
                    NoTH = model.TowerNoTH,
                    NoEN = model.TowerNoEN
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
