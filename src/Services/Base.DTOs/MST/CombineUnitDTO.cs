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
using System.Reactive;
using Database.Models.MasterKeys;
using static Database.Models.DbQueries.DBQueryParam;
using Database.Models.DbQueries;
using PagingExtensions;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using Microsoft.Data.SqlClient;
using Base.DTOs.FIN;
using Database.Models.DbQueries.FIN;
using Database.Models.DbQueries.MST;
using System.Data.Common;
using Dapper;
using System.Data;
using Microsoft.EntityFrameworkCore.Storage;

namespace Base.DTOs.MST
{
    public class CombineUnitDTO : BaseDTO
    {
        public ProjectDropdownDTO Project { get; set; }
        public UnitDropdownDTO Unit { get; set; }
        public UnitDropdownDTO UnitCombine { get; set; }
        public MasterCenterDropdownDTO CombineDocType { get; set; }
        public MasterCenterDropdownDTO CombineStatus { get; set; }
        public DateTime? ApprovedDate { get; set; }
        public string ApprovedBy { get; set; }
        public Guid? ApprovedByID { get; set; }
        public string ReasonDel { get; set; }
        public string ReasonEdit { get; set; }
        public string ActionApprove { get; set; }
        public string ApproveResultMsg { get; set; }
        public bool? IsEdit { get; set; }
        public bool? IsError { get; set; }
        public string ErrorMsg { get; set; }
        public bool? IsCanEdit { get; set; }
        public bool? IsCanDelete { get; set; }


