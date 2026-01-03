using Base.DTOs.SAL;
using Database.Models;
using Database.Models.CMS;
using Database.Models.DbQueries.CMS;
using Database.Models.USR;
using ErrorHandling;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using models = Database.Models;

namespace Base.DTOs.CMS
{
    public class ChangeLCSaleDTO : BaseDTO
    {
        /// <summary>
        /// โครงการ
        /// </summary>
        [Description("โครงการ")]
        public PRJ.ProjectDropdownDTO Project { get; set; }

        /// <summary>
        /// แปลง/ห้อง
        /// </summary>
        [Description("แปลง/ห้อง")]
        public PRJ.UnitDropdownDTO Unit { get; set; }

        /*
        /// <summary>
        /// เลขที่จอง
        /// </summary>
        [Description("เลขที่จอง")]
        public string BookingNo { get; set; }
        */

        /// <summary>
        /// เลขที่สัญญา
        /// </summary>
        [Description("เลขที่สัญญา")]
        public SAL.AgreementDropdownDTO Agreement { get; set; }

        /// <summary>
        /// ชื่อลูกค้า
        /// </summary>
        [Description("ชื่อลูกค้า")]
        public SAL.AgreementOwnerDropdownDTO CustomerName { get; set; }

        /// <summary>
        /// Effective Date
        /// </summary>
        [Description("Effective Date")]
        public DateTime? ActiveDate { get; set; }


        /// <summary>
        /// ประเภทพนักงานปิดการขาย (เดิม)
        /// GET Master/api/MasterCenters?MasterCenterGroupKey=SaleOfficerType
        /// </summary>
        [Description("ประเภทพนักงานปิดการขาย (เดิม)")]
        public MST.MasterCenterDropdownDTO OldSaleOfficerType { get; set; }
        /// <summary>
        /// รหัส Sale (เดิม)
        /// GET Identity/api/Users?roleCodes=LC&authorizeProjectIDs={projectID}
        /// </summary>
        [Description("รหัส Sale (เดิม)")]
        public USR.UserListDTO OldSaleUser { get; set; }
        /// <summary>
        /// รหัส Agent (เดิม)
        /// GET Master/api/Agents/DropdownList
        /// </summary>
        [Description("รหัส Agent (เดิม)")]
        public MST.AgentDropdownDTO OldAgent { get; set; }
        /// <summary>
        /// รหัสพนักงาน Agent (เดิม)
        /// GET Master/api/AgentEmployees/DropdownList?agentID={agentID}
        /// </summary>
        [Description("รหัสพนักงาน Agent (เดิม)")]
        public MST.AgentEmployeeDropdownDTO OldAgentEmployee { get; set; }
        /// <summary>
        /// รหัส Sale ประจำโครงการ (เดิม)
        /// GET Identity/api/Users?roleCodes=LC&authorizeProjectIDs={projectID}
        /// </summary>
        [Description("รหัส Sale ประจำโครงการ (เดิม)")]
        public USR.UserListDTO OldProjectSaleUser { get; set; }


        /// <summary>
        /// ประเภทพนักงานปิดการขาย (ใหม่)
        /// GET Master/api/MasterCenters?MasterCenterGroupKey=SaleOfficerType
        /// </summary>
        [Description("ประเภทพนักงานปิดการขาย (ใหม่)")]
        public MST.MasterCenterDropdownDTO NewSaleOfficerType { get; set; }
        /// <summary>
        /// รหัส Sale (ใหม่)
        /// GET Identity/api/Users?roleCodes=LC&authorizeProjectIDs={projectID}
        /// </summary>
        [Description("รหัส Sale (ใหม่)")]
        public USR.UserListDTO NewSaleUser { get; set; }
        /// <summary>
        /// รหัส Agent (ใหม่)
        /// GET Master/api/Agents/DropdownList
        /// </summary>
        [Description("รหัส Agent (ใหม่)")]
        public MST.AgentDropdownDTO NewAgent { get; set; }
        /// <summary>
        /// รหัสพนักงาน Agent (ใหม่)
        /// GET Master/api/AgentEmployees/DropdownList?agentID={agentID}
        /// </summary>
        [Description("รหัสพนักงาน Agent (ใหม่)")]
        public MST.AgentEmployeeDropdownDTO NewAgentEmployee { get; set; }
        /// <summary>
        /// รหัส Sale ประจำโครงการ (ใหม่)
        /// GET Identity/api/Users?roleCodes=LC&authorizeProjectIDs={projectID}
        /// </summary>
        [Description("รหัส Sale ประจำโครงการ (ใหม่)")]
        public USR.UserListDTO NewProjectSaleUser { get; set; }

