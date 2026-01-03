using Base.DTOs.MST;
using Database.Models;
using Database.Models.BI;
using Database.Models.MST;
using ErrorHandling;
using MST_General.Params.Filters;
using MST_General.Params.Outputs;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using PagingExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using static Base.DTOs.ParamMail;
using Common.Helper.Logging;

namespace MST_General.Services
{
    public class AgentsService : IAgentsService
    {
        public LogModel logModel { get; set; }
        private readonly DatabaseContext DB;

        public AgentsService(DatabaseContext db)
        {
            logModel = new LogModel("AgentsService", null);
            DB = db;
        }
        public async Task<List<AgentDropdownDTO>> GetAgentDropdownListAsync(string name, CancellationToken cancellationToken = default)
        {
            IQueryable<Agent> query = DB.Agents.AsNoTracking();

            #region Filter
            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(o => o.NameTH.Contains(name));
            }
            #endregion
            var results = await query.OrderBy(o => o.NameTH).Take(100)
                        .Select(o => AgentDropdownDTO.CreateFromModel(o)).ToListAsync(cancellationToken);

            return results;
        }

        public async Task<AgentPaging> GetAgentListAsync(AgentFilter request, PageParam pageParam, AgentSortByParam sortByParam, CancellationToken cancellationToken = default)
        {
            IQueryable<AgentQueryResult> query = DB.Agents.AsNoTracking().Select(o => new AgentQueryResult
            {
                Agent = o,
                SubDistrict = o.SubDistrict,
                District = o.District,
                Province = o.Province,
                UpdatedBy = o.UpdatedBy
            });

            #region filter
            if (!string.IsNullOrEmpty(request.NameTH))
            {
                query = query.Where(x => x.Agent.NameTH.Contains(request.NameTH));
            }
            if (!string.IsNullOrEmpty(request.NameEN))
            {
                query = query.Where(x => x.Agent.NameEN.Contains(request.NameEN));
            }
            if (!string.IsNullOrEmpty(request.UpdatedBy))
            {
                query = query.Where(x => x.UpdatedBy.DisplayName.Contains(request.UpdatedBy));
            }
            if (request.UpdatedFrom != null)
            {
                query = query.Where(x => x.Agent.Updated >= request.UpdatedFrom);
            }
            if (request.UpdatedTo != null)
            {
                query = query.Where(x => x.Agent.Updated <= request.UpdatedTo);
            }
            if (request.UpdatedFrom != null && request.UpdatedTo != null)
            {
                query = query.Where(x => x.Agent.Updated >= request.UpdatedFrom && x.Agent.Updated <= request.UpdatedTo);
            }
            #endregion

            AgentDTO.SortBy(sortByParam, ref query);

            var pageOutput = PagingHelper.Paging<AgentQueryResult>(pageParam, ref query);

            var results = await query
            .Select(o => AgentDTO.CreateFromQueryResult(o)).ToListAsync(cancellationToken);

            return new AgentPaging()
            {
                Agents = results,
                PageOutput = pageOutput
            };
        }

