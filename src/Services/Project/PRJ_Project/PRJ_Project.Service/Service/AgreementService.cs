using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Base.DTOs;
using Base.DTOs.PRJ;
using Common.Helper.Logging;
using Database.Models;
using Database.Models.LOG;
using Database.Models.MasterKeys;
using Database.Models.MST;
using Database.Models.PRJ;
using Database.Models.USR;
using Microsoft.EntityFrameworkCore;
using NPOI.SS.Formula.Functions;
using PagingExtensions;
using PRJ_Project.Params.Outputs;

namespace PRJ_Project.Services
{
    public class AgreementService : IAgreementService
    {
        public LogModel logModel { get; set; }
        private readonly DatabaseContext DB;
        public AgreementService(DatabaseContext db)
        {
            logModel = new LogModel("AgreementService", null);
            this.DB = db;
        }
        public async Task<AgreementDTO> GetAgreementAsync(Guid projectID, CancellationToken cancellationToken = default)
        {
            var model = await DB.AgreementConfigs.Include(o => o.LegalEntity)
                                                .Include(o => o.UpdatedBy)
                                                .Include(o => o.AttorneyNameTransfer)
                                                .FirstOrDefaultAsync(o => o.ProjectID == projectID, cancellationToken);
            if (model is null) return null;
            var AGOwner = await DB.AgreementProjectOwners
                                  .Include(o => o.User).FirstOrDefaultAsync(o => o.ProjectID == projectID, cancellationToken);
            var KCC = await DB.KCashCardTransfer
                            .Include(o => o.ProjectOwnerByUser)
                            .Where(o => o.ProjectID == projectID).OrderBy(o => o.Seqn_No).ToListAsync(cancellationToken);
            var pdType = await DB.Projects.Include(o => o.ProductType).FirstOrDefaultAsync(o => o.ID == projectID, cancellationToken);
            var result = AgreementDTO.CreateFromModels(model, AGOwner, KCC, pdType);
            return result;
        }

