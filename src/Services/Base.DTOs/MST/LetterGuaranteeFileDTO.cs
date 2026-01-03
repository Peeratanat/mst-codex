using FileStorage;
using Database.Models;
using Database.Models.Migrations;
using Database.Models.PRJ;
using Database.Models.SAL;
using FileStorage;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Base.DTOs.MST
{
    public class LetterGuaranteeFileDTO : BaseDTO
    {

        /// <summary>
        /// ชื่อไฟล์
        /// </summary>
        public string FileName { get; set; }
        /// <summary>
        /// path ไฟล์
        /// </summary>
        public string FilePath { get; set; }
        /// <summary>
        /// ไฟล์แนบ
        /// </summary>
        public FileDTO Files { get; set; }

        public string CreatedBy { get; set; }

        public DateTime? Created { get; set; }
        public string LGTypeName { get; set; }
        public string LGTypeKey { get; set; }

        public static async Task<LetterGuaranteeFileDTO> CreateFromModel(LetterGuaranteeFile model, FileHelper fileHelper)
        {
            if (model != null )
            {
                fileHelper._defaultBucket = "master-data";
                LetterGuaranteeFileDTO result = new LetterGuaranteeFileDTO()
                {
                    
                    Id = model.ID,
                    FileName = model.FileName,
                    FilePath = model.FilePath,
                    LGTypeName = model.LetterGuarantee?.LGType?.Name,
                    LGTypeKey = model.LetterGuarantee?.LGType?.Key,
                    Files = await FileDTO.CreateFromFileNameAsync(model.FilePath, fileHelper),
                    Updated = model.Updated,
                    UpdatedBy = model.UpdatedBy.DisplayName,
                    Created = model.Created,
                    CreatedBy = model.CreatedBy.DisplayName,
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