        public async Task<AgentDTO> GetAgentAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var model = await DB.Agents.AsNoTracking()
                                       .Include(o => o.Province)
                                       .Include(o => o.District)
                                       .Include(o => o.SubDistrict)
                                       .Include(o => o.UpdatedBy)
                                       .FirstOrDefaultAsync(o => o.ID == id, cancellationToken);
            var result = AgentDTO.CreateFromModel(model);
            return result;
        }

        public async Task<AgentDTO> CreateAgentAsync(AgentDTO input)
        {
            await input.ValidateAsync(DB);

            Agent model = new Agent();
            input.ToModel(ref model);

            await DB.Agents.AddAsync(model);
            await DB.SaveChangesAsync();

#if !DEBUG

            // ส่งเมลแจ้ง Commission Setting Agent Rate
            #region Sent Mail
            var isTest = bool.Parse($"{Environment.GetEnvironmentVariable("email_isTest")}");
            var test_MailTo = $"{Environment.GetEnvironmentVariable("email_Test_mailTo")}";
            var mailFrom = $"{Environment.GetEnvironmentVariable("email_mailFrom")}";

            string Topic = "For Action: มีการเพิ่ม Agent ใหม่ กรุณากำหนดค่า Commission ให้กับ Agent";
            string Content = @"<div style=""font-family:Tahoma; font-size:12px;"">
                                เรียน คุณ [HRName]
                                <BR />
                                <BR />
                                ได้มีการเพิ่ม Agent ใหม่ กรุณาระบุ Commission Setting 
                                <BR />
                                <B>ชื่อ Agent : </B> [AgentNameTH]                          
                                <BR />
                                <B>วัน-เวลาที่สร้าง : </B> [CreateDate]                          
                                <BR />
                                <BR />
                                <HR>
                                <BR />
                                ส่งโดย CRM Application System
                                </div>
                                <div>
                                <table border='0' cellspacing='0' cellpadding='0' style='font-family:AP;font-size:12px'>
                                  <tr>
                                    <td colspan='3' valign='top'></td>
                                  </tr>
                                  <tr>
                                    <td width='140' valign='top'></td>
                                    <td colspan='2' valign='top'></td>
                                  </tr>
                                  <tr>
                                    <td width='140' rowspan='5' valign='top'><p><img width='100' height='120' src='https://happyrefund.apthai.com/datashare/maillogo/aplogo.png' border='0' /></p>      <div align='right'></div>      <div align='right'></div>      <div align='right'></div>      <div align='right'></div>      <div align='right'></div></td>
                                    <td colspan='2' valign='top'><p><strong>AP (Thailand)&nbsp; Public Company Limited</strong> <br />
                                      170/57 18th Fl. Ocean Tower 1 , Ratchadapisek Rd. <br />
                                      Khongtoey Bangkok 10110</p></td>
                                  </tr>
                                  <tr>
                                    <td valign='top'><div align='left'>Sale Consult</div></td>
                                    <td valign='top'>: &nbsp;02-261-2518 Ext.649, 477, 648, 647</td>
                                  </tr>
                                  <tr>
                                    <td valign='top'><div align='left'>Account/FI Consult</div></td>
                                    <td valign='top'>: &nbsp;02-261-2518 Ext.373, 647</td>
                                    </tr>
                                  <tr>
                                    <td width='127' valign='top'><div align='left'>Email</div></td>
                                    <td width='266' valign='top'>: &nbsp;<a href='mailto:crmsale@apthai.com'>crmsale@apthai.com</a></td>
                                  </tr>
                                </table>
                                </div>";

            var roleHCPMID = await DB.Roles.Where(x => x.Code.Equals("HCPM")).Select(x => x.ID).FirstOrDefaultAsync();
            var emailList = await DB.UserRoles.Include(x => x.User).Where(x => x.RoleID == roleHCPMID).Select(x => x.User.Email).ToListAsync();
            var nameList = await DB.UserRoles.Include(x => x.User).Where(x => x.RoleID == roleHCPMID).Select(x => x.User.DisplayName).ToListAsync();
            string emailHR = String.Join(";", emailList);
            string nameHR = String.Join(", ", nameList);

            Content = Content.Replace("[HRName]", nameHR);
            Content = Content.Replace("[AgentNameTH]", model.NameTH);
            Content = Content.Replace("[CreateDate]", string.Format("{0: d/MM/yyyy HH:mm:ss}", model.Created));

            var mail = new Param_Mail
            {
                MailFrom = mailFrom,
                MailTo = (isTest) ? test_MailTo : emailHR,
                MailBCC = test_MailTo,
                Topic = ((isTest) ? "[DEV] " : "") + Topic,
                Detail = Content,
                MailType = "CreateAgent",
                Key1 = model.ID.ToString(),
                Key2 = model.NameTH,
                Key3 = "",
                Key4 = "",
                MailTime = DateTime.Now
            };

            var respon = await SendMail(mail);

            //if (!respon)
            //{
            //    ValidateException ex = new ValidateException();
            //    var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0001").FirstAsync();
            //    var msg = "ไม่สามารถส่ง E-Mail ได้ กรุณาติดต่อผู้ดูแลระบบ";
            //    ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            //    throw ex;
            //}
            #endregion
#endif

            var result = await GetAgentAsync(model.ID);
            return result;
        }

        public async Task<AgentDTO> UpdateAgentAsync(Guid id, AgentDTO input)
        {
            await input.ValidateAsync(DB);

            var model = await DB.Agents.FindAsync(id);
            input.ToModel(ref model);

            DB.Entry(model).State = EntityState.Modified;
            await DB.SaveChangesAsync();
            var result = await GetAgentAsync(model.ID);
            return result;
        }

        public async Task DeleteAgentAsync(Guid id)
        {
            #region Validate Check Used Data
            ValidateException ex = new ValidateException();
            var model = await DB.Agents.FindAsync(id);
            var agUsed = await DB.RateSettingAgents.FirstOrDefaultAsync(o => o.AgentID == id);
            if (agUsed != null)
            {
                var errMsg = await DB.ErrorMessages.FirstOrDefaultAsync(o => o.Key == "ERR0148");
                string desc = model?.NameEN;
                var msg = errMsg.Message.Replace("[field]", desc);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }

            if (ex.HasError)
            {
                throw ex;
            }
            #endregion

            model.IsDeleted = true;
            model.Updated = DateTime.Now;

            // await DB.AgentEmployees.Where(o => o.AgentID == id).ExecuteUpdateAsync(c =>
            //   c.SetProperty(col => col.IsDeleted, true)
            //   .SetProperty(col => col.Updated, DateTime.Now)
            // );

            var modelEmployees = await DB.AgentEmployees.Where(o => o.AgentID == id).ToListAsync();
            if (modelEmployees.Count > 0)
            {
                modelEmployees.ForEach(a => a.IsDeleted = true);
            }
            await DB.SaveChangesAsync();

        }

        private async Task<bool> SendMail(Param_Mail mail)
        {
            var requestUrl = $"{Environment.GetEnvironmentVariable("email_HostEmail")}";
            var param_Mails = new List<Param_Mail>();
            param_Mails.Add(mail);

            using (var httpClient = new HttpClient())
            {
                using (var stringContent = new StringContent(JsonConvert.SerializeObject(param_Mails), System.Text.Encoding.UTF8, "application/json"))
                using (var response = await httpClient.PostAsync(requestUrl, stringContent))
                {
                    var r = response;
                    if (r.StatusCode != HttpStatusCode.OK)
                    {
                        return false;
                    }
                }
                return true;
            }
        }
    }
}
