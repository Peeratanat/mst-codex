using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Database.Models;
using Database.Models.FIN;
using Database.Models.SAL;
using Microsoft.EntityFrameworkCore;
using Database.Models.MST;
using Database.Models.MasterKeys;
using PagingExtensions;
using Base.DbQueries;
using static Database.Models.DbQueries.DBQueryParam;
using ErrorHandling;
using Database.Models.DbQueries;
using Microsoft.Data.SqlClient;
using Database.Models.LOG;

namespace Base.DTOs
{
    public static class Extensions
    {
        public static string LeadingDigit(this string value, int Digit, string exten)
        {
            if (value != null && exten != null)
            {
                string sZero = "";
                for (int i = 0; i + value.Length < Digit; i++)
                {
                    sZero += exten;
                }

                value = sZero + value;
            }

            return value;
        }

        public async static Task<UnknownPayment> UpdateUnknownPaymentStatusAsync(this DatabaseContext db, Guid id)
        {
            string UnknownPaymentStatusKey = "";

            var UnknownPaymentModel = await db.UnknownPayments.Where(o => o.ID == id).FirstOrDefaultAsync();

            if (UnknownPaymentModel != null)
            {

                var PaymentMethods = await db.PaymentMethods.Include(x => x.Payment)
                    .Where(o => o.UnknownPaymentID == id && o.Payment.IsCancel == false).ToListAsync() ?? new List<PaymentMethod>();

                var SumReverseAmount = PaymentMethods.Sum(o => o.PayAmount);

                if (SumReverseAmount == 0)
                    UnknownPaymentStatusKey = UnknownPaymentStatusKeys.Wait;
                else if (UnknownPaymentModel.Amount > SumReverseAmount)
                    UnknownPaymentStatusKey = UnknownPaymentStatusKeys.Partial;
                else if (SumReverseAmount > UnknownPaymentModel.Amount) // จำนวนเงินที่กลับรายการมากกว่าจำนวนเงินที่ตั้งพัก
                {
                    ValidateException ex = new ValidateException();
                    var errMsg = await db.ErrorMessages.FirstAsync(o => o.Key == "ERR0089");
                    var msg = "ไม่สามารถเพิ่มรายการได้ เนื่องจากยอดเงินคงเหลือไม่เพียงพอ";
                    ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                    throw ex;
                }
                else if (UnknownPaymentModel.Amount == SumReverseAmount)
                    UnknownPaymentStatusKey = UnknownPaymentStatusKeys.Complete;
            }

            if (UnknownPaymentStatusKey != "")
            {
                var MasterCenter = db.MasterCenters.FirstOrDefault(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.UnknownPaymentStatus && o.Key == UnknownPaymentStatusKey) ?? new MasterCenter();
                UnknownPaymentModel.UnknownPaymentStatusID = MasterCenter.ID;

                db.Entry(UnknownPaymentModel).State = EntityState.Modified;
                await db.SaveChangesAsync();
            }

            return UnknownPaymentModel;
        }

        public static async Task<string> GetFETCustomerNameAsync(this DatabaseContext db, Guid BookingID)
        {
            var CustomerName = "";

            if (BookingID != Guid.Empty)
            {
                List<string> CustomerList = new List<string>();

                var AgreementModel = await db.Agreements.Where(e => e.BookingID == BookingID).FirstOrDefaultAsync() ?? new Agreement();

                if (AgreementModel.BookingID != null)
                {
                    var AgreementOwnerModel = db.AgreementOwners.Where(e => e.AgreementID == AgreementModel.ID && !e.IsThaiNationality).ToList() ?? new List<AgreementOwner>();
                    if (AgreementOwnerModel.Any())
                        CustomerList = AgreementOwnerModel.Select(e => (e.FullnameEN ?? "")).ToList() ?? new List<string>();
                }
                else
                {
                    var BookingOwnerModel = db.BookingOwners.Where(e => e.BookingID == BookingID && !e.IsThaiNationality && e.IsAgreementOwner == false).Include(x => x.ContactTitleEN).ToList() ?? new List<BookingOwner>();
                    if (BookingOwnerModel.Any())
                        CustomerList = BookingOwnerModel.Select(e => (e.FullnameEN ?? "")).ToList() ?? new List<string>();
                }

                if (CustomerList.Any())
                {
                    foreach (var name in CustomerList)
                    {
                        CustomerName += "," + name;
                    }

                    CustomerName = (CustomerName.Length > 2) ? CustomerName.Substring(1, CustomerName.Length - 1) : CustomerName;
                }
            }

            return CustomerName;
        }

