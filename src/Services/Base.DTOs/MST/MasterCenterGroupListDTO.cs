using Database.Models.MST;
using System;
using System.Collections.Generic;
using System.Text;

namespace Base.DTOs.MST
{
    public class MasterCenterGroupListDTO
    {
        /// <summary>
        /// รหัส กลุ่มของข้อมูลทั่วไป
        /// </summary>
        public string Key { get; set; }
        /// <summary>
        /// ชือ กลุ่มของข้อมูลทั่วไป
        /// </summary>
        public string Name { get; set; }

        public static MasterCenterGroupListDTO CreateFromModel(MasterCenterGroup model)
        {
            if (model != null)
            {
                var result = new MasterCenterGroupListDTO()
                {
                    Key = model.Key,
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
