using Database.Models;
using Database.Models.PRJ;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.DTOs.PRJ
{
    public class ModelDropdownDTO
    {
        /// <summary>
        /// Identity Tower ID
        /// </summary>
        /// <example></example>
        public Guid Id { get; set; }
        /// <summary>
        ///  รหัสแบบบ้าน
        /// </summary>
        /// <example></example>
        public string Code { get; set; }
        /// <summary>
        ///  ชื่อแบบบ้าน (TH)
        /// </summary>
        public string NameTH { get; set; }
        /// <summary>
        ///  ชื่อแบบบ้าน (EN)
        /// </summary>
        public string NameEN { get; set; }
        /// <summary>
        /// ประเภทบ้าน
        /// Master/api/TypeOfRealEstates/DropdownList
        /// </summary>
        public MST.TypeOfRealEstateDropdownDTO TypeOfRealEstate { get; set; }

        public bool? IsSelcted { get; set; }
        public bool? IsDisabled { get; set; }

        public static ModelDropdownDTO CreateFromModel(Model model)
        {
            if (model != null)
            {
                var result = new ModelDropdownDTO
                {
                    Id = model.ID,
                    Code = model.Code,
                    NameEN = model.NameEN,
                    NameTH = model.NameTH,
                    TypeOfRealEstate = MST.TypeOfRealEstateDropdownDTO.CreateFromModel(model.TypeOfRealEstate)
                };

                return result;
            }
            else
            {
                return null;
            }
        }

        public static async Task<ModelDropdownDTO> CreateFromModelForSpecMaterialAsync(Model model, Guid collectionID, DatabaseContext db)
        {
            if (model != null)
            {
                var result = new ModelDropdownDTO
                {
                    Id = model.ID,
                    Code = model.Code,
                    NameEN = model.NameEN,
                    NameTH = model.NameTH,
                    TypeOfRealEstate = MST.TypeOfRealEstateDropdownDTO.CreateFromModel(model.TypeOfRealEstate)
                };

                var specCollection = await db.SpecMaterialCollections.FirstOrDefaultAsync(o => o.ProjectID == model.ProjectID && o.ID == collectionID);
                if (specCollection?.ID == model.SpecMaterialCollectionID)
                {
                    result.IsSelcted = true;
                }
                else
                {
                    result.IsSelcted = false;
                }

                if (collectionID == model.SpecMaterialCollectionID)
                {
                    result.IsDisabled = false;
                }
                else
                {
                    if (model.SpecMaterialCollectionID == null)
                    {
                        result.IsDisabled = false;
                    }
                    else
                    {
                        result.IsDisabled = true;
                    }
                }

                return result;
            }
            else
            {
                return null;
            }
        }

 
    }
}
