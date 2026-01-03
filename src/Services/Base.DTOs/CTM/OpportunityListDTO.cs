using Database.Models.DbQueries.CTM;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using models = Database.Models;

namespace Base.DTOs.CTM
{
    public class OpportunityListDTO
    {
        /// <summary>
        /// ID ของ Opportunity
        /// </summary>
        public Guid? Id { get; set; }
        /// <summary>
        /// ข้อมูล Contact
        /// </summary>
        public ContactListDTO Contact { get; set; }
        /// <summary>
        /// โครงการ
        /// project/api/Projects/DropdownList
        /// </summary>
        public PRJ.ProjectDTO Project { get; set; }
        /// <summary>
        /// วันที่เข้าโครงการ
        /// </summary>
        public DateTime? ArriveDate { get; set; }
        /// <summary>
        /// วันที่แก้ไข
        /// </summary>
        public DateTime? UpdatedDate { get; set; }
        /// <summary>
        /// สถานะของ Opportunity
        /// masterdata/api/MasterCenters/DropdownList?masterCenterGroupKey=SalesOpportunity
        /// </summary>
        public MST.MasterCenterDropdownDTO SalesOpportunity { get; set; }
        /// <summary>
        /// LC Owner
        /// </summary>
        public USR.UserListDTO Owner { get; set; }
        /// <summary>
        /// Last Activity
        /// masterdata/api/MasterCenters/DropdownList?masterCenterGroupKey=OpportunityActivityType
        /// </summary>
        public MST.MasterCenterDropdownDTO LastActivity { get; set; }
        /// <summary>
        /// จำนวน Revisit
        /// </summary>
        public int RevisitCount { get; set; }
        /// <summary>
        /// สถานะทำแบบสอบถาม
        /// masterdata/api/MasterCenters/DropdownList?masterCenterGroupKey=StatusQuestionaire
        /// </summary>
        public MST.MasterCenterDropdownDTO StatusQuestionaire { get; set; }
        /// <summary>
        /// จำนวนคำถามที่ตอบคำถามแล้ว
        /// </summary>
        public int? AnsweredQuestionaire { get; set; }
        /// <summary>
        /// จำนวนคำถามทั้งหมด
        /// </summary>
        public int? TotalQuestionaire { get; set; }
        /// <summary>
        /// วันที่ตอบแบบสอบถาม
        /// </summary>
        public DateTime? QuestionaireDate { get; set; }

        /// <summary>
        ///  แสดงการแก้ไข
        /// </summary>
        public bool? IsEdit { get; set; }

        public static OpportunityListDTO CreateFromQueryResult(OpportunityQueryResult model, models.DatabaseContext DB)
        {
            if (model != null)
            {
                OpportunityListDTO result = new OpportunityListDTO()
                {
                    Id = model.Opportunity.ID,
                    ArriveDate = model.Opportunity.ArriveDate,
                    UpdatedDate = model.Opportunity.Updated,
                    SalesOpportunity = MST.MasterCenterDropdownDTO.CreateFromModel(model.SalesOpportunity),
                    Owner = USR.UserListDTO.CreateFromModel(model.Owner),
                    Contact = ContactListDTO.CreateFromModel(model.Contact, DB),
                    Project = PRJ.ProjectDTO.CreateFromModel(model.Project),
                    StatusQuestionaire = MST.MasterCenterDropdownDTO.CreateFromModel(model.StatusQuestionaire),
                    QuestionaireDate = model.Opportunity.QuestionaireDate,
                    LastActivity = MST.MasterCenterDropdownDTO.CreateFromModel(model.OpportunityActivityType),
                    RevisitCount = model.Opportunity.RevisitActivityCount,
                    IsEdit = false
                    
                };

                return result;
            }
            else
            {
                return null;
            }
        }

        public static OpportunityListDTO CreateForOppFromQueryResult(dbqGetOppListForChangeOppSP model, models.DatabaseContext DB)
        {
            if (model != null)
            {
                var contact = DB.Contacts.Where(o => o.ID == model.ContactID).FirstOrDefault();
                OpportunityListDTO result = new OpportunityListDTO()
                {
                    Id = model.ID,
                    ArriveDate = model.ArriveDate,              
                    Contact = ContactListDTO.CreateFromModel(contact, DB)
  
                };

                return result;
            }
            else
            {
                return null;
            }
        }

        public static OpportunityListDTO CreateFromModel(models.CTM.Opportunity model, models.DatabaseContext DB)
        {
            if (model != null)
            {
                OpportunityListDTO result = new OpportunityListDTO()
                {
                    Id = model.ID,
                    ArriveDate = model.ArriveDate,
                    UpdatedDate = model.Updated,
                    SalesOpportunity = MST.MasterCenterDropdownDTO.CreateFromModel(model.SalesOpportunity),
                    Owner = USR.UserListDTO.CreateFromModel(model.Owner),
                    Project = PRJ.ProjectDTO.CreateFromModel(model.Project),
                    StatusQuestionaire = MST.MasterCenterDropdownDTO.CreateFromModel(model.StatusQuestionaire),
                    QuestionaireDate = model.QuestionaireDate
                };
                result.Contact = ContactListDTO.CreateFromModel(model.Contact, DB);

                return result;
            }
            else
            {
                return null;
            }
        }

