using System.Data;
using Base.DTOs;
using Base.DTOs.PRM;
using Common.Helper.Logging;
using Database.Models;
using Database.Models.PRM;
using FileStorage;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MST_Promotion.Params.Filters;
using MST_Promotion.Services.Excel;
using OfficeOpenXml;
using ExcelExtensions;
using MST_Promotion.Params.Outputs;
using System.Data.Common;
using Dapper;
using Database.Models.DbQueries.PRM;
using Database.Models.DbQueries;
using Microsoft.EntityFrameworkCore.Storage;
using PagingExtensions;

namespace MST_Promotion.Services
{
    public class MappingAgreementService : IMappingAgreementService
    {
        private readonly DatabaseContext DB;
        private readonly IConfiguration Configuration;
        private FileHelper FileHelper;
        public LogModel logModel { get; set; }
        int Timeout = 300;
        public MappingAgreementService(DatabaseContext db)
        {
            //this.Configuration = configuration;
            logModel = new LogModel("MappingAgreementService", null);
            this.DB = db;
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(Environment.GetEnvironmentVariable("DBConnectionString"));

            Timeout = builder.ConnectTimeout;

            DB.Database.SetCommandTimeout(Timeout);

            var minioSapEndpoint = Environment.GetEnvironmentVariable("minio_Endpoint");
            var minioSapAccessKey = Environment.GetEnvironmentVariable("minio_AccessKey");
            var minioSapSecretKey = Environment.GetEnvironmentVariable("minio_SecretKey");
            var minioSapWithSSL = Environment.GetEnvironmentVariable("minio_WithSSL");
            // var minioBucketName = Environment.GetEnvironmentVariable("minio_DefaultBucket");
            // var minioTempBucketName = Environment.GetEnvironmentVariable("minio_TempBucket");
            var publicURL = Environment.GetEnvironmentVariable("minio_PublicURL");

            this.FileHelper = new FileHelper(minioSapEndpoint, minioSapAccessKey, minioSapSecretKey, "prc", "", publicURL, minioSapWithSSL == "true");
        }

