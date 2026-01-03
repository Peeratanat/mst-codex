using Database.Models;
using Database.Models.DbQueries.EQN;
using Database.Models.MasterKeys;
using Database.Models.PRJ;
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
	public class CancleMemoListDTO
    {

        public Guid? ID { get; set; }
        public string ProjectNo { get; set; }
        public string ProjectNameTH { get; set; }
        public string ProjectDisplayName { get; set; }
        public string UnitNo { get; set; }
        public string HouseNo { get; set; }
        public string MainOwnerName { get; set; }
        public string AgreementNo { get; set; }
        public Guid? AgreementID { get; set; }



        public static CancleMemoListDTO CreateFromModel(CancelMemo model)
        {
            if (model != null)
            {
                var result = new CancleMemoListDTO()
                {
                    ID = model.ID,
                    ProjectNo = model.Agreement.Project.ProjectNo,
                    ProjectNameTH = model.Agreement.Project.ProjectNameTH,
                    ProjectDisplayName = model.Agreement.Project.ProjectNo + "-" + model.Agreement.Project.ProjectNameTH,
                    UnitNo = model.Agreement.Unit.UnitNo,
                    HouseNo = model.Agreement.Unit.HouseNo,
                    MainOwnerName = model.Agreement.MainOwnerName,
                    AgreementNo = model.Agreement.AgreementNo,
                    AgreementID= model.Agreement.ID
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