        public static void SortBy(OpportunityListSortByParam sortByParam, ref IQueryable<OpportunityQueryResult> query)
        {
            IOrderedQueryable<OpportunityQueryResult> orderQuery;
            if (sortByParam.SortBy != null)
            {
                switch (sortByParam.SortBy.Value)
                {
                    case OpportunityListSortBy.FullName:
                        if (sortByParam.Ascending) orderQuery =  query.OrderBy(o => (o.Contact.FirstNameTH)).ThenBy(o => o.Contact.LastNameTH);
                        else orderQuery =  query.OrderByDescending(o => (o.Contact.FirstNameTH)).ThenByDescending(o => o.Contact.LastNameTH);
                        break;
                    case OpportunityListSortBy.ArriveDate:
                        if (sortByParam.Ascending) orderQuery =  query.OrderBy(o => o.Opportunity.ArriveDate);
                        else orderQuery =  query.OrderByDescending(o => o.Opportunity.ArriveDate);
                        break;
                    case OpportunityListSortBy.UpdatedDate:
                        if (sortByParam.Ascending) orderQuery =  query.OrderBy(o => o.Opportunity.Updated);
                        else orderQuery =  query.OrderByDescending(o => o.Opportunity.Updated);
                        break;
                    case OpportunityListSortBy.ContactNo:
                        if (sortByParam.Ascending) orderQuery =  query.OrderBy(o => o.Contact.ContactNo);
                        else orderQuery =  query.OrderByDescending(o => o.Contact.ContactNo);
                        break;
                    case OpportunityListSortBy.PhoneNumber:
                        if (sortByParam.Ascending) orderQuery =  query.OrderBy(o => o.ContactPhone.PhoneNumber);
                        else orderQuery =  query.OrderByDescending(o => o.ContactPhone.PhoneNumber);
                        break;
                    case OpportunityListSortBy.SalesOpportunity:
                        if (sortByParam.Ascending) orderQuery =  query.OrderBy(o => o.SalesOpportunity.Order);
                        else orderQuery =  query.OrderByDescending(o => o.SalesOpportunity.Order);
                        break;
                    case OpportunityListSortBy.Project:
                        if (sortByParam.Ascending) orderQuery =  query.OrderBy(o => o.Project.ProjectNo);
                        else orderQuery =  query.OrderByDescending(o => o.Project.ProjectNo);
                        break;
                    case OpportunityListSortBy.Owner:
                        if (sortByParam.Ascending) orderQuery =  query.OrderBy(o => o.Owner.FirstName);
                        else orderQuery =  query.OrderByDescending(o => o.Owner.FirstName);
                        break;
                    case OpportunityListSortBy.StatusQuestionaire:
                        if (sortByParam.Ascending) orderQuery =  query.OrderBy(o => o.StatusQuestionaire.Order);
                        else orderQuery =  query.OrderByDescending(o => o.StatusQuestionaire.Order);
                        break;
                    case OpportunityListSortBy.LastActivity:
                        if (sortByParam.Ascending) orderQuery =  query.OrderBy(o => o.OpportunityActivityType.Name);
                        else orderQuery =  query.OrderByDescending(o => o.OpportunityActivityType.Name);
                        break;
                    case OpportunityListSortBy.RevisitCount:
                        if (sortByParam.Ascending) orderQuery =  query.OrderBy(o => o.Opportunity.RevisitActivityCount);
                        else orderQuery =  query.OrderByDescending(o => o.Opportunity.RevisitActivityCount);
                        break;
                    default:
                        orderQuery =  query.OrderByDescending(o => o.Opportunity.Updated);
                        break;
                }
            }
            else
            {
                orderQuery =  query.OrderByDescending(o => o.Opportunity.Updated);
            }
            orderQuery.ThenBy(o => o.Opportunity.ID);
            query = orderQuery;
        }

    }

    public class OpportunityQueryResult
    {
        public models.CTM.Opportunity Opportunity { get; set; }
        public models.CTM.Contact Contact { get; set; }
        public models.PRJ.Project Project { get; set; }
        public models.MST.MasterCenter SalesOpportunity { get; set; }
        public models.MST.MasterCenter StatusQuestionaire { get; set; }
        public models.CTM.ContactPhone ContactPhone { get; set; }
        public models.MST.MasterCenter OpportunityActivityType { get; set; }
        public models.USR.User Owner { get; set; }
    }
}