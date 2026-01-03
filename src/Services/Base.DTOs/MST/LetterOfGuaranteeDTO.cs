using Base.DTOs.PRJ;
using Base.DTOs.SAL.Sortings;
using Base.DTOs.SAL;
using Database.Models;
using Database.Models.MST;
using Database.Models.PRJ;
using Database.Models.USR;
using ErrorHandling;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using models = Database.Models;
using Base.DTOs.USR;
using FileStorage;

namespace Base.DTOs.MST
{
    public class LetterOfGuaranteeDTO : BaseDTO
    {
        public DateTime? IssueDate { get; set; }
        public DateTime? ExpireDate { get; set; }
        public string MeterNumber { get; set; }
        public bool? IsJuristicSetup { get; set; }
        public DateTime? JuristicSetupDate { get; set; }
        public string JuristicSetupBy { get; set; }
        public string JuristicSetupRemarks { get; set; }
        public BankDTO Bank { get; set; }
        public CompanyDTO Company { get; set; }
        public string CostCenter { get; set; }
        public ProjectDropdownDTO Project { get; set; }
        public double? ProjectArea { get; set; }
        public string LetterOfGuaranteeNo { get; set; }
        public MasterCenterDropdownDTO LGGuarantor { get; set; }
        public MasterCenterDropdownDTO LGType { get; set; }
        public decimal? IssueAmount { get; set;}
        public decimal? RefundAmount { get; set;}
        public decimal? RemainAmount { get; set;}
        public MasterCenterDropdownDTO LGGuaranteeConditions {get; set;}
        public string Remark { get; set;}
        public DateTime? EffectiveDate { get; set; }
        public int? ConditionCalFee { get; set; }
        public double? FeeRate { get; set; }
        
        public DateTime? ExpiredPeriodDate { get; set; }
        public decimal? FeeRateAmountByPeriod { get; set; }

        public UserListDTO CreatedBy { get; set; }
        public DateTime? Created { get; set; }
        public UserListDTO UpdatedBy { get; set; }
        public DateTime? Updated { get; set; }

        public bool? IsCancel { get; set; }
        public DateTime? CancelDate { get; set; }
        public UserListDTO CancelBy { get; set; }
        public string CancelRemark { get; set; }
        public FileDTO AttachFile { get; set; }

        public MasterCenterDropdownDTO LGSubType { get; set; }

        public static LetterOfGuaranteeDTO CreateFromModel(LetterGuarantee model, DatabaseContext db)
        {
            if (model != null)
            {
                var CancelBy = db.Users.Where(o => o.ID == model.CancelByUserID).FirstOrDefault();
                
                var result = new LetterOfGuaranteeDTO()
                {
                    Id = model.ID,
                    IssueDate = model.IssueDate,
                    ExpireDate = model.ExpiredDate,
                    MeterNumber = model.MeterNumber,
                    IsJuristicSetup = model.IsJuristicSetup,
                    JuristicSetupDate = model.JuristicSetupDate,
                    JuristicSetupBy = model.JuristicSetupBy,
                    JuristicSetupRemarks = model.JuristicSetupRemarks,
                    Bank = BankDTO.CreateFromModel(model.Banks),
                    Company = CompanyDTO.CreateFromModel(model.Company),
                    CostCenter = model.CostCenter,
                    Project = ProjectDropdownDTO.CreateFromModel(model.Project),
                    ProjectArea = model.ProjectArea,
                    LetterOfGuaranteeNo = model.LetterOfGuaranteeNo,
                    LGGuarantor = MasterCenterDropdownDTO.CreateFromModel(model.LGGuarantor),
                    LGType = MasterCenterDropdownDTO.CreateFromModel(model.LGType),
                    IssueAmount = model.IssueAmount,
                    RefundAmount = model.RefundAmount,
                    RemainAmount = model.RemainAmount,
                    LGGuaranteeConditions = MasterCenterDropdownDTO.CreateFromModel(model.LGGuaranteeConditions),
                    Remark = model.Remark,
                    EffectiveDate = model.EffectiveDate,
                    ExpiredPeriodDate = model.ExpiredPeriodDate,
                    ConditionCalFee = model.ConditionCalFee,
                    FeeRate = model.FeeRate,
                    FeeRateAmountByPeriod = model.FeeRateAmountByPeriod,
                    Created = model.Created,
                    CreatedBy = UserListDTO.CreateFromModel(model.CreatedBy),
                    Updated = model.Updated,
                    UpdatedBy = UserListDTO.CreateFromModel(model.UpdatedBy),
                    IsCancel = model.IsCanceled,
                    CancelDate = model.CancelDate,
                    CancelBy = UserListDTO.CreateFromModel(CancelBy),
                    CancelRemark = model.CancelRemark,
                    LGSubType = MasterCenterDropdownDTO.CreateFromModel(model.LGSubType),

                };
                return result;
            }
            else
            {
                return null;
            }
        }