        public static PageOutput CreateBaseDTOFromQuery(this BaseDbQueries model)
        {
            var output = new PageOutput();

            output.Page = model.Page ?? 0;
            output.PageSize = model.PageSize ?? 0;
            output.PageCount = model.PageCount ?? 0;
            output.RecordCount = model.RecordCount ?? 0;

            return output;
        }

        public static async Task<int> DBExecuteProcedure(this DatabaseContext db, string spName, List<dbqParam> paramList)
        {
            string strQuery = string.Format("EXEC {0} {1}", spName, string.Join(", ", paramList.Select(o => string.Format("{0} = {1}", o.Key, o.sqlparam.ParameterName))));

            int result = await db.Database.ExecuteSqlRawAsync(strQuery, paramList.Select(o => o.sqlparam).ToArray());

            return result;
        }

        public static async Task<int> DBExecuteProcedureNonParams(this DatabaseContext db, string spName)
        {
            string strQuery = string.Format("EXEC {0} ", spName);
            int result = await db.Database.ExecuteSqlRawAsync(strQuery);

            return result;
        }

        public static async Task<string> IsCanPayAsync(this DatabaseContext db, Guid bookingID, bool? IsPaidExcess = null)
        {
            var _errMsg = "";

            var IsCanPay = false;

            if (bookingID != Guid.Empty)
            {
                var book = await db.Bookings.Where(e => e.ID == bookingID && e.IsCancelled == false).FirstOrDefaultAsync();

                if (book != null)
                {
                    if (!string.IsNullOrEmpty(book.BookingNo))
                    {
                        var flowMin = await db.MinPriceBudgetWorkflows
                                    .Include(o => o.MinPriceWorkflowType)
                                    .Include(o => o.MinPriceBudgetWorkflowStage)
                                    .Where(o => o.BookingID == bookingID
                                            && !o.IsCancelled
                                            && (!o.IsApproved.HasValue || (o.IsApproved.HasValue && !o.IsApproved.Value))
                                            && o.MinPriceBudgetWorkflowStage.Key == MinPriceBudgetWorkflowStageKeys.Booking)
                                    .FirstOrDefaultAsync();

                        var flowCancel = await db.CancelMemos
                                        .Where(o => o.BookingID == bookingID
                                                && o.HasAgreemnt == false
                                                && !o.ApproveDate.HasValue)
                                        .FirstOrDefaultAsync();

                        var flowUnit = await db.ChangeUnitWorkflows
                                        .Where(o => o.FromBookingID == bookingID && o.IsApproved == null)
                                        //&& (!o.IsApproved.HasValue || (o.IsApproved.HasValue && !o.IsApproved.Value)))
                                        .FirstOrDefaultAsync();

                        if (flowMin != null)
                        {
                            //type
                            if (flowMin.MinPriceWorkflowType.Key == MinPriceWorkflowTypeKeys.AdhocMoreThan5Percent)
                            {
                                var roleID = await db.Roles.Where(o => o.Code == "SBG").Select(o => o.ID).FirstOrDefaultAsync();
                                var approve = await db.MinPriceBudgetApprovals
                                                  .Where(o => o.MinPriceBudgetWorkflowID == flowMin.ID
                                                            && o.RoleID == roleID).FirstOrDefaultAsync();
                                if (approve.IsApproved.HasValue && approve.IsApproved.Value)
                                {
                                    IsCanPay = true;
                                    _errMsg = "";
                                }
                                else
                                {
                                    IsCanPay = false;
                                    _errMsg = "แปลงนี้อยู่ระหว่างอนุมัติ Min Price ไม่สามารถรับเงินได้";
                                }
                            }
                        }
                        else if (flowCancel != null)
                        {
                            IsCanPay = false;
                            _errMsg = "แปลงนี้อยู่ระหว่างยกเลิกจอง ไม่สามารถรับเงินได้";
                        }
                        else if (flowUnit != null)
                        {
                            IsCanPay = false;
                            _errMsg = "แปลงนี้อยู่ระหว่างตั้งเรื่องย้ายแปลง ไม่สามารถรับเงินได้";
                        }
                        else
                        {
                            //สัญญา
                            var agreement = await db.Agreements.Where(e => e.BookingID == bookingID && e.IsCancel == false).FirstOrDefaultAsync();
                            if (agreement != null)
                            {
                                //โอน
                                var transfer = await db.Transfers.Where(e => e.AgreementID == agreement.ID).FirstOrDefaultAsync();

                                if (transfer != null && (!(IsPaidExcess ?? false)))
                                {
                                    IsCanPay = (transfer.IsAccountApproved == false);
                                    _errMsg = (transfer.IsAccountApproved == false) ? "" : "แปลงนี้โอนกรรมสิทธ์เสร็จสิ้นแล้ว ไม่สามารถรับเงินได้";
                                }
                                else
                                {
                                    var flowMinAgreement = await db.MinPriceBudgetWorkflows
                                                                        .Include(o => o.MinPriceWorkflowType)
                                                                        .Include(o => o.MinPriceBudgetWorkflowStage)
                                                                        .Where(o => o.BookingID == agreement.BookingID
                                                                                && !o.IsCancelled
                                                                                && (!o.IsApproved.HasValue || (o.IsApproved.HasValue && !o.IsApproved.Value))
                                                                                && o.MinPriceBudgetWorkflowStage.Key == MinPriceBudgetWorkflowStageKeys.Contract)
                                                                        .FirstOrDefaultAsync();

                                    var flowCancelAgreement = await db.CancelMemos
                                                    .Where(o => o.AgreementID == agreement.ID
                                                            && o.HasAgreemnt == false
                                                            && !o.ApproveDate.HasValue)
                                                    .FirstOrDefaultAsync();

                                    var flowUnitAgreement = await db.ChangeUnitWorkflows
                                                    .Where(o => o.FromAgreementID == agreement.ID
                                                            && (!o.IsApproved.HasValue || (o.IsApproved.HasValue && !o.IsApproved.Value)))
                                                    .FirstOrDefaultAsync();

                                    //var flowOwnerAgreement = await db.ChangeAgreementOwnerWorkflows
                                    //           .Include(o => o.ChangeAgreementOwnerType)
                                    //           .Where(o => o.AgreementID == agreement.ID
                                    //                   && (!o.IsApproved.HasValue || (o.IsApproved.HasValue && !o.IsApproved.Value)))
                                    //           .FirstOrDefaultAsync();

                                    if (flowMinAgreement != null)
                                    {
                                        //type
                                        if (flowMinAgreement.MinPriceWorkflowType.Key == MinPriceWorkflowTypeKeys.AdhocMoreThan5Percent)
                                        {
                                            var roleID = await db.Roles.Where(o => o.Code == "SBG").Select(o => o.ID).FirstOrDefaultAsync();
                                            var approve = await db.MinPriceBudgetApprovals
                                                              .Where(o => o.MinPriceBudgetWorkflowID == flowMinAgreement.ID
                                                                        && o.RoleID == roleID).FirstOrDefaultAsync();
                                            if (approve.IsApproved.HasValue && approve.IsApproved.Value)
                                            {
                                                IsCanPay = true;
                                                _errMsg = "";
                                            }
                                            else
                                            {
                                                IsCanPay = false;
                                                _errMsg = "แปลงนี้อยู่ระหว่างอนุมัติ Min Price ไม่สามารถรับเงินได้";
                                            }
                                        }
                                    }
                                    else if (flowCancelAgreement != null)
                                    {
                                        IsCanPay = false;
                                        _errMsg = "แปลงนี้อยู่ระหว่างยกเลิกสัญญา ไม่สามารถรับเงินได้";
                                    }
                                    else if (flowUnitAgreement != null)
                                    {
                                        IsCanPay = false;
                                        _errMsg = "แปลงนี้อยู่ระหว่างตั้งเรื่องย้ายแปลง ไม่สามารถรับเงินได้";
                                    }
                                    //else if (flowOwnerAgreement != null)
                                    //{
                                    //    IsCanPay = false;
                                    //    _errMsg = "แปลงนี้อยู่ระหว่างตั้งเรื่องเปลื่ยนแปลงชื่อ ไม่สามารถรับเงินได้";
                                    //}
                                    else
                                    {
                                        IsCanPay = true;
                                        _errMsg = "";
                                    }
                                }
                            }
                            else
                            {
                                IsCanPay = true;
                                _errMsg = "";
                            }
                        }
                    }
                    else
                    {
                        IsCanPay = false;
                        _errMsg = "แปลงนี้อยู่ระหว่างยืนยันจอง ไม่สามารถรับเงินได้";
                    }
                }
                else
                {
                    IsCanPay = false;
                    _errMsg = "แปลงนี้สถานะว่าง ไม่สามารถรับเงินได้";
                }
            }

            return _errMsg;
        }

