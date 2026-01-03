using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Base.DTOs;
using Base.DTOs.MST;
using Database.Models;
using Database.Models.MasterKeys;
using Database.Models.MST;
using Common.Helper;
using MST_Finacc.Params.Filters;
using MST_Finacc.Params.Outputs;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using OfficeOpenXml;
using PagingExtensions;
using Common.Helper.Logging;
using FileStorage;

namespace MST_Finacc.Services
{
    public class BankAccountService : IBankAccountService
    {
        public LogModel logModel { get; set; }
        private readonly DatabaseContext DB;
        //private readonly IConfiguration Configuration;
        private FileHelper FileHelper;
        public BankAccountService(DatabaseContext db, IConfiguration configuration)
        {
            logModel = new LogModel("BankAccountService", null);
            DB = db;

            var minioSapEndpoint = Environment.GetEnvironmentVariable("minio_Endpoint");
            var minioSapAccessKey = Environment.GetEnvironmentVariable("minio_AccessKey");
            var minioSapSecretKey = Environment.GetEnvironmentVariable("minio_SecretKey");
            var minioSapWithSSL = Environment.GetEnvironmentVariable("minio_WithSSL");
            var minioTempBucketName = Environment.GetEnvironmentVariable("minio_TempBucket");
            var publicURL = Environment.GetEnvironmentVariable("minio_PublicURL");

            FileHelper = new FileHelper(minioSapEndpoint, minioSapAccessKey, minioSapSecretKey, minioTempBucketName, "", publicURL, minioSapWithSSL == "true");
        }

        public BankAccountService(DatabaseContext db)
        {
            logModel = new LogModel("BankAccountService", null);
            DB = db;

            var minioSapEndpoint = Environment.GetEnvironmentVariable("minio_Endpoint");
            var minioSapAccessKey = Environment.GetEnvironmentVariable("minio_AccessKey");
            var minioSapSecretKey = Environment.GetEnvironmentVariable("minio_SecretKey");
            var minioSapWithSSL = Environment.GetEnvironmentVariable("minio_WithSSL");
            var minioTempBucketName = Environment.GetEnvironmentVariable("minio_TempBucket");
            var publicURL = Environment.GetEnvironmentVariable("minio_PublicURL");

            FileHelper = new FileHelper(minioSapEndpoint, minioSapAccessKey, minioSapSecretKey, minioTempBucketName, "", publicURL, minioSapWithSSL == "true");
        }

        public async Task<List<BankAccountDropdownDTO>> GetBankAccountDropdownListAsync(string displayName, string bankAccountTypeKey, Guid? companyID, bool? IsWrongAccount, string paymentMethodTypeKey, bool? IsActive, BankAccountDropdownSortByParam sortByParam, CancellationToken cancellationToken = default)
        {
            var gLAccountTypeID = (await DB.GLAccountTypes.AsNoTracking().FirstOrDefaultAsync(o => o.Key == GLAccountTypeKeys.Bank, cancellationToken))?.ID;

            var query = DB.BankAccounts.Where(o => o.GLAccountTypeID == gLAccountTypeID).Include(o => o.Bank).Include(x => x.BankAccountType).AsQueryable();

            if (!string.IsNullOrEmpty(displayName))
                query = from o in query
                        let dName = !string.IsNullOrEmpty(o.DisplayName) ? o.DisplayName : (o.Bank.Alias ?? "") + " " + (o.BankAccountType.Name ?? "") + " " + (o.BankAccountNo ?? "")
                        where dName.Replace("-", string.Empty).ToLower().Contains(displayName.Replace("-", string.Empty).ToLower())
                        select o;

            if (!string.IsNullOrEmpty(bankAccountTypeKey))
                query = query.Where(o => o.BankAccountType.Key == bankAccountTypeKey);

            if (companyID != null)
            {
                if (!(IsWrongAccount ?? false))
                    query = query.Where(o => o.CompanyID == companyID);
                else
                    query = query.Where(o => o.CompanyID != companyID);
            }
            if (IsActive ?? false)
            {
                query = query.Where(o => o.IsActive == true);
            }
            if (!string.IsNullOrEmpty(paymentMethodTypeKey))
            {
                if (paymentMethodTypeKey.Equals(PaymentMethodKeys.QRCode))
                {
                    query = query.Where(o => o.IsQRCode == true);
                }
                if (paymentMethodTypeKey.Equals(PaymentMethodKeys.ForeignBankTransfer))
                {
                    query = query.Where(o => o.IsForeignTransfer == true);
                }
                if (paymentMethodTypeKey.Equals(PaymentMethodKeys.BankTransfer))
                {
                    query = query.Where(o => o.IsTransferAccount == true);
                }
                if (paymentMethodTypeKey.Equals(PaymentMethodKeys.BankTransfer))
                {
                    query = query.Where(o => o.IsTransferAccount == true);
                }
                if (paymentMethodTypeKey.Equals(PaymentMethodKeys.Deposit))
                {
                    query = query.Where(o => o.IsDepositAccount == true);
                }
            }

            BankAccountDropdownDTO.SortBy(sortByParam, ref query);

            var results = await query.Select(o => BankAccountDropdownDTO.CreateFromModel(o)).ToListAsync(cancellationToken);

            return results;
        }

