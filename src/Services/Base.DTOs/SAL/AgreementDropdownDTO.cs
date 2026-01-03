using Database.Models.SAL;
using System;
using System.Collections.Generic;
using System.Text;

namespace Base.DTOs.SAL
{
    public class AgreementDropdownDTO : BaseDTO
    {
        /// <summary>
        /// เลขที่สัญญา
        /// </summary>
        public string AgreementNo { get; set; }
        /// <summary>
        /// วันที่ทำสัญญา
        /// </summary>
        public DateTime? ContractDate { get; set; }
        /// <summary>
        /// ใบจอง
        /// </summary>
        public BookingDropdownDTO Booking { get; set; }

        /// <summary>
        /// พื้นที่เพิ่มลด ค่าบวก = พื้นที่โฉนด > พื้นที่ขาย
        /// </summary>
        public decimal AreaPricePerUnit { get; set; }
        /// <summary>
        /// สร้างโดย
        /// </summary>
        public string CreatedBy { get; set; }
        /// <summary>
        /// วันที่สร้าง
        /// </summary>
        public DateTime? Created { get; set; }

        /// <summary>
        /// วันที่โอนกรรมสิทธิ์
        /// </summary>
        public DateTime? TransferOwnershipDate { get; set; }

        /// <summary>
        /// ผู้ทำสัญญาหลัก
        /// </summary>
        public string MainOwnerName { get; set; }

        public static AgreementDropdownDTO CreateFromModel(Agreement model)
        {
            if (model != null)
            {
                var result = new AgreementDropdownDTO()
                {
                    Id = model.ID,
                    Updated = model.Updated,
                    Created = model.Created,
                    UpdatedBy = model.UpdatedBy?.DisplayName,
                    CreatedBy = model.CreatedBy?.DisplayName,
                    AgreementNo = model.AgreementNo,
                    ContractDate = model.ContractDate,
                    AreaPricePerUnit = model.AreaPricePerUnit,
                    Booking = BookingDropdownDTO.CreateFromModel(model.Booking),
                    TransferOwnershipDate = model.TransferOwnershipDate,
                    MainOwnerName = model.MainOwnerName
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
