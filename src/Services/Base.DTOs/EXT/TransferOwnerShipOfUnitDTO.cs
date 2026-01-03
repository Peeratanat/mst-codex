using Database.Models;
using Database.Models.DbQueries.EXT;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using models = Database.Models;

namespace Base.DTOs.EXT
{
    public class TransferOwnerShipOfUnitDTO
    {
        public string project_code { get; set; }
        public string projectname { get; set; }
        public string customercode { get; set; }
        public string ownername { get; set; }
        public string ownersex { get; set; }
        public string owneraddress1 { get; set; }
        public string ownersubdistrict1 { get; set; }
        public string ownerdistrict1 { get; set; }
        public string ownerprovince1 { get; set; }
        public string ownerpostalcode1 { get; set; }
        public string ownermobile1 { get; set; }
        public string owneremail { get; set; }
        public string address2 { get; set; }
        public string subdistrict2 { get; set; }
        public string district2 { get; set; }
        public string province2 { get; set; }
        public string postalcode2 { get; set; }
        public string tel2 { get; set; }
        public string address3 { get; set; }
        public string subdistrict3 { get; set; }
        public string district3 { get; set; }
        public string province3 { get; set; }
        public string postalcode3 { get; set; }
        public string tel3 { get; set; }
        public string idcardno { get; set; }
        public string dateofbirth { get; set; }
        public string residentstatus { get; set; }
        public string unit_no { get; set; }
        public string housenumber { get; set; }
        public string sourceunitcode { get; set; }
        public string building { get; set; }
        public string floor { get; set; }
        public double area { get; set; }
        public string ratio { get; set; }
        public string datetranferownership { get; set; }
        public string promotion { get; set; }
        public string startpro { get; set; }
        public string endpro { get; set; }
        public string contract_number { get; set; }
        public string contactid { get; set; }
        public string public_utility_ap { get; set; }
        public string public_utility_resident { get; set; }
        public string fund { get; set; }
        public string is_foreign { get; set; }
        public string water_meter { get; set; }
        public double costcentermount { get; set; }
        public string costcenterappay { get; set; }
        public double promotion_period { get; set; }

        public string titlename { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }

        public void ToModel(ref dbqGetTransferOwnerShipOfUnitList model)
        {

        }

        public static TransferOwnerShipOfUnitDTO CreateFromQuery(dbqGetTransferOwnerShipOfUnitList model, DatabaseContext db)
        {
            if (model != null)
            {
                var result = new TransferOwnerShipOfUnitDTO();
                result.project_code = model.project_code;
                result.projectname = model.projectname;
                result.customercode = model.customercode;
                result.ownername = model.ownername;
                result.ownersex = model.ownersex;
                result.owneraddress1 = model.owneraddress1;
                result.ownersubdistrict1 = model.ownersubdistrict1;
                result.ownerdistrict1 = model.ownerdistrict1;
                result.ownerprovince1 = model.ownerprovince1;
                result.ownerpostalcode1 = model.ownerpostalcode1;
                result.ownermobile1 = model.ownermobile1;
                result.owneremail = model.owneremail;
                result.address2 = model.address2;
                result.subdistrict2 = model.subdistrict2;
                result.district2 = model.district2;
                result.province2 = model.province2;
                result.postalcode2 = model.postalcode2;
                result.tel2 = model.tel2;
                result.address3 = model.address3;
                result.subdistrict3 = model.subdistrict3;
                result.district3 = model.district3;
                result.province3 = model.province3;
                result.postalcode3 = model.postalcode3;
                result.tel3 = model.tel3;
                result.idcardno = model.idcardno;
                result.dateofbirth = model.dateofbirth;
                result.residentstatus = model.residentstatus;
                result.unit_no = model.unit_no;
                result.housenumber = model.housenumber;
                result.sourceunitcode = model.sourceunitcode;
                result.building = model.building;
                result.floor = model.floor;
                result.area = model.area;
                result.ratio = model.ratio;
                result.datetranferownership = model.datetranferownership;
                result.promotion = model.promotion;
                result.startpro = model.startpro;
                result.endpro = model.endpro;
                result.contract_number = model.contract_number;
                result.contactid = model.contactid;
                result.public_utility_ap = model.public_utility_ap;
                result.public_utility_resident = model.public_utility_resident;
                result.fund = model.fund;
                result.is_foreign = model.is_foreign;
                result.water_meter = model.water_meter;
                result.costcentermount = model.costcentermount;
                result.costcenterappay = model.costcenterappay;
                result.promotion_period = model.promotion_period;

                result.titlename = model.titlename;
                result.firstname = model.firstname;
                result.lastname = model.lastname;

                return result;
            }
            else
            {
                return null;
            }
        }

    }
}
