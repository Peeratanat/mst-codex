using Database.Models;
using Database.Models.DbQueries.EXT;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using models = Database.Models;

namespace Base.DTOs.EXT
{
    public class UnitInfoDTO
    {
        public string ProjectNo { get; set; }
        public string ProjectName { get; set; }
        public string UnitNo { get; set; }
        public string ContactNo { get; set; }
        public string MainOwnerName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? TransferDate { get; set; }
        public string Mobile { get; set; }
        public string SellerID { get; set; }
        public string SellerFullName { get; set; }
        public string ContractNumber { get; set; }
        public string BookingNumber { get; set; }


        public void ToModel(ref dbqGetUnitInfo model)
        {
            model.ProjectNo = this.ProjectNo;
            model.ProjectName = this.ProjectName;
            model.UnitNo = this.UnitNo;
            model.ContactNo = this.MainOwnerName;
            model.MainOwnerName = this.MainOwnerName;
            model.FirstName = this.FirstName;
            model.LastName = this.LastName;
            model.TransferDate = this.TransferDate;
            model.Mobile = this.Mobile;
            model.SellerID = this.SellerID;
            model.SellerFullName = this.SellerFullName;
            model.ContractNumber = this.ContractNumber;
            model.BookingNumber = this.BookingNumber;

        }

        public static UnitInfoDTO CreateFromQuery(dbqGetUnitInfo model, DatabaseContext db)
        {
            if (model != null)
            {
                var result = new UnitInfoDTO();
                result.ProjectNo = model.ProjectNo;
                result.ProjectName = model.ProjectName;
                result.UnitNo = model.UnitNo;
                result.ContactNo = model.ContactNo;
                result.MainOwnerName = model.MainOwnerName;
                result.FirstName = model.FirstName;
                result.LastName = model.LastName;
                result.TransferDate = model.TransferDate;
                result.Mobile = model.Mobile;
                result.SellerID = model.SellerID;
                result.SellerFullName = model.SellerFullName;
                result.ContractNumber = model.ContractNumber;
                result.BookingNumber = model.BookingNumber;


                return result;
            }
            else
            {
                return null;
            }
        }

    }
}
