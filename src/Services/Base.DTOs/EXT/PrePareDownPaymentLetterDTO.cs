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
    public class PrePareDownPaymentLetterDTO
    {
        /// <summary>
        /// ID
        /// </summary>
        [Description("ProjectID")]
        public Guid? ProjectID { get; set; }

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


        public void ValidateLead(ref List<PrePareDownPaymentLetterDTO> result, ref PrePareDownPaymentLetterDTO project, string status, string description)
        {
            project.Status = status;
            project.Description = description;

            result.Add(project);
        }
    }
}
