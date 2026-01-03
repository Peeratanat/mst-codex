using Database.Models;
using Database.Models.SAL;
using FileStorage;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Base.DTOs.SAL
{
    public class AgreementFileDTO : BaseDTO
    {
        /// <summary>
        /// เลขที่สัญญา
        /// </summary>
        public string AgreementNo { get; set; }
        /// <summary>
        /// เลขที่เอกสารอ้างอิง
        /// </summary>
        public Guid? RefID { get; set; }
        /// <summary>
        /// สัญญา
        /// </summary>
        public SAL.AgreementDropdownDTO Agreement { get; set; }
        /// <summary>
        /// ประเภทเอกสาร
        /// </summary>
        public string DocType { get; set; }
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

        public static async Task<AgreementFileDTO> CreateFromModel(AgreementFile model, FileHelper fileHelper)
        {
            if (model != null )
            {
                AgreementFileDTO result = new AgreementFileDTO()
                {
                    AgreementNo = model.Agreement.AgreementNo,
                    Id = model.ID,
                    Agreement = SAL.AgreementDropdownDTO.CreateFromModel(model.Agreement),
                    FileName = model.FileName,
                    FilePath = model.FilePath,
                    DocType = model.DocType,
                    RefID = model.RefID,
                    Files = await FileDTO.CreateFromFileNameAsync(model.FilePath, fileHelper),
                    Updated = model.Updated,
                    UpdatedBy = string.IsNullOrEmpty(model?.UpdatedBy?.DisplayName) ? model?.CreatedBy?.DisplayName : model?.UpdatedBy?.DisplayName,
                    Created = model.Created,
                    CreatedBy = string.IsNullOrEmpty(model?.CreatedBy?.DisplayName) ? model?.CreatedBy?.DisplayName : model?.CreatedBy?.DisplayName
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