        /// <summary>
        /// ดึงรายการบัญชีธนาคาร
        /// UI: https://projects.invisionapp.com/d/main#/console/17482171/362367408/preview
        /// </summary>
        /// <returns>The bank account list async.</returns>
        /// <param name="filter">Filter.</param>
        /// <param name="pageParam">Page parameter.</param>
        /// <param name="sortByParam">Sort by parameter.</param>
        public async Task<BankAccountPaging> GetBankAccountListAsync(BankAccountFilter filter, PageParam pageParam, BankAccountSortByParam sortByParam, CancellationToken cancellationToken = default)
        {
            IQueryable<BankAccountQueryResult> query = DB.BankAccounts.AsNoTracking().Include(x => x.GLAccountCategory).Select(o =>
                                                                    new BankAccountQueryResult
                                                                    {
                                                                        BankAccount = o,
                                                                        Bank = o.Bank,
                                                                        BankAccountType = o.BankAccountType,
                                                                        BankBranch = o.BankBranch,
                                                                        Company = o.Company,
                                                                        Province = o.Province,
                                                                        GLAccountType = o.GLAccountType,
                                                                        UpdatedBy = o.UpdatedBy,
                                                                        GLAccountCategory = o.GLAccountCategory
                                                                    });

            #region Filter
            if (filter.BankID != null && filter.BankID != Guid.Empty)
            {
                query = query.Where(o => o.Bank.ID == filter.BankID);
            }
            if (!string.IsNullOrEmpty(filter.BankBranchName))
            {
                query = query.Where(o => o.BankBranch.Name.Contains(filter.BankBranchName));
            }
            if (!string.IsNullOrEmpty(filter.BankAccountNo))
            {
                query = query.Where(o => o.BankAccount.BankAccountNo.Contains(filter.BankAccountNo));
            }
            if (!string.IsNullOrEmpty(filter.BankAccountTypeKey))
            {
                var bankAccountTypeMasterCenterID = await DB.MasterCenters.AsNoTracking().FirstOrDefaultAsync(x => x.Key == filter.BankAccountTypeKey
                                                                       && x.MasterCenterGroupKey == MasterCenterGroupKeys.BankAccountType
                                                                      , cancellationToken);
                if (bankAccountTypeMasterCenterID is not null)
                    query = query.Where(x => x.BankAccountType.ID == bankAccountTypeMasterCenterID.ID);
            }

            if (filter.CompanyID != null && filter.CompanyID != Guid.Empty)
            {
                query = query.Where(o => o.Company.ID == filter.CompanyID);
            }
            if (!string.IsNullOrEmpty(filter.GLAccountNo))
            {
                query = query.Where(o => o.BankAccount.GLAccountNo.Contains(filter.GLAccountNo));
            }
            if (!string.IsNullOrEmpty(filter.GLRefCode))
            {
                query = query.Where(o => o.BankAccount.GLRefCode.Contains(filter.GLRefCode));
            }
            if (!string.IsNullOrEmpty(filter.Name))
            {
                query = query.Where(o => o.BankAccount.Name.Contains(filter.Name));
            }
            if (filter.IsActive != null)
            {
                query = query.Where(o => o.BankAccount.IsActive == filter.IsActive);
            }
            if (filter.HasVat != null)
            {
                query = query.Where(o => o.BankAccount.HasVat == filter.HasVat);
            }
            if (!string.IsNullOrEmpty(filter.GLAccountTypeKey))
            {
                var gLAccountTypeMasterCenterID = await DB.GLAccountTypes.FirstOrDefaultAsync(x => x.Key == filter.GLAccountTypeKey, cancellationToken);
                if (gLAccountTypeMasterCenterID is not null)
                    query = query.Where(x => x.GLAccountType.ID == gLAccountTypeMasterCenterID.ID);
            }
            if (filter.UpdatedFrom != null)
            {
                query = query.Where(o => o.BankAccount.Updated >= filter.UpdatedFrom);
            }
            if (filter.UpdatedTo != null)
            {
                query = query.Where(o => o.BankAccount.Updated <= filter.UpdatedTo);
            }
            if (!string.IsNullOrEmpty(filter.UpdatedBy))
            {
                query = query.Where(o => o.UpdatedBy.DisplayName.Contains(filter.UpdatedBy));
            }
            if (filter.IsTransferAccount != null)
            {
                query = query.Where(o => o.BankAccount.IsTransferAccount == filter.IsTransferAccount);
            }
            if (filter.IsDirectDebit != null)
            {
                query = query.Where(o => o.BankAccount.IsDirectDebit == filter.IsDirectDebit);
            }
            if (filter.IsDirectCredit != null)
            {
                query = query.Where(o => o.BankAccount.IsDirectCredit == filter.IsDirectCredit);
            }
            if (filter.IsDepositAccount != null)
            {
                query = query.Where(o => o.BankAccount.IsDepositAccount == filter.IsDepositAccount);
            }
            if (filter.IsPCard != null)
            {
                query = query.Where(o => o.BankAccount.IsPCard == filter.IsPCard);
            }
            if (filter.IsForeignTransfer != null)
            {
                query = query.Where(o => o.BankAccount.IsForeignTransfer == filter.IsForeignTransfer);
            }
            if (filter.IsQRCode != null)
            {
                query = query.Where(o => o.BankAccount.IsQRCode == filter.IsQRCode);
            }
            if (filter.IsBillPayment != null)
            {
                query = query.Where(o => o.BankAccount.IsBillPayment == filter.IsBillPayment);
            }
            #endregion

            BankAccountDTO.SortBy(sortByParam, ref query);

            var pageOutput = PagingHelper.Paging(pageParam, ref query);

            var queryResults = await query.ToListAsync(cancellationToken);

            var results = queryResults.Select(o => BankAccountDTO.CreateFromQueryResult(o)).ToList();

            return new BankAccountPaging()
            {
                PageOutput = pageOutput,
                BankAccounts = results
            };
        }

