using Database.Models.FIN;
using System;
using System.ComponentModel;

namespace Base.DTOs.FIN
{
    public class DepositHeaderDTO : BaseDTO
    {
        // <summary>
        // เลขที่นำฝาก
        // </summary>
        public string DepositNo { get; set; }

        // <summary>
        // วันที่นำฝากเงิน
        // </summary>
        public DateTime? DepositDate { get; set; }

        /// <summary>
        /// สถานะนำฝาก
        /// </summary>
        [Description("สถานะนำฝาก")]
        public bool DepositStatus { get; set; }

        // <summary>
        // บริษัท
        // </summary>
        [Description("บริษัท")]
        public MST.CompanyDTO Company { get; set; }

        // <summary>
        // บัญชีธนาคาร filter ตามข้อมูลบริษัท
        // </summary>
        [Description("บัญชีธนาคาร")]
        public MST.BankAccountDropdownDTO BankAccount { get; set; }

        ///// <summary>
        ///// ยอดรวมจำนวนเงินของการนำฝาก
        ///// </summary>
        //[Description("ยอดรวมจำนวนเงินของการนำฝาก")]
        //public decimal TotalAmount { get; set; }

        ///// <summary>
        ///// ยอดรวมค่าธรรมเนียมของการนำฝาก
        ///// </summary>
        //[Description("ยอดรวมค่าธรรมเนียมของการนำฝาก")]
        //public decimal TotalFee { get; set; }

        /// <summary>
        /// สถานะ Post GL
        /// </summary>
        [Description("สถานะ Post GL")]
        public bool IsPostGL { get; set; }

        //<summary>
        //ข้อมูล Post GL
        //สถานะ "Post GL แล้ว" = Object นี้มีค่า
        //สถานะ "ยังไม่ Post GL" = Object เป็นค่า NULL
        //</summary>
        [Description("ข้อมูล Post GL")]
        public string PostGLNo { get; set; }

        // <summary>
        // หมายเหตุ
        // </summary>
        [Description("หมายเหตุ")]
        public string Remark { get; set; }

        public static DepositHeaderDTO CreateFromDepositDetailQueryResult(DepositDetailQueryResult model)
        {
            if (model != null)
            {
                DepositHeaderDTO result = new DepositHeaderDTO()
                {
                    Id = model.DepositHeader.ID,
                    DepositNo = model.DepositHeader.DepositNo,
                    DepositDate = model.DepositHeader.DepositDate,

                    Company = MST.CompanyDTO.CreateFromModel(model.Company),
                    BankAccount = MST.BankAccountDropdownDTO.CreateFromModel(model.BankAccount),

                    DepositStatus = model.DepositHeader.DepositNo == null ? false : true,

                    //IsPostGL= model.DepositHeader.IsPostGL,
                    //PostGLNo = model.DepositHeader.PostGLNo,
                    Remark = model.DepositHeader.Remark
                };

                return result;
            }
            else
            {
                return null;
            }
        }

        public static DepositHeaderDTO CreateFromModel(DepositHeader model)
        {
            if (model != null)
            {
                DepositHeaderDTO result = new DepositHeaderDTO()
                {
                    Id = model.ID,
                    DepositNo = model.DepositNo,
                    DepositDate = model.DepositDate,
                    Company = MST.CompanyDTO.CreateFromModel(model?.BankAccount?.Company),
                    BankAccount = MST.BankAccountDropdownDTO.CreateFromModel(model?.BankAccount),
                    DepositStatus = model.DepositNo == null ? false : true,
                    Remark = model.Remark
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
