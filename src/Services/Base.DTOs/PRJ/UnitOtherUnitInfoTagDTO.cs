using Database.Models.PRJ;
using System;
using System.Collections.Generic;
using System.Text;

namespace Base.DTOs.PRJ
{
    public class UnitOtherUnitInfoTagDTO : BaseDTO
    {
        public string Name { get; set; }

        public static UnitOtherUnitInfoTagDTO CreateFromModel(UnitOtherUnitInfoTag model)
        {
            if (model != null)
            {
                var result = new UnitOtherUnitInfoTagDTO()
                {
                    Id = model.ID,
                    Name = model.Name,
                    Updated = model.Updated,
                    UpdatedBy = model.UpdatedBy?.DisplayName
                };

                return result;
            }
            else
            {
                return null;
            }
        }

        public void ToModel(ref UnitOtherUnitInfoTag model)
        {
            model.Name = this.Name;
        }
    }
}
