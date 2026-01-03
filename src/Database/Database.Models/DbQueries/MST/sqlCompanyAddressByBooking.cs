using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Linq;

namespace Database.Models.DbQueries.MST
{
    public static class sqlCompanyAddressByBooking
    {
        public static string QueryString = @"
            SELECT 'BookingID' = bk.ID
              , 'BookingNo' = bk.BookingNo
              , 'AgreementID' = ag.ID
              , 'AgreementNo' = ag.AgreementNo
              , 'CompanyNameTH' = c.NameTH
              , 'CompanyNameEN' = c.NameEN
              , 'CompanyHouseNoTH' = c.AddressTH
              , 'CompanyHouseNoEN' = c.AddressEN
              , 'CompanyBuildingTH' = c.BuildingTH
              , 'CompanyBuildingEN' = c.BuildingEN
              , 'CompanySoiTH' = c.SoiTH
              , 'CompanySoiEN' = c.SoiEN
              , 'CompanyRoadTH' = c.RoadTH
              , 'CompanyRoadEN' = c.RoadEN
              , 'CompanyProvinceEN' = prov.NameEN
              , 'CompanyProvinceTH' = prov.NameTH
              , 'CompanyDistrictEN' = dt.NameEN
              , 'CompanyDistrictTH' = dt.NameTH
              , 'CompanySubDistrictEN' = sdt.NameEN
              , 'CompanySubDistrictTH' = sdt.NameTH
              , 'CompanyPostalCode' = c.PostalCode
              , 'CompanyTelephone' = c.Telephone
              , 'CompanyFax' = c.Fax
              , 'ProjectNo' = prj.ProjectNo
              , 'ProjectName' = prj.ProjectNameTH
              , 'UnitNo' = un.UnitNo
              , 'CompanyTaxID' = c.TaxID
              , 'CompanyID' = c.ID
              , 'ProjectID' = prj.ID
              , 'UnitID' = un.ID
              , 'ProjectNameEN' = prj.ProjectNameTH
              , 'OwnerContactNo' = ISNULL(ago.ContactNo, bko.ContactNo)
              , 'DocumentNo' = ISNULL(ag.AgreementNo, bk.BookingNo)
            FROM SAL.Booking bk WITH (NOLOCK)
            LEFT JOIN SAL.BookingOwner bko WITH (NOLOCK) ON bko.BookingID = bk.ID AND bko.IsMainOwner = 1 AND bko.IsAgreementOwner = 0 AND bko.IsDeleted = 0
            LEFT JOIN SAL.Agreement ag WITH (NOLOCK) ON ag.BookingID = bk.ID AND ag.IsCancel = 0 AND ag.IsDeleted = 0
            LEFT JOIN SAL.AgreementOwner ago WITH (NOLOCK) ON ago.AgreementID = ag.ID AND ago.IsMainOwner = 1 AND ago.IsDeleted = 0
            LEFT JOIN PRJ.Unit un WITH (NOLOCK) ON un.ID = bk.UnitID
            LEFT JOIN PRJ.Project prj WITH (NOLOCK) ON prj.ID = bk.ProjectID
            LEFT JOIN MST.Company c WITH (NOLOCK) ON c.ID = prj.CompanyID
            LEFT JOIN MST.District dt WITH (NOLOCK) ON dt.ID = c.DistrictID
            LEFT JOIN MST.SubDistrict sdt WITH (NOLOCK) ON sdt.ID = c.SubDistrictID
            LEFT JOIN MST.Province prov WITH (NOLOCK) ON prov.ID = c.ProvinceID
            WHERE 1=1";
        //WHERE bk.ID = '66C96DFF-4747-4F9A-9128-0C14841775FC'";

        public static List<SqlParameter> QueryFilter(ref string QueryString, Guid BookingID)
        {
            List<SqlParameter> ParamList = new List<SqlParameter>();

            if (BookingID != Guid.Empty)
            {
                ParamList.Add(new SqlParameter("@prmBookingID", BookingID));
                QueryString += " AND bk.ID = @prmBookingID";
            }

            return ParamList;
        }

        public static List<SqlParameter> QueryFilterByList(ref string QueryString, List<Guid> BookingIDs)
        {
            List<SqlParameter> ParamList = new List<SqlParameter>();

            for (var i = 1; i <= BookingIDs.Count; i++)
            {
                ParamList.Add(new SqlParameter($"@BookingID{i.ToString()}", BookingIDs[i - 1]));
            }

            QueryString += string.Format(" AND bk.ID IN ({0})", string.Join(",", ParamList.Select(o => o.ParameterName).ToList()));

            return ParamList;
        }

        public class QueryResult
        {
            public Guid? BookingID { get; set; }
            public string BookingNo { get; set; }
            public Guid? AgreementID { get; set; }
            public string AgreementNo { get; set; }
            public string CompanyNameTH { get; set; }
            public string CompanyNameEN { get; set; }
            public string CompanyHouseNoTH { get; set; }
            public string CompanyHouseNoEN { get; set; }
            public string CompanyBuildingTH { get; set; }
            public string CompanyBuildingEN { get; set; }
            public string CompanySoiTH { get; set; }
            public string CompanySoiEN { get; set; }
            public string CompanyRoadTH { get; set; }
            public string CompanyRoadEN { get; set; }
            public string CompanyProvinceEN { get; set; }
            public string CompanyProvinceTH { get; set; }
            public string CompanyDistrictEN { get; set; }
            public string CompanyDistrictTH { get; set; }
            public string CompanySubDistrictEN { get; set; }
            public string CompanySubDistrictTH { get; set; }
            public string CompanyPostalCode { get; set; }
            public string CompanyTelephone { get; set; }
            public string CompanyFax { get; set; }
            public string ProjectNo { get; set; }
            public string ProjectName { get; set; }
            public string UnitNo { get; set; }
            public string CompanyTaxID { get; set; }
            public Guid? CompanyID { get; set; }
            public Guid? ProjectID { get; set; }
            public Guid? UnitID { get; set; }
            public string ProjectNameEN { get; set; }
            public string OwnerContactNo { get; set; }
            public string DocumentNo { get; set; } = "";
        }

    }
}


