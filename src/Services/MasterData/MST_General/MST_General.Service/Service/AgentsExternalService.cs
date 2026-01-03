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
using System.Net.Http.Headers;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using Azure.Core;
using System.Text;
using Microsoft.Extensions.Logging;
using Common;
using LinqKit;
using Microsoft.Extensions.Primitives;
using Base.DTOs.USR;
using Database.Models.USR;

namespace MST_General.Services
{
    public class AgentsExternalService : IAgentsExternalService
    {
        public LogModel logModel { get; set; }
        private readonly DatabaseContext DB;

        private readonly IHttpClientFactory _httpClientFactory;
        public AgentsExternalService(DatabaseContext db, IHttpClientFactory httpClientFactory)
        {
            logModel = new LogModel("AgentsExternalService", null);
            DB = db;
            _httpClientFactory = httpClientFactory;
        }


        public async Task<AgentExternalPaging> AgentExternalListAsync(AgentExternalFilter filter, PageParam pageParam, AgentExternalSortByParam sortByParam, CancellationToken cancellationToken = default)
        {
            var httpClient = _httpClientFactory.CreateClient(Constants.BCAuthClient);

            var requestUrl = "/api/ext/bc/v1/auth";
            var resultList = new List<AgentExternalDTO>();
            using (var response = await httpClient.GetAsync(requestUrl))
            {
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();

                    var resultAuths = JsonConvert.DeserializeObject<AuthBCResult>(data);

                    // var agentOwnerType = "";

                    //if (filter.AgentOwner == "4")
                    //    agentOwnerType = "AP";
                    //else if (filter.AgentOwner == "8")
                    //    agentOwnerType = "BC_Oversea";
                    //else if (filter.AgentOwner == "5")
                    //    agentOwnerType = "BC_CoAgency";
                    //else if (filter.AgentOwner == "7")
                    //    agentOwnerType = "BC";
                    //else
                    //    agentOwnerType = filter.AgentOwner;
                    var agentOwnerType = filter.AgentOwner;

                    if (string.IsNullOrEmpty(agentOwnerType)) //initial loadpage
                    {
                        List<string> agentOwnerList = new List<string> { "AP", "BC", "BC_CoAgency", "BC_Oversea" };//AP = AP Co-Agency, BC = BC, BC_CoAgency = BC Co-Agency, BC_Oversea = BC Oversea

                        foreach (var ownerKeyItem in agentOwnerList)
                        {
                            var requestagentListUrl = "/agent/v1/agent-list?AgentOwner=" + ownerKeyItem;

                            var settings = new JsonSerializerSettings() { DateFormatString = "yyyy-MM-dd hh:mm:ss" };

                            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", resultAuths.access_token);

                            using (var responseagentList = await httpClient.GetAsync(requestagentListUrl))
                            {
                                if (responseagentList.IsSuccessStatusCode)
                                {
                                    var contentseagent = await responseagentList.Content.ReadAsStringAsync();
                                    var resultdata = JsonConvert.DeserializeObject<List<AgentExternalDTO>>(contentseagent);

                                    var results = resultdata.Select(o => AgentExternalDTO.CreateFromQueryResult(o)).ToList();

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

                                        resultList.Add(result);
                                    }

                                    //xx            
                                }
                                else
                                {
                                    throw new Exception(responseagentList.RequestMessage?.ToString());
                                }
                            }
                        }

                        #region filter
                        var predicate = PredicateBuilder.New<AgentExternalDTO>(true);
                        if (!string.IsNullOrEmpty(filter.NameTH))
                        {
                            predicate = predicate.And(x => string.Join(" ", x.prefix_name_th, x.first_name_th, x.last_name_th).ToLower().Contains(filter.NameTH.ToLower()));
                        }
                        if (!string.IsNullOrEmpty(filter.NameEng))
                        {
                            predicate = predicate.And(x => string.Join(" ", x.prefix_name_eng, x.first_name_eng, x.last_name_eng).ToLower().Contains(filter.NameEng.ToLower()));
                        }
                        if (!string.IsNullOrEmpty(filter.AgentTypeName))
                        {
                            predicate = predicate.And(x => x.agent_type_name.ToLower().Contains(filter.AgentTypeName.ToLower()));
                        }
                        if (!string.IsNullOrEmpty(filter.TaxID))
                        {
                            predicate = predicate.And(x => (x.tax_id ?? "").ToLower().Contains(filter.TaxID.ToLower()));
                        }
                        if (!string.IsNullOrEmpty(filter.PassportNo))
                        {
                            predicate = predicate.And(x => (x.passport_no ?? "").ToLower().Contains(filter.PassportNo.ToLower()));
                        }
                        //if (!string.IsNullOrEmpty(filter.AgentOwner))
                        //{
                        //    predicate = predicate.And(x => (x.agent_owner ?? "").ToLower().Contains(filter.AgentOwner.ToLower()));
                        //}
                        if (!string.IsNullOrEmpty(filter.AllowSdh))
                        {
                            predicate = predicate.And(x => (x.allow_sdh ?? "").ToLower().Contains(filter.AllowSdh.ToLower()));
                        }
                        if (!string.IsNullOrEmpty(filter.AllowTh))
                        {
                            predicate = predicate.And(x => (x.allow_th ?? "").ToLower().Contains(filter.AllowTh.ToLower()));
                        }
                        if (!string.IsNullOrEmpty(filter.AllowCd))
                        {
                            predicate = predicate.And(x => (x.allow_cd ?? "").ToLower().Contains(filter.AllowCd.ToLower()));
                        }

                        if (!string.IsNullOrEmpty(filter.UpdatedBy))
                        {
                            predicate = predicate.And(x => (x.modify_by ?? x.create_by).ToLower().Contains(filter.UpdatedBy.ToLower()));
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
                        resultList = resultList.Where(predicate).ToList();
                        AgentExternalDTO.SortBy(sortByParam, ref resultList);
                        var pageOutput = PagingHelper.PagingList<AgentExternalDTO>(pageParam, ref resultList);

                        return new AgentExternalPaging()
                        {
                            AgentExternals = resultList,
                            PageOutput = pageOutput
                        };
                    }
                    //else
                    //{
                    //    var requestagentListUrl = "/agent/v1/agent-list?AgentOwner=" + agentOwnerType;

                    //    var settings = new JsonSerializerSettings() { DateFormatString = "yyyy-MM-dd hh:mm:ss" };

                    //    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", resultAuths.access_token);

                    //    using (var responseagentList = await httpClient.GetAsync(requestagentListUrl))
                    //    {
                    //        if (responseagentList.IsSuccessStatusCode)
                    //        {
                    //            var contentseagent = await responseagentList.Content.ReadAsStringAsync();
                    //            var resultdata = JsonConvert.DeserializeObject<List<AgentExternalDTO>>(contentseagent);

                    //            var results = resultdata.Select(o => AgentExternalDTO.CreateFromQueryResult(o)).ToList();

                    //            var employeeNos = results
                    //                .SelectMany(r => new[] { r.create_by, r.modify_by })
                    //                .Where(empNo => !string.IsNullOrEmpty(empNo))
                    //                .Distinct()
                    //                .ToList();

                    //            var users = await DB.Users
                    //                .Where(u => employeeNos.Contains(u.EmployeeNo))
                    //                .ToDictionaryAsync(u => u.EmployeeNo, u => u.DisplayName);

                    //            foreach (var result in results)
                    //            {
                    //                if (!string.IsNullOrEmpty(result.create_by) && users.TryGetValue(result.create_by, out var createByDisplayName))
                    //                {
                    //                    result.create_by = createByDisplayName;
                    //                }

                    //                if (!string.IsNullOrEmpty(result.modify_by) && users.TryGetValue(result.modify_by, out var modifyByDisplayName))
                    //                {
                    //                    result.modify_by = modifyByDisplayName;
                    //                }
                    //                if (result.modify_date is null)
                    //                {
                    //                    result.modify_date = result.create_date;
                    //                }
                    //                if (result.modify_by is null)
                    //                {
                    //                    result.modify_by = result.create_by;
                    //                }
                    //            }

                    //            #region filter
                    //            var predicate = PredicateBuilder.New<AgentExternalDTO>(true);
                    //            if (!string.IsNullOrEmpty(filter.NameTH))
                    //            {
                    //                predicate = predicate.And(x => string.Join(" ", x.prefix_name_th, x.first_name_th, x.last_name_th).ToLower().Contains(filter.NameTH.ToLower()));
                    //            }
                    //            if (!string.IsNullOrEmpty(filter.NameEng))
                    //            {
                    //                predicate = predicate.And(x => string.Join(" ", x.prefix_name_eng, x.first_name_eng, x.last_name_eng).ToLower().Contains(filter.NameEng.ToLower()));
                    //            }
                    //            if (!string.IsNullOrEmpty(filter.AgentTypeName))
                    //            {
                    //                predicate = predicate.And(x => x.agent_type_name.ToLower().Contains(filter.AgentTypeName.ToLower()));
                    //            }
                    //            if (!string.IsNullOrEmpty(filter.TaxID))
                    //            {
                    //                predicate = predicate.And(x => (x.tax_id ?? "").ToLower().Contains(filter.TaxID.ToLower()));
                    //            }
                    //            if (!string.IsNullOrEmpty(filter.PassportNo))
                    //            {
                    //                predicate = predicate.And(x => (x.passport_no ?? "").ToLower().Contains(filter.PassportNo.ToLower()));
                    //            }
                    //            if (!string.IsNullOrEmpty(filter.AgentOwner))
                    //            {
                    //                predicate = predicate.And(x => (x.agent_owner ?? "").ToLower().Contains(agentOwnerType.ToLower()));
                    //            }
                    //            if (!string.IsNullOrEmpty(filter.AllowSdh))
                    //            {
                    //                predicate = predicate.And(x => (x.allow_sdh ?? "").ToLower().Contains(filter.AllowSdh.ToLower()));
                    //            }
                    //            if (!string.IsNullOrEmpty(filter.AllowTh))
                    //            {
                    //                predicate = predicate.And(x => (x.allow_th ?? "").ToLower().Contains(filter.AllowTh.ToLower()));
                    //            }
                    //            if (!string.IsNullOrEmpty(filter.AllowCd))
                    //            {
                    //                predicate = predicate.And(x => (x.allow_cd ?? "").ToLower().Contains(filter.AllowCd.ToLower()));
                    //            }

                    //            if (!string.IsNullOrEmpty(filter.UpdatedBy))
                    //            {
                    //                predicate = predicate.And(x => (x.modify_by ?? x.create_by).ToLower().Contains(filter.UpdatedBy.ToLower()));
                    //            }

                    //            if (filter.UpdatedFrom != null)
                    //            {
                    //                predicate = predicate.And(x => x.modify_date.HasValue && x.modify_date.Value.Date >= filter.UpdatedFrom.Value.Date);
                    //            }
                    //            if (filter.UpdatedTo != null)
                    //            {
                    //                predicate = predicate.And(x => x.modify_date.HasValue && x.modify_date.Value.Date <= filter.UpdatedTo.Value.Date);
                    //            }
                    //            if (filter.UpdatedFrom != null || filter.UpdatedTo != null)
                    //            {
                    //                predicate = predicate.And(x => x.modify_date.HasValue &&

                    //                    (x.modify_date.Value.Date >= filter.UpdatedFrom.Value.Date) &&
                    //                    (x.modify_date.Value.Date <= filter.UpdatedTo.Value.Date)
                    //                );
                    //            }
                    //            #endregion
                    //            results = results.Where(predicate).ToList();
                    //            AgentExternalDTO.SortBy(sortByParam, ref results);
                    //            var pageOutput = PagingHelper.PagingList<AgentExternalDTO>(pageParam, ref results);

                    //            return new AgentExternalPaging()
                    //            {
                    //                AgentExternals = results,
                    //                PageOutput = pageOutput
                    //            };
                    //        }
                    //        else
                    //        {
                    //            throw new Exception(responseagentList.RequestMessage?.ToString());
                    //        }
                    //    }
                    //}
                    else
                    {
                        List<string> agentOwnerList = new List<string> { "AP", "BC", "BC_CoAgency", "BC_Oversea" };//AP = AP Co-Agency, BC = BC, BC_CoAgency = BC Co-Agency, BC_Oversea = BC Oversea

                        foreach (var ownerKeyItem in agentOwnerList)
                        {
                            var requestagentListUrl = "/agent/v1/agent-list?AgentOwner=" + ownerKeyItem;

                            var settings = new JsonSerializerSettings() { DateFormatString = "yyyy-MM-dd hh:mm:ss" };

                            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", resultAuths.access_token);

                            using (var responseagentList = await httpClient.GetAsync(requestagentListUrl))
                            {
                                if (responseagentList.IsSuccessStatusCode)
                                {
                                    var contentseagent = await responseagentList.Content.ReadAsStringAsync();
                                    var resultdata = JsonConvert.DeserializeObject<List<AgentExternalDTO>>(contentseagent);

                                    var results = resultdata.Select(o => AgentExternalDTO.CreateFromQueryResult(o)).ToList();

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

                                        resultList.Add(result);
                                    }

                                    //xx            
                                }
                                else
                                {
                                    throw new Exception(responseagentList.RequestMessage?.ToString());
                                }
                            }
                        }

                        #region filter
                        var predicate = PredicateBuilder.New<AgentExternalDTO>(true);
                        if (!string.IsNullOrEmpty(filter.NameTH))
                        {
                            predicate = predicate.And(x => string.Join(" ", x.prefix_name_th, x.first_name_th, x.last_name_th).ToLower().Contains(filter.NameTH.ToLower()));
                        }
                        if (!string.IsNullOrEmpty(filter.NameEng))
                        {
                            predicate = predicate.And(x => string.Join(" ", x.prefix_name_eng, x.first_name_eng, x.last_name_eng).ToLower().Contains(filter.NameEng.ToLower()));
                        }
                        if (!string.IsNullOrEmpty(filter.AgentTypeName))
                        {
                            predicate = predicate.And(x => x.agent_type_name.ToLower().Contains(filter.AgentTypeName.ToLower()));
                        }
                        if (!string.IsNullOrEmpty(filter.TaxID))
                        {
                            predicate = predicate.And(x => (x.tax_id ?? "").ToLower().Contains(filter.TaxID.ToLower()));
                        }
                        if (!string.IsNullOrEmpty(filter.PassportNo))
                        {
                            predicate = predicate.And(x => (x.passport_no ?? "").ToLower().Contains(filter.PassportNo.ToLower()));
                        }
                        if (!string.IsNullOrEmpty(filter.AgentOwner))
                        {
                            predicate = predicate.And(x => (x.agent_owner ?? "").ToLower().Contains(filter.AgentOwner.ToLower()));
                        }
                        if (!string.IsNullOrEmpty(filter.AllowSdh))
                        {
                            predicate = predicate.And(x => (x.allow_sdh ?? "").ToLower().Contains(filter.AllowSdh.ToLower()));
                        }
                        if (!string.IsNullOrEmpty(filter.AllowTh))
                        {
                            predicate = predicate.And(x => (x.allow_th ?? "").ToLower().Contains(filter.AllowTh.ToLower()));
                        }
                        if (!string.IsNullOrEmpty(filter.AllowCd))
                        {
                            predicate = predicate.And(x => (x.allow_cd ?? "").ToLower().Contains(filter.AllowCd.ToLower()));
                        }

                        if (!string.IsNullOrEmpty(filter.UpdatedBy))
                        {
                            predicate = predicate.And(x => (x.modify_by ?? x.create_by).ToLower().Contains(filter.UpdatedBy.ToLower()));
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
                        resultList = resultList.Where(predicate).ToList();
                        AgentExternalDTO.SortBy(sortByParam, ref resultList);
                        var pageOutput = PagingHelper.PagingList<AgentExternalDTO>(pageParam, ref resultList);

                        return new AgentExternalPaging()
                        {
                            AgentExternals = resultList,
                            PageOutput = pageOutput
                        };
                    }
                }
                else
                {
                    throw new Exception(response.RequestMessage?.ToString());
                }
            }
        }

