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
	public class ContactChangeLogDTO
    {
		public Guid? Id { get; set; }
        public ContactDTO Contact { get; set; }
        public string Actions { get; set; }
        public string ColumnChanged { get; set; }
        public string ValuesOld { get; set; }
        public string ValuesNew { get; set; }
        public DateTime? Created { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? Updated { get; set; }
        public string UpdatedBy { get; set; }
		

        public static ContactChangeLogDTO CreateFromQueryResule(ContactChangeLogQueryResult model)
        {
            if (model != null)
            {
                var result = new ContactChangeLogDTO();

                result.Id = model.ContactChangeLog?.ID;
                result.Contact = ContactDTO.CreateFromModel(model.Contact);
                result.Actions = model.ContactChangeLog?.Actions;
                result.ColumnChanged = model.ContactChangeLog?.ColumnChanged;
                result.ValuesOld = model.ContactChangeLog?.ValuesOld;
                result.ValuesNew = model.ContactChangeLog?.ValuesNew;
                result.Created = model.ContactChangeLog?.Created;
                result.CreatedBy = model.ContactChangeLog?.CreatedBy?.DisplayName;
                result.Updated = model.ContactChangeLog?.Updated;
                result.UpdatedBy = model.ContactChangeLog?.UpdatedBy?.DisplayName;



                return result;
            }
            else
            {
                return null;
            }
        }
        public static void SortBy(ContactChangeLogSortByParam sortByParam, ref IQueryable<ContactChangeLogQueryResult> query)
        {
            IOrderedQueryable<ContactChangeLogQueryResult> orderQuery;
            if (sortByParam.SortBy != null)
            {
                switch (sortByParam.SortBy.Value)
                {
                    case ContactChangeLogSortBy.ColumnChanged:
                        if (sortByParam.Ascending) orderQuery = query.OrderBy(o => o.ContactChangeLog.ColumnChanged);
                        else orderQuery = query.OrderByDescending(o => o.ContactChangeLog.ColumnChanged);
                        break;
                    case ContactChangeLogSortBy.ValuesOld:
                        if (sortByParam.Ascending) orderQuery = query.OrderBy(o => o.ContactChangeLog.ValuesOld);
                        else orderQuery = query.OrderByDescending(o => o.ContactChangeLog.ValuesOld);
                        break;
                    case ContactChangeLogSortBy.ValuesNew:
                        if (sortByParam.Ascending) orderQuery = query.OrderBy(o => o.ContactChangeLog.ValuesNew);
                        else orderQuery = query.OrderByDescending(o => o.ContactChangeLog.ValuesNew);
                        break;
                    case ContactChangeLogSortBy.Updated:
                        if (sortByParam.Ascending) orderQuery = query.OrderBy(o => o.ContactChangeLog.Updated);
                        else orderQuery = query.OrderByDescending(o => o.ContactChangeLog.Updated);
                        break;
                    case ContactChangeLogSortBy.UpdatedBy:
                        if (sortByParam.Ascending) orderQuery = query.OrderBy(o => o.ContactChangeLog.UpdatedBy.DisplayName);
                        else orderQuery = query.OrderByDescending(o => o.ContactChangeLog.UpdatedBy.DisplayName);
                        break;
                    default:
                        orderQuery = query.OrderByDescending(o => o.ContactChangeLog.Created);
                        break;
                    }
                }
            else
            {
                orderQuery = query.OrderByDescending(o => o.ContactChangeLog.Created);//.ThenBy(o => o.Contact.ID);
            }

            //orderQuery =  orderQuery.ThenBy(o => o.Contact.ID);
            query = orderQuery;
        }

    }

    public class ContactChangeLogQueryResult
    {
        public models.CTM.Contact_Change_Log ContactChangeLog { get; set; }
        public models.CTM.Contact Contact { get; set; }
    }
}
