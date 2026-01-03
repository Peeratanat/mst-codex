using Database.Models;
using Database.Models.MST;
using Database.Models.USR;
using ErrorHandling;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Base.DTOs.MST
{
    public class AgentExternalDTO
    {
        public Guid? agent_id { get; set; }
        public bool? is_thai { get; set; }
        public int agent_type_id { get; set; }
        public string agent_type_name { get; set; }
        public string prefix_name_th { get; set; }
        public string first_name_th { get; set; }
        public string last_name_th { get; set; }
        public string prefix_name_eng { get; set; }
        public string first_name_eng { get; set; }
        public string last_name_eng { get; set; }
        public string id_card { get; set; }
        public string passport_no { get; set; }
        public string tax_id { get; set; }
        public string agent_owner { get; set; }
        public DateTime? last_activity_date { get; set; }
        public string create_by { get; set; }
        public DateTime? create_date { get; set; }
        public string modify_by { get; set; }
        public DateTime? modify_date { get; set; }
        public int? is_allow_sdh { get; set; }
        public int? is_allow_th { get; set; }
        public int? is_allow_cd { get; set; }
        public string allow_sdh { get; set; }
        public string allow_th { get; set; }
        public string allow_cd { get; set; }
        public bool? is_preview { get; set; }


        public static AgentExternalDTO CreateFromQueryResult(AgentExternalDTO model)
        {
            if (model != null)
            {
                var result = new AgentExternalDTO()
                {
                    agent_id = model.agent_id,
                    is_thai = model.is_thai,
                    agent_type_id = model.agent_type_id,
                    agent_type_name = model.agent_type_name,
                    prefix_name_th = model.prefix_name_th,
                    first_name_th = model.first_name_th,
                    last_name_th = model.last_name_th,
                    prefix_name_eng = model.prefix_name_eng,
                    first_name_eng = model.first_name_eng,
                    last_name_eng = model.last_name_eng,
                    id_card = model.id_card,
                    passport_no = model.passport_no,
                    tax_id = model.tax_id,
                    //agent_owner = model.agent_owner == "AP" ? "AP_CoAgency" : model.agent_owner,
                    agent_owner = model.agent_owner == "AP" ? model.agent_owner : "BC",
                    //agent_owner = model.agent_owner,
                    create_by = model.create_by,
                    create_date = model.create_date,
                    modify_by = model.modify_by,
                    modify_date = model.modify_date,
                    last_activity_date = model.last_activity_date,
                    is_allow_sdh = model.is_allow_sdh == 1 ? 0 : 1,
                    is_allow_th = model.is_allow_th == 1 ? 0 : 1,
                    is_allow_cd = model.is_allow_cd == 1 ? 0 : 1,
                    //allow_sdh = model.is_allow_sdh !=null && model.is_allow_sdh == 1 ? "Yes" : "No",
                    //allow_th = model.is_allow_th != null && model.is_allow_th == 1 ? "Yes" : "No",
                    //allow_cd = model.is_allow_cd != null && model.is_allow_cd == 1 ? "Yes" : "No",
                    allow_sdh = model.is_allow_sdh != null && model.is_allow_sdh == 1 ? "No" : "Yes",
                    allow_th = model.is_allow_th != null && model.is_allow_th == 1 ? "No" : "Yes",
                    allow_cd = model.is_allow_cd != null && model.is_allow_cd == 1 ? "No" : "Yes",
                    is_preview = model.agent_owner != "AP" ? true : false
                };

                return result;
            }
            else
            {
                return null;
            }
        }

        public static void SortBy(AgentExternalSortByParam sortByParam, ref List<AgentExternalDTO> datas)
        {
            if (sortByParam.SortBy != null)
            {
                switch (sortByParam.SortBy.Value)
                {
                    case AgentExternalSortBy.first_name_th:
                        if (sortByParam.Ascending) datas = datas.OrderBy(o => o.first_name_th).ToList();
                        else datas = datas.OrderByDescending(o => o.first_name_th).ToList();
                        break;
                    case AgentExternalSortBy.first_name_eng:
                        if (sortByParam.Ascending) datas = datas.OrderBy(o => o.first_name_eng).ToList();
                        else datas = datas.OrderByDescending(o => o.first_name_eng).ToList();
                        break;
                    case AgentExternalSortBy.agent_type_name:
                        if (sortByParam.Ascending) datas = datas.OrderBy(o => o.agent_type_name).ToList();
                        else datas = datas.OrderByDescending(o => o.agent_type_name).ToList();
                        break;
                    case AgentExternalSortBy.agent_owner:
                        if (sortByParam.Ascending) datas = datas.OrderBy(o => o.agent_owner).ToList();
                        else datas = datas.OrderByDescending(o => o.agent_owner).ToList();
                        break;
                    case AgentExternalSortBy.allow_sdh:
                        if (sortByParam.Ascending) datas = datas.OrderBy(o => o.allow_sdh).ToList();
                        else datas = datas.OrderByDescending(o => o.allow_sdh).ToList();
                        break;
                    case AgentExternalSortBy.allow_th:
                        if (sortByParam.Ascending) datas = datas.OrderBy(o => o.allow_th).ToList();
                        else datas = datas.OrderByDescending(o => o.allow_th).ToList();
                        break;
                    case AgentExternalSortBy.allow_cd:
                        if (sortByParam.Ascending) datas = datas.OrderBy(o => o.allow_cd).ToList();
                        else datas = datas.OrderByDescending(o => o.allow_cd).ToList();
                        break;
                    case AgentExternalSortBy.modify_by:
                        if (sortByParam.Ascending) datas = datas.OrderBy(o => o.modify_by).ToList();
                        else datas = datas.OrderByDescending(o => o.modify_by).ToList();
                        break;
                    case AgentExternalSortBy.modify_date:
                        if (sortByParam.Ascending) datas = datas.OrderBy(o => o.modify_date).ToList();
                        else datas = datas.OrderByDescending(o => o.modify_date).ToList();
                        break;
                    default:
                        datas = datas.OrderBy(o => o.first_name_th).ToList();
                        break;
                }
            }
            else
            {
                datas = datas.OrderBy(o => o.first_name_th).ToList();
            }

        }

    }

}

public class AgentExternalQueryResult
{
    public Guid agent_id { get; set; }
    public bool? is_thai { get; set; }
    public int agent_type_id { get; set; }
    public string agent_type_name { get; set; }
    public string prefix_name_th { get; set; }
    public string first_name_th { get; set; }
    public string last_name_th { get; set; }
    public string prefix_name_eng { get; set; }
    public string first_name_eng { get; set; }
    public string last_name_eng { get; set; }
    public string id_card { get; set; }
    public string passport_no { get; set; }
    public string tax_id { get; set; }
    public string agent_owner { get; set; }
    public DateTime? last_activity_date { get; set; }
}


