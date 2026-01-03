using Database.Models.MasterKeys;
using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Linq;

namespace Database.Models.DbQueries.CTM
{
    public static class sqlContactAddressByProject
    {
        public static string QueryString = $@"
	        SELECT 'ContactID' = ct.ID
	          , 'ContactNo' = ct.ContactNo
              , 'ContactTitle' = (CASE WHEN titleTH.[Key] = '-1' THEN ct.TitleExtTH ELSE titleTH.Name END)
	          , 'ContactFirstNameTH' = ct.FirstNameTH
	          , 'ContactMiddleNameTH' = ct.MiddleNameTH
	          , 'ContactLastNameTH' = ct.LastNameTH              
	          , 'ContactTitleExtEN' = ct.TitleExtEN
	          , 'ContactFirstNameEN' = ct.FirstNameEN
	          , 'ContactMiddleNameEN' = ct.MiddleNameEN
	          , 'ContactLastNameEN' = ct.LastNameEN
	          , 'ContactHouseNoTH' = cta.HouseNoTH
	          , 'ContactMooTH' = cta.MooTH
	          , 'ContactVillageTH' = cta.VillageTH
	          , 'ContactSoiTH' = cta.SoiTH
	          , 'ContactRoadTH' = cta.RoadTH
	          , 'ContactHouseNoEN' = cta.HouseNoEN
	          , 'ContactMooEN' = cta.MooEN
	          , 'ContactVillageEN' = cta.VillageEN
	          , 'ContactSoiEN' = cta.SoiEN
	          , 'ContactRoadEN' = cta.RoadEN
	          , 'ContactPostalCode' = cta.PostalCode
	          , 'ContactCountryTH' = cunt.NameTH
	          , 'ContactCountryEN' = cunt.NameEN
              , 'ContactProvinceTH' = (CASE WHEN ct.IsThaiNationality = 1 OR cunt.Code = 'TH' THEN prov.NameTH ELSE cta.ForeignProvince END)
	          , 'ContactProvinceEN' = (CASE WHEN ct.IsThaiNationality = 1 OR cunt.Code = 'TH' THEN prov.NameEN ELSE cta.ForeignProvince END)
	          , 'ContactDistrictTH' = (CASE WHEN ct.IsThaiNationality = 1 OR cunt.Code = 'TH' THEN dt.NameTH ELSE cta.ForeignDistrict END)
	          , 'ContactDistrictEN' = (CASE WHEN ct.IsThaiNationality = 1 OR cunt.Code = 'TH' THEN dt.NameEN ELSE cta.ForeignDistrict END)
	          , 'ContactSubDistrictTH' = (CASE WHEN ct.IsThaiNationality = 1 OR cunt.Code = 'TH' THEN sdt.NameTH ELSE cta.ForeignSubDistrict END)
	          , 'ContactSubDistrictEN' = (CASE WHEN ct.IsThaiNationality = 1 OR cunt.Code = 'TH' THEN sdt.NameEN ELSE cta.ForeignSubDistrict END)
	          , 'ContactName' = ct.FullnameTH
              , 'ProjectID' = ctap.ProjectID
              , 'ContactAddressCreated' = cta.Created
	        FROM CTM.Contact ct WITH (NOLOCK)
	        LEFT JOIN MST.MasterCenter titleTH WITH (NOLOCK) ON titleTH.ID = ct.ContactTitleTHMasterCenterID
	        LEFT JOIN CTM.ContactAddress cta WITH (NOLOCK) ON cta.ContactID = ct.ID AND cta.IsDeleted = 0
	        LEFT JOIN CTM.ContactAddressProject ctap WITH (NOLOCK) ON ctap.ContactAddressID = cta.ID AND ctap.IsDeleted = 0
	        LEFT JOIN MST.District dt WITH (NOLOCK) ON dt.ID = cta.DistrictID
	        LEFT JOIN MST.SubDistrict sdt WITH (NOLOCK) ON sdt.ID = cta.SubDistrictID
	        LEFT JOIN MST.Province prov WITH (NOLOCK) ON prov.ID = cta.ProvinceID
	        LEFT JOIN MST.Country cunt WITH (NOLOCK) ON cunt.ID = cta.CountryID
            LEFT JOIN MST.MasterCenter mstAddType WITH (NOLOCK) ON mstAddType.ID = cta.ContactAddressTypeMasterCenterID
            WHERE 1=1 AND mstAddType.[Key] = '{ContactAddressTypeKeys.Contact}'";

