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
    public class AgentEmployeeDTO : BaseDTO
    {
        /// <summary>
        /// Agent
        /// </summary>
        public Guid? AgentID { get; set; }
        /// <summary>
        /// ชื่อ
        /// </summary>
        [Description("ชื่อ")]
        public string FirstName { get; set; }
        /// <summary>
        /// นามสกุล
        /// </summary>
        [Description("นามสกุล")]
        public string LastName { get; set; }
        /// <summary>
        /// เบอร์โทรศัพท์
        /// </summary>
        public string TelNo { get; set; }
        public static AgentEmployeeDTO CreateFromModel(AgentEmployee model)
        {
            if (model != null)
            {
                var result = new AgentEmployeeDTO()
                {
                    Id = model.ID,
                    FirstName = model.FirstName,
                    LastName = model.LastName,              
                    TelNo = model.TelNo,
                    Updated = model.Updated,
                    UpdatedBy = model.UpdatedBy?.DisplayName,
                    AgentID = model.AgentID
                };
                return result;
            }
            else
            {
                return null;
            }
        }

        public static AgentEmployeeDTO CreateFromQueryResult(AgentEmployeeQueryResult model)
        {
            if (model != null)
            {
                var result = new AgentEmployeeDTO()
                {
                    Id = model.AgentEmployee.ID,
                    FirstName = model.AgentEmployee.FirstName,
                    LastName = model.AgentEmployee.LastName,
                    TelNo = model.AgentEmployee.TelNo,
                    Updated = model.AgentEmployee.Updated,
                    UpdatedBy = model.UpdatedBy?.DisplayName,
                    AgentID = model.AgentEmployee.AgentID
                };

                return result;
            }
            else
            {
                return null;
            }
        }

        public static void SortBy(AgentEmployeeSortByParam sortByParam, ref IQueryable<AgentEmployeeQueryResult> query)
        {
            if (sortByParam.SortBy != null)
            {
                switch (sortByParam.SortBy.Value)
                {
                    case AgentEmployeeSortBy.FirstName:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.AgentEmployee.FirstName);
                        else query = query.OrderByDescending(o => o.AgentEmployee.FirstName);
                        break;
                    case AgentEmployeeSortBy.LastName:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.AgentEmployee.LastName);
                        else query = query.OrderByDescending(o => o.AgentEmployee.LastName);
                        break;
                    case AgentEmployeeSortBy.TelNo:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.AgentEmployee.TelNo);
                        else query = query.OrderByDescending(o => o.AgentEmployee.TelNo);
                        break;
                    case AgentEmployeeSortBy.Updated:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.AgentEmployee.Updated);
                        else query = query.OrderByDescending(o => o.AgentEmployee.Updated);
                        break;
                    case AgentEmployeeSortBy.UpdatedBy:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.UpdatedBy.DisplayName);
                        else query = query.OrderByDescending(o => o.UpdatedBy.DisplayName);
                        break;
                    default:
                        query = query.OrderBy(o => o.AgentEmployee.FirstName);
                        break;
                }
            }
            else
            {
                query = query.OrderBy(o => o.AgentEmployee.FirstName);
            }
        }

        public async Task ValidateAsync(DatabaseContext db)
        {
            ValidateException ex = new ValidateException();
            if (string.IsNullOrEmpty(this.FirstName))
            {
                var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                string desc = this.GetType().GetProperty(nameof(AgentEmployeeDTO.FirstName)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = errMsg.Message.Replace("[field]", desc);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }
            var checkUniqueFullName = this.Id != (Guid?)null ? await db.AgentEmployees.Where(o => o.FirstName.ToLower() + o.LastName.ToLower() == this.FirstName.ToLower()+this.LastName.ToLower() && o.ID != this.Id).CountAsync() > 0 : await db.AgentEmployees.Where(o => o.FirstName.ToLower() + o.LastName.ToLower() == this.FirstName.ToLower() + this.LastName.ToLower()).CountAsync() > 0;
            if (checkUniqueFullName)
            {
                var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0042").FirstAsync();
                string desc = this.GetType().GetProperty(nameof(AgentEmployeeDTO.FirstName)).GetCustomAttribute<DescriptionAttribute>().Description+" "+ this.GetType().GetProperty(nameof(AgentEmployeeDTO.LastName)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = errMsg.Message.Replace("[field]", desc);
                msg = msg.Replace("[value]", this.FirstName+" "+this.LastName);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }
            if (ex.HasError)
            {
                throw ex;
            }
        }

        public void ToModel(ref AgentEmployee model)
        {
            model.FirstName = this.FirstName;
            model.LastName = this.LastName;
            model.TelNo = this.TelNo;
            model.AgentID = this.AgentID;
        }
    }

    public class AgentEmployeeQueryResult
    {
        public AgentEmployee AgentEmployee { get; set; }
        public User UpdatedBy { get; set; }
    }
}
