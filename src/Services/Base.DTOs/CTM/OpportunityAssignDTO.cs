using Base.DTOs.USR;
using System;
using System.Collections.Generic;
using System.Text;
using models = Database.Models;

namespace Base.DTOs.CTM
{
    public class OpportunityAssignDTO
    {
        /// <summary>
        /// รายการ Opportunity
        /// </summary>
        public List<OpportunityListDTO> Opportunities { get; set; }
        /// <summary>
        /// ผู้ดูแล
        /// </summary>
        public USR.UserListDTO User { get; set; }

        public static OpportunityAssignDTO CreateFromModel(List<models.CTM.Opportunity> model, models.USR.User user, models.DatabaseContext DB)
        {
            if (model != null)
            {
                OpportunityAssignDTO opportunityAssign = new OpportunityAssignDTO();

                List<OpportunityListDTO> opportunities = new List<OpportunityListDTO>();
                foreach (var item in model)
                {
                    var opportunity = OpportunityListDTO.CreateFromModel(item, DB);
                    if (opportunity != null)
                        opportunities.Add(opportunity);
                }

                opportunityAssign.Opportunities = new List<OpportunityListDTO>();
                opportunityAssign.Opportunities.AddRange(opportunities);
                opportunityAssign.User = new UserListDTO();
                opportunityAssign.User = UserListDTO.CreateFromModel(user);

                return opportunityAssign;
            }
            else
            {
                return null;
            }
        }
    }
}
