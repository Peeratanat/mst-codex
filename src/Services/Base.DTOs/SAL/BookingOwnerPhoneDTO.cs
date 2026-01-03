using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using models = Database.Models;

namespace Base.DTOs.SAL
{
    public class BookingOwnerPhoneDTO
    {
        /// <summary>
        /// ID ของเบอร์โทรศัพท์ผู้จอง
        /// </summary>
        public Guid? Id { get; set; }
        /// <summary>
        /// มาจาก Contact Phone
        /// </summary>
        public Guid? FromContactPhoneID { get; set; }
        /// <summary>
        /// ประเภทเบอร์โทรศัพท์
        /// masterdata/api/MasterCenters/DropdownList?masterCenterGroupKey=PhoneType
        /// </summary>
        [Description("ประเภทเบอร์โทรศัพท์")]
        public MST.MasterCenterDropdownDTO PhoneType { get; set; }
        /// <summary>
        /// เบอร์โทรศัพท์
        /// </summary>
        [Description("เบอร์โทรศัพท์")]
        public string PhoneNumber { get; set; }
        /// <summary>
        /// เบอร์ต่อ
        /// </summary>
        [Description("เบอร์ต่อ")]
        public string PhoneNumberExt { get; set; }
        /// <summary>
        /// รหัสประเทศ
        /// </summary>
        [Description("รหัสประเทศ")]
        public string CountryCode { get; set; }
        /// <summary>
        /// สถานะเบอร์โทรศัพท์หลัก
        /// </summary>
        [Description("สถานะเบอร์โทรศัพท์หลัก")]
        public bool IsMain { get; set; }

        public static BookingOwnerPhoneDTO CreateFromModel(models.SAL.BookingOwnerPhone model)
        {
            if (model != null)
            {
                var result = new BookingOwnerPhoneDTO()
                {
                    Id = model.ID,
                    FromContactPhoneID = model.FromContactPhoneID,
                    PhoneType = MST.MasterCenterDropdownDTO.CreateFromModel(model.PhoneType),
                    PhoneNumber = model.PhoneNumber,
                    PhoneNumberExt = model.PhoneNumberExt,
                    CountryCode = model.CountryCode,
                    IsMain = model.IsMain
                };

                return result;
            }
            else
            {
                return null;
            }
        }

        public static BookingOwnerPhoneDTO CreateFromContactModel(models.CTM.ContactPhone model)
        {
            if (model != null)
            {
                var result = new BookingOwnerPhoneDTO()
                {
                    FromContactPhoneID = model.ID,
                    PhoneType = MST.MasterCenterDropdownDTO.CreateFromModel(model.PhoneType),
                    PhoneNumber = model.PhoneNumber,
                    PhoneNumberExt = model.PhoneNumberExt,
                    CountryCode = model.CountryCode,
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
