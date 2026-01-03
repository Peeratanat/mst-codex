using Database.Models.USR;

namespace Base.DTOs.IDT
{
    public class MenuDropdownDTO : BaseDTO
    {

        /// <summary>
        /// Module Code
        /// </summary>
        public string MenuCode { get; set; }

        /// <summary>
        /// Module DisplayName
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// Module NameTH
        /// </summary>
        public string MenuNameTH { get; set; }

        /// <summary>
        /// Module NameEng
        /// </summary>
        public string MenuNameEng { get; set; }

        /// <summary>
        /// Module NameEng
        /// </summary>
        public ModuleDropdownDTO ModuleDropdownDTO { get; set; }


        public static MenuDropdownDTO CreateFromModel(Menu model)
        {
            if (model != null)
            {
                var result = new MenuDropdownDTO()
                {
                    Id = model.ID,
                    MenuCode = model.MenuCode,
                    DisplayName = $"{model.MenuCode} - {model.MenuNameTH}",
                    MenuNameTH = model.MenuNameTH,
                    MenuNameEng = model.MenuNameEng,
                    ModuleDropdownDTO = ModuleDropdownDTO.CreateFromModel(model.Module),

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