        /// <summary>
        /// หมายเหตุ
        /// </summary>
        [Description("หมายเหตุ")]
        public string Remark { get; set; }

        /// <summary>
        /// ผู้สร้าง
        /// GET Identity/api/Users?roleCodes=LC&authorizeProjectIDs={projectID}
        /// </summary>
        [Description("ผู้สร้าง")]
        public USR.UserListDTO CreatedByUser { get; set; }

        /// <summary>
        /// วันที่สร้าง
        /// </summary>
        [Description("วันที่สร้าง")]
        public DateTime? CreateDate { get; set; }

        public static ChangeLCSaleDTO CreateFromModel(ChangeLCSale model)
        {
            if (model != null)
            {
                var result = new ChangeLCSaleDTO()
                {
                    Id = model.ID,
                    Project = PRJ.ProjectDropdownDTO.CreateFromModel(model.Agreement.Project),
                    Unit = PRJ.UnitDropdownDTO.CreateFromModel(model.Agreement.Unit),
                    Agreement = SAL.AgreementDropdownDTO.CreateFromModel(model.Agreement),
                    //CustomerName = SAL.AgreementOwnerDropdownDTO.CreateFromModel(model.),
                    ActiveDate = model.ActiveDate,
                    OldSaleOfficerType = MST.MasterCenterDropdownDTO.CreateFromModel(model.OldSaleOfficerType),
                    OldSaleUser = USR.UserListDTO.CreateFromModel(model.OldSaleUser),
                    OldAgent = MST.AgentDropdownDTO.CreateFromModel(model.OldAgent),
                    OldAgentEmployee = MST.AgentEmployeeDropdownDTO.CreateFromModel(model.OldAgentEmployee),
                    OldProjectSaleUser = USR.UserListDTO.CreateFromModel(model.OldProjectSaleUser),
                    NewSaleOfficerType = MST.MasterCenterDropdownDTO.CreateFromModel(model.NewSaleOfficerType),
                    NewSaleUser = USR.UserListDTO.CreateFromModel(model.NewSaleUser),
                    NewAgent = MST.AgentDropdownDTO.CreateFromModel(model.NewAgent),
                    NewAgentEmployee = MST.AgentEmployeeDropdownDTO.CreateFromModel(model.NewAgentEmployee),
                    NewProjectSaleUser = USR.UserListDTO.CreateFromModel(model.NewProjectSaleUser),
                    CreatedByUser = USR.UserListDTO.CreateFromModel(model.CreatedBy),
                    CreateDate = model.Created
                };
                return result;
            }
            else
            {
                return null;
            }
        }

