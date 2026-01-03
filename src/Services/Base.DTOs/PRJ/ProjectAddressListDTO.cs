using System;
using System.Collections.Generic;
using System.Linq;
using Base.DTOs.MST;
using Database.Models.MST;
using Database.Models.PRJ;
using Database.Models.USR;

namespace Base.DTOs.PRJ
{
    public class ProjectAddressListDTO : BaseDTO
    {
        /// <summary>
        /// ประเภทที่ตั้ง
        /// Master/api/MasterCenters?masterCenterGroupKey=ProjectAddressType
        /// </summary>
        public MasterCenterDropdownDTO ProjectAddressType { get; set; }
        /// <summary>
        /// ชื่อที่ตั้ง (TH)
        /// </summary>
        /// <value>The address name th.</value>
        public string AddressNameTH { get; set; }
        /// <summary>
        /// ชื่อที่ตั้ง (EN)
        /// </summary>
        /// <value>The address name en.</value>
        public string AddressNameEN { get; set; }
        /// <summary>
        /// เลขที่โฉนด (comma separated)
        /// </summary>
        /// <value>The title deed no.</value>
        public string TitleDeedNo { get; set; }
        /// <summary>
        /// เลขที่ดิน (comma separated)
        /// </summary>
        /// <value>The land no.</value>
        public string LandNo { get; set; }
        /// <summary>
        /// หน้าสำรวจ (comma separated)
        /// </summary>
        /// <value>The inspection no.</value>
        public string InspectionNo { get; set; }
        /// <summary>
        /// รหัสไปรษณีย์
        /// </summary>
        public string PostalCode { get; set; }
        /// <summary>
        /// จังหวัด
        /// Master/api/Provinces/DropdownList
        /// </summary>
        public ProvinceListDTO Province { get; set; }
        /// <summary>
        /// อำเภอ
        /// Master/api/Districts/DropdownList
        /// </summary>
        public DistrictListDTO District { get; set; }
        /// <summary>
        /// ตำบล
        /// Master/api/SubDistricts/DropdownList
        /// </summary>
        public SubDistrictListDTO SubDistrict { get; set; }
        /// <summary>
        /// ตำบล ข้อมูลตามโฉนด
        /// </summary>
        public SubDistrictListDTO TitledeedSubDistrict { get; set; }

        public static ProjectAddressListDTO CreateFromModel(Address model)
        {
            if (model != null)
            {
                var result = new ProjectAddressListDTO()
                {
                    Id = model.ID,
                    Updated = model.Updated,
                    UpdatedBy = model.UpdatedBy?.DisplayName,
                    ProjectAddressType = MasterCenterDropdownDTO.CreateFromModel(model.ProjectAddressType),
                    AddressNameEN = model.AddressNameEN,
                    AddressNameTH = model.AddressNameTH,
                    TitleDeedNo = model.TitleDeedNo,
                    LandNo = model.LandNo,
                    InspectionNo = model.InspectionNo,
                    Province = ProvinceListDTO.CreateFromModel(model.Province),
                    District = DistrictListDTO.CreateFromModel(model.District),
                    SubDistrict = SubDistrictListDTO.CreateFromModel(model.SubDistrict),
                    PostalCode = model.PostalCode
                };
                return result;
            }
            else
            {
                return null;
            }
        }
        public static ProjectAddressListDTO CreateFromQueryResult(ProjectAddressQueryResult model)
        {
            if (model != null)
            {
                var result = new ProjectAddressListDTO()
                {
                    Id = model.Address.ID,
                    Updated = model.Address.Updated,
                    UpdatedBy = model.UpdatedBy?.DisplayName,
                    AddressNameEN = model.Address.AddressNameEN,
                    AddressNameTH = model.Address.AddressNameTH,
                    TitleDeedNo = model.Address.TitleDeedNo,
                    LandNo = model.Address.LandNo,
                    InspectionNo = model.Address.InspectionNo,
                    PostalCode = model.Address.PostalCode,
                    ProjectAddressType = MasterCenterDropdownDTO.CreateFromModel(model.ProjectAddressType),
                    District = DistrictListDTO.CreateFromModel(model.District),
                    Province = ProvinceListDTO.CreateFromModel(model.Province),
                    SubDistrict = SubDistrictListDTO.CreateFromModel(model.SubDistrict),
                    //TitledeedSubDistrict = SubDistrictListDTO.CreateFromModel(model.TitledeedSubDistrict),
                };

                return result;
            }
            else
            {
                return null;
            }
        }

