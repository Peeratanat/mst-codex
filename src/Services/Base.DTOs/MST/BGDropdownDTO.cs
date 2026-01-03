using System;
using Database.Models.MST;

namespace Base.DTOs.MST
{
    public class BGDropdownDTO
    {
        public Guid? Id { get; set; }
        /// <summary>
        /// ชื่อ BG
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// รหัส BG
        /// </summary>
        public string BGNo { get; set; }

        public static BGDropdownDTO CreateFromModel(BG model)
        {
            if (model != null)
            {
                var result = new BGDropdownDTO()
                {
                    Id = model.ID,
                    Name = model.Name,
                    BGNo = model.BGNo
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
