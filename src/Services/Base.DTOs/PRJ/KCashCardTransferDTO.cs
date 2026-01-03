using Base.DTOs.USR;
using Database.Models;
using Database.Models.MST;
using Database.Models.PRJ;
using ErrorHandling;
using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Base.DTOs.PRJ
{
    public class KCashCardTransferDTO : BaseDTO
    {
        public Guid ID { get; set; }
        public int Seqn_No { get; set; }
        public UserListDTO User { get; set; }
        public bool? IsKCashCard { get; set; }
        public Guid? UserID { get; set; }

        public static List<KCashCardTransferDTO> CreateFromModel(List<KCashCardTransfer> model)
        {
            if (model != null)
            {
                var aa = new List<KCashCardTransferDTO>();

                foreach (var item in model)
                {
                    var result = new KCashCardTransferDTO();
                    result.ID = item.ID;
                    result.Seqn_No = item.Seqn_No;
                    result.User = UserListDTO.CreateFromModel(item.ProjectOwnerByUser);
                    result.IsKCashCard = item.IsKCashCard;
                    aa.Add(result);
                }

                return aa;
            }
            else
            {
                return null;
            }
        }
    }
}
