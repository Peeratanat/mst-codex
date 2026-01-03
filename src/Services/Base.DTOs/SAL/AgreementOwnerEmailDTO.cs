using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using models = Database.Models;

namespace Base.DTOs.SAL
{
    public class AgreementOwnerEmailDTO
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
        [Description("สถานะอีเมลหลัก")]
        public bool IsMain { get; set; }

        public static AgreementOwnerEmailDTO CreateFromModel(models.SAL.AgreementOwnerEmail model)
        {
            if (model != null)
            {
                var result = new AgreementOwnerEmailDTO()
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

        public static AgreementOwnerEmailDTO CreateFromContactModel(models.CTM.ContactEmail model)
        {
            if (model != null)
            {
                var result = new AgreementOwnerEmailDTO()
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
