using Database.Models;
using Database.Models.SAL;
using FileStorage;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Base.DTOs.SAL
{
    public class ChangeAgreementOwnerWorkflowFileDTO : BaseDTO
    {
        /// <summary>
        /// สัญญา
        /// </summary>
        public SAL.AgreementDropdownDTO Agreement { get; set; }
        /// <summary>
        /// เปลี่ยนแปลงชื่อผู้ทำสัญญา
        /// </summary>
        public SAL.ChangeAgreementOwnerWorkflowDTO ChangeAgreementOwner { get; set; }
        /// <summary>
        /// ID
        /// </summary>
        public Guid ID { get; set; }
        /// <summary>
        /// ชื่อไฟล์
        /// </summary>
        public string FileName { get; set; }
        /// <summary>
        /// ไฟล์แนบ
        /// </summary>
        public FileDTO Files { get; set; }
        /// <summary>
        /// วันที่Upload
        /// </summary>
        public DateTime? CreateDate { get; set; }
        public string CreatedBy { get; set; }

        public DateTime? Created { get; set; }

        public static async Task<ChangeAgreementOwnerWorkflowFileDTO> CreateFromModel(ChangeAgreementOwnerFile model, DatabaseContext DB, FileHelper fileHelper)
        {
            
            if (model != null)
            {
                ChangeAgreementOwnerWorkflowFileDTO result = new ChangeAgreementOwnerWorkflowFileDTO()
                {
                    Agreement = AgreementDropdownDTO.CreateFromModel(model.ChangeAgreementOwnerWorkflow.Agreement),
                    ChangeAgreementOwner = ChangeAgreementOwnerWorkflowDTO.CreateFromModelAsync(model.ChangeAgreementOwnerWorkflow, DB),
                    FileName = model.Name,
                    ID = model.ID,
                    CreateDate = model.Created,
                    Updated = model.Updated == null ? model.Created : model.Updated,
                    UpdatedBy = model.UpdatedBy == null ? model.CreatedBy.DisplayName : model.UpdatedBy.DisplayName,
                    Files = await FileDTO.CreateFromFileNameAsync(model.Name, fileHelper)

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