        public static async Task<string> GetNewPostGLDocumentAsync(this DatabaseContext db, string PostGLType, string SAPComCode, int iYear = 0, int iMonth = 0)
        {
            /*
                Format : <RV><SAP Com Code><yy><MM><NNNN>
                Table  : MST.RunningNumberCounters
                Column : Key = "xx_PostGLDocument"

                RV eg.    RV100019060001  Key = "RV_PostGLDocument"
                JV eg.    JV100019060001  Key = "JV_PostGLDocument"
                RR eg.    RR100019060001  Key = "RR_PostGLDocument"
                CA eg.    CA100019060001  Key = "CA_PostGLDocument"
                KR eg.    KR100019060001  Key = "KR_PostGLDocument"
            */

            string year = (iYear == 0) ? DateTime.Today.Year.ToString() : iYear.ToString();
            string month = (iMonth == 0) ? DateTime.Today.Month.ToString("00") : iMonth.ToString("00"); ;
            var runningKey = PostGLType + SAPComCode + year[2] + year[3] + month;
            var PostGLDocumentNo = string.Empty;

            var runningNumber = await db.RunningNumberCounters.Where(o => o.Key == runningKey && o.Type == string.Format("{0}_PostGLDocument", PostGLType)).FirstOrDefaultAsync();
            if (runningNumber == null)
            {
                var runningModel = new RunningNumberCounter()
                {
                    Key = runningKey,
                    Type = string.Format("{0}_PostGLDocument", PostGLType),
                    Count = 1
                };

                await db.RunningNumberCounters.AddAsync(runningModel);
                PostGLDocumentNo = runningKey + runningModel.Count.ToString("0000");
                await db.SaveChangesAsync();
            }
            else
            {
                runningNumber.Count += 1;
                PostGLDocumentNo = runningKey + runningNumber.Count.ToString("0000");
                db.Entry(runningNumber).State = EntityState.Modified;
                await db.SaveChangesAsync();
            }

            return PostGLDocumentNo;
        }

