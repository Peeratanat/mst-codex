using Database.Models.MST;
using System;
using System.Collections.Generic;
using System.Text;

namespace Base.DTOs.MST
{
    public class BGListDTO
    {
        public Guid Id { get; set; }
        /// <summary>
        /// รหัส BG
        /// </summary>
        public string BGNo { get; set; }
        /// <summary>
        /// ชื่อ BG
        /// </summary>
        public string Name { get; set; }

        public static BGListDTO CreateFromModel(BG model)
        {
            if (model != null)
            {
                var result = new BGListDTO()
                {
                    Id = model.ID,
                    BGNo = model.BGNo,
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
