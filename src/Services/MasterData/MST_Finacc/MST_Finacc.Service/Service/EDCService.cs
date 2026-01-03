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
using MST_Finacc.Params.Filters;
using MST_Finacc.Params.Outputs;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using OfficeOpenXml;
using PagingExtensions;
using Report.Integration;
using Common.Helper.Logging;
using FileStorage;

namespace MST_Finacc.Services
{
    public class EDCService : IEDCService
    {
        public LogModel logModel { get; set; }
        private readonly DatabaseContext DB;
        private readonly IConfiguration Configuration;
        private FileHelper FileHelper;

        public EDCService(DatabaseContext db)
        {
            //this.Configuration = configuration;
            logModel = new LogModel("EDCService", null);
            this.DB = db;

            var minioSapEndpoint = Environment.GetEnvironmentVariable("minio_Endpoint");
            var minioSapAccessKey = Environment.GetEnvironmentVariable("minio_AccessKey");
            var minioSapSecretKey = Environment.GetEnvironmentVariable("minio_SecretKey");
            var minioSapWithSSL = Environment.GetEnvironmentVariable("minio_WithSSL");
            var minioTempBucketName = Environment.GetEnvironmentVariable("minio_TempBucket");
            var publicURL = Environment.GetEnvironmentVariable("minio_PublicURL");

            this.FileHelper = new FileHelper(minioSapEndpoint, minioSapAccessKey, minioSapSecretKey, minioTempBucketName, "", publicURL, minioSapWithSSL == "true");
        }

        public EDCService(DatabaseContext db, IConfiguration configuration)
        {
            logModel = new LogModel("EDCService", null);
            this.DB = db;

            var minioSapEndpoint = Environment.GetEnvironmentVariable("minio_Endpoint");
            var minioSapAccessKey = Environment.GetEnvironmentVariable("minio_AccessKey");
            var minioSapSecretKey = Environment.GetEnvironmentVariable("minio_SecretKey");
            var minioSapWithSSL = Environment.GetEnvironmentVariable("minio_WithSSL");
            var minioTempBucketName = Environment.GetEnvironmentVariable("minio_TempBucket");
            var publicURL = Environment.GetEnvironmentVariable("minio_PublicURL");

            this.FileHelper = new FileHelper(minioSapEndpoint, minioSapAccessKey, minioSapSecretKey, minioTempBucketName, "", publicURL, minioSapWithSSL == "true");
        }

        /// <summary>
        /// ดึง dropdown เครื่องรูดบัตร
        /// </summary>
        /// <param name="projectID"></param>
        /// <param name="bankName"></param>
        /// <returns></returns>
        public async Task<List<EDCDropdownDTO>> GetEDCDropdownListUrlAsync(Guid? projectID, string bankName)
        {
            var query = DB.EDCs.Include(o => o.Bank).AsQueryable();
            if (projectID != null)
            {
                query = query.Where(o => o.ProjectID == projectID);
            }

            if (!string.IsNullOrEmpty(bankName))
            {
                query = query.Where(o => o.Bank.NameTH.ToLower().Contains(bankName.ToLower()));
            }

            var results = await query.OrderBy(o => o.Bank.NameTH).Take(100).Select(o => EDCDropdownDTO.CreateFromModel(o)).ToListAsync();

            return results;
        }

        /// <summary>
        /// ดึง dropdown เครื่องรูดบัตร
        /// </summary>
        /// <param name="projectID"></param>
        /// <param name="bankName"></param>
        /// <returns></returns>
        public async Task<List<BankDropdownDTO>> GetEDCBankDropdownListAsync(Guid? projectID, string bankName, bool? IsWrongProject)
        {
            var query = DB.EDCs.Include(o => o.Bank).AsQueryable();

            if (projectID != null)
                if (!(IsWrongProject ?? false))
                    query = query.Where(o => o.ProjectID == projectID);
                else
                    query = query.Where(o => o.ProjectID != projectID);

            if (!string.IsNullOrEmpty(bankName))
                query = query.Where(o => o.Bank.NameTH.ToLower().Contains(bankName.ToLower()));

            var bankQuery = query.Select(o => o.Bank).Distinct();

            var results = await bankQuery.OrderBy(o => o.NameTH).Select(o => BankDropdownDTO.CreateFromModel(o)).ToListAsync();

            return results;
        }