        /// <summary>
        /// UI: https://projects.invisionapp.com/d/main#/console/17482171/362367410/preview
        /// </summary>
        /// <returns>The bank account detail async.</returns>
        /// <param name="id">Identifier.</param>
        public async Task<BankAccountDTO> GetBankAccountDetailAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var model = await DB.BankAccounts.Where(o => o.ID == id)
                                             .Include(o => o.Bank)
                                             .Include(o => o.BankAccountType)
                                             .Include(o => o.BankBranch)
                                             .Include(o => o.Company)
                                             .Include(o => o.Province)
                                             .Include(o => o.GLAccountType)
                                             .Include(o => o.UpdatedBy)
                                             .Include(x => x.GLAccountCategory)
                                             .FirstOrDefaultAsync(cancellationToken);
            var result = BankAccountDTO.CreateFromModel(model);
            return result;
        }

        /// <summary>
        /// UI: https://projects.invisionapp.com/d/main#/console/17482171/362367410/preview
        /// </summary>
        /// <returns>The bank account async.</returns>
        /// <param name="input">Input.</param>
        public async Task<BankAccountDTO> CreateBankAccountAsync(BankAccountDTO input)
        {
            await input.ValidateAsync(DB);
            BankAccount model = new BankAccount();
            var glAccountTypeID = (await DB.GLAccountTypes.FirstOrDefaultAsync(o => o.Key == GLAccountTypeKeys.Bank))?.ID;

            input.GLAccountType = new GLAccountTypeDTO()
            {
                Id = glAccountTypeID
            };

            input.ToModel(ref model);

            model.Name = input.Bank?.NameTH + " " + input.BankAccountType?.Name + " " + input.BankAccountNo;

            await DB.BankAccounts.AddAsync(model);
            await DB.SaveChangesAsync();
            var result = await GetBankAccountDetailAsync(model.ID);
            return result;
        }

