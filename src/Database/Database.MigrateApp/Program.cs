using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;
using Database.Models.CTM;
using Database.Models;
using System.Threading.Tasks;
using Database.Models.MasterKeys;
using Database.Models.MST;

namespace Database.MigrateApp
{
    public class Program
    {
        private static DatabaseContext DB;

        static void Main(string[] args)
        {

        }

        static async Task Main2(string[] args)
        {
            Console.WriteLine("Starting..");
            DbContextFactory factory = new DbContextFactory();
            var db = factory.CreateDbContext();
            DB = db;

            var projects = await DB.Projects.Where(o => o.IsActive == true).OrderBy(o => o.ProjectNo).ToListAsync();
            var masterCenters = await DB.MasterCenters.ToListAsync();
            foreach (var project in projects)
            {
                using (var transaction = await DB.Database.BeginTransactionAsync())
                {
                    try
                    {
                        var activityTasks = await DB.ActivityTasks.Where(o => o.ProjectID == project.ID).ToListAsync();
                        if (activityTasks.Count > 0)
                        {
                            DB.RemoveRange(activityTasks);
                            await DB.SaveChangesAsync();
                        }

                        Console.Write(project.ProjectNo);
                        #region Lead
                        await UpdateLeadActivityTask(project.ID, masterCenters);
                        Console.Write(": Lead,");
                        #endregion

                        #region Opportunity
                        await UpdateOpportunityActivityTask(project.ID, masterCenters);
                        Console.Write(" Opportunity,");
                        #endregion

                        #region Revisit
                        await UpdateRevisitActivityTask(project.ID, masterCenters);
                        Console.Write(" Revisit");
                        #endregion
                        Console.WriteLine(" : Finished.");
                        await transaction.CommitAsync();
                    }
                    catch (Exception ex)
                    {
                        await transaction.RollbackAsync();
                        Console.WriteLine("Error: " + ex.Message);
                    }
                }
            }
        }

