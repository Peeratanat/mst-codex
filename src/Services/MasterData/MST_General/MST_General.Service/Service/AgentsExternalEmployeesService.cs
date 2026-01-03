using Base.DTOs;
using Base.DTOs.MST;
using Database.Models;
using Database.Models.MST;
using MST_General.Params.Filters;
using MST_General.Params.Outputs;
using Microsoft.EntityFrameworkCore;
using PagingExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Helper.Logging;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using Common;
using LinqKit;
using Microsoft.Extensions.Logging;

namespace MST_General.Services
{
    public class AgentsExternalEmployeesService : IAgentsEmployeeExternalService
    {
        public LogModel logModel { get; set; }
        private readonly DatabaseContext DB;
        private readonly IHttpClientFactory _httpClientFactory;

        public AgentsExternalEmployeesService(DatabaseContext db, IHttpClientFactory httpClientFactory)
        {
            logModel = new LogModel("AgentsExternalEmployeesService", null);
            DB = db;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<AgentExternalEmployeesPaging> AgentsExternalEmployeeListAsync(PageParam pageParam, AgentExternalSortByParam sortByParam, AgentEmployeeExternalFilter filter, CancellationToken cancellationToken = default)
        {

            var httpClient = _httpClientFactory.CreateClient(Constants.BCAuthClient);

            var requestUrl = "/api/ext/bc/v1/auth";

            using (var response = await httpClient.GetAsync(requestUrl))
            {
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync(cancellationToken);
                    var resultAuths = JsonConvert.DeserializeObject<AuthBCResult>(data);
                    var settings = new JsonSerializerSettings() { DateFormatString = "yyyy-MM-dd hh:mm:ss" };
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", resultAuths.access_token);

                    var requestagentListUrl = "/agent/v1/agent-sale-list?agent_id=" + filter.AgentID;

                    using (var responseagentList = await httpClient.GetAsync(requestagentListUrl))
                    {
                        if (responseagentList.IsSuccessStatusCode)
                        {
                            var contentseagent = await responseagentList.Content.ReadAsStringAsync(cancellationToken);
                            var resultdata = JsonConvert.DeserializeObject<List<AgentEmployeeExternalDTO>>(contentseagent);


                            var results = resultdata.Select(o => AgentEmployeeExternalDTO.CreateFromQueryResult(o)).ToList();
                            var employeeNos = results
                                .SelectMany(r => new[] { r.create_by, r.modify_by })
                                .Where(empNo => !string.IsNullOrEmpty(empNo))
                                .Distinct()
                                .ToList();

                            var users = await DB.Users
                                .Where(u => employeeNos.Contains(u.EmployeeNo))
                                .ToDictionaryAsync(u => u.EmployeeNo, u => u.DisplayName);

                            foreach (var result in results)
                            {
                                if (!string.IsNullOrEmpty(result.create_by) && users.TryGetValue(result.create_by, out var createByDisplayName))
                                {
                                    result.create_by = createByDisplayName;
                                }

                                if (!string.IsNullOrEmpty(result.modify_by) && users.TryGetValue(result.modify_by, out var modifyByDisplayName))
                                {
                                    result.modify_by = modifyByDisplayName;
                                }
                                if (result.modify_date is null)
                                {
                                    result.modify_date = result.create_date;
                                }
                                if (result.modify_by is null)
                                {
                                    result.modify_by = result.create_by;
                                }
                            }

                            #region filter
                            var predicate = PredicateBuilder.New<AgentEmployeeExternalDTO>(true);
                            if (!string.IsNullOrEmpty(filter.NameTH))
                            {
                                predicate = predicate.And(x => string.Join(" ", x.first_name_th).Contains(filter.NameTH));
                            }
                            if (!string.IsNullOrEmpty(filter.SurnameTH))
                            {
                                predicate = predicate.And(x => string.Join(" ", x.last_name_th).Contains(filter.SurnameTH));
                            }
                            if (!string.IsNullOrEmpty(filter.UpdatedBy))
                            {
                                predicate = predicate.And(x => (x.modify_by ?? x.create_by).Contains(filter.UpdatedBy));
                            }

                            if (filter.UpdatedFrom != null)
                            {
                                predicate = predicate.And(x => x.modify_date.HasValue && x.modify_date.Value.Date >= filter.UpdatedFrom.Value.Date);
                            }
                            if (filter.UpdatedTo != null)
                            {
                                predicate = predicate.And(x => x.modify_date.HasValue && x.modify_date.Value.Date <= filter.UpdatedTo.Value.Date);
                            }
                            if (filter.UpdatedFrom != null || filter.UpdatedTo != null)
                            {
                                predicate = predicate.And(x => x.modify_date.HasValue &&

                                    (x.modify_date.Value.Date >= filter.UpdatedFrom.Value.Date) &&
                                    (x.modify_date.Value.Date <= filter.UpdatedTo.Value.Date)
                                );
                            }
                            #endregion

                            results = results.Where(predicate).ToList();
                            AgentEmployeeExternalDTO.SortBy(sortByParam, ref results);
                            var pageOutput = PagingHelper.PagingList<AgentEmployeeExternalDTO>(pageParam, ref results);

                            return new AgentExternalEmployeesPaging()
                            {
                                AgentEmployeeExternal = results,
                                PageOutput = pageOutput
                            };
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
        }

        public async Task<AgentEmployeeExternalDTO> AgentsExternalEmployeeDetailAsync(Guid agentSaleID, CancellationToken cancellationToken = default)
        {
            var httpClient = _httpClientFactory.CreateClient(Constants.BCAuthClient);

            var requestUrl = "/api/ext/bc/v1/auth";
            using (var response = await httpClient.GetAsync(requestUrl))
            {
                if (response.IsSuccessStatusCode)
                {

                    var data = await response.Content.ReadAsStringAsync();
                    var resultAuths = JsonConvert.DeserializeObject<AuthBCResult>(data);
                    var settings = new JsonSerializerSettings() { DateFormatString = "yyyy-MM-dd hh:mm:ss" };

                    var requestagentListUrl = "/agent/v1/agent-sale?agent_sale_id=" + agentSaleID;

                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", resultAuths.access_token);

                    using (var responseagentList = await httpClient.GetAsync(requestagentListUrl))
                    {
                        if (responseagentList.IsSuccessStatusCode)
                        {
                            var contentseagent = await responseagentList.Content.ReadAsStringAsync();
                            var resultdata = JsonConvert.DeserializeObject<AgentEmployeeExternalDTO>(contentseagent);

                            var result = AgentEmployeeExternalDTO.CreateFromQueryResult(resultdata);

                            return result ?? new AgentEmployeeExternalDTO();
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
        }

        public async Task<AgentExternalEmployeesResp> UpdateAgentsExternalEmployeeAsync(AgentEmployeeExternalDTO input)
        {
            var httpClient = _httpClientFactory.CreateClient(Constants.BCAuthClient);

            var requestUrl = "/api/ext/bc/v1/auth";

            using (var response = await httpClient.GetAsync(requestUrl))
            {
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();

                    var results = JsonConvert.DeserializeObject<AuthBCResult>(data);

                    var requestagentListUrl = "/agent/v1/submit-agent-sale";

                    var settings = new JsonSerializerSettings() { DateFormatString = "yyyy-MM-dd hh:mm:ss" };

                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", results.access_token);

                    Dictionary<string, object> propertyMapUpdateResult = new Dictionary<string, object>
                        {
                             { "agent_sale_id",input.agent_sale_id},
                             { "agent_id",input.agent_id},
                             { "prefix_name_th",input.prefix_name_th },
                             { "first_name_th",input.first_name_th },
                             { "last_name_th",input.last_name_th },
                             { "modify_by",input.modify_by },
                             { "modify_date",DateTime.Now },
                        };


                    var stringPayload = JsonConvert.SerializeObject(propertyMapUpdateResult);
                    var httpContent = new StringContent(stringPayload, Encoding.UTF8, "application/json");

                    using (var responseagentList = await httpClient.PostAsync(requestagentListUrl, httpContent))
                    {
                        if (responseagentList.IsSuccessStatusCode)
                        {
                            var contentseagent = await responseagentList.Content.ReadAsStringAsync();
                            var resultdata = JsonConvert.DeserializeObject<AgentExternalEmployeesResp>(contentseagent);
                            return new AgentExternalEmployeesResp()
                            {
                                id = resultdata.id,
                                is_success = resultdata.is_success,
                                error_message = resultdata.error_message
                            };
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
        }

        public async Task<AgentExternalEmployeesResp> CreateAgentsExternalEmployeeAsync(AgentEmployeeExternalDTO input)
        {
            var httpClient = _httpClientFactory.CreateClient(Constants.BCAuthClient);

            var requestUrl = "/api/ext/bc/v1/auth";

            using (var response = await httpClient.GetAsync(requestUrl))
            {
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();

                    var results = JsonConvert.DeserializeObject<AuthBCResult>(data);

                    var requestagentListUrl = "/agent/v1/submit-agent-sale";

                    var settings = new JsonSerializerSettings() { DateFormatString = "yyyy-MM-dd hh:mm:ss" };

                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", results.access_token);

                    Dictionary<string, object> propertyMapUpdateResult = new Dictionary<string, object>
                        {
                             { "agent_id",input.agent_id },
                             { "prefix_name_th",input.prefix_name_th },
                             { "first_name_th",input.first_name_th },
                             { "last_name_th",input.last_name_th },
                             { "create_by",input.create_by },
                             { "create_date", DateTime.Now },
                             { "modify_by",input.create_by },
                             { "modify_date",DateTime.Now },
                        };


                    var stringPayload = JsonConvert.SerializeObject(propertyMapUpdateResult);
                    var httpContent = new StringContent(stringPayload, Encoding.UTF8, "application/json");

                    using (var responseagentList = await httpClient.PostAsync(requestagentListUrl, httpContent))
                    {
                        if (responseagentList.IsSuccessStatusCode)
                        {
                            var contentseagent = await responseagentList.Content.ReadAsStringAsync();
                            var resultdata = JsonConvert.DeserializeObject<AgentExternalEmployeesResp>(contentseagent);
                            return new AgentExternalEmployeesResp()
                            {
                                id = resultdata.id,
                                is_success = resultdata.is_success,
                                error_message = resultdata.error_message
                            };
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
        }

    }
}
