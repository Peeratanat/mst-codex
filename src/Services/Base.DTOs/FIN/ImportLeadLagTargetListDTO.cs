using Base.DTOs.USR;
using System; 
using Database.Models.PRJ;
using Base.DTOs.PRJ;
using Database.Models.BI;

namespace Base.DTOs.FIN
{
    public class ImportLeadLagTargetListDTO : BaseDTO
    {
        public int No { get; set; } 
        public string RecType { get; set; } 
        public string TypeName { get; set; } 
        public int? Y { get; set; }
        public int? Q { get; set; }
        public int? M { get; set; }
        public int? W { get; set; }
        public ProjectDTO Project { get; set; }
        public decimal? Amount { get; set; }
        public int? Unit { get; set; } 
        public string Description { get; set; }
        public bool IsError { get; set; }

        public void ToModel(ref Mst_LeadIndicator_Target model, Guid? userID = null)
        {
            model.ProjectID = this.Project?.Id ?? Guid.Empty; 
            model.Y = this.Y ?? 0;
            model.Q = this.Q ?? 0;
            model.M = this.M ?? 0;
            model.W = this.W ?? 0;
            model.Amount = this.Amount ?? 0;
            model.Unit = this.Unit ?? 0;
            model.RecType = this.RecType;
            //model.Created = DateTime.Now;
            //model.Updated = DateTime.Now;
            //if(userID != null)
            //{
            //    model.UpdatedByUserID = userID;
            //    model.CreatedByUserID = userID;
            //}
        }

        public static string GetTypeName(string type)
        {
            string value = "";
            switch (type.ToLower().Replace(" ", ""))
            {
                case "incentive": value = "Incentive"; break;
                case "projection": value = "Projection"; break;
                case "presales": value = "Presales"; break;
                case "qcpass": value = "ห้องผ่านqc"; break;
                case "defect": value = "ลูกค้าเข้าตรวจ"; break;
                case "getroom": value = "ลูกค้ารับห้อง"; break;
                case "bankapprove": value = "โอนสด + Bankอนุมัติ"; break;
                case "walk": value = "Walk"; break;
                case "revisit": value = "2nd walk++"; break;
                case "defect_actual": value = "Actual ลูกค้าเข้าตรวจจริง"; break;
                case "getroom_actual": value = "Actual ลูกค้ารับห้องจริง"; break;
                case "bc_target": value = "Target BC"; break;
                case "bc_actual": value = "Actual BC"; break;
                default: value = "error"; break;
            }
            return value;
        }

        public static string GetType(string type)
        {
            string value = "";
            switch (type.ToLower().Replace(" ", ""))
            {
                case "incentive": value = "Incentive"; break;
                case "projection": value = "Projection"; break;
                case "presales": value = "Presales"; break;
                case "ห้องผ่านqc": value = "QCpass"; break;
                case "ลูกค้าเข้าตรวจ": value = "Defect"; break;
                case "ลูกค้ารับห้อง": value = "Getroom"; break;
                case "โอนสด+bankอนุมัติ": value = "BankApprove"; break;
                case "walk": value = "Walk"; break;
                case "2ndwalk++": value = "Revisit"; break;
                case "actualลูกค้าเข้าตรวจจริง": value = "Defect_Actual"; break;
                case "actualลูกค้ารับห้องจริง": value = "Getroom_Actual"; break;
                case "targetbc": value = "BC_Target"; break;
                case "actualbc": value = "BC_Actual"; break;
                default: value = "error"; break;
            }
            return value;
        }
    }
     
}