        public static void SortBy(SortByParam sortByParam, ref IQueryable<ProjectAddressQueryResult> query)
        {
            if (!string.IsNullOrEmpty(sortByParam.SortBy))
            {
                if (sortByParam.SortBy.Equals("AddressNameTH", StringComparison.InvariantCultureIgnoreCase))
                {
                    if (sortByParam.Ascending) query = query.OrderBy(o => o.Address.AddressNameTH);
                    else query = query.OrderByDescending(o => o.Address.AddressNameTH);
                }
                else if (sortByParam.SortBy.Equals("TitleDeedNo", StringComparison.InvariantCultureIgnoreCase))
                {
                    if (sortByParam.Ascending) query = query.OrderBy(o => o.Address.TitleDeedNo);
                    else query = query.OrderByDescending(o => o.Address.TitleDeedNo);
                }
                else if (sortByParam.SortBy.Equals("LandNo", StringComparison.InvariantCultureIgnoreCase))
                {
                    if (sortByParam.Ascending) query = query.OrderBy(o => o.Address.LandNo);
                    else query = query.OrderByDescending(o => o.Address.LandNo);
                }
                else if (sortByParam.SortBy.Equals("InspectionNo", StringComparison.InvariantCultureIgnoreCase))
                {
                    if (sortByParam.Ascending) query = query.OrderBy(o => o.Address.InspectionNo);
                    else query = query.OrderByDescending(o => o.Address.InspectionNo);
                }
                else if (sortByParam.SortBy.Equals("SubDistrict", StringComparison.InvariantCultureIgnoreCase))
                {
                    if (sortByParam.Ascending) query = query.OrderBy(o => o.SubDistrict.NameTH);
                    else query = query.OrderByDescending(o => o.SubDistrict.NameTH);
                }
                else if (sortByParam.SortBy.Equals("District", StringComparison.InvariantCultureIgnoreCase))
                {
                    if (sortByParam.Ascending) query = query.OrderBy(o => o.District.NameTH);
                    else query = query.OrderByDescending(o => o.District.NameTH);
                }
                else if (sortByParam.SortBy.Equals("Province", StringComparison.InvariantCultureIgnoreCase))
                {
                    if (sortByParam.Ascending) query = query.OrderBy(o => o.Province.NameTH);
                    else query = query.OrderByDescending(o => o.Province.NameTH);
                }
                else if (sortByParam.SortBy.Equals("PostalCode", StringComparison.InvariantCultureIgnoreCase))
                {
                    if (sortByParam.Ascending) query = query.OrderBy(o => o.Address.PostalCode);
                    else query = query.OrderByDescending(o => o.Address.PostalCode);
                }
                else if (sortByParam.SortBy.Equals("ProjectAddressType", StringComparison.InvariantCultureIgnoreCase))
                {
                    if (sortByParam.Ascending) query = query.OrderBy(o => o.ProjectAddressType.Key);
                    else query = query.OrderByDescending(o => o.ProjectAddressType.Key);
                }
                else
                {
                    query = query.OrderBy(o => o.Address.AddressNameTH);
                }
            }
            else
            {
                query = query.OrderBy(o => o.Address.AddressNameTH);
            }
        }

    }
    public class ProjectAddressQueryResult
    {
        public Address Address { get; set; }
        public District District { get; set; }
        public Province Province { get; set; }
        public SubDistrict SubDistrict { get; set; }
        public SubDistrict TitledeedSubDistrict { get; set; }
        public MasterCenter ProjectAddressType { get; set; }
        public User UpdatedBy { get; set; }
    }
}