        //WHERE ct.ContactNo = '102000070'
        // AND ctap.ProjectID = '5005B1EA-664C-4480-89B9-514348E274F3'";

        public static List<SqlParameter> QueryFilter(ref string QueryString, string ContactNo, Guid ProjectID)
        {
            List<SqlParameter> ParamList = new List<SqlParameter>();

            if (!string.IsNullOrEmpty(ContactNo))
            {
                ParamList.Add(new SqlParameter("@prmContactNo", ContactNo));
                QueryString += " AND ct.ContactNo = @prmContactNo";
            }

            if (ProjectID != Guid.Empty)
            {
                ParamList.Add(new SqlParameter("@prmProjectID", ProjectID));
                QueryString += " AND ctap.ProjectID = @prmProjectID";
            }

            return ParamList;
        }

        public static List<SqlParameter> QueryFilterByList(ref string QueryString, List<string> ContactNos, List<Guid> ProjectIDs)
        {
            List<SqlParameter> AllParamList = new List<SqlParameter>();

            if ((ContactNos ?? new List<string>()).Count > 0)
            {
                List<SqlParameter> ParamList = new List<SqlParameter>();

                for (var i = 1; i <= ContactNos.Count; i++)
                {
                    ParamList.Add(new SqlParameter($"@ContactNo{i.ToString()}", ContactNos[i - 1]));
                }

                QueryString += string.Format(" AND ct.ContactNo IN ({0})", string.Join(",", ParamList.Select(o => o.ParameterName).ToList()));

                AllParamList.AddRange(ParamList);
            }

            if ((ProjectIDs ?? new List<Guid>()).Count > 0)
            {
                List<SqlParameter> ParamList = new List<SqlParameter>();

                for (var i = 1; i <= ProjectIDs.Count; i++)
                {
                    ParamList.Add(new SqlParameter($"@ProjectID{i.ToString()}", ProjectIDs[i - 1]));
                }

                QueryString += string.Format(" AND ctap.ProjectID IN ({0})", string.Join(",", ParamList.Select(o => o.ParameterName).ToList()));

                AllParamList.AddRange(ParamList);
            }

            return AllParamList;
        }

        public class QueryResult
        {
            public Guid? ContactID { get; set; }
            public string ContactNo { get; set; }
            public string ContactTitle { get; set; }
            public string ContactFirstNameTH { get; set; }
            public string ContactMiddleNameTH { get; set; }
            public string ContactLastNameTH { get; set; }
            public string ContactTitleExtEN { get; set; }
            public string ContactFirstNameEN { get; set; }
            public string ContactMiddleNameEN { get; set; }
            public string ContactLastNameEN { get; set; }
            public string ContactHouseNoTH { get; set; }
            public string ContactMooTH { get; set; }
            public string ContactVillageTH { get; set; }
            public string ContactSoiTH { get; set; }
            public string ContactRoadTH { get; set; }
            public string ContactHouseNoEN { get; set; }
            public string ContactMooEN { get; set; }
            public string ContactVillageEN { get; set; }
            public string ContactSoiEN { get; set; }
            public string ContactRoadEN { get; set; }
            public string ContactPostalCode { get; set; }
            public string ContactCountryTH { get; set; }
            public string ContactCountryEN { get; set; }
            public string ContactProvinceTH { get; set; }
            public string ContactProvinceEN { get; set; }
            public string ContactDistrictTH { get; set; }
            public string ContactDistrictEN { get; set; }
            public string ContactSubDistrictTH { get; set; }
            public string ContactSubDistrictEN { get; set; }
            public string ContactName { get; set; }

            public Guid? ProjectID { get; set; }

            public DateTime? ContactAddressCreated { get; set; }
        }

        public class SuccessResult
        {
            public string ContactNo { get; set; }

            public Guid? ProjectID { get; set; }
        }
    }
}


