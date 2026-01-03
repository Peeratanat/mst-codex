using Base.DTOs.MST;
using Database.Models;
using Database.Models.MST;
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
    public class TransferPrintFormDTO : BaseDTO
    {
        /// <summary>
        /// โอนกรรมสิทธิ์
        /// </summary>
        public TransferDropdownDTO Transfer { get; set; }

        /// <summary>
        /// ผู้รับมอบอำนาจโอนกรรมสิทธิ์
        /// </summary>
        public AttorneyTransferDTO AttorneyNameTransfer { get; set; }

        /// <summary>
        /// ผู้รับมอบอำนาจแทนบริษัท
        /// </summary>
        public string AttorneyNameCompany { get; set; }

        /// <summary>
        /// วันที่แทนตามมอบ ลว.(บริษัท)
        /// </summary>
        public DateTime? AttorneyDateCompany { get; set; }

        /// <summary>
        /// ผู้รับมอบอำนาจแทนลูกค้า
        /// </summary>
        public string AttorneyNameCustomer { get; set; }

        /// <summary>
        /// วันที่แทนตามมอบ ลว.(ลูกค้า)
        /// </summary>
        public DateTime? AttorneyDateCustomer { get; set; }

        /// <summary>
        /// ประเภท/แบบบ้าน
        /// </summary>
        public string ModelName { get; set; }

        /// <summary>
        /// วัตถุประสงค์ของการซื้อของนิติบุคคล
        /// masterdata/api/MasterCenters/DropdownList?masterCenterGroupKey=CorporateBuyingReason
        /// </summary>
        public MST.MasterCenterDropdownDTO CorporateBuyingReason { get; set; }

        /// <summary>
        /// วัตถุประสงค์ของการซื้อของนิติบุคคลอื่นๆ
        /// </summary>
        public string CorporateBuyingReasonOther { get; set; }

        /// <summary>
        /// ประเภทรั้ว
        /// masterdata/api/MasterCenters/DropdownList?masterCenterGroupKey=FenceType
        /// </summary>
        public MST.MasterCenterDropdownDTO FenceType { get; set; }

        /// <summary>
        /// ข้อ.3 อื่นๆ
        /// </summary>
        public string No3Other { get; set; }

        /// <summary>
        /// สร้างเอกสารโอนกรรมสิทธิ์
        /// </summary>
        public bool IsCreate { get; set; }


        /// <summary>
        /// สำนักงานที่ดิน
        /// </summary>
        public LandOfficeListDTO LandOffice { get; set; }
        /// <summary>
        /// แบบแสดงรายการภาษีธุรกิจเฉพาะ (ภ.ธ.40)
        /// </summary>
        public bool? HR_PT_40 { get; set; }
        /// <summary>
        /// คำขอจดทะเบียนสิทธิและนิติกรรม (ท.ด.1)
        /// </summary>
        public bool? R_TD_01 { get; set; }
        /// <summary>
        /// คำขอจดทะเบียนสิทธิและนิติกรรม (อ.ช.15)
        /// </summary>
        public bool? H_AH_15 { get; set; }
        /// <summary>
        /// บันทึกการประเมินราคาทรัพย์สิน (ท.ด.86)
        /// </summary>
        public bool? HR_TD_86 { get; set; }
        /// <summary>
        /// บันทึกถ้อยคำการชำระภาษีอากร (ท.ด.16)
        /// </summary>
        public bool? HR_TD_16 { get; set; }
        /// <summary>
        /// หนังสือสัญญาขายที่ดิน (ท.ด.13-1)สำนักงานที่ดิน
        /// </summary>
        public bool? R_TD_13_1 { get; set; }
        /// <summary>
        /// หนังสือสัญญาขายที่ดิน (อ.ช.23-1)สำนักงานที่ดิน
        /// </summary>
        public bool? H_AH_23_1 { get; set; }
        /// <summary>
        /// หนังสือสัญญาขายที่ดิน (ท.ด.13-2)ผู้ซื้อ,กรมสรรพากร
        /// </summary>
        public bool? R_TD_13_2 { get; set; }
        /// <summary>
        /// หนังสือสัญญาขายที่ดิน (อ.ช.23-2)ผู้ซื้อ,กรมสรรพากร
        /// </summary>
        public bool? H_AH_23_2 { get; set; }
        /// <summary>
        /// หนังสือสัญญาขายที่ดิน (ท.ด.13-3)ใบเปล่า
        /// </summary>
        public bool? R_TD_13_3 { get; set; }
        /// <summary>
        /// หนังสือสัญญาขายที่ดิน (อ.ช.23-3)ผู้ขาย
        /// </summary>
        public bool? H_AH_23_3 { get; set; }
        /// <summary>
        /// หนังสือสัญญาขายที่ดิน (อ.ช.23-4)ใบเปล่า
        /// </summary>
        public bool? H_AH_23_4 { get; set; }
        /// <summary>
        /// ใบรับค่าใช้จ่าย (A4)
        /// </summary>
        public bool? HR_Expenses_A4 { get; set; }
        /// <summary>
        /// ใบรับค่าใช้จ่าย (Print Dot)
        /// </summary>
        public bool? HR_Recv_Exps_PDOT { get; set; }
        /// <summary>
        /// รายงานการ์ดลูกค้า
        /// </summary>
        public bool? HR_CS_CARD { get; set; }
        /// <summary>
        /// ค่าใช้จ่าย ณ วันโอน (ภาษาไทย)
        /// </summary>
        public bool? HR_Exps_Transfer_TH { get; set; }
        /// <summary>
        /// ค่าใช้จ่าย ณ วันโอน (ภาษาอังกฤษ)
        /// </summary>
        public bool? HR_Exps_Transfer_EN { get; set; }
        /// <summary>
        /// รายงานโอน 3 ฝ่าย
        /// </summary>
        public bool? HR_TF_3DP { get; set; }
        /// <summary>
        /// บันทึกรับรองสถานะภาพ
        /// </summary>
        public bool? HR_TF_014 { get; set; }

        public async static Task<TransferPrintFormDTO> CreateFromModelAsync(TransferPrintForm model, DatabaseContext DB, bool IsCreate = false)
        {
            if (model != null)
            {
                var result = new TransferPrintFormDTO();

                result.Id = model.ID;
                result.Transfer = TransferDropdownDTO.CreateFromModel(model.Transfer);
                result.AttorneyNameTransfer = AttorneyTransferDTO.CreateFromModel(model.AttorneyNameTransfer);
                result.AttorneyNameCompany = model.AttorneyNameCompany;
                result.AttorneyDateCompany = model.AttorneyDateCompany;
                result.AttorneyNameCustomer = model.AttorneyNameCustomer;
                result.AttorneyDateCustomer = model.AttorneyDateCustomer;
                result.ModelName = model.ModelName;
                result.CorporateBuyingReason = MasterCenterDropdownDTO.CreateFromModel(model.CorporateBuyingReason);
                result.CorporateBuyingReasonOther = model.CorporateBuyingReasonOther;
                result.FenceType = MasterCenterDropdownDTO.CreateFromModel(model.FenceType);
                result.No3Other = model.No3Other;

                result.IsCreate = IsCreate;

                var modelTF = await DB.Transfers.Where(e => e.ID == model.TransferID)
                                   .Include(o => o.Project)
                                   .Include(o => o.Unit)
                            .FirstOrDefaultAsync() ?? new Transfer();

                model.Transfer = modelTF;

                //defualt checkList
                var docCheckList = await DB.LandOfficeDocCheckLists
                            .Where(o => o.LandOfficeID == model.Transfer.Unit.LandOfficeID
                                    && o.ProductTypeMasterCenterID == model.Transfer.Project.ProductTypeMasterCenterID).FirstOrDefaultAsync();

                if (docCheckList != null)
                {
                    result.HR_PT_40 = docCheckList.HR_PT_40;
                    result.R_TD_01 = docCheckList.R_TD_01;
                    result.H_AH_15 = docCheckList.H_AH_15;
                    result.HR_TD_86 = docCheckList.HR_TD_86;
                    result.HR_TD_16 = docCheckList.HR_TD_16;
                    result.R_TD_13_1 = docCheckList.R_TD_13_1;
                    result.H_AH_23_1 = docCheckList.H_AH_23_1;
                    result.R_TD_13_2 = docCheckList.R_TD_13_2;
                    result.H_AH_23_2 = docCheckList.H_AH_23_2;
                    result.R_TD_13_3 = docCheckList.R_TD_13_3;
                    result.H_AH_23_3 = docCheckList.H_AH_23_3;
                    result.H_AH_23_4 = docCheckList.H_AH_23_4;
                    result.HR_Expenses_A4 = docCheckList.HR_Expenses_A4;
                    result.HR_Recv_Exps_PDOT = docCheckList.HR_Recv_Exps_PDOT;
                    result.HR_CS_CARD = docCheckList.HR_CS_CARD;
                    result.HR_Exps_Transfer_TH = docCheckList.HR_Exps_Transfer_TH;
                    result.HR_Exps_Transfer_EN = docCheckList.HR_Exps_Transfer_EN;
                    result.HR_TF_3DP = false;
                }

                return result;
            }
            else
            {
                return null;
            }
        }

        public void ToModel(ref TransferPrintForm model)
        {
            model = model ?? new TransferPrintForm();

            model.TransferID = this.Transfer.Id;
            model.AttorneyNameTransferID = this.AttorneyNameTransfer?.Id;
            model.AttorneyNameCompany = this.AttorneyNameCompany;
            model.AttorneyDateCompany = this.AttorneyDateCompany;
            model.AttorneyNameCustomer = this.AttorneyNameCustomer;
            model.AttorneyDateCustomer = this.AttorneyDateCustomer;
            model.ModelName = this.ModelName;
            model.CorporateBuyingReasonMasterCenterID = this.CorporateBuyingReason?.Id;
            model.CorporateBuyingReasonOther = this.CorporateBuyingReasonOther;
            model.FenceTypeMasterCenterID = this.FenceType?.Id;
            model.No3Other = this.No3Other;
        }
    }
}
