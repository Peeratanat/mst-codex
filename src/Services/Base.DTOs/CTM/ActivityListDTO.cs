using Database.Models.CTM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using models = Database.Models;

namespace Base.DTOs.CTM
{
	public class ActivityListDTO
	{
		/// <summary>
		/// ID ของ Activity Task
		/// </summary>
		public Guid? Id { get; set; }
		/// <summary>
		/// โครงการ
		/// </summary>
		public PRJ.ProjectDTO Project { get; set; }
		/// <summary>
		/// ชื่อลูกค้า
		/// </summary>
		public string ContactFirstName { get; set; }
		/// <summary>
		/// นามสกุลลูกค้า
		/// </summary>
		public string ContactLastName { get; set; }
		/// <summary>
		/// เบอร์โทรศัพท์
		/// </summary>
		public string PhoneNumber { get; set; }
		/// <summary>
		/// วันที่ต้องทำ (Plan)
		/// </summary>
		public DateTime? DueDate { get; set; }
		/// <summary>
		/// วันที่ทำจริง (Plan)
		/// </summary>
		public DateTime? ActualDate { get; set; }
		/// <summary>
		/// สถานะ Overdue
		/// masterdata/api/MasterCenters/DropdownList?masterCenterGroupKey=ActivityTaskOverdueStatus
		/// </summary>
		public MST.MasterCenterDropdownDTO OverdueStatus { get; set; }
		/// <summary>
		/// จำนวนวันที่ Overdue (บวก = ยังไม่ overdue, ลบ = overdue แล้ว, 0 = ถึงกำหนดแล้ว)
		/// </summary>
		public int? OverdueDays { get; set; }
		/// <summary>
		/// จำนวนครั้ง เช่น Revisit ครั้งที่ 2 = 2
		/// </summary>
		public int? RepeatCount { get; set; }
		/// <summary>
		/// สถานะของ Activity Task
		/// masterdata/api/MasterCenters/DropdownList?masterCenterGroupKey=ActivityTaskStatus
		/// </summary>
		public MST.MasterCenterDropdownDTO ActivityTaskStatus { get; set; }
		/// <summary>
		/// หัวข้อของ Activity Task
		/// masterdata/api/MasterCenters/DropdownList?masterCenterGroupKey=ActivityTaskTopic
		/// </summary>
		public MST.MasterCenterDropdownDTO ActivityTaskTopic { get; set; }
		/// <summary>
		/// ชนิดของ Activity Task
		/// masterdata/api/MasterCenters/DropdownList?masterCenterGroupKey=ActivityTaskType
		/// </summary>
		public MST.MasterCenterDropdownDTO ActivityTaskType { get; set; }
		/// <summary>
		/// ชื่อของประเภท Activity ของแต่ละชนิด (Lead, Walk, Revisit)
		/// </summary>
		public string ActivityTypeName { get; set; }
		/// <summary>
		/// ผู้ดูแล
		/// </summary>
		public USR.UserListDTO Owner { get; set; }
		/// <summary>
		/// Ref Lead Activity
		/// </summary>
		public CTM.LeadActivityListDTO LeadActivity { get; set; }
		/// <summary>
		/// Ref Opportunity Activity
		/// </summary>
		public CTM.OpportunityActivityListDTO OpportunityActivity { get; set; }
		/// <summary>
		/// Ref Revisit Activity
		/// </summary>
		public CTM.RevisitActivityListDTO RevisitActivity { get; set; }
		/// <summary>
		/// ประเภท กรณี Lead (Call, Web, Facebook)_
		/// </summary>
		public MST.MasterCenterDropdownDTO LeadType { get; set; }

		public bool IsHidePhone { get; set; }
		public LeadListDTO Lead { get; set; }

