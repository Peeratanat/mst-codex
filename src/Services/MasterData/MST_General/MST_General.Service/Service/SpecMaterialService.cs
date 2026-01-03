using Base.DTOs;
using Base.DTOs.MST;
using Base.DTOs.PRJ;
using Base.DTOs.SAL.Sortings;
using Database.Models;
using Database.Models.ACC;
using Database.Models.MST;
using Database.Models.PRJ;
using ErrorHandling;
using MST_General.Params.Filters;
using MST_General.Params.Outputs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using PagingExtensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using Base.DTOs.CMS;
using Commission.Services.Excels;
using Database.Models.CMS;
using Database.Models.MasterKeys;
using System.Data;
using OfficeOpenXml;
using Common.Helper.Logging;
using ExcelExtensions;
using Common.Helper;
using FileStorage;
using Report.Integration;
using Database.Models.SAL;

namespace MST_General.Services
{
    public class SpecMaterialService : ISpecMaterialService
    {
        private readonly DatabaseContext DB;
        public LogModel logModel { get; set; }
        private FileHelper FileHelper;

        public SpecMaterialService(DatabaseContext db)
        {
            logModel = new LogModel("SpecMaterialService", null);
            DB = db;


            var minioEndpoint = Environment.GetEnvironmentVariable("minio_Endpoint");
            var minioAccessKey = Environment.GetEnvironmentVariable("minio_AccessKey");
            var minioSecretKey = Environment.GetEnvironmentVariable("minio_SecretKey");
            var minioBucketName = Environment.GetEnvironmentVariable("minio_TempBucket");
            var minioTempBucketName = Environment.GetEnvironmentVariable("minio_TempBucket");
            var publicURL = Environment.GetEnvironmentVariable("minio_PublicURL");
            var withSSL = Environment.GetEnvironmentVariable("minio_WithSSL") ?? string.Empty;

            FileHelper = new FileHelper(minioEndpoint, minioAccessKey, minioSecretKey, minioBucketName, minioTempBucketName, publicURL, Convert.ToBoolean(withSSL));
        }

        public async Task<SpecMaterialCollectionPaging> GetSpecMaterialCollectionListAsync(SpecMaterialCollectionFilter filter, PageParam pageParam, SpecMaterialCollectionSortByParam sortByParam, CancellationToken cancellationToken = default)
        {

            var query = from smc in DB.SpecMaterialCollections
                       .Include(o => o.UpdatedBy)
                        select new SpecMaterialCollectionQueryResult
                        {
                            SpecMaterialCollection = smc,
                        };

            #region Filter
            if (filter.ProjectID != null)
                query = query.Where(o => o.SpecMaterialCollection.ProjectID == filter.ProjectID);
            if (!string.IsNullOrEmpty(filter.Name))
                query = query.Where(o => o.SpecMaterialCollection.Name.ToLower().Contains(filter.Name.ToLower()));
            if (filter.IsActive != null)
                query = query.Where(o => o.SpecMaterialCollection.IsActive == filter.IsActive);
            if (!string.IsNullOrEmpty(filter.UpdatedBy))
                query = query.Where(o => o.SpecMaterialCollection.UpdatedBy.DisplayName.Contains(filter.UpdatedBy));
            if (filter.UpdatedFrom != null)
                query = query.Where(o => o.SpecMaterialCollection.Updated >= filter.UpdatedFrom);
            if (filter.UpdatedFrom != null)
                query = query.Where(o => o.SpecMaterialCollection.Updated <= filter.UpdatedTo);
            #endregion 

            var pageOutput = new SpecMaterialCollectionPaging();
            var result = new List<SpecMaterialCollectionDTO>();
            if (!string.IsNullOrEmpty(filter.ModelUse))
            {
                SpecMaterialCollectionDTO.SortBy(sortByParam, ref query);
                pageParam.Page = 1;
                pageParam.PageSize = 10000;
                pageOutput.PageOutput = PagingHelper.Paging<SpecMaterialCollectionQueryResult>(pageParam, ref query);

                var queryResults = await query.ToListAsync(cancellationToken);
                result = queryResults.Select(o => SpecMaterialCollectionDTO.CreateFromQueryResult(o, DB)).ToList();
                result = result.Where(o => o.ModelUse != null && o.ModelUse.ToLower().Contains(filter.ModelUse.ToLower())).ToList();
            }
            else
            {
                SpecMaterialCollectionDTO.SortBy(sortByParam, ref query);
                pageOutput.PageOutput = PagingHelper.Paging<SpecMaterialCollectionQueryResult>(pageParam, ref query);

                var queryResults = await query.ToListAsync(cancellationToken);
                result = queryResults.Select(o => SpecMaterialCollectionDTO.CreateFromQueryResult(o, DB)).ToList();
            }


            return new SpecMaterialCollectionPaging()
            {
                PageOutput = pageOutput.PageOutput,
                SpecMaterialCollection = result
            };
        }

        public async Task<SpecMaterialCollectionDetailPaging> GetSpecMaterialCollectionDetailAsync(Guid id, SpecMaterialCollectionDetailFilter filter, PageParam pageParam, SpecMaterialCollectionDetailSortByParam sortByParam, CancellationToken cancellationToken = default)
        {
            var query = from smcd in DB.SpecMaterialCollectionDetails.AsNoTracking().Include(o => o.UpdatedBy)
                        join smc in DB.SpecMaterialCollections.AsNoTracking() on smcd.SpecMaterialCollectionID equals smc.ID
                        //join m in DB.Models on smc.ID equals m.SpecMaterialCollectionID
                        join mst in DB.MasterCenters.AsNoTracking() on smcd.SpecMaterialGroupMasterCenterID equals mst.ID
                        join item in DB.SpecMaterialItems.Include(o => o.SpecMaterialGroup).Include(o => o.UpdatedBy).AsNoTracking() on
                                   new { SpecMaterialGroupMasterCenterID = mst.ID, SpecMaterialItemID = smcd.SpecMaterialItemID ?? Guid.Empty }
                            equals new { SpecMaterialGroupMasterCenterID = item.SpecMaterialGroupMasterCenterID ?? Guid.Empty, SpecMaterialItemID = item.ID }

                            //where smcd.SpecMaterialCollectionID == id

                        select new SpecMaterialCollectionDetailQueryResult
                        {
                            SpecMaterialCollectionDetail = smcd,
                            SpecMaterialCollection = smc,
                            //Model = m,
                            Group = mst,
                            SpecMaterialItem = item,
                        };


            query = query.Where(o => o.SpecMaterialCollectionDetail.SpecMaterialCollectionID == id);
            if (filter.GroupID != null)
                query = query.Where(o => o.SpecMaterialCollectionDetail.SpecMaterialGroupMasterCenterID == filter.GroupID);
            if (!string.IsNullOrEmpty(filter.Name))
                query = query.Where(o => o.SpecMaterialCollectionDetail.SpecMaterialItem.Name.Contains(filter.Name));
            if (!string.IsNullOrEmpty(filter.ItemDescription))
                query = query.Where(o => o.SpecMaterialItem.ItemDescription.Contains(filter.ItemDescription));
            if (!string.IsNullOrEmpty(filter.NameEN))
                query = query.Where(o => o.SpecMaterialCollectionDetail.SpecMaterialItem.NameEN.Contains(filter.NameEN));
            if (!string.IsNullOrEmpty(filter.ItemDescriptionEN))
                query = query.Where(o => o.SpecMaterialItem.ItemDescription.Contains(filter.ItemDescriptionEN));
            if (filter.UpdatedFrom != null)
                query = query.Where(o => o.SpecMaterialCollectionDetail.Updated >= filter.UpdatedFrom);
            if (filter.UpdatedTo != null)
                query = query.Where(o => o.SpecMaterialCollectionDetail.Updated <= filter.UpdatedTo);
            if (!string.IsNullOrEmpty(filter.UpdatedBy))
                query = query.Where(o => o.SpecMaterialCollectionDetail.UpdatedBy.DisplayName.Contains(filter.UpdatedBy));




            SpecMaterialCollectionDetailDTO.SortBy(sortByParam, ref query);
            var pageOutput = PagingHelper.Paging<SpecMaterialCollectionDetailQueryResult>(pageParam, ref query);

            var queryResults = await query.ToListAsync(cancellationToken);
            var result = queryResults.Select(o => SpecMaterialCollectionDetailDTO.CreateFromQueryResult(o, DB)).ToList();

            return new SpecMaterialCollectionDetailPaging()
            {
                PageOutput = pageOutput,
                SpecMaterialCollectionDetail = result
            };

        }



        public async Task<List<ModelDropdownDTO>> GetUnitModelByProjectAsync(Guid projectid, Guid collectionID, CancellationToken cancellationToken = default)
        {
            var model = await DB.Models.Where(o => o.ProjectID == projectid).GroupBy(o => new { o.NameTH }).Select(o => o.FirstOrDefault()).ToListAsync(cancellationToken);
            return model.Select(o => ModelDropdownDTO.CreateFromModelForSpecMaterialAsync(o, collectionID, DB))
                .Select(o => o.Result).ToList();
        }

        public async Task<SpecMaterialCollectionDTO> GetSpecMaterialCollectionAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var model = await DB.SpecMaterialCollections.FindAsync(id, cancellationToken);
            return SpecMaterialCollectionDTO.CreateFromModel(model, DB);
        }

        public async Task<SpecMaterialCollectionDTO> EditSpecMaterialCollectionAsync(Guid ID, Guid ProjectID, bool IsActive, string Name, List<SpecMaterialCollectionDetailDTO> model)
        {
            var specCollection = await DB.SpecMaterialCollections.FindAsync(ID);
            specCollection.IsActive = IsActive;
            specCollection.Name = Name;
            DB.Update(specCollection);
            List<Model> models = new List<Model>();
            var modelUse = await DB.Models.Where(o => o.ProjectID == ProjectID).ToListAsync();
            foreach (var item in model)
            {
                foreach (var mod in modelUse)
                {
                    if (item.Model.NameTH == mod.NameTH)
                    {
                        if (item.Model.IsSelcted.GetValueOrDefault())
                        {
                            mod.SpecMaterialCollectionID = ID;
                        }
                        else
                        {
                            if (ID == mod.SpecMaterialCollectionID)
                            {
                                mod.SpecMaterialCollectionID = null;
                            }

                        }
                    }
                    models.Add(mod);
                }
            }
            DB.Models.UpdateRange(models);
            await DB.SaveChangesAsync();
            var data = await GetSpecMaterialCollectionAsync(ID);
            return data;
        }

