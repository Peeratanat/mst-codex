using System;
using Database.Models.CTM;
using Database.Models.MST;
using Database.Models.SAL;

namespace Base.DTOs.SAL
{
    public class BookingOwnerDropdownDTO : BaseDTO
    {
        /// <summary>
        /// ชื่อจริง (ภาษาไทย)
        /// </summary>
        public string FirstNameTH { get; set; }
        /// <summary>
        /// ชื่อกลาง (ภาษาไทย)
        /// </summary>
        public string MiddleNameTH { get; set; }
        /// <summary>
        /// นามสกุล (ภาษาไทย)
        /// </summary>
        public string LastNameTH { get; set; }

        /// <summary>
        /// ชื่อจริง (ภาษาอังกฤษ)
        /// </summary>
        public string FirstNameEN { get; set; }
        /// <summary>
        /// ชื่อกลาง (ภาษาอังกฤษ)
        /// </summary>
        public string MiddleNameEN { get; set; }
        /// <summary>
        /// นามสกุล (ภาษาอังกฤษ)
        /// </summary>
        public string LastNameEN { get; set; }
        /// <summary>
        /// สัญชาตื
        /// </summary>
        public MasterCenter National { get; set; }
        
        public static BookingOwnerDropdownDTO CreateFromModel(BookingOwner model)
        {
            if (model != null)
            {
                BookingOwnerDropdownDTO result = new BookingOwnerDropdownDTO
                {
                    Id = model.ID,
                    Updated = model.Updated,
                    UpdatedBy = model.UpdatedBy?.DisplayName,
                    FirstNameTH = model.FirstNameTH,
                    LastNameTH = model.LastNameTH,
                    MiddleNameTH = model.MiddleNameTH,
                    FirstNameEN = model.FirstNameEN!=null? model.FirstNameEN : null,
                    MiddleNameEN = model.MiddleNameEN != null ? model.MiddleNameEN : null,
                    LastNameEN = model.LastNameEN != null ? model.LastNameEN : null,
                    National = model.National

                };

                return result;
            }
            else
            {
                return null;
            }
        }
        //public static BookingOwnerDropdownDTO CreateOppListFromModel(BookingOwner model, Contact modelContact)
        //{
        //    if (model != null)
        //    {
        //        BookingOwnerDropdownDTO result = new BookingOwnerDropdownDTO
        //        {
        //            Id = model.ID,
        //            Updated = model.Updated,
        //            UpdatedBy = model.UpdatedBy?.DisplayName,
        //            FirstNameTH = modelContact != null ? modelContact.FirstNameTH : "",
        //            LastNameTH = modelContact != null ? modelContact.LastNameTH : "",
        //            MiddleNameTH = model.MiddleNameTH,
        //            FirstNameEN = model.FirstNameEN != null ? model.FirstNameEN : null,
        //            MiddleNameEN = model.MiddleNameEN != null ? model.MiddleNameEN : null,
        //            LastNameEN = model.LastNameEN != null ? model.LastNameEN : null,
        //            National = model.National

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
