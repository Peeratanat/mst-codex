using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Base.DTOs;
using Base.DTOs.CTM;
using Common.Helper.Logging;
using CTM_CustomerConsent.Params.Filters;
using CTM_CustomerConsent.Params.Outputs;
using Dapper;
using Database.Models;
using Database.Models.CTM;
using Database.Models.DbQueries;
using Database.Models.DbQueries.CTM;
using Database.Models.MasterKeys;
using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using PagingExtensions;
using static Database.Models.DbQueries.DBQueryParam;

namespace CTM_CustomerConsent.Services.CustomerConsentService
{
    public class CustomerConsentService : ICustomerConsentService
    {
        private readonly DatabaseContext DB;
        private readonly DbQueryContext DBQuery;
        public LogModel logModel { get; set; }
        int Timeout = 300;
        public CustomerConsentService(DatabaseContext db, DbQueryContext dbQuery)
        {
            logModel = new LogModel("CustomerConsentService", null);
            DB = db;
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(Environment.GetEnvironmentVariable("DBConnectionString"));
            Timeout = builder.ConnectTimeout;
            DB.Database.SetCommandTimeout(Timeout);
        }

        public async Task<ConsentListPaging> GetConSentListAsync(ConsentFilter filter, PageParam pageParam, ConsentListSortByParam sortByParam, CancellationToken cancellationToken = default)
        {
            using DbCommand cmd = DB.Database.GetDbConnection().CreateCommand();
            DynamicParameters ParamList = new DynamicParameters();
            ParamList.Add("FirstName", filter.FirstName);
            ParamList.Add("LastName", filter.LastName);
            ParamList.Add("Identity", filter.Identity);
            ParamList.Add("Phonenumber", filter.Phonenumber);
            ParamList.Add("Email", filter.Email);
            ParamList.Add("ConsentText", filter.ConsentText);
            ParamList.Add("ReferentTypeID", filter.ReferentType);
            ParamList.Add("ReferentSubType", filter.ReferentSubType);
            ParamList.Add("ProjectID", filter.ProjectID);
            ParamList.Add("ContactNumber", filter.ContactNumber);
            ParamList.Add("CreateDateFrom", filter.CreateDateFrom);
            ParamList.Add("CreateDateTo", filter.CreateDateTo);
            ParamList.Add("UpdateDateFrom", filter.UpdateDateFrom);
            ParamList.Add("UpdateDateTo", filter.UpdateDateTo);
            ParamList.Add("EditBy", filter.EditBy);

            ParamList.Add("Page", pageParam?.Page ?? 1);
            ParamList.Add("PageSize", pageParam?.PageSize ?? 10);
            CommandDefinition commandDefinition = new(
                                         commandText: DBStoredNames.spConsentList,
                                         cancellationToken: cancellationToken,
                                         parameters: ParamList,
                                         commandTimeout: Timeout,
                                         transaction: DB?.Database?.CurrentTransaction?.GetDbTransaction(),
                                         commandType: CommandType.StoredProcedure);
            var queryResult = await cmd.Connection.QueryAsync<dbqConsentListSP>(commandDefinition);
            var result = queryResult.Select(o => ConsentListDTO.CreateFromQueryResult(o, DB)).ToList() ?? [];

            return new ConsentListPaging()
            {
                PageOutput = queryResult.FirstOrDefault() != null ? queryResult.FirstOrDefault().CreateBaseDTOFromQuery() : new PageOutput(),
                Consent = result ?? []
            };

        }