        public static async Task<MasterCenter> getPaymentStateByBookingAsync(this DatabaseContext db, Guid BookingID)
        {
            var UnitDocument = await (from b in db.Bookings.Where(o => o.ID == BookingID && o.IsCancelled == false)

                                      join ag in db.Agreements.Where(o => o.IsCancel == false) on b.ID equals ag.BookingID into agData
                                      from agModel in agData.DefaultIfEmpty()

                                      join tf in db.Transfers on agModel.ID equals tf.AgreementID into tfData
                                      from tfModel in tfData.DefaultIfEmpty()

                                      select new
                                      {
                                          Booking = b,
                                          Agreement = agModel ?? new Agreement(),
                                          Transfer = tfModel ?? new Transfer()
                                      }).FirstOrDefaultAsync();

            var PaymentState = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.PaymentState).ToListAsync();

            var result = new MasterCenter();

            if (UnitDocument.Transfer.TransferNo != null)
            {
                if (UnitDocument.Transfer.IsTransferConfirmed)
                    result = PaymentState.Where(o => o.Key == PaymentStateKeys.Transfer).FirstOrDefault();
                else
                    result = PaymentState.Where(o => o.Key == PaymentStateKeys.BeforeTransfer).FirstOrDefault();
            }
            else if (UnitDocument.Agreement.AgreementNo != null)
                result = PaymentState.Where(o => o.Key == PaymentStateKeys.Agreement).FirstOrDefault();
            else
                result = PaymentState.Where(o => o.Key == PaymentStateKeys.Booking).FirstOrDefault();

            if (result != null)
            {
                return result;
            }
            else
            {
                ValidateException ex = new ValidateException();
                ex.AddError("ERR9999", "Not found 'Payment State' at 'Extensions.getPaymentStateByBookingAsync()'", 1);
                throw ex;
            }
        }

        public static string renameFileWithTimeStamp(this string str)
        {
            string timestamp = Convert.ToString(DateTime.Today.Year) + DateTime.Now.ToString("MMdd_HHmmss");

            string objectName = "";

            var fileObj = str.Split(".");

            if (fileObj.Length > 1)
                objectName = $"{fileObj[0]}_{timestamp}.{fileObj[1]}";
            else
                objectName = $"{str}_{timestamp}";

            return objectName;
        }

