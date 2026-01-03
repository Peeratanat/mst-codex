using System;
using System.Collections.Generic;
using System.Text;

namespace Base.DTOs.MST
{
    public class CompanySortByParam
    {
        public CompanySortBy? SortBy { get; set; }
        public bool Ascending { get; set; }
    }

    public enum CompanySortBy
    {
        APAuthorizeRefID,
        Code,
        NameTH,
        NameEN,
        TaxID,
        AddressTH,
        AddressEN,
        BuildingTH,
        BuildingEN,
        SoiTH,
        SoiEN,
        RoadEN,
        RoadTH,
        PostalCode,
        Telephone,
        Fax,
        Website,
        SAPCompanyID,
        NameENOld,
        NameTHOld,
        Province,
        District,
        SubDistrict,
        Updated,
        UpdatedBy,
        IsUseInCRM
    }
}
