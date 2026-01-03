using Database.Models.USR;

namespace Base.DTOs.IDT
{
    public class RoleDropdownDTO : BaseDTO
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
        public string Name { get; set; }

        public static RoleDropdownDTO CreateFromModel(Role model)
        {
            if (model != null)
            {
                var result = new RoleDropdownDTO()
                {
                    Id = model.ID,
                    Code = model.Code,
                    DisplayName = $"{model.Code} - {model.Name}",
                    Name = model.Name,
                    
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
