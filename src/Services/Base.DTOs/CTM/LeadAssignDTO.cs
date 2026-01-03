using Base.DTOs.USR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using models = Database.Models;

namespace Base.DTOs.CTM
{
    public class LeadAssignDTO
    {
        /// <summary>
        /// รายการ Lead
        /// </summary>
        public List<LeadListDTO> Leads { get; set; }
        /// <summary>
        /// ผู้ดูแล
        /// </summary>
        public USR.UserListDTO User { get; set; }

        public static LeadAssignDTO CreateFromModel(List<models.CTM.Lead> model, models.USR.User user)
        {
            if(model != null)
            {
                LeadAssignDTO leadAssign = new LeadAssignDTO();

                List<LeadListDTO> leads = new List<LeadListDTO>();
                foreach(var item in model)
                {
                    var lead = LeadListDTO.CreateFromModel(item);
                    if (lead != null)
                        leads.Add(lead);
                }

                leadAssign.Leads = new List<LeadListDTO>();
                leadAssign.Leads.AddRange(leads);
                leadAssign.User = new UserListDTO();
                leadAssign.User = UserListDTO.CreateFromModel(user);

                return leadAssign;
            }
            else
            {
                return null;
            }
        }
    }
}