        private static async Task UpdateLeadActivityTask(Guid projectId, List<MasterCenter> masterCenters)
        {
            var activities = await DB.LeadActivities
                .Include(o => o.Lead)
                .ThenInclude(o => o.Contact)
                .Where(o => o.Lead.ProjectID == projectId).ToListAsync();

            foreach (var activity in activities)
            {
                try
                {
                    string firstName = null;
                    string lastName = null;
                    string phone = null;
                    if (activity.Lead.ContactID != null)
                    {
                        firstName = activity.Lead.Contact.FirstNameTH;
                        lastName = activity.Lead.Contact.LastNameTH;
                        phone = await DB.ContactPhones.Where(o => o.ContactID == activity.Lead.ContactID && o.IsMain == true).Select(o => o.PhoneNumber).FirstOrDefaultAsync();
                    }
                    else
                    {
                        firstName = activity.Lead.FirstName;
                        lastName = activity.Lead.LastName;
                        phone = activity.Lead.PhoneNumber;
                    }

                    var activityTaskStatus = masterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.ActivityTaskStatus).ToList();
                    Guid? activityTaskStatusId = null;

                    int overdue = 0;
                    if (!activity.IsCompleted)
                    {
                        overdue = Convert.ToInt32((activity.DueDate.Value.Date - DateTime.Today.Date).TotalDays);
                    }
                    else
                    {
                        overdue = Convert.ToInt32((activity.DueDate.Value.Date - activity.ActualDate.Value.Date).TotalDays);
                    }

                    Guid? overdueStatusId = null;
                    if (overdue == 0)
                    {
                        overdueStatusId = masterCenters.Find(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.ActivityTaskOverdueStatus && o.Key == "2")?.ID;
                        activityTaskStatusId = activity.IsCompleted ? activityTaskStatus.Find(o => o.Key == "2")?.ID : activityTaskStatus.Find(o => o.Key == "3")?.ID;
                    }
                    else if (overdue < 0)
                    {
                        overdueStatusId = masterCenters.Find(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.ActivityTaskOverdueStatus && o.Key == "3")?.ID;
                        activityTaskStatusId = activity.IsCompleted ? activityTaskStatus.Find(o => o.Key == "2")?.ID : activityTaskStatus.Find(o => o.Key == "3")?.ID;
                    }
                    else if (overdue > 0)
                    {
                        overdueStatusId = masterCenters.Find(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.ActivityTaskOverdueStatus && o.Key == "1")?.ID;
                        activityTaskStatusId = activity.IsCompleted ? activityTaskStatus.Find(o => o.Key == "2")?.ID : activityTaskStatus.Find(o => o.Key == "3")?.ID;
                    }

                    var activityTypeKey = masterCenters.Find(o => o.ID == activity.LeadActivityTypeMasterCenterID)?.Key;
                    int repeatCount = 0;
                    switch (activityTypeKey)
                    {
                        case "1":
                            repeatCount = 1;
                            break;
                        case "2":
                            repeatCount = 2;
                            break;
                        case "3":
                            repeatCount = 3;
                            break;
                        case "4":
                            repeatCount = 3;
                            break;
                    }

                    ActivityTask activityTask = new ActivityTask()
                    {
                        ProjectID = activity.Lead.ProjectID,
                        ContactFirstName = firstName,
                        ContactLastName = lastName,
                        PhoneNumber = phone,
                        DueDate = activity.DueDate,
                        ActivityTaskOverdueStatusMasterCenterID = overdueStatusId,
                        OverdueDays = overdue,
                        RepeatCount = repeatCount,
                        ActivityTaskStatusMasterCenterID = activityTaskStatusId,
                        ActivityTaskTopicMasterCenterID = masterCenters.Find(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.ActivityTaskTopic && o.Key == "1")?.ID,
                        ActivityTaskTypeMasterCenterID = masterCenters.Find(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.ActivityTaskType && o.Key == "2")?.ID,
                        ActivityTypeName = masterCenters.Find(o => o.ID == activity.LeadActivityTypeMasterCenterID)?.Name,
                        OwnerID = activity.Lead.OwnerID,
                        LeadActivityID = activity.ID
                    };
                    await DB.ActivityTasks.AddAsync(activityTask);

                    if (activity.AppointmentDate != null)
                    {
                        ActivityTask activityTaskAppointment = new ActivityTask()
                        {
                            ProjectID = activity.Lead.ProjectID,
                            ContactFirstName = firstName,
                            ContactLastName = lastName,
                            PhoneNumber = phone,
                            DueDate = activity.DueDate,
                            ActivityTaskOverdueStatusMasterCenterID = overdueStatusId,
                            OverdueDays = overdue,
                            RepeatCount = repeatCount,
                            ActivityTaskStatusMasterCenterID = masterCenters.Find(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.ActivityTaskStatus && o.Key == "2")?.ID,
                            ActivityTaskTopicMasterCenterID = masterCenters.Find(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.ActivityTaskTopic && o.Key == "1")?.ID,
                            ActivityTaskTypeMasterCenterID = masterCenters.Find(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.ActivityTaskType && o.Key == "5")?.ID,
                            ActivityTypeName = masterCenters.Find(o => o.ID == activity.LeadActivityTypeMasterCenterID)?.Name,
                            OwnerID = activity.Lead.OwnerID,
                            LeadActivityID = activity.ID
                        };
                        await DB.ActivityTasks.AddAsync(activityTaskAppointment);
                    }
                }
                catch
                {

                }
            }

            await DB.SaveChangesAsyncByMigrateApp();
        }

