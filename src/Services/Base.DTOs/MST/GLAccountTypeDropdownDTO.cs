using Database.Models.MasterKeys;
using Database.Models.MST;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using models = Database.Models;
namespace Base.DTOs.MST
{
    public class GLAccountTypeDropdownDTO
    {
        public Guid Id { get; set; }
        /// <summary>
        /// ชื่อ ข้อมูลพื้นฐานทั่วไป
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// รหัส ข้อมูลพื้นฐานทั่วไป
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// ลำดับ
        /// </summary>
        public int Order { get; set; }

        public static GLAccountTypeDropdownDTO CreateFromModel(GLAccountType model)
        {
            if (model != null)
            {
                var result = new GLAccountTypeDropdownDTO()
                {
                    Id = model.ID,
                    Name = model.Name,
                    Key = model.Key,
                    Order = model.Order
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
