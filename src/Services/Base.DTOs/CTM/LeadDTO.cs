using Database.Models.CTM;
using Database.Models.MST;
using ErrorHandling;
using Microsoft.EntityFrameworkCore;
using Minio.DataModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Reflection.Metadata;
using System.Threading.Tasks;
using models = Database.Models;

namespace Base.DTOs.CTM
{
    public class LeadDTO
    {
        /// <summary>
        /// ID ของ Lead
        /// </summary>
        public Guid? Id { get; set; }
        /// <summary>
        /// ชื่อจริง
        /// </summary>
        [Description("ชื่อจริง")]
        public string FirstName { get; set; }
        /// <summary>
        /// นามสกุล
        /// </summary>
        [Description("นามสกุล")]
        public string LastName { get; set; }
        /// <summary>
        /// เบอร์โทรศัพท์
        /// </summary>
        [Description("เบอร์โทรศัพท์")]
        public string PhoneNumber { get; set; }
        /// <summary>
        /// เบอร์โทรศัพท์บ้าน
        /// </summary>
        [Description("เบอร์โทรศัพท์บ้าน")]
        public string Telephone { get; set; }
        /// <summary>
        /// เบอร์โทรศัพท์ (ต่อ)
        /// </summary>
        public string TelephoneExt { get; set; }
        /// <summary>
        /// อีเมล
        /// </summary>
        [Description("อีเมล")]
        public string Email { get; set; }
        /// <summary>
        /// ประเภทของ Lead
        /// masterdata/api/MasterCenters/DropdownList?masterCenterGroupKey=LeadType
        /// </summary>
        public MST.MasterCenterDropdownDTO LeadType { get; set; }
        /// <summary>
        /// ประเภทย่อยของ Lead
        /// </summary>
        public string SubLeadType { get; set; }
        /// <summary>
        /// ผู้ดูแล Lead
        /// </summary>
        public USR.UserListDTO Owner { get; set; }
        /// <summary>
        /// โครงการ
        /// project/api/Projects/DropdownList
        /// </summary>
        [Description("โครงการ")]
        public PRJ.ProjectDTO Project { get; set; }
        /// <summary>
        /// โซนที่พักอาศัย
        /// </summary>
        public string VisitZone { get; set; }
        /// <summary>
        /// มาจากสื่อ
        /// masterdata/api/MasterCenters/DropdownList?masterCenterGroupKey=Advertisement
        /// </summary>
        public MST.MasterCenterDropdownDTO Advertisement { get; set; }
        /// <summary>
        /// Remark
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// คะแนนของ Lead
        /// </summary>
        public double? LeadScore { get; set; }
        /// <summary>
        /// สถานะของ Lead
        /// masterdata/api/MasterCenters/DropdownList?masterCenterGroupKey=LeadStatus
        /// </summary>
        public MST.MasterCenterDropdownDTO LeadStatus { get; set; }
        /// <summary>
        /// Campaign
        /// </summary>
        public string Campaign { get; set; }
        /// <summary>
        /// บ้านเลขที่
        /// </summary>
        public string HouseNo { get; set; }
        /// <summary>
        /// หมู่
        /// </summary>
        public string Moo { get; set; }
        /// <summary>
        /// หมู่บ้าน/อาคาร
        /// </summary>
        public string Village { get; set; }
        /// <summary>
        /// ซอย
        /// </summary>
        public string Soi { get; set; }
        /// <summary>
        /// ถนน
        /// </summary>
        public string Road { get; set; }
        /// <summary>
        /// รหัสไปรษณีย์
        /// </summary>
        public string PostalCode { get; set; }
        /// <summary>
        /// ประเทศ
        /// </summary>
        public string Country { get; set; }
        /// <summary>
        /// จังหวัด
        /// </summary>
        public string Province { get; set; }
        /// <summary>
        /// เขต/อำเภอ
        /// </summary>
        public string District { get; set; }
        /// <summary>
        /// แขวง/ตำบล
        /// </summary>
        public string SubDistrict { get; set; }
        /// <summary>
        /// ขนาดห้อง
        /// </summary>
        public string RoomSize { get; set; }
        /// <summary>
        /// ประเภทห้อง
        /// </summary>
        public string RoomType { get; set; }
        /// <summary>
        /// จำนวนคน
        /// </summary>
        public string NumberOfPerson { get; set; }
        /// <summary>
        /// จำนวน Unit
        /// </summary>
        public string NumberOfUnit { get; set; }
        /// <summary>
        /// จำนวนครั้งที่ติดต่อ
        /// </summary>
        public string NumberOfContact { get; set; }
        /// <summary>
        /// อำเภอที่ทำงาน
        /// </summary>
        public string DistrictOfWorking { get; set; }
        /// <summary>
        /// จังหวัดที่ทำงาน
        /// </summary>
        public string ProvinceOfWorking { get; set; }
        /// <summary>
        /// LeadVisitDate
        /// </summary>
        public DateTime? LeadVisitDate { get; set; }
        /// <summary>
        /// LeadVisitTime
        /// </summary>
        public string LeadVisitTime { get; set; }
        /// <summary>
        /// ลูกค้าต้องการให้โทรกลับหรือไม่?
        /// </summary>
        public bool? CallBack { get; set; }
        /// <summary>
        /// หมายเลขบัตรประชาชน
        /// </summary>
        public string CitizenIdentityNo { get; set; }
        /// <summary>
        /// UTMSource
        /// </summary>
        public string UTMSource { get; set; }
        /// <summary>
        /// UTMMedium
        /// </summary>
        public string UTMMedium { get; set; }
        /// <summary>
        /// UTMCampaign
        /// </summary>
        public string UTMCampaign { get; set; }
        /// <summary>
        /// วันที่สร้าง
        /// </summary>
        public DateTime? CreatedDate { get; set; }
        /// <summary>
        /// LeadDate
        /// </summary>
        public DateTime? LeadDate { get; set; }
        /// <summary>
        /// ConsentType PPDA
        /// masterdata/api/MasterCenters/DropdownList?masterCenterGroupKey=ConsentType
        /// </summary>
        public MST.MasterCenterDropdownDTO ConsentTypeStatus { get; set; }
        /// <summary>
        /// IsSendToBC
        /// </summary>
        public bool? IsSendToBC { get; set; }