        /// <summary>
        /// สร้าง List ของ Mapping Agreement โดยการ Upload Excel File
        /// UI: https://projects.invisionapp.com/d/main#/console/17482068/366447264/preview
        /// </summary>
        /// <returns>The mapping agreements data from excel async.</returns>
        /// <param name="input">Input.</param>
        public async Task<ImportMappingAgreementDTO> GetMappingAgreementsDataFromExcelAsync(FileDTO input)
        {
            //ดูตัวอย่างการสร้าง unit test File Upload ที่ Project > PriceListUnitTest
            //Url ตัวอย่างไฟล์ http://192.168.2.29:9001/xunit-tests/Export_MappingAgreement.xlsx
            var dt = await this.ConvertExcelToDataTable(input);
            /// Valudate Header
            if (dt.Columns.Count != 7)
            {
                throw new Exception("Invalid File Format");
            }
            //Read Excel Model
            var mappingAgreementExcels = new List<MappingAgreementExcelModel>();
            foreach (DataRow r in dt.Rows)
            {
                var excelModel = MappingAgreementExcelModel.CreateFromDataRow(r);
                mappingAgreementExcels.Add(excelModel);
            }
            List<MappingAgreementDTO> mappingAgreements = new List<MappingAgreementDTO>();
            var RunningNumber = 1;
            var AllMapAgreement = await DB.MappingAgreements.ToListAsync();
            foreach (var item in mappingAgreementExcels)
            {
                var materialGroupKey_Old = await DB.PromotionMaterials.Where(o => o.Code == item.OldMaterialCode).OrderByDescending(o => o.Created).FirstOrDefaultAsync();
                var materialGroupKey_New = await DB.PromotionMaterials.Where(o => o.Code == item.NewMaterialCode).OrderByDescending(o => o.Created).FirstOrDefaultAsync();

                MappingAgreementDTO mappingAgreement = new MappingAgreementDTO();
                mappingAgreement.No = RunningNumber;
                mappingAgreement.NewAgreement = item.NewAgreementNo;
                mappingAgreement.NewItem = item.NewItemNo;
                mappingAgreement.NewMaterialCode = item.NewMaterialCode;
                mappingAgreement.OldAgreement = item.OldAgreementNo;
                mappingAgreement.OldItem = item.OldItemNo;
                mappingAgreement.OldMaterialCode = item.OldMaterialCode;
                mappingAgreement.Remark = item.Remartk;

                if (string.IsNullOrEmpty(mappingAgreement.NewAgreement))
                {
                    mappingAgreement.IsValidData = false;
                    mappingAgreement.MsgError = "กรุณาระบุ New Agreement";
                }
                else if (string.IsNullOrEmpty(mappingAgreement.NewItem))
                {
                    mappingAgreement.IsValidData = false;
                    mappingAgreement.MsgError = "กรุณาระบุ New Item";
                }
                else if (string.IsNullOrEmpty(mappingAgreement.NewMaterialCode))
                {
                    mappingAgreement.IsValidData = false;
                    mappingAgreement.MsgError = "กรุณาระบุ New MaterialCode";
                }
                else if (string.IsNullOrEmpty(mappingAgreement.OldAgreement))
                {
                    mappingAgreement.IsValidData = false;
                    mappingAgreement.MsgError = "กรุณาระบุ Old Agreement";
                }
                else if (string.IsNullOrEmpty(mappingAgreement.OldItem))
                {
                    mappingAgreement.IsValidData = false;
                    mappingAgreement.MsgError = "กรุณาระบุ Old Item";
                }
                else if (string.IsNullOrEmpty(mappingAgreement.OldMaterialCode))
                {
                    mappingAgreement.IsValidData = false;
                    mappingAgreement.MsgError = "กรุณาระบุ Old MaterialCode";
                }
                else
                {
                    mappingAgreement.OldItem = mappingAgreement.OldItem.PadLeft(5, '0');
                    mappingAgreement.NewItem = mappingAgreement.NewItem.PadLeft(5, '0');
                    if (mappingAgreement.NewMaterialCode.IndexOf("-") <= 0)
                        mappingAgreement.NewMaterialCode = mappingAgreement.NewMaterialCode.PadLeft(18, '0');
                    if (mappingAgreement.OldMaterialCode.IndexOf("-") <= 0)
                        mappingAgreement.OldMaterialCode = mappingAgreement.OldMaterialCode.PadLeft(18, '0');

                    if (mappingAgreement.OldAgreement == mappingAgreement.NewAgreement
                        && mappingAgreement.OldItem == mappingAgreement.NewItem
                        )
                    {
                        mappingAgreement.IsValidData = false;
                        mappingAgreement.MsgError = "ข้อมูล Old Agreement,Old Item ซ้ำกับ New Agreement,New Item";
                    }
                    else if (mappingAgreement.OldMaterialCode.Length != mappingAgreement.NewMaterialCode.Length)
                    {
                        mappingAgreement.IsValidData = false;
                        mappingAgreement.MsgError = "Old MaterialCode และ New MaterialCode Format ไม่ตรงกัน";
                    }
                    else if (mappingAgreement.OldMaterialCode.Length == mappingAgreement.NewMaterialCode.Length)
                    {
                        if (materialGroupKey_Old != null && materialGroupKey_New != null)
                        {
                            if (materialGroupKey_Old.MaterialGroupKey != materialGroupKey_New.MaterialGroupKey)
                            {
                                mappingAgreement.IsValidData = false;
                                mappingAgreement.MsgError = "Old MaterialCode :" + materialGroupKey_Old.MaterialGroupKey + " และ New MaterialCode : " + materialGroupKey_New.MaterialGroupKey + " ไม่สามารถ Mapping Agreement ได้ เนื่องจาก MaterialCode ไม่ตรงกัน";

                            }
                        }

                        if (string.IsNullOrEmpty(mappingAgreement.MsgError))
                        {
                            // check duplicate
                            var haveOld = AllMapAgreement.Where(x => x.OldAgreement.Equals(mappingAgreement.OldAgreement)
                            && x.NewAgreement.Equals(mappingAgreement.NewAgreement)
                            && x.OldItem.Equals(mappingAgreement.OldItem)
                            && x.NewItem.Equals(mappingAgreement.NewItem)
                            && x.OldMaterialCode.Equals(mappingAgreement.OldMaterialCode)
                            && x.NewMaterialCode.Equals(mappingAgreement.NewMaterialCode)
                            ).FirstOrDefault();
                            if (haveOld != null)
                            {
                                mappingAgreement.IsValidData = false;
                                mappingAgreement.MsgError = "พบข้อมูล MappingAgreement ในระบบแล้ว กรุณาระบุด้วยข้อมูลใหม่";
                            }
                            else
                            {
                                mappingAgreement.IsValidData = true;
                            }
                        }
                    }
                    else
                    {
                        // check duplicate
                        var haveOld = AllMapAgreement.Where(x => x.OldAgreement.Equals(mappingAgreement.OldAgreement)
                        && x.NewAgreement.Equals(mappingAgreement.NewAgreement)
                        && x.OldItem.Equals(mappingAgreement.OldItem)
                        && x.NewItem.Equals(mappingAgreement.NewItem)
                        && x.OldMaterialCode.Equals(mappingAgreement.OldMaterialCode)
                        && x.NewMaterialCode.Equals(mappingAgreement.NewMaterialCode)
                        ).FirstOrDefault();
                        if (haveOld != null)
                        {
                            mappingAgreement.IsValidData = false;
                            mappingAgreement.MsgError = "พบข้อมูล MappingAgreement ในระบบแล้ว กรุณาระบุด้วยข้อมูลใหม่";
                        }
                        else
                        {
                            mappingAgreement.IsValidData = true;
                        }
                    }
                }
                mappingAgreements.Add(mappingAgreement);
                RunningNumber++;
            }
            ImportMappingAgreementDTO result = new ImportMappingAgreementDTO();
            result.MappingAgreementList = mappingAgreements;
            result.TotalSuccess = mappingAgreements.Where(x => x.IsValidData == true).Count();
            result.TotalError = mappingAgreements.Where(x => x.IsValidData == false).Count();
            return result;
        }

