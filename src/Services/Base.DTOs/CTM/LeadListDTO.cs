using Database.Models.CTM;
using System;
using System.Collections.Generic;
using System.Linq;
using models = Database.Models;

namespace Base.DTOs.CTM
{
    public class LeadListDTO
    {
        /// <summary>
        /// ID ของ Lead
        /// </summary>
        public Guid? Id { get; set; }
        /// <summary>
        /// ชื่อจริง
        /// </summary>
        public string FirstName { get; set; }
        /// <summary>
        /// นามสกุล
        /// </summary>
        public string LastName { get; set; }
        /// <summary>
        /// เบอร์โทรศัพท์
        /// </summary>
        public string PhoneNumber { get; set; }
        /// <summary>
        /// ประเภทของ Lead
        /// masterdata/api/MasterCenters/DropdownList?masterCenterGroupKey=LeadType
        /// </summary>
        public MST.MasterCenterDropdownDTO LeadType { get; set; }
        /// <summary>
        /// สื่อโฆษณา (มาจากสื่อ)
        /// masterdata/api/MasterCenters/DropdownList?masterCenterGroupKey=Advertisement
        /// </summary>
        public MST.MasterCenterDropdownDTO Advertisement { get; set; }
        /// <summary>
        /// ผู้ดูแล Lead
        /// </summary>
        public USR.UserListDTO Owner { get; set; }
        /// <summary>
        /// โครงการ
        /// project/api/Projects/DropdownList
        /// </summary>
        public PRJ.ProjectDTO Project { get; set; }
        /// <summary>
        /// วันที่สร้าง
        /// </summary>
        public DateTime? CreatedDate { get; set; }
        /// <summary>
        /// หมายเหตุ
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// มีคุณสมบัติเป็นContact
        /// </summary>
        public bool IsQualify { get; set; }
        /// <summary>
        /// LeadDate
        /// </summary>
        public DateTime? LeadDate { get; set; }

        public bool IsHidePhone { get; set; }

        public bool IsAgency { get; set; }

        public MST.MasterCenterDropdownDTO LeadStatus { get; set; }

        public string RefID { get; set; }

        public bool IsDeleted { get; set; }

        public string UnitNo { get; set; }

        public string SubLeadType { get; set; }

    public static LeadListDTO CreateFromQueryResult(LeadQueryResult model)
        {
            if (model != null)
            {
                LeadListDTO result = new LeadListDTO()
                {
                    Id = model.Lead.ID,
                    FirstName = model.Lead.FirstName,
                    LastName = model.Lead.LastName,
                    PhoneNumber = model.Lead.PhoneNumber,
                    LeadType = MST.MasterCenterDropdownDTO.CreateFromModel(model.LeadType),
                    Advertisement = MST.MasterCenterDropdownDTO.CreateFromModel(model.Advertisement),
                    Owner = USR.UserListDTO.CreateFromModel(model.Owner),
                    CreatedDate = model.Lead.Created,
                    Project = PRJ.ProjectDTO.CreateFromModel(model.Project),
                    Remark = model.Lead.Remark,
                    IsQualify = ChkIsQualify(model.Lead.FirstName, model.Lead.LastName),
                    LeadDate = model.Lead.LeadDate,
                    LeadStatus = MST.MasterCenterDropdownDTO.CreateFromModel(model.LeadStatus),
                    RefID = model.Lead.RefID
                };


                var projectnos = new List<string>() {
                    "40044", "60021", "60025", "60031", "70002", "70020", "70037", "70039", "70045"
                };

                if (result.PhoneNumber != null)
                {
                    result.IsHidePhone = true;
                    //foreach (var o in projectnos)
                    //{
                    //    if (result.Project.ProjectNo == o)
                    //    {
                    //        result.IsHidePhone = true;
                    //        break;
                    //    }
                    //    if (result.Project.ProjectNo == o)
                    //    {
                    //        result.IsHidePhone = false;

                    //    }

                    //}
                }

                return result;
            }
            else
            {
                return null;
            }
        }

