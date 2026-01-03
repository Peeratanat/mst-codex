using Database.Models;
using Database.Models.MST;
using Database.Models.USR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Base.DTOs.MST
{
    public class CompanyDTO : BaseDTO
    {
        /// <summary>
        /// รหัสบริษัท
        /// </summary>
        public string APAuthorizeRefID { get; set; }
        /// <summary>
        /// ตัวย่อ
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// ชื่อบริษัทภาษาไทย
        /// </summary>
        public string NameTH { get; set; }
        /// <summary>
        /// ชื่อบริษัทภาษาอังกฤษ
        /// </summary>
        public string NameEN { get; set; }
        /// <summary>
        /// ประจำตัวผู้เสียภาษี
        /// </summary>
        public string TaxID { get; set; }
        /// <summary>
        /// ที่อยู่ภาษาไทย
        /// </summary>
        public string AddressTH { get; set; }
        /// <summary>
        /// ที่อยู่ภาษาอังกฤษ
        /// </summary>
        public string AddressEN { get; set; }
        /// <summary>
        /// ชื่อตึกภาษาไทย
        /// </summary>
        public string BuildingTH { get; set; }
        /// <summary>
        /// ชื่อตึกภาษาอังกฤษ
        /// </summary>
        public string BuildingEN { get; set; }
        /// <summary>
        /// ซอยภาษาไทย
        /// </summary>
        public string SoiTH { get; set; }
        /// <summary>
        /// ซอยภาษาอังกฤษ
        /// </summary>
        public string SoiEN { get; set; }
        /// <summary>
        /// ถนนภาษาไทย
        /// </summary>
        public string RoadTH { get; set; }
        /// <summary>
        /// ถนนภาษาอังกฤษ
        /// </summary>
        public string RoadEN { get; set; }
        /// <summary>
        /// Master/SubDistricts/DropdownList
        /// ตำบล
        /// </summary>
        public SubDistrictListDTO SubDistrict { get; set; }
        /// <summary>
        /// Master/Districts/DropdownList
        /// อำเภอ
        /// </summary>
        public DistrictListDTO District { get; set; }
        /// <summary>
        /// Master/Provinces/DropdownList
        /// จังหวัด
        /// </summary>
        public ProvinceListDTO Province { get; set; }
        /// <summary>
        /// รหัสไปรษณีย์
        /// </summary>
        public string PostalCode { get; set; }
        /// <summary>
        /// เบอร์โทร
        /// </summary>
        public string Telephone { get; set; }
        /// <summary>
        /// เบอร์แฟ๊กซ์
        /// </summary>
        public string Fax { get; set; }
        /// <summary>
        /// เว๊ปไซส์
        /// </summary>
        public string Website { get; set; }
        /// <summary>
        /// รหัสบริษัทใน SAP
        /// </summary>
        public string SAPCompanyID { get; set; }
        /// <summary>
        /// ชื่อเก่าภาษาไทย
        /// </summary>
        public string NameTHOld { get; set; }
        /// <summary>
        /// ชื่อเก่าภาษอังกฤษ
        /// </summary>
        public string NameENOld { get; set; }
        /// <summary>
        /// เปิดใช้ที่ระบบ CRM
        /// </summary>
        public bool IsUseInCRM { get; set; }
        /// <summary>
        /// สถานะบริษัท
        /// </summary>
        public bool IsActive { get; set; }

        public static CompanyDTO CreateFromModel(Company model)
        {
            if (model != null)
            {
                var result = new CompanyDTO()
                {
                    Id = model.ID,
                    APAuthorizeRefID = model.APAuthorizeRefID,
                    Code = model.Code,
                    NameTH = model.NameTH,
                    NameEN = model.NameEN,
                    TaxID = model.TaxID,
                    AddressTH = model.AddressTH,
                    AddressEN = model.AddressEN,
                    BuildingTH = model.BuildingTH,
                    BuildingEN = model.BuildingEN,
                    SoiTH = model.SoiTH,
                    SoiEN = model.SoiEN,
                    RoadEN = model.RoadEN,
                    RoadTH = model.RoadTH,
                    PostalCode = model.PostalCode,
                    Telephone = model.Telephone,
                    Fax = model.Fax,
                    Website = model.Website,
                    SAPCompanyID = model.SAPCompanyID,
                    NameENOld = model.NameENOld,
                    NameTHOld = model.NameTHOld,
                    Updated = model.Updated,
                    UpdatedBy = model.UpdatedBy?.DisplayName,
                    District = DistrictListDTO.CreateFromModel(model.District),
                    Province = ProvinceListDTO.CreateFromModel(model.Province),
                    SubDistrict = SubDistrictListDTO.CreateFromModel(model.SubDistrict),
                    IsUseInCRM = model.IsUseInCRM,
                    IsActive = model.IsActive
                };

                return result;
            }
            else
            {
                return null;
            }
        }

        public static CompanyDTO CreateFromQueryResult(CompanyQueryResult model)
        {
            if (model != null)
            {
                var result = new CompanyDTO()
                {
                    Id = model.Company.ID,
                    APAuthorizeRefID = model.Company.APAuthorizeRefID,
                    Code = model.Company.Code,
                    NameTH = model.Company.NameTH,
                    NameEN = model.Company.NameEN,
                    TaxID = model.Company.TaxID,
                    AddressTH = model.Company.AddressTH,
                    AddressEN = model.Company.AddressEN,
                    BuildingTH = model.Company.BuildingTH,
                    BuildingEN = model.Company.BuildingEN,
                    SoiTH = model.Company.SoiTH,
                    SoiEN = model.Company.SoiEN,
                    RoadEN = model.Company.RoadEN,
                    RoadTH = model.Company.RoadTH,
                    PostalCode = model.Company.PostalCode,
                    Telephone = model.Company.Telephone,
                    Fax = model.Company.Fax,
                    Website = model.Company.Website,
                    SAPCompanyID = model.Company.SAPCompanyID,
                    NameENOld = model.Company.NameENOld,
                    NameTHOld = model.Company.NameTHOld,
                    Updated = model.Company.Updated,
                    UpdatedBy = model.UpdatedBy?.DisplayName,
                    District = DistrictListDTO.CreateFromModel(model.District),
                    Province = ProvinceListDTO.CreateFromModel(model.Province),
                    SubDistrict = SubDistrictListDTO.CreateFromModel(model.SubDistrict),
                    IsUseInCRM = model.Company.IsUseInCRM,
                    IsActive = model.Company.IsActive
                };

                return result;
            }
            else
            {
                return null;
            }
        }

        public static void SortBy(CompanySortByParam sortByParam, ref IQueryable<CompanyQueryResult> query)
        {
            if (sortByParam.SortBy != null)
            {
                switch (sortByParam.SortBy.Value)
                {

                    case CompanySortBy.APAuthorizeRefID:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.Company.APAuthorizeRefID);
                        else query = query.OrderByDescending(o => o.Company.APAuthorizeRefID);
                        break;
                    case CompanySortBy.Code:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.Company.Code);
                        else query = query.OrderByDescending(o => o.Company.Code);
                        break;
                    case CompanySortBy.NameTH:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.Company.NameTH);
                        else query = query.OrderByDescending(o => o.Company.NameTH);
                        break;
                    case CompanySortBy.NameEN:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.Company.NameEN);
                        else query = query.OrderByDescending(o => o.Company.NameEN);
                        break;
                    case CompanySortBy.TaxID:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.Company.TaxID);
                        else query = query.OrderByDescending(o => o.Company.TaxID);
                        break;
                    case CompanySortBy.AddressTH:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.Company.AddressTH);
                        else query = query.OrderByDescending(o => o.Company.AddressTH);
                        break;
                    case CompanySortBy.AddressEN:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.Company.AddressEN);
                        else query = query.OrderByDescending(o => o.Company.AddressEN);
                        break;
                    case CompanySortBy.BuildingTH:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.Company.BuildingTH);
                        else query = query.OrderByDescending(o => o.Company.BuildingTH);
                        break;
                    case CompanySortBy.BuildingEN:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.Company.BuildingEN);
                        else query = query.OrderByDescending(o => o.Company.BuildingEN);
                        break;
                    case CompanySortBy.SoiTH:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.Company.SoiTH);
                        else query = query.OrderByDescending(o => o.Company.SoiTH);
                        break;
                    case CompanySortBy.SoiEN:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.Company.SoiEN);
                        else query = query.OrderByDescending(o => o.Company.SoiEN);
                        break;
                    case CompanySortBy.RoadEN:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.Company.RoadEN);
                        else query = query.OrderByDescending(o => o.Company.RoadEN);
                        break;
                    case CompanySortBy.RoadTH:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.Company.RoadTH);
                        else query = query.OrderByDescending(o => o.Company.RoadTH);
                        break;
                    case CompanySortBy.PostalCode:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.Company.PostalCode);
                        else query = query.OrderByDescending(o => o.Company.PostalCode);
                        break;
                    case CompanySortBy.Telephone:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.Company.Telephone);
                        else query = query.OrderByDescending(o => o.Company.Telephone);
                        break;
                    case CompanySortBy.Fax:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.Company.Fax);
                        else query = query.OrderByDescending(o => o.Company.Fax);
                        break;
                    case CompanySortBy.Website:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.Company.Website);
                        else query = query.OrderByDescending(o => o.Company.Website);
                        break;
                    case CompanySortBy.SAPCompanyID:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.Company.SAPCompanyID);
                        else query = query.OrderByDescending(o => o.Company.SAPCompanyID);
                        break;
                    case CompanySortBy.NameENOld:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.Company.NameENOld);
                        else query = query.OrderByDescending(o => o.Company.NameENOld);
                        break;
                    case CompanySortBy.NameTHOld:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.Company.NameTHOld);
                        else query = query.OrderByDescending(o => o.Company.NameTHOld);
                        break;
                    case CompanySortBy.Province:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.Province.NameTH);
                        else query = query.OrderByDescending(o => o.Province.NameTH);
                        break;
                    case CompanySortBy.District:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.District.NameTH);
                        else query = query.OrderByDescending(o => o.District.NameTH);
                        break;
                    case CompanySortBy.SubDistrict:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.SubDistrict.NameTH);
                        else query = query.OrderByDescending(o => o.SubDistrict.NameTH);
                        break;
                    case CompanySortBy.Updated:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.Company.Updated);
                        else query = query.OrderByDescending(o => o.Company.Updated);
                        break;
                    case CompanySortBy.UpdatedBy:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.UpdatedBy.DisplayName);
                        else query = query.OrderByDescending(o => o.UpdatedBy.DisplayName);
                        break;
                    case CompanySortBy.IsUseInCRM:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.Company.IsUseInCRM);
                        else query = query.OrderByDescending(o => o.Company.IsUseInCRM);
                        break;
                    default:
                        query = query.OrderBy(o => o.Company.NameTH);
                        break;
                }
            }
            else
            {
                query = query.OrderBy(o => o.Company.NameTH);
            }
        }


        public void ToModel(ref Company model)
        {
            model.APAuthorizeRefID = APAuthorizeRefID;
            model.Code = Code;
            model.NameTH = NameTH;
            model.NameEN = NameEN;
            model.TaxID = TaxID;
            model.AddressTH = AddressTH;
            model.AddressEN = AddressEN;
            model.BuildingTH = BuildingTH;
            model.BuildingEN = BuildingEN;
            model.SoiTH = SoiTH;
            model.SoiEN = SoiEN;
            model.RoadEN = RoadEN;
            model.RoadTH = RoadTH;
            model.PostalCode = PostalCode;
            model.SubDistrictID = SubDistrict?.Id;
            model.DistrictID = District?.Id;
            model.ProvinceID = Province?.Id;
            model.Telephone = Telephone;
            model.Fax = Fax;
            model.Website = Website;
            model.SAPCompanyID = SAPCompanyID;
            model.NameENOld = NameENOld;
            model.NameTHOld = NameTHOld;
            model.IsUseInCRM = IsUseInCRM;
            model.IsActive = IsActive;
        }
    }
    public class CompanyQueryResult
    {
        public Company Company { get; set; }
        public SubDistrict SubDistrict { get; set; }
        public Province Province { get; set; }
        public District District { get; set; }
        public User UpdatedBy { get; set; }
    }
}
