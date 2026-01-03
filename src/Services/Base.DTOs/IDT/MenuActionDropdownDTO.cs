using Database.Models.USR;

namespace Base.DTOs.IDT
{
    public class MenuActionDropdownDTO : BaseDTO
    {

        /// <summary>
        /// Module Code
        /// </summary>
        public string MenuActionCode { get; set; }

        /// <summary>
        /// Module DisplayName
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// Module NameTH
        /// </summary>
        public string MenuActioname { get; set; }

        /// <summary>
        /// Module NameEng
        /// </summary>
        public MenuDropdownDTO MenuDropdownDTO { get; set; }


        public static MenuActionDropdownDTO CreateFromModel(MenuAction model)
        {
            if (model != null)
            {
                var result = new MenuActionDropdownDTO()
                {
                    Id = model.ID,
                    MenuActionCode = model.MenuActionCode,
                    DisplayName = $"{model.MenuActionCode} - {model.MenuActionName}",
                    MenuActioname = model.MenuActionName,
                    MenuDropdownDTO = MenuDropdownDTO.CreateFromModel(model.Menu),

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
