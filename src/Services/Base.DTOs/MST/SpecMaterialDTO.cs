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
    public class SpecMaterialDTO : BaseDTO
    {

        public Guid ID { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public bool? IsActive { get; set; }




        public static SpecMaterialDTO CreateFromModel(SpecMaterialItem model)
        {
            if (model != null)
            {
                var result = new SpecMaterialDTO()
                {
                    ID = model.ID,
                    Code = model.Code,
                    Name = model.Name,
                    IsActive = model.IsActive
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
