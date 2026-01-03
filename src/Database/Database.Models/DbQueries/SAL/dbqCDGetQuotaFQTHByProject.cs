using Base.DbQueries;
using Database.Models.PRJ;
using System;
using System.Collections.Generic;
using System.Text;

namespace Database.Models.DbQueries.SAL
{
    public class dbqCDGetQuotaFQTHByProject
    {


        public string ProjectNo { get; set; }

        public string ProjectName { get; set; }

        public int IsQuotaFQTQ { get; set; }

        public int FQ_Aval_Unit { get; set; }

        public double FQ_Aval_Area { get; set; }

        public double FQ_Aval_Per { get; set; }

        public int FQ_Sold_Unit { get; set; }

        public double FQ_Sold_Area { get; set; }

        public double FQ_Sold_Per { get; set; }

        public double FQ_Total_Per { get; set; }

        public int TH_Aval_Unit { get; set; }

        public double TH_Aval_Area { get; set; }

        public double TH_Aval_Per { get; set; }

        public int TH_Sold_Unit { get; set; }

        public double TH_Sold_Area { get; set; }

        public double TH_Sold_Per { get; set; }

        public double TH_Total_Per { get; set; }


        //SELECT @ProjectNo AS ProjectNo,
        //@ProjectName AS ProjectName,
        //CASE WHEN @i_proj_fqtq_active > 0 THEN 1 ELSE 0 END AS IsQuotaFQTQ, --มีการกำหนด โควต้า FQ/TQ อยู่หรือไม่

        //@FQ_Aval_Unit AS  FQ_Aval_Unit,			--FQ จำนวนห้องว่างโควต้า ต่างชาติ
        //@FQ_Aval_Area   AS FQ_Aval_Area,           --FQ พื้นที่ว่างโควต้า ต่างชาติ
        //@FQ_Aval_Per    AS FQ_Aval_Per,            --FQ % โควต้า ต่างชาติ ที่ว่าง
        //@FQ_Sold_Unit   AS FQ_Sold_Unit,           --FQ จำนวนห้องที่ขายได้โควต้า ต่างชาติ
        //@FQ_Sold_Area   AS FQ_Sold_Area,           --FQ พื้นที่ที่ขายได้โควต้า ต่างชาติ
        //@FQ_Sold_Per    AS FQ_Sold_Per,            --FQ % โควต้า ต่างชาติ ที่ขายได้
        //@FQ_Total_Per   AS FQ_Total_Per,           --FQ % สัดส่วนทั้งหมด โควต้า ต่างชาติ
        //@TH_Aval_Unit   AS TH_Aval_Unit,           --TH จำนวนห้องว่างโควต้า คนไทย
        //@TH_Aval_Area   AS TH_Aval_Area,           --TH พื้นที่ว่างโควต้า คนไทย
        //@TH_Aval_Per    AS TH_Aval_Per,            --TH % โควต้า คนไทย ที่ว่าง
        //@TH_Sold_Unit   AS TH_Sold_Unit,           --TH จำนวนห้องที่ขายได้โควต้า คนไทย
        //@TH_Sold_Area   AS TH_Sold_Area,           --TH พื้นที่ที่ขายได้โควต้า คนไทย
        //@TH_Sold_Per    AS TH_Sold_Per,            --TH % โควต้า คนไทย ที่ขายได้
        //@TH_Total_Per   AS TH_Total_Per			--TH % สัดส่วนทั้งหมด โควต้า คนไทย

    }
}