        public async Task<List<MasterCenterDropdownDTO>> GetEDCCreditCardPaymentTypeDropdownListAsync(Guid? bankID)
        {
            var query = DB.EDCFees.Include(x => x.CreditCardPaymentType).AsQueryable();

            if (bankID != null)
            {
                query = query.Where(x => x.BankID == bankID);
            }

            var bankQuery = await query.Select(o => o.CreditCardPaymentType).Distinct().ToListAsync();

            var results = bankQuery.Select(o => MasterCenterDropdownDTO.CreateFromModel(o)).ToList();

            return results;
        }

        /// <summary>
        /// ดึงเครื่องรูดบัตร
        /// UI: https://projects.invisionapp.com/d/main#/console/17482171/362367402/preview
        /// </summary>
        /// <returns>The EDCL ist async.</returns>
        /// <param name="filter">Filter.</param>
        /// <param name="pageParam">Page parameter.</param>
        /// <param name="sortByParam">Sort by parameter.</param>
        public async Task<EDCPaging> GetEDCListAsync(EDCFilter filter, PageParam pageParam, EDCSortByParam sortByParam)
        {
            IQueryable<EDCQueryResult> query = DB.EDCs
                .Include(o => o.Project.ProjectStatus)
                .Include(o => o.BankAccount)
                .ThenInclude(o => o.Bank)
                .Include(o => o.BankAccount)
                .ThenInclude(o => o.BankAccountType)
                .Select(o =>
                new EDCQueryResult
                {
                    EDC = o,
                    Bank = o.Bank,
                    BankAccount = o.BankAccount,
                    CardMachineStatus = o.CardMachineStatus,
                    CardMachineType = o.CardMachineType,
                    Company = o.Company,
                    Project = o.Project,
                    UpdatedBy = o.UpdatedBy
                });

            #region Filter

            if (!string.IsNullOrEmpty(filter.Code))
            {
                query = query.Where(o => o.EDC.Code.Contains(filter.Code));
            }
            if (!string.IsNullOrEmpty(filter.CardMachineTypeKey))
            {
                var cardMachineTypeMasterCenterID = await DB.MasterCenters.Where(x => x.Key == filter.CardMachineTypeKey
                                                                       && x.MasterCenterGroupKey == MasterCenterGroupKeys.CardMachineType)
                                                                      .Select(x => x.ID).FirstAsync();
                query = query.Where(x => x.CardMachineType.ID == cardMachineTypeMasterCenterID);
            }
            if (!string.IsNullOrEmpty(filter.ProjectStatusKey))
            {
                var projectStatusMasterCenterID = await DB.MasterCenters.Where(x => x.Key == filter.ProjectStatusKey
                                                                       && x.MasterCenterGroupKey == MasterCenterGroupKeys.ProjectStatus)
                                                                      .Select(x => x.ID).FirstAsync();
                query = query.Where(x => x.Project.ProjectStatusMasterCenterID == projectStatusMasterCenterID);
            }
            if (filter.BankAccountID != null && filter.BankAccountID != Guid.Empty)
            {
                query = query.Where(o => o.BankAccount.ID == filter.BankAccountID);
            }
            if (filter.CompanyID != null && filter.CompanyID != Guid.Empty)
            {
                query = query.Where(o => o.Company.ID == filter.CompanyID);
            }
            if (filter.ProjectID != null && filter.ProjectID != Guid.Empty)
            {
                query = query.Where(o => o.Project.ID == filter.ProjectID);
            }
            if (!string.IsNullOrEmpty(filter.ReceiveBy))
            {
                query = query.Where(o => o.EDC.ReceiveBy.Contains(filter.ReceiveBy));
            }
            if (!string.IsNullOrEmpty(filter.UpdatedBy))
            {
                query = query.Where(x => x.UpdatedBy.DisplayName.Contains(filter.UpdatedBy));
            }
            if (filter.UpdatedFrom != null)
            {
                query = query.Where(x => x.EDC.Updated >= filter.UpdatedFrom);
            }
            if (filter.UpdatedTo != null)
            {
                query = query.Where(x => x.EDC.Updated <= filter.UpdatedTo);
            }

            if (filter.ReceiveDateFrom != null)
            {
                query = query.Where(x => x.EDC.ReceiveDate >= filter.ReceiveDateFrom);
            }
            if (filter.ReceiveDateTo != null)
            {
                query = query.Where(x => x.EDC.ReceiveDate <= filter.ReceiveDateTo);
            }
            if (filter.ReceiveDateFrom != null && filter.ReceiveDateTo != null)
            {
                query = query.Where(x => x.EDC.ReceiveDate >= filter.ReceiveDateFrom
                                    && x.EDC.ReceiveDate <= filter.ReceiveDateTo);
            }
            if (!string.IsNullOrEmpty(filter.CardMachineStatusKey))
            {
                var cardMachineStatusMasterCenterID = await DB.MasterCenters.Where(x => x.Key == filter.CardMachineStatusKey
                                                                       && x.MasterCenterGroupKey == "CardMachineStatus")
                                                                      .Select(x => x.ID).FirstAsync();
                query = query.Where(x => x.CardMachineStatus.ID == cardMachineStatusMasterCenterID);
            }
            if (filter.BankID != null)
            {
                query = query.Where(x => x.Bank.ID == filter.BankID);
            }
            if (filter.TelNo != null)
            {
                query = query.Where(x => x.EDC.TelNo.Contains(filter.TelNo));
            }

            if (filter.IsLast5Unit ?? false)
            {
                var unit = await (from unitTmp in DB.Units
                                  join booking in DB.Bookings.Where(x => x.IsCancelled == false) on unitTmp.ID equals booking.UnitID
                                          into bookings
                                  from booking in bookings.DefaultIfEmpty()
                                  where booking == null
                                  select unitTmp).ToListAsync()
                           ;
                var project = unit.GroupBy(x => x.ProjectID).Select(x => new { ProjectID = x.Key, CountUnit = x.Count() }).ToList();
                var projectIDList = project.Where(x => x.CountUnit <= 5).Select(x => x.ProjectID).ToList();
                query = query.Where(x => projectIDList.Contains(x.EDC.ProjectID));
            }

            #endregion

            EDCDTO.SortBy(sortByParam, ref query);

            var pageOutput = PagingHelper.Paging<EDCQueryResult>(pageParam, ref query);

            var queryResults = await query.ToListAsync();

            var results = queryResults.Select(o => EDCDTO.CreateFromQueryResult(o)).ToList();

            return new EDCPaging()
            {
                PageOutput = pageOutput,
                EDCs = results
            };
        }