        private static async Task UpdateOpportunityActivityTask(Guid projectId, List<MasterCenter> masterCenters)
        {
            var activities = await DB.OpportunityActivities
                .Include(o => o.Opportunity)
                .ThenInclude(o => o.Contact)
                .Where(o => o.Opportunity.ProjectID == projectId).ToListAsync();

            foreach (var activity in activities)
            {
                try
                {
                    string firstName = null;
                    string lastName = null;
                    string phone = null;
                    if (activity.Opportunity.ContactID != null)
                    {
                        firstName = activity.Opportunity.Contact.FirstNameTH;
                        lastName = activity.Opportunity.Contact.LastNameTH;
                        phone = await DB.ContactPhones.Where(o => o.ContactID == activity.Opportunity.ContactID && o.IsMain == true).Select(o => o.PhoneNumber).FirstOrDefaultAsync();
                    }

                    var activityTaskStatus = masterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.ActivityTaskStatus).ToList();
                    Guid? activityTaskStatusId = null;

                    int overdue = 0;
                    if (!activity.IsCompleted)
                    {
                        overdue = Convert.ToInt32((activity.DueDate.Value.Date - DateTime.Today.Date).TotalDays);
                    }
                    else
                    {
                        overdue = Convert.ToInt32((activity.DueDate.Value.Date - activity.ActualDate.Value.Date).TotalDays);
                    }
                    Guid? overdueStatusId = null;
                    if (overdue == 0)
                    {
                        overdueStatusId = masterCenters.Find(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.ActivityTaskOverdueStatus && o.Key == "2")?.ID;
                        activityTaskStatusId = activity.IsCompleted ? activityTaskStatus.Find(o => o.Key == "2")?.ID : activityTaskStatus.Find(o => o.Key == "3")?.ID;
                    }
                    else if (overdue < 0)
                    {
                        overdueStatusId = masterCenters.Find(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.ActivityTaskOverdueStatus && o.Key == "3")?.ID;
                        activityTaskStatusId = activity.IsCompleted ? activityTaskStatus.Find(o => o.Key == "2")?.ID : activityTaskStatus.Find(o => o.Key == "3")?.ID;
                    }
                    else if (overdue > 0)
                    {
                        overdueStatusId = masterCenters.Find(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.ActivityTaskOverdueStatus && o.Key == "1")?.ID;
                        activityTaskStatusId = activity.IsCompleted ? activityTaskStatus.Find(o => o.Key == "2")?.ID : activityTaskStatus.Find(o => o.Key == "3")?.ID;
                    }

                    var ActivityTaskTypePhoneId = masterCenters.Find(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.ActivityTaskType && o.Key == "2")?.ID;
                    var ActivityTaskTypeHomeId = masterCenters.Find(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.ActivityTaskType && o.Key == "1")?.ID;
                    var ActivityTaskTypeAskId = masterCenters.Find(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.ActivityTaskType && o.Key == "4")?.ID;
                    var activityTypeKey = masterCenters.Find(o => o.ID == activity.OpportunityActivityTypeMasterCenterID)?.Key;
                    Guid? selectActivityTypeTask = null;
                    switch (activityTypeKey)
                    {
                        case "1":
                            selectActivityTypeTask = ActivityTaskTypeHomeId;
                            break;
                        case "2":
                            selectActivityTypeTask = ActivityTaskTypeAskId;
                            break;
                        case "3":
                            selectActivityTypeTask = ActivityTaskTypePhoneId;
                            break;
                        case "4":
                            selectActivityTypeTask = ActivityTaskTypePhoneId;
                            break;
                        case "5":
                            selectActivityTypeTask = ActivityTaskTypePhoneId;
                            break;
                        case "6":
                            selectActivityTypeTask = ActivityTaskTypePhoneId;
                            break;
                        case "7":
                            selectActivityTypeTask = ActivityTaskTypePhoneId;
                            break;
                    }

                    ActivityTask activityTask = new ActivityTask()
                    {
                        ProjectID = projectId,
                        ContactFirstName = firstName,
                        ContactLastName = lastName,
                        PhoneNumber = phone,
                        DueDate = activity.DueDate,
                        ActivityTaskOverdueStatusMasterCenterID = overdueStatusId,
                        OverdueDays = overdue,
                        RepeatCount = 0,
                        ActivityTaskStatusMasterCenterID = activityTaskStatusId,
                        ActivityTaskTopicMasterCenterID = masterCenters.Find(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.ActivityTaskTopic && o.Key == "2")?.ID,
                        ActivityTaskTypeMasterCenterID = selectActivityTypeTask,
                        ActivityTypeName = masterCenters.Find(o => o.ID == activity.OpportunityActivityTypeMasterCenterID)?.Name,
                        OwnerID = activity.Opportunity.OwnerID,
                        OpportunityActivityID = activity.ID
                    };
                    await DB.ActivityTasks.AddAsync(activityTask);

                    if (activity.AppointmentDate != null)
                    {
                        ActivityTask activityTaskAppointment = new ActivityTask()
                        {
                            ProjectID = projectId,
                            ContactFirstName = firstName,
                            ContactLastName = lastName,
                            PhoneNumber = phone,
                            DueDate = activity.DueDate,
                            ActivityTaskOverdueStatusMasterCenterID = overdueStatusId,
                            OverdueDays = overdue,
                            RepeatCount = 0,
                            ActivityTaskStatusMasterCenterID = masterCenters.Find(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.ActivityTaskStatus && o.Key == "2")?.ID,
                            ActivityTaskTopicMasterCenterID = masterCenters.Find(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.ActivityTaskTopic && o.Key == "2")?.ID,
                            ActivityTaskTypeMasterCenterID = masterCenters.Find(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.ActivityTaskType && o.Key == "5")?.ID,
                            ActivityTypeName = masterCenters.Find(o => o.ID == activity.OpportunityActivityTypeMasterCenterID)?.Name,
                            OwnerID = activity.Opportunity.OwnerID,
                            OpportunityActivityID = activity.ID
                        };
                        await DB.ActivityTasks.AddAsync(activityTaskAppointment);
                    }
                }
                catch
                {

                }
            }