		public static ActivityListDTO CreateFromQueryResult(ActivityQueryResult model, models.DatabaseContext DB)
		{
			if (model != null)
			{
				ActivityListDTO result = new ActivityListDTO()
				{
					Id = model.ActivityTask.ID,
					Project = PRJ.ProjectDTO.CreateFromModel(model.Project),
					ContactFirstName = model.ActivityTask.ContactFirstName,
					ContactLastName = model.ActivityTask.ContactLastName,
					PhoneNumber = model.ActivityTask.PhoneNumber,
					DueDate = model.ActivityTask.DueDate,
					OverdueStatus = MST.MasterCenterDropdownDTO.CreateFromModel(model.OverdueStatus),
					OverdueDays = model.ActivityTask.OverdueDays,
					RepeatCount = model.ActivityTask.RepeatCount,
					ActivityTaskStatus = MST.MasterCenterDropdownDTO.CreateFromModel(model.ActivityTaskStatus),
					ActivityTaskTopic = MST.MasterCenterDropdownDTO.CreateFromModel(model.ActivityTaskTopic),
					ActivityTaskType = MST.MasterCenterDropdownDTO.CreateFromModel(model.ActivityTaskType),
					ActivityTypeName = model.ActivityTask.ActivityTypeName,
					Owner = USR.UserListDTO.CreateFromModel(model.Owner),
					LeadActivity = CTM.LeadActivityListDTO.CreateFromModel(model.LeadActivity),
					OpportunityActivity = CTM.OpportunityActivityListDTO.CreateFromModel(model.OpportunityActivity),
					RevisitActivity = CTM.RevisitActivityListDTO.CreateFromModel(model.RevisitActivity, DB),
					LeadType = MST.MasterCenterDropdownDTO.CreateMasterLeadTypeFromModel(model.LeadType),
					Lead = model.Lead != null ? LeadListDTO.CreateFromModel(model.Lead) : null
				};

				if (model.LeadActivity != null)
					result.ActualDate = model.LeadActivity.ActualDate;
				else if (model.OpportunityActivity != null)
					result.ActualDate = model.OpportunityActivity.ActualDate;
				else if (model.RevisitActivity != null)
					result.ActualDate = model.RevisitActivity.ActualDate;


				var projectnos = new List<string>() {
					"40044", "60021", "60025", "60031", "70002", "70020", "70037", "70039", "70045"
				};

				if (result.PhoneNumber != null && model.LeadActivity != null)
				{

					result.IsHidePhone = true; //Hide PhoneNumber

					//var currentDateAdd2 = DateTime.Now.AddDays(2).Date;
					//var currentDateDelete30 = DateTime.Now.AddDays(-30).Date;
					//if (model.LeadActivity.DueDate != null)
					//{
					//	if (model.LeadActivity.DueDate.Value.Date > currentDateAdd2 || model.LeadActivity.DueDate.Value.Date < currentDateDelete30)
					//	{
					//		result.IsHidePhone = false; //Show PhoneNumber
					//	}
					//}

					//result.IsHidePhone = true;
					//    foreach (var o in projectnos)
					//    {
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

				result.IsHidePhone = true;

				return result;
			}
			else
			{
				return null;
			}
		}

		public static void SortBy(ActivityListSortByParam sortByParam, ref IQueryable<ActivityQueryResult> query)
		{
			if (sortByParam.SortBy != null)
			{
				switch (sortByParam.SortBy.Value)
				{
					case ActivityListSortBy.ActivityTaskTopic:
						if (sortByParam.Ascending) query = query.OrderBy(o => o.ActivityTaskTopic.Name);
						else query = query.OrderByDescending(o => o.ActivityTaskStatus.Name);
						break;
					case ActivityListSortBy.LeadType:
						if (sortByParam.Ascending) query = query.OrderBy(o => o.LeadType.Name);
						else query = query.OrderByDescending(o => o.LeadType.Name);
						break;
					case ActivityListSortBy.ActivityTaskType:
						if (sortByParam.Ascending) query = query.OrderBy(o => o.ActivityTaskType.Name);
						else query = query.OrderByDescending(o => o.ActivityTaskType.Name);
						break;
					case ActivityListSortBy.Project:
						if (sortByParam.Ascending) query = query.OrderBy(o => o.Project.ProjectNo);
						else query = query.OrderByDescending(o => o.Project.ProjectNo);
						break;
					case ActivityListSortBy.FirstName:
						if (sortByParam.Ascending) query = query.OrderBy(o => o.ActivityTask.ContactFirstName);
						else query = query.OrderByDescending(o => o.ActivityTask.ContactFirstName);
						break;
					case ActivityListSortBy.LastName:
						if (sortByParam.Ascending) query = query.OrderBy(o => o.ActivityTask.ContactLastName);
						else query = query.OrderByDescending(o => o.ActivityTask.ContactLastName);
						break;
					case ActivityListSortBy.PhoneNumber:
						if (sortByParam.Ascending) query = query.OrderBy(o => o.ActivityTask.PhoneNumber);
						else query = query.OrderByDescending(o => o.ActivityTask.PhoneNumber);
						break;
					case ActivityListSortBy.OverdueDays:
						if (sortByParam.Ascending) query = query.OrderBy(o => o.ActivityTask.OverdueDays);
						else query = query.OrderByDescending(o => o.ActivityTask.OverdueDays);
						break;
					case ActivityListSortBy.Owner:
						if (sortByParam.Ascending) query = query.OrderBy(o => o.Owner.FirstName);
						else query = query.OrderByDescending(o => o.Owner.FirstName);
						break;
					case ActivityListSortBy.ActivityTaskStatus:
						if (sortByParam.Ascending) query = query.OrderBy(o => o.ActivityTaskStatus.Name);
						else query = query.OrderByDescending(o => o.ActivityTaskStatus.Name);
						break;
					case ActivityListSortBy.DueDate:
						if (sortByParam.Ascending) query = query.OrderBy(o => o.ActivityTask.DueDate).ThenByDescending(o => o.ActivityTaskStatus.Order);
						else query = query.OrderByDescending(o => o.ActivityTask.DueDate).ThenByDescending(o => o.ActivityTaskStatus.Order);
						break;
                    case ActivityListSortBy.SubLeadType:
						if (sortByParam.Ascending) query = query.OrderBy(o => o.Lead.SubLeadType);
                        else query = query.OrderByDescending(o => o.Lead.SubLeadType);
                        break;
                    default:
						query = query.OrderBy(o => o.ActivityTask.DueDate).ThenByDescending(o => o.ActivityTaskStatus.Order);
						break;
				}
			}
			else
			{
				query = query.OrderBy(o => o.ActivityTask.DueDate).ThenByDescending(o => o.ActivityTaskStatus.Order);
			}
		}
	}

	public class ActivityQueryResult
	{
		public models.CTM.ActivityTask ActivityTask { get; set; }
		public models.MST.MasterCenter OverdueStatus { get; set; }
		public models.MST.MasterCenter ActivityTaskStatus { get; set; }
		public models.MST.MasterCenter ActivityTaskTopic { get; set; }
		public models.MST.MasterCenter ActivityTaskType { get; set; }
		public models.PRJ.Project Project { get; set; }
		public models.USR.User Owner { get; set; }
		public models.CTM.LeadActivity LeadActivity { get; set; }
		public models.CTM.OpportunityActivity OpportunityActivity { get; set; }
		public models.CTM.RevisitActivity RevisitActivity { get; set; }
		public models.MST.MasterCenter LeadType { get; set; }
		public models.CTM.Lead Lead { get; set; } 
	}
}