        /// <summary>
        /// UI: https://projects.invisionapp.com/d/main#/console/17482171/362367403/preview
        /// </summary>
        /// <returns>The EDOD etail async.</returns>
        /// <param name="id">Identifier.</param>
        public async Task<EDCDTO> GetEDCDetailAsync(Guid id)
        {
            var model = await DB.EDCs.Where(o => o.ID == id)
                                     .Include(o => o.Bank)
                                     .Include(o => o.Company)
                                     .Include(o => o.BankAccount)
                                     .Include(o => o.CardMachineType)
                                     .Include(o => o.CardMachineStatus)
                                     .Include(o => o.Project)
                                     .Include(o => o.UpdatedBy)
                                     .FirstAsync();
            var result = EDCDTO.CreateFromModel(model);
            return result;
        }

        /// <summary>
        /// UI: https://projects.invisionapp.com/d/main#/console/17482171/362367403/preview
        /// </summary>
        /// <returns>The EDCA sync.</returns>
        /// <param name="input">Input.</param>
        public async Task<EDCDTO> CreateEDCAsync(EDCDTO input)
        {
            await input.ValidateAsync(DB);
            EDC model = new EDC();
            input.ToModel(ref model);
            await DB.EDCs.AddAsync(model);
            await DB.SaveChangesAsync();
            var result = await this.GetEDCDetailAsync(model.ID);
            return result;
        }

        /// <summary>
        /// https://projects.invisionapp.com/d/main#/console/17482171/362367403/preview
        /// </summary>
        /// <returns>The EDCA sync.</returns>
        /// <param name="id">Identifier.</param>
        /// <param name="input">Input.</param>
        public async Task<EDCDTO> UpdateEDCAsync(Guid id, EDCDTO input)
        {
            await input.ValidateAsync(DB);
            var model = await DB.EDCs.FindAsync(id);
            input.ToModel(ref model);

            DB.Entry(model).State = EntityState.Modified;
            await DB.SaveChangesAsync();
            var result = await this.GetEDCDetailAsync(model.ID);
            return result;
        }

