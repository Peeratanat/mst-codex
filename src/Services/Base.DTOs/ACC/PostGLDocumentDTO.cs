using Base.DTOs.PRJ;
using Database.Models.ACC;
using Database.Models.FIN;
using Database.Models.MasterKeys;
using Database.Models.MST;
using Database.Models.PRJ;
using Database.Models.SAL;
using Database.Models.USR;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Base.DTOs.ACC
{
	public class PostGLDocumentDTO
	{
		/// <summary>
		/// ประเภทการโพส
		/// </summary>
		public string PostGLType { get; set; }

		/// <summary>
		/// PostGLHeaderID
		/// </summary>
		public Guid PostGLHeaderID { get; set; }

		/// <summary>
		/// เลขที่ Post
		/// </summary>
		public string DocumentNo { get; set; }

		/// <summary>
		/// PostingDate
		/// </summary>
		public DateTime PostingDate { get; set; }

		/// <summary>
		/// ผู้ทำรายการ
		/// </summary>
		public string CreateBy { get; set; }

		/// <summary>
		/// วันที่ทำรายการ
		/// </summary>
		public DateTime? CreateDate { get; set; }

		/// <summary>
		/// IsCancel ?
		/// </summary>
		public bool IsCancel { get; set; }

		/// <summary>
		/// CA Detail
		/// </summary>
		public CA_Detail CA_Detail { get; set; }

		/// <summary>
		/// ฟอร์ม RV
		/// </summary>
		public List<PostGLDocument_RV> PostGLDocument_RV { get; set; }

		/// <summary>
		/// ฟอร์ม PI/UN
		/// </summary>
		public List<PostGLDocument_PI_UN> PostGLDocument_PI_UN { get; set; }

		/// <summary>
		/// ฟอร์ม JV
		/// </summary>
		public PostGLDocument_JV PostGLDocument_JV { get; set; }

		/// <summary>
		/// ฟอร์ม CA
		/// </summary>
		public PostGLDocument_CA PostGLDocument_CA { get; set; }

		/// <summary>
		/// ฟอร์ม RF
		/// </summary>
		public List<PostGLDocument_RF> PostGLDocument_RF { get; set; }

		/// <summary>
		/// รายละเอียด DR/CR
		/// </summary>
		public List<PostGLDocumentDetail> PostGLDocumentDetail { get; set; }

		///// <summary>
		///// welcomehomeamount
		///// </summary>
		//public decimal? WelcomeHomeAmount { get; set; }

		/// <summary>
		/// SumTotalAmount
		/// </summary>
		public decimal? SumTotalAmount { get; set; }
		///// <summary>
		///// welcomehome_flag
		///// </summary>
		//public string WelcomehomeFlag { get; set; }
		///// <summary>
		///// เลขที่โอน
		///// </summary>
		//public string TransferNumber { get; set; }

		///// <summary>
		///// วันที่โอนกรรมสิทธิ์
		///// </summary>
		//public DateTime? TransferDateApprove { get; set; }

		///// <summary>
		///// TotalAmount
		///// </summary>
		//public decimal? TotalAmount { get; set; }

		/// <summary>
		/// ชื่อบริษัท TH
		/// </summary>
		public string NameTH { get; set; }

		/// <summary>
		/// ชื่อบริษัท EN
		/// </summary>
		public string NameEN { get; set; }

		public static PostGLDocumentDTO CreateFromQueryResult_RV(List<QueryResult_PostGLDocument_RV> models)
		{
			var result = new PostGLDocumentDTO();

			if (models.Any())
			{
				var result_RVs = new List<PostGLDocument_RV>();

				var model = models.FirstOrDefault();

				result.PostGLType = PostGLDocumentTypeKeys.RV;
				result.PostGLHeaderID = model.PostGLHeader?.ID ?? Guid.Empty;
				result.DocumentNo = model.PostGLHeader?.DocumentNo;
				result.PostingDate = model.PostGLHeader?.PostingDate ?? DateTime.Now;
				result.CreateBy = model.PostGLHeader?.CreatedBy?.DisplayName;
				result.CreateDate = model.PostGLHeader?.Created;
				result.IsCancel = model.PostGLHeader?.IsCancel ?? false;

				result_RVs = models.Select(o => new PostGLDocument_RV
				{
					ReceiptTempNo = o.Payment?.ReceiptTempNo,
					ReceiveDate = o.Payment?.ReceiveDate,
					DepositNo = o.PaymentMethod?.DepositNo,
					DepositDate = o.PaymentMethod?.DepositDate,
					Project = ProjectDropdownDTO.CreateFromModel(o.Project),
					Unit = UnitDropdownDTO.CreateFromModel(o.Unit),
					PaymentMethodType = o.PaymentMethodType?.Name,
					MethodAmount = o.PaymentMethod?.PayAmount ?? 0
				}).ToList();

				result.PostGLDocument_RV = result_RVs;

				return result;
			}
			else
			{
				return new PostGLDocumentDTO();
			}
		}

		public static PostGLDocumentDTO CreateFromQueryResult_AC_RV(List<QueryResult_PostGLDocument_AC_RV> models)
		{
			var result = new PostGLDocumentDTO();

			if (models.Any())
			{
				var result_RVs = new List<PostGLDocument_RV>();

				var model = models.FirstOrDefault();

				result.PostGLType = PostGLDocumentTypeKeys.RV;
				result.PostGLHeaderID = model.PostGLHeader?.ID ?? Guid.Empty;
				result.DocumentNo = model.PostGLHeader?.DocumentNo;
				result.PostingDate = model.PostGLHeader?.PostingDate ?? DateTime.Now;
				result.CreateBy = model.PostGLHeader?.CreatedBy?.DisplayName;
				result.CreateDate = model.PostGLHeader?.Created;
				result.IsCancel = model.PostGLHeader?.IsCancel ?? false;

				result_RVs = models.Select(o => new PostGLDocument_RV
				{
					ReceiptTempNo = "auto cancel",
					ReceiveDate = o.Payment?.ReceiveDate,
					//DepositNo = o.PaymentMethod?.DepositNo,
					//DepositDate = o.PaymentMethod?.DepositDate,
					Project = ProjectDropdownDTO.CreateFromModel(o.Project),
					Unit = UnitDropdownDTO.CreateFromModel(o.Unit),
					PaymentMethodType = o.PaymentMethodType?.Name,
					MethodAmount = o.Payment?.Amount ?? 0
				}).ToList();

				result.PostGLDocument_RV = result_RVs;

				return result;
			}
			else
			{
				return new PostGLDocumentDTO();
			}
		}

		public static PostGLDocumentDTO CreateFromQueryResult_PI(List<QueryResult_PostGLDocument_PI> models)
		{
			var result = new PostGLDocumentDTO();

			if (models.Any())
			{
				var result_PI_UNs = new List<PostGLDocument_PI_UN>();

				var model = models.FirstOrDefault();

				result.PostGLType = PostGLDocumentTypeKeys.PI;
				result.PostGLHeaderID = model.PostGLHeader?.ID ?? Guid.Empty;
				result.DocumentNo = model.PostGLHeader?.DocumentNo;
				result.PostingDate = model.PostGLHeader?.PostingDate ?? DateTime.Now;
				result.CreateBy = model.PostGLHeader?.CreatedBy?.DisplayName;
				result.CreateDate = model.PostGLHeader?.Created;
				result.IsCancel = model.PostGLHeader?.IsCancel ?? false;

				result_PI_UNs = models.Select(o => new PostGLDocument_PI_UN
				{
					ReceiptTempNo = o.Payment?.ReceiptTempNo,
					ReceiveDate = o.Payment?.ReceiveDate,
					RV_DocumentNo = o.Payment?.PostGLDocumentNo,
					RV_DocumentDate = o.Payment?.PostGLDate,
					Project = ProjectDropdownDTO.CreateFromModel(o.Project),
					Unit = UnitDropdownDTO.CreateFromModel(o.Unit),
					PaymentMethodType = o.PaymentMethod?.PaymentMethodType?.Name,
					Amount = o.PaymentMethod?.PayAmount ?? 0,
				}).ToList();

				result.PostGLDocument_PI_UN = result_PI_UNs;

				return result;
			}
			else
			{
				return new PostGLDocumentDTO();
			}
		}

		public static PostGLDocumentDTO CreateFromQueryResult_PB(List<QueryResult_PostGLDocument_PB> models)
		{
			var result = new PostGLDocumentDTO();

			if (models.Any())
			{
				var result_PI_UNs = new List<PostGLDocument_PI_UN>();

				var model = models.FirstOrDefault();

				result.PostGLType = PostGLDocumentTypeKeys.PI;
				result.PostGLHeaderID = model.PostGLHeader?.ID ?? Guid.Empty;
				result.DocumentNo = model.PostGLHeader?.DocumentNo;
				result.PostingDate = model.PostGLHeader?.PostingDate ?? DateTime.Now;
				result.CreateBy = model.PostGLHeader?.CreatedBy?.DisplayName;
				result.CreateDate = model.PostGLHeader?.Created;
				result.IsCancel = model.PostGLHeader?.IsCancel ?? false;

				result_PI_UNs = models.Select(o => new PostGLDocument_PI_UN
				{
					ReceiptTempNo = o.Payment?.ReceiptPrebookNo,
					ReceiveDate = o.Payment?.ReceiveDate,
					RV_DocumentNo = o.Payment?.PostGLDocumentNo,
					RV_DocumentDate = o.Payment?.PostGLDate,
					Project = ProjectDropdownDTO.CreateFromModel(o.Project),
					Unit = UnitDropdownDTO.CreateFromModel(o.Unit),
					PaymentMethodType = o.PaymentMethod?.PaymentMethodType?.Name,
					Amount = o.PaymentMethod?.PayAmount ?? 0,
				}).ToList();

				result.PostGLDocument_PI_UN = result_PI_UNs;

				return result;
			}
			else
			{
				return new PostGLDocumentDTO();
			}
		}

		public static PostGLDocumentDTO CreateFromQueryResult_UN(QueryResult_PostGLDocument_UN model)
		{
			var result = new PostGLDocumentDTO();

			if (model != null)
			{
				result.PostGLType = PostGLDocumentTypeKeys.UN;
				result.PostGLHeaderID = model.PostGLHeader?.ID ?? Guid.Empty;
				result.DocumentNo = model.PostGLHeader?.DocumentNo;
				result.PostingDate = model.PostGLHeader?.PostingDate ?? DateTime.Now;
				result.CreateBy = model.PostGLHeader?.CreatedBy?.DisplayName;
				result.CreateDate = model.PostGLHeader?.Created;
				result.IsCancel = model.PostGLHeader?.IsCancel ?? false;

				var result_UN = new PostGLDocument_PI_UN
				{
					ReceiptTempNo = (model.UnknownPayment?.BookingID.HasValue ?? false) ? model.UnknownPayment?.UnknownPaymentCode : "",
					ReceiveDate = model.UnknownPayment?.ReceiveDate,
					Project = ProjectDropdownDTO.CreateFromModel(model.Project),
					Unit = UnitDropdownDTO.CreateFromModel(model.Unit),
					PaymentMethodType = model.PaymentMethodType?.Name,
					Amount = model.UnknownPayment?.Amount ?? 0
				};

				result.PostGLDocument_PI_UN = new List<PostGLDocument_PI_UN>();
				result.PostGLDocument_PI_UN.Add(result_UN);

				return result;
			}
			else
			{
				return new PostGLDocumentDTO();
			}
		}

		public static PostGLDocumentDTO CreateFromQueryResult_JV_ChangeUnit(QueryResult_PostGLDocument_JV_ChangeUnit model)
		{
			var result = new PostGLDocumentDTO();

			if (model != null)
			{
				result.PostGLType = PostGLDocumentTypeKeys.JV + "_ChangeUnit";
				result.PostGLHeaderID = model.PostGLHeader?.ID ?? Guid.Empty;
				result.DocumentNo = model.PostGLHeader?.DocumentNo;
				result.PostingDate = model.PostGLHeader?.PostingDate ?? DateTime.Now;
				result.CreateBy = model.PostGLHeader?.CreatedBy?.DisplayName;
				result.CreateDate = model.PostGLHeader?.Created;
				result.IsCancel = model.PostGLHeader?.IsCancel ?? false;

				var result_JV = new PostGLDocument_JV
				{
					BookingDate = model.FromBooking?.BookingDate,
					CustomerName = model.FromBooking?.MainOwnerName,
					Project = ProjectDropdownDTO.CreateFromModel(model.Project),
					Unit_old = UnitDropdownDTO.CreateFromModel(model.Unit_Old),
					Unit_new = UnitDropdownDTO.CreateFromModel(model.Unit_New),
					TotalPaidAmount = model.ChangeUnitWorkflow?.TotalPaid ?? 0
				};

				result.PostGLDocument_JV = result_JV;

				return result;
			}
			else
			{
				return new PostGLDocumentDTO();
			}
		}

		public static PostGLDocumentDTO CreateFromQueryResult_JV_Cancel(QueryResult_PostGLDocument_JV_Cancel model)
		{
			var result = new PostGLDocumentDTO();

			if (model != null)
			{
				result.PostGLType = PostGLDocumentTypeKeys.JV + "_CancelUnit";
				result.PostGLHeaderID = model.PostGLHeader?.ID ?? Guid.Empty;
				result.DocumentNo = model.PostGLHeader?.DocumentNo;
				result.PostingDate = model.PostGLHeader?.PostingDate ?? DateTime.Now;
				result.CreateBy = model.PostGLHeader?.CreatedBy?.DisplayName;
				result.CreateDate = model.PostGLHeader?.Created;
				result.IsCancel = model.PostGLHeader?.IsCancel ?? false;

				var result_JV = new PostGLDocument_JV
				{
					BookingDate = model.CancelMemo?.Booking?.BookingDate,
					CustomerName = model.CancelMemo?.Booking?.MainOwnerName,
					Project = ProjectDropdownDTO.CreateFromModel(model.CancelMemo?.Booking?.Project),
					Unit_old = UnitDropdownDTO.CreateFromModel(model.CancelMemo?.Booking?.Unit),
					TotalPaidAmount = model.CancelMemo?.TotalReceivedAmount ?? 0
				};

				result.PostGLDocument_JV = result_JV;

				return result;
			}
			else
			{
				return new PostGLDocumentDTO();
			}
		}

		public static PostGLDocumentDTO CreateFromQueryResult_CA(QueryResult_PostGLDocument_CA model)
		{
			var result = new PostGLDocumentDTO();

			if (model != null)
			{
				result.PostGLType = PostGLDocumentTypeKeys.CA;
				result.PostGLHeaderID = model.PostGLHeader?.ID ?? Guid.Empty;
				result.DocumentNo = model.PostGLHeader?.DocumentNo;
				result.PostingDate = model.PostGLHeader?.PostingDate ?? DateTime.Now;
				result.CreateBy = model.PostGLHeader?.CreatedBy?.DisplayName;
				result.CreateDate = model.PostGLHeader?.Created;
				result.IsCancel = model.PostGLHeader?.IsCancel ?? false;

				var result_CA = new PostGLDocument_CA
				{
					ReferentNo = model.PostGLHeaderRef.ReferentNo,
					ReferentDate = model.PostGLHeaderRef.PostingDate,
					Remark = model.PostGLHeader?.DeleteReason
				};

				result.PostGLDocument_CA = result_CA;

				return result;
			}
			else
			{
				return new PostGLDocumentDTO();
			}
		}

		public static PostGLDocumentDTO CreateFromQueryResult_RF(QueryResult_PostGLDocument_RF model, List<RefundContactDTO> data)
		{
			var result = new PostGLDocumentDTO();

			if (model != null)
			{
				var result_RFs = new PostGLDocument_RF();
				result.PostGLDocument_RF = new List<PostGLDocument_RF>();
				result.PostGLType = PostGLDocumentTypeKeys.RF;
				result.PostGLHeaderID = model.PostGLHeader?.ID ?? Guid.Empty;
				result.DocumentNo = model.PostGLHeader?.DocumentNo;
				result.PostingDate = model.PostGLHeader?.PostingDate ?? DateTime.Now;
				result.SumTotalAmount = 0;
				if (data.Count > 0)
				{
					result.NameTH = !string.IsNullOrEmpty(data[0].nameth) ? data[0].nameth : "-";
					result.NameEN = !string.IsNullOrEmpty(data[0].nameen) ? data[0].nameen : "-";
					if (data[0].remainingtotalamount > 0 && data[0].welcomehome_flag != "Y") // modify by teerapat_t 2021 - 07 - 14
					//if (data[0].remainingtotalamount > 1000 && data[0].welcomehome_flag != "Y")
					{
						result.SumTotalAmount += data[0].remainingtotalamount;

						result_RFs = new PostGLDocument_RF(); //Refund cus.

						result_RFs.TransferTempNo = data[0].transfernumber;
						result_RFs.Project = ProjectDropdownDTO.CreateFromModel(model.Project);
						result_RFs.Unit = UnitDropdownDTO.CreateFromModel(model.Unit);
						result_RFs.TransferDateApprove = data[0].transferdateapprove;
						//result_RFs.PostGLDetailText = detailData[0].Text1,
						result_RFs.PostGLDetailText = "Refund customers over payment";
						//result_RFs.MethodAmount = model.PostGLHeader?.TotalAmount ?? 0;
						result_RFs.MethodAmount = data[0].remainingtotalamount ?? 0;

						result.PostGLDocument_RF.Add(result_RFs);
					}
					if (!string.IsNullOrEmpty(data[0].welcomehome_flag) )
					{
						//if (data[0].welcomehome_flag.Equals("Y") && data[0].remainingtotalamount <= 1000) //--modify by teerapat_t 2021-07-14
						if (data[0].welcomehome_flag.Equals("Y") && data[0].remainingtotalamount <= 0)
						{
							result.SumTotalAmount += data[0].welcomehome_amount;

							result_RFs = new PostGLDocument_RF(); //WelcomeHome

							result_RFs.TransferTempNo = data[0].transfernumber;
							result_RFs.Project = ProjectDropdownDTO.CreateFromModel(model.Project);
							result_RFs.Unit = UnitDropdownDTO.CreateFromModel(model.Unit);
							result_RFs.TransferDateApprove = data[0].transferdateapprove;
							result_RFs.PostGLDetailText = "Welcome home";
							result_RFs.MethodAmount = data[0].welcomehome_amount ?? 0;
							result.PostGLDocument_RF.Add(result_RFs);

							//result_RFs = new PostGLDocument_RF();
							//result_RFs.PostGLDetailText = "รวม";
							//result_RFs.MethodAmount = result.SumTotalAmount ?? 0;
							//result.PostGLDocument_RF.Add(result_RFs);
						}
					}
					//if (data[0].remainingtotalamount > 1000 && data[0].welcomehome_flag == "Y") //--modify by teerapat_t 2021-07-14
					if (data[0].remainingtotalamount > 0 && data[0].welcomehome_flag == "Y")
					{
						//if (data[0].remainingtotalamount > 1000) //--modify by teerapat_t 2021-07-14
						if (data[0].remainingtotalamount > 0)
						{
							result.SumTotalAmount += data[0].remainingtotalamount;

							result_RFs = new PostGLDocument_RF(); //Refund cus.

							result_RFs.TransferTempNo = data[0].transfernumber;
							result_RFs.Project = ProjectDropdownDTO.CreateFromModel(model.Project);
							result_RFs.Unit = UnitDropdownDTO.CreateFromModel(model.Unit);
							result_RFs.TransferDateApprove = data[0].transferdateapprove;
							//result_RFs.PostGLDetailText = detailData[0].Text1,
							result_RFs.PostGLDetailText = "Refund customers over payment";
							//result_RFs.MethodAmount = model.PostGLHeader?.TotalAmount ?? 0;
							result_RFs.MethodAmount = data[0].remainingtotalamount ?? 0;

							result.PostGLDocument_RF.Add(result_RFs);
						}
						if (data[0].welcomehome_flag.Equals("Y"))
						{
							result.SumTotalAmount += data[0].welcomehome_amount;

							result_RFs = new PostGLDocument_RF(); //WelcomeHome

							result_RFs.TransferTempNo = data[0].transfernumber;
							result_RFs.Project = ProjectDropdownDTO.CreateFromModel(model.Project);
							result_RFs.Unit = UnitDropdownDTO.CreateFromModel(model.Unit);
							result_RFs.TransferDateApprove = data[0].transferdateapprove;
							result_RFs.PostGLDetailText = "Welcome home";
							result_RFs.MethodAmount = data[0].welcomehome_amount ?? 0;
							result.PostGLDocument_RF.Add(result_RFs);

                            result_RFs = new PostGLDocument_RF();
                            result_RFs.PostGLDetailText = "รวม";
                            result_RFs.MethodAmount = result.SumTotalAmount ?? 0;
                            result.PostGLDocument_RF.Add(result_RFs);
                        }

						//result_RFs = new PostGLDocument_RF();
      //                  result_RFs.PostGLDetailText = "รวม";
      //                  result_RFs.MethodAmount = result.SumTotalAmount ?? 0;
      //                  result.PostGLDocument_RF.Add(result_RFs);
                    }
                }
				
				return result;
			}
			else
			{
				return new PostGLDocumentDTO();
			}
		}

		public static PostGLDocumentDetail CreateDetailFromPostGLDetails(PostGLDetail model)
		{
			var result = new PostGLDocumentDetail();

			if (model != null)
			{
				result.PostGLType = model.PostGLType;
				result.GLAccount = model.GLAccount?.GLAccountNo;
				result.GLAccountName = (model.GLAccount?.GLAccountType?.Key ?? "") == "Bank" ? model.GLAccount?.DisplayName : model.GLAccount?.Name;
				result.DRAmount = (model.PostGLType == "DR") ? model.Amount : 0;
				result.CRAmount = (model.PostGLType == "CR") ? model.Amount : 0;

				return result;
			}
			else
			{
				return new PostGLDocumentDetail();
			}
		}
		public static PostGLDocumentDetail CreateDetailFromPostGLDetailsRF(PostGLDetail model, decimal? sumTotalAmount)
		{
			var result = new PostGLDocumentDetail();

			if (model != null)
			{
				result.PostGLType = model.PostGLType;
				// result.GLAccount = model.GLAccount?.GLAccountNo;
				result.GLAccount = model.AccountCode;
				//result.GLAccountName = (model.GLAccount?.GLAccountType?.Key ?? "") == "Bank" ? model.GLAccount?.DisplayName : model.GLAccount?.Name;
				result.GLAccountName = !string.IsNullOrEmpty(model.AccountName) ? model.AccountName : "บัญชีพักคืนเงินลูกค้า-CRM";
				result.DRAmount = (model.PostGLType == "DR") ? sumTotalAmount.Value : 0;
				result.CRAmount = (model.PostGLType == "CR") ? sumTotalAmount.Value : 0;

				return result;
			}
			else
			{
				return new PostGLDocumentDetail();
			}
		}

		public static PostGLHeader MultiplePaymentToPostGLHeader(PostGLHeader_RVMultiplePayment model)
		{
			if (model != null)
			{
				PostGLHeader result = new PostGLHeader();

				result.ID = model.ID;
				result.Created = model.Created;
				result.Updated = model.Updated;
				result.CreatedByUserID = model.CreatedByUserID;
				result.UpdatedByUserID = model.UpdatedByUserID;
				result.IsDeleted = model.IsDeleted;
				result.DocumentNo = model.DocumentNo;
				result.CompanyID = model.CompanyID;
				result.DocumentDate = model.DocumentDate;
				result.PostingDate = model.PostingDate;
				result.PostGLDocumentTypeMasterCenterID = model.PostGLDocumentTypeMasterCenterID;
				result.LastMigrateDate = model.LastMigrateDate;
				result.RefMigrateID1 = model.RefMigrateID1;
				result.RefMigrateID2 = model.RefMigrateID2;
				result.RefMigrateID3 = model.RefMigrateID3;
				result.ReferentID = model.ReferentID;
				result.ReferentType = model.ReferentType;
				result.TotalAmount = model.TotalAmount;
				result.DeleteReason = model.DeleteReason;
				result.Fee = model.Fee;
				result.IsCancel = model.IsCancel;
				result.PostGLDate = model.PostGLDate;
				result.PostGLDocumentNo = model.PostGLDocumentNo;
				result.Description = model.Description;
				result.ReferentNo = model.ReferentNo;
				result.DocType = model.DocType;

				return result;
			}
			else
			{
				return new PostGLHeader();
			}
		}
	}

	#region PostGLDocument - Detail
	public class PostGLDocument_RV
	{
		/// <summary>
		/// เลขที่ใบเสร็จ
		/// </summary>
		public string ReceiptTempNo { get; set; }

		/// <summary>
		/// วันที่ใบเสร็จ
		/// </summary>
		public DateTime? ReceiveDate { get; set; }

		/// <summary>
		/// เลขที่นำฝาก
		/// </summary>
		public string DepositNo { get; set; }

		/// <summary>
		/// วันที่นำฝาก
		/// </summary>
		public DateTime? DepositDate { get; set; }

		/// <summary>
		/// โครงการ
		/// </summary>
		public ProjectDropdownDTO Project { get; set; }

		/// <summary>
		/// แปลง
		/// </summary>
		public UnitDropdownDTO Unit { get; set; }

		/// <summary>
		/// ชำระโดย(ประเภทการชำระ)
		/// </summary>
		public string PaymentMethodType { get; set; }

		/// <summary>
		/// จำนวนเงิน
		/// </summary>
		public decimal MethodAmount { get; set; }
	}

	public class PostGLDocument_PI_UN
	{
		/// <summary>
		/// เลขที่ใบเสร็จ
		/// </summary>
		public string ReceiptTempNo { get; set; }

		/// <summary>
		/// วันที่ใบเสร็จ
		/// </summary>
		public DateTime? ReceiveDate { get; set; }

		/// <summary>
		/// เลขที่ RV
		/// </summary>
		public string RV_DocumentNo { get; set; }

		/// <summary>
		/// วันที่โพส RV
		/// </summary>
		public DateTime? RV_DocumentDate { get; set; }

		/// <summary>
		/// โครงการ
		/// </summary>
		public ProjectDropdownDTO Project { get; set; }

		/// <summary>
		/// แปลง
		/// </summary>
		public UnitDropdownDTO Unit { get; set; }

		/// <summary>
		/// ชำระโดย(ประเภทการชำระ)
		/// </summary>
		public string PaymentMethodType { get; set; }

		/// <summary>
		/// จำนวนเงิน
		/// </summary>
		public decimal Amount { get; set; }
	}

	public class PostGLDocument_JV
	{
		/// <summary>
		/// เลขที่จอง
		/// </summary>
		public DateTime? BookingDate { get; set; }

		/// <summary>
		/// ชื่อลูกค้า
		/// </summary>
		public string CustomerName { get; set; }

		/// <summary>
		/// โครงการ
		/// </summary>
		public ProjectDropdownDTO Project { get; set; }

		/// <summary>
		/// แปลงเก่า
		/// </summary>
		public UnitDropdownDTO Unit_old { get; set; }

		/// <summary>
		/// แปลงใหม่
		/// </summary>
		public UnitDropdownDTO Unit_new { get; set; }

		/// <summary>
		/// จำนวนเงิน
		/// </summary>
		public decimal TotalPaidAmount { get; set; }
	}

	public class PostGLDocument_CA
	{
		/// <summary>
		/// เลขที่อ้างอิง
		/// </summary>
		public string ReferentNo { get; set; }

		/// <summary>
		/// วันที่ของเลขที่อ้างอิง
		/// </summary>
		public DateTime ReferentDate { get; set; }

		/// <summary>
		/// Remark
		/// </summary>
		public string Remark { get; set; }
	}

	public class PostGLDocument_RF
	{
		/// <summary>
		/// เลขที่ใบเสร็จ
		/// </summary>
		public string ReceiptTempNo { get; set; }

		/// <summary>
		/// เลขที่โอน
		/// </summary>
		public string TransferTempNo { get; set; }

		/// <summary>
		/// วันที่ใบเสร็จ
		/// </summary>
		public DateTime? ReceiveDate { get; set; }

		/// <summary>
		/// เลขที่นำฝาก
		/// </summary>
		public string DepositNo { get; set; }

		/// <summary>
		/// วันที่นำฝาก
		/// </summary>
		public DateTime? DepositDate { get; set; }

		/// <summary>
		/// โครงการ
		/// </summary>
		public ProjectDropdownDTO Project { get; set; }

		/// <summary>
		/// แปลง
		/// </summary>
		public UnitDropdownDTO Unit { get; set; }

		/// <summary>
		/// ชำระโดย(ประเภทการชำระ)
		/// </summary>
		public string PaymentMethodType { get; set; }

		/// <summary>
		/// จำนวนเงิน
		/// </summary>
		/// 
		public decimal MethodAmount { get; set; }

		/// <summary>
		/// วันที่โอนกรรมสิทธิ์
		/// </summary>
		/// 
		public DateTime? TransferDateApprove { get; set; }

		/// <summary>
		/// ประเภท
		/// </summary>
		/// 
		public string PostGLDetailText { get; set; }
	}

	public class PostGLDocumentDetail
	{
		/// <summary>
		/// ประเภทรายการ Credit/Debit
		/// </summary>
		public string PostGLType { get; set; }

		/// <summary>
		/// เลขที่ GL
		/// </summary>
		public string GLAccount { get; set; }

		/// <summary>
		/// ชื่อ GL
		/// </summary>
		public string GLAccountName { get; set; }

		/// <summary>
		/// จำนวนเงิน DR
		/// </summary>
		public decimal DRAmount { get; set; }

		/// <summary>
		/// จำนวนเงิน CR
		/// </summary>
		public decimal CRAmount { get; set; }
		/// <summary>
		/// text1
		/// </summary>
		public string Text1 { get; set; }
		/// <summary>
		/// จำนวนเงินรวม
		/// </summary>
		public decimal? TotalAmount { get; set; }
	}

	public class CA_Detail
	{
		/// <summary>
		/// CA PostGLHeaderID
		/// </summary>
		public Guid? CA_PostGLHeaderID { get; set; }

		/// <summary>
		/// CA DocumentNo
		/// </summary>
		public string CA_DocumentNo { get; set; }

		/// <summary>
		/// CA PostingDate
		/// </summary>
		public DateTime? CA_PostingDate { get; set; }


		/// <summary>
		/// CA DeleteReason
		/// </summary>
		public string CA_DeleteReason { get; set; }
		public string CA_CreateBy { get; set; }
	}

	#endregion

	#region QueryResult

	public class QueryResult_PostGLDocument_RV
	{
		public PaymentMethod PaymentMethod { get; set; }

		public Payment Payment { get; set; }
		public Project Project { get; set; }
		public Unit Unit { get; set; }
		public MasterCenter PaymentMethodType { get; set; }

		public PostGLHeader PostGLHeader { get; set; }
		public User CreateBy { get; set; }
	}

	public class QueryResult_PostGLDocument_AC_RV
	{
		//public PaymentMethod PaymentMethod { get; set; }
		public UnknownPayment Payment { get; set; }
		public Project Project { get; set; }
		public Unit Unit { get; set; }
		public MasterCenter PaymentMethodType { get; set; }

		public PostGLHeader PostGLHeader { get; set; }
		public User CreateBy { get; set; }
	}

	public class QueryResult_PostGLDocument_PI
	{
		public PaymentMethod PaymentMethod { get; set; }

		public Payment Payment { get; set; }
		public Project Project { get; set; }
		public Unit Unit { get; set; }
		public MasterCenter PaymentMethodType { get; set; }

		public PostGLHeader PostGLHeader { get; set; }
		public User CreateBy { get; set; }
	}

	public class QueryResult_PostGLDocument_PB
	{
		public PaymentMethodPrebook PaymentMethod { get; set; }

		public PaymentPrebook Payment { get; set; }
		public Project Project { get; set; }
		public Unit Unit { get; set; }
		public MasterCenter PaymentMethodType { get; set; }

		public PostGLHeader PostGLHeader { get; set; }
		public User CreateBy { get; set; }
	}

	public class QueryResult_PostGLDocument_UN
	{
		public UnknownPayment UnknownPayment { get; set; }

		public Project Project { get; set; }
		public Unit Unit { get; set; }

		public MasterCenter PaymentMethodType { get; set; }

		public PostGLHeader PostGLHeader { get; set; }
		public User CreateBy { get; set; }
	}

	public class QueryResult_PostGLDocument_JV_ChangeUnit
	{
		public ChangeUnitWorkflow ChangeUnitWorkflow { get; set; }

		public Booking FromBooking { get; set; }
		public Project Project { get; set; }
		public Unit Unit_Old { get; set; }
		public Unit Unit_New { get; set; }


		public PostGLHeader PostGLHeader { get; set; }
		public User CreateBy { get; set; }
	}

	public class QueryResult_PostGLDocument_JV_Cancel
	{
		public CancelMemo CancelMemo { get; set; }

		public Booking Booking { get; set; }
		public Project Project { get; set; }
		public Unit Unit { get; set; }

		public PostGLHeader PostGLHeader { get; set; }
		public User CreateBy { get; set; }
	}

	public class QueryResult_PostGLDocument_CA
	{
		public PostGLHeader PostGLHeader { get; set; }
		public User CreateBy { get; set; }

		public PostGLHeader PostGLHeaderRef { get; set; }
	}

	public class QueryResult_PostGLDocument_RF
	{
		public Project Project { get; set; }
		public Unit Unit { get; set; }
		public MasterCenter PaymentMethodType { get; set; }
		public PostGLHeader PostGLHeader { get; set; }
		public User CreateBy { get; set; }
	}

	#endregion
}