        /// <summary>
        /// ล้างข้อมูล Mapping Agreement เก่าทิ้งทั้งหมด แล้ว Insert เข้าไปใหม่
        /// UI: https://projects.invisionapp.com/d/main#/console/17482068/366447264/preview
        /// </summary>
        /// <returns>The import mapping agreements async.</returns>
        /// <param name="inputs">Inputs.</param>
        public async Task<List<MappingAgreementDTO>> ConfirmImportMappingAgreementsAsync(ImportMappingAgreementDTO inputs)
        {
            var allData = await DB.MappingAgreements.ToListAsync();
            List<MappingAgreement> updateList = new List<MappingAgreement>();
            var mappingAgreements = new List<MappingAgreement>();
            var listData = inputs.MappingAgreementList.Where(x => x.IsValidData == true).ToList();
            foreach (var item in listData)
            {
                MappingAgreement model = new MappingAgreement();
                var oldAg = allData.Where(x => x.NewAgreement == item.OldAgreement && x.NewItem == item.OldItem && x.NewMaterialCode == item.OldMaterialCode).ToList();
                foreach (var modelOld in oldAg)
                {
                    modelOld.IsDeleted = true;
                    updateList.Add(modelOld);
                }
                item.ToModel(ref model);
                mappingAgreements.Add(model);
            }
            DB.UpdateRange(updateList);
            await DB.AddRangeAsync(mappingAgreements);
            await DB.SaveChangesAsync();

            var results = new List<MappingAgreementDTO>();
            return results;
        }