            await DB.SaveChangesAsyncByMigrateApp();
        }

        private static async Task UpdateRevisitActivityTask(Guid projectId, List<MasterCenter> masterCenters)
        {
            var activities = await DB.RevisitActivities
                .Include(o => o.Opportunity)
                .ThenInclude(o => o.Contact)
                .Where(o => o.Opportunity.ProjectID == projectId).ToListAsync();
            foreach (var activity in activities)
            {
                try
                {
                    string firstName = null;
                    string lastName = null;
                    string phone = null;
                    if (activity.Opportunity.ContactID != null)
                    {
                        firstName = activity.Opportunity.Contact.FirstNameTH;
                        lastName = activity.Opportunity.Contact.LastNameTH;
                        phone = await DB.ContactPhones.Where(o => o.ContactID == activity.Opportunity.ContactID && o.IsMain == true).Select(o => o.PhoneNumber).FirstOrDefaultAsync();
                    }

                    var activityTaskStatus = masterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.ActivityTaskStatus).ToList();
                    Guid? activityTaskStatusId = null;

                    int overdue = 0;
                    Guid? overdueStatusId = masterCenters.Find(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.ActivityTaskOverdueStatus && o.Key == "2")?.ID;
                    activityTaskStatusId = activityTaskStatus.Find(o => o.Key == "2")?.ID;

                    var activityTypeKey = masterCenters.Find(o => o.ID == activity.RevisitActivityTypeMasterCenterID)?.Key;
                    int repeatCount = 0;
                    switch (activityTypeKey)
                    {
                        case "1":
                            repeatCount = 1;
                            break;
                        case "2":
                            repeatCount = 2;
                            break;
                        case "3":
                            repeatCount = 3;
                            break;
                        case "4":
                            repeatCount = 3;
                            break;
                    }

                    ActivityTask activityTask = new ActivityTask()
                    {
                        ProjectID = projectId,
                        ContactFirstName = firstName,
                        ContactLastName = lastName,
                        PhoneNumber = phone,
                        DueDate = activity.ActualDate,
                        ActivityTaskOverdueStatusMasterCenterID = overdueStatusId,
                        OverdueDays = overdue,
                        RepeatCount = repeatCount,
                        ActivityTaskStatusMasterCenterID = activityTaskStatusId,
                        ActivityTaskTopicMasterCenterID = masterCenters.Find(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.ActivityTaskTopic && o.Key == "3")?.ID,
                        ActivityTaskTypeMasterCenterID = masterCenters.Find(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.ActivityTaskType && o.Key == "3")?.ID,
                        ActivityTypeName = masterCenters.Find(o => o.ID == activity.RevisitActivityTypeMasterCenterID)?.Name,
                        OwnerID = activity.Opportunity.OwnerID,
                        RevisitActivityID = activity.ID
                    };
                    await DB.ActivityTasks.AddAsync(activityTask);

                    if (activity.AppointmentDate != null)
                    {
                        ActivityTask activityTaskAppointment = new ActivityTask()
                        {
                            ProjectID = projectId,
                            ContactFirstName = firstName,
                            ContactLastName = lastName,
                            PhoneNumber = phone,
                            DueDate = activity.ActualDate,
                            ActivityTaskOverdueStatusMasterCenterID = overdueStatusId,
                            OverdueDays = overdue,
                            RepeatCount = repeatCount,
                            ActivityTaskStatusMasterCenterID = masterCenters.Find(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.ActivityTaskStatus && o.Key == "2")?.ID,
                            ActivityTaskTopicMasterCenterID = masterCenters.Find(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.ActivityTaskTopic && o.Key == "3")?.ID,
                            ActivityTaskTypeMasterCenterID = masterCenters.Find(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.ActivityTaskType && o.Key == "5")?.ID,
                            ActivityTypeName = masterCenters.Find(o => o.ID == activity.RevisitActivityTypeMasterCenterID)?.Name,
                            OwnerID = activity.Opportunity.OwnerID,
                            RevisitActivityID = activity.ID
                        };
                        await DB.ActivityTasks.AddAsync(activityTaskAppointment);
                    }
                }
                catch
                {

                }
            }

            await DB.SaveChangesAsyncByMigrateApp();
        }

        private static async Task UpdateOpportunityInfo(Guid projectId)
        {
            var opps = await DB.Opportunities.Where(o => o.ProjectID == projectId).ToListAsync();

            foreach (var opp in opps)
            {
                var contactModel = await DB.Contacts.Where(o => o.ID == opp.ContactID).FirstOrDefaultAsync();
                if (contactModel != null)
                {
                    var oppCount = DB.Opportunities.Where(o => o.ContactID == contactModel.ID && o.IsDeleted != true && o.ID != opp.ID).Count();
                    var lastOppModel = DB.Opportunities.Where(o => o.ContactID == contactModel.ID && o.IsDeleted != true && o.ID != opp.ID).OrderByDescending(o => o.Created).FirstOrDefault();

                    contactModel.OpportunityCount = oppCount;
                    contactModel.LastOpportunityID = lastOppModel?.ID;
                    DB.Contacts.Update(contactModel);
                }
            }
        }

        private static async Task UpdateOpportunityActivityInfo(Guid projectId)
        {
            var opps = await DB.Opportunities.Where(o => o.ProjectID == projectId).ToListAsync();

            foreach (var opp in opps)
            {
                var oppact = await DB.OpportunityActivities.Where(o => o.OpportunityID == opp.ID).ToListAsync();
                foreach (var act in oppact)
                {
                    var lastOppActModel = DB.OpportunityActivities.Where(o => o.OpportunityID == act.OpportunityID && o.IsDeleted != true && o.ID != act.ID).OrderByDescending(o => o.Created).FirstOrDefault();

                    opp.LastOpportunityActivityID = lastOppActModel?.ID;
                    DB.Opportunities.Update(opp);
                }
            }
        }

        private static async Task UpdateOpportunityRevisitInfo(Guid projectId)
        {
            var opps = await DB.Opportunities.Where(o => o.ProjectID == projectId).ToListAsync();

            foreach (var opp in opps)
            {
                var oppact = await DB.RevisitActivities.Where(o => o.OpportunityID == opp.ID).ToListAsync();
                foreach (var act in oppact)
                {
                    var oppCount = DB.RevisitActivities.Where(o => o.OpportunityID == act.ID && o.IsDeleted != true && o.ID != act.ID).Count();

                    opp.RevisitActivityCount = oppCount;
                    DB.Opportunities.Update(opp);
                }
            }
        }
    }
}
