using Database.Models.MST;
using System;
namespace Base.DTOs.MST
{
    public class SubBGDropdownDTO
    {
        public Guid Id { get; set; }
        /// <summary>
        /// รหัส Sub BG
        /// </summary>
        public string SubBGNo { get; set; }
        /// <summary>
        /// ชื่อ Sub BG
        /// </summary>
        public string Name { get; set; }

        public static SubBGDropdownDTO CreateFromModel(SubBG model)
        {
            if (model != null)
            {
                SubBGDropdownDTO result = new SubBGDropdownDTO()
                {
                    Id = model.ID,
                    SubBGNo = model.SubBGNo,
                    Name = model.Name,
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