        public async Task DeleteSpecMaterialItemAsync(Guid id)
        {
            var model = await DB.SpecMaterialItems.FindAsync(id);

            if (model is not null)
            {
                model.IsDeleted = true;
                await DB.SaveChangesAsync();
            }

            //     await DB.SpecMaterialItems.Where(o => o.SpecMaterialItemID == id).ExecuteUpdateAsync(c =>
            //        c.SetProperty(col => col.IsDeleted, true)
            //        .SetProperty(col => col.Updated, DateTime.Now)
            //    );
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        public async Task<SpecMaterialCollectionDetailPaging> GetAllSpecMaterialCollectionItemsAsync(SpecMaterialCollectionDetailFilter filter, PageParam pageParam, SpecMaterialCollectionDetailSortByParam sortByParam, CancellationToken cancellationToken = default)
        {
            var query = from item in DB.SpecMaterialItems.AsNoTracking().Include(o => o.UpdatedBy).Include(o => o.Project).ThenInclude(o => o.BG).Include(o => o.SpecMaterialGroup)
                        join smcd in DB.SpecMaterialCollectionDetails.AsNoTracking() on item.ID equals smcd.SpecMaterialItemID
                        join smc in DB.SpecMaterialCollections.AsNoTracking() on smcd.SpecMaterialCollectionID equals smc.ID
                        join mst in DB.MasterCenters.AsNoTracking() on item.SpecMaterialGroupMasterCenterID equals mst.ID

                        select new SpecMaterialCollectionDetailQueryResult
                        {
                            SpecMaterialCollectionDetail = smcd,
                            SpecMaterialCollection = smc,
                            //Model = m,
                            Group = mst,
                            SpecMaterialItem = item
                        };

            if (filter.GroupID != null)
                query = query.Where(o => o.SpecMaterialItem.SpecMaterialGroupMasterCenterID == filter.GroupID);
            if (!string.IsNullOrEmpty(filter.Name))
                query = query.Where(o => o.SpecMaterialItem.Name.Contains(filter.Name));
            if (!string.IsNullOrEmpty(filter.ItemDescription))
                query = query.Where(o => o.SpecMaterialItem.ItemDescription.Contains(filter.ItemDescription));
            if (!string.IsNullOrEmpty(filter.NameEN))
                query = query.Where(o => o.SpecMaterialItem.Name.Contains(filter.NameEN));
            if (!string.IsNullOrEmpty(filter.ItemDescriptionEN))
                query = query.Where(o => o.SpecMaterialItem.ItemDescription.Contains(filter.ItemDescriptionEN));
            if (filter.UpdatedFrom != null)
                query = query.Where(o => o.SpecMaterialItem.Updated >= filter.UpdatedFrom);
            if (filter.UpdatedTo != null)
                query = query.Where(o => o.SpecMaterialItem.Updated <= filter.UpdatedTo);
            if (!string.IsNullOrEmpty(filter.UpdatedBy))
                query = query.Where(o => o.SpecMaterialItem.UpdatedBy.DisplayName == filter.UpdatedBy);
            if (filter.BGNo?.Count > 0)
                query = query.Where(o => filter.BGNo.Contains(o.SpecMaterialItem.Project.BG.BGNo));


            SpecMaterialCollectionDetailDTO.SortBy(sortByParam, ref query);
            var pageOutput = PagingHelper.Paging<SpecMaterialCollectionDetailQueryResult>(pageParam, ref query);

            var queryResults = await query.ToListAsync();
            var result = queryResults.Select(o => SpecMaterialCollectionDetailDTO.CreateFromQueryResult(o, DB)).ToList();

            return new SpecMaterialCollectionDetailPaging()
            {
                PageOutput = pageOutput,
                SpecMaterialCollectionDetail = result
            };

        }


        public async Task<SpecMaterialItemDTO> AddSpecMaterialItemsAsync(SpecMaterialCollectionDTO input)
        {

            var specItem = await DB.SpecMaterialItems.Where(o => o.SpecMaterialGroupMasterCenterID == input.Group.Id).OrderByDescending(o => o.Order).FirstOrDefaultAsync();
            SpecMaterialItem Model = new SpecMaterialItem
            {
                SpecMaterialGroupMasterCenterID = input.Group.Id,
                Name = input.SpecMaterialItem.Name
            };

            if (specItem == null)
            {
                var codeData = 1;
                Model.Code = codeData.ToString();
                Model.Order = 1;
            }
            else
            {
                var codeData = int.Parse(specItem.Code) + 1;
                Model.Code = codeData.ToString();
                Model.Order = specItem.Order + 1;
            }

            Model.ItemDescription = input.SpecMaterialItem.ItemDescription;
            Model.IsActive = true;

            DB.SpecMaterialItems.Add(Model);
            await DB.SaveChangesAsync();
            var result = await GetSpecMaterialItemByIdAsync(Model.ID);
            return result;
        }

        public async Task<SpecMaterialItemDTO> EditSpecMaterialItemsAsync(SpecMaterialCollectionDTO input)
        {

            var model = await DB.SpecMaterialItems.FirstOrDefaultAsync(o => o.ID == input.SpecMaterialItem.Id);
            if (model is not null)
            {
                model.ItemDescription = input.SpecMaterialItem.ItemDescription;
                model.Name = input.SpecMaterialItem.Name;
                model.SpecMaterialGroupMasterCenterID = input.Group.Id;

                DB.SpecMaterialItems.Update(model);
                await DB.SaveChangesAsync();
            }


            // await DB.SpecMaterialItems.Where(o => o.ID == input.SpecMaterialItem.Id).ExecuteUpdateAsync(c =>
            //     c.SetProperty(col => col.ItemDescription, input.SpecMaterialItem.ItemDescription)
            //     .SetProperty(col => col.Name, input.SpecMaterialItem.Name)
            //     .SetProperty(col => col.SpecMaterialGroupMasterCenterID, input.Group.Id)
            //     .SetProperty(col => col.Updated, DateTime.Now)
            // );
            // await DB.SaveChangesAsync();
            var result = await GetSpecMaterialItemByIdAsync((Guid)input.SpecMaterialItem.Id);
            return result;
        }

        public async Task<SpecMaterialItemDTO> GetSpecMaterialItemByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var model = await DB.SpecMaterialItems.Include(o => o.SpecMaterialGroup).Include(o => o.UpdatedBy).FirstOrDefaultAsync(o => o.ID == id, cancellationToken);
            return SpecMaterialItemDTO.CreateFromModel(model);
        }