        public static LeadListDTO CreateHidePhoneFromQueryResult(LeadQueryResult model, List<LeadActivity> leadActivitiess, bool isUserRoleBC)
        {
            if (model != null)
            {
                LeadListDTO result = new LeadListDTO()
                {
                    Id = model.Lead.ID,
                    FirstName = model.Lead.FirstName,
                    LastName = model.Lead.LastName,
                    PhoneNumber = model.Lead.PhoneNumber,
                    LeadType = MST.MasterCenterDropdownDTO.CreateMasterLeadTypeFromModel(model.LeadType),
                    Advertisement = MST.MasterCenterDropdownDTO.CreateFromModel(model.Advertisement),
                    Owner = USR.UserListDTO.CreateFromModel(model.Owner),
                    CreatedDate = model.Lead.Created,
                    Project = PRJ.ProjectDTO.CreateFromModel(model.Project),
                    Remark = model.Lead.Remark,
                    IsQualify = ChkIsQualify(model.Lead.FirstName, model.Lead.LastName),
                    LeadDate = model.Lead.LeadDate,
                    IsAgency = model.Lead.RefMigrateID3 == "Lead Agency" ? true : false,
                    LeadStatus = MST.MasterCenterDropdownDTO.CreateFromModel(model.LeadStatus),
                    RefID = model.Lead.RefID,
                    IsDeleted = model.Lead.IsDeleted,
                    UnitNo = model.Lead.UnitNo,
                    SubLeadType = model.Lead.SubLeadType
                };


                var projectnos = new List<string>() {
                    "40044", "60021", "60025", "60031", "70002", "70020", "70037", "70039", "70045"
                };

                if (result.PhoneNumber != null)
                {
                    result.IsHidePhone = isUserRoleBC ? false : true; //Hide PhoneNumber

                    //var leadAct = leadActivitiess.Where(o => o.LeadID == model.Lead.ID).OrderByDescending(o => o.Created).FirstOrDefault();
                    //if (leadAct != null && leadAct.DueDate != null)
                    //{
                    //    var currentDateAdd2 = DateTime.Now.AddDays(2).Date;
                    //    var currentDateDelete30 = DateTime.Now.AddDays(-30).Date;

                    //    if (leadAct.DueDate.Value.Date > currentDateAdd2 || leadAct.DueDate.Value.Date < currentDateDelete30)
                    //    {
                    //        result.IsHidePhone = false; //Show PhoneNumber
                    //    }           
                    //}

                    //result.IsHidePhone = true;
                    //foreach (var o in projectnos)
                    //{
                    //    if (result.Project.ProjectNo == o)
                    //    {
                    //        result.IsHidePhone = true;
                    //        break;
                    //    }
                    //    if (result.Project.ProjectNo == o)
                    //    {
                    //        result.IsHidePhone = false;

                    //    }

                    //}
                }


                return result;
            }
            else
            {
                return null;
            }
        }

        public static bool ChkIsQualify(string firstName, string lastName)
        {
            bool isQuality = true;
            if ((string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName)) || (firstName.Equals("-") || lastName.Equals("-")))
                isQuality = false;

            return isQuality;
        }

        public static LeadListDTO CreateFromModel(models.CTM.Lead model)
        {
            if (model != null)
            {
                LeadListDTO result = new LeadListDTO()
                {
                    Id = model.ID,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    PhoneNumber = model.PhoneNumber,
                    LeadType = MST.MasterCenterDropdownDTO.CreateFromModel(model.LeadType),
                    Advertisement = MST.MasterCenterDropdownDTO.CreateFromModel(model.Advertisement),
                    Owner = USR.UserListDTO.CreateFromModel(model.Owner),
                    CreatedDate = model.Created,
                    Project = PRJ.ProjectDTO.CreateFromModel(model.Project),
                    Remark = model.Remark,
                    SubLeadType = model.SubLeadType,
                };

                return result;
            }
            else
            {
                return null;
            }
        }

