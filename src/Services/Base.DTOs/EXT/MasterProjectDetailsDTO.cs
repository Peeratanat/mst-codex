using Database.Models;
using Database.Models.DbQueries.EXT;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using models = Database.Models;

namespace Base.DTOs.EXT
{
    public class MasterProjectDetailsDTO
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

        public void ToModel(ref dbqGetProjectDetailList model)
        {

            model.project_code = this.project_code;
            model.project_name_th = this.project_name_th;
            model.project_name_en = this.project_name_en;
            model.dimension = this.dimension;
            model.status_juristic = this.status_juristic;
            model.vat = this.vat;
            model.project_address = this.project_address;
            model.project_subdistrict = this.project_subdistrict;
            model.project_district = this.project_district;
            model.project_province = this.project_province;
            model.project_postalcode = this.project_postalcode;
            model.project_water_tariff_rate = this.project_water_tariff_rate;
            model.datetransfer_facility = this.datetransfer_facility;
            model.telephone = this.telephone;
            model.fax = this.fax;
            model.email = this.email;
            model.tax = this.tax;
            model.unit = this.unit;
            model.shop = this.shop;
            model.common_fee = this.common_fee;
            model.cal_common_fee = this.cal_common_fee;
            model.common_fee_round = this.common_fee_round;
            model.sale_area = this.sale_area;
            model.date_establishment = this.date_establishment;
            model.register_number = this.register_number;
            model.update_at = this.update_at;
            model.start_date = this.start_date;
            model.brand = this.brand;

        }

        public static MasterProjectDetailsDTO CreateFromQuery(dbqGetProjectDetailList model, DatabaseContext db)
        {
            if (model != null)
            {
                var result = new MasterProjectDetailsDTO();
                result.project_code = model.project_code;
                result.project_name_th = model.project_name_th;
                result.project_name_en = model.project_name_en;
                result.dimension = model.dimension;
                result.status_juristic = model.status_juristic;
                result.vat = model.vat;
                result.project_address = model.project_address;
                result.project_subdistrict = model.project_subdistrict;
                result.project_district = model.project_district;
                result.project_province = model.project_province;
                result.project_postalcode = model.project_postalcode;
                result.project_water_tariff_rate = model.project_water_tariff_rate;
                result.datetransfer_facility = model.datetransfer_facility;
                result.telephone = model.telephone;
                result.fax = model.fax;
                result.email = model.email;
                result.tax = model.tax;
                result.unit = model.unit;
                result.shop = model.shop;
                result.common_fee = model.common_fee;
                result.cal_common_fee = model.cal_common_fee;
                result.common_fee_round = model.common_fee_round;
                result.sale_area = model.sale_area;
                result.date_establishment = model.date_establishment;
                result.register_number = model.register_number;
                result.update_at = model.update_at;
                result.start_date = model.start_date;
                result.brand = model.brand;
                result.project_status = model.project_status;

                result.FirstUnitTransferDate = model.FirstUnitTransferDate;
                result.LasttUnitTransferDate = model.LasttUnitTransferDate;
                result.ActualTransferCount = model.ActualTransferCount;
                result.AllArea = model.AllArea;
                result.ShopArea = model.ShopArea;

                return result;
            }
            else
            {
                return null;
            }
        }

    }
}
