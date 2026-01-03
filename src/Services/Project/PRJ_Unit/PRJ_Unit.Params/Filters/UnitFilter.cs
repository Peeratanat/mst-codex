using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PRJ_Unit.Params.Filters
{
    public class UnitFilter : BaseFilter
    {
        public Guid? AddressID { get; set; }
        public string UnitNo { get; set; }
        public string HouseNo { get; set; }
        public string ModelCode { get; set; }
        public Guid? TypeOfRealEstateID { get; set; }
        public string ModelName { get; set; }
        public Guid? TowerID { get; set; }
        public Guid? FloorID { get; set; }
        public string UnitDirectionKey { get; set; }
        public string UnitTypeKey { get; set; }
        public string AssetTypeKey { get; set; }
        public string UnitStatusKey { get; set; }
        public double? SaleAreaFrom { get; set; }
        public double? SaleAreaTo { get; set; }
        public double? TitleDeedAreaFrom { get; set; }
        public double? TitleDeedAreaTo { get; set; }
        public double? NumberOfPrivilegeFrom { get; set; }
        public double? NumberOfPrivilegeTo { get; set; }
        public double? NumberOfParkingFixFrom { get; set; }
        public double? NumberOfParkingFixTo { get; set; }
        public double? NumberOfParkingUnFixFrom { get; set; }
        public double? NumberOfParkingUnFixTo { get; set; }
        public string isForeignUnit { get; set; }
        public bool? IsAddressProject { get; set; }
    }
}