        /// <summary>
        /// Export Excel โดยใช้ไฟล์ Template 
        /// UI: https://projects.invisionapp.com/d/main#/console/17482068/366447264/preview
        /// </summary>
        /// <returns>The mapping agreements async.</returns>
        public async Task<FileDTO> ExportMappingAgreementsAsync(MappingAgreementFilter filter, MappingAgreementSortByParam sortByParam, CancellationToken cancellationToken = default)
        {
            ExportExcel result = new ExportExcel();
            var dataTmp = await GetMappingAgreementsList(filter, new PageParam(), sortByParam,cancellationToken);
            var data = dataTmp.MappingAgreements;
            string path = Path.Combine(FileHelper.GetApplicationRootPath(), "ExcelTemplates", "Export_MappingAgreement.xlsx");
            byte[] tmp = File.ReadAllBytes(path);
            using (System.IO.MemoryStream stream = new System.IO.MemoryStream(tmp))
            using (ExcelPackage package = new ExcelPackage(stream))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets.First();

                int _oldAgreementNoIndex = MappingAgreementExcelModel._oldAgreementNoIndex + 1;
                int _oldItemNoIndex = MappingAgreementExcelModel._oldItemNoIndex + 1;
                int _oldMaterialCodeIndex = MappingAgreementExcelModel._oldMaterialCodeIndex + 1;
                int _newAgreementNoIndex = MappingAgreementExcelModel._newAgreementNoIndex + 1;
                int _newItemNoIndex = MappingAgreementExcelModel._newItemNoIndex + 1;
                int _newMaterialCodeIndex = MappingAgreementExcelModel._newMaterialCodeIndex + 1;
                int _remarkIndex = MappingAgreementExcelModel._remarkIndex + 1;
                int _createDateIndex = MappingAgreementExcelModel._createDateIndex + 1;

                for (int c = 3; c < data.Count + 3; c++)
                {
                    worksheet.Cells[c, _oldAgreementNoIndex].Value = data[c - 3].OldAgreement;
                    worksheet.Cells[c, _oldItemNoIndex].Value = data[c - 3].OldItem;
                    worksheet.Cells[c, _oldMaterialCodeIndex].Value = data[c - 3].OldMaterialCode;
                    worksheet.Cells[c, _newAgreementNoIndex].Value = data[c - 3].NewAgreement;
                    worksheet.Cells[c, _newItemNoIndex].Value = data[c - 3].NewItem;
                    worksheet.Cells[c, _newMaterialCodeIndex].Value = data[c - 3].NewMaterialCode;
                    worksheet.Cells[c, _remarkIndex].Value = data[c - 3].Remark;
                    worksheet.Cells[c, _createDateIndex].Value = data[c - 3].Created.GetValueOrDefault().ToString("dd/MM/yyyy");
                }

                //worksheet.Cells.AutoFitColumns();

                result.FileContent = package.GetAsByteArray();
                result.FileName = "Export_MappingAgreement.xlsx";
                result.FileType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            }

            Stream fileStream = new MemoryStream(result.FileContent);
            string fileName = $"{result.FileName}";
            string contentType = result.FileType;
            string filePath = $"mapping-agreement/";

            var uploadResult = await this.FileHelper.UploadFileFromStreamWithTimestamp(fileStream, filePath, fileName, contentType);