        public static ChangeLCSaleDTO CreateFromQueryResult(ChangeLCSaleQueryResult model)
        {
            if (model != null)
            {
                var result = new ChangeLCSaleDTO()
                {
                    Id = model.ChangeLCSale?.ID,
                    Project = PRJ.ProjectDropdownDTO.CreateFromModel(model.Agreement?.Project),
                    Unit = PRJ.UnitDropdownDTO.CreateFromModel(model.Agreement?.Unit),
                    Agreement = SAL.AgreementDropdownDTO.CreateFromModel(model.Agreement),
                    CustomerName = SAL.AgreementOwnerDropdownDTO.CreateFromModel(model.AgreementOwner),
                    ActiveDate = model.ChangeLCSale?.ActiveDate ?? DateTime.Now,
                    OldSaleOfficerType = MST.MasterCenterDropdownDTO.CreateFromModel(model?.OldSaleOfficerType),
                    OldSaleUser = USR.UserListDTO.CreateFromModel(model?.OldSaleUser),
                    OldAgent = MST.AgentDropdownDTO.CreateFromModel(model?.OldAgent),
                    OldAgentEmployee = MST.AgentEmployeeDropdownDTO.CreateFromModel(model?.OldAgentEmployee),
                    OldProjectSaleUser = USR.UserListDTO.CreateFromModel(model?.OldProjectSaleUser),
                    NewSaleOfficerType = MST.MasterCenterDropdownDTO.CreateFromModel(model.NewSaleOfficerType),
                    NewSaleUser = USR.UserListDTO.CreateFromModel(model.NewSaleUser),
                    NewAgent = MST.AgentDropdownDTO.CreateFromModel(model.NewAgent),
                    NewAgentEmployee = MST.AgentEmployeeDropdownDTO.CreateFromModel(model.NewAgentEmployee),
                    NewProjectSaleUser = USR.UserListDTO.CreateFromModel(model.NewProjectSaleUser),
                    CreatedByUser = USR.UserListDTO.CreateFromModel(model?.CreatedByUser),
                    CreateDate = model.ChangeLCSale?.Created
                };

                return result;
            }
            else
            {
                return null;
            }
        }

        public static void SortBy(ChangeLCSaleSortByParam sortByParam, ref IQueryable<ChangeLCSaleQueryResult> query)
        {
            IOrderedQueryable<ChangeLCSaleQueryResult> orderQuery;
            if (sortByParam.SortBy != null)
            {
                switch (sortByParam.SortBy.Value)
                {
                    case ChangeLCSaleSortBy.ProjectID:
                        if (sortByParam.Ascending) orderQuery = query.OrderBy(o => o.ChangeLCSale.Agreement.ProjectID);
                        else orderQuery = query.OrderByDescending(o => o.ChangeLCSale.Agreement.ProjectID);
                        break;
                    case ChangeLCSaleSortBy.Unit:
                        if (sortByParam.Ascending) orderQuery = query.OrderBy(o => o.ChangeLCSale.Agreement.Unit.UnitNo);
                        else orderQuery = query.OrderByDescending(o => o.ChangeLCSale.Agreement.Unit.UnitNo);
                        break;
                    case ChangeLCSaleSortBy.Agreement:
                        if (sortByParam.Ascending) orderQuery = query.OrderBy(o => o.Agreement.AgreementNo);
                        else orderQuery = query.OrderByDescending(o => o.Agreement.AgreementNo);
                        break;
                    case ChangeLCSaleSortBy.CustomerName:
                        if (sortByParam.Ascending) orderQuery = query.OrderBy(o => o.AgreementOwner.FirstNameTH).ThenBy(o => o.AgreementOwner.LastNameTH);
                        else orderQuery = query.OrderByDescending(o => o.AgreementOwner.FirstNameTH).ThenBy(o => o.AgreementOwner.LastNameTH);
                        break;
                    case ChangeLCSaleSortBy.ActiveDate:
                        if (sortByParam.Ascending) orderQuery = query.OrderBy(o => o.ChangeLCSale.ActiveDate);
                        else orderQuery = query.OrderByDescending(o => o.ChangeLCSale.ActiveDate);
                        break;
                    case ChangeLCSaleSortBy.OldSaleOfficerType:
                        if (sortByParam.Ascending) orderQuery = query.OrderBy(o => o.ChangeLCSale.OldSaleOfficerType.Name);
                        else orderQuery = query.OrderByDescending(o => o.ChangeLCSale.OldSaleOfficerType.Name);
                        break;
                    case ChangeLCSaleSortBy.OldAgent:
                        if (sortByParam.Ascending) orderQuery = query.OrderBy(o => o.ChangeLCSale.OldAgent.NameTH);
                        else orderQuery = query.OrderByDescending(o => o.ChangeLCSale.OldAgent.NameTH);
                        break;
                    case ChangeLCSaleSortBy.OldAgentEmployee:
                        if (sortByParam.Ascending) orderQuery = query.OrderBy(o => o.ChangeLCSale.OldAgentEmployee.FirstName);
                        else orderQuery = query.OrderByDescending(o => o.ChangeLCSale.OldAgentEmployee.FirstName);
                        break;
                    case ChangeLCSaleSortBy.OldSaleUser:
                        if (sortByParam.Ascending) orderQuery = query.OrderBy(o => o.ChangeLCSale.OldSaleUser.FirstName);
                        else orderQuery = query.OrderByDescending(o => o.ChangeLCSale.OldSaleUser.FirstName);
                        break;
                    case ChangeLCSaleSortBy.OldProjectSaleUser:
                        if (sortByParam.Ascending) orderQuery = query.OrderBy(o => o.ChangeLCSale.OldProjectSaleUser.FirstName);
                        else orderQuery = query.OrderByDescending(o => o.ChangeLCSale.OldProjectSaleUser.FirstName);
                        break;
                    default:
                        orderQuery = query.OrderBy(o => o.ChangeLCSale.ActiveDate);
                        break;
                }
            }
            else
            {
                orderQuery = query.OrderBy(o => o.ChangeLCSale.ActiveDate);
            }

            orderQuery.ThenBy(o => o.ChangeLCSale.ID);
            query = orderQuery;
        }

