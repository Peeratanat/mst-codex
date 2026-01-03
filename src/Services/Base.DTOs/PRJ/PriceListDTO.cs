using Database.Models;
using Database.Models.MST;
using Database.Models.PRJ;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Base.DTOs.PRJ
{
    public class PriceListDTO : BaseDTO
    {
        public DateTime? ActiveDate { get; set; }
        /// <summary>
        ///  เลขที่แปลง
        /// </summary>
        /// <example></example>
        public string UnitNo { get; set; }
        /// <summary>
        ///  พื้นที่ผังขาย
        /// </summary>
        public double? SaleArea { get; set; }
        /// <summary>
        ///  พื้นที่โฉนด
        /// </summary>
        public double? TitleDeedArea { get; set; }
        /// <summary>
        ///  พื้นที่เพิ่มลด
        /// </summary>
        public double? OffsetArea { get; set; }
        /// <summary>
        ///  ราคาพื้นที่เพิ่มลด (ต่อหน่วย), ราคาพื้นที่ต่อหน่วย
        /// </summary>
        public decimal? OffsetAreaUnitPrice { get; set; }
        /// <summary>
        ///  ราคาพื้นที่เพิ่มลด
        /// </summary>
        public decimal? OffsetAreaPrice { get; set; }
        /// <summary>
        ///  ราคาขายรวม
        /// </summary>
        public decimal? TotalSalePrice { get; set; }
        /// <summary>
        ///  % เงินดาวน์
        /// </summary>
        public double? PercentDownPayment { get; set; }
        /// <summary>
        ///  เงินจอง
        /// </summary>
        /// <example></example>
        public decimal? BookingAmount { get; set; }
        /// <summary>
        ///  เงินทำสัญญา
        /// </summary>
        public decimal? ContractAmount { get; set; }
        /// <summary>
        ///  งวดดาวน์
        /// </summary>
        public double? DownPaymentPeriod { get; set; }
        /// <summary>
        ///  เงินดาวน์ต่องวด
        /// </summary>
        public decimal? DownPaymentPerPeriod { get; set; }
        /// <summary>
        ///  เงินดาวน์ 
        /// </summary>
        public decimal? DownAmount { get; set; }
        /// <summary>
        ///  งวดดาวน์พิเศษ
        /// </summary>
        public string SpecialDown { get; set; }
        /// <summary>
        ///  เงินดาวน์พิเศษ
        /// </summary>
        public string SpecialDownPrice { get; set; }
        /// <summary>
        ///  สถานะขาย
        ///  Master/api/MasterCenters?masterCenterGroupKey=UnitStatus
        /// </summary>
        public MST.MasterCenterDropdownDTO UnitStatus { get; set; }
        /// <summary>
        ///  เปิด-ปิดตามแปลงว่าง
        /// </summary>
        public bool IsAction { get; set; }

        public static PriceListDTO CreateFromQueryResult(PriceListQueryResult model)
        {
            if (model != null)
            {
                var result = new PriceListDTO()
                {
                    Id = model.PriceList?.ID,
                    UnitNo = model.Unit?.UnitNo,
                    SaleArea = model.Unit?.SaleArea,
                    ActiveDate = model.PriceList?.ActiveDate,
                    BookingAmount = model.BookingAmount?.Amount,
                    TotalSalePrice = model.TotalSalePrice?.Amount,
                    ContractAmount = model.ContractAmount?.Amount,
                    TitleDeedArea = model.TitleDeedArea,
                    DownAmount = model.DownAmount?.Amount == 0 ? null : model.DownAmount?.Amount,
                    OffsetArea = model.OffsetArea,
                    OffsetAreaUnitPrice = model.OffsetAreaUnitPrice,
                    DownPaymentPeriod = model.DownAmount?.Installment,
                    DownPaymentPerPeriod = model.DownPaymentPerPeriod,
                    PercentDownPayment = model.PercentDownPayment,
                    SpecialDown = model.SpecialDown,
                    SpecialDownPrice = model.SpecialDownPrice,
                    //OffsetAreaPrice = model.OffsetAreaPrice != null ? (decimal?)Decimal.Round(Convert.ToDecimal(model.OffsetAreaUnitPrice)) : null,
                    OffsetAreaPrice = model.OffsetAreaPrice != null ? model.OffsetAreaPrice : 0,
                    UnitStatus = MST.MasterCenterDropdownDTO.CreateFromModel(model.UnitStatus),
                    Updated = model.PriceList?.Updated,
                    UpdatedBy = model.PriceList?.UpdatedBy?.DisplayName,
                    IsAction = PriceListDTO.CheckActions(model.UnitStatus)
                };

                return result;
            }
            else
            {
                return null;
            }
        }
        public static PriceListDTO CreateFromDarfAsync(PriceList model, DatabaseContext db)
        {
            if (model != null)
            {
                var item = db.PriceListItems.Where(o=>o.PriceListID == model.ID).FirstOrDefault();
                var result = new PriceListDTO()
                {
                    Id = model.ID,
                    UnitNo = model.Unit?.UnitNo,
                    TotalSalePrice = item?.Amount,
                };

                return result;
            }
            else
            {
                return null;
            }
        }

        public static void SortBy(PriceListSortByParam sortByParam, ref List<PriceListDTO> listDTOs)
        {
            if (sortByParam.SortBy != null)
            {

                switch (sortByParam.SortBy.Value)
                {
                    case PriceListSortBy.UnitNo:
                        if (sortByParam.Ascending) listDTOs = listDTOs.OrderBy(o => o.UnitNo).ToList();
                        else listDTOs = listDTOs.OrderByDescending(o => o.UnitNo).ToList();
                        break;
                    case PriceListSortBy.SaleArea:
                        if (sortByParam.Ascending) listDTOs = listDTOs.OrderBy(o => o.SaleArea).ToList();
                        else listDTOs = listDTOs.OrderByDescending(o => o.SaleArea).ToList();
                        break;
                    case PriceListSortBy.ActiveDate:
                        if (sortByParam.Ascending) listDTOs = listDTOs.OrderBy(o => o.ActiveDate).ToList();
                        else listDTOs = listDTOs.OrderByDescending(o => o.ActiveDate).ToList();
                        break;
                    case PriceListSortBy.BookingAmount:
                        if (sortByParam.Ascending) listDTOs = listDTOs.OrderBy(o => o.BookingAmount).ToList();
                        else listDTOs = listDTOs.OrderByDescending(o => o.BookingAmount).ToList();
                        break;
                    case PriceListSortBy.TotalSalePrice:
                        if (sortByParam.Ascending) listDTOs = listDTOs.OrderBy(o => o.TotalSalePrice).ToList();
                        else listDTOs = listDTOs.OrderByDescending(o => o.TotalSalePrice).ToList();
                        break;
                    case PriceListSortBy.ContractAmount:
                        if (sortByParam.Ascending) listDTOs = listDTOs.OrderBy(o => o.ContractAmount).ToList();
                        else listDTOs = listDTOs.OrderByDescending(o => o.ContractAmount).ToList();
                        break;
                    case PriceListSortBy.TitleDeedArea:
                        if (sortByParam.Ascending) listDTOs = listDTOs.OrderBy(o => o.TitleDeedArea).ToList();
                        else listDTOs = listDTOs.OrderByDescending(o => o.TitleDeedArea).ToList();
                        break;
                    case PriceListSortBy.DownAmount:
                        if (sortByParam.Ascending) listDTOs = listDTOs.OrderBy(o => o.DownAmount).ToList();
                        else listDTOs = listDTOs.OrderByDescending(o => o.DownAmount).ToList();
                        break;
                    case PriceListSortBy.OffsetArea:
                        if (sortByParam.Ascending) listDTOs = listDTOs.OrderBy(o => o.OffsetArea).ToList();
                        else listDTOs = listDTOs.OrderByDescending(o => o.OffsetArea).ToList();
                        break;
                    case PriceListSortBy.OffsetAreaUnitPrice:
                        if (sortByParam.Ascending) listDTOs = listDTOs.OrderBy(o => o.OffsetAreaUnitPrice).ToList();
                        else listDTOs = listDTOs.OrderByDescending(o => o.OffsetAreaUnitPrice).ToList();
                        break;
                    case PriceListSortBy.DownPaymentPeriod:
                        if (sortByParam.Ascending) listDTOs = listDTOs.OrderBy(o => o.DownPaymentPeriod).ToList();
                        else listDTOs = listDTOs.OrderByDescending(o => o.DownPaymentPeriod).ToList();
                        break;
                    case PriceListSortBy.DownPaymentPerPeriod:
                        if (sortByParam.Ascending) listDTOs = listDTOs.OrderBy(o => o.DownPaymentPerPeriod).ToList();
                        else listDTOs = listDTOs.OrderByDescending(o => o.DownPaymentPerPeriod).ToList();
                        break;
                    case PriceListSortBy.PercentDownPayment:
                        if (sortByParam.Ascending) listDTOs = listDTOs.OrderBy(o => o.PercentDownPayment).ToList();
                        else listDTOs = listDTOs.OrderByDescending(o => o.PercentDownPayment).ToList();
                        break;
                    case PriceListSortBy.SpecialDown:
                        if (sortByParam.Ascending) listDTOs = listDTOs.OrderBy(o => o.SpecialDown).ToList();
                        else listDTOs = listDTOs.OrderByDescending(o => o.SpecialDown).ToList();
                        break;
                    case PriceListSortBy.SpecialDownPrice:
                        if (sortByParam.Ascending) listDTOs = listDTOs.OrderBy(o => o.SpecialDownPrice).ToList();
                        else listDTOs = listDTOs.OrderByDescending(o => o.SpecialDownPrice).ToList();
                        break;
                    case PriceListSortBy.OffsetAreaPrice:
                        if (sortByParam.Ascending) listDTOs = listDTOs.OrderBy(o => o.OffsetAreaPrice).ToList();
                        else listDTOs = listDTOs.OrderByDescending(o => o.OffsetAreaPrice).ToList();
                        break;
                    case PriceListSortBy.UnitStatus:
                        if (sortByParam.Ascending) listDTOs = listDTOs.OrderBy(o => o.UnitStatus?.Name).ToList();
                        else listDTOs = listDTOs.OrderByDescending(o => o.UnitStatus?.Name).ToList();
                        break;
                    case PriceListSortBy.Updated:
                        if (sortByParam.Ascending) listDTOs = listDTOs.OrderBy(o => o.Updated).ToList();
                        else listDTOs = listDTOs.OrderByDescending(o => o.Updated).ToList();
                        break;
                    case PriceListSortBy.UpdatedBy:
                        if (sortByParam.Ascending) listDTOs = listDTOs.OrderBy(o => o.UpdatedBy).ToList();
                        else listDTOs = listDTOs.OrderByDescending(o => o.UpdatedBy).ToList();
                        break;
                    default:
                        listDTOs = listDTOs.OrderBy(o => o.UnitNo).ToList();
                        break;
                }
            }
            else
            {
                listDTOs = listDTOs.OrderBy(o => o.UnitNo).ToList();
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
    public class PriceListQueryResult
    {
        public Unit Unit { get; set; }
        public PriceList PriceList { get; set; }
        public PriceListItemDTO BookingAmount { get; set; }
        public PriceListItemDTO TotalSalePrice { get; set; }
        public PriceListItemDTO ContractAmount { get; set; }
        public PriceListItemDTO DownAmount { get; set; }
        public string SpecialDown { get; set; }
        public string SpecialDownPrice { get; set; }
        public double? TitleDeedArea { get; set; }
        public double? PercentDownPayment { get; set; }
        public decimal? OffsetAreaUnitPrice { get; set; }
        public decimal? DownPaymentPerPeriod { get; set; }
        public double? OffsetArea { get; set; }
        public decimal? OffsetAreaPrice { get; set; }
        public MasterCenter UnitStatus { get; set; }
        public MasterCenter AssetType { get; set; }
    }
    public class TempPriceListQueryResult
    {
        public TitledeedDetail Titledeed { get; set; }
        public Unit Unit { get; set; }
        public PriceList PriceList { get; set; }
        public List<PriceListItemDTO> PriceListItems { get; set; }
        public MasterCenter UnitStatus { get; set; }
    }
}
