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

    public class AgentEmployeeExternalDTO
    {
        public Guid? agent_sale_id { get; set; }
        public Guid? agent_id { get; set; }
        public string prefix_name_th { get; set; }
        public string first_name_th { get; set; }
        public string last_name_th { get; set; }
        public string create_by { get; set; }
        public DateTime? create_date { get; set; }
        public string modify_by { get; set; }
        public DateTime? modify_date { get; set; }

        public static AgentEmployeeExternalDTO CreateFromQueryResult(AgentEmployeeExternalDTO model)
        {
            if (model != null)
            {
                var result = new AgentEmployeeExternalDTO()
                {
                    agent_sale_id = model.agent_sale_id,
                    agent_id = model.agent_id,
                    prefix_name_th = model.prefix_name_th,
                    first_name_th = model.first_name_th,
                    last_name_th = model.last_name_th,
                    create_by = model.create_by,
                    create_date = model.create_date,
                    modify_by = model.modify_by,
                    modify_date = model.modify_date

                };

                return result;
            }
            else
            {
                return null;
            }
        }


        public static void SortBy(AgentExternalSortByParam sortByParam, ref List<AgentEmployeeExternalDTO> datas)
        {
            if (sortByParam.SortBy != null)
            {
                switch (sortByParam.SortBy.Value)
                {
                    case AgentExternalSortBy.first_name_th:
                        if (sortByParam.Ascending) datas = datas.OrderBy(o => o.first_name_th).ToList();
                        else datas = datas.OrderByDescending(o => o.first_name_th).ToList();
                        break;
                    case AgentExternalSortBy.last_name_th:
                        if (sortByParam.Ascending) datas = datas.OrderBy(o => o.last_name_th).ToList();
                        else datas = datas.OrderByDescending(o => o.last_name_th).ToList();
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

    public class AgentEmployeeExternalQueryResult
    {
        public Guid? agent_sale_id { get; set; }
        public Guid? agent_id { get; set; }
        public string prefix_name_th { get; set; }
        public string first_name_th { get; set; }
        public string last_name_th { get; set; }
    }
}