        /// <summary>
        /// UI: https://projects.invisionapp.com/d/main#/console/17482171/362367402/preview
        /// </summary>
        /// <returns>The EDCA sync.</returns>
        /// <param name="id">Identifier.</param>
        public async Task DeleteEDCAsync(Guid id)
        {
            var model = await DB.EDCs.FindAsync(id);
            model.IsDeleted = true;

            DB.Entry(model).State = EntityState.Modified;
            await DB.SaveChangesAsync();
        }

        /// <summary>
        /// ดึงธนาคารจากข้อมูลเครื่องรูดบัตร (Group By ธนาคาร)
        /// UI: https://projects.invisionapp.com/d/main#/console/17482171/362367404/preview
        /// </summary>
        /// <returns>The EDCB ank list async.</returns>
        /// <param name="filter">Filter.</param>
        /// <param name="pageParam">Page parameter.</param>
        /// <param name="sortByParam">Sort by parameter.</param>
        public async Task<EDCBankPaging> GetEDCBankListAsync(EDCBankFilter filter, PageParam pageParam, EDCBankSortByParam sortByParam)
        {
            var edcGroups = await DB.EDCs
                .Include(x => x.Bank)
                .Include(o => o.UpdatedBy)
                .GroupBy(o => o.Bank)
                .Select(g => new
                {
                    Bank = g.Key,
                    EDC = g.OrderByDescending(p => p.Updated).FirstOrDefault()
                })
                .Where(x => x.Bank != null)
                .ToListAsync();

            var edcFees = await DB.EDCFees
                .Include(o => o.UpdatedBy)
                .OrderBy(x => x.BankID)
                .ThenByDescending(x => x.Updated)
                .ToListAsync();

            var query = from main in edcGroups
                        join edcFee in edcFees
                            on main.Bank.ID equals edcFee.BankID into edcFeesGroup
                        from edcFee in edcFeesGroup.DefaultIfEmpty()
                        group new { main, edcFee } by new { a = main.Bank.ID, main.EDC.ID } into g
                        let firstItem = g.FirstOrDefault()
                        select new EDCBankQueryResult
                        {
                            Bank = firstItem.main.Bank,
                            EDC = firstItem.main.EDC,
                            EDCFee = firstItem.edcFee,
                            UpdatedBy = firstItem.edcFee?.UpdatedBy ?? firstItem.main.EDC.UpdatedBy,
                            Updated = firstItem.edcFee?.Updated ?? firstItem.main.EDC.Updated
                        };

            #region Filter  
            if (filter.BankID != null && filter.BankID != Guid.Empty)
            {
                query = query.Where(o => o.Bank.ID == filter.BankID);
            }
            if (!string.IsNullOrEmpty(filter.BankName))
            {
                query = query.Where(o => o.Bank.NameTH.ToLower().Contains(filter.BankName.ToLower()));
            }
            if (filter.UpdatedFrom != null)
            {
                query = query.Where(x => x.Updated >= filter.UpdatedFrom);
            }
            if (filter.UpdatedTo != null)
            {
                query = query.Where(x => x.Updated <= filter.UpdatedTo);
            }


            #endregion

            var queryResults = query.ToList();

            if (!string.IsNullOrEmpty(filter.UpdatedBy))
            {
                queryResults = queryResults.Where(x => x.UpdatedBy?.DisplayName?.ToLower().Contains(filter.UpdatedBy?.ToLower()) ?? false).ToList();
            }


            EDCBankDTO.SortBy(sortByParam, ref queryResults);

            var pageOutput = PagingHelper.PagingList<EDCBankQueryResult>(pageParam, ref queryResults);

            var results = queryResults.Select(o => EDCBankDTO.CreateFromQueryResult(o)).ToList();

            return new EDCBankPaging()
            {
                PageOutput = pageOutput,
                EDCBanks = results
            };

        }