            return new FileDTO()
            {
                Name = uploadResult.Name,
                Url = uploadResult.Url
            };
        }
        public async Task<DataTable> ConvertExcelToDataTable(FileDTO input)
        {
            var excelStream = await FileHelper.GetStreamFromUrlAsync(input.Url);
            string fileName = input.Name;
            var fileExtention = fileName != null ? fileName.Split('.').ToList().Last() : null;

            bool hasHeader = true;
            using (Stream stream = new MemoryStream(XLSToXLSXConverter.ReadFully(excelStream)))
            {
                byte[] excelByte;
                if (fileExtention == "xls")
                {
                    excelByte = XLSToXLSXConverter.Convert(stream);
                }
                else
                {
                    excelByte = XLSToXLSXConverter.ReadFully(stream);
                }
                using (System.IO.MemoryStream xlsxStream = new System.IO.MemoryStream(excelByte))
                using (var pck = new OfficeOpenXml.ExcelPackage(xlsxStream))
                {
                    var ws = pck.Workbook.Worksheets.First();
                    DataTable tbl = new DataTable();
                    foreach (var firstRowCell in ws.Cells[1, 1, 1, ws.Dimension.End.Column])
                    {
                        tbl.Columns.Add(hasHeader ? firstRowCell.Text : string.Format("Column {0}", firstRowCell.Start.Column));
                    }
                    var startRow = hasHeader ? 3 : 2;
                    for (int rowNum = startRow; rowNum <= ws.Dimension.End.Row; rowNum++)
                    {
                        var wsRow = ws.Cells[rowNum, 1, rowNum, ws.Dimension.End.Column];
                        DataRow row = tbl.Rows.Add();
                        foreach (var cell in wsRow)
                        {
                            row[cell.Start.Column - 1] = cell.Text;
                        }
                    }

                    return tbl;
                }
            }
        }
        public async Task<MappingAgreementPaging> GetMappingAgreementsList(MappingAgreementFilter filter, PageParam pageParam, MappingAgreementSortByParam sortByParam, CancellationToken cancellationToken = default)
        {
            using DbCommand cmd = DB.Database.GetDbConnection().CreateCommand();
            DynamicParameters ParamList = new();
            #region filter
            if (!string.IsNullOrEmpty(filter.OldAgreement))
            {
                ParamList.Add("OldAgreement", filter.OldAgreement);
            }
            if (!string.IsNullOrEmpty(filter.OldItem))
            {
                ParamList.Add("OldItem", filter.OldItem);
            }
            if (!string.IsNullOrEmpty(filter.NewAgreement))
            {
                ParamList.Add("NewAgreement", filter.NewAgreement);
            }
            if (!string.IsNullOrEmpty(filter.NewItem))
            {
                ParamList.Add("NewItem", filter.NewItem);
            }
            if (!string.IsNullOrEmpty(filter.MaterialCode))
            {
                ParamList.Add("MaterialCode", filter.MaterialCode);
            }
            if (!string.IsNullOrEmpty(filter.MaterialName))
            {
                ParamList.Add("MaterialName", filter.MaterialName);
            }
            if (!string.IsNullOrEmpty(filter.MaterialType))
            {
                ParamList.Add("MaterialType", filter.MaterialType);
            }
            if (!string.IsNullOrEmpty(filter.Create))
            {
                ParamList.Add("Create", filter.Create);
            }
            if (filter.CreatedFrom != null)
            {
                ParamList.Add("CreatedFrom", filter.CreatedFrom);
            }
            if (filter.CreatedTo != null)
            {
                ParamList.Add("CreatedTo", filter.CreatedTo);
            }
            ParamList.Add("Page", pageParam?.Page ?? 1);
            ParamList.Add("PageSize", pageParam?.PageSize ?? 99999);
            #endregion
            #region Sort
            var sortby = string.Empty;
            bool sort = false;
            sortby = nameof(dbqMappingAgreement.Created);
            if (sortByParam.SortBy != null)
            {
                sort = sortByParam.Ascending;
                switch (sortByParam.SortBy.Value)
                {
                    case MappingAgreementSortBy.OldAgreement:
                        sortby = nameof(dbqMappingAgreement.OldAgreement);
                        break;
                    case MappingAgreementSortBy.OldItem:
                        sortby = nameof(dbqMappingAgreement.OldItem);
                        break;
                    case MappingAgreementSortBy.OldMaterialCode:
                        sortby = nameof(dbqMappingAgreement.OldMaterialCode);
                        break;
                    case MappingAgreementSortBy.NewAgreement:
                        sortby = nameof(dbqMappingAgreement.NewAgreement);
                        break;
                    case MappingAgreementSortBy.NewItem:
                        sortby = nameof(dbqMappingAgreement.NewItem);
                        break;
                    case MappingAgreementSortBy.NewMaterialCode:
                        sortby = nameof(dbqMappingAgreement.NewMaterialCode);
                        break;
                    case MappingAgreementSortBy.Created:
                        sortby = nameof(dbqMappingAgreement.Created);
                        break;
                    case MappingAgreementSortBy.CreateBy:
                        sortby = nameof(dbqMappingAgreement.CreateBy);
                        break;
                    default:
                        sortby = nameof(dbqMappingAgreement.Created);
                        break;
                }
            }
            ParamList.Add("Sys_SortBy", sortby);
            ParamList.Add("Sys_SortType", sort ? "asc" : "desc");

            #endregion

            CommandDefinition commandDefinition = new(
                                                         commandText: DBStoredNames.spMappingAgreementList,
                                                         parameters: ParamList,
                                                         commandTimeout: Timeout,
                                                         cancellationToken: cancellationToken,
                                                         transaction: DB?.Database?.CurrentTransaction?.GetDbTransaction(),
                                                         commandType: CommandType.StoredProcedure);
            var querylist = (await cmd.Connection.QueryAsync<dbqMappingAgreement>(commandDefinition))?.ToList() ?? new();

            var results = new List<MappingAgreementDTO>();
            PageOutput pageout = new PageOutput();
            if (querylist.Count > 0)
            {
                results = querylist.Select(o => MappingAgreementDTO.CreateFromQuery(o)).ToList();
                pageout = querylist.FirstOrDefault() != null ? querylist.FirstOrDefault().CreateBaseDTOFromQuery() : new PageOutput();
            }
            return new MappingAgreementPaging()
            {
                PageOutput = pageout,
                MappingAgreements = results
            };
        }