        public static LetterOfGuaranteeDTO CreateFromModels(LetterGuarantee model, DatabaseContext db )
        {
            if (model != null)
            {
                var CancelBy = db.Users.Where(o => o.ID == model.CancelByUserID).FirstOrDefault();

                var result = new LetterOfGuaranteeDTO()
                {
                    Id = model.ID,
                    IssueDate = model.IssueDate,
                    ExpireDate = model.ExpiredDate,
                    MeterNumber = model.MeterNumber,
                    IsJuristicSetup = model.IsJuristicSetup,
                    JuristicSetupDate = model.JuristicSetupDate,
                    JuristicSetupBy = model.JuristicSetupBy,
                    JuristicSetupRemarks = model.JuristicSetupRemarks,
                    Bank = BankDTO.CreateFromModel(model.Banks),
                    Company = CompanyDTO.CreateFromModel(model.Company),
                    CostCenter = model.CostCenter,
                    Project = ProjectDropdownDTO.CreateFromModel(model.Project),
                    ProjectArea = model.ProjectArea,
                    LetterOfGuaranteeNo = model.LetterOfGuaranteeNo,
                    LGGuarantor = MasterCenterDropdownDTO.CreateFromModel(model.LGGuarantor),
                    LGType = MasterCenterDropdownDTO.CreateFromModel(model.LGType),
                    IssueAmount = model.IssueAmount,
                    RefundAmount = model.RefundAmount,
                    RemainAmount = model.RemainAmount,
                    LGGuaranteeConditions = MasterCenterDropdownDTO.CreateFromModel(model.LGGuaranteeConditions),
                    Remark = model.Remark,
                    EffectiveDate = model.EffectiveDate,
                    ExpiredPeriodDate = model.ExpiredPeriodDate,
                    ConditionCalFee = model.ConditionCalFee,
                    FeeRate = model.FeeRate,
                    FeeRateAmountByPeriod = model.FeeRateAmountByPeriod,
                    Created = model.Created,
                    CreatedBy = UserListDTO.CreateFromModel(model.CreatedBy),
                    Updated = model.Updated,
                    UpdatedBy = UserListDTO.CreateFromModel(model.UpdatedBy),
                    IsCancel = model.IsCanceled,
                    CancelDate = model.CancelDate,
                    CancelBy = UserListDTO.CreateFromModel(CancelBy),
                    CancelRemark = model.CancelRemark,
                    LGSubType = MasterCenterDropdownDTO.CreateFromModel(model.LGSubType),
                };
                return result;
            }
            else
            {
                return null;
            }
        }

        public static LetterOfGuaranteeDTO CreateFromQueryResult(LetterOfGuaranteeQueryResult model, DatabaseContext db)
        {
            if (model != null)
            {
                var file = db.LetterGuaranteeFiles.Where(o => o.LetterGuaranteeID == model.LetterOfGuarantee.ID).FirstOrDefault();
                var CancelBy = db.Users.Where(o => o.ID == model.LetterOfGuarantee.CancelByUserID).FirstOrDefault();
                LetterOfGuaranteeDTO result = new LetterOfGuaranteeDTO()
                {
                    Id = model.LetterOfGuarantee.ID,
                    IssueDate = model.LetterOfGuarantee.IssueDate,
                    ExpireDate = model.LetterOfGuarantee.ExpiredDate,
                    MeterNumber = model.LetterOfGuarantee.MeterNumber,
                    IsJuristicSetup = model.LetterOfGuarantee.IsJuristicSetup,
                    JuristicSetupDate = model.LetterOfGuarantee.JuristicSetupDate,
                    JuristicSetupBy = model.LetterOfGuarantee.JuristicSetupBy,
                    JuristicSetupRemarks = model.LetterOfGuarantee.JuristicSetupRemarks,
                    Bank = BankDTO.CreateFromModel(model.Bank),
                    Company = CompanyDTO.CreateFromModel(model.Company),
                    CostCenter = model.LetterOfGuarantee.CostCenter,
                    Project = ProjectDropdownDTO.CreateFromModel(model.Project),
                    ProjectArea = model.LetterOfGuarantee.ProjectArea,
                    LetterOfGuaranteeNo = model.LetterOfGuarantee.LetterOfGuaranteeNo,
                    LGGuarantor = MasterCenterDropdownDTO.CreateFromModel(model.LetterOfGuarantee.LGGuarantor),
                    LGType = MasterCenterDropdownDTO.CreateFromModel(model.LetterOfGuarantee.LGType),
                    IssueAmount = model.LetterOfGuarantee.IssueAmount,
                    RefundAmount = model.LetterOfGuarantee.RefundAmount,
                    RemainAmount = model.LetterOfGuarantee.RemainAmount,
                    LGGuaranteeConditions = MasterCenterDropdownDTO.CreateFromModel(model.LetterOfGuarantee.LGGuaranteeConditions),
                    Remark = model.LetterOfGuarantee.Remark,
                    EffectiveDate = model.LetterOfGuarantee.EffectiveDate,
                    ExpiredPeriodDate = model.LetterOfGuarantee.ExpiredPeriodDate,
                    ConditionCalFee = model.LetterOfGuarantee.ConditionCalFee,
                    FeeRate = model.LetterOfGuarantee.FeeRate,
                    FeeRateAmountByPeriod = model.LetterOfGuarantee.FeeRateAmountByPeriod,
                    Created = model.LetterOfGuarantee.Created,
                    CreatedBy = UserListDTO.CreateFromModel(model.LetterOfGuarantee.CreatedBy),
                    Updated = model.LetterOfGuarantee.Updated,
                    UpdatedBy = UserListDTO.CreateFromModel(model.LetterOfGuarantee.UpdatedBy),
                    IsCancel = model.LetterOfGuarantee.IsCanceled,
                    CancelDate = model.LetterOfGuarantee.CancelDate,
                    CancelBy = UserListDTO.CreateFromModel(CancelBy),
                    CancelRemark = model.LetterOfGuarantee.CancelRemark,
                    LGSubType = MasterCenterDropdownDTO.CreateFromModel(model.LGSubType),
                };
                return result;
            }
            else
            {
                return null;
            }
        }

