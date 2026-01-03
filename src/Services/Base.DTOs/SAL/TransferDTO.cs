using Base.DTOs.FIN;
using Base.DTOs.MST;
using Base.DTOs.USR;
using Database.Models;
using Database.Models.MasterKeys;
using Database.Models.PRJ;
using Database.Models.PRM;
using Database.Models.SAL;
using ErrorHandling;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Base.DTOs.SAL
{
    public class TransferDTO : BaseDTO
    {
        /// <summary>
        /// โครงการ
        /// </summary>
        public PRJ.ProjectDropdownDTO Project { get; set; }
        /// <summary>
        /// แปลง
        /// </summary>
        public PRJ.UnitDTO Unit { get; set; }
        /// <summary>
        /// เลขที่โอนกรรมสิทธิ์
        /// </summary>
        public string TransferNo { get; set; }
        /// <summary>
        /// สัญญา
        /// </summary>
        public AgreementDropdownDTO Agreement { get; set; }
        /// <summary>
        /// พื้นที่ (ตร.ว/ตร.ม)
        /// </summary>
        public double? StandardArea { get; set; }
        /// <summary>
        /// พื้นที่ที่ใช้คำนวนราคาประเมิณ
        /// </summary>
        public double? LandArea { get; set; }
        /// <summary>
        /// ราคาประเมิณ
        /// </summary>
        public decimal? LandEstimatePrice { get; set; }
        /// <summary>
        /// LC โอน
        /// </summary>
        [Description("LC โอน")]
        public USR.UserListDTO TransferSale { get; set; }
        /// <summary>
        /// วันที่นัดโอนกรรมสิทธื์
        /// </summary>
        [Description("วันที่นัดโอนกรรมสิทธื์")]
        public DateTime? ScheduleTransferDate { get; set; }
        /// <summary>
        /// วันที่โอนจริง
        /// </summary>
        public DateTime? ActualTransferDate { get; set; }
        /// <summary>
        /// ภาษีเงินได้นิติบุคคล
        /// </summary>
        public decimal? CompanyIncomeTax { get; set; }
        /// <summary>
        /// ภาษีเงินได้ธุรกิจเฉพาะ
        /// </summary>
        public decimal? BusinessTax { get; set; }
        /// <summary>
        /// ภาษีท้องถิ่น
        /// </summary>
        public decimal? LocalTax { get; set; }
        /// <summary>
        /// รูดบัตร P.Card กระทรวงการคลัง
        /// </summary>
        public decimal? MinistryPCard { get; set; }
        /// <summary>
        /// เงินสดหรือเช็คกระทรวงการคลัง
        /// </summary>
        public decimal? MinistryCashOrCheque { get; set; }
        /// <summary>
        /// เช็คค่ามิเตอร์
        /// </summary>
        public MST.MasterCenterDropdownDTO MeterCheque { get; set; }
        /// <summary>
        /// ลูกค้าจ่ายค่าจดจำนอง
        /// </summary>
        public decimal? CustomerPayMortgage { get; set; }
        /// <summary>
        /// ลูกค้าจ่ายค่าธรรมเนียม
        /// </summary>
        public decimal? CustomerPayFee { get; set; }
        /// <summary>
        /// บริษัทจ่ายค่าธรรมเนียม
        /// </summary>
        public decimal? CompanyPayFee { get; set; }
        /// <summary>
        /// ฟรีค่าจดจำนอง-ค่าธรรมเนียม
        /// </summary>
        public decimal? FreeFee { get; set; }
        /// <summary>
        /// (-)ขาด/(+)เกิน
        /// </summary>
        public decimal? DocumentFee { get; set; }

        /// <summary>
        /// ยอดคงเหลือ AP
        /// </summary>
        public decimal? APBalance { get; set; }
        /// <summary>
        /// ยกยอดไปนิติบุคคล
        /// </summary>
        public bool? IsAPBalanceTransfer { get; set; }
        /// <summary>
        /// ยอดคงเหลือ AP ยกยอดไปนิติบุคคล
        /// </summary>
        public decimal? APBalanceTransfer { get; set; }
        /// <summary>
        /// คงเหลือสุทธิ (AP)
        /// </summary>
        public decimal? APNetBalance { get; set; }
        /// <summary>
        /// เงินทอนก่อนโอน
        /// </summary>
        public decimal? APChangeAmountBeforeTransfer { get; set; }
        /// <summary>
        /// รวมเงินทอน AP
        /// </summary>
        public decimal? APChangeAmount { get; set; }
        /// <summary>
        /// การทอนคืน AP
        /// </summary>
        public bool? IsAPGiveChange { get; set; }
        /// <summary>
        /// จ่ายด้วย AP
        /// </summary>
        public MST.MasterCenterDropdownDTO APPayWithMemo { get; set; }
        /// <summary>
        /// ยอดคงเหลือนิติบุคคล
        /// </summary>
        public decimal? LegalEntityBalance { get; set; }
        /// <summary>
        /// ยกยอดไป AP
        /// </summary>
        public bool? IsLegalEntityBalanceTransfer { get; set; }
        /// <summary>
        /// ยอดคงเหลือนิติบุคคล ยกยอดไป AP
        /// </summary>
        public decimal? LegalEntityBalanceTransfer { get; set; }
        /// <summary>
        /// คงเหลือสุทธิ (นิติบุคคล)
        /// </summary>
        public decimal? LegalEntityNetBalance { get; set; }
        /// <summary>
        /// เงินทอนก่อนโอน
        /// </summary>
        public decimal? LegalEntityChangeAmountBeforeTransfer { get; set; }
        /// <summary>
        /// รวมเงินทอน  (นิติบุคคล)
        /// </summary>
        public decimal? LegalEntityChangeAmount { get; set; }
        /// <summary>
        /// การทอนคืนนิติบุคคล
        /// </summary>
        public bool? IsLegalEntityGiveChange { get; set; }
        /// <summary>
        /// จ่ายด้วย
        /// </summary>
        public MST.MasterCenterDropdownDTO LegalEntityPayWithMemo { get; set; }

        /// <summary>
        /// รวมรับเงินสดย่อย
        /// </summary>
        public decimal? PettyCashAmount { get; set; }
        /// <summary>
        /// ค่าเดินทางไป
        /// </summary>
        public decimal? GoTransportAmount { get; set; }
        /// <summary>
        /// ค่าเดินทางกลับ
        /// </summary>
        public decimal? ReturnTransportAmount { get; set; }
        /// <summary>
        /// ค่าเดินทางระหว่าง สนง. ที่ดิน
        /// </summary>
        public decimal? LandOfficeTransportAmount { get; set; }
        /// <summary>
        /// ค่าทางด่วนไป
        /// </summary>
        public decimal? GoTollWayAmount { get; set; }
        /// <summary>
        /// ค่าทางด่วนกลับ
        /// </summary>
        public decimal? ReturnTollWayAmount { get; set; }
        /// <summary>
        /// ค่าทางด่วนระหว่าง สนง. ที่ดิน
        /// </summary>
        public decimal? LandOfficeTollWayAmount { get; set; }
        /// <summary>
        /// รับรองเจ้าหน้าที่
        /// </summary>
        public decimal? SupportOfficerAmount { get; set; }
        /// <summary>
        /// ค่าถ่ายเอกสาร
        /// </summary>
        public decimal? CopyDocumentAmount { get; set; }

        /// <summary>
        /// พร้อมโอน
        /// </summary>
        public bool? IsReadyToTransfer { get; set; }
        /// <summary>
        /// วันที่พร้อมโอน
        /// </summary>
        public DateTime? ReadyToTransferDate { get; set; }
        /// <summary>
        /// ยืนยันโอนจริง
        /// </summary>
        public bool? IsTransferConfirmed { get; set; }
        /// <summary>
        /// ผู้กดยืนยันโอนจริง
        /// </summary>
        public Guid? TransferConfirmedUserID { get; set; }
        /// <summary>
        /// วันที่ยืนยันโอนจริง
        /// </summary>
        public DateTime? TransferConfirmedDate { get; set; }
        /// <summary>
        /// นำส่งการเงิน
        /// </summary>
        public bool? IsSentToFinance { get; set; }
        /// <summary>
        /// วันที่นำส่งการเงิน
        /// </summary>
        public DateTime? SentToFinanceDate { get; set; }
        /// <summary>
        /// ยืนยันชำระเงิน
        /// </summary>
        public bool? IsPaymentConfirmed { get; set; }
        /// <summary>
        /// วันที่ยืนยันชำระเงิน
        /// </summary>
        public DateTime? PaymentConfirmedDate { get; set; }
        /// <summary>
        /// บัญชีอนุมัติ
        /// </summary>
        public bool? IsAccountApproved { get; set; }
        /// <summary>
        /// วันที่บัญชีอนุมัติ
        /// </summary>
        public DateTime? AccountApprovedDate { get; set; }
        ///<summary>
        ///สถานะโอนกรรมสิทธิ์
        ///</summary>
        public MasterCenterDropdownDTO TransferStatus { get; set; }

        /// <summary>
        ///ค่าบ้าน (เงินที่เรียกเก็บ)
        /// </summary>
        public decimal? APAmount { get; set; }
        /// <summary>
        ///ค่าบ้าน (เงินที่จ่ายมาแล้ว)
        /// </summary>
        public decimal? APAmountPaymented { get; set; }
        /// <summary>
        ///ค่ามิเตอร์ (เงินที่เรียกเก็บ)
        /// </summary>
        public decimal? MeterAmount { get; set; }
        /// <summary>
        ///ค่ามิเตอร์ (เงินที่จ่ายมาแล้ว)
        /// </summary>
        public decimal? MeterAmountPaymented { get; set; }
        /// <summary>
        ///ค่าเงินจ่ายที่ดิน (เงินที่เรียกเก็บ)
        /// </summary>
        public decimal? LandAmount { get; set; }
        /// <summary>
        ///ค่าเงินจ่ายที่ดิน (เงินที่จ่ายมาแล้ว)
        /// </summary>
        public decimal? LandAmountPaymented { get; set; }
        /// <summary>
        ///(-)ขาด/(+)เกิน (AP)
        /// </summary>
        public decimal? APTotalBalance { get; set; }
        /// <summary>
        ///รวม AP (เงินที่เรียกเก็บ)
        /// </summary>
        public decimal? APTotalAmount { get; set; }
        /// <summary>
        ///รวม AP (เงินที่จ่ายมาแล้ว)
        /// </summary>
        public decimal? APTotalAmountPaymented { get; set; }

        /// <summary>
        ///ค่าสาธารณูปโภค (เงินที่เรียกเก็บ)
        /// </summary>
        public decimal? LegalEntityAmount { get; set; }
        /// <summary>
        ///ค่าสาธารณูปโภค (เงินที่จ่ายมาแล้ว)
        /// </summary>
        public decimal? LegalEntityAmountPaymented { get; set; }
        /// <summary>
        ///(-)ขาด/(+)เกิน (นิติ)
        /// </summary>
        public decimal? LegalEntityTotalBalance { get; set; }
        /// <summary>
        ///รวม นิติ (เงินที่เรียกเก็บ)
        /// </summary>
        public decimal? LegalEntityTotalAmount { get; set; }
        /// <summary>
        ///รวม นิติ (เงินที่จ่ายมาแล้ว)
        /// </summary>
        public decimal? LegalEntityTotalAmountPaymented { get; set; }


        ///<summary>
        ///เงินบริษัทที่จ่ายมา
        ///</summary>
        public decimal? SumAPPaymented { get; set; }
        ///<summary>
        ///เงินนิติบุคคลที่จ่ายมา
        ///</summary>
        public decimal? SumLegalEntityPaymented { get; set; }

        ///<summary>
        ///มอบอำนาจหรือไม่?
        ///</summary>
        public bool? IsAssignAuthority { get; set; }
        ///<summary>
        ///ผู้แก้ไขมอบอำนาจ
        ///</summary>
        public USR.UserDTO AssignAuthorityUser { get; set; }
        ///<summary>
        ///วันที่แก้ไขมอบอำนาจ
        ///</summary>
        public DateTime? AssignAuthorityDate { get; set; }

        ///<summary>
        ///ลูกค้าได้รับเอกสารหลังโอนกรรมสิทธิ์แล้วหรือไม่?
        ///</summary>
        public bool? IsReceiveDocument { get; set; }
        ///<summary>
        ///ผู้แก้ไขลูกค้าได้รับเอกสารหลังโอนกรรมสิทธิ์แล้ว
        ///</summary>
        public USR.UserDTO ReceiveDocumentUser { get; set; }
        ///<summary>
        ///วันที่แก้ไขลูกค้าได้รับเอกสารหลังโอนกรรมสิทธิ์แล้ว
        ///</summary>
        public DateTime? ReceiveDocumentDate { get; set; }
        ///<summary>
        ///เหตุผล
        ///</summary>
        public string TransferDocumentRemark { get; set; }

        //public List<TransferExpenseListDTO> TransferExpenseList { get; set; }
        public List<SummaryTransferPayment> SummaryAPPaymentList { get; set; }
        public List<SummaryTransferPayment> SummaryLegalEntityPaymentList { get; set; }

        /// <summary>
        /// พื้นที่เพิ่ม-ลด
        /// </summary>
        public double? AddOnArea { get; set; }


        /// <summary>
        /// ค่าส่วนกลาง
        /// </summary>
        [Description("ค่าส่วนกลาง")]
        public decimal CommonFeeCharge { get; set; }

        /// <summary>
        /// ค่าใช้จ่ายที่ไม่เก็บจากลูกค้า
        /// </summary>
        [Description("ค่าใช้จ่ายที่ไม่เก็บจากลูกค้า")]
        public decimal? CustomerNoPayAmount { get; set; }

        /// <summary>
        /// ค่าใช้จ่ายที่เก็บจากลูกค้า
        /// </summary>
        [Description("ค่าใช้จ่ายที่เก็บจากลูกค้า")]
        public decimal? CustomerPayAmount { get; set; }

        /// <summary>
        /// ค่่าจ่ายทีดิน
        /// </summary>
        [Description("ค่่าจ่ายทีดิน")]
        public decimal LandAmountCharge { get; set; }

        /// <summary>
        /// ค่ามิเตอร์
        /// </summary>
        [Description("ค่ามิเตอร์")]
        public decimal MeterAmountCharge { get; set; }

        /// <summary>
        /// รวมเงินที่เก็บจากลูกค้า
        /// </summary>
        [Description("รวมเงินที่เก็บจากลูกค้า")]
        public decimal? TotalCustomerPayAmount { get; set; }

        /// <summary>
        /// รวมเงินที่ชำระมาแล้ว
        /// </summary>
        [Description("รวมเงินที่ชำระมาแล้ว")]
        public decimal TotalPaidAmount { get; set; }

        /// <summary>
        /// รวมบริษัทรับเงิน
        /// </summary>
        [Description("รวมบริษัทรับเงิน")]
        public decimal TotalAPRecieveAmt { get; set; }

        /// <summary>
        /// รวมนิติบุคคลรับเงิน
        /// </summary>
        [Description("รวมนิติบุคคลรับเงิน")]
        public decimal TotalLegalRecieveAmt { get; set; }

        /// <summary>
        /// รวมกระทรวงการคลังรับเงิน
        /// </summary>
        [Description("รวมกระทรวงการคลังรับเงิน")]
        public decimal TotalMOFRecieveAmt { get; set; }

        /// <summary>
        /// รวมบริษัทรับเงินก่อนโอน
        /// </summary>
        [Description("รวมบริษัทรับเงินก่อนโอน")]
        public decimal TotalAPRecieveAmtBfTransfer { get; set; }

        /// <summary>
        /// รวมนิติบุคคลรับเงินก่อนโอน
        /// </summary>
        [Description("รวมนิติบุคคลรับเงินก่อนโอน")]
        public decimal TotalLegalRecieveAmtBfTransfer { get; set; }

        /// <summary>
        /// รวมกระทรวงการคลังรับเงินก่อนโอน
        /// </summary>
        [Description("รวมกระทรวงการคลังรับเงินก่อนโอน")]
        public decimal TotalMOFRecieveAmtBfTransfer { get; set; }

        /// <summary>
        /// ราคาพื้นที่ต่อหน่วย
        /// </summary>
        public decimal AreaPricePerUnit { get; set; }

        /// <summary>
        /// นิติบุคคล
        /// </summary>
        public LegalEntityDTO LegalEntity { get; set; }

        ///// <summary>
        ///// ค่าเนื้อที่เพิ่ม-ลด
        ///// </summary>
        //public decimal IncreasingAreaPrice { get; set; }

        /// <summary>
        /// ผู้รับโอน : มอบอำนาจหรือไม่
        /// </summary>
        [Description("ผู้รับโอน : มอบอำนาจหรือไม่?")]
        public bool IsOwnerAssignAuthority { get; set; }
        /// <summary>
        /// ผู้รับโอน : มอบอำนาจโดยบริษัท
        /// </summary>
        [Description("ผู้รับโอน : มอบอำนาจโดยบริษัท")]
        //public bool? IsOwnerAssignAuthorityByCompany { get; set; }
        public int? IsOwnerAssignAuthorityByCompany { get; set; }
        /// <summary>
        /// ผู้รับโอน : ชื่อผู้รับมอบอำนาจ
        /// </summary>
        [Description("ผู้รับโอน : ชื่อผู้รับมอบอำนาจ")]
        public string OwnerAuthorityName { get; set; }

        /// <summary>
        /// รวมค่าใช้จ่าย (ค่าจดจำนอง + ค่าธรรมเนียมการโอน + (ค่าดำเนินการเอกสาร (ลูกค้า) + (ค่าดำเนินการเอกสาร (บริษัท) / 2)))
        /// </summary>
        public decimal SumTransferFee { get; set; }

        /// <summary>
        /// ชื่อผู้รับมอบอำนาจบริษัท
        /// </summary>
        [Description("ชื่อผู้รับมอบอำนาจบริษัท")]
        public string CompanyAuthorityName { get; set; }

        /// <summary>
        /// ผู้ใช้งาน P.Card
        /// </summary>
        [Description("ผู้ใช้งาน P.Card")]
        public USR.UserListDTO PCardUser { get; set; }

        /// <summary>
        /// เงินรอรับชำระจากการตั้งพัก
        /// </summary>
        [Description("เงินรอรับชำระจากการตั้งพัก")]
        public decimal? SuspenseAmount { get; set; }

        /// <summary>
        /// ยกยอดไปเป็นเงินทอน(เงินตั้งพัก)
        /// </summary>
        [Description("ยกยอดไปเป็นเงินทอน(เงินตั้งพัก)")]
        public bool? IsSuspenseChange { get; set; }

        /// <summary>
        /// เงินรับชำระเกินก่อนโอน
        /// </summary>
        [Description("เงินรับชำระเกินก่อนโอน")]
        public decimal? RecieveAmtOverBfTransfer { get; set; }

        /// <summary>
        /// ค่าดำเนินการเอกสาร
        /// </summary>
        [Description("ค่าดำเนินการเอกสาร")]
        public decimal? DocumentFeeCharge { get; set; }

        /// <summary>
        /// ฟรีค่าดำเนินการเอกสาร
        /// </summary>
        public decimal? FreeDocumentFee { get; set; }

        /// <summary>
        ///ค่าดำเนินการเอกสาร (เงินที่เรียกเก็บ)
        /// </summary>
        public decimal? CustDocumentFee { get; set; }
        /// <summary>
        ///ค่าดำเนินการเอกสาร (เงินที่จ่ายมาแล้ว)
        /// </summary>
        public decimal? CustDocumentFeePaymented { get; set; }

        /// <summary>
        /// เงินรับชำระเกินหลังโอน
        /// </summary>
        [Description("เงินรับชำระเกินหลังโอน")]
        public decimal? RecieveAmtOverAtTransfer { get; set; }

        /// <summary>
        /// ค่าดำเนินการเอกสาร สำหรับคำนวณ (ขาด-เกิน)
        /// </summary>
        public decimal? DocumentFeeAmtForCal { get; set; }

        /// <summary>
        /// User ทำ การทอนคืน (AP)
        /// </summary>
        public bool? FlagUserAPGiveChange { get; set; }

        /// <summary>
        /// User ทำ จ่ายด้วย (AP)
        /// </summary>
        public bool? FlagUserAPPayWithMemo { get; set; }

        /// <summary>
        /// User ทำ การทอนคืน (Legal)
        /// </summary>
        public bool? FlagUserLegalEntityGiveChange { get; set; }

        /// <summary>
        /// User ทำ จ่ายด้วย (Legal)
        /// </summary>
        public bool? FlagUserLegalEntityPayWithMemo { get; set; }

        /// <summary>
        /// TransferFi
        /// </summary>
        public TransferFiDTO TransferFi { get; set; }

        /// <summary>
        /// เปิดปุ่ม Memo
        /// </summary>
        public bool IsCanMemo { get; set; }

        /// <summary>
        /// ลูกค้าจ่ายค่าส่วนกลาง
        /// </summary>
        public bool IsCustCommonFeeCharge { get; set; }

        /// <summary>
        /// ลูกค้าจ่ายค่ากองทุนแรกเข้า
        /// </summary>
        public bool IsCustFirstSinkingFund { get; set; }

        /// <summary>
        /// ค่าสาธารณูปโภคอื่นๆ
        /// </summary>
        public decimal? FacilityAmt { get; set; }

        /// <summary>
        /// ค่าสาธารณูปโภคอื่นๆ (รับชำระมาก่อนโอน)
        /// </summary>
        public decimal? FacilityBeforeTransferAmt { get; set; }

        public bool isRequestTitleDeed { get; set; }

        public int? IsTransferByAgent { get; set; }

        /// <summary>
        /// โครงการซื้อคืน
        /// </summary>
        public bool IsProjectRePurchase { get; set; }

        public async static Task<TransferDTO> CreateFromModelAsync(Transfer model, DatabaseContext DB)
        {
            if (model != null)
            {
                var titledeedRQFlow = await DB.TitledeedRequestFlows.Where(o => o.BookingID == model.Agreement.BookingID).FirstOrDefaultAsync();
                var titledeedConfig = await DB.TitledeedConfigs.Where(o => o.ProjectID == model.Project.ID).FirstOrDefaultAsync();
                var result = new TransferDTO();

                result.Id = model.ID;
                result.Project = PRJ.ProjectDropdownDTO.CreateFromModel(model.Project);
                result.Unit = PRJ.UnitDTO.CreateFromModel(model.Unit);
                result.TransferNo = model.TransferNo;
                result.Agreement = AgreementDropdownDTO.CreateFromModel(model.Agreement);
                result.StandardArea = model.StandardArea;
                result.LandArea = model.LandArea;
                result.LandEstimatePrice = model.LandEstimatePrice;
                result.TransferSale = USR.UserListDTO.CreateFromModel(model.TransferSale);
                result.ScheduleTransferDate = model.ScheduleTransferDate;
                result.ActualTransferDate = model.ActualTransferDate;
                result.CompanyIncomeTax = model.CompanyIncomeTax;
                result.BusinessTax = model.BusinessTax;
                result.LocalTax = model.LocalTax;
                result.MinistryPCard = model.MinistryPCard;
                result.MinistryCashOrCheque = model.MinistryCashOrCheque;
                result.MeterCheque = MasterCenterDropdownDTO.CreateFromModel(model.MeterCheque);

                result.APBalance = model.APBalance;
                result.IsAPBalanceTransfer = model.IsAPBalanceTransfer;
                result.APBalanceTransfer = model.APBalanceTransfer;
                result.APNetBalance = (model.APBalance ?? 0) - (model.APBalanceTransfer ?? 0);
                result.APChangeAmountBeforeTransfer = model.APChangeAmountBeforeTransfer;
                result.APChangeAmount = model.APChangeAmount;
                result.IsAPGiveChange = model.IsAPGiveChange;
                result.APPayWithMemo = MasterCenterDropdownDTO.CreateFromModel(model.APPayWithMemo);

                result.LegalEntityBalance = model.LegalEntityBalance;
                result.IsLegalEntityBalanceTransfer = model.IsLegalEntityBalanceTransfer;
                result.LegalEntityBalanceTransfer = model.LegalEntityBalanceTransfer;
                result.LegalEntityNetBalance = (model.LegalEntityBalance ?? 0) - (model.LegalEntityBalanceTransfer ?? 0);
                result.LegalEntityChangeAmount = model.LegalEntityChangeAmount;
                result.IsLegalEntityGiveChange = model.IsLegalEntityGiveChange;
                result.LegalEntityPayWithMemo = MasterCenterDropdownDTO.CreateFromModel(model.LegalEntityPayWithMemo);

                result.PettyCashAmount = model.PettyCashAmount;
                result.GoTransportAmount = model.GoTransportAmount;
                result.ReturnTransportAmount = model.ReturnTransportAmount;
                result.LandOfficeTransportAmount = model.LandOfficeTransportAmount;
                result.GoTollWayAmount = model.GoTollWayAmount;
                result.ReturnTollWayAmount = model.ReturnTollWayAmount;
                result.LandOfficeTollWayAmount = model.LandOfficeTollWayAmount;
                result.SupportOfficerAmount = model.SupportOfficerAmount;
                result.CopyDocumentAmount = model.CopyDocumentAmount;

                result.IsReadyToTransfer = model.IsReadyToTransfer;
                result.ReadyToTransferDate = model.ReadyToTransferDate;
                result.IsTransferConfirmed = model.IsTransferConfirmed;
                result.TransferConfirmedUserID = model.TransferConfirmedUserID;
                result.TransferConfirmedDate = model.TransferConfirmedDate;
                //result.IsSentToFinance = model.IsSentToFinance;
                //result.SentToFinanceDate = model.SentToFinanceDate;
                result.IsPaymentConfirmed = model.IsPaymentConfirmed;
                result.PaymentConfirmedDate = model.PaymentConfirmedDate;
                result.IsAccountApproved = model.IsAccountApproved;
                result.AccountApprovedDate = model.AccountApprovedDate;
                result.TransferStatus = MasterCenterDropdownDTO.CreateFromModel(model.TransferStatus);
                result.SumAPPaymented = model.SumAPPaymented;
                result.SumLegalEntityPaymented = model.SumLegalEntityPaymented;

                result.CommonFeeCharge = model.CommonFeeCharge;
                result.CustomerNoPayAmount = model.CustomerNoPayAmount;
                result.CustomerPayAmount = model.CustomerPayAmount;
                result.LandAmountCharge = model.LandAmountCharge;
                result.MeterAmountCharge = model.MeterAmountCharge;
                result.TotalCustomerPayAmount = model.TotalCustomerPayAmount;
                result.TotalPaidAmount = model.TotalPaidAmount;

                //AP
                result.APAmount = model.APAmount;
                result.MeterAmount = model.MeterAmount;
                result.LandAmount = model.LandAmount;

                //นิติ
                result.LegalEntityAmount = model.LegalEntityAmount;

                #region "ข้อมูล Control Doc "
                result.IsAssignAuthority = model.IsAssignAuthority;
                result.AssignAuthorityUser = UserDTO.CreateFromModel(model.AssignAuthorityUser);
                result.AssignAuthorityDate = model.AssignAuthorityDate;
                result.IsReceiveDocument = model.IsReceiveDocument;
                result.ReceiveDocumentUser = UserDTO.CreateFromModel(model.ReceiveDocumentUser);
                result.ReceiveDocumentDate = model.ReceiveDocumentDate;
                result.TransferDocumentRemark = model.TransferDocumentRemark;
                #endregion

                result.AddOnArea = 0;
                if ((result.Unit?.TitleDeed?.TitledeedArea ?? 0.00) > 0 && (result.StandardArea ?? 0) > 0)
                {
                    result.AddOnArea = (result.Unit?.TitleDeed?.TitledeedArea ?? 0.00) - (result.StandardArea ?? 0);
                }

                #region นิติบุคคล
                var agreementConfig = await DB.AgreementConfigs
                                    .Include(o => o.LegalEntity)
                                    .Include(o => o.AttorneyNameTransfer)
                                    .Where(o => o.ProjectID == model.ProjectID)
                                    .FirstOrDefaultAsync();

                if (agreementConfig != null)
                {
                    result.LegalEntity = LegalEntityDTO.CreateFromModel(agreementConfig?.LegalEntity);

                    result.CompanyAuthorityName = agreementConfig.AttorneyNameTransfer?.Atty_FullName;
                }

                #endregion

                #region ผู้รับโอน

                var owner = await DB.TransferOwners
                                    .OrderBy(o => o.Order)
                                    .Where(o => o.TransferID == model.ID)
                                    .FirstOrDefaultAsync();

                if (owner != null)
                {
                    result.IsOwnerAssignAuthority = titledeedRQFlow != null ? titledeedRQFlow.IsAuthorized.Value : owner.IsAssignAuthority;
                    //result.IsOwnerAssignAuthorityByCompany = owner.IsAssignAuthorityByCompany;
                    if (model.IsTransferByAgent == null)
                    {
                        model.IsTransferByAgent = false;
                    }

                    if (owner.IsAssignAuthorityByCompany == true && model.IsTransferByAgent == false) // บริษัท
                    {
                        result.IsOwnerAssignAuthorityByCompany = 1;
                    } else if (owner.IsAssignAuthorityByCompany == false && model.IsTransferByAgent == false) //บุคคลอื่น
                    {
                        result.IsOwnerAssignAuthorityByCompany = 2;
                    } else if (owner.IsAssignAuthorityByCompany == false && model.IsTransferByAgent == true) //Agent
                    {
                        result.IsOwnerAssignAuthorityByCompany = 3;
                    }
                    result.OwnerAuthorityName = owner.AuthorityName;

                    //result.IsOwnerAssignAuthority = owner.IsAssignAuthority;
                    //result.IsOwnerAssignAuthorityByCompany = owner.IsAssignAuthorityByCompany;
                    //result.OwnerAuthorityName = owner.AuthorityName;
                }

                #endregion

                #region รวมค่าใช้จ่าย

                decimal FreeDocumentFee = 0;

                decimal SumTransferFee = 0;

                var transferFeeList = await DB.TransferExpenses
                        .Include(o => o.MasterPriceItem)
                        .Where(o => o.TransferID == model.ID)
                        .ToListAsync() ?? new List<TransferExpense>();

                foreach (var a in transferFeeList)
                {
                    if (a.MasterPriceItem.Key == MasterPriceItemKeys.MortgageFee)
                    {
                        SumTransferFee += (a.BuyerAmount + a.SellerAmount);
                    }
                    else if (a.MasterPriceItem.Key == MasterPriceItemKeys.TransferFee)
                    {
                        SumTransferFee += (a.BuyerAmount + (a.SellerAmount / 2));
                    }
                    else if (a.MasterPriceItem.Key == MasterPriceItemKeys.DocumentFeeCharge)
                    {
                        SumTransferFee += (a.BuyerAmount + a.SellerAmount);

                        FreeDocumentFee = a.SellerAmount;
                    }
                    else if (a.MasterPriceItem.Key == MasterPriceItemKeys.MortgageFeeOverLoan)
                    {
                        SumTransferFee += (a.BuyerAmount + a.SellerAmount);
                    }
                }

                result.SumTransferFee = SumTransferFee;

                #endregion

                result.PCardUser = USR.UserListDTO.CreateFromModel(model.PCardUser);

                result.IsSuspenseChange = model.IsSuspenseChange ?? false;
                result.SuspenseAmount = model.SuspenseAmount ?? 0;

                result.LegalEntityChangeAmount = model.LegalEntityChangeAmount ?? 0;
                result.LegalEntityChangeAmountBeforeTransfer = 0; //model.LegalEntityChangeAmountBeforeTransfer;

                result.RecieveAmtOverBfTransfer = 0; //model.RecieveAmtOverBfTransfer;
                result.RecieveAmtOverAtTransfer = 0; //model.RecieveAmtOverAtTransfer;

                result.CustomerPayMortgage = model.CustomerPayMortgage ?? 0;
                result.CustomerPayFee = model.CustomerPayFee ?? 0;
                result.CompanyPayFee = model.CompanyPayFee ?? 0;

                result.FreeFee = model.FreeFee ?? 0;
                result.DocumentFeeCharge = model.DocumentFeeCharge ?? 0;
                result.FreeDocumentFee = FreeDocumentFee;
                result.DocumentFee = model.DocumentFee ?? 0;

                result.DocumentFeeAmtForCal = 0;

                result.FlagUserAPPayWithMemo = model.FlagUserAPPayWithMemo ?? false;
                result.FlagUserLegalEntityPayWithMemo = model.FlagUserLegalEntityPayWithMemo ?? false;
                result.FlagUserAPGiveChange = model.FlagUserAPGiveChange ?? false;
                result.FlagUserLegalEntityGiveChange = model.FlagUserLegalEntityGiveChange ?? false;

                result.IsCanMemo = false;
                result.IsCustCommonFeeCharge = false;
                result.IsCustFirstSinkingFund = false;
                result.FacilityAmt = 0;
                result.FacilityBeforeTransferAmt = 0;

                //if (model.IsTransferByAgent == true)
                //{
                //    result.IsTransferByAgent = 1;
                //} else
                //{
                //    result.IsTransferByAgent = 0;
                //}


                //result.isRequestTitleDeed = titledeedRQFlow != null ? true : false;

                if (titledeedConfig?.IsFIRequest == true)
                {
                    result.isRequestTitleDeed = true;
                } 
                else
                {
                    if (titledeedRQFlow != null)
                    {
                        result.isRequestTitleDeed = true;
                    }
                    else
                    {
                        result.isRequestTitleDeed = false;
                    }
                }
                


                return result;
            }
            else
            {
                return null;
            }
        }

        public async static Task<TransferDTO> CreateFromAgreementModelAsync(Agreement model, DatabaseContext DB, TitledeedRequestFlow titledeedRQFlow , Guid? userID = null, List<TransferExpenseListDTO> transferFeeList = null)
        {
            if (model != null)
            {
                model = model ?? new Agreement();
                model.Project = model.Project ?? new Database.Models.PRJ.Project();
                model.Unit = model.Unit ?? new Database.Models.PRJ.Unit();
                model.Booking = model.Booking ?? new Booking();

                var result = new TransferDTO();

                result.Project = PRJ.ProjectDropdownDTO.CreateFromModel(model.Project);
                result.Unit = PRJ.UnitDTO.CreateFromModel(model.Unit);
                result.TransferNo = "";
                result.Agreement = AgreementDropdownDTO.CreateFromModel(model);

                if (userID != null)
                {
                    var transferSale = await DB.Users.Where(o => o.ID == userID).FirstOrDefaultAsync();
                    result.TransferSale = USR.UserListDTO.CreateFromModel(transferSale);
                }

                result.ScheduleTransferDate = titledeedRQFlow != null ? titledeedRQFlow.ScheduleTransferDate: model.TransferOwnershipDate;
                //result.ScheduleTransferDate = model.TransferOwnershipDate;
                result.ActualTransferDate = null;

                result.CompanyIncomeTax = 0;
                result.BusinessTax = 0;
                result.LocalTax = 0;
                result.LandArea = 0;
                result.LandEstimatePrice = 0;

                var transferFee = await DB.TransferFeeResults.Where(o => o.AgreementID == model.ID).FirstOrDefaultAsync();

                if (transferFee != null)
                {
                    result.CompanyIncomeTax = transferFee.CompanyIncomeTax ?? 0; //ภาษีเงินได้นิติบุคคล
                    result.BusinessTax = transferFee.BusinessTax ?? 0;
                    result.LocalTax = transferFee.LocalTax ?? 0;
                    result.LandArea = transferFee.SaleArea ?? 0;
                    result.LandEstimatePrice = transferFee.EstimateTotalPrice ?? 0;
                }

                result.StandardArea = model.Booking.SaleArea ?? 0;

                result.MinistryPCard = 0;
                result.MinistryCashOrCheque = 0;//เงินสดหรือเช็คกระทรวงการคลัง

                //result.MeterCheque = MST.MasterCenterDropdownDTO.CreateFromModel(model.MeterCheque);

                result.APBalance = 0;
                //result.IsAPBalanceTransfer = false;
                result.APBalanceTransfer = 0;
                result.APChangeAmountBeforeTransfer = 0;
                result.APChangeAmount = 0;
                //result.IsAPGiveChange = false;
                //result.APPayWithMemo = null;
                result.LegalEntityBalance = 0;
                //result.IsLegalEntityBalanceTransfer = false;
                result.LegalEntityBalanceTransfer = 0;
                //result.IsLegalEntityGiveChange = false;
                //result.LegalEntityPayWithMemo = null;

                result.PettyCashAmount = 1200; //Default 1200
                result.GoTransportAmount = 0;
                result.ReturnTransportAmount = 0;
                result.LandOfficeTransportAmount = 0;
                result.GoTollWayAmount = 0;
                result.ReturnTollWayAmount = 0;
                result.LandOfficeTollWayAmount = 0;

                result.SupportOfficerAmount = 500; //Default 500

                result.CopyDocumentAmount = 50; //Default 50

                //var ProjectNo = model.Project.ProjectNo ?? "";
                //if (ProjectNo == "60016" || ProjectNo == "40052")
                //{
                //    result.CopyDocumentAmount = 0; //Default 0
                //}
                //else
                //{
                //    result.CopyDocumentAmount = 50; //Default 50
                //}

                result.IsReadyToTransfer = false;
                //result.ReadyToTransferDate = model.ReadyToTransferDate;
                result.IsTransferConfirmed = false;
                //result.TransferConfirmedUserID = model.TransferConfirmedUserID;
                //result.TransferConfirmedDate = model.TransferConfirmedDate;
                result.IsSentToFinance = false;
                //result.SentToFinanceDate = model.SentToFinanceDate;
                result.IsPaymentConfirmed = false;
                //result.PaymentConfirmedDate = model.PaymentConfirmedDate;
                result.IsAccountApproved = false;
                //result.AccountApprovedDate = model.AccountApprovedDate;
                result.TransferStatus = MasterCenterDropdownDTO.CreateFromModel(await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.TransferStatus && o.Key == TransferStatusKeys.WaitingForTransfer).FirstOrDefaultAsync());

                result.APAmount = 0;
                result.MeterAmount = 0;
                result.LandAmount = 0;
                result.APTotalAmount = 0;
                result.APAmountPaymented = 0;
                result.MeterAmountPaymented = 0;
                result.LandAmountPaymented = 0;
                result.APTotalAmountPaymented = 0;
                result.APTotalBalance = 0;
                result.APNetBalance = 0;

                result.LegalEntityAmount = 0;
                result.LegalEntityTotalAmount = 0;
                result.LegalEntityAmountPaymented = 0;
                result.LegalEntityTotalAmountPaymented = 0;
                result.LegalEntityTotalBalance = 0;
                result.LegalEntityNetBalance = 0;

                result.CustDocumentFee = 0;
                result.CustDocumentFeePaymented = 0;

                result.SumAPPaymented = 0;
                result.SumLegalEntityPaymented = 0;

                result.APTotalAmount = Math.Ceiling(((result.APAmount ?? 0) + (result.MeterAmount ?? 0) + (result.LandAmount ?? 0)));
                result.APTotalAmountPaymented = Math.Ceiling(((result.APAmountPaymented ?? 0) + (result.MeterAmountPaymented ?? 0) + (result.LandAmountPaymented ?? 0) + (result.APTotalBalance ?? 0)));

                result.LegalEntityTotalAmount = (result.LegalEntityAmount ?? 0);
                result.LegalEntityTotalAmountPaymented = Math.Ceiling(((result.LegalEntityAmountPaymented ?? 0) + (result.LegalEntityTotalBalance ?? 0)));

                #region "สรุปรับเงิน AP"
                result.SummaryAPPaymentList = new List<SummaryTransferPayment>();
                result.SummaryAPPaymentList.Add(
                                new SummaryTransferPayment
                                {
                                    nOrder = 1,
                                    PaymentID = "AP",
                                    PaymentName = "ค่าบ้าน",
                                    Payable = result.APAmount,
                                    Paymented = (result.APAmountPaymented ?? 0)
                                }
                            );
                result.SummaryAPPaymentList.Add(
                                new SummaryTransferPayment
                                {
                                    nOrder = 2,
                                    PaymentID = "Meter",
                                    PaymentName = "ค่ามิเตอร์",
                                    Payable = result.MeterAmount,
                                    Paymented = (result.MeterAmountPaymented ?? 0)
                                }
                            );
                result.SummaryAPPaymentList.Add(
                                new SummaryTransferPayment
                                {
                                    nOrder = 3,
                                    PaymentID = "Land",
                                    PaymentName = "จ่ายค่าที่ดิน",
                                    Payable = result.LandAmount,
                                    Paymented = (result.LandAmountPaymented ?? 0)
                                }
                            );
                result.SummaryAPPaymentList.Add(
                                new SummaryTransferPayment
                                {
                                    nOrder = 4,
                                    PaymentID = "DocumentFee",
                                    PaymentName = "ค่าดำเนินการเอกสาร",
                                    Payable = result.CustDocumentFee,
                                    Paymented = (result.CustDocumentFeePaymented ?? 0)
                                }
                            );
                result.SummaryAPPaymentList.Add(
                                new SummaryTransferPayment
                                {
                                    nOrder = 5,
                                    PaymentID = "Balance",
                                    PaymentName = "(-)ขาด/(+)เกิน",
                                    Payable = null,
                                    Paymented = (result.APTotalBalance ?? 0)
                                }
                            );
                result.SummaryAPPaymentList.Add(
                                new SummaryTransferPayment
                                {
                                    nOrder = 5,
                                    PaymentID = "Total",
                                    PaymentName = "รวม",
                                    Payable = result.APTotalAmount,
                                    Paymented = (result.APTotalAmountPaymented ?? 0)
                                }
                            );
                #endregion

                #region "สรุปรับเงิน นิติบุคคล"
                result.SummaryLegalEntityPaymentList = new List<SummaryTransferPayment>();
                result.SummaryLegalEntityPaymentList.Add(
                                new SummaryTransferPayment
                                {
                                    nOrder = 1,
                                    PaymentID = "LegalEntity",
                                    PaymentName = "ค่าสาธารณูปโภคอื่นๆ",
                                    Payable = result.LegalEntityAmount,
                                    Paymented = (result.LegalEntityAmountPaymented ?? 0)
                                }
                            );
                result.SummaryLegalEntityPaymentList.Add(
                                new SummaryTransferPayment
                                {
                                    nOrder = 2,
                                    PaymentID = "Balance",
                                    PaymentName = "(-)ขาด/(+)เกิน",
                                    Payable = null,
                                    Paymented = (result.LegalEntityTotalBalance ?? 0)
                                }
                            );
                result.SummaryLegalEntityPaymentList.Add(
                                new SummaryTransferPayment
                                {
                                    nOrder = 3,
                                    PaymentID = "Total",
                                    PaymentName = "รวม",
                                    Payable = result.LegalEntityTotalAmount,
                                    Paymented = (result.LegalEntityTotalAmountPaymented ?? 0)
                                }
                            );
                #endregion

                result.AddOnArea = 0;
                if ((result.Unit?.TitleDeed?.TitledeedArea ?? 0.00) > 0 && (result.StandardArea ?? 0) > 0)
                {
                    result.AddOnArea = (result.Unit?.TitleDeed?.TitledeedArea ?? 0.00) - (result.StandardArea ?? 0);
                }

                #region ราคาพื้นที่ต่อหน่วย

                var bookingID = model.BookingID;

                var unitPriceModel = await DB.UnitPrices
                    .Include(o => o.Booking)
                    .ThenInclude(o => o.ReferContact)
                    .Include(o => o.UnitPriceStage)
                    .Where(o => o.BookingID == bookingID
                                && o.UnitPriceStage.Key == UnitPriceStageKeys.TransferPromotion).FirstOrDefaultAsync();

                if (unitPriceModel == null)
                {
                    unitPriceModel = await DB.UnitPrices
                       .Include(o => o.Booking)
                       .ThenInclude(o => o.ReferContact)
                       .Include(o => o.UnitPriceStage)
                       .Where(o => o.BookingID == bookingID
                                   && o.UnitPriceStage.Key == UnitPriceStageKeys.Agreement).FirstOrDefaultAsync();
                }

                if (unitPriceModel != null)
                {
                    //ราคาพื้นที่ต่อหน่วย
                    decimal AreaPricePerUnit = 0;

                    //แนวราบ
                    if (model.Project.ProductType.Key == ProductTypeKeys.LowRise)
                    {
                        var PriceListItem = await DB.PriceListItems
                            .Include(o => o.PriceList)
                            .Include(o => o.MasterPriceItem)
                            .Where(o =>
                                o.PriceList.UnitID == model.UnitID
                                && o.MasterPriceItem.Key == MasterPriceItemKeys.ExtraAreaPrice
                            )
                            .OrderByDescending(o => o.PriceList.ActiveDate)
                            .FirstOrDefaultAsync() ?? new Database.Models.PRJ.PriceListItem();

                        //Master Data > Price List
                        AreaPricePerUnit = (PriceListItem.PricePerUnitAmount ?? 0);
                    }
                    //แนวสูง
                    else if (model.Project.ProductType.Key == ProductTypeKeys.HighRise)
                    {
                        //ราคาบ้านในสัญญา
                        var SellingPrice = unitPriceModel.AgreementPrice ?? 0;

                        //var TitledeedDetail = await DB.TitledeedDetails
                        //                    .Where(o => o.UnitID == model.UnitID)
                        //                    .FirstOrDefaultAsync() ?? new Database.Models.PRJ.TitledeedDetail();

                        ////พื้นที่โฉนด
                        //decimal TitledeedArea = 0; decimal.TryParse((TitledeedDetail.TitledeedArea ?? 0).ToString(), out TitledeedArea);

                        //var TitledeedAreaCall = TitledeedArea == 0 ? 1 : TitledeedArea;

                        ////ราคาบ้านในสัญญา / พื้นที่โฉนด
                        //AreaPricePerUnit = Math.Ceiling((SellingPrice / TitledeedAreaCall));

                        //พื้นที่ขาย
                        decimal StandardArea = 0; decimal.TryParse((result.StandardArea ?? 0).ToString(), out StandardArea);

                        StandardArea = (StandardArea > 0 ? StandardArea : 1);

                        //ราคาบ้านในสัญญา / พื้นที่ขาย
                        //AreaPricePerUnit = Math.Ceiling((SellingPrice / (StandardArea > 0 ? StandardArea : 1)));
                        AreaPricePerUnit = (SellingPrice / (StandardArea > 0 ? StandardArea : 1));

                    }

                    result.AreaPricePerUnit = AreaPricePerUnit;


                }

                #endregion

                var agreementConfig = await DB.AgreementConfigs
                                    .Include(o => o.LegalEntity)
                                    .Include(o => o.AttorneyNameTransfer)
                                    .Where(o => o.ProjectID == model.ProjectID)
                                    .FirstOrDefaultAsync() ?? new AgreementConfig();

                //var AttyEmployeeNo = agreementConfig.AttorneyNameTransfer?.Atty_EmployeeCode;

                var KCashCardTransfer = await DB.KCashCardTransfer
                                    .Include(o => o.ProjectOwnerByUser)
                                    .Where(o =>
                                            o.ProjectID == model.ProjectID
                                            && (o.IsKCashCard ?? false) == true
                                        )
                                    .FirstOrDefaultAsync() ?? new Database.Models.MST.KCashCardTransfer();

                var PCardUser = KCashCardTransfer.ProjectOwnerByUser;

                result.PCardUser = USR.UserListDTO.CreateFromModel(PCardUser);

                #region นิติบุคคล
                result.LegalEntity = LegalEntityDTO.CreateFromModel(agreementConfig?.LegalEntity);
                #endregion

                #region ผู้รับโอน

                result.IsOwnerAssignAuthority = titledeedRQFlow != null ? titledeedRQFlow.IsAuthorized.Value : false;
                //result.IsOwnerAssignAuthorityByCompany = titledeedRQFlow?.IsAuthorized == true ? true : false;
                if (titledeedRQFlow?.IsAuthorized == true)
                {
                    result.IsOwnerAssignAuthorityByCompany = 1;
                } else
                {
                    result.IsOwnerAssignAuthorityByCompany = 2;
                }
                result.OwnerAuthorityName = "";

                #endregion

                result.IsSuspenseChange = false;
                result.SuspenseAmount = 0;

                result.LegalEntityChangeAmount = 0;
                result.LegalEntityChangeAmountBeforeTransfer = 0;

                result.RecieveAmtOverBfTransfer = 0;
                result.RecieveAmtOverAtTransfer = 0;

                decimal CustomerPayMortgage = 0;
                decimal CompanyPayMortgage = 0;

                decimal CustomerPayFee = 0;
                decimal CompanyPayFee = 0;
                decimal FreeCompanyPayFee = 0;

                decimal FreeFee = 0;
                decimal DocumentFeeCharge = 0;
                decimal FreeDocumentFee = 0;
                decimal DocumentFee = 0;
                decimal DocumentFeeAmtForCal = 0;

                foreach (var a in transferFeeList)
                {
                    if (a.MasterPriceItem.Key == MasterPriceItemKeys.TransferFee)
                    {
                        CustomerPayFee = a.BuyerAmount;
                        CompanyPayFee = a.SellerAmount;
                        FreeCompanyPayFee = a.SellerAmount;

                        if (a.ExpenseReponsibleBy.Key == ExpenseReponsibleByKeys.Half)
                        {
                            FreeCompanyPayFee = 0;
                        }
                        else if (a.ExpenseReponsibleBy.Key == ExpenseReponsibleByKeys.Company)
                        {
                            CompanyPayFee = a.SellerAmount / 2;
                            FreeCompanyPayFee = a.SellerAmount / 2;
                        }
                    }
                    else if (a.MasterPriceItem.Key == MasterPriceItemKeys.DocumentFeeCharge)
                    {
                        FreeDocumentFee = a.SellerAmount;

                        if (a.ExpenseReponsibleBy.Key == ExpenseReponsibleByKeys.Company)
                        {
                            DocumentFeeAmtForCal = a.SellerAmount;
                            DocumentFee = DocumentFeeCharge - DocumentFeeAmtForCal;

                            DocumentFee = DocumentFee < 0 ? 0 : DocumentFee;
                        }
                        else if (a.ExpenseReponsibleBy.Key == ExpenseReponsibleByKeys.Customer)
                        {
                            DocumentFeeAmtForCal = a.BuyerAmount;
                            DocumentFee = DocumentFeeCharge - DocumentFeeAmtForCal;
                        }
                    }

                }

                CustomerPayMortgage = transferFeeList.Where(o =>
                        o.MasterPriceItem.Key == MasterPriceItemKeys.MortgageFee
                        || o.MasterPriceItem.Key == MasterPriceItemKeys.MortgageFeeOverLoan
                    ).Select(o => o.BuyerAmount).Sum();

                CompanyPayMortgage = transferFeeList.Where(o =>
                        o.MasterPriceItem.Key == MasterPriceItemKeys.MortgageFee
                        || o.MasterPriceItem.Key == MasterPriceItemKeys.MortgageFeeOverLoan
                    ).Select(o => o.SellerAmount).Sum();

                FreeFee = CompanyPayMortgage + FreeCompanyPayFee;

                result.CustomerPayMortgage = CustomerPayMortgage;
                result.CustomerPayFee = CustomerPayFee;
                result.CompanyPayFee = CompanyPayFee;

                result.FreeFee = FreeFee;
                result.DocumentFeeCharge = 0; //DocumentFeeCharge;
                result.FreeDocumentFee = FreeDocumentFee;
                result.DocumentFee = DocumentFee;

                result.DocumentFeeAmtForCal = 0;

                result.FlagUserAPPayWithMemo = false;
                result.FlagUserLegalEntityPayWithMemo = false;
                result.FlagUserAPGiveChange = false;
                result.FlagUserLegalEntityGiveChange = false;

                #region เช็คค่ามิเตอร์

                var IsFreeMeterCheque = false;

                var IsFreeElectricMeter = transferFeeList.Where(o =>
                        o.MasterPriceItem.Key == MasterPriceItemKeys.ElectricMeter
                        && (
                            o.ExpenseReponsibleBy.Key == ExpenseReponsibleByKeys.Customer
                            || o.ExpenseReponsibleBy.Key == ExpenseReponsibleByKeys.Half
                        )
                        && (o.PricePerUnit ?? 0) > 0
                    ).Any() ? false : true;

                var IsFreeWaterMeter = transferFeeList.Where(o =>
                        o.MasterPriceItem.Key == MasterPriceItemKeys.WaterMeter
                        && (
                            o.ExpenseReponsibleBy.Key == ExpenseReponsibleByKeys.Customer
                            || o.ExpenseReponsibleBy.Key == ExpenseReponsibleByKeys.Half
                        )
                        && (o.PricePerUnit ?? 0) > 0
                    ).Any() ? false : true;

                if (IsFreeElectricMeter && IsFreeWaterMeter)
                {
                    IsFreeMeterCheque = true;
                }

                //ฟรี่
                if (IsFreeMeterCheque)
                {
                    var modelMeterCheque = DB.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.MeterCheque && o.Key == MeterChequeKeys.Free).FirstOrDefault();
                    result.MeterCheque = MasterCenterDropdownDTO.CreateFromModel(modelMeterCheque);
                }
                //ไม่รวมในค่าบ้าน
                else
                {
                    var modelMeterCheque = DB.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.MeterCheque && o.Key == MeterChequeKeys.NotTotalInHouseValue).FirstOrDefault();
                    result.MeterCheque = MasterCenterDropdownDTO.CreateFromModel(modelMeterCheque);
                }

                #endregion

                return result;
            }
            else
            {
                return null;
            }
        }

        public async Task ValidateAsync(DatabaseContext db, bool IsRepeatCreate = false)
        {

            ValidateException ex = new ValidateException();
            if (IsRepeatCreate)
            {
                var msg = "มีการตั้งเรื่องโอนกรรมสิทธิ์แล้ว กรุณารีเฟรชหน้าอีกครั้ง";
                ex.AddError("ERRX001", msg, 0);
            }
            else if (!this.ScheduleTransferDate.HasValue)
            {
                var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                string desc = this.GetType().GetProperty(nameof(TransferDTO.ScheduleTransferDate)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = errMsg.Message.Replace("[field]", desc);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }
            else if (this.TransferSale == null)
            {
                var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                string desc = this.GetType().GetProperty(nameof(TransferDTO.TransferSale)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = errMsg.Message.Replace("[field]", desc);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }

            if (ex.HasError)
            {
                throw ex;
            }
        }

        public void ToModel(ref Transfer model)
        {
            model = model ?? new Transfer();

            model.ProjectID = this.Project?.Id;
            model.UnitID = this.Unit?.Id;
            model.AgreementID = this.Agreement?.Id;
            model.MeterChequeMasterCenterID = this.MeterCheque?.Id;
            model.TransferSaleUserID = this.TransferSale?.Id;

            //model.Project = this.Project;
            //model.Unit = this.Unit;
            model.TransferNo = this.TransferNo;
            //model.Agreement = this.Agreement;
            model.StandardArea = this.StandardArea ?? 0;
            model.LandArea = this.LandArea ?? 0;
            model.LandEstimatePrice = this.LandEstimatePrice ?? 0;
            //model.TransferSale = this.TransferSale;
            model.ScheduleTransferDate = this.ScheduleTransferDate.HasValue ? this.ScheduleTransferDate.Value.Date : this.ScheduleTransferDate;
            model.ActualTransferDate = this.ActualTransferDate.HasValue ? this.ActualTransferDate.Value.Date : this.ActualTransferDate;
            model.CompanyIncomeTax = this.CompanyIncomeTax ?? 0;
            model.BusinessTax = this.BusinessTax ?? 0;
            model.LocalTax = this.LocalTax ?? 0;
            model.MinistryPCard = this.MinistryPCard ?? 0;
            model.MinistryCashOrCheque = this.MinistryCashOrCheque ?? 0;
            //model.MeterCheque = this.MeterCheque;
            model.CustomerPayMortgage = this.CustomerPayMortgage ?? 0;
            model.CustomerPayFee = this.CustomerPayFee ?? 0;
            model.CompanyPayFee = this.CompanyPayFee ?? 0;
            model.FreeFee = this.FreeFee ?? 0;
            model.DocumentFee = this.DocumentFee ?? 0;
            model.APBalance = this.APBalance ?? 0;
            model.IsAPBalanceTransfer = this.IsAPBalanceTransfer ?? false;
            model.APBalanceTransfer = this.APBalanceTransfer ?? 0;
            model.APChangeAmountBeforeTransfer = this.APChangeAmountBeforeTransfer ?? 0;
            model.APChangeAmount = this.APChangeAmount ?? 0;
            model.IsAPGiveChange = this.IsAPGiveChange;
            model.APPayWithMemoMasterCenterID = this.APPayWithMemo?.Id;
            model.LegalEntityBalance = this.LegalEntityBalance ?? 0;
            model.IsLegalEntityBalanceTransfer = this.IsLegalEntityBalanceTransfer ?? false;
            model.LegalEntityBalanceTransfer = this.LegalEntityBalanceTransfer ?? 0;
            model.LegalEntityChangeAmount = (this.LegalEntityBalance ?? 0) - (this.LegalEntityBalanceTransfer ?? 0); //this.LegalEntityChangeAmount;
            model.IsLegalEntityGiveChange = this.IsLegalEntityGiveChange;
            model.LegalEntityPayWithMemoMasterCenterID = this.LegalEntityPayWithMemo?.Id;
            model.PettyCashAmount = this.PettyCashAmount ?? 0;
            model.GoTransportAmount = this.GoTransportAmount ?? 0;
            model.ReturnTransportAmount = this.ReturnTransportAmount ?? 0;
            model.LandOfficeTransportAmount = this.LandOfficeTransportAmount ?? 0;
            model.GoTollWayAmount = this.GoTollWayAmount ?? 0;
            model.ReturnTollWayAmount = this.ReturnTollWayAmount ?? 0;
            model.LandOfficeTollWayAmount = this.LandOfficeTollWayAmount ?? 0;
            model.SupportOfficerAmount = this.SupportOfficerAmount ?? 0;
            model.CopyDocumentAmount = this.CopyDocumentAmount ?? 0;
            model.IsReadyToTransfer = this.IsReadyToTransfer ?? false;
            model.ReadyToTransferDate = this.ReadyToTransferDate;
            model.IsTransferConfirmed = this.IsTransferConfirmed ?? false;
            model.TransferConfirmedUserID = this.TransferConfirmedUserID;
            model.TransferConfirmedDate = this.TransferConfirmedDate;
            //model.IsSentToFinance = this.IsSentToFinance ?? false;
            //model.SentToFinanceDate = this.SentToFinanceDate;
            model.IsPaymentConfirmed = this.IsPaymentConfirmed ?? false;
            model.PaymentConfirmedDate = this.PaymentConfirmedDate;
            model.IsAccountApproved = this.IsAccountApproved ?? false;
            model.AccountApprovedDate = this.AccountApprovedDate;

            model.APAmount = this.APAmount ?? 0;
            model.MeterAmount = this.MeterAmount ?? 0;
            model.LandAmount = this.LandAmount ?? 0;
            model.LegalEntityAmount = this.LegalEntityAmount ?? 0;

            model.SumAPPaymented = this.SumAPPaymented ?? 0;
            model.SumLegalEntityPaymented = this.SumLegalEntityPaymented ?? 0;

            model.CommonFeeCharge = this.CommonFeeCharge;
            model.CustomerNoPayAmount = this.CustomerNoPayAmount ?? 0;
            model.CustomerPayAmount = this.CustomerPayAmount ?? 0;
            model.LandAmountCharge = this.LandAmountCharge;
            model.MeterAmountCharge = this.MeterAmountCharge;
            model.TotalCustomerPayAmount = this.TotalCustomerPayAmount ?? 0;
            model.TotalPaidAmount = this.TotalPaidAmount;

            //model.AreaPricePerUnit = this.AreaPricePerUnit;
            //model.IncreasingAreaPrice = this.IncreasingAreaPrice;

            model.PCardUserID = this.PCardUser?.Id;

            model.IsSuspenseChange = this.IsSuspenseChange ?? false;
            model.SuspenseAmount = this.SuspenseAmount ?? 0;

            model.DocumentFeeCharge = this.DocumentFeeCharge ?? 0;

            model.FlagUserAPGiveChange = this.FlagUserAPGiveChange ?? false;
            model.FlagUserAPPayWithMemo = this.FlagUserAPPayWithMemo ?? false;
            model.FlagUserLegalEntityGiveChange = this.FlagUserLegalEntityGiveChange ?? false;
            model.FlagUserLegalEntityPayWithMemo = this.FlagUserLegalEntityPayWithMemo ?? false;

            if (this.IsTransferByAgent == 1)
            {
                model.IsTransferByAgent = true;
            } else
            {
                model.IsTransferByAgent = false;
            }
            

        }

        public class SummaryTransferPayment
        {
            public int nOrder { get; set; }
            public string PaymentID { get; set; }
            public string PaymentName { get; set; }
            public decimal? Payable { get; set; }
            public decimal Paymented { get; set; }
        }

        public class api_GetUnitEndProductDate_Param
        {
            public string ProjectId { get; set; }
            public string UnitNumber { get; set; }
        }

        public class api_GetUnitEndProductDate_Response
        {
            public bool? success { get; set; }
            public string messages { get; set; }
            public List<EndProductUnit> data { get; set; }
        }

        public class EndProductUnit
        {
            public string ProductID { get; set; }
            public string UnitNumber { get; set; }
            public string WBSNumber { get; set; }
            public string SAPCode { get; set; }
            public DateTime? NProductDate { get; set; }
        }

        public class api_GetUnitReceiveDate_Param
        {
            public string ProjectId { get; set; }
            public string UnitNumber { get; set; }
        }

        public class api_GetUnitReceiveDate_Response
        {
            public bool? success { get; set; }
            public string messages { get; set; }
            public List<DefectReceiveUnit> data { get; set; }
        }

        public class DefectReceiveUnit
        {
            public string ProjectNo { get; set; }
            public string SerialNo { get; set; }
            public string RAuditNo { get; set; }
            public DateTime? DocOpenDate { get; set; }
            public string TDefectDocNo { get; set; }
            public DateTime? DocReceiveUnitDate { get; set; }
            public string ContactID { get; set; }
            public string ContactName { get; set; }
            public bool? DocIsActive { get; set; }
        }

    }
}