        public async Task DeleteMappingAgreementAsync(Guid id)
        {
            var model = await DB.MappingAgreements.Where(x => x.ID == id)
                                             .FirstAsync();
            model.IsDeleted = true;

            DB.Entry(model).State = EntityState.Modified;
            await DB.SaveChangesAsync();
        }
        public async Task<FileDTO> ExportTemplatesMappingAgreementsAsync()
        {
            ExportExcel result = new ExportExcel();
            var dataTmp = await GetMappingAgreementsList(new MappingAgreementFilter(), new PageParam(), new MappingAgreementSortByParam());
            var data = dataTmp.MappingAgreements;
            string path = Path.Combine(FileHelper.GetApplicationRootPath(), "ExcelTemplates", "Export_MappingAgreement.xlsx");
            byte[] tmp = File.ReadAllBytes(path);
            using (System.IO.MemoryStream stream = new System.IO.MemoryStream(tmp))
            using (ExcelPackage package = new ExcelPackage(stream))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets.First();

                int _oldAgreementNoIndex = MappingAgreementExcelModel._oldAgreementNoIndex + 1;
                int _oldItemNoIndex = MappingAgreementExcelModel._oldItemNoIndex + 1;
                int _oldMaterialCodeIndex = MappingAgreementExcelModel._oldMaterialCodeIndex + 1;

                for (int c = 3; c < data.Count + 3; c++)
                {
                    worksheet.Cells[c, _oldAgreementNoIndex].Value = data[c - 3].NewAgreement;
                    worksheet.Cells[c, _oldItemNoIndex].Value = data[c - 3].NewItem;
                    worksheet.Cells[c, _oldMaterialCodeIndex].Value = data[c - 3].NewMaterialCode;
                }
                worksheet.DeleteColumn(8);
                result.FileContent = package.GetAsByteArray();
                result.FileName = "Template_MappingAgreement.xlsx";
                result.FileType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            }

            Stream fileStream = new MemoryStream(result.FileContent);
            string fileName = $"{result.FileName}";
            string contentType = result.FileType;
            string filePath = $"mapping-agreement/";

            var uploadResult = await this.FileHelper.UploadFileFromStreamWithOutGuid(fileStream, null, filePath, fileName, contentType);

            return new FileDTO()
            {
                Name = uploadResult.Name,
                Url = uploadResult.Url
            };
        }
        public async Task<MappingAgreementDTO> AddMappingAgreementsAsync(MappingAgreementDTO inputs)
        {
            await inputs.ValidateAsync(DB);
            List<MappingAgreement> updateList = new List<MappingAgreement>();
            var mappingAgreements = new List<MappingAgreement>();
            MappingAgreement model = new MappingAgreement();
            var oldAg = await DB.MappingAgreements.Where(x => x.NewAgreement == inputs.OldAgreement && x.NewItem == inputs.OldItem && x.NewMaterialCode == inputs.OldMaterialCode).ToListAsync();
            foreach (var modelOld in oldAg)
            {
                modelOld.IsDeleted = true;
                updateList.Add(modelOld);
            }
            DB.UpdateRange(updateList);
            inputs.ToModel(ref model);

            await DB.MappingAgreements.AddAsync(model);
            await DB.SaveChangesAsync();

            var results = new MappingAgreementDTO();
            return results;
        }
 
    }
}
