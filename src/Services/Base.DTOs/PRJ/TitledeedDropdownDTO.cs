using System;
using Database.Models.PRJ;

namespace Base.DTOs.PRJ
{
    public class TitleDeedDropdownDTO : BaseDTO
    {
        /// <summary>
        /// เลขที่โฉนด
        /// </summary>
        public string TitledeedNo { get; set; }
        /// <summary>
        /// พื้นที่โฉนด
        /// </summary>
        public double? TitledeedArea { get; set; }

        public static TitleDeedDropdownDTO CreateFromModel(TitledeedDetail model)
        {
            if (model != null)
            {
                var result = new TitleDeedDropdownDTO();

                result.Id = model.ID;
                result.TitledeedNo = model.TitledeedNo;
                result.TitledeedArea = model.TitledeedArea;
                result.Updated = model.Updated;
                result.UpdatedBy = model.UpdatedBy?.DisplayName;

                return result;
            }
            else
            {
                return null;
            }
        }

    }
}
