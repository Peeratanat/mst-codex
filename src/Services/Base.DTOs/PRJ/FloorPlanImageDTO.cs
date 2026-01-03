using System;
using System.Threading.Tasks;
using Database.Models.PRJ;
using FileStorage;

namespace Base.DTOs.PRJ
{
    public class FloorPlanImageDTO : BaseDTO
    {
        public string Name { get; set; }
        public FileDTO File { get; set; }

        public static async Task<FloorPlanImageDTO> CreateFromModelAsync(FloorPlanImage model, FileHelper fileHelper)
        {
            if (model != null)
            {
                var result = new FloorPlanImageDTO()
                {
                    Id = model.ID,
                    Name = model.Name,
                    Updated = model.Updated,
                    UpdatedBy = model.UpdatedBy?.DisplayName
                };

                result.File = await FileDTO.CreateFromBucketandFileNameAsync("projects",model.FileName, fileHelper);

                return result;
            }
            else
            {
                return null;
            }
        }

        public void ToModel(ref FloorPlanImage model)
        {
            model.Name = this.Name;
            model.FileName = this.File?.Name;
        }
    }
}
