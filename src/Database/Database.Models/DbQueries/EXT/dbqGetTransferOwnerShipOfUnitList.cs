using Base.DbQueries;
using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;

namespace Database.Models.DbQueries.EXT
{
    public class dbqGetTransferOwnerShipOfUnitList
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

    }
}