        public static CombineUnitDTO CreateFromModel(CombineUnitQueryResult model, DatabaseContext db)
        {
            if (model != null)
            {
                var result = new CombineUnitDTO()
                {
                    Id = model.CombineUnit.ID,
                    Project = ProjectDropdownDTO.CreateFromModel(model.Project),
                    Unit = UnitDropdownDTO.CreateFromModel(model.Unit),
                    UnitCombine = UnitDropdownDTO.CreateFromModel(model.UnitCombine),
                    CombineDocType = MasterCenterDropdownDTO.CreateFromModel(model.CombineDocType),
                    CombineStatus = MasterCenterDropdownDTO.CreateFromModel(model.CombineStatus),
                    ApprovedDate = model.CombineUnit.ApprovedDate,
                    ApprovedBy = model.ApprovedBy.DisplayName,
                    ReasonDel = model.CombineUnit.ReasonDel,
                    ReasonEdit = model.CombineUnit.ReasonEdit,
                    Updated = model.CombineUnit.Updated,
                    UpdatedBy = model.CombineUnit.UpdatedBy?.DisplayName
                };
                return result;
            }
            else
            {
                return null;
            }
        }
        public static void SortBy(CombineUnitSortByParam sortByParam, ref IQueryable<CombineUnitQueryResult> query)
        {
            if (sortByParam.SortBy != null)
            {
                switch (sortByParam.SortBy.Value)
                {
                    //case CombineUnitSortBy.Project:
                    //    if (sortByParam.Ascending) query = query.OrderBy(o => o.CombineUnit.Project.ProjectNo);
                    //    else query = query.OrderByDescending(o => o.CombineUnit.Project.ProjectNo);
                    //    break;
                    case CombineUnitSortBy.Unit:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.CombineUnit.Unit.UnitNo);
                        else query = query.OrderByDescending(o => o.CombineUnit.Unit.UnitNo);
                        break;
                    case CombineUnitSortBy.UnitCombine:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.CombineUnit.UnitCombine.UnitNo);
                        else query = query.OrderByDescending(o => o.CombineUnit.UnitCombine.UnitNo);
                        break;
                    case CombineUnitSortBy.CombineDocType:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.CombineUnit.CombineDocType.Key);
                        else query = query.OrderByDescending(o => o.CombineUnit.CombineDocType.Key);
                        break;
                    case CombineUnitSortBy.CombineStatus:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.CombineUnit.CombineStatus.Key);
                        else query = query.OrderByDescending(o => o.CombineUnit.CombineStatus.Key);
                        break;
                    case CombineUnitSortBy.Updated:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.CombineUnit.Updated);
                        else query = query.OrderByDescending(o => o.CombineUnit.Updated);
                        break;
                    case CombineUnitSortBy.UpdatedBy:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.CombineUnit.UpdatedBy.DisplayName);
                        else query = query.OrderByDescending(o => o.CombineUnit.UpdatedBy.DisplayName);
                        break;
                }
            }
            else
            {
                //query = query.OrderByDescending(o => o.LetterOfGuarantee.IssueDate).ThenBy(o => o.Project.ProjectNo);
            }
        }
        public async Task ValidateAddAsync(DatabaseContext db)
        {
            List<string> ex = new List<string>();

            using DbCommand cmd = db.Database.GetDbConnection().CreateCommand();
            DynamicParameters ParamList = new DynamicParameters();
            ParamList.Add("ProjectID", Project.Id);
            ParamList.Add("UnitID", Unit.Id);

            CommandDefinition commandDefinition = new(
                                         commandText: DBStoredNames.spGetUnitCanCombine,
                                         parameters: ParamList,
                                         transaction: db?.Database?.CurrentTransaction?.GetDbTransaction(),
                                         commandType: CommandType.StoredProcedure);
            var queryUnit = await cmd.Connection.QueryAsync<dbqUnitCanCombine>(commandDefinition);
            var DataUnit1 = queryUnit.ToList();

            ParamList = new DynamicParameters();
            ParamList.Add("ProjectID", Project.Id);
            ParamList.Add("UnitID", UnitCombine.Id);

            commandDefinition = new(
                                       commandText: DBStoredNames.spGetUnitCanCombine,
                                       parameters: ParamList,
                                       transaction: db?.Database?.CurrentTransaction?.GetDbTransaction(),
                                       commandType: CommandType.StoredProcedure);

            var queryUnit2 = await cmd.Connection.QueryAsync<dbqUnitCanCombine>(commandDefinition);
            var DataUnit2 = queryUnit2.ToList();
            if (!DataUnit1.Any())
            {
                string desc = $@"{Unit.UnitNo} สถานะแปลงไม่ถูกต้อง";
                ex.Add(desc);
            }
            if (!DataUnit2.Any())
            {
                string desc = $@"{UnitCombine.UnitNo} สถานะแปลงไม่ถูกต้อง";
                ex.Add(desc);
            }
            if (ex.Any())
            {
                IsError = true;
                ErrorMsg = string.Join(',', ex);
                //              throw ex;
            }
        }
        public static CombineUnitDTO CreateFromQuery(dbqUnitCombine model)
        {
            if (model != null)
            {
                CombineUnitDTO result = new CombineUnitDTO();
                result.Id = model.CombineUnitID ?? new Guid();
                result.Project = new ProjectDropdownDTO { Id = model.ProjectID, ProjectNameTH = model.ProjectNameTH, ProjectNameEN = model.ProjectNameEN, ProjectNo = model.ProjectNo };
                result.Unit = new UnitDropdownDTO { Id = model.UnitID ?? new Guid(), UnitNo = model.UnitNo };
                result.UnitCombine = new UnitDropdownDTO { Id = model.UnitCombineID ?? new Guid(), UnitNo = model.UnitCombineNo };
                result.CombineDocType = new MasterCenterDropdownDTO { Id = model.CombineDocTypeID ?? new Guid(), Key = model.CombineDocTypeKey, Name = model.CombineDocTypeName };
                result.CombineStatus = new MasterCenterDropdownDTO { Id = model.CombineStatusID ?? new Guid(), Key = model.CombineStatusKey, Name = model.CombineStatusName };
                result.ApprovedBy = model.ApprovedBy;
                result.ApprovedDate = model.ApprovedDate;
                result.Updated = model.Updated;
                result.UpdatedBy = model.UpdatedBy;
                result.IsEdit = model.IsEdit;
                result.IsCanDelete = model.IsCanDelete;
                result.IsCanEdit = model.IsCanEdit;
                return result;
            }
            else
            {
                return null;
            }
        }
        public class CombineUnitQueryResult
        {
            public models.MST.CombineUnit CombineUnit { get; set; }
            public models.PRJ.Project Project { get; set; }
            public models.PRJ.Unit Unit { get; set; }
            public models.PRJ.Unit UnitCombine { get; set; }
            public models.MST.MasterCenter CombineDocType { get; set; }
            public models.MST.MasterCenter CombineStatus { get; set; }
            public models.USR.User ApprovedBy { get; set; }
            public models.USR.User UpdatedBy { get; set; }

        }
    }
}
