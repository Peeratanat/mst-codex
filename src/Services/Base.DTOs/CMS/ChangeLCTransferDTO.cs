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
    public class ChangeLCTransferDTO : BaseDTO
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


        /// <summary>
        /// เลขที่โอน
        /// </summary>
        [Description("เลขที่โอน")]
        public SAL.TransferDropdownDTO Transfer { get; set; }

        /// <summary>
        /// ชื่อลูกค้า
        /// </summary>
        [Description("ชื่อลูกค้า")]
        public SAL.TransferOwnerDropdownDTO CustomerName { get; set; }


        /// <summary>
        /// Effective Date
        /// </summary>
        [Description("Effective Date")]
        public DateTime? ActiveDate { get; set; }

        /// <summary>
        /// รหัส LC โอน (เดิม)
        /// GET Identity/api/Users?roleCodes=LC&authorizeProjectIDs={projectID}
        /// </summary>
        [Description("รหัส LC โอน (เดิม)")]
        public USR.UserListDTO OldLCTransfer { get; set; }

        /// <summary>
        /// รหัส LC โอน (ใหม่)
        /// GET Identity/api/Users?roleCodes=LC&authorizeProjectIDs={projectID}
        /// </summary>
        [Description("รหัส LC โอน (ใหม่)")]
        public USR.UserListDTO NewLCTransfer { get; set; }

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

        public static ChangeLCTransferDTO CreateFromModel(ChangeLCTransfer model)
        {
            if (model != null)
            {
                var result = new ChangeLCTransferDTO()
                {
                    Id = model.ID,
                    Project = PRJ.ProjectDropdownDTO.CreateFromModel(model.Transfer.Project),
                    Unit = PRJ.UnitDropdownDTO.CreateFromModel(model.Transfer.Unit),
                    Transfer = SAL.TransferDropdownDTO.CreateFromModel(model.Transfer),
                    //CustomerName = SAL.TransferOwnerDropdownDTO.CreateFromModel(model.TransferOwner),
                    ActiveDate = model.ActiveDate,
                    OldLCTransfer = USR.UserListDTO.CreateFromModel(model.OldLCTransfer),
                    NewLCTransfer = USR.UserListDTO.CreateFromModel(model.NewLCTransfer),
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

        public static ChangeLCTransferDTO CreateFromQueryResult(ChangeLCTransferQueryResult model)
        {
            if (model != null)
            {
                var result = new ChangeLCTransferDTO()
                {
                    Id = model.ChangeLCTransfer?.ID,
                    Project = PRJ.ProjectDropdownDTO.CreateFromModel(model.Transfer.Agreement.Project),
                    Unit = PRJ.UnitDropdownDTO.CreateFromModel(model.Transfer.Unit),
                    Transfer = SAL.TransferDropdownDTO.CreateFromModel(model.Transfer),
                    CustomerName = SAL.TransferOwnerDropdownDTO.CreateFromModel(model.TransferOwner),
                    ActiveDate = model.ChangeLCTransfer?.ActiveDate ?? DateTime.Now,
                    OldLCTransfer = USR.UserListDTO.CreateFromModel(model.OldLCTransfer),
                    NewLCTransfer = USR.UserListDTO.CreateFromModel(model.NewLCTransfer),
                    CreatedByUser = USR.UserListDTO.CreateFromModel(model.CreatedByUser),
                    CreateDate = model.ChangeLCTransfer?.Created
                };

                return result;
            }
            else
            {
                return null;
            }
        }

        public static void SortBy(ChangeLCTransferSortByParam sortByParam, ref IQueryable<ChangeLCTransferQueryResult> query)
        {
            IOrderedQueryable<ChangeLCTransferQueryResult> orderQuery;
            if (sortByParam.SortBy != null)
            {
                switch (sortByParam.SortBy.Value)
                {
                    case ChangeLCTransferSortBy.ProjectID:
                        if (sortByParam.Ascending) orderQuery = query.OrderBy(o => o.ChangeLCTransfer.Transfer.Agreement.ProjectID);
                        else orderQuery = query.OrderByDescending(o => o.ChangeLCTransfer.Transfer.Agreement.ProjectID);
                        break;
                    case ChangeLCTransferSortBy.Unit:
                        if (sortByParam.Ascending) orderQuery = query.OrderBy(o => o.ChangeLCTransfer.Transfer.Unit.UnitNo);
                        else orderQuery = query.OrderByDescending(o => o.ChangeLCTransfer.Transfer.Unit.UnitNo);
                        break;
                    case ChangeLCTransferSortBy.Transfer:
                        if (sortByParam.Ascending) orderQuery = query.OrderBy(o => o.ChangeLCTransfer.Transfer.TransferNo);
                        else orderQuery = query.OrderByDescending(o => o.ChangeLCTransfer.Transfer.TransferNo);
                        break;
                    case ChangeLCTransferSortBy.CustomerName:
                        if (sortByParam.Ascending) orderQuery = query.OrderBy(o => o.TransferOwner.FirstNameTH).ThenBy(o => o.TransferOwner.LastNameTH);
                        else orderQuery = query.OrderByDescending(o => o.TransferOwner.FirstNameTH).ThenBy(o => o.TransferOwner.LastNameTH);
                        break;
                    case ChangeLCTransferSortBy.ActiveDate:
                        if (sortByParam.Ascending) orderQuery = query.OrderBy(o => o.ChangeLCTransfer.ActiveDate);
                        else orderQuery = query.OrderByDescending(o => o.ChangeLCTransfer.ActiveDate);
                        break;
                    case ChangeLCTransferSortBy.OldLCTransfer:
                        if (sortByParam.Ascending) orderQuery = query.OrderBy(o => o.ChangeLCTransfer.OldLCTransfer.FirstName);
                        else orderQuery = query.OrderByDescending(o => o.ChangeLCTransfer.OldLCTransfer.FirstName);
                        break;
                    default:
                        orderQuery = query.OrderBy(o => o.ChangeLCTransfer.ActiveDate);
                        break;
                }
            }
            else
            {
                orderQuery = query.OrderBy(o => o.ChangeLCTransfer.ActiveDate);
            }

            orderQuery.ThenBy(o => o.ChangeLCTransfer.ID);
            query = orderQuery;
        }

        public async Task ValidateAsync(DatabaseContext db)
        {
            ValidateException ex = new ValidateException();
            if (!this.ActiveDate.HasValue)
            {
                var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                string desc = this.GetType().GetProperty(nameof(ChangeLCTransferDTO.ActiveDate)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = errMsg.Message.Replace("[field]", desc);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }
            if (this.Project == null)
            {
                var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                string desc = this.GetType().GetProperty(nameof(ChangeLCTransferDTO.Project)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = errMsg.Message.Replace("[field]", desc);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }
            if (this.Transfer == null)
            {
                var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                string desc = this.GetType().GetProperty(nameof(ChangeLCTransferDTO.Transfer)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = errMsg.Message.Replace("[field]", desc);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }
            if (this.NewLCTransfer == null)
            {
                var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                string desc = this.GetType().GetProperty(nameof(ChangeLCTransferDTO.NewLCTransfer)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = errMsg.Message.Replace("[field]", desc);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }

            if (ex.HasError)
            {
                throw ex;
            }
        }

        public void ToModel(ref ChangeLCTransfer model)
        {
            model.ActiveDate = this.ActiveDate;
            model.TransferID = this.Transfer.Id;
            model.OldLCTransferID = this.OldLCTransfer?.Id;
            model.NewLCTransferID = this.NewLCTransfer.Id;
            model.Remark = this.Remark;
        }

        public static ChangeLCTransferDTO CreateFromQuery(dbqChangeLCTransferList model, DatabaseContext db)
        {          
            if (model != null)
            {
                var modelProject = db.Projects.Where(e => e.ID == model.ProjectID).FirstOrDefault();
                var modelUnit = db.Units.Where(e => e.ID == model.UnitID).FirstOrDefault();
                var modelTransfer = db.Transfers.Include(o => o.Agreement).Where(e => e.ID == model.TransferID).FirstOrDefault();
                var modelTransferOwner = db.TransferOwners.Where(e => e.ID == model.TransferOwnerID).FirstOrDefault();
                var modelOldTransferUser = db.Users.IgnoreQueryFilters().Where(e => e.ID == model.OldTransferSaleUserID).FirstOrDefault();

                var result = new ChangeLCTransferDTO()
                {
                    Id = model.TransferID,
                    Project = PRJ.ProjectDropdownDTO.CreateFromModel(modelProject),
                    Unit = PRJ.UnitDropdownDTO.CreateFromModel(modelUnit),
                    Transfer = SAL.TransferDropdownDTO.CreateFromModel(modelTransfer),
                    CustomerName = SAL.TransferOwnerDropdownDTO.CreateFromModel(modelTransferOwner),
                    ActiveDate = model.ActiveDate ?? DateTime.Now,
                    OldLCTransfer = USR.UserListDTO.CreateFromModel(modelOldTransferUser),
                    //NewLCTransfer = USR.UserListDTO.CreateFromModel(modelNewLCTransfer),
                    //CreatedByUser = USR.UserListDTO.CreateFromModel(modelCreatedByUser),
                    //CreateDate = modelCreated
                };

                return result;
            }
            else
            {
                return null;
            }
        }
    }

    public class ChangeLCTransferQueryResult
    {
        public ChangeLCTransfer ChangeLCTransfer { get; set; }
        public models.PRJ.Project Project { get; set; }
        public models.PRJ.Unit Unit { get; set; }
        public models.SAL.Agreement Agreement { get; set; }
        public models.SAL.Transfer Transfer { get; set; }
        public models.SAL.TransferOwner TransferOwner { get; set; }
        public User OldLCTransfer { get; set; }
        public User NewLCTransfer { get; set; }
        public User CreatedByUser { get; set; }
    }
}
