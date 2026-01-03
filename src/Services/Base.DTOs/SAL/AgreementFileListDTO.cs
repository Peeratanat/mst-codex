using Base.DTOs.MST;
using Base.DTOs.SAL.Sortings;
using Database.Models;
using Database.Models.DbQueries.SAL;
using Database.Models.MST;
using Database.Models.PRJ;
using Database.Models.SAL;
using FileStorage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.DTOs.SAL
{
    public class AgreementFileListDTO : BaseDTO
    {
        public Guid BookingID { get; set; }
        public DateTime? BookingDate { get; set; }
        public string BookingNo { get; set; }
        public string ProjectNo { get; set; }
        public string ProjectNameTH { get; set; }
        public string UnitNo { get; set; }
        public Guid? AgreementID { get; set; }
        public string AgreementNo { get; set; }
        public bool? IsPrintApproved { get; set; }
        public Guid? PrintApprovedByUserID { get; set; }
        public DateTime? PrintApprovedDate { get; set; }
        public string FileName { get; set; }
        public string DocType { get; set; }
        public string DocYearMonth { get; set; }
        public string FilePath { get; set; }
        public FileDTO Files { get; set; }
        public string Uploader { get; set; }
        public DateTime? UploadDate { get; set; }
        public string AGOwnerName { get; set; }
        public string Remark { get; set; }
        public Guid? AgreementFileID { get; set; }

        public static async Task<AgreementFileListDTO> CreateFromModel(dbqAgreementFileList model, FileHelper fileHelper)
        {
            if (model != null)
            {
                AgreementFileListDTO result = new AgreementFileListDTO()
                {
                    BookingID = model.BookingID,
                    BookingDate = model.BookingDate,
                    BookingNo = model.BookingNo,
                    ProjectNo =   model.ProjectNo,
                    ProjectNameTH = model.ProjectNameTH,
                    UnitNo = model.UnitNo,
                    AgreementID = model.AgreementID,
                    AgreementNo = model.AgreementNo,
                    IsPrintApproved = model.IsPrintApproved,
                    PrintApprovedByUserID = model.PrintApprovedByUserID,
                    PrintApprovedDate = model.PrintApprovedDate,
                    FileName = model.FileName,
                    DocType = model.DocType,
                    DocYearMonth = model.DocYearMonth,
                    FilePath = model.FilePath,
                    Files = await FileDTO.CreateFromFileNameAsync(model.FilePath, fileHelper),
                    Uploader = model.Uploader,
                    UploadDate = model.UploadDate,
                    AGOwnerName = model.AGOwnerName,
                    Remark = model.Remark,
                    AgreementFileID = model.AgreementFileID
                    ////Updated = model.Updated,
                    ////UpdatedBy = string.IsNullOrEmpty(model?.UpdatedBy?.DisplayName) ? model?.CreatedBy?.DisplayName : model?.UpdatedBy?.DisplayName
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