        public async Task<AgreementDTO> UpdateAgreementAsync(Guid projectID, Guid id, AgreementDTO input)
        {
            await input.ValidateUpdateAsync(DB, projectID);
            var project = await DB.Projects.FirstOrDefaultAsync(o => o.ID == projectID);

            var model = await DB.AgreementConfigs
                                      .Include(o => o.LegalEntity)
                                      .Include(o => o.AttorneyNameTransfer).FirstOrDefaultAsync(x => x.ID == id && x.ProjectID == projectID);


            var IsInsertLog = false;
            if (
                (input.LicenseProductExpireDate != model.LicenseProductExpireDate) ||
                (input.LicenseProductIssueDate != model.LicenseProductIssueDate) ||
                (input.LicenseProductNo != model.LicenseProductNo) ||
                (input.LicenseProductRemark != model.LicenseProductRemark) ||
                (input.PreLicenseLandExpireDate != model.PreLicenseLandExpireDate) ||
                (input.PreLicenseLandNo != model.PreLicenseLandNo) ||
                (input.ExpectedEnvironmentalApprovalDate != model.ExpectedEnvironmentalApprovalDate) ||
                (input.CondoConstructionPermitSubmitDate != model.CondoConstructionPermitSubmitDate) ||
                (input.ExpectedPermitReceiveDate != model.ExpectedPermitReceiveDate) ||
                (input.CondoConstructionPermitNo != model.CondoConstructionPermitNo) ||
                (input.CondoConstructionPermitDate != model.CondoConstructionPermitDate) ||
                (input.CondoConstructionPermitExpireDate != model.CondoConstructionPermitExpireDate) ||
                (input.CondoConstructionPermitRemark != model.CondoConstructionPermitRemark)
            )
            {
                IsInsertLog = true;
            }
            if (IsInsertLog)
            {


                var agcLog = await DB.AgreementConfigEditLogs.Where(o => o.ProjectID == projectID).FirstOrDefaultAsync();
                if (agcLog == null)
                {
                    var addLogFirst = new AgreementConfigEditLog();
                    addLogFirst.ProjectID = projectID;
                    addLogFirst.LicenseProductExpireDate = model.LicenseProductExpireDate;
                    addLogFirst.LicenseProductIssueDate = model.LicenseProductIssueDate;
                    addLogFirst.LicenseProductNo = model.LicenseProductNo;
                    addLogFirst.LicenseProductRemark = model.LicenseProductRemark;
                    addLogFirst.PreLicenseLandExpireDate = model.PreLicenseLandExpireDate;
                    addLogFirst.PreLicenseLandNo = model.PreLicenseLandNo;
                    addLogFirst.ExpectedEnvironmentalApprovalDate = model.ExpectedEnvironmentalApprovalDate;
                    addLogFirst.CondoConstructionPermitSubmitDate = model.CondoConstructionPermitSubmitDate;
                    addLogFirst.ExpectedPermitReceiveDate = model.ExpectedPermitReceiveDate;
                    addLogFirst.CondoConstructionPermitNo = model.CondoConstructionPermitNo;
                    addLogFirst.CondoConstructionPermitDate = model.CondoConstructionPermitDate;
                    addLogFirst.CondoConstructionPermitExpireDate = model.CondoConstructionPermitExpireDate;
                    addLogFirst.CondoConstructionPermitRemark = model.CondoConstructionPermitRemark;

                    DB.AgreementConfigEditLogs.Add(addLogFirst);
                }
            }

            input.ToModel(ref model);

            var agreementDataStatusMasterCenterID = await this.AgreementDataStatus(projectID);

            project.AgreementDataStatusMasterCenterID = agreementDataStatusMasterCenterID;

            DB.Entry(model).State = EntityState.Modified;

            if (input.AGOwner != null)
            {
                var AGOwner = await DB.AgreementProjectOwners.FirstOrDefaultAsync(o => o.ProjectID == projectID);
                if (AGOwner != null)
                {
                    AGOwner.AGOwnerUserID = input.AGOwner.Id;
                    AGOwner.AGOwnerEmployeeNo = input.AGOwner.EmployeeNo;
                    AGOwner.AGOwnerName = input.AGOwner.DisplayName;
                    DB.AgreementProjectOwners.UpdateRange(AGOwner);
                }
                else
                {
                    var Projects = await DB.Projects.FirstOrDefaultAsync(o => o.ID == projectID);
                    AgreementProjectOwner AGOwners = new AgreementProjectOwner
                    {
                        AGOwnerUserID = input.AGOwner.Id,
                        AGOwnerEmployeeNo = input.AGOwner.EmployeeNo,
                        AGOwnerName = input.AGOwner.DisplayName,
                        ProjectID = projectID,
                        ProjectNo = Projects.ProjectNo,
                        ProjectName = Projects.ProjectNameTH
                    };
                    await DB.AgreementProjectOwners.AddAsync(AGOwners);
                }

            }

            if (input.KCC != null)
            {


                var Seqn_No = 0;
                foreach (var i in input.KCC)
                {
                    Guid? userID = null;

                    if (i.User.UserID != null)
                    {
                        userID = i.User.UserID;
                    }
                    else
                    {
                        User newUser = new User
                        {
                            ID = i.User.Id,
                            EmployeeNo = i.User.EmployeeNo,
                            DisplayName = i.User.DisplayName,
                            FirstName = i.User.FirstName,
                            LastName = i.User.LastName
                        };
                        await DB.Users.AddAsync(newUser);
                        await DB.SaveChangesAsync();
                        userID = newUser.ID;
                    }


                    var KCashCard = await DB.KCashCardTransfer.FirstOrDefaultAsync(o => o.ID == i.ID);
                    if (KCashCard != null)
                    {
                        KCashCard.ProjectOwnerByUserID = userID;
                        KCashCard.EmployeeNo = i.User.EmployeeNo;
                        KCashCard.DisplayName = i.User.DisplayName;
                        KCashCard.IsKCashCard = i.IsKCashCard;
                        DB.KCashCardTransfer.UpdateRange(KCashCard);
                        await DB.SaveChangesAsync();
                    }
                    else
                    {
                        KCashCardTransfer newKcc = new KCashCardTransfer
                        {
                            ProjectID = projectID,
                            Seqn_No = Seqn_No,
                            ProjectOwnerByUserID = userID,
                            EmployeeNo = i.User.EmployeeNo,
                            DisplayName = i.User.DisplayName,
                            IsKCashCard = i.IsKCashCard
                        };
                        await DB.KCashCardTransfer.AddRangeAsync(newKcc);
                        await DB.SaveChangesAsync();
                    }
                    Seqn_No += 1;
                }
            }


            if (IsInsertLog)
            {

                var addLog = new AgreementConfigEditLog();

                addLog.ProjectID = projectID;
                addLog.LicenseProductExpireDate = input.LicenseProductExpireDate;
                addLog.LicenseProductIssueDate = input.LicenseProductIssueDate;
                addLog.LicenseProductNo = input.LicenseProductNo;
                addLog.LicenseProductRemark = input.LicenseProductRemark;
                addLog.PreLicenseLandExpireDate = input.PreLicenseLandExpireDate;
                addLog.PreLicenseLandNo = input.PreLicenseLandNo;
                addLog.ExpectedEnvironmentalApprovalDate = input.ExpectedEnvironmentalApprovalDate;
                addLog.CondoConstructionPermitSubmitDate = input.CondoConstructionPermitSubmitDate;
                addLog.ExpectedPermitReceiveDate = input.ExpectedPermitReceiveDate;
                addLog.CondoConstructionPermitNo = input.CondoConstructionPermitNo;
                addLog.CondoConstructionPermitDate = input.CondoConstructionPermitDate;
                addLog.CondoConstructionPermitExpireDate = input.CondoConstructionPermitExpireDate;
                addLog.CondoConstructionPermitRemark = input.CondoConstructionPermitRemark;

                DB.AgreementConfigEditLogs.Add(addLog);
            }



            await DB.SaveChangesAsync();
            var result = AgreementDTO.CreateFromModel(model);
            return result;
        }
        private async Task<Guid> AgreementDataStatus(Guid projectID)
        {
            var model = await DB.AgreementConfigs.FirstAsync(o => o.ProjectID == projectID);
            var agreementDataStatusReadyToContractMasterCenterID = (await DB.MasterCenters.FirstAsync(o => o.MasterCenterGroupKey == "ProjectDataStatus" && o.Key == ProjectDataStatusKeys.Contract)).ID; //พร้อมทำสัญญา
            var agreementDataStatusTransferMasterCenterID = (await DB.MasterCenters.FirstAsync(o => o.MasterCenterGroupKey == "ProjectDataStatus" && o.Key == ProjectDataStatusKeys.Transfer)).ID; //พร้อมโอน
            var agreementDataStatusPrepareMasterCenterID = (await DB.MasterCenters.FirstAsync(o => o.MasterCenterGroupKey == "ProjectDataStatus" && o.Key == ProjectDataStatusKeys.Draft)).ID;  //อยู่ระหว่างจัดเตรียม
            var agreementDataStatusMasterCenterID = agreementDataStatusPrepareMasterCenterID;

            if (!string.IsNullOrEmpty(model.AttorneyNameTH1) //ผู้รับมอบอำนาจ 1 (TH)
                && !string.IsNullOrEmpty(model.AttorneyNameEN1) //ผู้รับมอบอำนาจ 1 (EN)
                && !string.IsNullOrEmpty(model.PreferApproveName) //ผู้รับมอบอำนาจขอปลอด
                )
            {
                agreementDataStatusMasterCenterID = agreementDataStatusReadyToContractMasterCenterID;
            }
            if (!string.IsNullOrEmpty(model.AttorneyNameTH1) //ผู้รับมอบอำนาจ 1 (TH)
                && !string.IsNullOrEmpty(model.AttorneyNameEN1) //ผู้รับมอบอำนาจ 1 (EN)
                && !string.IsNullOrEmpty(model.PreferApproveName) //ผู้รับมอบอำนาจขอปลอด
                && model.AttorneyNameTransfer != null //ผู้รับมอบอำนาจโอนกรรมสิทธิ์
                )
            {
                agreementDataStatusMasterCenterID = agreementDataStatusTransferMasterCenterID;
            }

            return agreementDataStatusMasterCenterID;
        }
    }
}