        public async Task ValidateAsync(DatabaseContext db)
        {
            ValidateException ex = new ValidateException();
            if (!this.ActiveDate.HasValue)
            {
                var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                string desc = this.GetType().GetProperty(nameof(ChangeLCSaleDTO.ActiveDate)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = errMsg.Message.Replace("[field]", desc);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }
            if (this.Project == null)
            {
                var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                string desc = this.GetType().GetProperty(nameof(ChangeLCSaleDTO.Project)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = errMsg.Message.Replace("[field]", desc);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }
            if (this.Agreement == null)
            {
                var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                string desc = this.GetType().GetProperty(nameof(ChangeLCSaleDTO.Agreement)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = errMsg.Message.Replace("[field]", desc);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }
            if (this.NewSaleOfficerType == null)
            {
                var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                string desc = this.GetType().GetProperty(nameof(ChangeLCSaleDTO.NewSaleOfficerType)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = errMsg.Message.Replace("[field]", desc);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }
            if (this.NewProjectSaleUser == null)
            {
                var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                string desc = this.GetType().GetProperty(nameof(ChangeLCSaleDTO.NewProjectSaleUser)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = errMsg.Message.Replace("[field]", desc);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }

            if (ex.HasError)
            {
                throw ex;
            }
        }

        public void ToModel(ref ChangeLCSale model)
        {
            model.ActiveDate = this.ActiveDate;
            model.AgreementID = this.Agreement.Id;

            model.OldSaleOfficerTypeMasterCenterID = this.OldSaleOfficerType?.Id;
            model.OldAgentID = this.OldAgent?.Id;
            model.OldAgentEmployeeID = this.OldAgentEmployee?.Id;
            model.OldSaleUserID = this.OldSaleUser?.Id;
            model.OldProjectSaleUserID = this.OldProjectSaleUser?.Id;
            model.NewSaleOfficerTypeMasterCenterID = this.NewSaleOfficerType.Id;

            //AP
            if (this.NewSaleOfficerType.Id.ToString().ToUpper() == "5D4C85D9-4DB1-4972-8924-262001DB4EC0")
            {
                model.NewAgentID = null;
                model.NewAgentEmployeeID = null;
                model.NewSaleUserID = this.NewSaleUser?.Id;
                model.NewProjectSaleUserID = this.NewProjectSaleUser.Id;
            }
            //Agency
            else if (this.NewSaleOfficerType.Id.ToString().ToUpper() == "0CF1524F-E0D7-4E42-B8C4-2310F5A2BCB0")
            {
                model.NewAgentID = this.NewAgent?.Id;
                model.NewAgentEmployeeID = this.NewAgentEmployee?.Id;
                model.NewSaleUserID = null;
                model.NewProjectSaleUserID = this.NewProjectSaleUser.Id;
            }
            //AP (Agency Referal)
            else if (this.NewSaleOfficerType.Id.ToString().ToUpper() == "919A75D2-7784-45A7-A9E6-695EA9ADFBAA")
            {
                model.NewAgentID = this.NewAgent?.Id;
                model.NewAgentEmployeeID = null;
                model.NewSaleUserID = this.NewSaleUser?.Id;
                model.NewProjectSaleUserID = this.NewProjectSaleUser.Id;
            }

            model.Remark = this.Remark;
        }

        public static ChangeLCSaleDTO CreateFromQuery(dbqChangeLCSaleList model, DatabaseContext db)
        {
            if (model != null)
            {
                var modelProject = db.Projects.Where(e => e.ID == model.ProjectID).FirstOrDefault();
                //var modelUnit = db.Units.Where(e => e.ID == model.UnitID).FirstOrDefault();
                var modelAgreement = db.Agreements.Include(o => o.Booking).Where(e => e.ID == model.AgreementID).FirstOrDefault();
                var modelAgreementOwner = db.AgreementOwners.Where(e => e.ID == model.AgreementOwnerID).FirstOrDefault();
                var modelOldSaleUser = db.Users.IgnoreQueryFilters().Where(e => e.ID == model.OldSaleUserID).FirstOrDefault();
                var modelOldProjectSaleUser = db.Users.IgnoreQueryFilters().Where(e => e.ID == model.OldProjectSaleUserID).FirstOrDefault();
                var modelOldSaleOfficerType = db.MasterCenters.Where(e => e.ID == model.OldSaleOfficerTypeMasterCenterID).FirstOrDefault();
                var modelOldAgent = db.Agents.IgnoreQueryFilters().Where(e => e.ID == model.OldAgentID).FirstOrDefault();
                var modelOldAgentEmployee = db.AgentEmployees.IgnoreQueryFilters().Where(e => e.ID == model.OldAgentEmployeeID).FirstOrDefault();

                var result = new ChangeLCSaleDTO()
                {
                    Id = model.AgreementID,
                    Project = PRJ.ProjectDropdownDTO.CreateFromModel(modelProject),
                    Unit = new PRJ.UnitDropdownDTO { Id = model.UnitID ?? new Guid(), UnitNo = model.UnitNo },
                    Agreement = SAL.AgreementDropdownDTO.CreateFromModel(modelAgreement), 
                    CustomerName = SAL.AgreementOwnerDropdownDTO.CreateFromModel(modelAgreementOwner),
                    ActiveDate = model.ActiveDate ?? DateTime.Now,
                    OldSaleOfficerType = MST.MasterCenterDropdownDTO.CreateFromModel(modelOldSaleOfficerType),
                    OldSaleUser = USR.UserListDTO.CreateFromModel(modelOldSaleUser),
                    OldAgent = MST.AgentDropdownDTO.CreateFromModel(modelOldAgent),
                    OldAgentEmployee = MST.AgentEmployeeDropdownDTO.CreateFromModel(modelOldAgentEmployee),
                    OldProjectSaleUser = USR.UserListDTO.CreateFromModel(modelOldProjectSaleUser),
                    //NewSaleOfficerType = MST.MasterCenterDropdownDTO.CreateFromModel(modelNewSaleOfficerType),
                    //NewSaleUser = USR.UserListDTO.CreateFromModel(modelNewSaleUser),
                    //NewAgent = MST.AgentDropdownDTO.CreateFromModel(modelNewAgent),
                    //NewAgentEmployee = MST.AgentEmployeeDropdownDTO.CreateFromModel(modelNewAgentEmployee),
                    //NewProjectSaleUser = USR.UserListDTO.CreateFromModel(modelNewProjectSaleUser),
                    //CreatedByUser = USR.UserListDTO.CreateFromModel(modelCreatedByUser),
                    //CreateDate = model.Created
                };

                return result;
            }
            else
            {
                return null;
            }
        }

        public static ChangeLCSaleDTO CreateFromQueryForExport(dbqChangeLCSaleList model, DatabaseContext db)
        {
            if (model != null)
            {
                //var modelProject = db.Projects.Where(e => e.ID == model.ProjectID).FirstOrDefault();
                //var modelUnit = db.Units.Where(e => e.ID == model.UnitID).FirstOrDefault();
                //var modelAgreement = db.Agreements.Include(o => o.Booking).Where(e => e.ID == model.AgreementID).FirstOrDefault();
                //var modelAgreementOwner = db.AgreementOwners.Where(e => e.ID == model.AgreementOwnerID).FirstOrDefault();
                var modelOldSaleUser = db.Users.IgnoreQueryFilters().Where(e => e.ID == model.OldSaleUserID).FirstOrDefault();
                var modelOldProjectSaleUser = db.Users.IgnoreQueryFilters().Where(e => e.ID == model.OldProjectSaleUserID).FirstOrDefault();
                //var modelOldSaleOfficerType = db.MasterCenters.Where(e => e.ID == model.OldSaleOfficerTypeMasterCenterID).FirstOrDefault();
                var modelOldAgent = db.Agents.IgnoreQueryFilters().Where(e => e.ID == model.OldAgentID).FirstOrDefault();
                var modelOldAgentEmployee = db.AgentEmployees.IgnoreQueryFilters().Where(e => e.ID == model.OldAgentEmployeeID).FirstOrDefault();

                var result = new ChangeLCSaleDTO()
                {
                    Id = model.AgreementID,
                    Project = new PRJ.ProjectDropdownDTO {Id= model.ProjectID,ProjectNo = model.ProjectNo },
                    Unit =new PRJ.UnitDropdownDTO {Id=model.UnitID??new Guid() , UnitNo=model.UnitNo } ,
                    Agreement =new SAL.AgreementDropdownDTO {Id=model.AgreementID, AgreementNo = model.AgreementNo},
                    //CustomerName = SAL.AgreementOwnerDropdownDTO.CreateFromModel(modelAgreementOwner),
                    ActiveDate = model.ActiveDate ?? DateTime.Now,
                    //OldSaleOfficerType = MST.MasterCenterDropdownDTO.CreateFromModel(modelOldSaleOfficerType),
                    OldProjectSaleUser = USR.UserListDTO.CreateFromModel(modelOldProjectSaleUser),
                    OldSaleUser = USR.UserListDTO.CreateFromModel(modelOldSaleUser),
                    OldAgent = MST.AgentDropdownDTO.CreateFromModel(modelOldAgent),
                    OldAgentEmployee = MST.AgentEmployeeDropdownDTO.CreateFromModel(modelOldAgentEmployee),
                    Remark = model.Remark
                };

                return result;
            }
            else
            {
                return null;
            }
        }
    }

    public class ChangeLCSaleQueryResult
    {
        public ChangeLCSale ChangeLCSale { get; set; }
        public models.PRJ.Project Project { get; set; }
        public models.PRJ.Unit Unit { get; set; }
        public models.SAL.Agreement Agreement { get; set; }
        public models.SAL.Booking Booking { get; set; }
        public models.SAL.AgreementOwner AgreementOwner { get; set; }
        public models.MST.MasterCenter OldSaleOfficerType { get; set; }
        public models.MST.Agent OldAgent { get; set; }
        public models.MST.AgentEmployee OldAgentEmployee { get; set; }
        public User OldSaleUser { get; set; }
        public User OldProjectSaleUser { get; set; }
        public models.MST.MasterCenter NewSaleOfficerType { get; set; }
        public models.MST.Agent NewAgent { get; set; }
        public models.MST.AgentEmployee NewAgentEmployee { get; set; }
        public User NewSaleUser { get; set; }
        public User NewProjectSaleUser { get; set; }
        public User CreatedByUser { get; set; }
    }
}