        public async Task<SpecMaterialCollectionDetailDTO> GetSpecMaterialDetailByItemIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var model = await DB.SpecMaterialCollectionDetails.Include(o => o.SpecMaterialGroup).Include(o => o.UpdatedBy).FirstOrDefaultAsync(o => o.SpecMaterialItemID == id, cancellationToken);
            return SpecMaterialCollectionDetailDTO.CreatedFromModel(model, DB);
        }

        public async Task DeleteSpecMaterialCollectionAsync(Guid id)
        {
            var model = await DB.SpecMaterialCollections.FirstOrDefaultAsync(o => o.ID == id);
            if (model is not null)
            {
                model.IsDeleted = true;
                model.IsActive = false;
                await DB.SaveChangesAsync();
            }

            // await DB.SpecMaterialCollections.Where(o => o.ID == id).ExecuteUpdateAsync(c =>
            //     c.SetProperty(col => col.IsDeleted, true)
            //     .SetProperty(col => col.Updated, DateTime.Now)
            // );
        }
        #region  Excel
        public async Task<FileDTO> ExportTemplateSpecMaterialAsync(Guid ProjectID)
        {
            {
                ExportExcel result = new ExportExcel();

                string path = Path.Combine(FileHelper.GetApplicationRootPath(), "ExcelTemplates", "TemplateSpecMaterial.xlsx");


                var project = await DB.Projects.Where(o => o.ID == ProjectID).Include(o => o.BG).FirstOrDefaultAsync();
                var Detail = await DB.SpecMaterialTemplates.Where(o => o.BG == project.BG.BGNo).OrderBy(o => o.SpecMaterialGroupTH).ThenBy(o => o.SpecMaterialTypeTH).ToListAsync();

                byte[] tmp = await File.ReadAllBytesAsync(path);


                using (MemoryStream stream = new System.IO.MemoryStream(tmp))
                using (ExcelPackage package = new ExcelPackage(stream))
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets.First();
                    int nRow = 2;

                    foreach (var row in Detail)
                    {
                        //var mstGroup = await DB.MasterCenters.Where(o => o.ID == row.SpecMaterialGroupMasterCenterID).FirstOrDefaultAsync();
                        //var Item = await DB.SpecMaterialItems.Where(o => o.ID == row.SpecMaterialItemID).FirstOrDefaultAsync();
                        worksheet.Cells[nRow, 1].Value = "ชุดวัสดุมาตรฐาน";//row.SpecMaterialGroupTH ?? string.Empty;
                        worksheet.Cells[nRow, 2].Value = project.ProjectNo ?? string.Empty;
                        worksheet.Cells[nRow, 3].Value = "All";
                        worksheet.Cells[nRow, 4].Value = row.SpecMaterialGroupTH ?? string.Empty;
                        worksheet.Cells[nRow, 5].Value = row.SpecMaterialTypeTH ?? string.Empty;
                        worksheet.Cells[nRow, 6].Value = row.SpecMaterialTypeEN ?? string.Empty;
                        worksheet.Cells[nRow, 7].Value = row.SpecMaterialDetailTH ?? string.Empty;
                        worksheet.Cells[nRow, 8].Value = row.SpecMaterialDetailEN ?? string.Empty;


                        nRow++;
                    }

                    result.FileContent = package.GetAsByteArray();
                    result.FileName = project.ProjectNo + "_SpecMaterial_Template" + DateTime.Now.ToString("yyyy-MM-dd") + ".xlsx";
                    result.FileType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                }

                Stream fileStream = new MemoryStream(result.FileContent);
                string fileName = result.FileName;
                string contentType = result.FileType;
                string filePath = $"SpecMaterial/";
                var minioBucketName = Environment.GetEnvironmentVariable("minio_TempBucket");
                var uploadResult = await this.FileHelper.UploadFileFromStreamWithOutGuid(fileStream, minioBucketName, filePath, fileName, contentType);

                return new FileDTO()
                {
                    Name = fileName,
                    Url = uploadResult.Url
                };
            }
        }


        public async Task<FileDTO> ExportSpecMaterialDetailAsync(Guid SpecMaterialCollectionID)
        {
            ExportExcel result = new ExportExcel();

            string path = Path.Combine(FileHelper.GetApplicationRootPath(), "ExcelTemplates", "TemplateSpecMaterial.xlsx");

            var Detail = await DB.SpecMaterialCollectionDetails
                .Include(o => o.SpecMaterialCollection)
                .Include(o => o.SpecMaterialGroup)
                .Include(o => o.SpecMaterialItem)
                .Where(o => o.SpecMaterialCollectionID == SpecMaterialCollectionID && o.SpecMaterialItem.IsDeleted == false).OrderBy(o => o.SpecMaterialGroup.Order).ThenBy(o => o.SpecMaterialItem.Name).ToListAsync();
            var project = await DB.Projects.Where(o => o.ID == Detail[0].SpecMaterialCollection.ProjectID).FirstOrDefaultAsync();

            var model = await DB.Models.Where(o => o.SpecMaterialCollectionID == Detail[0].SpecMaterialCollectionID).GroupBy(o => new { o.NameTH }).Select(o => o.FirstOrDefault()).ToListAsync();
            var modelAll = await DB.Models.Where(o => o.ProjectID == project.ID).GroupBy(o => new { o.NameTH }).Select(o => o.FirstOrDefault()).ToListAsync();

            byte[] tmp = await File.ReadAllBytesAsync(path);

            using (MemoryStream stream = new System.IO.MemoryStream(tmp))
            using (ExcelPackage package = new ExcelPackage(stream))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets.First();
                int nRow = 2;

                foreach (var row in Detail)
                {
                    var s = "";
                    var c = 0;
                    var mstGroup = await DB.MasterCenters.Where(o => o.ID == row.SpecMaterialGroupMasterCenterID).FirstOrDefaultAsync();
                    //var Item = await DB.SpecMaterialItems.Where(o => o.ID == row.SpecMaterialItemID).FirstOrDefaultAsync();
                    worksheet.Cells[nRow, 1].Value = row.SpecMaterialCollection.Name ?? string.Empty;
                    worksheet.Cells[nRow, 2].Value = project.ProjectNo ?? string.Empty;
                    if (model.Count == modelAll.Count)
                    {
                        worksheet.Cells[nRow, 3].Value = "All";
                    }
                    else
                    {
                        foreach (var m in model)
                        {

                            if (model.Count > 0)
                            {
                                s = ",";
                            }

                            worksheet.Cells[nRow, 3].Value += m.NameTH;
                            if (c < model.Count - 1)
                            {
                                worksheet.Cells[nRow, 3].Value += s;
                            }
                            c++;
                        }
                    }

                    worksheet.Cells[nRow, 4].Value = mstGroup.Name ?? string.Empty;
                    worksheet.Cells[nRow, 5].Value = row.SpecMaterialItem.Name ?? string.Empty;
                    worksheet.Cells[nRow, 6].Value = row.SpecMaterialItem.NameEN ?? string.Empty;
                    worksheet.Cells[nRow, 7].Value = row.SpecMaterialItem.ItemDescription ?? string.Empty;
                    worksheet.Cells[nRow, 8].Value = row.SpecMaterialItem.ItemDescriptionEN ?? string.Empty;



                    nRow++;
                }

                result.FileContent = package.GetAsByteArray();
                result.FileName = project.ProjectNo + "_SpecMaterial_" + DateTime.Now.ToString("yyyy-MM-dd") + ".xlsx";
                result.FileType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            }

            Stream fileStream = new MemoryStream(result.FileContent);
            string fileName = result.FileName;
            string contentType = result.FileType;
            string filePath = $"SpecMaterial/";
            var minioBucketName = Environment.GetEnvironmentVariable("minio_TempBucket");
            var uploadResult = await this.FileHelper.UploadFileFromStreamWithOutGuid(fileStream, minioBucketName, filePath, fileName, contentType);

            return new FileDTO()
            {
                Name = fileName,
                Url = uploadResult.Url
            };
        }
        public async Task<SpecMaterialExcelDTO> ImportSpecMaterialExcel(Guid projectID, FileDTO input, Guid? userID)
        {
            #region Validate Data

            var result = new SpecMaterialExcelDTO { Success = 0, Error = 0, ErrorMessages = new List<string>() };

            var dt = await ConvertExcelToDataTable(input);
            if (dt.Columns.Count != 9)
            {
                throw new Exception("Invalid File Format");
            }
            var row = 2;
            var error = 0;

            var checkNullProjects = new List<string>();
            var checkProjectNotFounds = new List<string>();
            var checkNullCollectionName = new List<string>();
            var checkDupCollectionName = new List<string>();


            //Read Excel Model

            var projects = await DB.Projects.Where(o => o.ID == projectID).ToListAsync();
            var SpecMaterialExcelModels = new List<SpecMaterialExcelModel>();
            foreach (DataRow r in dt.Rows)
            {
                var isError = false;
                var excelModel = SpecMaterialExcelModel.CreateFromDataRow(r);

                if (
                    string.IsNullOrEmpty(excelModel.CollectionName) &&
                    string.IsNullOrEmpty(excelModel.ProjectNo) &&
                    string.IsNullOrEmpty(excelModel.Model) &&
                    string.IsNullOrEmpty(excelModel.MaterialGroup) &&
                    string.IsNullOrEmpty(excelModel.MaterialItem) &&
                    string.IsNullOrEmpty(excelModel.MaterialItemDescription) &&
                    string.IsNullOrEmpty(excelModel.MaterialItemEN) &&
                    string.IsNullOrEmpty(excelModel.MaterialItemDescriptionEN)
                    )
                {
                    continue;
                }

                if (string.IsNullOrEmpty(excelModel.Model))
                {
                    ValidateException ex = new ValidateException();
                    ex.AddError("1", "โปรดระบุแบบบ้าน", 1);
                    throw ex;
                }


                if (!string.IsNullOrEmpty(excelModel.ProjectNo.Trim().ToLower()))
                {
                    SpecMaterialExcelModels.Add(excelModel);


                    #region Validate                
                    if (string.IsNullOrEmpty(excelModel.ProjectNo.Trim().ToLower()))
                    {
                        ValidateException ex = new ValidateException();
                        ex.AddError("1", "รหัสโครงการไม่ถูกต้อง", 1);
                        throw ex;
                    }
                    else
                    {
                        var prj = await DB.Projects.Where(o => o.ProjectNo.Trim().ToLower() == excelModel.ProjectNo.Trim().ToLower()).FirstOrDefaultAsync();
                        if (prj == null)
                        {
                            ValidateException ex = new ValidateException();
                            ex.AddError("1", "รหัสโครงการไม่ถูกต้อง", 1);
                            throw ex;
                        }
                        else
                        {
                            var checkPrj = await DB.Projects.Where(o => o.ProjectNo.Trim().ToLower() == excelModel.ProjectNo.Trim().ToLower()).FirstOrDefaultAsync();
                            if (prj.ID != projectID)
                            {
                                ValidateException ex = new ValidateException();
                                ex.AddError("1", "รหัสโครงการไม่ถูกต้อง", 1);
                                throw ex;
                            }
                        }
                    }

                    var modelsListr = new List<string>();
                    if (SpecMaterialExcelModels[0].Model.ToLower() == "all")
                    {
                        var modelsLista = await DB.Models.Where(o => o.ProjectID == projectID).GroupBy(o => new { o.NameTH }).ToListAsync();
                        modelsListr = modelsLista.Select(o => o.Key.NameTH).ToList();
                    }
                    else
                    {
                        var modelsLista = await DB.Models.Where(o => o.ProjectID == projectID && o.NameTH.ToLower() == excelModel.Model.ToLower()).GroupBy(o => new { o.NameTH }).Select(o => o.FirstOrDefault()).ToListAsync();
                        modelsListr = modelsLista.Select(o => o.NameTH).ToList();
                    }

                    if (!string.IsNullOrEmpty(excelModel.Model) && !excelModel.Model.ToLower().Equals("all"))
                    {
                        var model = await DB.Models.Where(o => o.ProjectID == projectID && o.NameTH.ToLower() == excelModel.Model.ToLower()).FirstOrDefaultAsync();
                        if (model == null)
                        {
                            ValidateException ex = new ValidateException();
                            ex.AddError("1", "ไม่พบแบบบ้าน " + excelModel.Model, 1);
                            throw ex;
                        }
                    }

                    #endregion

                    if (isError)
                    {
                        error++;
                    }
                }
                else
                {
                    ValidateException ex = new ValidateException();
                    ex.AddError("1", "รหัสโครงการไม่ถูกต้อง", 1);
                    throw ex;
                }

                if (string.IsNullOrEmpty(excelModel.MaterialGroup))
                {
                    ValidateException ex = new ValidateException();
                    ex.AddError("1", "โปรดระบุกลุ่มวัสดุ", 1);
                    throw ex;
                }
                if (string.IsNullOrEmpty(excelModel.MaterialItem))
                {
                    ValidateException ex = new ValidateException();
                    ex.AddError("1", "โปรดระบุประเภทวัสดุ", 1);
                    throw ex;
                }
                if (string.IsNullOrEmpty(excelModel.MaterialItemDescription))
                {
                    ValidateException ex = new ValidateException();
                    ex.AddError("1", "โปรดระบุรายการวัสดุ", 1);
                    throw ex;
                }
                row++;
            }

            if (string.IsNullOrEmpty(SpecMaterialExcelModels[0].CollectionName))
            {
                //checkNullCollectionName.Add((row).ToString());
                //isError = true;

            }
            else
            {
                if (SpecMaterialExcelModels[0].Model.ToLower() == "all")
                {
                    var matcollNames = await DB.SpecMaterialCollections.Where(o => o.ProjectID == projectID).Select(o => o.ID).ToListAsync();
                    var smc = await DB.SpecMaterialCollectionDetails.Where(o => matcollNames.Contains(o.SpecMaterialCollectionID.Value)).ToListAsync();
                    smc.ForEach(o => o.IsActive = false);
                    smc.ForEach(o => o.IsDeleted = true);
                    DB.SpecMaterialCollectionDetails.UpdateRange(smc);
                    await DB.SaveChangesAsync();
                }
                else
                {
                    var groupModelInFile = SpecMaterialExcelModels.GroupBy(o => o.Model.ToLower()).Select(o => o.Key).ToList();
                    var groupModelInDB = await DB.Models.Where(o => o.ProjectID == projectID && groupModelInFile.Contains(o.NameTH.ToLower())).Select(o => o.SpecMaterialCollectionID).ToListAsync();
                    var smc = await DB.SpecMaterialCollectionDetails.Where(o => groupModelInDB.Contains(o.SpecMaterialCollectionID)).ToListAsync();
                    smc.ForEach(o => o.IsActive = false);
                    smc.ForEach(o => o.IsDeleted = true);
                    DB.SpecMaterialCollectionDetails.UpdateRange(smc);
                    await DB.SaveChangesAsync();
                }
            }
            #endregion

            #region RowErrors
            var rowErrors = new List<string>();
            rowErrors.AddRange(checkNullProjects);
            rowErrors.AddRange(checkProjectNotFounds);
            #endregion

            List<SpecMaterialCollection> SpecMaterialCollections = new List<SpecMaterialCollection>();
            List<SpecMaterialItem> SpecMaterialItems = new List<SpecMaterialItem>();
            List<SpecMaterialCollectionDetail> SpecMaterialCollectionDetails = new List<SpecMaterialCollectionDetail>();
            //Update Data
            var rowIntErrors = rowErrors.Distinct().Select(o => Convert.ToInt32(o)).ToList();
            row = 2;
            var createDate = DateTime.Now;
            var groupItem = "";
            var countCollection = 0;
            var matCollectionID = Guid.NewGuid();
            var codeNo = 0;
            var isUpdate = false;
            var delCount = 0;
            var errorList = new List<ImportError>();
            var model1 = "";
            var collectionID = Guid.NewGuid();
            var modelName = "";

            var modelsList = new List<string>();
            var tmpCollectionID = Guid.NewGuid();
            if (SpecMaterialExcelModels[0].Model.ToLower() == "all")
            {
                var modelsLista = await DB.Models.Where(o => o.ProjectID == projectID).GroupBy(o => new { o.NameTH }).ToListAsync();
                modelsList = modelsLista.Select(o => o.Key.NameTH).ToList();
            }
            else
            {
                //modelsList = item.Model.Split(",").ToList();
            }

            if (modelsList.Count > 0)// All
            {
                var SpecMaterialGroupAll = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey.Equals("SpecMaterialGroup")).ToListAsync();
                var SpecMaterialCollectionsAll = await DB.SpecMaterialCollections.Where(o => o.ProjectID == projectID).ToListAsync();
                var SpecMaterialCollectionsDetailAll = await DB.SpecMaterialCollectionDetails.ToListAsync();
                List<SpecMaterialCollection> updateSpecMaterialCollection = new List<SpecMaterialCollection>();

                foreach (var md in modelsList)
                {
                    var loopUdt = false;
                    foreach (var item in SpecMaterialExcelModels)
                    {
                        if (item.Model.ToLower() == "all")
                        {
                            if (md != modelName)
                            {
                                modelName = md;
                                countCollection = 0;
                            }

                            if (!rowIntErrors.Contains(row))
                            {
                                var collectionNewName = item.ProjectNo.Trim() + "-" + md;
                                var masterCenter = SpecMaterialGroupAll.Where(o => o.Name == item.MaterialGroup).FirstOrDefault();
                                isUpdate = SpecMaterialCollectionsAll.Where(o => o.Name == collectionNewName).Any();
                                var checkItem = new SpecMaterialItem();
                                if (isUpdate)
                                {
                                    var name = item.ProjectNo.Trim() + "-" + md;

                                    var matcollName = SpecMaterialCollectionsAll.Where(o => o.Name.Equals(name)).FirstOrDefault();
                                    updateSpecMaterialCollection.Add(matcollName);
                                    collectionID = matcollName.ID;
                                    //DB.SpecMaterialCollections.Update(matcollName);

                                    if (masterCenter != null)
                                    {
                                        checkItem = await DB.SpecMaterialItems
                                            .Where(o => o.ProjectID == projectID &&
                                                        o.SpecMaterialGroupMasterCenterID == masterCenter.ID &&
                                                        o.Name == item.MaterialItem &&
                                                        o.ItemDescription == item.MaterialItemDescription
                                                        ).FirstOrDefaultAsync();

                                    }
                                    else
                                    {
                                        checkItem = null;
                                    }

                                    var specMatDetail = new SpecMaterialCollectionDetail();
                                    var specMatItemd = new SpecMaterialItem();
                                    if (!groupItem.Equals(item.MaterialGroup))
                                    {
                                        if (masterCenter != null)
                                        {
                                            specMatItemd.SpecMaterialGroupMasterCenterID = masterCenter.ID;
                                            if (!groupItem.Equals(item.MaterialItem))
                                            {
                                                var project = await DB.Projects.Where(o => o.ProjectNo.Trim() == item.ProjectNo.Trim()).FirstOrDefaultAsync();
                                                var specMateItem = await DB.SpecMaterialItems.Where(o => o.ProjectID == project.ID && o.Name.Contains(item.MaterialItem)).AnyAsync();
                                                if (!specMateItem)
                                                {
                                                    var errors = new ImportError();
                                                    errors.ProjectID = projectID;
                                                    errors.SpecMaterialCollectionName = item.ProjectNo.Trim() + "-" + md;
                                                    errors.SpecMaterialGroup = item.MaterialGroup;
                                                    errors.SpecMaterialType = item.MaterialItem;
                                                    errors.SpecMaterialItem = item.MaterialItemDescription;
                                                    errors.SpecMaterialTypeEN = item.MaterialItemEN;
                                                    errors.SpecMaterialItemEN = item.MaterialItemDescriptionEN;
                                                    errors.ErrorDescription = "ไม่พบข้อมูลประเภทวัสดุ";
                                                    errors.ModelName = md;
                                                    errorList.Add(errors);

                                                    error++;
                                                    continue;
                                                }
                                            }
                                        }
                                        else
                                        {
                                            if (!groupItem.Equals(item.MaterialItem))
                                            {
                                                var project = await DB.Projects.Where(o => o.ProjectNo.Trim() == item.ProjectNo.Trim()).FirstOrDefaultAsync();
                                                var specMateItem = await DB.SpecMaterialItems.Where(o => o.ProjectID == project.ID && o.Name.Contains(item.MaterialItem)).AnyAsync();
                                                if (!specMateItem)
                                                {
                                                    var errors = new ImportError();
                                                    errors.ProjectID = projectID;
                                                    errors.SpecMaterialCollectionName = item.ProjectNo.Trim() + "-" + md;
                                                    errors.SpecMaterialGroup = item.MaterialGroup;
                                                    errors.SpecMaterialType = item.MaterialItem;
                                                    errors.SpecMaterialItem = item.MaterialItemDescription;
                                                    errors.SpecMaterialTypeEN = item.MaterialItemEN;
                                                    errors.SpecMaterialItemEN = item.MaterialItemDescriptionEN;
                                                    errors.ErrorDescription = "ไม่พบข้อมูลกลุ่มวัสดุ ไม่พบข้อมูลประเภทวัสดุ";
                                                    errors.ModelName = md;
                                                    errorList.Add(errors);

                                                    error++;

                                                    continue;
                                                }
                                                else
                                                {
                                                    var errors = new ImportError();
                                                    errors.ProjectID = projectID;
                                                    errors.SpecMaterialCollectionName = item.ProjectNo.Trim() + "-" + md;
                                                    errors.SpecMaterialGroup = item.MaterialGroup;
                                                    errors.SpecMaterialType = item.MaterialItem;
                                                    errors.SpecMaterialItem = item.MaterialItemDescription;
                                                    errors.SpecMaterialTypeEN = item.MaterialItemEN;
                                                    errors.SpecMaterialItemEN = item.MaterialItemDescriptionEN;
                                                    errors.ModelName = md;
                                                    errors.ErrorDescription = "ไม่พบข้อมูลกลุ่มวัสดุ";
                                                    errorList.Add(errors);

                                                    error++;

                                                    continue;
                                                }
                                            }
                                        }
                                    }

                                    if (!groupItem.Equals(item.MaterialGroup))
                                    {
                                        if (masterCenter != null)
                                        {
                                            specMatItemd.SpecMaterialGroupMasterCenterID = masterCenter.ID;
                                            if (!groupItem.Equals(item.MaterialItem))
                                            {
                                                var project = await DB.Projects.Where(o => o.ProjectNo.Trim() == item.ProjectNo.Trim()).FirstOrDefaultAsync();
                                                var specMateItem = await DB.SpecMaterialItems.Where(o => o.ProjectID == project.ID && o.Name.Contains(item.MaterialItem)).AnyAsync();
                                                if (!specMateItem)
                                                {
                                                    var errors = new ImportError();
                                                    errors.ProjectID = projectID;
                                                    errors.SpecMaterialCollectionName = item.ProjectNo.Trim() + "-" + md;
                                                    errors.SpecMaterialGroup = item.MaterialGroup;
                                                    errors.SpecMaterialType = item.MaterialItem;
                                                    errors.SpecMaterialItem = item.MaterialItemDescription;
                                                    errors.SpecMaterialTypeEN = item.MaterialItemEN;
                                                    errors.SpecMaterialItemEN = item.MaterialItemDescriptionEN;
                                                    errors.ErrorDescription = "ไม่พบข้อมูลประเภทวัสดุ";
                                                    errors.ModelName = md;
                                                    errorList.Add(errors);

                                                    error++;
                                                    continue;
                                                }
                                            }
                                        }
                                        else
                                        {
                                            if (!groupItem.Equals(item.MaterialItem))
                                            {
                                                var project = await DB.Projects.Where(o => o.ProjectNo.Trim() == item.ProjectNo.Trim()).FirstOrDefaultAsync();
                                                var specMateItem = await DB.SpecMaterialItems.Where(o => o.ProjectID == project.ID && o.Name.Contains(item.MaterialItem)).AnyAsync();
                                                if (!specMateItem)
                                                {
                                                    var errors = new ImportError();
                                                    errors.ProjectID = projectID;
                                                    errors.SpecMaterialCollectionName = item.ProjectNo.Trim() + "-" + md;
                                                    errors.SpecMaterialGroup = item.MaterialGroup;
                                                    errors.SpecMaterialType = item.MaterialItem;
                                                    errors.SpecMaterialItem = item.MaterialItemDescription;
                                                    errors.SpecMaterialTypeEN = item.MaterialItemEN;
                                                    errors.SpecMaterialItemEN = item.MaterialItemDescriptionEN;
                                                    errors.ErrorDescription = "ไม่พบข้อมูลกลุ่มวัสดุ ไม่พบข้อมูลประเภทวัสดุ";
                                                    errors.ModelName = md;
                                                    errorList.Add(errors);

                                                    error++;

                                                    continue;
                                                }
                                                else
                                                {
                                                    var errors = new ImportError();
                                                    errors.ProjectID = projectID;
                                                    errors.SpecMaterialCollectionName = item.ProjectNo.Trim() + "-" + md;
                                                    errors.SpecMaterialGroup = item.MaterialGroup;
                                                    errors.SpecMaterialType = item.MaterialItem;
                                                    errors.SpecMaterialItem = item.MaterialItemDescription;
                                                    errors.SpecMaterialTypeEN = item.MaterialItemEN;
                                                    errors.SpecMaterialItemEN = item.MaterialItemDescriptionEN;
                                                    errors.ErrorDescription = "ไม่พบข้อมูลกลุ่มวัสดุ";
                                                    errors.ModelName = md;
                                                    errorList.Add(errors);

                                                    error++;

                                                    continue;
                                                }
                                            }
                                        }
                                    }


                                    if (checkItem != null)
                                    {
                                        //SpecMaterialItems.Add(checkItem);
                                        specMatDetail.SpecMaterialItemID = checkItem.ID;
                                        specMatDetail.SpecMaterialCollectionID = collectionID;
                                        specMatDetail.SpecMaterialGroupMasterCenterID = masterCenter.ID;
                                        specMatDetail.IsActive = true;
                                        specMatDetail.Updated = DateTime.Now;
                                        SpecMaterialCollectionDetails.Add(specMatDetail);
                                    }
                                    else
                                    {
                                        var code = await DB.SpecMaterialItems.Where(o => o.ProjectID == projectID && o.SpecMaterialGroupMasterCenterID == masterCenter.ID).OrderByDescending(o => o.Code).FirstOrDefaultAsync();
                                        specMatItemd.SpecMaterialGroupMasterCenterID = masterCenter.ID;
                                        specMatItemd.Name = item.MaterialItem;
                                        specMatItemd.ItemDescription = item.MaterialItemDescription;
                                        if (code == null)
                                        {
                                            var c = 0;
                                            specMatItemd.Code = c.ToString();
                                            specMatItemd.Order = 0;
                                        }
                                        else
                                        {
                                            int c = int.Parse(code.Code) + 1;
                                            specMatItemd.Code = c.ToString();
                                            specMatItemd.Order = c;
                                        }

                                        specMatItemd.IsActive = true;
                                        specMatItemd.NameEN = item.MaterialItemEN;
                                        specMatItemd.ItemDescriptionEN = item.MaterialItemDescriptionEN;
                                        specMatItemd.ProjectID = projectID;
                                        specMatItemd.Updated = DateTime.Now;
                                        DB.SpecMaterialItems.Add(specMatItemd);

                                        specMatDetail.SpecMaterialItemID = specMatItemd.ID;
                                        specMatDetail.SpecMaterialCollectionID = collectionID;
                                        specMatDetail.SpecMaterialGroupMasterCenterID = masterCenter.ID;
                                        specMatDetail.IsActive = true;
                                        specMatDetail.Updated = DateTime.Now;
                                        SpecMaterialCollectionDetails.Add(specMatDetail);
                                    }




                                }
                                else
                                {
                                    //เพิ่ม SpecMaterialCollection
                                    if (countCollection == 0)
                                    {
                                        var matColl = new SpecMaterialCollection();
                                        matColl.ID = Guid.NewGuid();
                                        matColl.Name = item.ProjectNo.Trim() + "-" + md;
                                        matColl.ProjectID = projectID;
                                        matColl.IsActive = true;
                                        matCollectionID = matColl.ID;
                                        SpecMaterialCollections.Add(matColl);
                                        countCollection++;
                                    }

                                    //เพิ่ม SpecMaterialItem
                                    var specMatItem = new SpecMaterialItem();

                                    if (!groupItem.Equals(item.MaterialGroup) && masterCenter == null)
                                    {
                                        if (masterCenter != null)
                                        {
                                            specMatItem.SpecMaterialGroupMasterCenterID = masterCenter.ID;
                                            if (!groupItem.Equals(item.MaterialItem))
                                            {
                                                var project = await DB.Projects.Where(o => o.ProjectNo.Trim() == item.ProjectNo.Trim()).FirstOrDefaultAsync();
                                                var specMateItem = await DB.SpecMaterialItems.Where(o => o.ProjectID == project.ID && o.Name.Contains(item.MaterialItem)).AnyAsync();
                                                if (!specMateItem)
                                                {
                                                    var errors = new ImportError();
                                                    errors.ProjectID = projectID;
                                                    errors.SpecMaterialCollectionName = item.ProjectNo.Trim() + "-" + md;
                                                    errors.SpecMaterialGroup = item.MaterialGroup;
                                                    errors.SpecMaterialType = item.MaterialItem;
                                                    errors.SpecMaterialItem = item.MaterialItemDescription;
                                                    errors.ErrorDescription = "ไม่พบข้อมูลประเภทวัสดุ";
                                                    errors.SpecMaterialTypeEN = item.MaterialItemEN;
                                                    errors.SpecMaterialItemEN = item.MaterialItemDescriptionEN;
                                                    errors.ModelName = md;
                                                    errorList.Add(errors);

                                                    error++;
                                                    continue;
                                                }
                                            }
                                        }
                                        else
                                        {
                                            if (!groupItem.Equals(item.MaterialItem))
                                            {
                                                var project = await DB.Projects.Where(o => o.ProjectNo.Trim() == item.ProjectNo.Trim()).FirstOrDefaultAsync();
                                                var specMateItem = await DB.SpecMaterialItems.Where(o => o.ProjectID == project.ID && o.Name.Contains(item.MaterialItem)).AnyAsync();
                                                if (!specMateItem)
                                                {
                                                    var errors = new ImportError();
                                                    errors.ProjectID = projectID;
                                                    errors.SpecMaterialCollectionName = item.ProjectNo.Trim() + "-" + md;
                                                    errors.SpecMaterialGroup = item.MaterialGroup;
                                                    errors.SpecMaterialType = item.MaterialItem;
                                                    errors.SpecMaterialItem = item.MaterialItemDescription;
                                                    errors.ErrorDescription = "ไม่พบข้อมูลกลุ่มวัสดุ ไม่พบข้อมูลประเภทวัสดุ";
                                                    errors.SpecMaterialTypeEN = item.MaterialItemEN;
                                                    errors.SpecMaterialItemEN = item.MaterialItemDescriptionEN;
                                                    errors.ModelName = md;
                                                    errorList.Add(errors);

                                                    error++;

                                                    continue;
                                                }
                                                else
                                                {
                                                    var errors = new ImportError();
                                                    errors.ProjectID = projectID;
                                                    errors.SpecMaterialCollectionName = item.ProjectNo.Trim() + "-" + md;
                                                    errors.SpecMaterialGroup = item.MaterialGroup;
                                                    errors.SpecMaterialType = item.MaterialItem;
                                                    errors.SpecMaterialItem = item.MaterialItemDescription;
                                                    errors.SpecMaterialTypeEN = item.MaterialItemEN;
                                                    errors.SpecMaterialItemEN = item.MaterialItemDescriptionEN;
                                                    errors.ModelName = md;
                                                    errors.ErrorDescription = "ไม่พบข้อมูลกลุ่มวัสดุ";
                                                    errorList.Add(errors);

                                                    error++;

                                                    continue;
                                                }
                                            }
                                        }
                                    }



                                    var specItem = await DB.SpecMaterialItems.Where(o => o.SpecMaterialGroupMasterCenterID == specMatItem.SpecMaterialGroupMasterCenterID && o.ProjectID == projectID).OrderByDescending(o => o.Order).FirstOrDefaultAsync();
                                    if (specItem == null)
                                    {
                                        codeNo = SpecMaterialItems.Where(o => o.SpecMaterialGroupMasterCenterID == specMatItem.SpecMaterialGroupMasterCenterID && o.ProjectID == projectID).OrderByDescending(o => o.Order).Select(o => o.Order.GetValueOrDefault()).FirstOrDefault();
                                        codeNo++;
                                        specMatItem.Code = codeNo.ToString();
                                        specMatItem.Order = codeNo;
                                    }
                                    else
                                    {
                                        codeNo = SpecMaterialItems.Where(o => o.SpecMaterialGroupMasterCenterID == specMatItem.SpecMaterialGroupMasterCenterID && o.ProjectID == projectID).OrderByDescending(o => o.Order).Select(o => o.Order.GetValueOrDefault()).FirstOrDefault();
                                        codeNo++;
                                        specMatItem.Code = codeNo.ToString();
                                        specMatItem.Order = codeNo;
                                    }
                                    specMatItem.Name = item.MaterialItem;
                                    specMatItem.ItemDescription = item.MaterialItemDescription;
                                    specMatItem.NameEN = item.MaterialItemEN;
                                    specMatItem.ItemDescriptionEN = item.MaterialItemDescriptionEN;
                                    specMatItem.IsActive = true;
                                    specMatItem.ProjectID = projectID;
                                    specMatItem.SpecMaterialGroupMasterCenterID = masterCenter.ID;
                                    SpecMaterialItems.Add(specMatItem);

                                    //เพิ่ม SpecMaterialCollectionDetail
                                    var matCollDetail = new SpecMaterialCollectionDetail();
                                    if (masterCenter != null)
                                    {
                                        matCollDetail.SpecMaterialGroupMasterCenterID = masterCenter.ID;
                                    }
                                    else
                                    {
                                        error++;
                                    }
                                    matCollDetail.SpecMaterialItemID = specMatItem.ID;
                                    matCollDetail.SpecMaterialCollectionID = matCollectionID;
                                    matCollDetail.IsActive = true;
                                    SpecMaterialCollectionDetails.Add(matCollDetail);
                                    result.Success++;
                                    var model = await DB.Models.Where(o => o.NameTH == md && o.ProjectID == projectID).ToListAsync();
                                    foreach (var mm in model)
                                    {
                                        mm.SpecMaterialCollectionID = matCollectionID;
                                        DB.Models.Update(mm);
                                    }
                                }

                            }
                            row++;
                        }
                        else
                        {
                            if (item.Model.ToLower() == md.ToLower())
                            {
                                if (!rowIntErrors.Contains(row))
                                {
                                    var collectionNewName = $"{item.ProjectNo.Trim()}-{item.Model}";
                                    var masterCenter = SpecMaterialGroupAll.Where(o => o.Name == item.MaterialGroup).FirstOrDefault();
                                    isUpdate = SpecMaterialCollectionsAll.Where(o => o.Name.ToLower().Equals(collectionNewName.ToLower())).Any();
                                    var checkItem = new SpecMaterialItem();
                                    if (isUpdate)
                                    {



                                        var name = item.ProjectNo.Trim() + "-" + item.Model;

                                        var matcollName = SpecMaterialCollectionsAll.Where(o => o.Name.ToLower().Equals(name.ToLower())).FirstOrDefault();
                                        collectionID = matcollName.ID;

                                        if (!collectionID.Equals(tmpCollectionID))
                                        {

                                            //var specMattDetaila = SpecMaterialCollectionsDetailAll.Where(o => o.SpecMaterialCollectionID == Guid.Parse("fe96a8d7-8348-4789-b802-d114e189d626")).ToList();
                                            //specMattDetaila.ForEach(o => o.IsActive = false);
                                            //specMattDetaila.ForEach(o => o.IsDeleted = true);
                                            //SpecMaterialCollectionsDetailAll.UpdateRange(specMattDetaila);

                                            tmpCollectionID = collectionID;
                                            var a = SpecMaterialCollectionDetails.Where(o => collectionID.Equals(o.SpecMaterialCollectionID.Value)).ToList();
                                            a.ForEach(o => o.IsActive = false);
                                            a.ForEach(o => o.IsDeleted = true);
                                            loopUdt = true;
                                        }

                                        if (masterCenter != null)
                                        {
                                            checkItem = SpecMaterialItems
                                                .Where(o => o.ProjectID == projectID &&
                                                            o.SpecMaterialGroupMasterCenterID == masterCenter.ID &&
                                                            o.Name == item.MaterialItem &&
                                                            o.ItemDescription == item.MaterialItemDescription
                                                            ).FirstOrDefault();

                                        }
                                        else
                                        {
                                            checkItem = null;
                                        }


                                        if (checkItem != null)
                                        {
                                            SpecMaterialCollectionDetail newDetail = new SpecMaterialCollectionDetail();
                                            newDetail.SpecMaterialGroupMasterCenterID = masterCenter.ID;
                                            newDetail.SpecMaterialItemID = checkItem.ID;
                                            newDetail.SpecMaterialCollectionID = collectionID;
                                            newDetail.IsActive = true;
                                            newDetail.Updated = DateTime.Now;
                                            SpecMaterialCollectionDetails.Add(newDetail);
                                        }
                                        else
                                        {
                                            SpecMaterialItem newItem = new SpecMaterialItem();
                                            newItem.Name = item.MaterialItem;
                                            newItem.ItemDescription = item.MaterialItemDescription;
                                            newItem.NameEN = item.MaterialItemEN;
                                            newItem.ItemDescriptionEN = item.MaterialItemDescriptionEN;
                                            newItem.IsActive = true;
                                            newItem.ProjectID = projectID;
                                            newItem.SpecMaterialGroupMasterCenterID = masterCenter.ID;
                                            newItem.Updated = DateTime.Now;
                                            SpecMaterialItems.Add(newItem);

                                            SpecMaterialCollectionDetail newDetail = new SpecMaterialCollectionDetail();
                                            newDetail.SpecMaterialGroupMasterCenterID = masterCenter.ID;
                                            newDetail.SpecMaterialItemID = newItem.ID;
                                            newDetail.SpecMaterialCollectionID = collectionID;
                                            newDetail.IsActive = true;
                                            newDetail.Updated = DateTime.Now;
                                            SpecMaterialCollectionDetails.Add(newDetail);

                                            //var ssd = SpecMaterialCollectionDetails.Where(o => o.SpecMaterialCollectionID == collectionID).ToList();
                                            //var code = SpecMaterialItems.Where(o => o.ID == ssd[loopUdt].SpecMaterialItemID).FirstOrDefault();
                                            //code.Name = item.MaterialItem;
                                            //code.ItemDescription = item.MaterialItemDescription;
                                            //code.NameEN = item.MaterialItemEN;
                                            //code.ItemDescriptionEN = item.MaterialItemDescriptionEN;
                                            //loopUdt++;
                                        }
                                    }
                                    else
                                    {
                                        var aa = SpecMaterialCollections.Where(o => o.Name.ToLower().Equals(collectionNewName.ToLower())).FirstOrDefault();

                                        collectionID = aa.ID;

                                        if (!collectionID.Equals(tmpCollectionID))
                                        {

                                            //var specMattDetaila = SpecMaterialCollectionsDetailAll.Where(o => o.SpecMaterialCollectionID == Guid.Parse("fe96a8d7-8348-4789-b802-d114e189d626")).ToList();
                                            //specMattDetaila.ForEach(o => o.IsActive = false);
                                            //specMattDetaila.ForEach(o => o.IsDeleted = true);
                                            //SpecMaterialCollectionsDetailAll.UpdateRange(specMattDetaila);

                                            tmpCollectionID = collectionID;
                                            var a = SpecMaterialCollectionDetails.Where(o => collectionID.Equals(o.SpecMaterialCollectionID.Value)).ToList();
                                            a.ForEach(o => o.IsActive = false);
                                            a.ForEach(o => o.IsDeleted = true);
                                            loopUdt = true;
                                        }

                                        if (masterCenter != null)
                                        {
                                            checkItem = SpecMaterialItems
                                                .Where(o => o.ProjectID == projectID &&
                                                            o.SpecMaterialGroupMasterCenterID == masterCenter.ID &&
                                                            o.Name == item.MaterialItem &&
                                                            o.ItemDescription == item.MaterialItemDescription
                                                            ).FirstOrDefault();

                                        }
                                        else
                                        {
                                            checkItem = null;
                                        }


                                        if (checkItem != null)
                                        {
                                            SpecMaterialCollectionDetail newDetail = new SpecMaterialCollectionDetail();
                                            newDetail.SpecMaterialGroupMasterCenterID = masterCenter.ID;
                                            newDetail.SpecMaterialItemID = checkItem.ID;
                                            newDetail.SpecMaterialCollectionID = collectionID;
                                            newDetail.IsActive = true;
                                            newDetail.Updated = DateTime.Now;
                                            SpecMaterialCollectionDetails.Add(newDetail);
                                        }
                                        else
                                        {
                                            SpecMaterialItem newItem = new SpecMaterialItem();
                                            newItem.Name = item.MaterialItem;
                                            newItem.ItemDescription = item.MaterialItemDescription;
                                            newItem.NameEN = item.MaterialItemEN;
                                            newItem.ItemDescriptionEN = item.MaterialItemDescriptionEN;
                                            newItem.IsActive = true;
                                            newItem.ProjectID = projectID;
                                            newItem.SpecMaterialGroupMasterCenterID = masterCenter.ID;
                                            newItem.Updated = DateTime.Now;
                                            SpecMaterialItems.Add(newItem);

                                            SpecMaterialCollectionDetail newDetail = new SpecMaterialCollectionDetail();
                                            newDetail.SpecMaterialGroupMasterCenterID = masterCenter.ID;
                                            newDetail.SpecMaterialItemID = newItem.ID;
                                            newDetail.SpecMaterialCollectionID = collectionID;
                                            newDetail.IsActive = true;
                                            newDetail.Updated = DateTime.Now;
                                            SpecMaterialCollectionDetails.Add(newDetail);

                                            //var ssd = SpecMaterialCollectionDetails.Where(o => o.SpecMaterialCollectionID == collectionID).ToList();
                                            //var code = SpecMaterialItems.Where(o => o.ID == ssd[loopUdt].SpecMaterialItemID).FirstOrDefault();
                                            //code.Name = item.MaterialItem;
                                            //code.ItemDescription = item.MaterialItemDescription;
                                            //code.NameEN = item.MaterialItemEN;
                                            //code.ItemDescriptionEN = item.MaterialItemDescriptionEN;
                                            //loopUdt++;
                                        }
                                    }
                                }

                            }
                        }
                    }
                }



                if (updateSpecMaterialCollection.Any())
                {
                    DB.SpecMaterialCollections.UpdateRange(updateSpecMaterialCollection);
                }
            }
            else // By sheet
            {
                var SpecMaterialGroupAll = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey.Equals("SpecMaterialGroup")).ToListAsync();
                var SpecMaterialCollectionsAll = await DB.SpecMaterialCollections.Where(o => o.ProjectID == projectID).ToListAsync();
                List<SpecMaterialCollection> updateSpecMaterialCollection = new List<SpecMaterialCollection>();
                foreach (var item in SpecMaterialExcelModels)
                {
                    if (!rowIntErrors.Contains(row))
                    {
                        //var model = await DB.Models.Where(o => o.NameTH.ToLower() == item.Model.ToLower() && o.ProjectID == projectID).FirstOrDefaultAsync();
                        var collectionNewName = item.ProjectNo.Trim() + "-" + item.Model;
                        var masterCenter = SpecMaterialGroupAll.Where(o => o.MasterCenterGroupKey == "SpecMaterialGroup" && o.Name == item.MaterialGroup).FirstOrDefault();
                        isUpdate = SpecMaterialCollectionsAll.Where(o => o.Name.ToLower() == collectionNewName.ToLower()).Any();
                        var checkItem = new SpecMaterialItem();
                        if (isUpdate)
                        {
                            var name = item.ProjectNo.Trim() + "-" + item.Model;

                            var matcollName = SpecMaterialCollectionsAll.Where(o => o.Name.ToLower().Equals(name.ToLower())).FirstOrDefault();
                            updateSpecMaterialCollection.Add(matcollName);
                            collectionID = matcollName.ID;
                            DB.SpecMaterialCollections.Update(matcollName);

                            if (masterCenter != null)
                            {
                                checkItem = await DB.SpecMaterialItems
                                                    .Where(o => o.ProjectID == projectID &&
                                                                o.SpecMaterialGroupMasterCenterID == masterCenter.ID &&
                                                                o.Name == item.MaterialItem &&
                                                                o.ItemDescription == item.MaterialItemDescription
                                                                ).FirstOrDefaultAsync();
                            }
                            else
                            {
                                checkItem = null;
                            }


                            if (!groupItem.Equals(item.MaterialGroup) && masterCenter == null)
                            {
                                if (masterCenter != null)
                                {
                                    //specMatItem.SpecMaterialGroupMasterCenterID = masterCenter.ID;
                                    if (!groupItem.Equals(item.MaterialItem))
                                    {
                                        var project = await DB.Projects.Where(o => o.ProjectNo == item.ProjectNo).FirstOrDefaultAsync();
                                        var specMateItem = await DB.SpecMaterialItems.Where(o => o.ProjectID == project.ID && o.Name.Equals(item.MaterialItem)).AnyAsync();
                                        if (!specMateItem)
                                        {
                                            var errors = new ImportError();
                                            errors.ProjectID = projectID;
                                            errors.SpecMaterialCollectionName = item.ProjectNo + "-" + item.Model;
                                            errors.SpecMaterialGroup = item.MaterialGroup;
                                            errors.SpecMaterialType = item.MaterialItem;
                                            errors.SpecMaterialItem = item.MaterialItemDescription;
                                            errors.ErrorDescription = "ไม่พบข้อมูลประเภทวัสดุ";
                                            errors.SpecMaterialTypeEN = item.MaterialItemEN;
                                            errors.SpecMaterialItemEN = item.MaterialItemDescriptionEN;
                                            errors.ModelName = item.Model;
                                            errorList.Add(errors);

                                            error++;
                                            continue;
                                        }
                                    }
                                }
                                else
                                {
                                    if (!groupItem.Equals(item.MaterialItem))
                                    {
                                        var project = await DB.Projects.Where(o => o.ProjectNo == item.ProjectNo).FirstOrDefaultAsync();
                                        var specMateItem = await DB.SpecMaterialItems.Where(o => o.ProjectID == project.ID && o.Name.Equals(item.MaterialItem)).AnyAsync();
                                        if (!specMateItem)
                                        {
                                            var errors = new ImportError();
                                            errors.ProjectID = projectID;
                                            errors.SpecMaterialCollectionName = item.ProjectNo + "-" + item.Model;
                                            errors.SpecMaterialGroup = item.MaterialGroup;
                                            errors.SpecMaterialType = item.MaterialItem;
                                            errors.SpecMaterialItem = item.MaterialItemDescription;
                                            errors.ErrorDescription = "ไม่พบข้อมูลกลุ่มวัสดุ ไม่พบข้อมูลประเภทวัสดุ";
                                            errors.SpecMaterialTypeEN = item.MaterialItemEN;
                                            errors.SpecMaterialItemEN = item.MaterialItemDescriptionEN;
                                            errors.ModelName = item.Model;
                                            errorList.Add(errors);

                                            error++;

                                            continue;
                                        }
                                        else
                                        {
                                            var errors = new ImportError();
                                            errors.ProjectID = projectID;
                                            errors.SpecMaterialCollectionName = item.ProjectNo + "-" + item.Model;
                                            errors.SpecMaterialGroup = item.MaterialGroup;
                                            errors.SpecMaterialType = item.MaterialItem;
                                            errors.SpecMaterialItem = item.MaterialItemDescription;
                                            errors.SpecMaterialTypeEN = item.MaterialItemEN;
                                            errors.SpecMaterialItemEN = item.MaterialItemDescriptionEN;
                                            errors.ModelName = item.Model;
                                            errors.ErrorDescription = "ไม่พบข้อมูลกลุ่มวัสดุ";
                                            errorList.Add(errors);

                                            error++;

                                            continue;
                                        }
                                    }
                                }
                            }
                            else
                            {

                                var specMateItem = await DB.SpecMaterialItems.Where(o => o.ProjectID == projectID && o.Name.Equals(item.MaterialItem)).AnyAsync();
                                if (!specMateItem)
                                {
                                    var errors = new ImportError();
                                    errors.ProjectID = projectID;
                                    errors.SpecMaterialCollectionName = item.ProjectNo + "-" + item.Model;
                                    errors.SpecMaterialGroup = item.MaterialGroup;
                                    errors.SpecMaterialType = item.MaterialItem;
                                    errors.SpecMaterialItem = item.MaterialItemDescription;
                                    errors.SpecMaterialTypeEN = item.MaterialItemEN;
                                    errors.SpecMaterialItemEN = item.MaterialItemDescriptionEN;
                                    errors.ModelName = item.Model;
                                    errors.ErrorDescription = "ไม่พบข้อมูลประเภทวัสดุ";
                                    errorList.Add(errors);

                                    error++;

                                    continue;
                                }
                            }


                            var specMatDetail = new SpecMaterialCollectionDetail();
                            var specMatItemd = new SpecMaterialItem();
                            if (checkItem != null)
                            {
                                specMatDetail.SpecMaterialItemID = checkItem.ID;
                                specMatDetail.SpecMaterialCollectionID = collectionID;
                                specMatDetail.SpecMaterialGroupMasterCenterID = masterCenter.ID;
                                specMatDetail.IsActive = true;
                                DB.SpecMaterialCollectionDetails.Add(specMatDetail);

                                if (!checkItem.Equals(item.MaterialItem))
                                {
                                    checkItem.Name = item.MaterialItem;
                                    checkItem.Updated = DateTime.Now;
                                    DB.SpecMaterialItems.Update(checkItem);
                                }
                                if (!checkItem.NameEN.Equals(item.MaterialItemEN))
                                {
                                    checkItem.NameEN = item.MaterialItemEN;
                                    checkItem.Updated = DateTime.Now;
                                    DB.SpecMaterialItems.Update(checkItem);
                                }
                                if (!checkItem.ItemDescription.Equals(item.MaterialItemDescription))
                                {
                                    checkItem.ItemDescription = item.MaterialItemDescription;
                                    checkItem.Updated = DateTime.Now;
                                    DB.SpecMaterialItems.Update(checkItem);
                                }
                                if (!checkItem.ItemDescriptionEN.Equals(item.MaterialItemDescriptionEN))
                                {
                                    checkItem.ItemDescriptionEN = item.MaterialItemDescriptionEN;
                                    checkItem.Updated = DateTime.Now;
                                    DB.SpecMaterialItems.Update(checkItem);
                                }

                            }
                            else
                            {

                                var code = await DB.SpecMaterialItems.Where(o => o.ProjectID == projectID && o.SpecMaterialGroupMasterCenterID == masterCenter.ID).OrderByDescending(o => o.Code).FirstOrDefaultAsync();
                                specMatItemd.SpecMaterialGroupMasterCenterID = masterCenter.ID;
                                specMatItemd.Name = item.MaterialItem;
                                specMatItemd.ItemDescription = item.MaterialItemDescription;
                                if (code == null)
                                {
                                    var c = 0;
                                    specMatItemd.Code = c.ToString();
                                    specMatItemd.Order = 0;
                                }
                                else
                                {
                                    int c = int.Parse(code.Code) + 1;
                                    specMatItemd.Code = c.ToString();
                                    specMatItemd.Order = c;
                                }

                                specMatItemd.IsActive = true;
                                specMatItemd.NameEN = item.MaterialItemEN;
                                specMatItemd.ItemDescriptionEN = item.MaterialItemDescriptionEN;
                                specMatItemd.ProjectID = projectID;
                                specMatItemd.Updated = DateTime.Now;
                                DB.SpecMaterialItems.Add(specMatItemd);

                                specMatDetail.SpecMaterialItemID = specMatItemd.ID;
                                specMatDetail.SpecMaterialCollectionID = collectionID;
                                specMatDetail.SpecMaterialGroupMasterCenterID = masterCenter.ID;
                                specMatDetail.IsActive = true;
                                specMatDetail.Updated = DateTime.Now;
                                SpecMaterialCollectionDetails.Add(specMatDetail);
                            }

                        }
                        else
                        {

                            if (item.Model.ToLower() != modelName.ToLower())
                            {
                                modelName = item.Model.ToLower();
                                countCollection = 0;
                            }
                            //เพิ่ม SpecMaterialCollection
                            if (countCollection == 0)
                            {
                                var matColl = new SpecMaterialCollection();
                                var mName = await DB.Models.Where(o => o.ProjectID == projectID && o.NameTH.ToLower() == item.Model.ToLower()).GroupBy(o => new { o.NameTH }).Select(o => o.FirstOrDefault()).FirstOrDefaultAsync();
                                matColl.ID = Guid.NewGuid();
                                matColl.Name = item.ProjectNo.Trim() + "-" + mName.NameTH;    //item.Model;
                                matColl.ProjectID = projectID;
                                matColl.IsActive = true;

                                matCollectionID = matColl.ID;
                                SpecMaterialCollections.Add(matColl);
                                countCollection++;
                            }

                            //เพิ่ม SpecMaterialItem
                            var specMatItem = new SpecMaterialItem();

                            var specMatttItem = await DB.SpecMaterialCollectionDetails.Include(o => o.SpecMaterialItem).Where(o => o.SpecMaterialItem.ProjectID == projectID && o.SpecMaterialCollectionID == matCollectionID).AnyAsync();


                            if (!groupItem.Equals(item.MaterialGroup) && masterCenter == null)
                            {
                                if (masterCenter != null)
                                {
                                    specMatItem.SpecMaterialGroupMasterCenterID = masterCenter.ID;
                                    if (!groupItem.Equals(item.MaterialItem))
                                    {
                                        var project = await DB.Projects.Where(o => o.ProjectNo.Trim() == item.ProjectNo.Trim()).FirstOrDefaultAsync();
                                        var specMateItem = await DB.SpecMaterialItems.Where(o => o.ProjectID == project.ID && o.Name.Contains(item.MaterialItem)).AnyAsync();
                                        if (!specMateItem)
                                        {
                                            var errors = new ImportError();
                                            errors.ProjectID = projectID;
                                            errors.SpecMaterialCollectionName = item.ProjectNo.Trim() + "-" + item.Model;
                                            errors.SpecMaterialGroup = item.MaterialGroup;
                                            errors.SpecMaterialType = item.MaterialItem;
                                            errors.SpecMaterialItem = item.MaterialItemDescription;
                                            errors.ErrorDescription = "ไม่พบข้อมูลประเภทวัสดุ";
                                            errors.SpecMaterialTypeEN = item.MaterialItemEN;
                                            errors.SpecMaterialItemEN = item.MaterialItemDescriptionEN;
                                            errors.ModelName = item.Model;
                                            errorList.Add(errors);

                                            error++;
                                            continue;
                                        }
                                    }
                                }
                                else
                                {
                                    if (!groupItem.Equals(item.MaterialItem))
                                    {
                                        var project = await DB.Projects.Where(o => o.ProjectNo.Trim() == item.ProjectNo.Trim()).FirstOrDefaultAsync();
                                        var specMateItem = await DB.SpecMaterialItems.Where(o => o.ProjectID == project.ID && o.Name.Contains(item.MaterialItem)).AnyAsync();
                                        if (!specMateItem)
                                        {
                                            var errors = new ImportError();
                                            errors.ProjectID = projectID;
                                            errors.SpecMaterialCollectionName = item.ProjectNo.Trim() + "-" + item.Model;
                                            errors.SpecMaterialGroup = item.MaterialGroup;
                                            errors.SpecMaterialType = item.MaterialItem;
                                            errors.SpecMaterialItem = item.MaterialItemDescription;
                                            errors.ErrorDescription = "ไม่พบข้อมูลกลุ่มวัสดุ ไม่พบข้อมูลประเภทวัสดุ";
                                            errors.SpecMaterialTypeEN = item.MaterialItemEN;
                                            errors.SpecMaterialItemEN = item.MaterialItemDescriptionEN;
                                            errors.ModelName = item.Model;
                                            errorList.Add(errors);

                                            error++;

                                            continue;
                                        }
                                        else
                                        {
                                            var errors = new ImportError();
                                            errors.ProjectID = projectID;
                                            errors.SpecMaterialCollectionName = item.ProjectNo.Trim() + "-" + item.Model;
                                            errors.SpecMaterialGroup = item.MaterialGroup;
                                            errors.SpecMaterialType = item.MaterialItem;
                                            errors.SpecMaterialItem = item.MaterialItemDescription;
                                            errors.SpecMaterialTypeEN = item.MaterialItemEN;
                                            errors.SpecMaterialItemEN = item.MaterialItemDescriptionEN;
                                            errors.ModelName = item.Model;
                                            errors.ErrorDescription = "ไม่พบข้อมูลกลุ่มวัสดุ";
                                            errorList.Add(errors);

                                            error++;

                                            continue;
                                        }
                                    }
                                }
                            }



                            var specItem = await DB.SpecMaterialItems.Where(o => o.SpecMaterialGroupMasterCenterID == specMatItem.SpecMaterialGroupMasterCenterID && o.ProjectID == projectID).OrderByDescending(o => o.Order).FirstOrDefaultAsync();
                            if (specItem == null)
                            {
                                codeNo = SpecMaterialItems.Where(o => o.SpecMaterialGroupMasterCenterID == specMatItem.SpecMaterialGroupMasterCenterID && o.ProjectID == projectID).OrderByDescending(o => o.Order).Select(o => o.Order.GetValueOrDefault()).FirstOrDefault();
                                codeNo++;
                                specMatItem.Code = codeNo.ToString();
                                specMatItem.Order = codeNo;
                            }
                            else
                            {
                                codeNo = SpecMaterialItems.Where(o => o.SpecMaterialGroupMasterCenterID == specMatItem.SpecMaterialGroupMasterCenterID && o.ProjectID == projectID).OrderByDescending(o => o.Order).Select(o => o.Order.GetValueOrDefault()).FirstOrDefault();
                                codeNo++;
                                specMatItem.Code = codeNo.ToString();
                                specMatItem.Order = codeNo;
                            }
                            specMatItem.Name = item.MaterialItem;
                            specMatItem.ItemDescription = item.MaterialItemDescription;
                            specMatItem.NameEN = item.MaterialItemEN;
                            specMatItem.ItemDescriptionEN = item.MaterialItemDescriptionEN;
                            specMatItem.IsActive = true;
                            specMatItem.ProjectID = projectID;
                            specMatItem.SpecMaterialGroupMasterCenterID = masterCenter.ID;
                            specMatItem.Updated = DateTime.Now;
                            SpecMaterialItems.Add(specMatItem);

                            //เพิ่ม SpecMaterialCollectionDetail
                            var matCollDetail = new SpecMaterialCollectionDetail();
                            if (masterCenter != null)
                            {
                                matCollDetail.SpecMaterialGroupMasterCenterID = masterCenter.ID;
                            }
                            else
                            {
                                error++;
                            }
                            matCollDetail.SpecMaterialItemID = specMatItem.ID;
                            matCollDetail.SpecMaterialCollectionID = matCollectionID;
                            matCollDetail.IsActive = true;
                            matCollDetail.Updated = DateTime.Now;
                            SpecMaterialCollectionDetails.Add(matCollDetail);
                            result.Success++;
                        }


                        var model = await DB.Models.Where(o => o.NameTH.ToLower() == modelName.ToLower() && o.ProjectID == projectID).ToListAsync();
                        foreach (var mm in model)
                        {
                            mm.SpecMaterialCollectionID = matCollectionID;
                            DB.Models.Update(mm);
                        }

                    }


                    row++;
                }
            }

            await DB.SpecMaterialCollections.AddRangeAsync(SpecMaterialCollections);
            await DB.SpecMaterialItems.AddRangeAsync(SpecMaterialItems);
            await DB.SpecMaterialCollectionDetails.AddRangeAsync(SpecMaterialCollectionDetails.Where(o => o.IsDeleted == false));
            await DB.SaveChangesAsync();
            result.ErrorData = errorList;
            result.Error = error;
            return result;
        }
        private async Task<DataTable> ConvertExcelToDataTable(FileDTO input)
        {
            var excelStream = await this.FileHelper.GetStreamFromUrlAsync(input.Url);
            string fileName = input.Name;
            var fileExtention = fileName != null ? fileName.Split('.').ToList().Last() : null;

            bool hasHeader = true;
            using (Stream stream = new MemoryStream(XLSToXLSXConverter.ReadFully(excelStream)))
            {
                byte[] excelByte;
                if (fileExtention.ToLower() == "xls")
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
                    var wss = pck.Workbook.Worksheets;//.First();
                    DataTable tbl = new DataTable();
                    var haveHeader = false;
                    foreach (var ws in wss)
                    {
                        if (!haveHeader)
                        {
                            foreach (var firstRowCell in ws.Cells[1, 1, 1, 8])
                            {
                                tbl.Columns.Add(hasHeader ? firstRowCell.Text : string.Format("Column {0}", firstRowCell.Start.Column));
                            }
                            tbl.Columns.Add("Column 99");
                            haveHeader = true;
                        }

                        var startRow = hasHeader ? 2 : 1;
                        for (int rowNum = startRow; rowNum <= ws.Dimension.End.Row; rowNum++)
                        {
                            var wsRow = ws.Cells[rowNum, 1, rowNum, ws.Dimension.End.Column];
                            DataRow row = tbl.Rows.Add();
                            foreach (var cell in wsRow)
                            {
                                row[cell.Start.Column - 1] = cell.Text;
                            }
                            row[8] = ws.Name;
                        }
                    }


                    return tbl;
                }
            }
        }

        #endregion




        public async Task<ReportResult> ExportSpecMaterialTMPPrintFormUrlAsync(Guid ID, bool IsEN = false)
        {
            string printform = "";
            string producttype = "";

            var specMaterialCollection = await DB.SpecMaterialCollections.Where(o => o.ID == ID).FirstOrDefaultAsync();
            producttype = await DB.Projects.Include(o => o.ProductType).Where(o => o.ID == specMaterialCollection.ProjectID).Select(o => o.ProductType.Key).FirstOrDefaultAsync();


            printform = "PF_AG_SpecMaterial_TMP";



            ReportFactory reportFactory = null;
            reportFactory = new ReportFactory(ReportFolder.AG, printform, ShowAs.PDF);

            reportFactory.AddParameter("@ID", ID);


            if (producttype.Equals("1"))
            {
                reportFactory.AddParameter("@isEN", 0);
            }
            else
            {
                reportFactory.AddParameter("@isEN", 1);
            }


            return reportFactory.CreateUrl();
        }
        public async Task<bool> AddErrorRecordAsync(List<ImportError> input)
        {
            var countLoop = 1;
            var addMaster = false;
            var masterOld = "";
            List<MasterCenter> newMstList = new List<MasterCenter>();
            input = input.OrderBy(o => o.SpecMaterialGroup).ToList();
            foreach (var i in input)
            {
                var masteCenter = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == "SpecMaterialGroup" && o.Name == i.SpecMaterialGroup).FirstOrDefaultAsync();
                var orderMst = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == "SpecMaterialGroup").OrderByDescending(o => o.Order).FirstOrDefaultAsync();
                var collection = await DB.SpecMaterialCollections.Where(o => o.Name == i.SpecMaterialCollectionName).FirstOrDefaultAsync();
                var Key = orderMst.Order + countLoop;
                if (masteCenter == null)
                {

                    if (!masterOld.Equals(i.SpecMaterialGroup))
                    {

                        MasterCenter newMst = new MasterCenter
                        {
                            ID = Guid.NewGuid(),
                            Name = i.SpecMaterialGroup,
                            Key = Key.ToString(),
                            IsActive = true,
                            Order = Key,
                            MasterCenterGroupKey = "SpecMaterialGroup"
                        };
                        DB.MasterCenters.Add(newMst);
                        newMstList.Add(newMst);
                        masterOld = i.SpecMaterialGroup;
                    }



                    var specMatItem = await DB.SpecMaterialItems.Where(o => o.ProjectID == i.ProjectID).OrderByDescending(o => o.Order).FirstOrDefaultAsync();
                    var mst = newMstList.Find(o => o.Name == i.SpecMaterialGroup);
                    SpecMaterialItem newMatItem = new SpecMaterialItem
                    {
                        ID = Guid.NewGuid(),
                        SpecMaterialGroupMasterCenterID = mst.ID
                    };
                    if (specMatItem != null)
                    {
                        newMatItem.Order = specMatItem.Order + 1;
                    }
                    else
                    {
                        newMatItem.Order = 0;
                    }
                    newMatItem.Code = newMatItem.Order.ToString();
                    newMatItem.Name = i.SpecMaterialType;
                    newMatItem.ItemDescription = i.SpecMaterialItem;
                    newMatItem.IsActive = true;
                    newMatItem.ProjectID = i.ProjectID;
                    newMatItem.NameEN = i.SpecMaterialTypeEN;
                    newMatItem.ItemDescriptionEN = i.SpecMaterialItemEN;

                    DB.SpecMaterialItems.Add(newMatItem);

                    SpecMaterialCollectionDetail specMaterialCollectionDetail = new SpecMaterialCollectionDetail
                    {
                        SpecMaterialGroupMasterCenterID = mst.ID,
                        SpecMaterialItemID = newMatItem.ID,
                        SpecMaterialCollectionID = collection.ID,
                        IsActive = true
                    };

                    DB.SpecMaterialCollectionDetails.Add(specMaterialCollectionDetail);
                }
                else
                {
                    var specMatItem = await DB.SpecMaterialItems.Where(o => o.ProjectID == i.ProjectID).OrderByDescending(o => o.Order).FirstOrDefaultAsync();
                    SpecMaterialItem newMatItem = new SpecMaterialItem
                    {
                        ID = Guid.NewGuid(),
                        SpecMaterialGroupMasterCenterID = masteCenter.ID
                    };
                    if (specMatItem != null)
                    {
                        newMatItem.Order = specMatItem.Order + 1;
                    }
                    else
                    {
                        newMatItem.Order = 0;
                    }
                    newMatItem.Code = newMatItem.Order.ToString();
                    newMatItem.Name = i.SpecMaterialType;
                    newMatItem.ItemDescription = i.SpecMaterialItem;
                    newMatItem.IsActive = true;
                    newMatItem.ProjectID = i.ProjectID;
                    newMatItem.NameEN = i.SpecMaterialTypeEN;
                    newMatItem.ItemDescriptionEN = i.SpecMaterialItemEN;

                    DB.SpecMaterialItems.Add(newMatItem);

                    SpecMaterialCollectionDetail specMaterialCollectionDetail = new SpecMaterialCollectionDetail
                    {
                        SpecMaterialGroupMasterCenterID = masteCenter.ID,
                        SpecMaterialItemID = newMatItem.ID,
                        SpecMaterialCollectionID = collection.ID,
                        IsActive = true
                    };

                    DB.SpecMaterialCollectionDetails.Add(specMaterialCollectionDetail);
                }
                countLoop++;

            }

            await DB.SaveChangeAsync();

            return true;
        }

        public async Task<bool> UpdateCollectionAgreementAsync(Guid agreementID)
        {
            var result = false;

            var agreement = await DB.Agreements.Where(o => o.ID == agreementID).FirstOrDefaultAsync();
            var unit = await DB.Units.Include(o => o.Model).Where(o => o.ID == agreement.UnitID).FirstOrDefaultAsync();

            if (agreement.SpecMaterialCollectionID == null)
            {
                agreement.SpecMaterialCollectionID = unit.Model.SpecMaterialCollectionID;
                DB.Agreements.Update(agreement);
                await DB.SaveChangesAsync();
            }

            return result;
        }

        public  async  Task<ReportResult> ExportSpecMaterialPrintFormUrlAsync(Guid ID, bool IsEN = false)
        {
            string printform = "";
            string producttype = "";

            Agreement agreement = new Agreement();
            Booking booking = new Booking();
            Model model = new Model();

            agreement = await DB.Agreements.Include(o => o.Project).Include(o => o.Unit).ThenInclude(o => o.Model).Where(o => o.ID == ID).FirstOrDefaultAsync() ?? new Agreement();

            if (agreement == null)
            {
                booking = await DB.Bookings
                                  .Include(o => o.Project)
                                  .Include(o => o.Unit)
                                    .ThenInclude(o => o.Model)
                                  .Where(o => o.ID == ID).FirstOrDefaultAsync() ?? new Booking();
                model = await DB.Models
                                .Where(o => o.ID == booking.Unit.ModelID).FirstOrDefaultAsync() ??  new Model();
            }
            else
            {
                model = await DB.Models
                                .Where(o => o.ID == agreement.Unit.ModelID).FirstOrDefaultAsync()?? new Model() ;
            }

            if (agreement != null)
            {
                producttype = await DB.Projects.Include(o => o.ProductType).Where(o => o.ID == agreement.ProjectID).Select(o => o.ProductType.Key).FirstOrDefaultAsync() ?? string.Empty;
            }
            else if (booking != null)
            {
                producttype = await DB.Projects.Include(o => o.ProductType).Where(o => o.ID == booking.ProjectID).Select(o => o.ProductType.Key).FirstOrDefaultAsync() ?? string.Empty;
            }


            if (producttype.Equals("1"))
            {
                printform = "PF_AG_SpecMaterial";
            }
            else
            {
                printform = "PF_AG_SpecMaterial";
            }


            ReportFactory reportFactory = null;
            reportFactory = new ReportFactory(ReportFolder.AG, printform, ShowAs.PDF);

            if (agreement != null)
            {
                reportFactory.AddParameter("@ProjectID", agreement.ProjectID);
                reportFactory.AddParameter("@ModelID", agreement.Unit?.ModelID);
                reportFactory.AddParameter("@ID", agreement.ID);
                reportFactory.AddParameter("@SpecMaterialCollectionID", model.SpecMaterialCollectionID);
            }
            else if (booking != null)
            {
                reportFactory.AddParameter("@ProjectID", booking.ProjectID);
                reportFactory.AddParameter("@ModelID", booking.Unit?.ModelID);
                reportFactory.AddParameter("@ID", booking.ID);
                reportFactory.AddParameter("@SpecMaterialCollectionID", model.SpecMaterialCollectionID);
            }


            if (producttype.Equals("1"))
            {
                reportFactory.AddParameter("@isEN", 0);
            }
            else
            {
                reportFactory.AddParameter("@isEN", 1);
            }


            return reportFactory.CreateUrl();
        }
    }
}
