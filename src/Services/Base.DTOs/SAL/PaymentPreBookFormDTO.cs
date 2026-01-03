using Base.DTOs.MST;
using Base.DTOs.SAL;
using Database.Models;
using Database.Models.DbQueries.FIN;
using Database.Models.DbQueries.Finance;
using Database.Models.DbQueries.SAL;
using Database.Models.FIN;
using Database.Models.MasterKeys;
using Database.Models.MST;
using Database.Models.PRM;
using Database.Models.SAL;
using ErrorHandling;
using FileStorage;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Base.DTOs.FIN
{
	public class PaymentPreBookFormDTO : BaseDTO
	{
		/// <summary>
		/// บริศัท
		/// </summary>
		public CompanyDropdownDTO Company { get; set; }

		/// <summary>
		/// โครงการ
		/// Project/api/Projects/DropdownList
		/// </summary>
		public PRJ.ProjectDropdownDTO Project { get; set; }

		/// <summary>
		/// แปลง
		/// Project/api/Projects/{projectID}/Units/DropdownList
		/// </summary>
		public PRJ.UnitDropdownDTO Unit { get; set; }

		/// <summary>
		/// รายการที่ต้องชำระ
		/// </summary>
		public List<PaymentUnitPriceItemDTO> PaymentItems { get; set; }

		/// <summary>
		/// วิธีการชำระ
		/// </summary>
		public List<PaymentMethodPreBookDTO> PaymentMethods { get; set; }

		/// <summary>
		/// วิธีการชำระ
		/// </summary>
		public PaymentMethodPreBookDTO PaymentMethod { get; set; }

		/// <summary>
		/// ชนิดของช่องทางชำระที่สามารถชำระได้
		/// </summary>
		public List<MasterCenterDropdownDTO> AuthorizedPaymentMethodTypes { get; set; }

		/// <summary>
		/// วันที่ชำระ
		/// </summary>
		[Description("วันที่ชำระ")]
		public DateTime ReceiveDate { get; set; }

		/// <summary>
		/// ไฟล์แนบ
		/// </summary>
		public FileDTO AttachFile { get; set; }

		/// <summary>
		/// หมายเหตุ (บันทึกข้อความ)
		/// </summary>
		public string Remark { get; set; }

		/// <summary>
		/// เหตุผลการยกเลิกจ่ายเงิน
		/// </summary>
		public string RejectReason { get; set; }

		/// <summary>
		/// หมายเหตุ (ReceiptTempHeader)
		/// </summary>
		public string CancelRemark { get; set; }

		/// <summary>
		/// ชนิดของ PaymentForm
		/// </summary>
		public PaymentFormTypePreBook PaymentFormTypePreBook { get; set; }

		/// <summary>
		/// สามารถเพิ่ม Payment Method ใหม่ได้
		/// </summary>
		public bool CanAddNewPaymentMethod { get; set; }

		/// <summary>
		/// สามารถแก้ไขยอดชำระเงินได้
		/// </summary>
		public bool CanEditPayAmount { get; set; }

		/// <summary>
		/// ID ของ UnknownPayment หรืออื่นๆ
		/// </summary>
		public Guid? RefID { get; set; }

		public bool? IsWrongAccount { get; set; }

		public Guid? PaymentID { get; set; }
		public Guid? PaymentPreBookID { get; set; }
		public Guid? PaymentMethodPreBookID { get; set; }
		/// <summary>
		/// เลข UN
		/// </summary>
		public string UnknownPaymentCode { get; set; }

		public bool? IsValidateComfirm { get; set; }

		public string ValidateComfirmMsg { get; set; }

		public bool? IsPaidExcess { get; set; }
		public bool? IsReturnTranfer { get; set; }

		public bool IsFromLC { get; set; } = false;

		public Guid? BookingID { get; set; }
		public string CustomerFirstName { get; set; }
		public string CustomerLastName { get; set; }
		public string CustomerCitizenIdentityNo { get; set; }
		public decimal BookingAmount { get; set; }
		public Guid? QuotationID { get; set; }
		public decimal TotalAmount { get; set; }
		public bool IsPaid { get; set; }
		public string ReceiptPrebookNo { get; set; }
		public bool IsCancel { get; set; }
		public string PaymentItemName { get; set; }
		public string PaymentMethodName { get; set; }
		public string PostGLDocumentNo { get; set; }
		public DateTime? PostGLDate { get; set; }
		public bool? IsPostGL { get; set; }
		public string PostGLStatus { get; set; }



		public string BookingSTID { get; set; }
		public string QuotationSTID { get; set; }
		public string QuotationID_FromPrebook { get; set; }
		public string ReceiptTempHeaderID { get; set; }
		public string PaymentSTID { get; set; }
		public bool IsDeleted { get; set; }

		public static PaymentPreBookFormDTO CreateFromPaymentPrebookDataAsync(QuotationPreBookQueryResult model)
		{
			if (model.PaymentPrebook != null)
			{
				var result = new PaymentPreBookFormDTO();
				result.Id = model.PaymentPrebook.ID;
				result.ReceiveDate = model.PaymentPrebook.ReceiveDate;
				result.CustomerFirstName = model.PaymentPrebook.CustomerFirstName;
				result.CustomerLastName = model.PaymentPrebook.CustomerLastName;
				result.CustomerCitizenIdentityNo = model.PaymentPrebook.CustomerCitizenIdentityNo;
				result.TotalAmount = model.PaymentPrebook.TotalAmount;
				result.Remark = model.PaymentPrebook.Remark;
				result.QuotationID = model.PaymentPrebook.QuotationID;
				//result.AttachFile = modelPaymentPrebook.AttachFile;
				//result.AttachFileName = modelPaymentPrebook.AttachFileName;		
				result.ReceiptPrebookNo = model.PaymentPrebook.ReceiptPrebookNo;
				result.IsCancel = model.PaymentPrebook.IsCancel;
				result.PaymentItemName = model.PaymentPrebook.PaymentItemName;
				result.PaymentMethodName = model.PaymentPrebook.PaymentMethodName;
				result.PostGLDocumentNo = model.PaymentPrebook.PostGLDocumentNo;
				result.PostGLDate = model.PaymentPrebook.PostGLDate;
				result.IsFromLC = model.PaymentPrebook.IsFromLC;
				result.RejectReason = model.PaymentPrebook.RejectReason;
				result.IsPostGL = model.PaymentPrebook.IsPostGL;
				result.IsDeleted = model.PaymentPrebook.IsDeleted;
				if (model.PaymentPrebook.IsPostGL == true)
				{
					result.PostGLStatus = "Post แล้ว";
				}
				else
				{
					result.PostGLStatus = "ยังไม่ Post";
				}


				result.PaymentMethod = PaymentMethodPreBookDTO.CreateFromListModel(model);
				return result;
			}
			else
			{
				return null;
			}

		}
		public static async Task<PaymentPreBookFormDTO> CreateFromPaymentPrebookAsync(Quotation model, DatabaseContext db, DbQueryContext dbq, Guid? refID = null, PaymentFormTypePreBook formType = PaymentFormTypePreBook.Normal, decimal paidAmount = 0)
		{
			if (model != null)
			{
				var priceList = await db.GetActivePriceListAsync(model.UnitID);
				var result = new PaymentPreBookFormDTO();

				result.Company = CompanyDropdownDTO.CreateFromModel(model.Project?.Company);
				result.Project = PRJ.ProjectDropdownDTO.CreateFromModel(model.Project);
				result.Unit = PRJ.UnitDropdownDTO.CreateFromModel(model.Unit);
				result.PaymentMethods = new List<PaymentMethodPreBookDTO>();
				result.RefID = refID;
				result.CanAddNewPaymentMethod = formType == PaymentFormTypePreBook.Normal ? true : false;
				result.CanEditPayAmount = formType == PaymentFormTypePreBook.Normal ? true : false;
				result.AuthorizedPaymentMethodTypes = new List<MasterCenterDropdownDTO>();

				if (formType == PaymentFormTypePreBook.Normal)
				{
					var allPaymentBooking = PaymentMethodKeys.NeedToDepositKeys;
					var masterCenterAuthorizeds = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.PaymentMethod && allPaymentBooking.Contains(o.Key)).ToListAsync();
					result.AuthorizedPaymentMethodTypes.AddRange(masterCenterAuthorizeds.Select(o => MST.MasterCenterDropdownDTO.CreateFromModel(o)).ToList());
				}
				else if (formType == PaymentFormTypePreBook.UnknownPayment)
				{
					var masterCenterAuthorized = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.PaymentMethod && o.Key == PaymentMethodKeys.UnknowPayment).FirstAsync();
					result.AuthorizedPaymentMethodTypes.Add(MST.MasterCenterDropdownDTO.CreateFromModel(masterCenterAuthorized));
					result.PaymentMethods = new List<PaymentMethodPreBookDTO> { new PaymentMethodPreBookDTO { PayAmount = paidAmount, PaymentMethodType = MST.MasterCenterDropdownDTO.CreateFromModel(masterCenterAuthorized) } };
				}


				result.BookingAmount = (decimal)(priceList != null ? priceList.PriceListItems.Find(o => o.MasterPriceItemID == MasterPriceItemIDs.BookingAmount)?.Amount : 0);

				return result;
			}
			else
			{
				return null;
			}

		}
		public static async Task<PaymentPreBookFormDTO> CreateFromPaymentPrebookUpdateAsync(Quotation model, PaymentPrebook modelPaymentPrebook, PaymentMethodPrebook modelPaymentMethodPrebook, DatabaseContext db, DbQueryContext dbq, Guid? refID = null, PaymentFormTypePreBook formType = PaymentFormTypePreBook.Normal, decimal paidAmount = 0)
		{
			if (model != null)
			{
				var priceList = await db.GetActivePriceListAsync(model.UnitID);
				var result = new PaymentPreBookFormDTO();
				result.Id = modelPaymentPrebook.ID;
				result.ReceiveDate = modelPaymentPrebook.ReceiveDate;
				result.CustomerFirstName = modelPaymentPrebook.CustomerFirstName;
				result.CustomerLastName = modelPaymentPrebook.CustomerLastName;
				result.CustomerCitizenIdentityNo = modelPaymentPrebook.CustomerCitizenIdentityNo;
				result.TotalAmount = modelPaymentPrebook.TotalAmount;
				result.Remark = modelPaymentPrebook.Remark;

				result.Company = CompanyDropdownDTO.CreateFromModel(model.Project?.Company);
				result.Project = PRJ.ProjectDropdownDTO.CreateFromModel(model.Project);
				result.Unit = PRJ.UnitDropdownDTO.CreateFromModel(model.Unit);
				result.PaymentMethods = new List<PaymentMethodPreBookDTO>();
				result.RefID = refID;
				result.CanAddNewPaymentMethod = formType == PaymentFormTypePreBook.Normal ? true : false;
				result.CanEditPayAmount = formType == PaymentFormTypePreBook.Normal ? true : false;
				result.AuthorizedPaymentMethodTypes = new List<MasterCenterDropdownDTO>();

				if (formType == PaymentFormTypePreBook.Normal)
				{
					var allPaymentBooking = PaymentMethodKeys.NeedToDepositKeys;
					var masterCenterAuthorizeds = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.PaymentMethod && allPaymentBooking.Contains(o.Key)).ToListAsync();
					result.AuthorizedPaymentMethodTypes.AddRange(masterCenterAuthorizeds.Select(o => MST.MasterCenterDropdownDTO.CreateFromModel(o)).ToList());
				}
				else if (formType == PaymentFormTypePreBook.UnknownPayment)
				{
					var masterCenterAuthorized = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.PaymentMethod && o.Key == PaymentMethodKeys.UnknowPayment).FirstAsync();
					result.AuthorizedPaymentMethodTypes.Add(MST.MasterCenterDropdownDTO.CreateFromModel(masterCenterAuthorized));
					result.PaymentMethods = new List<PaymentMethodPreBookDTO> { new PaymentMethodPreBookDTO { PayAmount = paidAmount, PaymentMethodType = MST.MasterCenterDropdownDTO.CreateFromModel(masterCenterAuthorized) } };
				}


				//var masterCenterAuthorized = await db.MasterCenters.Where(o => o.ID == modelPaymentMethodPrebook.PaymentMethodTypeMasterCenterID).FirstOrDefaultAsync();

				result.PaymentMethods = new List<PaymentMethodPreBookDTO> { new PaymentMethodPreBookDTO
					{
						//Id = modelPaymentMethodPrebook.ID,
						//Bank =  BankDropdownDTO.CreateFromModel(modelPaymentMethodPrebook.Bank),
						//BankAccount = BankAccountDropdownDTO.CreateFromModel(modelPaymentMethodPrebook.BankAccount),
						//BankBranchName = modelPaymentMethodPrebook.BankBranchName,
						////BillPaymentDetailID = modelPaymentMethodPrebook.BillPaymentDetailID,
						////CancelRemark = modelPaymentMethodPrebook.CancelRemark,
						////ChangeWorkFlowID = modelPaymentMethodPrebook.ChangeWorkFlowID,
						////ChequeDate = model.ChequeDate,
						//PaymentMethodType = MST.MasterCenterDropdownDTO.CreateFromModel(masterCenterAuthorized)					
					}
				};

				result.PaymentMethod = PaymentMethodPreBookDTO.CreateFromModel(modelPaymentMethodPrebook);
				result.BookingAmount = priceList != null ? priceList.PriceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemIDs.BookingAmount).Select(o => o.Amount).FirstOrDefault() : 0;
				result.TotalAmount = modelPaymentPrebook.TotalAmount;

				return result;
			}
			else
			{
				return null;
			}

		}

		public static PaymentPreBookFormDTO CreateFromBookingBillPayment(Booking model, DatabaseContext db, FileHelper fileHelper, Guid? refID = null, PaymentFormTypePreBook formType = PaymentFormTypePreBook.Normal, decimal paidAmount = 0)
		{
			if (model != null)
			{
				var result = new PaymentPreBookFormDTO();

				result.Company = new CompanyDropdownDTO { Id = model.Project.CompanyID ?? Guid.Empty };
				result.Project = PRJ.ProjectDropdownDTO.CreateFromModel(model.Project);
				result.Unit = PRJ.UnitDropdownDTO.CreateFromModel(model.Unit);
				result.PaymentItems = new List<PaymentUnitPriceItemDTO>();
				result.PaymentMethods = new List<PaymentMethodPreBookDTO>();
				result.RefID = refID;
				result.CanAddNewPaymentMethod = formType == PaymentFormTypePreBook.Normal ? true : false;
				result.CanEditPayAmount = formType == PaymentFormTypePreBook.Normal ? true : false;
				result.AuthorizedPaymentMethodTypes = new List<MasterCenterDropdownDTO>();

				return result;
			}
			else
			{
				return null;
			}

		}

		public static PaymentPreBookFormDTO CreateFromSqlBookingForPayment(sqlBookingForPayment.QueryResult model)
		{
			if (model != null)
			{
				var result = new PaymentPreBookFormDTO();

				result.Company = new CompanyDropdownDTO { Id = model.CompanyID ?? Guid.Empty };
				result.Project = new PRJ.ProjectDropdownDTO();
				result.Unit = new PRJ.UnitDropdownDTO();
				result.PaymentItems = new List<PaymentUnitPriceItemDTO>();
				result.PaymentMethods = new List<PaymentMethodPreBookDTO>();
				result.CanAddNewPaymentMethod = false;
				result.CanEditPayAmount = false;
				result.AuthorizedPaymentMethodTypes = new List<MasterCenterDropdownDTO>();
				return result;
			}
			else
			{
				return null;
			}

		}

		public async Task ValidateBeforeUpdateAsync(int UpdateType, Guid? PaymentID, DatabaseContext db)
		{
			ValidateException ex = new ValidateException();

			if (UpdateType == 0)
				return;

			var msg = new ErrorMessage();
			bool NotFindPayment = false;
			bool ChkDeposit = false;
			bool ChkIsCancel = false;
			bool ChkPostRv = false;
			bool ChkACApprove = false;
			bool ChkChangeUnit = false;
			bool ChkPaymentStateTranfer = false;
			bool ChkOldReciveChangeUnit = false; //เป็นใบเสร็จเก่าจากการย้ายแปลง

			var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0092").FirstOrDefaultAsync();

			var modelPayment = db.Payments.Include(x => x.PaymentState).Where(x => x.ID == PaymentID).FirstOrDefault();

			if (modelPayment == null)
			{
				NotFindPayment = true;
			}
			else
			{
				if (!string.IsNullOrEmpty(modelPayment.DepositNo))
				{
					var PaymentMethods = await db.PaymentMethods.Include(x => x.PaymentMethodType).Where(x => x.PaymentID == PaymentID && PaymentMethodKeys.IsDepositMethodType.Contains(x.PaymentMethodType.Key)).FirstOrDefaultAsync();
					if (PaymentMethods != null)
					{
						ChkDeposit = true;
					}
				}
				if (modelPayment.IsCancel)
				{
					ChkIsCancel = true;
				}
				if (!string.IsNullOrEmpty(modelPayment.PostGLDocumentNo))
				{
					ChkPostRv = true;
				}

				var TransferModel = await db.Transfers.Include(x => x.Agreement).Where(x => x.Agreement.BookingID == modelPayment.BookingID).FirstOrDefaultAsync();
				if (TransferModel != null)
				{
					if (TransferModel.IsAccountApproved)
					{
						ChkACApprove = true;
					}
				}

				var PaymentMethodChangeUnit = await db.PaymentMethods.Include(x => x.PaymentMethodType).Where(x => x.PaymentID == PaymentID && x.PaymentMethodType.Key == PaymentMethodKeys.ChangeContract).FirstOrDefaultAsync();
				if (PaymentMethodChangeUnit != null)
				{
					ChkChangeUnit = true;
				}

				if (PaymentStateKeys.Transfer.Equals(modelPayment.PaymentState?.Key))
				{
					ChkPaymentStateTranfer = true;
				}

				var BookingModel = await db.Bookings.Where(x => x.ID == modelPayment.BookingID).IgnoreQueryFilters().FirstOrDefaultAsync() ?? new Booking();
				if (BookingModel.IsCancelled == true || BookingModel.IsDeleted == true)
				{
					ChkOldReciveChangeUnit = true;
				}
			}

			if (NotFindPayment || ChkDeposit || ChkIsCancel || ChkPostRv || ChkACApprove
				|| ChkChangeUnit || ChkPaymentStateTranfer || ChkOldReciveChangeUnit)
			{
				if (UpdateType == 2)
				{
					if (NotFindPayment)
					{
						ex.AddError(errMsg.Key, "ไม่สามารถลบรายการได้ เนื่องจากไม่พบข้อมูล", (int)errMsg.Type);
					}
					else if (ChkPaymentStateTranfer)
					{
						ex.AddError(errMsg.Key, "ไม่สามารถยกเลิกใบเสร็จได้ เนื่องจากเป็นใบเสร็จที่เกิดจากการรับชำระเงินหลังโอน", (int)errMsg.Type);
					}
					else if (ChkOldReciveChangeUnit)
					{
						ex.AddError(errMsg.Key, "ไม่สามารถยกเลิกใบเสร็จได้ เนื่องจากเป็นใบเสร็จที่ย้ายแปลงแล้ว", (int)errMsg.Type);
					}
					else if (ChkChangeUnit)
					{
						ex.AddError(errMsg.Key, "ไม่สามารถยกเลิกใบเสร็จได้ เนื่องจากเป็นใบเสร็จจากการย้ายแปลง", (int)errMsg.Type);
					}
					else if (ChkDeposit)
					{
						ex.AddError(errMsg.Key, "ใบเสร็จรายการนี้ได้นำฝากแล้ว ไม่สามารถยกเลิกได้ \n โปรดติดต่อทางการเงิน", (int)errMsg.Type);
					}
					else if (ChkIsCancel)
					{
						ex.AddError(errMsg.Key, "รายการที่เลือกถูกยกเลิกแล้ว \n ไม่สามารถดำเนินการได้", (int)errMsg.Type);
					}
					else if (ChkACApprove)
					{
						ex.AddError(errMsg.Key, "ไม่สามารถลบรายการได้ เนื่องจากเป็นห้องที่บัญชีอนุมัติแล้ว", (int)errMsg.Type);
					}
					else if (ChkPostRv)
					{
						this.IsValidateComfirm = true;
						this.ValidateComfirmMsg = "ใบเสร็จรายการนี้ Post RV แล้ว \n ต้องการยกเลิกใบเสร็จหรือไม่";
					}
				}
				else
				{
					if (NotFindPayment)
					{
						ex.AddError(errMsg.Key, "ไม่สามารถแก้ไขรายการได้ เนื่องจากไม่พบข้อมูล", (int)errMsg.Type);
					}
					else if (ChkPaymentStateTranfer)
					{
						ex.AddError(errMsg.Key, "ไม่สามารถแก้ไขใบเสร็จได้ เนื่องจากเป็นใบเสร็จที่เกิดจากการรับชำระเงินหลังโอน", (int)errMsg.Type);
					}
					else if (ChkChangeUnit)
					{
						ex.AddError(errMsg.Key, "ไม่สามารถแก้ไขใบเสร็จได้ เนื่องจากเป็นใบเสร็จจากการย้ายแปลง", (int)errMsg.Type);
					}
					else if (ChkDeposit)
					{
						ex.AddError(errMsg.Key, "ใบเสร็จรายการนี้ได้นำฝากแล้ว \n ไม่สามารถแก้ไขได้", (int)errMsg.Type);
					}
					else if (ChkIsCancel)
					{
						ex.AddError(errMsg.Key, "รายการที่เลือกถูกยกเลิกแล้ว \n ไม่สามารถแก้ไขได้", (int)errMsg.Type);
					}
					else if (ChkPostRv)
					{
						this.IsValidateComfirm = true;
						this.ValidateComfirmMsg = "ใบเสร็จรายการนี้ Post RV แล้ว \n ต้องการแก้ไขใบเสร็จหรือไม่";
					}
				}
			}
			if (ex.HasError)
				throw ex;
		}

		public static PaymentPreBookFormDTO CreateFromConvertPYToPresaleModel(dbqConvertPYToPresale model)
		{
			if (model != null)
			{
				PaymentPreBookFormDTO result = new PaymentPreBookFormDTO()
				{
					BookingSTID = model.BookingID,
					QuotationSTID = model.QuotationID,
					QuotationID_FromPrebook = model.QuotationID_FromPrebook,
					PaymentSTID = model.PaymentID,
					ReceiptTempHeaderID = model.ReceiptTempHeaderID

				};

				return result;
			}
			else
			{
				return null;
			}
		}

		public static PaymentPreBookFormDTO CreateFromCancleMemoPreBookModel(PaymentPrebook model)
		{

			if (model != null)
			{
				PaymentPreBookFormDTO result = new PaymentPreBookFormDTO()
				{
					CustomerFirstName = model.CustomerFirstName,
					CustomerLastName = model.CustomerFirstName,
					TotalAmount = model.TotalAmount,
					PaymentPreBookID = model.ID

				};

				return result;
			}
			else
			{
				return null;
			}
		}

		public static async Task<PaymentPreBookFormDTO> CreateFromTotalAmountPreBookModel(PaymentPrebook model)
		{

			if (model != null)
			{
				PaymentPreBookFormDTO result = new PaymentPreBookFormDTO()
				{
					TotalAmount = model.TotalAmount,
					PaymentPreBookID = model.ID
				};

				return result;
			}
			else
			{
				return null;
			}
		}

	}

	public enum PaymentFormTypePreBook
	{
		Normal = 0,
		UnknownPayment = 1,
		ChangeUnit = 2,
		DirectCredit = 3,
		DirectDebit = 4,
		BillPayment = 5,
		PreTransfer = 6,
	}

	public class UnitPaymentResultPreBook
	{
		public Guid BookingID { get; set; }
		public MasterPriceItem MasterPrice { get; set; }
		public int Period { get; set; }
		public decimal? PayAmount { get; set; }
		public DateTime? PaymentDate { get; set; }
	}

	public class UnitPriceItemWithPromotionPreBook
	{
		public UnitPriceItem UnitPriceItem { get; set; }
		public MasterPriceItem MasterPriceItem { get; set; }
		public SalePromotionExpense SalePromotionExpense { get; set; }
		public MasterCenter UnitPriceStage { get; set; }
	}

	public class PaymentMethodItemTextPreBook
	{
		public Guid? PaymentMethodID { get; set; }
		public string ItemText { get; set; }
	}
}