        public static void SortBy(LetterOfGuaranteeSortByParam sortByParam, ref IQueryable<LetterOfGuaranteeQueryResult> query)
        {
            if (sortByParam.SortBy != null)
            {
                switch (sortByParam.SortBy.Value)
                {
                    case LetterOfGuaranteeSortBy.Project:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.LetterOfGuarantee.Project.ProjectNo);
                        else query = query.OrderByDescending(o => o.LetterOfGuarantee.Project.ProjectNo);
                        break;
                    case LetterOfGuaranteeSortBy.IssueDate:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.LetterOfGuarantee.IssueDate);
                        else query = query.OrderByDescending(o => o.LetterOfGuarantee.IssueDate);
                        break;
                    case LetterOfGuaranteeSortBy.ExpiredDate:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.LetterOfGuarantee.ExpiredDate);
                        else query = query.OrderByDescending(o => o.LetterOfGuarantee.ExpiredDate);
                        break;
                    case LetterOfGuaranteeSortBy.MeterNumber:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.LetterOfGuarantee.MeterNumber);
                        else query = query.OrderByDescending(o => o.LetterOfGuarantee.MeterNumber);
                        break;
                    case LetterOfGuaranteeSortBy.IsJuristicSetup:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.LetterOfGuarantee.IsJuristicSetup);
                        else query = query.OrderByDescending(o => o.LetterOfGuarantee.IsJuristicSetup);
                        break;
                    case LetterOfGuaranteeSortBy.JuristicSetupDate:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.LetterOfGuarantee.JuristicSetupDate);
                        else query = query.OrderByDescending(o => o.LetterOfGuarantee.JuristicSetupDate);
                        break;
                    case LetterOfGuaranteeSortBy.JuristicSetupBy:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.LetterOfGuarantee.JuristicSetupBy);
                        else query = query.OrderByDescending(o => o.LetterOfGuarantee.JuristicSetupBy);
                        break;
                    case LetterOfGuaranteeSortBy.JuristicSetupRemark:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.LetterOfGuarantee.JuristicSetupRemarks);
                        else query = query.OrderByDescending(o => o.LetterOfGuarantee.JuristicSetupRemarks);
                        break;
                    case LetterOfGuaranteeSortBy.Bank:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.LetterOfGuarantee.Banks.NameTH);
                        else query = query.OrderByDescending(o => o.LetterOfGuarantee.Banks.NameTH);
                        break;
                    case LetterOfGuaranteeSortBy.Company:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.LetterOfGuarantee.Company.Code);
                        else query = query.OrderByDescending(o => o.LetterOfGuarantee.Company.Code);
                        break;
                    case LetterOfGuaranteeSortBy.CostCenter:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.LetterOfGuarantee.CostCenter);
                        else query = query.OrderByDescending(o => o.LetterOfGuarantee.CostCenter);
                        break;
                    case LetterOfGuaranteeSortBy.ProjectArea:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.LetterOfGuarantee.ProjectArea);
                        else query = query.OrderByDescending(o => o.LetterOfGuarantee.Project);
                        break;
                    case LetterOfGuaranteeSortBy.LetterOfGuaranteeNo:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.LetterOfGuarantee.LetterOfGuaranteeNo);
                        else query = query.OrderByDescending(o => o.LetterOfGuarantee.LetterOfGuaranteeNo);
                        break;
                    case LetterOfGuaranteeSortBy.LGGuarantor:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.LetterOfGuarantee.LGGuarantor.Name);
                        else query = query.OrderByDescending(o => o.LetterOfGuarantee.LGGuarantor.Name);
                        break;
                    case LetterOfGuaranteeSortBy.LGType:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.LetterOfGuarantee.LGType.Name);
                        else query = query.OrderByDescending(o => o.LetterOfGuarantee.LGType.Name);
                        break;
                    case LetterOfGuaranteeSortBy.IssueAmount:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.LetterOfGuarantee.IssueAmount);
                        else query = query.OrderByDescending(o => o.LetterOfGuarantee.IssueAmount);
                        break;
                    case LetterOfGuaranteeSortBy.RefundAmount:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.LetterOfGuarantee.RefundAmount);
                        else query = query.OrderByDescending(o => o.LetterOfGuarantee.RefundAmount);
                        break;
                    case LetterOfGuaranteeSortBy.RemainAmount:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.LetterOfGuarantee.RemainAmount);
                        else query = query.OrderByDescending(o => o.LetterOfGuarantee.RemainAmount);
                        break;
                    case LetterOfGuaranteeSortBy.Status:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.LetterOfGuarantee.IsCanceled);
                        else query = query.OrderByDescending(o => o.LetterOfGuarantee.IsCanceled);
                        break;
                    case LetterOfGuaranteeSortBy.LGGuaranteeConditions:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.LetterOfGuarantee.LGGuaranteeConditions.Name);
                        else query = query.OrderByDescending(o => o.LetterOfGuarantee.LGGuaranteeConditions.Name);
                        break;
                    case LetterOfGuaranteeSortBy.Remark:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.LetterOfGuarantee.Remark);
                        else query = query.OrderByDescending(o => o.LetterOfGuarantee.Remark);
                        break;
                    case LetterOfGuaranteeSortBy.EffectiveDate:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.LetterOfGuarantee.EffectiveDate);
                        else query = query.OrderByDescending(o => o.LetterOfGuarantee.EffectiveDate);
                        break;
                    case LetterOfGuaranteeSortBy.ExpiredPeriodDate:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.LetterOfGuarantee.ExpiredPeriodDate);
                        else query = query.OrderByDescending(o => o.LetterOfGuarantee.ExpiredPeriodDate);
                        break;
                    case LetterOfGuaranteeSortBy.ConditionCalFee:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.LetterOfGuarantee.ConditionCalFee);
                        else query = query.OrderByDescending(o => o.LetterOfGuarantee.ConditionCalFee);
                        break;
                    case LetterOfGuaranteeSortBy.FeeRate:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.LetterOfGuarantee.FeeRate);
                        else query = query.OrderByDescending(o => o.LetterOfGuarantee.FeeRate);
                        break;
                    case LetterOfGuaranteeSortBy.FeeRateAmountByPeriod:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.LetterOfGuarantee.FeeRateAmountByPeriod);
                        else query = query.OrderByDescending(o => o.LetterOfGuarantee.FeeRateAmountByPeriod);
                        break;
                    case LetterOfGuaranteeSortBy.LGSubType:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.LetterOfGuarantee.LGSubType.Name);
                        else query = query.OrderByDescending(o => o.LetterOfGuarantee.LGSubType.Name);
                        break;
                }
            }
            else
            {
                query = query.OrderByDescending(o => o.LetterOfGuarantee.IssueDate).ThenBy(o => o.Project.ProjectNo);
            }
        }

        public class LetterOfGuaranteeQueryResult
        {
            public models.PRJ.LetterGuarantee LetterOfGuarantee { get; set; }
            public models.MST.Bank Bank { get; set; }
            public models.MST.Company Company { get; set; }
            public models.PRJ.Project Project { get; set; }
            public models.MST.MasterCenter LGGuarantor { get; set; }
            public models.MST.MasterCenter LGType { get; set; }
            public models.MST.MasterCenter LGGuaranteeCondetions { get; set; }
            public models.USR.User CancelBy { get; set; }
            public models.USR.User CreatedBy { get; set; }
            public models.USR.User UpdatedBy { get; set; }
            public models.MST.MasterCenter LGSubType { get; set; }
        }
    }
}
