using Base.DTOs.PRJ;
using Base.DTOs.MST;
using System;
using System.ComponentModel;
using Database.Models.DbQueries.ACC;

namespace Base.DTOs.ACC
{
    public class PostGLHeaderDTO : BaseDTO
    {
        /// <summary>
        /// เลขที่ Doc ของการ PostGL PI,RV,JV,CA
        /// </summary>
        [Description("เลขที่เอกสาร ของการ PostGL PI,RV,JV,CA")]
        public string DocumentNo { get; set; }        
        
        [Description("เลขที่เอกสารอ้างอิง")]
        public string ReferentNo { get; set; }

        /// <summary>
        /// ประเภท Doc RV,JV,PI,CA 
        /// </summary>
        [Description("ประเภท Doc RV,JV,PI,CA")]
        public MasterCenterDropdownDTO PostGLDocumentTypeMasterCenter { get; set; }

        /// <summary>
        /// Company
        /// </summary>
        [Description("Company")]
        public CompanyDropdownDTO Company { get; set; }

        /// <summary>
        /// Project
        /// </summary>
        [Description("Project")]
        public ProjectDropdownDTO Project { get; set; }

        /// <summary>
        /// Unit
        /// </summary>
        [Description("Unit")]
        public UnitDropdownDTO Unit { get; set; }

        /// <summary>
        /// บัญชีบริษัท
        /// </summary>
        [Description("บัญชีบริษัท")]
        public BankAccountDropdownDTO BankAccount { get; set; }

        /// <summary>
        /// วันที่เอกสาร
        /// </summary>
        [Description("วันที่เอกสาร/วันที่ทำรายการ")]
        public DateTime? DocumentDate { get; set; }

        /// <summary>
        /// รายละเอียด
        /// </summary>
        [Description("รายละเอียด")]
        public string Description { get; set; }

        /// <summary>
        /// วันที่ Posting date
        /// </summary>
        [Description("วันที่ Posting date")]
        public DateTime? PostingDate { get; set; }

        /// <summary>
        /// ID ของรายการที่อ้างอิงการ Post GL
        /// </summary>
        [Description("ID ของรายการที่อ้างอิงการ Post GL")]
        public Guid? ReferentID { get; set; }

        /// <summary>
        /// แหล่งข้อมูลของ ReferentID = DepositHeader,PaymentMethod,UnknowPayment,PostGLHeader(กรณี Type CA)
        /// </summary>
        [Description("แหล่งข้อมูลของ ReferentID = DepositHeader,PaymentMethod,UnknowPayment,CancelMemo,ChangeUnitWorkflow,PostGLHeader(กรณี Type CA)")]
        public string ReferentType { get; set; }

        /// <summary>
        /// หมายเหตุ
        /// </summary>
        [Description("หมายเหตุ")]
        public string Remark { get; set; }

        /// <summary>
        /// จำนวนเงินที่ Post
        /// </summary>
        [Description("จำนวนเงินที่ Post")]
        public decimal? Amount { get; set; }

        /// <summary>
        /// ค่าธรรมเนียม
        /// </summary>
        [Description("ค่าธรรมเนียม")]
        public decimal? Fee { get; set; }

        /// <summary>
        /// คงเหลือ
        /// </summary>
        [Description("คงเหลือ")]
        public decimal? RemainAmount { get; set; }

        /// <summary>
        /// โพสโดย
        /// </summary>
        [Description("โพสโดย")]
        public String CreatedBy { get; set; }

        /// <summary>
        /// วันที่โพส
        /// </summary>
        [Description("วันที่โพส")]
        public DateTime? Created { get; set; }

        /// <summary>
        /// 1=Active  0=cancel
        /// </summary>
        [Description("1=Active  0=cancel")]
        public bool? IsActive { get; set; }

        /// <summary>
        /// เลขอ้างอิงรายการที่ยกเลิก
        /// </summary>
        [Description("เลขอ้างอิงรายการที่ยกเลิก")]
        public string CancelReferentNo { get; set; }

        [Description("SumAmount")]
        public decimal? SumAmount { get; set; }

        [Description("SumFee")]
        public decimal? SumFee { get; set; }

        public static PostGLHeaderDTO CreateFromDBQuery(dbqPostGLData model)
        {
            if (model != null)
            {
                PostGLHeaderDTO result = new PostGLHeaderDTO();

                result.Id = model.PostGLHeaderID;
                result.DocumentNo = model.DocumentNo;
                result.ReferentNo = model.ReferentNo;
                result.PostGLDocumentTypeMasterCenter = new MasterCenterDropdownDTO { Id = model.PostGLTypeID ?? Guid.Empty, Name = model.PostGLTypeName, Key = model.PostGLTypeKey };
                result.Company = new CompanyDropdownDTO { Id = model.CompanyID ?? Guid.Empty, NameEN = model.CompanyName, NameTH = model.CompanyName, SAPCompanyID = model.SAPCompanyID };
                result.Project = new ProjectDropdownDTO { Id = model.ProjectID, ProjectNo = model.ProjectNo, ProjectNameTH = model.ProjectName };
                result.Unit = new UnitDropdownDTO { Id = model.UnitID ?? Guid.Empty, UnitNo = model.UnitNo };
                result.BankAccount = new BankAccountDropdownDTO { Id = model.BankAccountID, DisplayName = model.BankAccountName, BankAccountNo = model.BankAccountNo };
                result.DocumentDate = model.DocumentDate;
                result.PostingDate = model.PostingDate;
                result.Amount = model.TotalAmount;
                result.Fee = model.TotalFee;
                result.RemainAmount = model.TotalBalance;
                result.CreatedBy = model.PostedBy;
                result.Created = model.Created;
                result.IsActive = !model.IsCancel;
                result.SumAmount = model.SumAmount;
                result.SumFee = model.SumFee;
                result.CancelReferentNo = model.CancelReferentNo;

                return result;
            }
            else
            {
                return null;
            }
        }
    }
}