        public async Task<BankAccountDTO> CreateChartOfAccountAsync(BankAccountDTO input)
        {
            await input.ValidateChartOfAccountAsync(DB);
            BankAccount model = new BankAccount();
            input.ToModel(ref model);
            var key = "GL";
            var type = "MST.BankAccount";
            var runningno = await DB.RunningNumberCounters.FirstOrDefaultAsync(o => o.Key == key && o.Type == type);
            if (runningno == null)
            {
                var runningNumberCounter = new RunningNumberCounter
                {
                    Key = key,
                    Type = type,
                    Count = 1
                };
                await DB.RunningNumberCounters.AddAsync(runningNumberCounter);
                await DB.SaveChangesAsync();

                model.GLRefCode = key + runningNumberCounter.Count.ToString("000");
                runningNumberCounter.Count++;
                DB.Entry(runningNumberCounter).State = EntityState.Modified;
            }
            else
            {
                model.GLRefCode = key + runningno.Count.ToString("000");
                runningno.Count++;
                DB.Entry(runningno).State = EntityState.Modified;
            }
            await DB.BankAccounts.AddAsync(model);
            await DB.SaveChangesAsync();
            var result = await GetBankAccountDetailAsync(model.ID);
            return result;
        }

        /// <summary>
        /// UI: https://projects.invisionapp.com/d/main#/console/17482171/362367410/preview
        /// </summary>
        /// <returns>The bank account async.</returns>
        /// <param name="id">Identifier.</param>
        /// <param name="input">Input.</param>
        public async Task<BankAccountDTO> UpdateBankAccountAsync(Guid id, BankAccountDTO input)
        {
            await input.ValidateAsync(DB);
            var model = await DB.BankAccounts.FindAsync(id);
            input.ToModel(ref model);

            DB.Entry(model).State = EntityState.Modified;
            await DB.SaveChangesAsync();
            var result = await GetBankAccountDetailAsync(model.ID);
            return result;
        }

        public async Task<BankAccountDTO> UpdateChartOfAccountAsync(Guid id, BankAccountDTO input)
        {
            await input.ValidateChartOfAccountAsync(DB);
            var model = await DB.BankAccounts.FindAsync(id);
            input.ToModel(ref model);

            DB.Entry(model).State = EntityState.Modified;
            await DB.SaveChangesAsync();
            var result = await GetBankAccountDetailAsync(model.ID);
            return result;
        }

        /// <summary>
        /// UI: https://projects.invisionapp.com/d/main#/console/17482171/362367408/preview
        /// </summary>
        /// <returns>The bank account async.</returns>
        /// <param name="id">Identifier.</param>
        public async Task DeleteBankAccountAsync(Guid id)
        {
            BankAccountDTO bankAccount = new BankAccountDTO { Id = id };
            await bankAccount.ValidateDeleteAsync(DB);
            var model = await DB.BankAccounts.FindAsync(id);
            if (model is not null)
            {
                model.IsDeleted = true;
            }

            DB.Entry(model).State = EntityState.Modified;
            await DB.SaveChangesAsync();
        }

