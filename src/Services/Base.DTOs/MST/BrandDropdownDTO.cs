using Database.Models.MST;
using System;
namespace Base.DTOs.MST
{
    public class BrandDropdownDTO
    {
        public Guid Id { get; set; }
        /// <summary>
        /// รหัสแบรนด์
        /// </summary>
        public string BrandNo { get; set; }
        /// <summary>
        /// ชื่อแบรนด์
        /// </summary>
        public string Name { get; set; }

        public static BrandDropdownDTO CreateFromModel(Brand model)
        {
            if (model != null)
            {
                var result = new BrandDropdownDTO()
                {
                    Id = model.ID,
                    BrandNo = model.BrandNo,
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
