using Base.DTOs.PRJ;
using Database.Models;
using Database.Models.PRJ;
using ErrorHandling;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;

namespace PRJ_Budget.Services.Excels
{
    public class BudgetMinPriceTransferExcelModel
    {
        public const int _projectBGIndex = 0;
        public const int _projectSUBBGIndex = 1;
        public const int _projectNoIndex = 2;
        public const int _projectNameIndex = 3;
        public const int _yearIndex = 4;
        public const int _quarterIndex = 5;
        public const int _transferTotalAmountIndex = 6;
        public const int _transferTotalUnitIndex = 7;

        /// <summary>
        /// รหัสโครงการ
        /// </summary>
        public string ProjectNo { get; set; }
        /// <summary>
        /// รหัสโครงการ
        /// </summary>
        public string ProjectName { get; set; }
        /// <summary>
        /// ปี
        /// </summary>
        public int Year { get; set; }
        /// <summary>
        /// ควอเตอร์
        /// </summary>
        public int Quarter { get; set; }
        /// <summary>
        /// Total Budget Transfer
        /// </summary>
        public double TransferTotalAmount { get; set; }
        /// <summary>
        /// Budget Transfer/Unit
        /// </summary>
        public double TransferTotalUnit { get; set; }

        public bool isCorrected { get; set; }

        public string Remark { get; set; }

        public static BudgetMinPriceTransferExcelModel CreateFromDataRow(DataRow dr, DatabaseContext DB)
        {
            var result = new BudgetMinPriceTransferExcelModel(); 
            BudgetMinPriceDTO MsgDTO = new BudgetMinPriceDTO();
            result.ProjectNo = dr[_projectNoIndex]?.ToString();
            result.ProjectName = dr[_projectNameIndex]?.ToString();
            result.isCorrected = true;
            int year;
            if (int.TryParse(dr[_yearIndex]?.ToString(), out year))
            {
                if (year < 2000 || year > 2500)
                {
                    var errMsg = DB.ErrorMessages.Where(o => o.Key == "ERR0101").FirstOrDefault();
                    result.isCorrected = false;
                    result.Remark = !string.IsNullOrEmpty(result.Remark) ? result.Remark : errMsg.Message;
                }
                else
                {
                    result.Year = year;
                }
            }
            else
            { 
                var errMsg = DB.ErrorMessages.Where(o => o.Key == "ERR0001").FirstOrDefault();
                result.isCorrected = false;
                result.Remark = !string.IsNullOrEmpty(result.Remark) ? result.Remark : errMsg.Message.Replace("[message]", "Year")  ;
            }
            int quarter;
            if (int.TryParse(dr[_quarterIndex]?.ToString(), out quarter))
            {
                if (quarter < 1 || quarter > 4)
                { 
                    result.isCorrected = false;
                    var errMsg = DB.ErrorMessages.Where(o => o.Key == "ERR0006").FirstOrDefault();
                    result.Remark = !string.IsNullOrEmpty(result.Remark) ? result.Remark : errMsg.Message.Replace("[message]", "กรุณาระบุ Quarter 1 - 4") ;
                }
                else
                {
                    result.Quarter = quarter;
                }

            }
            else
            {
                var errMsg = DB.ErrorMessages.Where(o => o.Key == "ERR0001").FirstOrDefault();
                result.isCorrected = false;
                result.Remark = !string.IsNullOrEmpty(result.Remark) ? result.Remark : errMsg.Message.Replace("[message]", "Quarter");
            }
            if (result.Quarter != 0 && result.Year != 0)
            {
                if (CheckOldQuater( result.Year, result.Quarter))
                {
                    var errMsg = DB.ErrorMessages.Where(o => o.Key == "ERR0168").FirstOrDefault(); 
                    result.isCorrected = false;
                    result.Remark = !string.IsNullOrEmpty(result.Remark) ? result.Remark : errMsg.Message.ToString() ;
                }
            }
            double transferTotalAmount;
            string strTransferTotalAmount = dr[_transferTotalAmountIndex]?.ToString().Trim();
            if (strTransferTotalAmount == "-")
                strTransferTotalAmount = "0";
            if (double.TryParse(strTransferTotalAmount, out transferTotalAmount))
            {
                if (transferTotalAmount < 0)
                {
                    var errMsg = DB.ErrorMessages.Where(o => o.Key == "ERR0102").FirstOrDefault();
                    result.isCorrected = false;
                    result.Remark = !string.IsNullOrEmpty(result.Remark) ? result.Remark : errMsg.Message + " Transfer Total Amount";
                }
                else
                {
                    result.TransferTotalAmount = transferTotalAmount;
                }
            }
            else
            {
                var errMsg = DB.ErrorMessages.Where(o => o.Key == "ERR0102").FirstOrDefault();
                result.isCorrected = false;
                result.Remark = !string.IsNullOrEmpty(result.Remark) ? result.Remark : errMsg.Message + " Transfer Total Amount";
            }
            double transferTotalUnit;
            string strtransferTotalUnit = dr[_transferTotalUnitIndex]?.ToString().Trim();
            if (strtransferTotalUnit == "-")
                strtransferTotalUnit = "0";
            if (double.TryParse(strtransferTotalUnit, out transferTotalUnit))
            {
                if (transferTotalAmount < 0)
                {
                    var errMsg = DB.ErrorMessages.Where(o => o.Key == "ERR0102").FirstOrDefault();
                    result.isCorrected = false;
                    result.Remark = !string.IsNullOrEmpty(result.Remark) ? result.Remark : errMsg.Message + " Transfer Total Unit";
                }
                else
                {
                    result.TransferTotalUnit = transferTotalUnit;
                }
            }
            else
            {
                var errMsg = DB.ErrorMessages.Where(o => o.Key == "ERR0102").FirstOrDefault(); 
                result.isCorrected = false;
                result.Remark = !string.IsNullOrEmpty(result.Remark) ? result.Remark : errMsg.Message + " Transfer Total Unit" ;
            }
            if (result.TransferTotalUnit > result.TransferTotalAmount)
            {
                result.isCorrected = false;
                result.Remark = !string.IsNullOrEmpty(result.Remark) ? result.Remark : "Budget/Unit ต้องห้ามมากกว่า Total Transfer Budget";
            }
            return result;
        }
        public static bool CheckOldQuater(int? year, int? Quater)
        {
            bool result = false;
            var today = DateTime.Now;
            var inputQuater = int.Parse((year ?? 0).ToString() + (Quater ?? 0).ToString());
            int QuaterNow = 0;
            if (today.Month >= 10)
            {
                QuaterNow = 4;
            }
            else if (today.Month >= 7)
            {
                QuaterNow = 3;
            }
            else if (today.Month >= 4)
            {
                QuaterNow = 2;
            }
            else
            {
                QuaterNow = 1;
            }
            var NowQuater = int.Parse(today.Year.ToString() + QuaterNow.ToString());
            if (inputQuater < NowQuater)
            {
                result = true;
            }
            return result;
        }

    }
}
