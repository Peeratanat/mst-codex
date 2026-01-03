using Base.DTOs.PRJ;
using Base.DTOs.SAL.Sortings;
using Base.DTOs.SAL;
using Database.Models;
using Database.Models.MST;
using Database.Models.PRJ;
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
using models = Database.Models;
using Base.DTOs.USR; 
using static Base.DTOs.MST.LetterOfGuaranteeDTO;

namespace Base.DTOs.MST
{
    public class AnswerFAQDTO : BaseDTO
    {
        public Guid ID { get; set; }
        public string AnswerDesc { get; set; }
        public int? Order { get; set; }
        public string Remark { get; set; }
        public bool? IsSubmit { get; set; }


        public static AnswerFAQDTO CreateFromModel(AnswerFAQ model)
        {
            if (model != null)
            {
                
                var result = new AnswerFAQDTO()
                {
                    ID = model.ID,
                    AnswerDesc = model.AnswerDesc,
                    Order = model.Order,
                    Remark = model.Remark
                };
                return result;
            }
            else
            {
                return null;
            }
        }

       
    }
}
