using Database.Models.SAL;
using System;
using System.Collections.Generic;
using System.Text;

namespace Base.DTOs.SAL
{
    public class AdminEditUserDTO : BaseDTO
    {
        /// <summary>
        /// จอง
        /// </summary>
        public BookingDTO booking { get; set; }
        /// <summary>
        /// ตนจอง
        /// </summary>
        public List<BookingOwnerDTO> bookingOwner { get; set; }
        /// <summary>
        /// ตนจองสัญญา
        /// </summary>
        public List<BookingOwnerDTO> bookingAgreementOwner { get; set; }
        /// <summary>
        /// สัญญา
        /// </summary>
        public AgreementDTO agreement { get; set; }
        /// <summary>
        /// ตนทำสัญญา
        /// </summary>
        public List<AgreementOwnerDTO> agreementOwner { get; set; }
        /// <summary>
        /// โอน
        /// </summary>
        public TransferDTO transfer { get; set; }
        /// <summary>
        /// ตนโอน
        /// </summary>
        public List<TransferOwnerDTO> transferOwner { get; set; }


        //public static AgreementDropdownDTO CreateFromModel(Agreement model)
        //{
        //    if (model != null)
        //    {
        //        var result = new AgreementDropdownDTO()
        //        {
        //            Id = model.ID,
        //            Updated = model.Updated,
        //            Created = model.Created,
        //            UpdatedBy = model.UpdatedBy?.DisplayName,
        //            CreatedBy = model.CreatedBy?.DisplayName,
        //            AgreementNo = model.AgreementNo,
        //            ContractDate = model.ContractDate,
        //            AreaPricePerUnit = model.AreaPricePerUnit,
        //            Booking = BookingDropdownDTO.CreateFromModel(model.Booking),
        //            TransferOwnershipDate = model.TransferOwnershipDate,
        //            MainOwnerName = model.MainOwnerName
        //        };

        //        return result;
        //    }
        //    else
        //    {
        //        return null;
        //    }
        //}

    }
}
