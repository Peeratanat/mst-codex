using Database.Models;
using Database.Models.DbQueries.EQN;
using Database.Models.MasterKeys;
using Database.Models.PRJ;
using Database.Models.PRM;
using Database.Models.SAL;
using Database.Models.USR;
using ErrorHandling;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using models = Database.Models;

namespace Base.DTOs.EXT
{
	public class ChangeUnitAgreementListDTO
    {

        public Guid? ChangeUnitID { get; set; }
        public string UnitNoOld { get; set; }
        public string UnitNoNew { get; set; }
        public string MainOwnerName { get; set; }
        public string AgreementNo { get; set; }
        public DateTime? CreatedDate { get; set; }




        public static ChangeUnitAgreementListDTO CreateFromModel(ChangeUnitWorkflow model , DatabaseContext DB)
        {
            if (model != null)
            {
                var result = new ChangeUnitAgreementListDTO()
                {
                    ChangeUnitID = model.ID,
                    UnitNoOld = model.FromAgreement.Unit.UnitNo,
                    UnitNoNew = model.ToAgreement.Unit.UnitNo,
                    MainOwnerName = model.FromAgreement.MainOwnerName,
                    AgreementNo = model.FromAgreement.AgreementNo,
                    CreatedDate = model.Created,
            
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
