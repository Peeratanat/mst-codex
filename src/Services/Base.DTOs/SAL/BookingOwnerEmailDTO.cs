using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using models = Database.Models;

namespace Base.DTOs.SAL
{
    public class BookingOwnerEmailDTO
    {
        /// <summary>
        /// ID ของ Email ผู้จอง
        /// </summary>
        public Guid? Id { get; set; }
        /// <summary>
        /// มาจาก Contact Email 
        /// </summary>
        public Guid? FromContactEmailID { get; set; }
        /// <summary>
        /// อีเมล
        /// </summary>
        [Description("อีเมล")]
        public string Email { get; set; }
        /// <summary>
        /// สถานะอีเมลหลัก
        /// </summary>
        public bool IsMain { get; set; }

        public static BookingOwnerEmailDTO CreateFromModel(models.SAL.BookingOwnerEmail model)
        {
            if (model != null)
            {
                var result = new BookingOwnerEmailDTO()
                {
                    Id = model.ID,
                    FromContactEmailID = model.FromContactEmailID,
                    Email = model.Email,
                    IsMain = model.IsMain
                };

                return result;
            }
            else
            {
                return null;
            }
        }

        public static BookingOwnerEmailDTO CreateFromContactModel(models.CTM.ContactEmail model)
        {
            if (model != null)
            {
                var result = new BookingOwnerEmailDTO()
                {
                    FromContactEmailID = model.ID,
                    Email = model.Email,
                    IsMain = model.IsMain
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
