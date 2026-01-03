using Database.Models.USR;

namespace Base.DTOs.IDT
{
    public class ModuleDropdownDTO : BaseDTO
    {

        /// <summary>
        /// Module Code
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// Module DisplayName
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// Module NameTH
        /// </summary>
        public string NameTH { get; set; }

        /// <summary>
        /// Module NameEng
        /// </summary>
        public string NameEng { get; set; }


        public static ModuleDropdownDTO CreateFromModel(Module model)
        {
            if (model != null)
            {
                var result = new ModuleDropdownDTO()
                {
                    Id = model.ID,
                    Code = model.Code,
                    DisplayName = $"{model.Code} - {model.NameTH}",
                    NameTH = model.NameTH,
                    NameEng = model.NameEng,
                    
                    Updated = model.Updated.HasValue ? model.Updated  : model.Created,
                    UpdatedBy = string.IsNullOrEmpty(model.UpdatedBy?.DisplayName) ? model.UpdatedBy?.DisplayName : model.CreatedBy?.DisplayName,
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