        public List<LeadScoreListDTO> LeadScoreList { get; set; }
        public bool IsHidePhone { get; set; }

        /// <summary>
        /// SubLeadTypeMaster ประเภทย่อย
        /// </summary>
        public MST.MasterCenterDropdownDTO SubLeadTypeMaster { get; set; }

        /// <summary>
        /// IsAgency
        /// </summary>
        public bool IsAgency { get; set; }
        /// <summary>
        /// RefID
        /// </summary>
        public string RefID { get; set; }

        /// <summary>
        /// แปลงที่สนใจ
        /// </summary>
        public string UnitNo { get; set; }

        public string LeadAppointDay { get; set; }

        public string LeadAppointTime { get; set; }

        public string LeadCallbackDay { get; set; }

        public string LeadCallbackTime { get; set; }

        public string LineID { get; set; }

        public List<LeadAssignHistory> LeadAssignHistoryList { get; set; }

        public async static Task<LeadDTO> CreateFromModel(models.CTM.Lead model, models.DatabaseContext DB)
        {
            if (model != null)
            {
                LeadDTO result = new LeadDTO()
                {
                    Id = model.ID,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    PhoneNumber = model.PhoneNumber,
                    Campaign = String.IsNullOrEmpty(model.Compaign) ? "-" : model.Compaign,
                    LeadType = MST.MasterCenterDropdownDTO.CreateFromModel(model.LeadType),
                    SubLeadType = model.SubLeadType,
                    Owner = USR.UserListDTO.CreateFromModel(model.Owner),
                    Project = PRJ.ProjectDTO.CreateFromModel(model.Project),
                    VisitZone = model.VisitZone,
                    Advertisement = MST.MasterCenterDropdownDTO.CreateFromModel(model.Advertisement),
                    Remark = model.Remark,
                    LeadStatus = MST.MasterCenterDropdownDTO.CreateFromModel(model.LeadStatus),
                    CreatedDate = model.Created,
                    Telephone = model.Telephone,
                    TelephoneExt = model.TelephoneExt,
                    HouseNo = model.HouseNo,
                    Moo = model.Moo,
                    Village = model.Village,
                    Soi = model.Soi,
                    Road = model.Road,
                    District = model.District,
                    SubDistrict = model.SubDistrict,
                    Province = model.Province,
                    PostalCode = model.PostalCode,
                    Email = model.Email,
                    RoomSize = model.RoomSize,
                    RoomType = model.RoomType,
                    NumberOfPerson = model.NumberOfPerson,
                    NumberOfContact = model.NumberOfContact,
                    NumberOfUnit = model.NumberOfUnit,
                    DistrictOfWorking = model.DistrictOfWorking,
                    ProvinceOfWorking = model.ProvinceOfWorking,
                    LeadVisitDate = model.LeadVisitDate,
                    LeadVisitTime = model.LeadVisitTime,
                    CallBack = model.CallBack,
                    CitizenIdentityNo = model.CitizenIdentityNo,
                    UTMCampaign = model.UTMCampaign,
                    UTMMedium = model.UTMMedium,
                    UTMSource = model.UTMSource,
                    Country = model.Country,
                    LeadDate = model.LeadDate,
                    IsAgency = model.RefMigrateID3 == "Lead Agency" ? true : false,
                    RefID = model.RefID,
                    LineID = model.LineID,
                    LeadAppointDay = model.LeadAppointDay,
                    LeadAppointTime = model.LeadAppointTime,
                    LeadCallbackDay = model.LeadCallbackDay,
                    LeadCallbackTime = model.LeadCallbackTime,

                };

                var leadScoring = await DB.LeadScorings
                    .Include(o => o.LeadScoringType)
                    .Where(o => o.LeadID == model.ID && o.IsLatestScore == true)
                    .OrderBy(o => o.LeadScoringType.Order).ToListAsync();

                if(leadScoring.Count > 0)
                {
                    result.LeadScoreList = leadScoring.Select(o => LeadScoreListDTO.CreateFromModel(o)).ToList();
                    if(model.LeadScore != null)
                    {
                        result.LeadScore = model.LeadScore;
                    }
                    else
                    {
                        result.LeadScore = leadScoring.Where(o => o.IsGetScore == true).Sum(o => o.Score);
                    }
                }
                else
                {
                    result.LeadScore = 0;
                    result.LeadScoreList = new List<LeadScoreListDTO>();

                    var leadScoringTypeList = await DB.LeadScoringTypes.OrderBy(o => o.Order).ToListAsync();
                    
                    foreach(var item in leadScoringTypeList)
                    {
                        LeadScoreListDTO score = new LeadScoreListDTO()
                        {
                            LeadID = model.ID,
                            LeadScoringTypeID = item.ID,
                            Order = item.Order,
                            Topic = item.Topic,
                            Score = 0,
                            IsGetScore = false
                        };

                        result.LeadScoreList.Add(score);
                    }
                }



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
                    //    else
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
        public async static Task<LeadDTO> CreateHidePhoneFromModel(models.CTM.Lead model, models.DatabaseContext DB, List<LeadActivity> leadActivitiess, MasterCenter master, bool isUserRoleBC)
        {
            if (model != null)
            {
                var prj = await DB.Projects.Include(o => o.BG).Where(o => o.ID == model.ProjectID).FirstOrDefaultAsync();
                LeadDTO result = new LeadDTO()
                {
                    Id = model.ID,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    PhoneNumber = model.PhoneNumber,
                    Campaign = String.IsNullOrEmpty(model.Compaign) ? "-" : model.Compaign,
                    LeadType = MST.MasterCenterDropdownDTO.CreateFromModel(model.LeadType),
                    SubLeadType = model.SubLeadType,
                    Owner = USR.UserListDTO.CreateFromModel(model.Owner),
                    Project = PRJ.ProjectDTO.CreateFromModel(model.Project),
                    VisitZone = model.VisitZone,
                    Advertisement = MST.MasterCenterDropdownDTO.CreateFromModel(model.Advertisement),
                    Remark = model.Remark,
                    LeadStatus = MST.MasterCenterDropdownDTO.CreateFromModel(model.LeadStatus),
                    CreatedDate = model.Created,
                    Telephone = model.Telephone,
                    TelephoneExt = model.TelephoneExt,
                    HouseNo = model.HouseNo,
                    Moo = model.Moo,
                    Village = model.Village,
                    Soi = model.Soi,
                    Road = model.Road,
                    District = model.District,
                    SubDistrict = model.SubDistrict,
                    Province = model.Province,
                    PostalCode = model.PostalCode,
                    Email = model.Email,
                    RoomSize = model.RoomSize,
                    RoomType = model.RoomType,
                    NumberOfPerson = model.NumberOfPerson,
                    NumberOfContact = model.NumberOfContact,
                    NumberOfUnit = model.NumberOfUnit,
                    DistrictOfWorking = model.DistrictOfWorking,
                    ProvinceOfWorking = model.ProvinceOfWorking,
                    LeadVisitDate = model.LeadVisitDate,
                    LeadVisitTime = model.LeadVisitTime,
                    CallBack = model.CallBack,
                    CitizenIdentityNo = model.CitizenIdentityNo,
                    UTMCampaign = model.UTMCampaign,
                    UTMMedium = model.UTMMedium,
                    UTMSource = model.UTMSource,
                    Country = model.Country,
                    LeadDate = model.LeadDate,
                    ConsentTypeStatus = MST.MasterCenterDropdownDTO.CreateFromModel(model.ConsentType),
                    IsSendToBC = model.IsSendToBC,
                    RefID = model.RefID,
                    SubLeadTypeMaster = MST.MasterCenterDropdownDTO.CreateFromModel(master),
                    UnitNo = model.UnitNo,
                    LeadAppointDay = model.LeadAppointDay,
                    LeadAppointTime = model.LeadAppointTime,
                    LeadCallbackDay = model.LeadCallbackDay,
                    LeadCallbackTime = model.LeadCallbackTime,
                    LineID = model.LineID,
                };

                var leadScoring = await DB.LeadScorings
                    .Include(o => o.LeadScoringType)
                    .Where(o => o.LeadID == model.ID && o.IsLatestScore == true)
                    .OrderBy(o => o.LeadScoringType.Order).ToListAsync();

                if (leadScoring.Count > 0)
                {
                    result.LeadScoreList = leadScoring.Select(o => LeadScoreListDTO.CreateFromModel(o)).ToList();
                    if (model.LeadScore != null)
                    {
                        result.LeadScore = model.LeadScore;
                    }
                    else
                    {
                        result.LeadScore = leadScoring.Where(o => o.IsGetScore == true).Sum(o => o.Score);
                    }
                }
                else
                {
                    result.LeadScore = 0;
                    result.LeadScoreList = new List<LeadScoreListDTO>();

                    var leadScoringTypeList = await DB.LeadScoringTypes.OrderBy(o => o.Order).ToListAsync();

                    foreach (var item in leadScoringTypeList)
                    {
                        LeadScoreListDTO score = new LeadScoreListDTO()
                        {
                            LeadID = model.ID,
                            LeadScoringTypeID = item.ID,
                            Order = item.Order,
                            Topic = item.Topic,
                            Score = 0,
                            IsGetScore = false
                        };

                        result.LeadScoreList.Add(score);
                    }
                }



                var projectnos = new List<string>() {
                    "40044", "60021", "60025", "60031", "70002", "70020", "70037", "70039", "70045"
                };

                if (result.PhoneNumber != null)
                {
                    result.IsHidePhone = isUserRoleBC ? false : true; //Hide PhoneNumber

                    //var leadAct = leadActivitiess.OrderByDescending(o => o.Created).FirstOrDefault();
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
                    //    else
                    //    {
                    //        result.IsHidePhone = false;


                    //    }


                    //}
                }

                var leadAssignHistorys = DB.LeadAssigns
                                .Include(o => o.Owner)
                                .Include(o => o.CreatedBy)
                            .Where(o => o.LeadID == model.ID)
                    .OrderByDescending(o => o.Created)
                    .ToList();

                if (leadAssignHistorys.Count > 0)
                {
                    result.LeadAssignHistoryList = new List<LeadAssignHistory>();
                    foreach (var item in leadAssignHistorys)
                    {
                        var displayName = "";
                        if (item.CreatedBy != null)
                        {
                            if (!item.CreatedBy.IsClient)
                            {
                                displayName = item.CreatedBy.DisplayName;
                            }
                            else
                            {
                                displayName = "Auto Assign by CRM System";
                            }
                        }
                        else
                        {
                            displayName = "Auto Assign by CRM System";
                        }

                        LeadAssignHistory lah = new LeadAssignHistory()
                        {
                            LCOwner = item.Owner != null ? item.Owner.DisplayName : "",
                            AssignBy = displayName,
                            AssignByOriginal = displayName,
                            AssignDate = item.Created
                        };

                        result.LeadAssignHistoryList.Add(lah);
                    }
                }

                return result;
            }
            else
            {
                return null;
            }
        }

        public class LeadAssignHistory
        {
            public string LCOwner { get; set; }

            public string AssignBy { get; set; }

            public string AssignByOriginal { get; set; }

            public DateTime? AssignDate { get; set; }
        }

        public async Task ValidateAsync(models.DatabaseContext DB, bool isCreate)
        {
            ValidateException ex = new ValidateException();
            if (this.Project == null)
            {
                var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                string desc = this.GetType().GetProperty(nameof(LeadDTO.Project)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = errMsg.Message.Replace("[field]", desc);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }

            if (string.IsNullOrEmpty(this.FirstName))
            {
                var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                string desc = this.GetType().GetProperty(nameof(LeadDTO.FirstName)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = errMsg.Message.Replace("[field]", desc);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }
            else
            {
                if (!this.FirstName.CheckAllLang(false, false, false, null, " "))
                {
                    var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0012").FirstAsync();
                    string desc = this.GetType().GetProperty(nameof(LeadDTO.FirstName)).GetCustomAttribute<DescriptionAttribute>().Description;
                    var msg = errMsg.Message.Replace("[field]", desc);
                    ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                }
            }

            if (string.IsNullOrEmpty(this.LastName))
            {
                var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                string desc = this.GetType().GetProperty(nameof(LeadDTO.LastName)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = errMsg.Message.Replace("[field]", desc);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }
            else
            {
                if (!this.LastName.CheckAllLang(false, false, false, null, " "))
                {
                    var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0012").FirstAsync();
                    string desc = this.GetType().GetProperty(nameof(LeadDTO.LastName)).GetCustomAttribute<DescriptionAttribute>().Description;
                    var msg = errMsg.Message.Replace("[field]", desc);
                    ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                }
            }

            if (string.IsNullOrEmpty(this.PhoneNumber))
            {
                var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                string desc = this.GetType().GetProperty(nameof(LeadDTO.PhoneNumber)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = errMsg.Message.Replace("[field]", desc);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }
            else
            {
                if (!this.PhoneNumber.IsOnlyNumber() && isCreate)
                {
                    var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0004").FirstAsync();
                    string desc = this.GetType().GetProperty(nameof(LeadDTO.PhoneNumber)).GetCustomAttribute<DescriptionAttribute>().Description;
                    var msg = errMsg.Message.Replace("[field]", desc);
                    ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                }
            }

            if (!string.IsNullOrEmpty(this.Telephone))
            {
                if (!this.Telephone.IsOnlyNumber())
                {
                    var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0004").FirstAsync();
                    string desc = this.GetType().GetProperty(nameof(LeadDTO.Telephone)).GetCustomAttribute<DescriptionAttribute>().Description;
                    var msg = errMsg.Message.Replace("[field]", desc);
                    ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                }
            }

            if (!string.IsNullOrEmpty(this.Email))
            {
                if (!this.Email.IsValidEmail())
                {
                    var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0015").FirstAsync();
                    var msg = errMsg.Message;
                    ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                }
            }

            if (ex.HasError)
            {
                throw ex;
            }
        }

        public void ToModel(ref models.CTM.Lead model)
        {
            model.FirstName = this.FirstName;
            model.LastName = this.LastName;
            model.ProjectID = this.Project?.Id;
            model.VisitZone = this.VisitZone;
            model.AdvertisementMasterCenterID = this.Advertisement?.Id;
            model.Remark = this.Remark;
            model.Created = this.CreatedDate;
            model.Compaign = this.Campaign;
            model.Telephone = this.Telephone;
            model.TelephoneExt = this.TelephoneExt;
            model.HouseNo = this.HouseNo;
            model.Moo = this.Moo;
            model.Village = this.Village;
            model.Soi = this.Soi;
            model.Road = this.Road;
            model.District = this.District;
            model.SubDistrict = this.SubDistrict;
            model.Province = this.Province;
            model.PostalCode = this.PostalCode;
            model.Email = this.Email;
            model.RoomSize = this.RoomSize;
            model.RoomType = this.RoomType;
            model.NumberOfPerson = this.NumberOfPerson;
            model.NumberOfContact = this.NumberOfContact;
            model.NumberOfUnit = this.NumberOfUnit;
            model.DistrictOfWorking = this.DistrictOfWorking;
            model.ProvinceOfWorking = this.ProvinceOfWorking;
            model.LeadVisitDate = this.LeadVisitDate;
            model.LeadVisitTime = this.LeadVisitTime;
            model.CallBack = this.CallBack;
            model.CitizenIdentityNo = this.CitizenIdentityNo;
            model.UTMCampaign = this.UTMCampaign;
            model.UTMMedium = this.UTMMedium;
            model.UTMSource = this.UTMSource;
            //model.PhoneNumber = this.PhoneNumber;
            model.Country = this.Country;
        }
    }
}