        public async Task<AgentExternalDTO> AgentExternaDetailAsync(Guid agentID, CancellationToken cancellationToken = default)
        {
            var httpClient = _httpClientFactory.CreateClient(Constants.BCAuthClient);
            var requestUrl = "/api/ext/bc/v1/auth";
            using (var response = await httpClient.GetAsync(requestUrl))
            {
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();

                    var resultAuths = JsonConvert.DeserializeObject<AuthBCResult>(data);

                    var requestagentListUrl = "/agent/v1/agent?agent_id=" + agentID;

                    var settings = new JsonSerializerSettings() { DateFormatString = "yyyy-MM-dd hh:mm:ss" };

                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", resultAuths.access_token);

                    using (var responseagentList = await httpClient.GetAsync(requestagentListUrl))
                    {
                        if (responseagentList.IsSuccessStatusCode)
                        {
                            var contentseagent = await responseagentList.Content.ReadAsStringAsync();
                            var resultdata = JsonConvert.DeserializeObject<AgentExternalDTO>(contentseagent);

                            var result = AgentExternalDTO.CreateFromQueryResult(resultdata);

                            return result ?? new AgentExternalDTO();
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

        public async Task<AgentExternalResp> UpdateAgentExternalAsync(AgentExternalDTO input)
        {
            var httpClient = _httpClientFactory.CreateClient(Constants.BCAuthClient);


            var requestUrl = "/api/ext/bc/v1/auth";

            using (var response = await httpClient.GetAsync(requestUrl))
            {
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();

                    var results = JsonConvert.DeserializeObject<AuthBCResult>(data);

                    var requestagentListUrl = "/agent/v1/submit-agent";

                    var settings = new JsonSerializerSettings() { DateFormatString = "yyyy-MM-dd hh:mm:ss" };

                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", results.access_token);


                    Dictionary<string, object> propertyMapUpdateResult = new Dictionary<string, object>
                        {
                             { "agent_id",input.agent_id},
                             { "is_thai",input.is_thai },
                             { "agent_type_id",input.agent_type_id },
                             { "agent_type_name",input.agent_type_name },
                             { "prefix_name_th",input.prefix_name_th },
                             { "first_name_th",input.first_name_th },
                             { "last_name_th",input.last_name_th },
                             { "prefix_name_eng",input.prefix_name_eng },
                             { "first_name_eng",input.first_name_eng },
                             { "last_name_eng",input.last_name_eng },
                             { "id_card",input.id_card },
                             { "passport_no",input.passport_no },
                             { "tax_id",input.tax_id },
                             { "agent_owner",input.agent_owner },
                             { "last_activity_date",input.last_activity_date },
                             { "modify_by",input.modify_by },
                             { "modify_date",DateTime.Now },
                             //{ "is_allow_sdh",input.is_allow_sdh == 1 ? 0 : 1 },
                             //{ "is_allow_cd",input.is_allow_cd == 1 ? 0 : 1  },
                             //{ "is_allow_th",input.is_allow_th == 1 ? 0 : 1  }
                             { "is_allow_sdh",input.is_allow_sdh},
                             { "is_allow_cd",input.is_allow_cd},
                             { "is_allow_th",input.is_allow_th}

                        };


                    var stringPayload = JsonConvert.SerializeObject(propertyMapUpdateResult);
                    var httpContent = new StringContent(stringPayload, Encoding.UTF8, "application/json");

                    using (var responseagentList = await httpClient.PostAsync(requestagentListUrl, httpContent))
                    {
                        if (responseagentList.IsSuccessStatusCode)
                        {
                            var contentseagent = await responseagentList.Content.ReadAsStringAsync();
                            var resultdata = JsonConvert.DeserializeObject<AgentExternalResp>(contentseagent);
                            return new AgentExternalResp()
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

        public async Task<AgentExternalResp> CreateAgentExternalAsync(AgentExternalDTO input)
        {
            var httpClient = _httpClientFactory.CreateClient(Constants.BCAuthClient);

            var requestUrl = "/api/ext/bc/v1/auth";

            using (var response = await httpClient.GetAsync(requestUrl))
            {
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();

                    var results = JsonConvert.DeserializeObject<AuthBCResult>(data);

                    var requestagentListUrl = "/agent/v1/submit-agent";

                    var settings = new JsonSerializerSettings() { DateFormatString = "yyyy-MM-dd hh:mm:ss" };

                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", results.access_token);


                    Dictionary<string, object> propertyMapAddResult = new Dictionary<string, object>
                        {
                             { "is_thai",input.is_thai },
                             { "agent_type_id",input.agent_type_id },
                             { "agent_type_name",input.agent_type_name },
                             { "prefix_name_th",input.prefix_name_th },
                             { "first_name_th",input.first_name_th },
                             { "last_name_th",input.last_name_th },
                             { "prefix_name_eng",input.prefix_name_eng },
                             { "first_name_eng",input.first_name_eng },
                             { "last_name_eng",input.last_name_eng },
                             { "id_card",input.id_card },
                             { "passport_no",input.passport_no },
                             { "tax_id",input.tax_id },
                             { "agent_owner",input.agent_owner },
                             { "last_activity_date",input.last_activity_date },
                             { "create_by",input.create_by },
                             { "create_date", DateTime.Now },
                             { "modify_by",input.create_by },
                             { "modify_date",DateTime.Now },
                             //{ "is_allow_sdh",input.is_allow_sdh == 1 ? 0 : 1 },
                             //{ "is_allow_cd",input.is_allow_cd == 1 ? 0 : 1  },
                             //{ "is_allow_th",input.is_allow_th == 1 ? 0 : 1  }
                             { "is_allow_sdh",input.is_allow_sdh },
                             { "is_allow_cd",input.is_allow_cd },
                             { "is_allow_th",input.is_allow_th }
                        };


                    var stringPayload = JsonConvert.SerializeObject(propertyMapAddResult);
                    var httpContent = new StringContent(stringPayload, Encoding.UTF8, "application/json");

                    using (var responseagentList = await httpClient.PostAsync(requestagentListUrl, httpContent))
                    {
                        if (responseagentList.IsSuccessStatusCode)
                        {
                            var contentseagent = await responseagentList.Content.ReadAsStringAsync();
                            var resultdata = JsonConvert.DeserializeObject<AgentExternalResp>(contentseagent);

                            return new AgentExternalResp()
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

        public async Task<List<AgentExternalPrefixDTO>> GetPrefixListAsync(int isCorporate, CancellationToken cancellationToken = default)
        {
            var httpClient = _httpClientFactory.CreateClient(Constants.BCAuthClient);

            var requestUrl = "/api/ext/bc/v1/auth";
            var resultAuthBC = new List<AgentDropdownDTO>();
            using (var response = await httpClient.GetAsync(requestUrl))
            {
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();

                    var resultAuths = JsonConvert.DeserializeObject<AuthBCResult>(data);

                    var requestagentListUrl = "/master/v1/prefix-list?iS_BUSINESS_CUSTOMER=" + isCorporate;

                    var settings = new JsonSerializerSettings() { DateFormatString = "yyyy-MM-dd hh:mm:ss" };

                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", resultAuths.access_token);

                    using (var responseagentList = await httpClient.GetAsync(requestagentListUrl))
                    {
                        if (responseagentList.IsSuccessStatusCode)
                        {
                            var contentseagent = await responseagentList.Content.ReadAsStringAsync();
                            var resultdata = JsonConvert.DeserializeObject<List<AgentExternalPrefixDTO>>(contentseagent);

                            var result = resultdata.Select(o => AgentExternalPrefixDTO.CreateFromQueryResult(o)).ToList();

                            return result ?? new List<AgentExternalPrefixDTO>();
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

        public async Task<List<AgentBusinessTypeDropdownDTO>> GetBusinessTypeListAsync(CancellationToken cancellationToken = default)
        {
            var httpClient = _httpClientFactory.CreateClient(Constants.BCAuthClient);

            var requestUrl = "/api/ext/bc/v1/auth";

            using (var response = await httpClient.GetAsync(requestUrl))
            {
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();

                    var resultAuths = JsonConvert.DeserializeObject<AuthBCResult>(data);

                    var requestagentListUrl = "/master/v1/business-type-list";

                    var settings = new JsonSerializerSettings() { DateFormatString = "yyyy-MM-dd hh:mm:ss" };

                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", resultAuths.access_token);

                    using (var responseagentList = await httpClient.GetAsync(requestagentListUrl))
                    {
                        if (responseagentList.IsSuccessStatusCode)
                        {
                            var contentseagent = await responseagentList.Content.ReadAsStringAsync();
                            var resultdata = JsonConvert.DeserializeObject<List<AgentBusinessTypeDropdownDTO>>(contentseagent);

                            var result = resultdata.Select(o => AgentBusinessTypeDropdownDTO.CreateFromModel(o)).ToList();

                            return result ?? new List<AgentBusinessTypeDropdownDTO>();
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

        public async Task<AgentExternalResp> CheckDuplicateAgentExternalAsync(AgentExternalFilter filter, CancellationToken cancellationToken = default)
        {
            var httpClient = _httpClientFactory.CreateClient(Constants.BCAuthClient);

            var requestUrl = "/api/ext/bc/v1/auth";
            var resultList = new List<AgentExternalDTO>();
            using (var response = await httpClient.GetAsync(requestUrl))
            {
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();

                    var resultAuths = JsonConvert.DeserializeObject<AuthBCResult>(data);

                    var agentOwnerType = filter.AgentOwner;


                    var requestagentListUrl = "/agent/v1/agent-list?AgentOwner=" + agentOwnerType;

                    var settings = new JsonSerializerSettings() { DateFormatString = "yyyy-MM-dd hh:mm:ss" };

                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", resultAuths.access_token);

                    using (var responseagentList = await httpClient.GetAsync(requestagentListUrl))
                    {
                        if (responseagentList.IsSuccessStatusCode)
                        {
                            var contentseagent = await responseagentList.Content.ReadAsStringAsync();

                            var resultdata = JsonConvert.DeserializeObject<List<AgentExternalDTO>>(contentseagent);

                            var results = resultdata.Select(o => AgentExternalDTO.CreateFromQueryResult(o)).ToList();

                            #region filter
                            var predicate = PredicateBuilder.New<AgentExternalDTO>(true);

                            if (filter.AgentID != null)
                            {
                                predicate = predicate.And(x => x.agent_id != filter.AgentID);
                            }
                            if (!string.IsNullOrEmpty(filter.IDCard))
                            {
                                predicate = predicate.And(x => (x.id_card ?? "").ToLower().Contains(filter.IDCard.ToLower()));
                                predicate = predicate.And(x => (x.tax_id ?? "").ToLower().Contains(filter.IDCard.ToLower()));
                            }
                            if (!string.IsNullOrEmpty(filter.TaxID))
                            {
                                predicate = predicate.And(x => (x.id_card ?? "").ToLower().Contains(filter.TaxID.ToLower()));
                                predicate = predicate.And(x => (x.tax_id ?? "").ToLower().Contains(filter.TaxID.ToLower()));
                            }
                            if (!string.IsNullOrEmpty(filter.PassportNo))
                            {
                                predicate = predicate.And(x => (x.passport_no ?? "").ToLower().Contains(filter.PassportNo.ToLower()));
                            }
                            #endregion

                            results = results.Where(predicate).ToList();
                            if (results.Count > 0)
                            {
                                return new AgentExternalResp() { id = 0, is_success = false, error_message = "มีข้อมูล Agent นี้แล้วในระบบ CRM AP!" };
                            }
                            else
                            {
                                return new AgentExternalResp() { id = 0, is_success = true, error_message = null };
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
        }

        public async Task<List<AgentExternalDTO>> AgentExternalAPOwnerListAsync(AgentExternalFilter filter, CancellationToken cancellationToken = default)
        {
            var httpClient = _httpClientFactory.CreateClient(Constants.BCAuthClient);

            var requestUrl = "/api/ext/bc/v1/auth";
            var resultList = new List<AgentExternalDTO>();
            using (var response = await httpClient.GetAsync(requestUrl))
            {
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();

                    var resultAuths = JsonConvert.DeserializeObject<AuthBCResult>(data);

                    var agentOwnerType = filter.AgentOwner;


                    var requestagentListUrl = "/agent/v1/agent-list?AgentOwner=" + agentOwnerType;

                    var settings = new JsonSerializerSettings() { DateFormatString = "yyyy-MM-dd hh:mm:ss" };

                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", resultAuths.access_token);

                    using (var responseagentList = await httpClient.GetAsync(requestagentListUrl))
                    {
                        if (responseagentList.IsSuccessStatusCode)
                        {
                            var contentseagent = await responseagentList.Content.ReadAsStringAsync();

                            var resultdata = JsonConvert.DeserializeObject<List<AgentExternalDTO>>(contentseagent);

                            var results = resultdata.Select(o => AgentExternalDTO.CreateFromQueryResult(o)).ToList();

                            #region filter
                            //var predicate = PredicateBuilder.New<AgentExternalDTO>(true);

                            //if (filter.AgentID != null)
                            //{
                            //    predicate = predicate.And(x => x.agent_id != filter.AgentID);
                            //}
                            //if (!string.IsNullOrEmpty(filter.IDCard))
                            //{
                            //    predicate = predicate.And(x => (x.id_card ?? "").ToLower().Contains(filter.IDCard.ToLower()));
                            //    predicate = predicate.And(x => (x.tax_id ?? "").ToLower().Contains(filter.IDCard.ToLower()));
                            //}
                            //if (!string.IsNullOrEmpty(filter.TaxID))
                            //{
                            //    predicate = predicate.And(x => (x.id_card ?? "").ToLower().Contains(filter.TaxID.ToLower()));
                            //    predicate = predicate.And(x => (x.tax_id ?? "").ToLower().Contains(filter.TaxID.ToLower()));
                            //}
                            //if (!string.IsNullOrEmpty(filter.PassportNo))
                            //{
                            //    predicate = predicate.And(x => (x.passport_no ?? "").ToLower().Contains(filter.PassportNo.ToLower()));
                            //}
                            #endregion

                            //results = results.Where(predicate).ToList();
                            resultList = results;
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
            return resultList;
        }

        public async Task<UserAuthBGDTO> CheckAuthUserBGAsync(Guid? userID, CancellationToken cancellationToken = default)
        {
            //--bg 1 sdh บ้านเดี่ยว
            //--th 2 th ทาวเฮ้า
            //--cd 3 cd คอนโด
            bool isSDHbyUser = false;
            bool isTHbyUser = false;
            bool isCDbyUser = false;

            var ur = await DB.UserAuthorizeProjects
                      .Include(o => o.Project)
                               .ThenInclude(o => o.BG)
                      .Include(o => o.User).Where(o => o.UserID == userID && o.Project.IsActive).GroupBy(o => o.Project.BG.BGNo).ToListAsync();

            if (ur.Count == 1)
            {
                var ura = await DB.UserAuthorizeProjects
                                   .Include(o => o.Project)
                                            .ThenInclude(o => o.BG)
                                   .Include(o => o.User).Where(o => o.UserID == userID).FirstOrDefaultAsync();

                if (ura.Project?.BG?.BGNo == "1")
                {
                    isSDHbyUser = true;
                }
                else if (ura.Project?.BG?.BGNo == "2")
                {
                    isTHbyUser = true;
                }
                else if (ura.Project?.BG?.BGNo == "3")
                {
                    isCDbyUser = true;
                }

            }

            return new UserAuthBGDTO()
            {
                isSDH = isSDHbyUser,
                isTH = isTHbyUser,
                isCD = isCDbyUser
            };
        }
    }
}
