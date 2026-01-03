using Base.DbQueries;
using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;

namespace Database.Models.DbQueries.EXT
{
    public class dbqGetProjectDetailList
    {
        public string project_code { get; set; }
        public string project_name_th { get; set; }
        public string project_name_en { get; set; }
        public string dimension { get; set; }
        public string status_juristic { get; set; }
        public string vat { get; set; }
        public string project_address { get; set; }
        public string project_subdistrict { get; set; }
        public string project_district { get; set; }
        public string project_province { get; set; }
        public string project_postalcode { get; set; }
        public string project_water_tariff_rate { get; set; }
        public string datetransfer_facility { get; set; }
        public string telephone { get; set; }
        public string fax { get; set; }
        public string email { get; set; }
        public string tax { get; set; }
        public string unit { get; set; }
        public string shop { get; set; }
        public string common_fee { get; set; }
        public string cal_common_fee { get; set; }
        public string common_fee_round { get; set; }
        public double sale_area { get; set; }
        public string date_establishment { get; set; }
        public string register_number { get; set; }
        public string update_at { get; set; }
        public string start_date { get; set; }
        public string brand { get; set; }
        public string project_status { get; set; }

        public DateTime? FirstUnitTransferDate { get; set; }
        public DateTime? LasttUnitTransferDate { get; set; }
        public int ActualTransferCount { get; set; }
        public double AllArea { get; set; }
        public double ShopArea { get; set; }
    }
}


