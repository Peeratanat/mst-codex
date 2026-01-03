using Base.DTOs.PRJ;
using Database.Models;
using Database.Models.MST;
using Database.Models.PRJ;
using Database.Models.USR;
using ErrorHandling;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Base.DTOs.MST
{
    public class SpecMaterialItemDTO : BaseDTO
    {

        public MasterCenterDropdownDTO Group { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string ItemDescription { get; set; }
        public int? Order { get; set; }
        public bool? IsActive { get; set; }
        public string NameEN { get; set; }
        public string ItemDescriptionEN { get; set; }

        public static SpecMaterialItemDTO CreateFromModel(SpecMaterialItem model)
        {
            if (model != null)
            {
                var result = new SpecMaterialItemDTO()
                {
                    Id = model.ID,
                    Code = model.Code,
                    Name = model.Name,
                    IsActive = model.IsActive,
                    ItemDescription = model.ItemDescription,
                    Updated = model.Updated,
                    UpdatedBy = model.UpdatedBy?.DisplayName,
                    NameEN = model.NameEN,
                    ItemDescriptionEN = model.ItemDescriptionEN,
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
