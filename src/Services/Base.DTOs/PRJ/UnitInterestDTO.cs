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
using System.Text;
using System.Threading.Tasks;

namespace Base.DTOs.PRJ
{
    public class UnitInterestDTO : BaseDTO
    {
        public Guid? ProjectID { get; set; }
        public UnitDropdownDTO unitDTO { get; set; }
        public FloorDropdownDTO floorDTO { get; set; }
        public TowerDropdownDTO towerDTO { get; set; }
        public List<UnitControlInterestDTO> unitControlInterestDTOs { get; set; }
        public static UnitInterestDTO CreateFromQueryResult(UnitControlInterestGroupResult model)
        {
            if (model != null)
            {
                var result = new UnitInterestDTO()
                {
                    ProjectID = model.ProjectID,
                    unitDTO =  UnitDropdownDTO.CreateFromModel(model.Unit) ,
                    floorDTO = FloorDropdownDTO.CreateFromModel(model.Floor) ,
                    towerDTO = TowerDropdownDTO.CreateFromModel(model.Tower) ,  
                    Id = model.UnitID,
                };
                result.unitControlInterestDTOs = model.Details.Select(x => new UnitControlInterestDTO { 
                    Id = x.UnitControlInterestID,
                    EffectiveDate = x.EffectiveDate,
                    ExpiredDate = x.ExpiredDate,
                    Remark = x.Remark,
                    ProjectID = model.ProjectID,
                    UnitID = model.UnitID,
                    InterestCounter = x.InterestCounter
                }).ToList();
                return result;
            }
            else
            {
                return null;
            }
        }
        public static void SortBy(UnitControlInterestSortByParam sortByParam, ref IQueryable<UnitControlInterestQueryResult> query)
        {
            if (sortByParam.SortBy != null)
            {
                switch (sortByParam.SortBy.Value)
                {
                    case UnitControlInterestSortByParamSortBy.UnitNo:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.Unit.UnitNo);
                        else query = query.OrderByDescending(o => o.Unit.UnitNo);
                        break;
                    case UnitControlInterestSortByParamSortBy.TowerName:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.Tower.TowerCode);
                        else query = query.OrderByDescending(o => o.Tower.TowerCode);
                        break;
                    case UnitControlInterestSortByParamSortBy.FloorName:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.Floor.NameTH);
                        else query = query.OrderByDescending(o => o.Floor.NameTH);
                        break;
                    case UnitControlInterestSortByParamSortBy.UnitStatus:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.Unit.UnitStatus.Name);
                        else query = query.OrderByDescending(o => o.Unit.UnitStatus.Name);
                        break;
                    default:
                        query = query.OrderBy(o => o.Unit.UnitNo);
                        break;
                }
            }
            else
            {
                query = query.OrderBy(o => o.Unit.UnitNo);
            }
        }
    }
    public class UnitControlInterestQueryResult
    {
        public Floor Floor { get; set; }
        public Tower Tower { get; set; }
        public Unit Unit { get; set; }
        public Guid? ProjectID { get; set; }
        public Guid? UnitID { get; set; }
        public Guid? FloorID { get; set; }
        public DateTime? EffectiveDate { get; set; }
        public DateTime? ExpiredDate { get; set; }
        public int? InterestCounter { get; set; }
        public string Remark { get; set; }
        public Guid? UnitControlInterestID { get; set; }
    }
    public class UnitControlInterestGroupResult
    {
        public Floor Floor { get; set; }
        public Tower Tower { get; set; }
        public Unit Unit { get; set; }
        public Guid? ProjectID { get; set; }
        public Guid? UnitID { get; set; }
        public Guid? FloorID { get; set; }
        public List<UnitControlInterestQueryResult> Details { get; set; }
    }
}