        public async Task UpdateCustomerConsentAsync(UpdateCustomerConsentDTO input)
        {
            if ((input.ReferentDataList ?? new List<UpdateCustomerConsentDTO.ReferentData>()).Any())
            {
                var ConsentRefType = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.ConsentReferentType).ToListAsync();

                // Lead
                var LeadList = input.ReferentDataList.Where(o => o.ReferentType.Key == ConsentReferentTypeKeys.Lead).Select(o => o.ReferentID).ToList() ?? new List<Guid>();

                if (LeadList.Any())
                {
                    var LeadData = await DB.Leads.Where(o => LeadList.Contains(o.ID)).ToListAsync();

                    var Lead_ConsentRefTypeID = ConsentRefType.Find(o => o.Key == ConsentReferentTypeKeys.Lead).ID;
                    var Lead_ConsentHistoty = LeadData
                        .Select(o =>
                            new CustomerConsentHistory
                            {
                                ReferentTypeID = Lead_ConsentRefTypeID,
                                ReferentID = o.ID,
                                OldConsentTypeMasterCenterID = o.ConsentTypeMasterCenterID,
                                NewConsentTypeMasterCenterID = input.ConsentType.Id,
                                EmployeeNo = input.EmpoyeeNo,
                                EmployeeName = input.EmpoyeeName
                            }).ToList();
                    // await DB.BulkInsertAsync(Lead_ConsentHistoty);
                    // await DB.Leads.Where(o => LeadList.Contains(o.ID)).ExecuteUpdateAsync(c =>
                    //     c.SetProperty(col => col.ConsentTypeMasterCenterID, input.ConsentType.Id)
                    //     .SetProperty(col => col.Updated, DateTime.Now)
                    // );

                    await DB.CustomerConsentHistorys.AddRangeAsync(Lead_ConsentHistoty);

                    LeadData.ForEach(o => o.ConsentTypeMasterCenterID = input.ConsentType.Id);
                    DB.UpdateRange(LeadData);

                }

                // Opportunity
                var OpportunityList = input.ReferentDataList.Where(o => o.ReferentType.Key == ConsentReferentTypeKeys.Opportunity).Select(o => o.ReferentID).ToList() ?? new List<Guid>();
                var ContactList_FromOpp = new List<Contact>();
                if (OpportunityList.Any())
                {
                    var OpportunitieData = await DB.Opportunities.Where(o => OpportunityList.Contains(o.ID)).ToListAsync();

                    var OpportunitieData_ConsentRefTypeID = ConsentRefType.Find(o => o.Key == ConsentReferentTypeKeys.Opportunity).ID;

                    var Opportunitie_ConsentHistoty = OpportunitieData
                        .Select(o =>
                            new CustomerConsentHistory
                            {
                                ReferentTypeID = OpportunitieData_ConsentRefTypeID,
                                ReferentID = o.ID,
                                OldConsentTypeMasterCenterID = o.ConsentTypeMasterCenterID,
                                NewConsentTypeMasterCenterID = input.ConsentType.Id,
                                EmployeeNo = input.EmpoyeeNo,
                                EmployeeName = input.EmpoyeeName
                            }).ToList();

                    // await DB.BulkInsertAsync(Opportunitie_ConsentHistoty);

                    // await DB.Opportunities.Where(o => OpportunityList.Contains(o.ID)).ExecuteUpdateAsync(c =>
                    //     c.SetProperty(col => col.ConsentTypeMasterCenterID, input.ConsentType.Id)
                    //     .SetProperty(col => col.Updated, DateTime.Now)
                    // );

                    await DB.CustomerConsentHistorys.AddRangeAsync(Opportunitie_ConsentHistoty);

                    OpportunitieData.ForEach(o => { o.ConsentTypeMasterCenterID = input.ConsentType.Id; o.Updated = DateTime.Now; });
                    DB.UpdateRange(OpportunitieData);
                }


                // Contact
                var ContactList = input.ReferentDataList.Where(o => o.ReferentType.Key == ConsentReferentTypeKeys.Contact).Select(o => o.ReferentID).ToList() ?? new List<Guid>();

                if (ContactList.Any())
                {
                    var ContactData = await DB.Contacts.Where(o => ContactList.Contains(o.ID)).ToListAsync();

                    var Contact_ConsentRefTypeID = ConsentRefType.Find(o => o.Key == ConsentReferentTypeKeys.Contact).ID;
                    var Contact_ConsentHistoty = ContactData
                        .Select(o =>
                            new CustomerConsentHistory
                            {
                                ReferentTypeID = Contact_ConsentRefTypeID,
                                ReferentID = o.ID,
                                OldConsentTypeMasterCenterID = o.ConsentTypeMasterCenterID,
                                NewConsentTypeMasterCenterID = input.ConsentType.Id,
                                EmployeeNo = input.EmpoyeeNo,
                                EmployeeName = input.EmpoyeeName
                            }).ToList();
                    await DB.BulkInsertAsync(Contact_ConsentHistoty);

                    await DB.Contacts.Where(o => ContactList.Contains(o.ID)).ExecuteUpdateAsync(c =>
                        c.SetProperty(col => col.ConsentTypeMasterCenterID, input.ConsentType.Id)
                        .SetProperty(col => col.Updated, DateTime.Now)
                    );

                    // await DB.CustomerConsentHistorys.AddRangeAsync(Contact_ConsentHistoty);

                    // ContactData.ForEach(o => o.ConsentTypeMasterCenterID = input.ConsentType.Id);

                    // DB.UpdateRange(ContactData);
                }

                await DB.SaveChangesAsync();
            }
        }
    }
}