        /// <summary>
        /// ดึงข้อมูลค่าธรรมเนียมเครื่องรูดบัตร (จากธนาคารที่เลือก)
        /// UI: https://projects.invisionapp.com/d/main#/console/17482171/362367405/preview
        /// </summary>
        /// <returns>The EDCF ee list async.</returns>
        /// <param name="filter">Filter.</param>
        /// <param name="pageParam">Page parameter.</param>
        /// <param name="sortByParam">Sort by parameter.</param>
        public async Task<EDCFeePaging> GetEDCFeeListAsync(EDCFeeFilter filter, PageParam pageParam, EDCFeeSortByParam sortByParam)
        {
            IQueryable<EDCFeeQueryResult> query = DB.EDCFees.Include(o => o.UpdatedBy).Select(o =>
                                                                     new EDCFeeQueryResult
                                                                     {
                                                                         EDCFee = o,
                                                                         Bank = o.Bank,
                                                                         CreditCardPaymentType = o.CreditCardPaymentType,
                                                                         CreditCardType = o.CreditCardType,
                                                                         PaymentCardType = o.PaymentCardType
                                                                     });
            #region Filter

            if (filter.BankID != null && filter.BankID != Guid.Empty)
            {
                query = query.Where(o => o.Bank.ID == filter.BankID);
            }
            if (!string.IsNullOrEmpty(filter.PaymentCardTypeKey))
            {
                var paymentCardTypeMasterCenterID = await DB.MasterCenters.Where(x => x.Key == filter.PaymentCardTypeKey
                                                                       && x.MasterCenterGroupKey == MasterCenterGroupKeys.PaymentMethod)
                                                                      .Select(x => x.ID).FirstOrDefaultAsync();
                query = query.Where(o => o.PaymentCardType.ID == paymentCardTypeMasterCenterID);
            }
            if (!string.IsNullOrEmpty(filter.CreditCardTypeKey))
            {
                var creditCardTypeMasterCenterID = await DB.MasterCenters.Where(x => x.Key == filter.CreditCardTypeKey
                                                                       && x.MasterCenterGroupKey == MasterCenterGroupKeys.CreditCardType)
                                                                      .Select(x => x.ID).FirstOrDefaultAsync();
                query = query.Where(o => o.CreditCardType.ID == creditCardTypeMasterCenterID);
            }
            if (!string.IsNullOrEmpty(filter.CreditCardPaymentTypeKey))
            {
                var creditCardPaymentTypeMasterCenterID = await DB.MasterCenters.Where(x => x.Key == filter.CreditCardPaymentTypeKey
                                                                       && x.MasterCenterGroupKey == MasterCenterGroupKeys.CreditCardPaymentType)
                                                                      .Select(x => x.ID).FirstOrDefaultAsync();
                query = query.Where(o => o.CreditCardPaymentType.ID == creditCardPaymentTypeMasterCenterID);
            }

            if (filter.FeeFrom != null)
            {
                query = query.Where(o => o.EDCFee.Fee >= filter.FeeFrom);
            }
            if (filter.FeeTo != null)
            {
                query = query.Where(o => o.EDCFee.Fee <= filter.FeeTo);
            }
            if (filter.IsEDCBankCreditCard != null)
            {
                query = query.Where(o => o.EDCFee.IsEDCBankCreditCard == filter.IsEDCBankCreditCard);
            }
            #endregion

            var queryResults = await query.ToListAsync();

            var results = queryResults.Select(o => EDCFeeDTO.CreateFromQueryResult(o)).ToList();

            if (!string.IsNullOrEmpty(filter.CreditCardPromotionName))
            {
                results = results.Where(o => o.CreditCardPromotionName.Contains(filter.CreditCardPromotionName)).ToList();
            }

            EDCFeeDTO.SortBy(sortByParam, ref results);

            var pageOutput = PagingHelper.PagingList<EDCFeeDTO>(pageParam, ref results);

            return new EDCFeePaging()
            {
                PageOutput = pageOutput,
                EDCFees = results
            };
        }