        public static void ConvertToMinIOFileParam(ref string fileBucket, ref string fileName)
        {
            var minioBucket = fileBucket.Split("/")[0];

            var minioFileName = "";

            for (int i = 1; i < fileBucket.Split("/").Length; i++)
            {
                minioFileName += fileBucket.Split("/")[i] + "/";
            }

            minioFileName += fileName;

            fileBucket = minioBucket;
            fileName = minioFileName;
        }

        public static async Task UpdateBillPaymentDetailStatus(this DatabaseContext db, Guid? id, bool UpdateBookingNull = false)
        {

            var PaymentMethodList = await db.PaymentMethods.Include(x => x.Payment).Include(x => x.PaymentMethodType).Where(x => x.BillPaymentDetailID == id && x.Payment.IsCancel == false).ToListAsync();
            var amount = PaymentMethodList.Select(x => x.PayAmount).Sum();
            var PaymentMethod = PaymentMethodList.FirstOrDefault() ?? new PaymentMethod();
            var billPaymentDetail = await db.BillPaymentDetails.Where(x => x.ID == id).FirstOrDefaultAsync() ?? new BillPaymentDetail();
            var UnknownPayment = await db.UnknownPayments.Where(x => x.BillPaymentDetailID == billPaymentDetail.ID).Select(x => x.Amount).ToListAsync();
            amount = amount + UnknownPayment.Sum();

            if (billPaymentDetail.PayAmount == amount)
            {
                var StatusComplete = await db.MasterCenters.Where(x => x.MasterCenterGroupKey == MasterCenterGroupKeys.BillPaymentStatus && x.Key == BillPaymentStatusKey.Complete).FirstOrDefaultAsync();
                billPaymentDetail.BillPaymentStatusMasterCenterID = StatusComplete.ID;
            }
            else if (amount > billPaymentDetail.PayAmount)
            {
                ValidateException ex = new ValidateException();
                var errMsg = await db.ErrorMessages.FirstAsync(o => o.Key == "ERR9999");
                var msg = errMsg.Message.Replace("[message]", "มีการใส่ยอดเงินเกิน");
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                throw ex;
            }
            else if (amount < billPaymentDetail.PayAmount && (PaymentMethodList.Any() || UnknownPayment.Any()))
            {
                var StatusSplit = await db.MasterCenters.Where(x => x.MasterCenterGroupKey == MasterCenterGroupKeys.BillPaymentStatus && x.Key == BillPaymentStatusKey.Split).FirstOrDefaultAsync();
                billPaymentDetail.BillPaymentStatusMasterCenterID = StatusSplit.ID;
            }
            else if (PaymentMethodList.Count() == 0)
            {
                var StatusNotFound = await db.MasterCenters.Where(x => x.MasterCenterGroupKey == MasterCenterGroupKeys.BillPaymentStatus && x.Key == BillPaymentStatusKey.NotFound).FirstOrDefaultAsync();
                billPaymentDetail.BillPaymentStatusMasterCenterID = StatusNotFound.ID;
            }
            if (UpdateBookingNull)
            {
                billPaymentDetail.BookingID = null;
            }
            db.Entry(billPaymentDetail).State = EntityState.Modified;
            await db.SaveChangesAsync();
        }

        public static async Task DirectCreditDebitApprovalFormCancel(this DatabaseContext db, Guid? BookingID, string DirectApprovalFormStatus)
        {
            #region DirectApprovalFormStatus
            // ยกเลิก (ยกเลิกห้อง) ยกเลิกสัญญา = DirectApprovalFormStatusKey.CancelUnit
            // ยกเลิก (โอนสิทธิ์เปลี่ยนมือ) โอนสิทธื์เปลี่ยนชื่อ = DirectApprovalFormStatusKey.CancelChangeOwner
            // ยกเลิก (โอนกรรมสิทธิ์) ตั้งเรื่องโอนแล้วยกเลิก = DirectApprovalFormStatusKey.CancelTransferred
            #endregion
            var model = await db.DirectCreditDebitApprovalForms.Where(x => x.BookingID == BookingID &&
            (x.DirectApprovalFormStatus.Key.Equals(DirectApprovalFormStatusKey.Approved)
            || x.DirectApprovalFormStatus.Key.Equals(DirectApprovalFormStatusKey.Waiting)
            || x.DirectApprovalFormStatus.Key.Equals(DirectApprovalFormStatusKey.New))
            ).FirstOrDefaultAsync() ?? null;
            if (model != null)
            {
                var NewStatus = await db.MasterCenters.Where(x => x.MasterCenterGroupKey == MasterCenterGroupKeys.DirectApprovalFormStatus && x.Key == DirectApprovalFormStatus).Select(x => x.ID).FirstOrDefaultAsync();
                if (NewStatus != null)
                {
                    model.DirectApprovalFormStatusMasterCenterID = NewStatus;
                    model.CancelDate = DateTime.Now;
                    db.Entry(model).State = EntityState.Modified;
                    await db.SaveChangesAsync();
                }
            }
        }

