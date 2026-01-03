using Base.DTOs;
using Base.DTOs.MST;
using Base.DTOs.PRJ;
using Common.Helper.Logging;
using Confluent.Kafka;
using Dapper;
using Database.Models;
using Database.Models.DbQueries;
using Database.Models.DbQueries.MST;
using Database.Models.LOG;
using Database.Models.MasterKeys;
using Database.Models.MST;
using ErrorHandling;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Newtonsoft.Json;
using PagingExtensions;
using PRJ_CombineUnit.Params.Filters;
using PRJ_CombineUnit.Params.Outputs;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using static Base.DTOs.MST.CombineUnitDTO;
using static Base.DTOs.ParamMail;
using static Database.Models.DbQueries.DBQueryParam;

namespace PRJ_CombineUnit.Services
{
    public class CombineUnitService : ICombineUnitService
    {
        private readonly DatabaseContext DB;
        public LogModel logModel { get; set; }
        public CombineUnitService(DatabaseContext db)
        {
            logModel = new LogModel("LetterOfGuaranteeService", null);
            this.DB = db;
        }

        public async Task<CombineUnitPaging> GetCombineUnitList(CombineUnitFilter filter, PageParam pageParam, CombineUnitSortByParam sortByParam, CancellationToken cancellationToken = default)
        {
            using DbCommand cmd = DB.Database.GetDbConnection().CreateCommand();
            DynamicParameters ParamList = new DynamicParameters();
            ParamList.Add("ProjectID", filter.ProjectID);
            ParamList.Add("UnitNo", filter.UnitNo);
            ParamList.Add("UnitNoCombine", filter.UnitNoCombine);
            ParamList.Add("CombineStatusID", filter.CombineStatusMasterCenterID);
            ParamList.Add("CombineDocTypeID", filter.CombineDocTypeMasterCenterID);
            ParamList.Add("UpdatedBy", filter.UpdatedBy);
            ParamList.Add("UpdatedFrom", filter.UpdatedFrom);
            ParamList.Add("UpdatedTo", filter.UpdatedTo);
            ParamList.Add("UserID", filter.UserID);

            var sortby = string.Empty;
            bool sort = false;
            sortby = nameof(CombineUnitSortBy.Updated);
            if (sortByParam.SortBy != null)
            {
                sort = sortByParam.Ascending;
                switch (sortByParam.SortBy.Value)
                {
                    case CombineUnitSortBy.Project:
                        sortby = nameof(CombineUnitSortBy.Project);
                        break;
                    case CombineUnitSortBy.Unit:
                        sortby = nameof(CombineUnitSortBy.Unit);
                        break;
                    case CombineUnitSortBy.UnitCombine:
                        sortby = nameof(CombineUnitSortBy.UnitCombine);
                        break;
                    case CombineUnitSortBy.CombineDocType:
                        sortby = nameof(CombineUnitSortBy.CombineDocType);
                        break;
                    case CombineUnitSortBy.CombineStatus:
                        sortby = nameof(CombineUnitSortBy.CombineStatus);
                        break;
                    case CombineUnitSortBy.ApproveDate:
                        sortby = nameof(CombineUnitSortBy.ApproveDate);
                        break;
                    case CombineUnitSortBy.ApproveBy:
                        sortby = nameof(CombineUnitSortBy.ApproveBy);
                        break;
                    case CombineUnitSortBy.Updated:
                        sortby = nameof(CombineUnitSortBy.Updated);
                        break;
                    case CombineUnitSortBy.UpdatedBy:
                        sortby = nameof(CombineUnitSortBy.UpdatedBy);
                        break;
                    default:
                        sortby = nameof(CombineUnitSortBy.Updated);
                        break;
                }
            }


            ParamList.Add("@Sys_SortBy", sortby);
            ParamList.Add("@Sys_SortType", sort ? "asc" : "desc");
            ParamList.Add("@Page", pageParam?.Page ?? 1);
            ParamList.Add("@PageSize", pageParam?.PageSize ?? 999);

            CommandDefinition commandDefinition = new(
                                 commandText: DBStoredNames.spGetUnitCombineList,
                                 parameters: ParamList,
                                 cancellationToken: cancellationToken,
                                 transaction: DB?.Database?.CurrentTransaction?.GetDbTransaction(),
                                 commandType: CommandType.StoredProcedure);
            var query = await cmd.Connection.QueryAsync<dbqUnitCombine>(commandDefinition) ?? new List<dbqUnitCombine>();

            var querylist = query?.ToList();
            var results = new List<CombineUnitDTO>();
            PageOutput pageout = new PageOutput();
            var queryFirst = querylist?.FirstOrDefault();
            if (querylist?.Count > 0)
            {
                results = querylist.Select(o => CreateFromQuery(o)).ToList();
                pageout = queryFirst != null ? queryFirst.CreateBaseDTOFromQuery() : new PageOutput();
            }
            return new CombineUnitPaging()
            {
                PageOutput = pageout,
                CombineUnit = results
            };
        }
        public async Task<CombineUnitPaging> GetCombineHistoryList(Guid? CombineID, PageParam pageParam, CancellationToken cancellationToken = default)
        {
            using DbCommand cmd = DB.Database.GetDbConnection().CreateCommand();
            DynamicParameters ParamList = new DynamicParameters();
            ParamList.Add("CombineID", CombineID);
            ParamList.Add("Page", pageParam?.Page ?? 1);
            ParamList.Add("PageSize", pageParam?.PageSize ?? 999);

            CommandDefinition commandDefinition = new(
                                         commandText: DBStoredNames.spGetUnitCombineHistoryList,
                                         cancellationToken: cancellationToken,
                                         parameters: ParamList,
                                         transaction: DB?.Database?.CurrentTransaction?.GetDbTransaction(),
                                         commandType: CommandType.StoredProcedure);
            var query = await cmd.Connection.QueryAsync<dbqUnitCombine>(commandDefinition) ?? new List<dbqUnitCombine>();

            var querylist = query.ToList();
            var results = new List<CombineUnitDTO>();
            PageOutput pageout = new PageOutput();
            if (querylist.Count > 0)
            {
                results = querylist.Select(o => CreateFromQuery(o)).ToList();
                pageout = querylist.FirstOrDefault() != null ? querylist.FirstOrDefault().CreateBaseDTOFromQuery() : new PageOutput();
            }
            return new CombineUnitPaging()
            {
                PageOutput = pageout,
                CombineUnit = results
            };
        }