        /// <summary>
        /// UI: https://projects.invisionapp.com/d/main#/console/17482171/362367405/preview
        /// </summary>
        /// <returns>The EDCF ee async.</returns>
        /// <param name="input">Input.</param>
        public async Task<EDCFeeDTO> CreateEDCFeeAsync(EDCFeeDTO input)
        {
            await input.ValidateAsync(DB);
            EDCFee model = new EDCFee();
            input.ToModel(ref model);
            await DB.EDCFees.AddAsync(model);
            await DB.SaveChangesAsync();

            var dataresult = await DB.EDCFees.Where(o => o.ID == model.ID)
                                             .Include(o => o.PaymentCardType)
                                             .Include(o => o.Bank)
                                             .Include(o => o.CreditCardType)
                                             .Include(o => o.CreditCardPaymentType)
                                             .FirstOrDefaultAsync();
            var result = EDCFeeDTO.CreateFromModel(dataresult);
            return result;
        }

        /// <summary>
        /// UI: https://projects.invisionapp.com/d/main#/console/17482171/362367405/preview
        /// </summary>
        /// <returns>The EDCF ee async.</returns>
        /// <param name="id">Identifier.</param>
        /// <param name="input">Input.</param>
        public async Task<EDCFeeDTO> UpdateEDCFeeAsync(Guid id, EDCFeeDTO input)
        {
            await input.ValidateAsync(DB);
            var model = await DB.EDCFees.FindAsync(id);
            input.ToModel(ref model);

            DB.Entry(model).State = EntityState.Modified;
            await DB.SaveChangesAsync();

            var dataresult = await DB.EDCFees.Where(o => o.ID == model.ID)
                                             .Include(o => o.PaymentCardType)
                                             .Include(o => o.Bank)
                                             .Include(o => o.CreditCardType)
                                             .Include(o => o.CreditCardPaymentType)
                                             .FirstOrDefaultAsync();

            var result = EDCFeeDTO.CreateFromModel(dataresult);
            return result;
        }

        /// <summary>
        /// UI: https://projects.invisionapp.com/d/main#/console/17482171/362367405/preview
        /// </summary>
        /// <returns>The EDCF ee async.</returns>
        /// <param name="id">Identifier.</param>
        public async Task DeleteEDCFeeAsync(Guid id)
        {
            var model = await DB.EDCFees.FindAsync(id);
            model.IsDeleted = true;

            DB.Entry(model).State = EntityState.Modified;
            await DB.SaveChangesAsync();
        }

        public async Task<decimal> GetFeeAsync(Guid edcID, Guid creditCardBankID, Guid creditCardTypeMasterCenterID, Guid creditCardPaymentTypeMasterCenterID, Guid paymentCardTypeMasterCenterID, decimal payAmount)
        {
            var edc = await DB.EDCs.FirstAsync(o => o.ID == edcID);
            bool isSameBank = edc.BankID == creditCardBankID;
            var fee = await DB.EDCFees.Where(o => o.BankID == edc.BankID
                                            && o.CreditCardTypeMasterCenterID == creditCardTypeMasterCenterID
                                            && o.CreditCardPaymentTypeMasterCenterID == creditCardPaymentTypeMasterCenterID
                                            && o.PaymentCardTypeMasterCenterID == paymentCardTypeMasterCenterID
                                            && o.IsEDCBankCreditCard == isSameBank
                                            )
                                            .Select(o => o.Fee)
                                            .FirstOrDefaultAsync();
            decimal feeAmount = (Convert.ToDecimal(fee) * payAmount) / 100;
            return feeAmount;
        }

        public async Task<double> GetFeePercentAsync(Guid edcID, Guid creditCardBankID, Guid creditCardTypeMasterCenterID, Guid creditCardPaymentTypeMasterCenterID, Guid paymentCardTypeMasterCenterID)
        {
            var edc = await DB.EDCs.FirstAsync(o => o.ID == edcID);
            bool isSameBank = edc.BankID == creditCardBankID;
            var fee = await DB.EDCFees.Where(o => o.BankID == edc.BankID
                                            && o.CreditCardTypeMasterCenterID == creditCardTypeMasterCenterID
                                            && o.CreditCardPaymentTypeMasterCenterID == creditCardPaymentTypeMasterCenterID
                                            && o.PaymentCardTypeMasterCenterID == paymentCardTypeMasterCenterID
                                            && o.IsEDCBankCreditCard == isSameBank
                                            )
                                            .Select(o => o.Fee)
                                            .FirstOrDefaultAsync();
            return fee;
        }

