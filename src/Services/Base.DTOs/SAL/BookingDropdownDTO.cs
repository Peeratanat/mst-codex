using System;
using Database.Models.SAL;

namespace Base.DTOs.SAL
{
    public class BookingDropdownDTO : BaseDTO
    {
        /// <summary>
        /// เลขที่ใบจอง
        /// </summary>
        public string BookingNo { get; set; }

        public static BookingDropdownDTO CreateFromModel(Booking model)
        {
            if (model != null)
            {
                var result = new BookingDropdownDTO()
                {
                    Id = model.ID,
                    Updated = model.Updated,
                    UpdatedBy = model.UpdatedBy?.DisplayName,
                    BookingNo = model.BookingNo
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
