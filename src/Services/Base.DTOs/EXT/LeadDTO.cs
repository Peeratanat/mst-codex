using Database.Models.CTM;
using ErrorHandling;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using models = Database.Models;

namespace Base.DTOs.EXT
{
    public class LeadDTO
    {
        /// <summary>
        /// ID
        /// </summary>
        [Description("ID")]
        public Guid? Id { get; set; }
        /// <summary>
        /// Ref.ID Source
        /// </summary>
        [Description("Ref.ID Source")]
        public string LeadsID { get; set; }
        /// <summary>
        /// RefLeadID
        /// </summary>
        [Description("Ref.LeadID")]
        public string RefLeadID { get; set; }
        /// <summary>
        /// สถานะ Success, Error
        /// </summary>
        [Description("Status")]
        public string Status { get; set; }
        /// <summary>
        /// Description
        /// </summary>
        [Description("Description")]
        public string Description { get; set; }


        public void ValidateLead(ref List<LeadDTO> result, ref LeadDTO lead, string status, string description)
        {
            lead.Status = status;
            lead.Description = description;

            result.Add(lead);
        }
    }
}