        public async Task<ReportResult> ExportEDCListUrlAsync(EDCFilter filter, ShowAs showAs)
        {
            ReportFactory reportFactory = null;
            if (showAs != null)
            {
                reportFactory = new ReportFactory(Configuration, ReportFolder.FI, "RP_FI_035", showAs);
            }
            else
            {
                reportFactory = new ReportFactory(Configuration, ReportFolder.FI, "RP_FI_035");
            }
            reportFactory.AddParameter("BankAccountID", filter.BankAccountID);
            reportFactory.AddParameter("CardMachineStatusKey", filter.CardMachineStatusKey);
            reportFactory.AddParameter("CardMachineTypeKey", filter.CardMachineTypeKey);
            reportFactory.AddParameter("Code", filter.Code);
            reportFactory.AddParameter("CompanyID", filter.CompanyID);
            reportFactory.AddParameter("ProjectID", filter.ProjectID);
            reportFactory.AddParameter("ProjectStatusKey", filter.ProjectStatusKey);
            reportFactory.AddParameter("ReceiveBy", filter.ReceiveBy);
            reportFactory.AddParameter("ReceiveDateFrom", filter.ReceiveDateFrom?.Ticks);
            reportFactory.AddParameter("ReceiveDateTo", filter.ReceiveDateTo?.Ticks);
            return reportFactory.CreateUrl();
        }

        public async Task<FileDTO> ExportExcelEDCAsync(EDCFilter filter)
        {
            ExportExcel result = new ExportExcel();
            var Data = GetEDCListAsync(filter, null, new EDCSortByParam()).Result;
            string path = Path.Combine(FileHelper.GetApplicationRootPath(), "ExcelTemplates", "TemplatesEDCExport.xlsx");
            byte[] tmp = await File.ReadAllBytesAsync(path);

            using (System.IO.MemoryStream stream = new System.IO.MemoryStream(tmp))
            using (ExcelPackage package = new ExcelPackage(stream))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets.First();
                int nRow = 2;
                foreach (var row in Data.EDCs)
                {
                    worksheet.Cells[nRow, 1].Value = row.Code ?? string.Empty;
                    worksheet.Cells[nRow, 2].Value = row.Bank.NameTH ?? string.Empty;
                    worksheet.Cells[nRow, 3].Value = row.CardMachineType.Name ?? string.Empty;
                    worksheet.Cells[nRow, 4].Value = row.BankAccount?.DisplayName; //row.ReceiveDate?.ToString("dd/MM/yyyy (HH:mm)");
                    worksheet.Cells[nRow, 5].Value = row.Company?.SAPCompanyID + "-" + row.Company?.NameTH;
                    worksheet.Cells[nRow, 6].Value = row.Project?.ProjectNo + "-" + row.Project?.ProjectNameTH;
                    worksheet.Cells[nRow, 7].Value = row.Project?.ProjectStatus?.Name;
                    worksheet.Cells[nRow, 8].Value = row.ReceiveBy;
                    worksheet.Cells[nRow, 9].Value = row.ReceiveDate?.ToString("dd/MM/yyyy");
                    worksheet.Cells[nRow, 10].Value = row.CardMachineStatus?.Name;
                    worksheet.Cells[nRow, 11].Value = row.TelNo;
                    worksheet.Cells[nRow, 12].Value = row.Updated?.ToString("dd/MM/yyyy (HH:mm)");
                    worksheet.Cells[nRow, 13].Value = row.UpdatedBy;
                    nRow++;
                }
                result.FileContent = package.GetAsByteArray();
                result.FileName = "Export_EDC" + DateTime.Now.ToString("yyyy_MM_ddTHH_mm_ss") + ".xlsx";
                result.FileType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            }
            Stream fileStream = new MemoryStream(result.FileContent);
            string fileName = result.FileName;
            string contentType = result.FileType;
            string filePath = $"Export_EDC/Export/";
            //var minioTempBucketName = Configuration["Minio:TempBucket"];
            var minioTempBucketName = Environment.GetEnvironmentVariable("minio_TempBucket");

            var uploadResult = await this.FileHelper.UploadFileFromStreamWithOutGuid(fileStream, minioTempBucketName, filePath, fileName, contentType);

            return new FileDTO()
            {
                Name = fileName,
                Url = uploadResult.Url
            };
        }
    }
}