        public async Task<List<CombineUnitDTO>> CreateCombineUnitAsync(List<CombineUnitDTO> input)
        {
            ValidateException ex = new ValidateException();
            if (input != null && input.Any())
            {

                var CombineStatus = await DB.MasterCenters.Where(x => x.MasterCenterGroupKey.Equals(MasterCenterGroupKeys.CombineStatus)).ToListAsync();
                var CombineStatusWait = CombineStatus.Find(x => x.Key == MasterCombineStatusKeys.Wait);
                foreach (var item in input)
                {
                    if (item != null)
                    {
                        await item.ValidateAddAsync(DB);
                        var checkDup = input.Where(x => x.Unit.Id == item.Unit.Id || x.UnitCombine.Id == item.Unit.Id).ToList();
                        var checkComDup = input.Where(x => x.Unit.Id == item.UnitCombine.Id || x.UnitCombine.Id == item.UnitCombine.Id).ToList();
                        if (checkDup.Count > 1)
                        {
                            item.IsError = true;
                            item.ErrorMsg = $@"เลขที่แปลง {item.Unit.UnitNo} ซ้ำ";
                        }
                        if (checkComDup.Count > 1)
                        {
                            item.IsError = true;
                            item.ErrorMsg = $@"เลขที่แปลง {item.UnitCombine.UnitNo} ซ้ำ";
                        }

                        if (!(item.IsError ?? false))
                        {
                            var model = new CombineUnit
                            {
                                ProjectID = item.Project.Id,
                                UnitID = item.Unit.Id,
                                UnitIDCombine = item.UnitCombine.Id,
                                CombineDocTypeMasterCenterID = item.CombineDocType.Id,
                                CombineStatusMasterCenterID = CombineStatusWait.ID,
                            };
                            DB.CombineUnits.Add(model);
                            item.Id = model.ID;
                            var his = new CombineUnitHist
                            {

                                ProjectID = item.Project.Id,
                                UnitID = item.Unit.Id,
                                UnitIDCombine = item.UnitCombine.Id,
                                CombineUnitID = model.ID,
                                CombineStatusMasterCenterID = CombineStatusWait.ID,
                                CombineDocTypeMasterCenterID = item.CombineDocType.Id,
                                ProcessType = "Add"
                            };
                            DB.CombineUnitHists.Add(his);
                        }
                        else
                        {
                            ex.AddError("", item.ErrorMsg, 1);
                        }
                    }
                }
                await DB.SaveChangesAsync();
            }
            if (ex.HasError)
            {
                throw ex;
            }
            return input;
        }
        public async Task<List<CombineUnitDTO>> CreateAndApproveCombineUnitAsync(List<CombineUnitDTO> input)
        {
            ValidateException ex = new ValidateException();
            if (input != null && input.Any())
            {

                var CombineStatus = await DB.MasterCenters.Where(x => x.MasterCenterGroupKey.Equals(MasterCenterGroupKeys.CombineStatus)).ToListAsync();
                var CombineStatusWaitApprove = CombineStatus.Find(x => x.Key == MasterCombineStatusKeys.WaitApprove);
                var UnitCombinesAdd = new List<CombineUnit>();
                foreach (var item in input)
                {
                    if (item != null)
                    {
                        await item.ValidateAddAsync(DB);
                        var checkDup = input.Where(x => x.Unit.Id == item.Unit.Id || x.UnitCombine.Id == item.Unit.Id).ToList();
                        var checkComDup = input.Where(x => x.Unit.Id == item.UnitCombine.Id || x.UnitCombine.Id == item.UnitCombine.Id).ToList();
                        if (checkDup.Count > 1)
                        {
                            item.IsError = true;
                            item.ErrorMsg = $@"เลขที่แปลง {item.Unit.UnitNo} ซ้ำ";
                        }
                        if (checkComDup.Count > 1)
                        {
                            item.IsError = true;
                            item.ErrorMsg = $@"เลขที่แปลง {item.UnitCombine.UnitNo} ซ้ำ";
                        }

                        if (!(item.IsError ?? false))
                        {
                            var model = new CombineUnit
                            {
                                ProjectID = item.Project.Id,
                                UnitID = item.Unit.Id,
                                UnitIDCombine = item.UnitCombine.Id,
                                CombineDocTypeMasterCenterID = item.CombineDocType.Id,
                                CombineStatusMasterCenterID = CombineStatusWaitApprove.ID,
                            };
                            DB.CombineUnits.Add(model);
                            await DB.SaveChangesAsync();
                            item.Id = model.ID;
                            var his = new CombineUnitHist
                            {

                                ProjectID = item.Project.Id,
                                UnitID = item.Unit.Id,
                                UnitIDCombine = item.UnitCombine.Id,
                                CombineUnitID = model.ID,
                                CombineStatusMasterCenterID = CombineStatusWaitApprove.ID,
                                CombineDocTypeMasterCenterID = item.CombineDocType.Id,
                                ProcessType = "Add"
                            };
                            DB.CombineUnitHists.Add(his);
                            var mailLog = new CombineUnitMailLog
                            {
                                CombineUnitID = model.ID,
                                ProcessType = "Approve",
                                UnitID = model.UnitID,
                                UnitIDCombine = model.UnitIDCombine,
                                CombineDocTypeMasterCenterID = model.CombineDocTypeMasterCenterID,
                                Action = "Pending"
                            };
                            DB.CombineUnitMailLogs.Add(mailLog);
                            var Add = await DB.CombineUnits.Where(x => x.ID == model.ID).Include(x => x.Unit).Include(x => x.UnitCombine).Include(x => x.CombineDocType).FirstOrDefaultAsync();
                            UnitCombinesAdd.Add(Add);
                        }
                        else
                        {
                            ex.AddError("", item.ErrorMsg, 1);
                        }
                    }
                }
                await DB.SaveChangesAsync();
                if (UnitCombinesAdd.Any())
                {
                    var projectID = input.FirstOrDefault().Project.Id;

                    using DbCommand cmd = DB.Database.GetDbConnection().CreateCommand();
                    DynamicParameters ParamList = new DynamicParameters();
                    ParamList.Add("ProjectID", projectID);

                    CommandDefinition commandDefinition = new(
                                                 commandText: DBStoredNames.spGetVPUserByProject,
                                                 parameters: ParamList,
                                                 transaction: DB?.Database?.CurrentTransaction?.GetDbTransaction(),
                                                 commandType: CommandType.StoredProcedure);
                    var vp = await cmd.Connection.QueryFirstOrDefaultAsync<dbqVPUser>(commandDefinition) ?? new();
                    if (vp == null)
                    {
                        ex.AddError("", "ไม่พบ VP กรุณาตรวจสอบ", 1);
                        throw ex;
                    }
                    var MailHeader = $@"For Approval: ขออนุมัติการรวมแปลง/ห้อง (Combine Unit) โครงการ {vp.ProjectName} ({vp.ProjectNo})";
                    var MailContent = GenContentMailApprove(vp, UnitCombinesAdd);
                    string html = Common.Common.GetMailBodyTemplate();
                    var mailBody = html.Replace("{bodycontent}", MailContent);


                    Param_Mail2 mailParam = new Param_Mail2();
                    mailParam.Subject = MailHeader;
                    mailParam.Ref1 = Environment.GetEnvironmentVariable("email_mailType") ?? "";
                    mailParam.TO = vp.Email;
                    mailParam.CC = Environment.GetEnvironmentVariable("email_cc") ?? ""; ;
                    mailParam.SenderMail = Environment.GetEnvironmentVariable("email_mailFrom") ?? "";
                    string istest = Environment.GetEnvironmentVariable("email_isTest") ?? "true";
                    mailParam.SenderSystem = "CRM Sale";
                    mailParam.BCC = "";
                    mailParam.SenderName = "CRM Sale";
                    if (istest.ToLower() == "true" || istest.ToLower() == "t" || istest.ToLower() == "1")
                    {
                        mailBody += $"</br></br>MailTo={mailParam.TO}</br>MailCc={mailParam.CC}";
                        mailParam.TO = Environment.GetEnvironmentVariable("email_Test_mailTo") ?? "";
                        mailParam.Subject = mailParam.Subject.Replace("For Approval", "For Approval-Dev");
                        // mailParam.MailCC = MailAPICC;
                    }
                    mailParam.Content = mailBody;
                    if (!string.IsNullOrEmpty(mailParam.Subject))
                    {
                        var host = Environment.GetEnvironmentVariable("email_HostEmail");

                        var requestUrl = $"{host}";

                        using (var httpClient = new HttpClient())
                        {
                            httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
                            httpClient.DefaultRequestHeaders.Add("apikey", Environment.GetEnvironmentVariable("email_apikey"));
                            httpClient.DefaultRequestHeaders.Add("apitoken", Environment.GetEnvironmentVariable("email_apitoken"));

                            var stringContent = new StringContent(JsonConvert.SerializeObject(mailParam), System.Text.Encoding.UTF8, "application/json");
                            using (var response = await httpClient.PostAsync(requestUrl, stringContent))
                            {
                                if (response.StatusCode != System.Net.HttpStatusCode.OK)
                                {
                                    throw new Exception(response.RequestMessage?.ToString());
                                }
                            }
                        }
                    }
                }
            }
            if (ex.HasError)
            {
                throw ex;
            }
            return input;
        }
        public async Task<List<UnitDropdownDTO>> GetUnitDropdownCanCombineAsync(CombineUnitDDLDTO input, CancellationToken cancellationToken = default)
        {
            using DbCommand cmd = DB.Database.GetDbConnection().CreateCommand();
            DynamicParameters ParamList = new DynamicParameters();
            ParamList.Add("ProjectID", input.ProjectID);
            ParamList.Add("UnitNO", input.txt);

            CommandDefinition commandDefinition = new(
                                         commandText: DBStoredNames.spGetUnitCanCombine,
                                         parameters: ParamList,
                                         cancellationToken: cancellationToken,
                                         transaction: DB?.Database?.CurrentTransaction?.GetDbTransaction(),
                                         commandType: CommandType.StoredProcedure);
            var queryUnit = await cmd.Connection.QueryAsync<dbqUnitCanCombine>(commandDefinition) ?? new List<dbqUnitCanCombine>();
            var DataUnit = queryUnit.ToList();
            if (input.input != null && input.input.Any())
            {
                var ListUnitId = input.input.Select(x => x.Unit.Id).ToList();
                ListUnitId.AddRange(input.input.Select(x => x.UnitCombine.Id).ToList());
                DataUnit = DataUnit.Where(x => !ListUnitId.Contains(x.UnitID ?? new Guid())).ToList();
            }
            if (input.UnitID != null)
            {
                DataUnit = DataUnit.Where(x => x.UnitID != input.UnitID).ToList();
            }
            var result = DataUnit.Select(o => new UnitDropdownDTO
            {
                Id = o.UnitID ?? new Guid(),
                UnitNo = o.UnitNo,
                ProjectID = o.ProjectID
            }).ToList();
            return result;
        }
        public async Task<List<CombineUnitDTO>> SendApproveAsync(List<CombineUnitDTO> input)
        {
            ValidateException ex = new ValidateException();
            if (input != null && input.Any())
            {

                var CombineStatus = await DB.MasterCenters.Where(x => x.MasterCenterGroupKey.Equals(MasterCenterGroupKeys.CombineStatus)).ToListAsync();
                var CombineStatusWaitApprove = CombineStatus.Find(x => x.Key == MasterCombineStatusKeys.WaitApprove);
                var projectID = input.FirstOrDefault().Project.Id;

                using DbCommand cmd = DB.Database.GetDbConnection().CreateCommand();
                DynamicParameters ParamList = new DynamicParameters();
                ParamList.Add("ProjectID", projectID);

                CommandDefinition commandDefinition = new(
                                             commandText: DBStoredNames.spGetVPUserByProject,
                                             parameters: ParamList,
                                             transaction: DB?.Database?.CurrentTransaction?.GetDbTransaction(),
                                             commandType: CommandType.StoredProcedure);
                var vp = await cmd.Connection.QueryFirstOrDefaultAsync<dbqVPUser>(commandDefinition) ?? new();
                var CombineID = input.Select(x => x.Id).ToList();
                var UnitCombines = await DB.CombineUnits.Where(x => CombineID.Contains(x.ID)).Include(x => x.CombineStatus).Include(x => x.Unit).Include(x => x.UnitCombine).Include(x => x.CombineDocType).ToListAsync();

                var UnitCombinesAdd = UnitCombines.Where(x => x.CombineStatus.Key.Equals(MasterCombineStatusKeys.Wait)).ToList();

                var UnitCombinesResend = UnitCombines.Where(x => x.CombineStatus.Key.Equals(MasterCombineStatusKeys.WaitApprove)).ToList();
                var MailList = new List<Param_Mail2>();
                if (UnitCombinesAdd.Any())
                {
                    var MailHeader = $@"For Approval: ขออนุมัติการรวมแปลง/ห้อง (Combine Unit) โครงการ {vp.ProjectName} ({vp.ProjectNo})";
                    var MailContent = GenContentMailApprove(vp, UnitCombinesAdd);
                    string html = Common.Common.GetMailBodyTemplate();
                    var mailBody = html.Replace("{bodycontent}", MailContent);
                    foreach (var item in UnitCombinesAdd)
                    {
                        if (item != null)
                        {
                            item.CombineStatusMasterCenterID = CombineStatusWaitApprove.ID;
                            var his = new CombineUnitHist
                            {

                                ProjectID = item.ProjectID,
                                UnitID = item.UnitID,
                                UnitIDCombine = item.UnitIDCombine,
                                CombineUnitID = item.ID,
                                CombineStatusMasterCenterID = CombineStatusWaitApprove.ID,
                                CombineDocTypeMasterCenterID = item.CombineDocTypeMasterCenterID,
                                ApprovedDate = item.ApprovedDate,
                                ApprovedByUserID = item.ApprovedByUserID,
                                ReasonDel = item.ReasonDel,
                                ReasonEdit = item.ReasonEdit,
                                ProcessType = "Add"
                            };
                            DB.CombineUnitHists.Add(his);

                            var mailLog = new CombineUnitMailLog
                            {
                                CombineUnitID = item.ID,
                                ProcessType = "Approve",
                                UnitID = item.UnitID,
                                UnitIDCombine = item.UnitIDCombine,
                                CombineDocTypeMasterCenterID = item.CombineDocTypeMasterCenterID,
                                Action = "Pending"
                            };
                            DB.CombineUnitMailLogs.Add(mailLog);
                        }
                    }

                    Param_Mail2 mailParam = new Param_Mail2();
                    mailParam.Subject = MailHeader;
                    mailParam.Ref1 = Environment.GetEnvironmentVariable("email_mailType") ?? "";
                    mailParam.TO = vp.Email;
                    mailParam.CC = Environment.GetEnvironmentVariable("email_cc") ?? ""; ;
                    mailParam.SenderMail = Environment.GetEnvironmentVariable("email_mailFrom") ?? "";
                    mailParam.SenderSystem = "CRM Sale";
                    mailParam.BCC = "";
                    mailParam.SenderName = "CRM Sale";
                    string istest = Environment.GetEnvironmentVariable("email_isTest") ?? "true";
                    if (istest.ToLower() == "true" || istest.ToLower() == "t" || istest.ToLower() == "1")
                    {
                        mailBody += $"</br></br>MailTo={mailParam.TO}</br>MailCc={mailParam.CC}";
                        mailParam.TO = Environment.GetEnvironmentVariable("email_Test_mailTo") ?? "";
                        //mailParam.Topic = "[Test] - " + mailParam.Topic;
                        mailParam.Subject = mailParam.Subject.Replace("For Approval", "For Approval-Dev");
                        // mailParam.MailCC = MailAPICC;
                    }
                    mailParam.Content = mailBody;
                    if (!string.IsNullOrEmpty(mailParam.Subject))
                    {
                        MailList.Add(mailParam);
                    }
                    DB.CombineUnits.UpdateRange(UnitCombinesAdd);
                }
                #region edit
                //if (UnitCombinesEdit.Any())
                //{
                //    var MailHeader = $@"For Approval: ขออนุมัติการแก้ไขข้อมูลการรวมแปลง/ห้อง (Combine Unit) โครงการ {vp.ProjectName} ({vp.ProjectNo})";
                //    var MailContent = GenContentMailApproveEdit(vp, UnitCombinesEdit);
                //    string html = Common.Common.GetMailBodyTemplate();
                //    var mailBody = html.Replace("{bodycontent}", MailContent);
                //    foreach (var item in UnitCombinesEdit)
                //    {
                //        if (item != null)
                //        {
                //            item.CombineStatusMasterCenterID = CombineStatusWaitApprove.ID;
                //            var his = new CombineUnitHist
                //            {

                //                ProjectID = item.ProjectID,
                //                UnitID = item.UnitID,
                //                UnitIDCombine = item.UnitIDCombine,
                //                CombineUnitID = item.ID,
                //                CombineStatusMasterCenterID = CombineStatusWaitApprove.ID,
                //                CombineDocTypeMasterCenterID = item.CombineDocTypeMasterCenterID,
                //                ApprovedDate = item.ApprovedDate,
                //                ApprovedByUserID = item.ApprovedByUserID,
                //                ReasonDel = item.ReasonDel,
                //                ReasonEdit = item.ReasonEdit,
                //                ProcessType = "Update"
                //            };
                //            DB.CombineUnitHists.Add(his);

                //            var mailLog = new CombineUnitMailLog
                //            {
                //                CombineUnitID = item.ID,
                //                ProcessType = "ApproveEdit",
                //                UnitID = item.UnitID,
                //                UnitIDCombine = item.UnitIDCombine,
                //                CombineDocTypeMasterCenterID = item.CombineDocTypeMasterCenterID,
                //                Action = "Pending"
                //            };
                //            DB.CombineUnitMailLogs.Add(mailLog);
                //        }
                //    }

                //    Param_Mail mailParam = new Param_Mail();
                //    mailParam.Topic = MailHeader;
                //    mailParam.MailType = Environment.GetEnvironmentVariable("email_mailType") ?? "";
                //    mailParam.MailTo = vp.Email;
                //    mailParam.MailCC = Environment.GetEnvironmentVariable("email_cc") ?? ""; ;
                //    mailParam.MailFrom = Environment.GetEnvironmentVariable("email_mailFrom") ?? "";
                //    string istest = Environment.GetEnvironmentVariable("email_isTest") ?? "true";
                //    if (istest.ToLower() == "true" || istest.ToLower() == "t" || istest.ToLower() == "1")
                //    {
                //        mailBody += $"</br></br>MailTo={mailParam.MailTo}</br>MailCc={mailParam.MailCC}";
                //        mailParam.MailTo = Environment.GetEnvironmentVariable("email_Test_mailTo") ?? "";
                //        mailParam.Topic = "[Test] - " + mailParam.Topic;
                //        // mailParam.MailCC = MailAPICC;
                //    }
                //    mailParam.Detail = mailBody;
                //    if (!string.IsNullOrEmpty(mailParam.Topic))
                //    {
                //        MailList.Add(mailParam);
                //    }
                //    DB.CombineUnits.UpdateRange(UnitCombinesAdd);
                //}
                #endregion 
                if (UnitCombinesResend.Any())
                {
                    var combineApprove = new List<CombineUnit>();
                    var combineCancel = new List<CombineUnit>();
                    foreach (var item in UnitCombinesResend)
                    {
                        var LastHis = await DB.CombineUnitHists.Where(x => x.CombineUnitID == item.ID).OrderByDescending(x => x.Created).FirstOrDefaultAsync();
                        if (LastHis != null && LastHis.ProcessType.Equals("Delete"))
                        {
                            combineCancel.Add(item);
                        }
                        else
                        {
                            combineApprove.Add(item);
                        }
                    }
                    if (combineApprove.Any())
                    {
                        var MailHeader = $@"For Approval: ขออนุมัติการรวมแปลง/ห้อง (Combine Unit) โครงการ {vp.ProjectName} ({vp.ProjectNo}) (Resend)";
                        var MailContent = GenContentMailApprove(vp, combineApprove);
                        string html = Common.Common.GetMailBodyTemplate();
                        var mailBody = html.Replace("{bodycontent}", MailContent);
                        foreach (var item in combineApprove)
                        {
                            if (item != null)
                            {
                                var mailLog = new CombineUnitMailLog
                                {
                                    CombineUnitID = item.ID,
                                    ProcessType = "Approve",
                                    UnitID = item.UnitID,
                                    UnitIDCombine = item.UnitIDCombine,
                                    CombineDocTypeMasterCenterID = item.CombineDocTypeMasterCenterID,
                                    Action = "Pending"
                                };
                                DB.CombineUnitMailLogs.Add(mailLog);
                            }
                        }

                        Param_Mail2 mailParam = new Param_Mail2();
                        mailParam.Subject = MailHeader;
                        mailParam.Ref1 = Environment.GetEnvironmentVariable("email_mailType") ?? "";
                        mailParam.TO = vp.Email;
                        mailParam.CC = Environment.GetEnvironmentVariable("email_cc") ?? ""; ;
                        mailParam.SenderMail = Environment.GetEnvironmentVariable("email_mailFrom") ?? "";
                        mailParam.BCC = "";
                        mailParam.SenderSystem = "CRM Sale";
                        mailParam.SenderName = "CRM Sale";
                        string istest = Environment.GetEnvironmentVariable("email_isTest") ?? "true";
                        if (istest.ToLower() == "true" || istest.ToLower() == "t" || istest.ToLower() == "1")
                        {
                            mailBody += $"</br></br>MailTo={mailParam.TO}</br>MailCc={mailParam.CC}";
                            mailParam.TO = Environment.GetEnvironmentVariable("email_Test_mailTo") ?? "";
                            //mailParam.Topic = "[Test] - " + mailParam.Topic;
                            mailParam.Subject = mailParam.Subject.Replace("For Approval", "For Approval-Dev");
                            // mailParam.MailCC = MailAPICC;
                        }
                        mailParam.Content = mailBody;
                        if (!string.IsNullOrEmpty(mailParam.Subject))
                        {
                            MailList.Add(mailParam);
                        }
                    }
                    if (combineCancel.Any())
                    {
                        var MailHeader = $@"For Approval: ขออนุมัติยกเลิกข้อมูลการรวมแปลง/ห้อง (Combine Unit) โครงการ {vp.ProjectName} ({vp.ProjectNo}) (Resend)";
                        //var MailContent = GenContentMailApprove(vp, UnitCombinesAdd);
                        var MailContent = GenContentMailApproveDelete(vp, combineCancel);
                        string html = Common.Common.GetMailBodyTemplate();
                        var mailBody = html.Replace("{bodycontent}", MailContent);
                        foreach (var item in combineCancel)
                        {
                            if (item != null)
                            {
                                var mailLog = new CombineUnitMailLog
                                {
                                    CombineUnitID = item.ID,
                                    ProcessType = "Delete",
                                    UnitID = item.UnitID,
                                    UnitIDCombine = item.UnitIDCombine,
                                    CombineDocTypeMasterCenterID = item.CombineDocTypeMasterCenterID,
                                    ReasonDel = item.ReasonDel,
                                    ReasonEdit = item.ReasonEdit,
                                    Action = "Pending"
                                };
                                DB.CombineUnitMailLogs.Add(mailLog);
                            }
                        }

                        Param_Mail2 mailParam = new Param_Mail2();
                        mailParam.Subject = MailHeader;
                        mailParam.Ref1 = Environment.GetEnvironmentVariable("email_mailType") ?? "";
                        mailParam.TO = vp.Email;
                        mailParam.CC = Environment.GetEnvironmentVariable("email_cc") ?? ""; ;
                        mailParam.SenderMail = Environment.GetEnvironmentVariable("email_mailFrom") ?? "";
                        mailParam.SenderSystem = "CRM Sale";
                        mailParam.SenderName   = "CRM Sale";
                        mailParam.BCC = "";
                        string istest = Environment.GetEnvironmentVariable("email_isTest") ?? "true";
                        if (istest.ToLower() == "true" || istest.ToLower() == "t" || istest.ToLower() == "1")
                        {
                            mailBody += $"</br></br>MailTo={mailParam.TO}</br>MailCc={mailParam.CC}";
                            mailParam.TO = Environment.GetEnvironmentVariable("email_Test_mailTo") ?? "";
                            //mailParam.Topic = "[Test] - " + mailParam.Topic;
                            mailParam.Subject = mailParam.Subject.Replace("For Approval", "For Approval-Dev");
                            // mailParam.MailCC = MailAPICC;
                        }
                        mailParam.Content = mailBody;
                        if (!string.IsNullOrEmpty(mailParam.Subject))
                        {
                            MailList.Add(mailParam);
                        }
                    }
                }
                #region send mail
                if (MailList.Any())
                {
                    var host = Environment.GetEnvironmentVariable("email_HostEmail");

                    var requestUrl = $"{host}";

                    using (var httpClient = new HttpClient())
                    {
                        httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
                        httpClient.DefaultRequestHeaders.Add("apikey", Environment.GetEnvironmentVariable("email_apikey"));
                        httpClient.DefaultRequestHeaders.Add("apitoken", Environment.GetEnvironmentVariable("email_apitoken")); 
                        foreach (var mail in MailList)
                        {
                            var stringContent = new StringContent(JsonConvert.SerializeObject(mail), System.Text.Encoding.UTF8, "application/json");
                            using (var response = await httpClient.PostAsync(requestUrl, stringContent))
                            {
                                if (response.StatusCode != System.Net.HttpStatusCode.OK)
                                {
                                    throw new Exception(response.RequestMessage?.ToString());
                                }
                            }
                        } 
                    }
                }
                #endregion

                await DB.SaveChangesAsync();
            }
            if (ex.HasError)
            {
                throw ex;
            }
            return input;
        }
        public async Task<CombineUnitDTO> ApproveAsync(CombineUnitDTO input)
        {
            var ApproveAction = input.ActionApprove;
            if (input != null)
            {
                var combine = await DB.CombineUnits.FirstOrDefaultAsync(x => x.ID == input.Id);
                if (combine == null)
                {
                    ValidateException ex = new ValidateException();
                    ex.AddError("", "ไม่พบรายการ", 1);
                    throw ex;
                }
                // ApproveAction
                // Approve = Approve state Add
                // ApproveEdit = Approve state Edit
                // ApproveDelete = Approve state Delete
                // Reject = Reject state Add
                // RejectEdit = Reject state Edit
                // RejectDelete = Reject state Delete

                var CombineStatus = await DB.MasterCenters.Where(x => x.MasterCenterGroupKey.Equals(MasterCenterGroupKeys.CombineStatus)).ToListAsync();
                var CombineStatusWaitApprove = CombineStatus.Find(x => x.Key == MasterCombineStatusKeys.WaitApprove);
                var CombineStatusApprove = CombineStatus.Find(x => x.Key == MasterCombineStatusKeys.Approve);
                var CombineStatusReject = CombineStatus.Find(x => x.Key == MasterCombineStatusKeys.Reject);

                var StatusUpdate = ApproveAction.StartsWith("approve") ? CombineStatusApprove : CombineStatusReject;
                if (combine.CombineStatusMasterCenterID != CombineStatusWaitApprove?.ID)
                {
                    ValidateException ex = new ValidateException();
                    ex.AddError("", "รายการนี้ ดำเนินการแล้ว", 1);
                    throw ex;
                }
                combine.ApprovedDate = DateTime.Now;
                combine.ApprovedByUserID = input.ApprovedByID;
                var ProcessType = "";
                if (ApproveAction.Equals("approve"))
                {
                    combine.CombineStatusMasterCenterID = StatusUpdate?.ID;
                    ProcessType = "Add";
                }
                else if (ApproveAction.Equals("reject"))
                {
                    combine.CombineStatusMasterCenterID = StatusUpdate?.ID;
                    ProcessType = "Add";
                }
                else if (ApproveAction.Equals("approveedit"))
                {
                    combine.CombineStatusMasterCenterID = StatusUpdate?.ID;
                }
                else if (ApproveAction.Equals("rejectedit"))
                {
                    combine.CombineStatusMasterCenterID = StatusUpdate?.ID;
                }
                else if (ApproveAction.Equals("approvedelete"))
                {
                    combine.CombineStatusMasterCenterID = StatusUpdate?.ID;
                    combine.IsDeleted = true;
                    ProcessType = "Delete";
                }
                else if (ApproveAction.Equals("rejectdelete"))
                {
                    combine.CombineStatusMasterCenterID = CombineStatusApprove?.ID;
                    ProcessType = "Delete";
                }
                var his = new CombineUnitHist
                {
                    ProjectID = combine.ProjectID,
                    UnitID = combine.UnitID,
                    UnitIDCombine = combine.UnitIDCombine,
                    CombineUnitID = combine.ID,
                    CombineStatusMasterCenterID = combine.CombineStatusMasterCenterID,
                    CombineDocTypeMasterCenterID = combine.CombineDocTypeMasterCenterID,
                    ApprovedDate = combine.ApprovedDate,
                    ApprovedByUserID = combine.ApprovedByUserID,
                    ReasonDel = combine.ReasonDel,
                    ReasonEdit = combine.ReasonEdit,
                    ProcessType = ProcessType
                };
                //LastMailLog
                var mailLog = await DB.CombineUnitMailLogs.Where(x => x.CombineUnitID == combine.ID).OrderByDescending(x => x.Created).FirstOrDefaultAsync();
                if (mailLog != null)
                {
                    mailLog.Action = ApproveAction.StartsWith("approve") ? "Approve" : "Reject";
                    DB.CombineUnitMailLogs.Update(mailLog);
                }

                DB.CombineUnitHists.Add(his);
                DB.CombineUnits.Update(combine);
                await DB.SaveChangesAsync(input.ApprovedByID ?? new Guid());
            }
            input.ApproveResultMsg = ApproveAction.StartsWith("approve") ? "Approve Success" : "Reject Success";
            return input;
        }
        public async Task<CombineUnitDTO> EditCombineAsync(CombineUnitDTO input)
        {
            if (input != null)
            {
                var combine = await DB.CombineUnits.Where(x => x.ID == input.Id).FirstOrDefaultAsync();
                if (combine == null)
                {
                    ValidateException ex = new ValidateException();
                    ex.AddError("", "ไม่พบรายการ", 1);
                    throw ex;
                }
                var CombineStatus = await DB.MasterCenters.Where(x => x.MasterCenterGroupKey.Equals(MasterCenterGroupKeys.CombineStatus)).ToListAsync();

                combine.UnitID = input.Unit.Id;
                combine.UnitIDCombine = input.UnitCombine.Id;
                combine.CombineDocTypeMasterCenterID = input.CombineDocType.Id;

                var his = new CombineUnitHist
                {
                    ProjectID = combine.ProjectID,
                    UnitID = combine.UnitID,
                    UnitIDCombine = combine.UnitIDCombine,
                    CombineUnitID = combine.ID,
                    CombineStatusMasterCenterID = combine.CombineStatusMasterCenterID,
                    CombineDocTypeMasterCenterID = combine.CombineDocTypeMasterCenterID,
                    ApprovedDate = combine.ApprovedDate,
                    ApprovedByUserID = combine.ApprovedByUserID,
                    ReasonDel = combine.ReasonDel,
                    ReasonEdit = combine.ReasonEdit,
                    ProcessType = "Update"
                };
                DB.CombineUnitHists.Add(his);
                DB.CombineUnits.Update(combine);
                await DB.SaveChangesAsync();
            }
            return input;
        }
        public async Task<CombineUnitDTO> DeleteCombineAsync(CombineUnitDTO input)
        {
            if (input != null)
            {
                var combine = await DB.CombineUnits.Where(x => x.ID == input.Id).Include(x => x.CombineStatus).Include(x => x.Unit).Include(x => x.UnitCombine).Include(x => x.CombineDocType).FirstOrDefaultAsync();
                if (combine == null)
                {
                    ValidateException ex = new ValidateException();
                    ex.AddError("", "ไม่พบรายการ", 1);
                    throw ex;
                }
                var CombineStatus = await DB.MasterCenters.Where(x => x.MasterCenterGroupKey.Equals(MasterCenterGroupKeys.CombineStatus)).ToListAsync();
                var CombineStatusWaitApprove = CombineStatus.Find(x => x.Key == MasterCombineStatusKeys.WaitApprove);
                if (combine.CombineStatusMasterCenterID == CombineStatusWaitApprove?.ID)
                {
                    ValidateException ex = new ValidateException();
                    ex.AddError("", "สถานะรายการไม่ถูกต้อง", 1);
                    throw ex;
                }
                if (combine.ApprovedDate == null)
                {
                    combine.IsDeleted = true;
                    combine.ReasonDel = input.ReasonDel;

                    var his = new CombineUnitHist
                    {
                        ProjectID = combine.ProjectID,
                        UnitID = combine.UnitID,
                        UnitIDCombine = combine.UnitIDCombine,
                        CombineUnitID = combine.ID,
                        CombineStatusMasterCenterID = combine.CombineStatusMasterCenterID,
                        CombineDocTypeMasterCenterID = combine.CombineDocTypeMasterCenterID,
                        ApprovedDate = combine.ApprovedDate,
                        ApprovedByUserID = combine.ApprovedByUserID,
                        ReasonDel = combine.ReasonDel,
                        ReasonEdit = combine.ReasonEdit,
                        ProcessType = "Delete"
                    };
                    var mailLog = new CombineUnitMailLog
                    {
                        CombineUnitID = combine.ID,
                        ProcessType = "Delete",
                        UnitID = combine.UnitID,
                        UnitIDCombine = combine.UnitIDCombine,
                        CombineDocTypeMasterCenterID = combine.CombineDocTypeMasterCenterID,
                        ReasonDel = combine.ReasonDel,
                        ReasonEdit = combine.ReasonEdit,
                        Action = "Pending"
                    };
                    DB.CombineUnitHists.Add(his);
                    DB.CombineUnitMailLogs.Add(mailLog);
                    DB.CombineUnits.Update(combine);
                    await DB.SaveChangesAsync();
                }
                else
                {

                    combine.CombineStatusMasterCenterID = CombineStatusWaitApprove.ID;
                    combine.ApprovedDate = null;
                    combine.ApprovedByUserID = null;
                    combine.ReasonDel = input.ReasonDel;

                    var his = new CombineUnitHist
                    {
                        ProjectID = combine.ProjectID,
                        UnitID = combine.UnitID,
                        UnitIDCombine = combine.UnitIDCombine,
                        CombineUnitID = combine.ID,
                        CombineStatusMasterCenterID = combine.CombineStatusMasterCenterID,
                        CombineDocTypeMasterCenterID = combine.CombineDocTypeMasterCenterID,
                        ApprovedDate = combine.ApprovedDate,
                        ApprovedByUserID = combine.ApprovedByUserID,
                        ReasonDel = combine.ReasonDel,
                        ReasonEdit = combine.ReasonEdit,
                        ProcessType = "Delete"
                    };
                    DB.CombineUnitHists.Add(his);
                    DB.CombineUnits.Update(combine);
                    await DB.SaveChangesAsync();
                    // Send Mail
                    var projectID = combine.ProjectID;
#if !DEBUG

                    using DbCommand cmd = DB.Database.GetDbConnection().CreateCommand();
                    DynamicParameters ParamList = new DynamicParameters();
                    ParamList.Add("ProjectID", projectID);
                    CommandDefinition commandDefinition = new(
                                                 commandText: DBStoredNames.spGetVPUserByProject,
                                                 parameters: ParamList,
                                                 transaction: DB?.Database?.CurrentTransaction?.GetDbTransaction(),
                                                 commandType: CommandType.StoredProcedure);
                    var vp = await cmd.Connection.QueryFirstOrDefaultAsync<dbqVPUser>(commandDefinition) ?? new();
                    if (vp == null)
                    {
                        ValidateException ex = new ValidateException();
                        ex.AddError("", "ไม่พบ VP กรุณาตรวจสอบ", 1);
                        throw ex;
                    }
                    var MailHeader = $@"For Approval: ขออนุมัติยกเลิกข้อมูลการรวมแปลง/ห้อง (Combine Unit) โครงการ {vp.ProjectName} ({vp.ProjectNo})";
                    var MailContent = GenContentMailApproveDelete(vp, new List<CombineUnit> { combine });

                    string html = Common.Common.GetMailBodyTemplate();
                    var mailBody = html.Replace("{bodycontent}", MailContent);
                    Param_Mail mailParam = new Param_Mail();
                    mailParam.Topic = MailHeader;
                    mailParam.MailType = Environment.GetEnvironmentVariable("email_mailType") ?? "";
                    mailParam.MailTo = vp.Email;
                    mailParam.MailCC = Environment.GetEnvironmentVariable("email_cc") ?? ""; ;
                    mailParam.MailFrom = Environment.GetEnvironmentVariable("email_mailFrom") ?? "";
                    string istest = Environment.GetEnvironmentVariable("email_isTest") ?? "true";
                    if (istest.ToLower() == "true" || istest.ToLower() == "t" || istest.ToLower() == "1")
                    {
                        mailBody += $"</br></br>MailTo={mailParam.MailTo}</br>MailCc={mailParam.MailCC}";
                        mailParam.MailTo = Environment.GetEnvironmentVariable("email_Test_mailTo") ?? "";
                        mailParam.Topic = mailParam.Topic.Replace("For Approval", "For Approval-Dev");
                        // mailParam.MailCC = MailAPICC;
                    }
                    mailParam.Detail = mailBody;
                    if (!string.IsNullOrEmpty(mailParam.Topic))
                    {
                        var host = Environment.GetEnvironmentVariable("email_HostEmail");

                        var requestUrl = $"{host}";

                        using (var httpClient = new HttpClient())
                        {
                            var stringContent = new StringContent(JsonConvert.SerializeObject(new List<Param_Mail> { mailParam }), System.Text.Encoding.UTF8, "application/json");
                            using (var response = await httpClient.PostAsync(requestUrl, stringContent))
                            {
                                if (response.StatusCode != System.Net.HttpStatusCode.OK)
                                {
                                    throw new Exception(response.RequestMessage?.ToString());
                                }
                            }
                        }
                    }
#endif
                }

            }
            return input;
        }
        public async Task<List<ProjectDropdownDTO>> GetProjectDropdownListAsync(string name, Guid? companyID, bool isActive, string projectStatusKey, Guid? UserID = null, CancellationToken cancellationToken = default)
        {
            IQueryable<Database.Models.PRJ.Project> query = null;

            if (UserID.HasValue)
            {
                query = DB.UserAuthorizeProjects
                        .Include(o => o.Project)
                        .Where(o => o.Project.IsActive == isActive && o.UserID == UserID).Select(p => p.Project);
            }
            else
            {
                query = DB.Projects;
            }

            query = query.Include(o => o.ProjectStatus)
                            .Include(o => o.ProductType)
                            .Include(o => o.BG)
                            .Include(o => o.SubBG)
                            .Where(o => o.IsActive == isActive && o.SubBG.SubBGNo.Substring(2, 1) != "0"
                            && o.ProductType.Key.Equals(ProductTypeKeys.HighRise));

            if (!string.IsNullOrEmpty(name))
            {
                name = name.Replace("-", "");
                name = name.ToLower();
                query = query.Where(o => ((o.ProjectNo ?? "") + (o.ProjectNameTH ?? "")).ToLower().Contains(name));
            }

            if (companyID != null && companyID != Guid.Empty)
                query = query.Where(o => o.CompanyID == companyID);

            if (!string.IsNullOrEmpty(projectStatusKey))
            {
                var projectStatusMasterCenterID = await DB.MasterCenters.Where(o => o.Key == projectStatusKey
                                                                      && o.MasterCenterGroupKey == MasterCenterGroupKeys.ProjectStatus)
                                                                     .Select(o => o.ID).FirstAsync();
                query = query.Where(o => o.ProjectStatusMasterCenterID == projectStatusMasterCenterID);
            }

            var queryResults = await query.OrderBy(o => o.ProjectNo).ThenBy(o => o.ProjectNameTH).OrderBy(o => o.ProjectNo).ToListAsync(cancellationToken);

            var results = queryResults.Select(o => ProjectDropdownDTO.CreateFromModel(o)).ToList();

            results = results.Distinct().ToList();

            return results;
        }
        #region Mail Content
        private string GenContentMailApprove(dbqVPUser vp, List<CombineUnit> UnitCombines)
        {
            var MailContent = $@"&nbsp; เรียน คุณ {vp.FirstName} {vp.LastName} <br />
                                 &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; โครงการ {vp.ProjectName} ({vp.ProjectNo}) มีการขออนุมัติการรวมแปลง/ห้อง <br />
                                &nbsp;&nbsp;&nbsp;    มีรายการที่ท่านต้องพิจารณา จำนวน {UnitCombines.Count()} รายการ ดังนี้ <br /><br />
                                    ";
            MailContent += $@"&nbsp;&nbsp;&nbsp;<table style='border-collapse: collapse; ' border='1'>
<tbody>
<tr style='background-color:#8992a7;color:#FFFFFF ;font-weight: bold;text-align: center;'>
<td class='tableClass' >ลำดับ</td>
<td class='tableClass' >เลขที่แปลง</td>
<td class='tableClass' >รวมกับเลขที่แปลง</td>
<td class='tableClass' >แบบบันทึก</td>
<td class='tableClass' >Action</td>
</tr> ";
            int i = 1;
            var crmMasterDataUrl = $"{Environment.GetEnvironmentVariable("APMailAPI_masterdataurl")}";
            foreach (var unit in UnitCombines)
            {
                var ApproveUrl = $@"{crmMasterDataUrl}/combineunit/approve;user={vp.CRMID};combineid={unit.ID};action=approve";
                var RejectUrl = $@"{crmMasterDataUrl}/combineunit/approve;user={vp.CRMID};combineid={unit.ID};action=reject";

                MailContent += $@"
<tr'>
<td class='tableClass' style='text-align: center;' >&nbsp; {i++} &nbsp;</td>
<td class='tableClass' style='text-align: center;' >&nbsp; {unit.Unit.UnitNo} &nbsp;</td>
<td class='tableClass' style='text-align: center;' >&nbsp; {unit.UnitCombine.UnitNo} &nbsp;</td>
<td class='tableClass' >&nbsp; {unit.CombineDocType.Name} &nbsp;</td>
<td class='tableClass' > 
    &nbsp;<a href='{ApproveUrl}' target='_blank'>Approve</a>
    &nbsp;
    <a href='{RejectUrl}' target='_blank'>Reject</a>
    &nbsp;
</td>
</tr> ";
                ;
            }
            MailContent += $@"</tbody>
                            </table>
                            <br/><br /><br />";
            return MailContent;
        }
        private string GenContentMailApproveEdit(dbqVPUser vp, List<CombineUnit> UnitCombines)
        {
            var MailContent = $@"&nbsp; เรียน คุณ{vp.FirstName} {vp.LastName} <br />
                                 &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; โครงการ {vp.ProjectName} ({vp.ProjectNo}) มีการขออนุมัติการแก้ไขข้อมูลการรวมแปลง/ห้อง <br />
                                &nbsp;&nbsp;&nbsp;    มีรายการที่ท่านต้องพิจารณา จำนวน {UnitCombines.Count()} รายการ ดังนี้ <br /><br />
                                    ";
            MailContent += $@"&nbsp;&nbsp;&nbsp;<table style='border-collapse: collapse; ' border='1'>
<tbody>
<tr style='background-color:#8992a7;color:#FFFFFF ;font-weight: bold;text-align: center;'>
<td class='tableClass' >ลำดับ</td>
<td class='tableClass' >เลขที่แปลง</td>
<td class='tableClass' >รวมกับแปลงเลขที่</td>
<td class='tableClass' >แบบบันทึก</td>
<td class='tableClass' >การแก้ไข</td>
<td class='tableClass' >Action</td>
</tr> ";
            int i = 1;
            var crmMasterDataUrl = $"{Environment.GetEnvironmentVariable("APMailAPI_masterdataurl")}";
            foreach (var unit in UnitCombines)
            {
                var ApproveUrl = $@"{crmMasterDataUrl}/combineunit/approve;user={vp.CRMID};combineid={unit.ID};action=approveedit";
                var RejectUrl = $@"{crmMasterDataUrl}/combineunit/approve;user={vp.CRMID};combineid={unit.ID};action=rejectedit";

                MailContent += $@"
<tr'>
<td class='tableClass' style='text-align: center;' >&nbsp; {i++} &nbsp;</td>
<td class='tableClass' style='text-align: center;' >&nbsp; {unit.Unit.UnitNo} &nbsp;</td>
<td class='tableClass' style='text-align: center;' >&nbsp; {unit.UnitCombine.UnitNo} &nbsp;</td>
<td class='tableClass' >&nbsp; {unit.CombineDocType.Name} &nbsp;</td>
<td class='tableClass' >&nbsp; {unit.ReasonEdit} &nbsp;</td>
<td class='tableClass' > 
    &nbsp;<a href='{ApproveUrl}' target='_blank'>Approve</a>
    &nbsp;
    <a href='{RejectUrl}' target='_blank'>Reject</a>
    &nbsp;
</td>
</tr> ";
                ;
            }
            MailContent += $@"</tbody>
                            </table>
                            <br/><br /><br />";
            return MailContent;
        }
        private string GenContentMailApproveDelete(dbqVPUser vp, List<CombineUnit> UnitCombines)
        {
            var MailContent = $@"&nbsp; เรียน คุณ {vp.FirstName} {vp.LastName} <br />
                                 &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; โครงการ {vp.ProjectName} ({vp.ProjectNo}) มีการขออนุมัติ <span style=' text-decoration: underline; color: #ff0000; font-weight: bold;' >ยกเลิก</span> ข้อมูลการรวมแปลง/ห้อง <br />
                                &nbsp;&nbsp;&nbsp;    มีรายการที่ท่านต้องพิจารณา จำนวน {UnitCombines.Count()} รายการ ดังนี้ <br /><br />
                                    ";
            var crmMasterDataUrl = $"{Environment.GetEnvironmentVariable("APMailAPI_masterdataurl")}";
            MailContent += $@"&nbsp;&nbsp;&nbsp;<table style='border-collapse: collapse; ' border='1'>
                                <tbody>
                                <tr style='background-color:#8992a7;color:#FFFFFF ;font-weight: bold;text-align: center;'>
                                <td class='tableClass' >ลำดับ</td>
                                <td class='tableClass' >เลขที่แปลง</td>
                                <td class='tableClass' >รวมกับเลขที่แปลง</td>
                                <td class='tableClass' >แบบบันทึก</td>
                                <td class='tableClass' >เหตุผลการยกเลิก</td>
                                <td class='tableClass' >Action</td>
                                </tr> ";
            int i = 1;
            foreach (var unit in UnitCombines)
            {
                var ApproveUrl = $@"{crmMasterDataUrl}/combineunit/approve;user={vp.CRMID};combineid={unit.ID};action=approvedelete";
                var RejectUrl = $@"{crmMasterDataUrl}/combineunit/approve;user={vp.CRMID};combineid={unit.ID};action=rejectdelete";
                MailContent += $@"
<tr'>
<td class='tableClass' style='text-align: center;' >&nbsp; {i++} &nbsp;</td>
<td class='tableClass' style='text-align: center;' >&nbsp; {unit.Unit.UnitNo} &nbsp;</td>
<td class='tableClass' style='text-align: center;' >&nbsp; {unit.UnitCombine.UnitNo} &nbsp;</td>
<td class='tableClass' >&nbsp; {unit.CombineDocType.Name} &nbsp;</td>
<td class='tableClass' >&nbsp; {unit.ReasonDel} &nbsp;</td>
<td class='tableClass' > 
    &nbsp;<a href='{ApproveUrl}' target='_blank'>Approve</a>
    &nbsp;
    <a href='{RejectUrl}' target='_blank'>Reject</a>
    &nbsp;
</td>
</tr> ";
                ;
            }
            MailContent += $@"</tbody>
                            </table>
                            <br/><br /><br />";
            return MailContent;
        }
        #endregion
    }
}
