using Database.Models;
using Auth_User.Params.Outputs;
using Microsoft.EntityFrameworkCore;
using System.Data;
using Common.Helper.Logging;
using Base.DTOs.MST;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using Common;

namespace Auth_User.Services
{
    public class AgentsBCService : IAgentsBCService
    {
        private readonly DatabaseContext DB;
        public LogModel logModel { get; set; }
        int Timeout = 300;
        private readonly IHttpClientFactory _httpClientFactory;

        public AgentsBCService(DatabaseContext db, IHttpClientFactory httpClientFactory)
        {
            DB = db;
            DB.Database.SetCommandTimeout(Timeout);
            logModel = new LogModel("AgentsBCService", null);
            _httpClientFactory = httpClientFactory;
        }

        public async Task<List<AgentDropdownDTO>> AgentBCListAsync(string agentOwner, string name)
        {
            //DDL Agency
            //agent-list

            var httpClient = _httpClientFactory.CreateClient(Constants.BCAuthClient);

            var requestUrl = "/api/ext/bc/v1/auth";
            var resultAgentBC = new List<AgentDropdownDTO>();
            using (var response = await httpClient.GetAsync(requestUrl))
            {
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();

                    var results = JsonConvert.DeserializeObject<AuthBCResult>(data);

                    var requestagentListUrl = "/agent/v1/agent-list?AgentOwner=" + agentOwner;

                    var settings = new JsonSerializerSettings() { DateFormatString = "yyyy-MM-dd hh:mm:ss" };

                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", results.access_token);

                    using (var responseagentList = await httpClient.GetAsync(requestagentListUrl))
                    {

                        if (responseagentList.IsSuccessStatusCode)
                        {

                            var contentseagent = await responseagentList.Content.ReadAsStringAsync();
                            var resultdata = JsonConvert.DeserializeObject<List<AgentListBCResult>>(contentseagent);
                            foreach (var item in resultdata)
                            {
                                var aBC = new AgentDropdownDTO();

                                aBC.Id = item.agent_id;

                                aBC.NameTH = item.first_name_th?.Trim() + " " + item.last_name_th?.Trim();
                                aBC.NameEN = item.first_name_eng?.Trim() + " " + item.last_name_eng?.Trim();

                                //if (item.first_name_eng == "FABRIZIO")
                                //{
                                //    var nameEN = item.first_name_eng;
                                //}

                                if (string.IsNullOrEmpty(aBC.NameTH) || aBC.NameTH.Equals("-") || aBC.NameTH.Equals(" "))
                                {
                                    aBC.NameTH = aBC.NameEN;
                                }

                                aBC.Create_by = item.create_by;
                                aBC.Create_date = item.create_date;
                                aBC.Modify_by = item.modify_by;
                                aBC.Modify_date = item.modify_date;
                                resultAgentBC.Add(aBC);
                            }

                            if (!string.IsNullOrEmpty(name) && resultAgentBC.Count() > 0)
                            {
                                var nameSearch = name.Trim();
                                resultAgentBC = resultAgentBC.Where(o => !string.IsNullOrEmpty(o.NameTH)).ToList();
                                resultAgentBC = resultAgentBC.Where(o => o.NameTH.ToLower().Contains(nameSearch.ToLower())).ToList();
                            }
                        }
                        else
                        {
                            throw new Exception(responseagentList.RequestMessage?.ToString());
                        }
                    }
                }
                else
                {
                    throw new Exception(response.RequestMessage?.ToString());
                }
            }
            return resultAgentBC ?? new List<AgentDropdownDTO>();
        }

        public async Task<List<AgentEmployeeDropdownDTO>> AgentBCSaleListAsync(Guid? agentID, string name)
        {
            //DDL พนักงานปิดการขาย (Agency)
            //agent-sale-list
 
 
            var httpClient = _httpClientFactory.CreateClient(Constants.BCAuthClient);
            var requestUrl = "/api/ext/bc/v1/auth";
            var resultAgentEmpBC = new List<AgentEmployeeDropdownDTO>();
            using (var response = await httpClient.GetAsync(requestUrl))
            {
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();

                    var results = JsonConvert.DeserializeObject<AuthBCResult>(data);
                    var requestagentListUrl = "";
                    if (agentID != null)
                    {
                        requestagentListUrl = "agent/v1/agent-sale-list?agent_id=" + agentID;
                    }
                    else
                    {
                        requestagentListUrl = "agent/v1/agent-sale-list";
                    }

                    var settings = new JsonSerializerSettings() { DateFormatString = "yyyy-MM-dd hh:mm:ss" };

                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", results.access_token);

                    using (var responseagentList = await httpClient.GetAsync(requestagentListUrl))
                    {

                        if (responseagentList.IsSuccessStatusCode)
                        {

                            var contentseagent = await responseagentList.Content.ReadAsStringAsync();
                            var resultdata = JsonConvert.DeserializeObject<List<AgentListBCSaleResult>>(contentseagent);

                            foreach (var item in resultdata)
                            {
                                var aBC = new AgentEmployeeDropdownDTO();

                                aBC.Id = item.agent_sale_id;
                                aBC.FirstName = item.first_name_th?.Trim();
                                aBC.LastName = item.last_name_th?.Trim();
                                aBC.Fullname = item.first_name_th?.Trim() + " " + item.last_name_th?.Trim();

                                resultAgentEmpBC.Add(aBC);
                            }

                            if (!string.IsNullOrEmpty(name) && resultAgentEmpBC.Count() > 0)
                            {
                                var nameSearch = name.Trim();
                                resultAgentEmpBC = resultAgentEmpBC.Where(o => !string.IsNullOrEmpty(o.Fullname)).ToList();
                                resultAgentEmpBC = resultAgentEmpBC.Where(o => o.Fullname.ToLower().Contains(nameSearch.ToLower())).ToList();
                            }
                        }
                        else
                        {
                            throw new Exception(responseagentList.RequestMessage?.ToString());
                        }
                    }
                }
                else
                {
                    throw new Exception(response.RequestMessage?.ToString());
                }
            }
            return resultAgentEmpBC ?? new List<AgentEmployeeDropdownDTO>();
        }


    }
}
