using Database.Models.PRJ;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Base.DTOs.PRJ
{
    /// <summary>
    /// Model: BudgetMinPrice
    /// </summary>
    public class BudgetMinPriceDTO
    {
        public Guid? ID { get; set; }

        /// <summary>
        /// โครงการ
        /// </summary>
        public ProjectDropdownDTO Project { get; set; }
        /// <summary>
        /// ปี
        /// </summary>
        [Description("ปี")]
        public int Year { get; set; }
        /// <summary>
        /// ควอเตอร์
        /// </summary>
        [Description("ควอเตอร์")]
        public int Quarter { get; set; }
        /// <summary>
        /// Total Budget Sale
        /// </summary>
        public decimal QuarterlyTotalAmount { get; set; }
        /// <summary>
        /// Total Budget Transfer
        /// </summary>
        public decimal TransferTotalAmount { get; set; }
        /// <summary>
        /// Old Total Budget Transfer
        /// </summary>
        public decimal OldTransferTotalAmount { get; set; }
        /// <summary>
        /// Budget Transfer/Unit
        /// </summary>
        public decimal TransferTotalUnit { get; set; }
        /// <summary>
        /// Old Budget Transfer/Unit
        /// </summary>
        public decimal OldTransferTotalUnit { get; set; }
        /// <summary>
        /// ข้อมูลถูกต้อง
        /// </summary>
        public bool isCorrected { get; set; }

        /// <summary>
        /// จำนวนข้อมูลที่ถูก
        /// </summary>
        public int TotalSuccess { get; set; }
        /// <summary>
        /// จำนวนข้อมูลที่ผิด
        /// </summary>
        public int TotalError { get; set; }

        /// <summary>
        /// แก้ไขโดย
        /// </summary>
        public string UpdatedBy { get; set; }
        /// <summary>
        /// วันที่แก้ไข
        /// </summary>
        public DateTime? Updated { get; set; }

        /// <summary>
        /// แก้ไขโดย
        /// </summary>
        public string Remark { get; set; }


        public static BudgetMinPriceDTO CreateFromQueryResult(BudgetMinPriceQueryResult model, int? Year = null, int? Quarter = null)
        {
            if (model != null)
            {
                var result = new BudgetMinPriceDTO()
                {
                    ID = model.BudgetMinPriceQuarterly?.ID,
                    Project = ProjectDropdownDTO.CreateFromModel(model.Project),
                    Year = model.BudgetMinPrice.Year,
                    Quarter = model.BudgetMinPrice.Quarter,
                    QuarterlyTotalAmount = model.BudgetMinPriceQuarterly?.TotalAmount ?? 0,
                    TransferTotalAmount = model.BudgetMinPriceTransfer?.TotalAmount ?? 0,
                    TransferTotalUnit = model.BudgetMinPriceTransfer?.UnitAmount ?? 0,
                    Updated = model.BudgetMinPrice.Updated,
                    UpdatedBy = model.BudgetMinPrice.UpdatedBy?.DisplayName
                };
                if (result.Year == 0 && Year != null)
                    result.Year = Year ?? 0;
                if (result.Quarter == 0 && Quarter != null)
                    result.Quarter = Quarter ?? 0;
                return result;
            }
            else
            {
                return null;
            }
        }

        public void ToModelQuarterly(ref BudgetMinPrice model)
        {
            model.ProjectID = this.Project.Id.Value;
            model.Year = this.Year;
            model.Quarter = this.Quarter;
            model.ActiveDate = DateTime.Now;
            model.TotalAmount = this.QuarterlyTotalAmount;
        }

        public void ToModelTransfer(ref BudgetMinPrice model)
        {
            model.ProjectID = this.Project.Id.Value;
            model.Year = this.Year;
            model.Quarter = this.Quarter;
            model.ActiveDate = DateTime.Now;
            model.UnitAmount = this.TransferTotalUnit;
            model.TotalAmount = this.TransferTotalAmount;
        }
    }
    public class BudgetMinPriceQueryResult
    {
        public BudgetMinPrice BudgetMinPrice { get; set; }
        public Project Project { get; set; }
        public BudgetMinPrice BudgetMinPriceTransfer { get; set; }
        public BudgetMinPrice BudgetMinPriceQuarterly { get; set; }
    }

}
