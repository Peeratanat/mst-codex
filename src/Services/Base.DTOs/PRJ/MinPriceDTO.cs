using Database.Models;
using Database.Models.DbQueries.PRJ;
using Database.Models.MST;
using Database.Models.PRJ;
using Database.Models.USR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Base.DTOs.PRJ
{
    public class MinPriceDTO : BaseDTO
    {
        /// <summary>
        /// ข้อมูลแปลง
        /// Project/api/Projects/{projectID}/Units
        /// </summary>
        public UnitDTO Unit { get; set; }
        /// <summary>
        ///  ราคาทุนล่าสุด
        /// </summary>
        public decimal? Cost { get; set; }
        /// <summary>
        ///  ชนิดราคาทุน
        ///  Master/api/MasterCenters?masterCenterGroupKey=MinPriceType
        /// </summary>
        public MST.MasterCenterDropdownDTO MinPriceType { get; set; }
        /// <summary>
        ///  ROI Minprice
        /// </summary>
        public decimal? ROIMinprice { get; set; }
        /// <summary>
        ///  ราคาขาย
        /// </summary>
        public decimal? SalePrice { get; set; }
        /// <summary>
        ///  Min อนุมัติ
        /// </summary>
        public decimal? ApprovedMinPrice { get; set; }
        /// <summary>
        ///  Doc Type
        ///  Master/api/MasterCenters?masterCenterGroupKey=DocType
        /// </summary>
        public MST.MasterCenterDropdownDTO DocType { get; set; }
        /// <summary>
        /// ข้อมูลโฉนด
        /// Project/api/Projects/{projectID}/TitleDeeds
        /// </summary>
        public TitleDeedListDTO TitleDeed { get; set; }
        /// <summary>
        ///  เปิด-ปิดตามแปลงว่าง
        /// </summary>
        public bool IsAction { get; set; }
        public static MinPriceDTO CreateFromModel(MinPrice model, TitledeedDetail titledeed)
        {
            if (model != null)
            {
                var result = new MinPriceDTO()
                {
                    Id = model.ID,
                    Unit = UnitDTO.CreateFromModel(model.Unit),
                    TitleDeed = TitleDeedListDTO.CreateFromModel(titledeed),
                    Cost = model.Cost,
                    MinPriceType = MST.MasterCenterDropdownDTO.CreateFromModel(model.MinPriceType),
                    ROIMinprice = model.ROIMinprice,
                    SalePrice = model.SalePrice,
                    DocType = MST.MasterCenterDropdownDTO.CreateFromModel(model.DocType),
                    ApprovedMinPrice = model.ApprovedMinPrice,
                    Updated = model.Updated,
                    UpdatedBy = model.UpdatedBy?.DisplayName
                };
                return result;
            }
            else
            {
                return null;
            }
        }
      
        public static MinPriceDTO CreateFromQueryResult(MinPriceQueryResult model)
        {
            if (model != null)
            {
                var result = new MinPriceDTO()
                {
                    Id = model.MinPrice.ID,
                    Unit = UnitDTO.CreateFromModel(model.Unit),
                    Cost = model.MinPrice.Cost,
                    MinPriceType = MST.MasterCenterDropdownDTO.CreateFromModel(model.MinPriceType),
                    DocType = MST.MasterCenterDropdownDTO.CreateFromModel(model.DocType),
                    TitleDeed = TitleDeedListDTO.CreateFromModel(model.Titledeed),
                    ROIMinprice = model.MinPrice.ROIMinprice,
                    SalePrice = model.MinPrice.SalePrice,
                    ApprovedMinPrice = model.MinPrice.ApprovedMinPrice,
                    Updated = model.MinPrice.Updated,
                    UpdatedBy = model.UpdatedBy?.DisplayName ?? string.Empty
                };

                return result;
            }
            else
            {
                return null;
            }
        }
      
        public static MinPriceDTO CreateFromQueryWithSalePriceResult(DatabaseContext db, MinPriceQueryResult model)
        {
            if (model != null)
            {
                var result = new MinPriceDTO()
                {
                    Id = model.MinPrice.ID,
                    Unit = UnitDTO.CreateFromModel(model.Unit),
                    Cost = model.MinPrice.Cost,
                    MinPriceType = MST.MasterCenterDropdownDTO.CreateFromModel(model.MinPriceType),
                    DocType = MST.MasterCenterDropdownDTO.CreateFromModel(model.DocType),
                    TitleDeed = TitleDeedListDTO.CreateFromModel(model.Titledeed),
                    ROIMinprice = model.MinPrice?.ROIMinprice,
                    SalePrice = GetMinPrice(db, model.Unit),
                    ApprovedMinPrice = model.MinPrice?.ApprovedMinPrice,
                    Updated = model.MinPrice?.Updated,
                    UpdatedBy = model.MinPrice.UpdatedBy?.DisplayName ?? string.Empty
                };

                return result;
            }
            else
            {
                return null;
            }
        }
      
        public static decimal? GetMinPrice(DatabaseContext DB, Unit unit)
        {
            if (unit != null)
            {
                var priceList = DB.PriceLists.Where(o => o.UnitID == unit.ID)
                                             .GroupBy(o => o.UnitID)
                                             .Select(o => o.First())
                                             .OrderByDescending(o => o.Created)
                                             .FirstOrDefault();

                if (priceList != null)
                {
                    var priceListItems = DB.PriceListItems.Where(o => o.PriceListID == priceList.ID)
                                                        .GroupBy(o => o.PriceListID)
                                                        .Select(o => o.First())
                                                        .OrderByDescending(o => o.Created)
                                                        .FirstOrDefault();
                    if (priceListItems != null)
                    {
                        var salePrice = priceListItems.Amount > 0 ? priceListItems.Amount : 0;
                        return salePrice;
                    }
                    else
                    {
                        return 0;
                    }
                }
                else
                {
                    return 0;
                }
            }
            else
            {
                return 0;
            }
        }
      
        public static MinPriceDTO CreateFromQueryTempResult(MinPriceQueryResult model)
        {
            if (model != null)
            {
                var result = new MinPriceDTO()
                {
                    Unit = UnitDTO.CreateFromModel(model.Unit),
                };

                return result;
            }
            else
            {
                return null;
            }
        }

        public static void SortBy(MinPriceSortByParam sortByParam, ref List<MinPriceDTO> query)
        {
            if (sortByParam.SortBy != null)
            {
                switch (sortByParam.SortBy.Value)
                {
                    case MinPriceSortBy.Unit_UnitNo:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.Unit?.UnitNo).ToList();
                        else query = query.OrderByDescending(o => o.Unit?.UnitNo).ToList();
                        break;
                    case MinPriceSortBy.Unit_HouseNo:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.Unit?.HouseNo).ToList();
                        else query = query.OrderByDescending(o => o.Unit?.HouseNo).ToList();
                        break;
                    case MinPriceSortBy.Unit_SaleArea:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.Unit?.SaleArea).ToList();
                        else query = query.OrderByDescending(o => o.Unit?.SaleArea).ToList();
                        break;
                    case MinPriceSortBy.Titledeed_TitledeedArea:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.TitleDeed?.TitledeedArea).ToList();
                        else query = query.OrderByDescending(o => o.TitleDeed?.TitledeedArea).ToList();
                        break;
                    case MinPriceSortBy.Cost:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.Cost).ToList();
                        else query = query.OrderByDescending(o => o.Cost).ToList();
                        break;
                    case MinPriceSortBy.MinPriceType:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.MinPriceType?.Name).ToList();
                        else query = query.OrderByDescending(o => o.MinPriceType?.Name).ToList();
                        break;
                    case MinPriceSortBy.ROIMinprice:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.ROIMinprice).ToList();
                        else query = query.OrderByDescending(o => o.ROIMinprice).ToList();
                        break;
                    case MinPriceSortBy.SalePrice:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.SalePrice).ToList();
                        else query = query.OrderByDescending(o => o.SalePrice).ToList();
                        break;
                    case MinPriceSortBy.ApprovedMinPrice:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.ApprovedMinPrice).ToList();
                        else query = query.OrderByDescending(o => o.ApprovedMinPrice).ToList();
                        break;
                    case MinPriceSortBy.DocType:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.DocType?.Name).ToList();
                        else query = query.OrderByDescending(o => o.DocType?.Name).ToList();
                        break;
                    case MinPriceSortBy.Updated:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.Updated).ToList();
                        else query = query.OrderByDescending(o => o.Updated).ToList();
                        break;
                    case MinPriceSortBy.UpdatedBy:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.UpdatedBy).ToList();
                        else query = query.OrderByDescending(o => o.UpdatedBy).ToList();
                        break;
                    default:
                        query = query.OrderBy(o => o.Unit.UnitNo).ToList();
                        break;
                }
            }
            else
            {
                query = query.OrderBy(o => o.Unit?.UnitNo).ToList();
            }
        }

        public void ToModel(ref MinPrice model)
        {
            model.UnitID = this.Unit?.Id;
            model.Cost = this.Cost;
            model.MinPriceTypeMasterCenterID = this.MinPriceType?.Id;
            model.ROIMinprice = this.ROIMinprice;
            model.SalePrice = this.SalePrice;
            //model.DocTypeMasterCenterID = this.DocType?.Id;
        }

        public static async Task<MinPriceDTO> CreateFromQueryAsync(dbqMinPriceList model, DatabaseContext db)
        {
            if (model != null)
            {
                var modelMinPriceType = await db.MasterCenters.FirstOrDefaultAsync(o => o.ID == model.MinPriceTypeID);
                var modelDocType = await db.MasterCenters.FirstOrDefaultAsync(o => o.ID == model.MinPriceStageID);
                var modelUnitStatus = await db.MasterCenters.FirstOrDefaultAsync(o => o.ID == model.UnitStatusID);

                var result = new MinPriceDTO()
                {
                    Id = model.MinPriceID,
                    Unit = new UnitDTO() {
                        Id = model.UnitID,
                        UnitNo = model.UnitNo,
                        HouseNo = model.HouseNo,
                        SaleArea = model.SaleArea,
                        SapwbsNo = model.SAPWBSNo
                    }, 
                    Cost = model.MinPrice,
                    MinPriceType = MST.MasterCenterDropdownDTO.CreateFromModel(modelMinPriceType),
                    DocType = MST.MasterCenterDropdownDTO.CreateFromModel(modelDocType),
                    TitleDeed = new TitleDeedListDTO() {
                        TitledeedArea = model.TitledeedArea
                    },
                    ROIMinprice = model.ROIMinprice,
                    SalePrice = model.SellingPrice,
                    ApprovedMinPrice = model.ApproveMinprice,
                    Updated = model.Created,
                    UpdatedBy = model.CreatedByName ?? string.Empty,
                    IsAction = CheckActions(modelUnitStatus)
                };
                result.Unit.UnitStatus = MST.MasterCenterDropdownDTO.CreateFromModel(modelUnitStatus);
                return result;
            }
            else
            {
                return null;
            }
        }
        public static bool CheckActions(MasterCenter model) //UnitStatus
        {
            if (model != null)
            {
                if (model.Key.Equals("0") && model.Order == 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
    }
    public class MinPriceQueryResult
    {
        public MinPrice MinPrice { get; set; }
        public PriceList PriceList { get; set; }
        public Unit Unit { get; set; }
        public MasterCenter MinPriceType { get; set; }
        public MasterCenter DocType { get; set; }
        public TitledeedDetail Titledeed { get; set; }
        public User UpdatedBy { get; set; }
    }
}