        public static void SortBy(LeadListSortByParam sortByParam, ref IQueryable<LeadQueryResult> query)
        {
            IOrderedQueryable<LeadQueryResult> orderQuery;
            if (sortByParam.SortBy != null)
            {
                switch (sortByParam.SortBy.Value)
                {
                    case LeadListSortBy.FirstName:
                        if (sortByParam.Ascending) orderQuery = query.OrderBy(o => o.Lead.FirstName);
                        else orderQuery = query.OrderByDescending(o => o.Lead.FirstName);
                        break;
                    case LeadListSortBy.LastName:
                        if (sortByParam.Ascending) orderQuery = query.OrderBy(o => o.Lead.LastName);
                        else orderQuery = query.OrderByDescending(o => o.Lead.LastName);
                        break;
                    case LeadListSortBy.PhoneNumber:
                        if (sortByParam.Ascending) orderQuery = query.OrderBy(o => o.Lead.PhoneNumber);
                        else orderQuery = query.OrderByDescending(o => o.Lead.PhoneNumber);
                        break;
                    case LeadListSortBy.LeadType:
                        if (sortByParam.Ascending) orderQuery = query.OrderBy(o => o.Lead.LeadType.Name);
                        else orderQuery = query.OrderByDescending(o => o.Lead.LeadType.Name);
                        break;
                    case LeadListSortBy.Owner:
                        if (sortByParam.Ascending) orderQuery = query.OrderBy(o => o.Owner.FirstName);
                        else orderQuery = query.OrderByDescending(o => o.Owner.FirstName);
                        break;
                    case LeadListSortBy.Project:
                        if (sortByParam.Ascending) orderQuery = query.OrderBy(o => o.Project.ProjectNo);
                        else orderQuery = query.OrderByDescending(o => o.Project.ProjectNo);
                        break;
                    case LeadListSortBy.CreatedDate:
                        if (sortByParam.Ascending) orderQuery = query.OrderBy(o => o.Lead.Created);
                        else orderQuery = query.OrderByDescending(o => o.Lead.Created);
                        break;
                    case LeadListSortBy.SubLeadType:
                        if (sortByParam.Ascending) orderQuery = query.OrderBy(o => o.Lead.SubLeadType);
                        else orderQuery = query.OrderByDescending(o => o.Lead.SubLeadType);
                        break;
                    default:
                        orderQuery = query.OrderBy(o => o.Lead.FirstName);
                        break;
                }
            }
            else
            {
                orderQuery = query.OrderBy(o => o.Lead.FirstName);
            }

            orderQuery.ThenBy(o => o.Lead.ID);
            query = orderQuery;
        }

        public static void VisitorSortBy(VisitorLeadListSortByParam sortByParam, ref IQueryable<LeadQueryResult> query)
        {
            if (sortByParam.SortBy != null)
            {
                switch (sortByParam.SortBy.Value)
                {
                    case VisitorLeadListSortBy.Advertisement:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.Lead.Advertisement.Name);
                        else query = query.OrderByDescending(o => o.Lead.Advertisement.Name);
                        break;
                    case VisitorLeadListSortBy.Project:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.Project.ProjectNo);
                        else query = query.OrderByDescending(o => o.Project.ProjectNo);
                        break;
                    case VisitorLeadListSortBy.CreatedDate:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.Lead.Created);
                        else query = query.OrderByDescending(o => o.Lead.Created);
                        break;
                    case VisitorLeadListSortBy.Remark:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.Lead.Remark);
                        else query = query.OrderByDescending(o => o.Lead.Remark);
                        break;
                    default:
                        query = query.OrderBy(o => o.Lead.Created);
                        break;
                }
            }
            else
            {
                query = query.OrderBy(o => o.Lead.Created);
            }
        }
    }

    public class LeadQueryResult
    {
        public models.CTM.Lead Lead { get; set; }
        public models.CTM.Contact Contact { get; set; }
        public models.PRJ.Project Project { get; set; }
        public models.USR.User Owner { get; set; }
        public models.MST.MasterCenter LeadType { get; set; }
        public models.MST.MasterCenter Advertisement { get; set; }
        public models.MST.MasterCenter LeadStatus { get; set; }
    }
}