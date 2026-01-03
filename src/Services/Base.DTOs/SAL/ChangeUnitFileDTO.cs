using FileStorage;
using System;
using System.Threading.Tasks;
using Database.Models.SAL;
using Database.Models;

namespace Base.DTOs.SAL
{
    /// <summary>
    /// ไฟล์แนบการตั้งเรื่องย้ายแปลง
    /// </summary>
    public class ChangeUnitFileDTO : BaseDTO
    {
        public string Name { get; set; }
        public FileDTO File { get; set; }

        public static async Task<ChangeUnitFileDTO> CreateFromModel(ChangeUnitFile model, DatabaseContext DB, FileHelper fileHelper)
        {
            if (model != null)
            {
                ChangeUnitFileDTO result = new ChangeUnitFileDTO()
                {
                    Name = model.Name,
                    File = await FileDTO.CreateFromFileNameAsync(model.Name, fileHelper),
                    Id = model.ID,
                    Updated = model.Updated == null ? model.Created : model.Updated,
                    UpdatedBy = model.UpdatedBy == null ? model.CreatedBy.DisplayName : model.UpdatedBy.DisplayName
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
