using Database.Models;
using Database.Models.CTM;
using Database.Models.DbQueries.EQN;
using Database.Models.MasterKeys;
using Database.Models.PRJ;
using Database.Models.USR;
using ErrorHandling;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using models = Database.Models;

namespace Base.DTOs.CTM
{
	public class ContactAuditDTO
    {
		public Guid? Id { get; set; }
        public string Actions { get; set; }
        public ContactDTO Contact { get; set; }
        public string ContactNo { get; set; }
        public string FirstNameTH { get; set; }
        public string LastNameTH { get; set; }
        public DateTime? Created { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? Updated { get; set; }
        public string UpdatedBy { get; set; }
		

        public static ContactAuditDTO CreateFromQueryResult(ContactAuditQueryResult model)
        {
            if (model != null)
            {
                var result = new ContactAuditDTO
                {
                    Id = model.ContactAudit?.ID,
                    Actions = model.ContactAudit?.Actions,
                    Contact = ContactDTO.CreateFromModel(model.Contact),
                    ContactNo = model.Contact?.ContactNo,
                    FirstNameTH = model.Contact?.FirstNameTH,
                    LastNameTH = model.Contact?.LastNameTH,
                    Created = model.ContactAudit?.Created,
                    CreatedBy = model.ContactAudit?.CreatedBy?.DisplayName,
                    Updated = model.ContactAudit?.Updated,
                    UpdatedBy = model.ContactAudit?.UpdatedBy?.DisplayName
                };



                return result;
            }
            else
            {
                return null;
            }
        }
        public static void SortBy(ContactAuditSortByParam sortByParam, ref IQueryable<ContactAuditQueryResult> query)
        {
            IOrderedQueryable<ContactAuditQueryResult> orderQuery;
            if (sortByParam.SortBy != null)
            {
                switch (sortByParam.SortBy.Value)
                {
                    case ContactAuditSortBy.ContactNo:
                        if (sortByParam.Ascending) orderQuery = query.OrderBy(o => o.ContactAudit.ContactNo);
                        else orderQuery = query.OrderByDescending(o => o.ContactAudit.ContactNo);
                        break;
                    case ContactAuditSortBy.FirstNameTH:
                        if (sortByParam.Ascending) orderQuery = query.OrderBy(o => o.ContactAudit.FirstNameTH);
                        else orderQuery = query.OrderByDescending(o => o.ContactAudit.FirstNameTH);
                        break;
                    case ContactAuditSortBy.LastNameTH:
                        if (sortByParam.Ascending) orderQuery = query.OrderBy(o => o.ContactAudit.LastNameTH);
                        else orderQuery = query.OrderByDescending(o => o.ContactAudit.LastNameTH);
                        break;
                    case ContactAuditSortBy.UpdatedDate:
                        if (sortByParam.Ascending) orderQuery = query.OrderBy(o => o.ContactAudit.Updated);
                        else orderQuery = query.OrderByDescending(o => o.ContactAudit.Updated);
                        break;
                    case ContactAuditSortBy.UpdatedBy:
                        if (sortByParam.Ascending) orderQuery = query.OrderBy(o => o.ContactAudit.Updated);
                        else orderQuery = query.OrderByDescending(o => o.ContactAudit.Updated);
                        break;
                    default:
                        orderQuery = query.OrderByDescending(o => o.ContactAudit.Created);
                        break;
                }
            }
            else
            {
                orderQuery = query.OrderByDescending(o => o.ContactAudit.Created);//.ThenBy(o => o.Contact.ID);
            }

            //orderQuery =  orderQuery.ThenBy(o => o.Contact.ID);
            query = orderQuery;
        }
    }

    public class ContactAuditQueryResult
    {
        public models.CTM.Contact_Audit ContactAudit { get; set; }
        public models.CTM.Contact Contact { get; set; }
    }
}