        /// <summary>
        /// UI: https://projects.invisionapp.com/d/main#/console/17482171/362367408/preview
        /// </summary>
        public async Task DeleteBankAccountListAsync(List<BankAccountDTO> inputs)
        {
            foreach (var item in inputs)
            {
                await item.ValidateDeleteAsync(DB);
                var model = await DB.BankAccounts.FirstOrDefaultAsync(x => x.ID == item.Id);
                if (model is not null)
                {
                    model.IsDeleted = true;
                }
                DB.Entry(model).State = EntityState.Modified;
            }
            await DB.SaveChangesAsync();
        }

        public async Task<FileDTO> ExportExcelBankAccAsync(BankAccountFilter filter, PageParam pageParam, BankAccountSortByParam sortByParam, CancellationToken cancellationToken = default)
        {
            ExportExcel result = new ExportExcel();
            var Data = GetBankAccountListAsync(filter, pageParam, sortByParam).Result;
            string path = Path.Combine(FileHelper.GetApplicationRootPath(), "ExcelTemplates", "TemplatesBankACCExport.xlsx");
            byte[] tmp = await File.ReadAllBytesAsync(path);

            using (System.IO.MemoryStream stream = new System.IO.MemoryStream(tmp))
            using (ExcelPackage package = new ExcelPackage(stream))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets.First();
                int nRow = 2;
                foreach (var row in Data.BankAccounts)
                {
                    worksheet.Cells[nRow, 1].Value = row.Bank.NameTH ?? string.Empty;
                    worksheet.Cells[nRow, 2].Value = row.BankBranch?.Name ?? string.Empty;
                    worksheet.Cells[nRow, 3].Value = row.BankAccountNo ?? string.Empty;
                    worksheet.Cells[nRow, 4].Value = row.BankAccountType.Name; //row.ReceiveDate?.ToString("dd/MM/yyyy (HH:mm)");
                    worksheet.Cells[nRow, 5].Value = row.Company?.SAPCompanyID + "-" + row.Company?.NameTH;
                    worksheet.Cells[nRow, 6].Value = row.GLAccountNo;
                    worksheet.Cells[nRow, 7].Value = row.IsActive ? "Active" : "Inactive";
                    worksheet.Cells[nRow, 8].Value = row.IsTransferAccount ? "Active" : "Inactive";
                    worksheet.Cells[nRow, 9].Value = row.IsDepositAccount ? "Active" : "Inactive";
                    worksheet.Cells[nRow, 10].Value = row.IsDirectDebit ? "Active" : "Inactive";
                    worksheet.Cells[nRow, 11].Value = row.IsDirectCredit ? "Active" : "Inactive";
                    worksheet.Cells[nRow, 12].Value = row.IsQRCode ? "Active" : "Inactive";
                    worksheet.Cells[nRow, 13].Value = row.IsPCard ? "Active" : "Inactive";
                    worksheet.Cells[nRow, 14].Value = row.IsForeignTransfer ? "Active" : "Inactive";
                    worksheet.Cells[nRow, 15].Value = row.IsBillPayment ? "Active" : "Inactive";
                    worksheet.Cells[nRow, 16].Value = row.ServiceCode;
                    worksheet.Cells[nRow, 17].Value = row.MerchantID;
                    worksheet.Cells[nRow, 18].Value = row.DRServiceCode;
                    worksheet.Cells[nRow, 19].Value = row.Updated?.ToString("dd/MM/yyyy (HH:mm)");
                    worksheet.Cells[nRow, 20].Value = row.UpdatedBy;
                    nRow++;
                }
                result.FileContent = package.GetAsByteArray();
                result.FileName = "Export_BankAccount" + DateTime.Now.ToString("yyyy_MM_ddTHH_mm_ss") + ".xlsx";
                result.FileType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            }
            Stream fileStream = new MemoryStream(result.FileContent);
            string fileName = result.FileName;
            string contentType = result.FileType;
            string filePath = $"Export_BANKACCOUNT/Export/";
            //var minioTempBucketName = Configuration["Minio:TempBucket"];
            var minioTempBucketName = Environment.GetEnvironmentVariable("minio_TempBucket");
            var uploadResult = await FileHelper.UploadFileFromStreamWithOutGuid(fileStream, minioTempBucketName, filePath, fileName, contentType);

            return new FileDTO()
            {
                Name = fileName,
                Url = uploadResult.Url
            };
        }
    }
}
