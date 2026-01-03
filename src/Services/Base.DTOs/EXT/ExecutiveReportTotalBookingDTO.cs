using Database.Models;
using Database.Models.DbQueries.EXT;
using Database.Models.SAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using models = Database.Models;

namespace Base.DTOs.EXT
{
    public class ExecutiveReportTotalBookingDTO
    {
        public Guid? ProjectID { get; set; }
        public string ProductID { get; set; }
        public string Project { get; set; }
        public string BU { get; set; }
        public DateTime? StartSale { get; set; }
        public int? TotalUnit { get; set; }
        public decimal? TotalPrice { get; set; }
        public int? EmptyUnit { get; set; }
        public decimal? EmptyPrice { get; set; }
        public decimal? BookingTotalPrice { get; set; }
        public int? BookingTotalUnit { get; set; }
        public decimal? CancelTotalPrice { get; set; }
        public int? CancelTotalUnit { get; set; }
        public decimal? ContractPrice { get; set; }
        public decimal? TransferDiscount { get; set; }
        public decimal? AreaPrice { get; set; }
        public decimal? ExtraDiscount { get; set; }
        public decimal? TransferTotalPrice { get; set; }
        public int? TransferTotalUnit { get; set; }
        public decimal? PTDBookingTotalPrice { get; set; }
        public int? PTDBookingTotalUnit { get; set; }
        public decimal? PTDTransferTotalPrice { get; set; }
        public int? PTDTransferTotalUnit { get; set; }
        public string ProjectGroup { get; set; }

        public static ExecutiveReportTotalBookingDTO CreateFromQuery(dbqExecutiveReportTotalBooking model)
        {
            if (model != null)
            {
                var result = new ExecutiveReportTotalBookingDTO()
                {
                    ProjectID = model.ProjectID,
                    ProductID = model.ProductID,
                    Project = model.Project,
                    BU = model.BU,
                    StartSale = model.StartSale,
                    TotalUnit = model.TotalUnit,
                    TotalPrice = model.TotalPrice,
                    EmptyUnit = model.EmptyUnit,
                    EmptyPrice = model.EmptyPrice,
                    BookingTotalPrice = model.BookingTotalPrice,
                    BookingTotalUnit = model.BookingTotalUnit,
                    CancelTotalPrice = model.CancelTotalPrice,
                    CancelTotalUnit = model.CancelTotalUnit,
                    ContractPrice = model.ContractPrice,
                    TransferDiscount = model.TransferDiscount,
                    AreaPrice = model.AreaPrice,
                    ExtraDiscount = model.ExtraDiscount,
                    TransferTotalPrice = model.TransferTotalPrice,
                    TransferTotalUnit = model.TransferTotalUnit,
                    PTDBookingTotalPrice = model.PTDBookingTotalPrice,
                    PTDBookingTotalUnit = model.PTDBookingTotalUnit,
                    PTDTransferTotalPrice = model.PTDTransferTotalPrice,
                    PTDTransferTotalUnit = model.PTDTransferTotalUnit,
                    ProjectGroup = model.ProjectGroup,
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
