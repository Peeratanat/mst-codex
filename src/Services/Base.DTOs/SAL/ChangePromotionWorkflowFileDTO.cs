using FileStorage;
using System;
using System.Threading.Tasks;
using Database.Models;
using Database.Models.PRM;

namespace Base.DTOs.SAL
{
    /// <summary>
    /// ไฟล์แนบการอนุมัติเปลี่ยนแปลงโปรโมชั่น
    /// </summary>
    public class ChangePromotionWorkflowFileDTO : BaseDTO
    {
        public string Name { get; set; }
        public FileDTO File { get; set; }

        public static async Task<ChangePromotionWorkflowFileDTO> CreateFromModel(ChangePromotionWorkflowFile model, DatabaseContext DB, FileHelper fileHelper)
        {
            if (model != null)
            {
                ChangePromotionWorkflowFileDTO result = new ChangePromotionWorkflowFileDTO()
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