        public static DateTime? DateStartMonth(DateTime? dateValue)
        {
            if (dateValue != null && dateValue.HasValue)
            {
                int y = dateValue.Value.Year;
                int m = dateValue.Value.Month;
                int d = 1;

                return new DateTime(y, m, d);
            }
            else
            {
                return null;
            }
        }

        public static DateTime? DateEndMonth(DateTime? dateValue)
        {
            if (dateValue != null && dateValue.HasValue)
            {
                int y = dateValue.Value.Year;
                int m = dateValue.Value.Month;
                int d = DateTime.DaysInMonth(y, m);

                return new DateTime(y, m, d);
            }
            else
            {
                return null;
            }
        }

        public static IEnumerable<List<T>> SplitList<T>(List<T> locations, int nSize = 30)
        {
            for (int i = 0; i < locations.Count; i += nSize)
            {
                yield return locations.GetRange(i, Math.Min(nSize, locations.Count - i));
            }
        }

        public static async Task<bool> UpdateAppveDateAsync(this DatabaseContext db, Guid BookingID)
        {
            var BookingPayAmount = await db.PaymentItems
             .Include(o => o.MasterPriceItem)
             .Include(o => o.Payment)
             .Where(o => o.MasterPriceItem.Key == MasterPriceItemKeys.BookingAmount && o.Payment.BookingID == BookingID && o.Payment.IsCancel == false).SumAsync(o => o.PayAmount);

            var BookingAmount = await db.UnitPrices
                  .Include(o => o.UnitPriceStage)
                  .Where(o => o.UnitPriceStage.Key == UnitPriceStageKeys.Booking && o.BookingID == BookingID)
                  .Select(o => o.BookingAmount).FirstOrDefaultAsync();

            if (BookingPayAmount >= BookingAmount)
            {
                var book = await db.Bookings.Where(o => o.ID == BookingID).FirstAsync();
                book.ApproveDate = DateTime.Now;
                db.Entry(book).State = EntityState.Modified;
                #region save log ChangeSaleOfficer
                var log = new ChangeSaleOfficer()
                {
                    ID = Guid.NewGuid(),
                    BookingID = book.ID,
                    AgentEmployeeID = book.AgentEmployeeID,
                    AgentID = book.AgentID,
                    ProjectSaleUserID = book.ProjectSaleUserID,
                    SaleOfficerTypeMasterCenterID = book.SaleOfficerTypeMasterCenterID,
                    SaleUserID = book.SaleUserID,
                    IsDeleted = false,
                };
                await db.ChangeSaleOfficers.AddAsync(log);
                #endregion
                await db.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public static async Task<DateTime> GetActivePostingDate(this DbQueryContext dbQuery, Guid CompanyID, DateTime PostingDate)
        {
            var ParamList = new List<dbqParam>
                        {
                            new dbqParam { sqlparam = new SqlParameter("@CompanyID", CompanyID) },
                            new dbqParam { sqlparam = new SqlParameter("@PostingDate", PostingDate) }
                        };

            string strQuery = string.Format("SELECT 'Value' = {0} ({1})", DBStoredNames.fnGetActivePostingDate, string.Join(", ", ParamList.Select(o => o.sqlparam.ParameterName)));
            var res = await dbQuery.fnScalarValue_DateTime.FromSqlRaw(strQuery, ParamList.Select(o => o.sqlparam).ToArray()).FirstOrDefaultAsync() ?? new DBFunctionOutputScalar<DateTime?>();

            if (!res.Value.HasValue)
            {

            }

            return (res.Value ?? DateTime.Now).Date;
        }
    }
}
