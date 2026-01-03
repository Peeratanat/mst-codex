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
	public class PostponeDetailDTO
    {
        public Guid? Id { get; set; }
        public string ProjectNo { get; set; }
        public string ProjectNameTH { get; set; }
        public string ProjectDisplayName { get; set; }
        public string UnitNo { get; set; }
        public DateTime? PostponeDate { get; set; }
        public string Remark { get; set; }
        public string UpdatedBy { get; set; }
        public string CreateBy { get; set; }
        public Guid AgreementID { get; set; }
        public DateTime? Createdate { get; set; }
        public DateTime? Updatedate { get; set; }


        public static PostponeDetailDTO CreateFromModel(PostponeTransfer model , DatabaseContext db)
        {
            if (model != null)
            {
                var result = new PostponeDetailDTO()
                {
                    Id = model.ID,
                    AgreementID = model.Agreement.ID,
                    ProjectNo = model.Agreement.Project.ProjectNo,
                    ProjectNameTH = model.Agreement.Project.ProjectNameTH,
                    ProjectDisplayName = model.Agreement.Project.ProjectNo + "-" + model.Agreement.Project.ProjectNameTH,
                    UnitNo = model.Agreement.Unit.UnitNo,
                    PostponeDate = model.PostponeTransferDate,
                    Remark = model.Remark,
                    UpdatedBy = db.Users.Where(o => o.ID == model.UpdatedByUserID).Select(o => o.DisplayName).FirstOrDefault(),
                    CreateBy = db.Users.Where(o => o.ID == model.CreatedByUserID).Select(o => o.DisplayName).FirstOrDefault(),
                    Createdate = model.Created,
                    Updatedate =  model.Updated,

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
