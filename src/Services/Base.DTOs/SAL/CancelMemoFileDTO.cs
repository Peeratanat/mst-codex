using Database.Models;
using Database.Models.SAL;
using FileStorage;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Base.DTOs.SAL
{
    public class CancelMemoFileDTO : BaseDTO
    {
        /// <summary>
        /// Memo ใบยกเลิก
        /// </summary>
        public CancelMemoDTO CancelMemo { get; set; }

        /// <summary>
        /// ชื่อไฟล์
        /// </summary>
        public string FileName { get; set; }
        /// <summary>
        /// ไฟล์แนบ
        /// </summary>
        public FileDTO Files { get; set; }
        /// <summary>
        /// ประเภทไฟล์แนบ 1=หลักฐานการกู้เงินไม่ผ่าน/2=สำเนา Book Bank
        /// </summary>
        public int FileType { get; set; }

        public static async Task<CancelMemoFileDTO> CreateFromModel(CancelMemoFile model, DatabaseContext DB, FileHelper fileHelper)
        {

            if (model != null)
            {
                CancelMemoFileDTO result = new CancelMemoFileDTO()
                {
                    CancelMemo = await CancelMemoDTO.CreateFromModelAsync(model.CancelMemo, DB),
                    FileName = model.Name,
                    Files = await FileDTO.CreateFromFileNameAsync(model.File, fileHelper),
                    FileType = model.FileType
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
