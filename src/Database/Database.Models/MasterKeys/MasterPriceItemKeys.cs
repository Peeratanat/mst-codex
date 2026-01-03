using System;
using System.Collections.Generic;

namespace Database.Models.MasterKeys
{
    public static class MasterPriceItemKeys
    {
        /// <summary>
        // รายการกำจัดปลวก
        /// <summary>
        public static string ExterminatorPrice = "ExterminatorPrice";

        /// <summary>
        // รายการเพิ่มเสาเข็ม
        /// <summary>
        public static string StakePrice = "StakePrice";

        /// <summary>
        // ค่าเบ็ดเตล็ดอื่นๆ
        /// <summary>
        public static string MiscellaneousPrice = "MiscellaneousPrice";

        /// <summary>
        // ค่าเบี้ยประกันอาคาร
        /// <summary>
        public static string BuildingInsurancePrice = "BuildingInsurancePrice";

        /// <summary>
        // เพิ่มเครื่องปรับอากาศ
        /// <summary>
        public static string AirConditioningPrice = "AirConditioningPrice";

        /// <summary>
        // ชำระเงินหลังโอน
        /// <summary>
        public static string PaymentAfterTransferPrice = "PaymentAfterTransferPrice";

        /// <summary>
        // เพิ่มค่าเฟอร์นิเจอร์
        /// <summary>
        public static string FurniturePrice = "FurniturePrice";

        /// <summary>
        // เงินประกันตกแต่ง
        /// <summary>
        public static string InteriorDecorationInsurancePrice = "InteriorDecorationInsurancePrice";

        /// <summary>
        // รายการเพิ่ม(เปลี่ยน)สายไฟ
        /// <summary>
        public static string PowerCablePrice = "PowerCablePrice";

        /// <summary>
        // เงินเกิน นิติบุคคล
        /// <summary>
        public static string ExcessJuristicPersonPrice = "ExcessJuristicPersonPrice";

        /// <summary>
        // ค่าเปลี่ยนแปลง เพิ่มหรือลด วัสดุ
        /// <summary>
        public static string AdjustMaterialPrice = "AdjustMaterialPrice";

        /// <summary>
        // เงินรับส่วนที่เกิน
        /// <summary>
        public static string ExcessMoneyPrice = "ExcessMoneyPrice";

        /// <summary>
        // เงินเกิน AP
        /// <summary>
        public static string ExcessAPPrice = "ExcessAPPrice";

        /// <summary>
        // ชำระเพิ่มเนื่องจากมีการบันทึกสั่งจ่าย
        /// <summary>
        public static string AdditionalPaymentPrice = "AdditionalPaymentPrice";

        /// <summary>
        // ราคาขาย
        /// <summary>
        public static string SellingPrice = "SellingPrice";

        /// <summary>
        // ส่วนลดเงินสด
        /// <summary>
        public static string CashDiscount = "CashDiscount";

        /// <summary>
        // ราคาขายสุทธิ
        /// <summary>
        public static string NetSellingPrice = "NetSellingPrice";

        /// <summary>
        // ส่วนลด ณ​ วันโอน
        /// <summary>
        public static string TransferDiscount = "TransferDiscount";

        /// <summary>
        // ราคาขายหลังหักส่วนลด ([ราคาขาย]-[ส่วนลดเงินสด]-[ส่วนลด ณ​ วันโอน])  
        /// <summary>
        public static string TotalPrice = "TotalPrice";

        /// <summary>
        // ส่วนลด Friend get Friend
        /// <summary>
        public static string FGFDiscount = "FGFDiscount";

        /// <summary>
        // ส่วนลด Free Down
        /// <summary>
        public static string FreeDownDiscount = "FreeDownDiscount";

        /// <summary>
        // มูลค่าบ้านสุทธิหลังหักส่วนลดทุกอย่างแล้ว ([ราคาขาย]-[มูลค่ารวมโปรโมชั่นที่ลูกค้าได้รับ]-[ส่วนลด Freedown])  
        /// <summary>
        public static string RevenueAmount = "RevenueAmount";

        /// <summary>
        // เงินจอง
        /// <summary>
        public static string BookingAmount = "BookingAmount";

        /// <summary>
        // เงินสัญญา
        /// <summary>
        public static string ContractAmount = "ContractAmount";

        /// <summary>
        // เงินดาวน์
        /// <summary>
        //public static string DownAmount = "DownAmount";
        public static string InstallmentAmount = "InstallmentAmount";

        /// <summary>
        // ยอดโอนกรรมสิทธิ์
        /// <summary>
        public static string TransferAmount = "TransferAmount";

        /// <summary>
        // มิเตอร์ไฟ
        /// <summary>
        public static string ElectricMeter = "ElectricMeter";

        /// <summary>
        // มิเตอร์น้ำ
        /// <summary>
        public static string WaterMeter = "WaterMeter";

        /// <summary>
        // ค่าส่วนกลาง
        /// <summary>
        public static string CommonFeeCharge = "CommonFeeCharge";

        /// <summary>
        // ค่ากองทุนแรกเข้าเรียกเก็บครั้งเดียว
        /// <summary>
        public static string FirstSinkingFund = "FirstSinkingFund";

        /// <summary>
        // ค่าธรรมเนียมการโอน
        /// <summary>
        public static string TransferFee = "TransferFee";

        /// <summary>
        // ค่าจดจำนอง
        /// <summary>
        public static string MortgageFee = "MortgageFee";

        /// <summary>
        // ค่าดำเนินการเอกสาร
        /// <summary>
        public static string DocumentFeeCharge = "DocumentFeeCharge";

        /// <summary>
        // ค่าธรรมเนียมการเปลี่ยนมือ
        /// <summary>
        public static string OwnershipTransferFee = "OwnershipTransferFee";

        /// <summary>
        // ราคาพื้นที่เพิ่มลด
        /// <summary>
        public static string ExtraAreaPrice = "ExtraAreaPrice";

        /// <summary>
        // ค่าส่วนกลาง (AP ช่วยจ่าย)
        /// <summary>
        public static string CommonFeeChargeAPPay = "CommonFeeChargeAPPay";

        /// <summary>
        // ส่วนลด ณ วันโอน(โปรโอน)
        /// <summary>
        public static string TransferDiscountTransferPromotion = "TransferDiscountTransferPromotion";

        /// <summary>
        // ค่าจดจำนอง(กรณีกู้เกิน)
        /// <summary>
        public static string MortgageFeeOverLoan = "MortgageFeeOverLoan";

        /// <summary>
        // Extra Discount
        /// <summary>
        public static string ExtraDiscount = "ExtraDiscount";

        /// <summary>
        // รับเงินก่อนทำสัญญา
        /// <summary>
        public static string AdvanceContractPayment = "AdvanceContractPayment";


        public static List<string> Discount = new List<string>
        {
            FreeDownDiscount,
            //ExtraAreaPrice,
            CashDiscount,
            ExtraDiscount,
            FGFDiscount,
            TransferDiscount
        };

        public static List<string> HousePayment = new List<string>
        {
            BookingAmount,
            ContractAmount,
            InstallmentAmount,
            TransferAmount,
            ExtraAreaPrice,
            AdvanceContractPayment
        };

        public static List<string> JuristicPayment = new List<string>
        {
            ExcessJuristicPersonPrice,
            FirstSinkingFund,
            CommonFeeCharge 
        };

        public static List<string> MinistryOfFinance = new List<string>
        {
            TransferFee
        };
    }
}
