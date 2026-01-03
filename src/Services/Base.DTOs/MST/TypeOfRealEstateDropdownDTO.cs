using Database.Models.MST;
using System;
using System.Collections.Generic;
using System.Text;

namespace Base.DTOs.MST
{
    public class TypeOfRealEstateDropdownDTO
    {
        public Guid Id { get; set; }
        /// <summary>
        /// ชื่อประเภทบ้าน
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// รหัสประเภทบ้าน
        /// </summary>
        public string Code { get; set; }

        public static TypeOfRealEstateDropdownDTO CreateFromModel(TypeOfRealEstate model)
        {
            if (model != null)
            {
                var result = new TypeOfRealEstateDropdownDTO()
                {
                    Id = model.ID,
                    Name = model.Name,
                    Code = model.Code
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
