using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Database.Models.MasterKeys;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using ErrorHandling;

namespace Database.Models
{
    public static class Extensions
    {
        public static InternalEntityEntry GetInternalEntityEntry(this EntityEntry entityEntry)
        {
            var internalEntry = (InternalEntityEntry)entityEntry
                .GetType()
                .GetProperty("InternalEntry", BindingFlags.NonPublic | BindingFlags.Instance)
                .GetValue(entityEntry);

            return internalEntry;
        }

        public async static Task<PRJ.PriceList> GetActivePriceListAsync(this DbSet<PRJ.PriceList> models, Guid unitID)
        {
            var result = await (from p in models.Include(o => o.PriceListItems)
                                where p.ActiveDate <= DateTime.Now && p.UnitID == unitID
                                orderby p.ActiveDate descending
                                select p).FirstOrDefaultAsync();
            return result;
        }

        public async static Task<PRJ.PriceList> GetActivePriceListAsync(this DatabaseContext db, Guid unitID)
        {
            var result = await (from p in db.PriceLists.Include(o => o.PriceListItems)
                                where p.ActiveDate <= DateTime.Now && p.UnitID == unitID
                                orderby p.ActiveDate descending
                                select p).FirstOrDefaultAsync();
            return result;
        }

        public async static Task<Guid> GetIDAsync(this DbSet<MST.MasterCenter> models, string masterCenterGroupKey, string key)
        {
            var result = await models.Where(o => o.MasterCenterGroupKey == masterCenterGroupKey && o.Key == key).Select(o => o.ID).FirstOrDefaultAsync();
            return result;
        }

        public static Guid GetID(this DbSet<MST.MasterCenter> models, string masterCenterGroupKey, string key)
        {
            var result = models.Where(o => o.MasterCenterGroupKey == masterCenterGroupKey && o.Key == key).Select(o => o.ID).FirstOrDefault();
            return result;
        }

        public async static Task<MST.MasterCenter> GetAsync(this DbSet<MST.MasterCenter> models, string masterCenterGroupKey, string key)
        {
            var result = await models.Where(o => o.MasterCenterGroupKey == masterCenterGroupKey && o.Key == key).Select(o => o).FirstOrDefaultAsync();
            return result;
        }

        public async static Task<MST.ErrorMessage> GetAsync(this DbSet<MST.ErrorMessage> models, string key)
        {
            var result = await models.Where(o => o.Key == key).Select(o => o).FirstOrDefaultAsync();
            return result;
        }

        public async static Task CreateBudgetPromotionSyncJobAsync(this List<PRJ.BudgetPromotion> budgetPromotions, DatabaseContext db)
        {
            var masterCenterBudgetPromotionTypeSaleID = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.BudgetPromotionType && o.Key == BudgetPromotionTypeKeys.Sale).Select(o => o.ID).FirstAsync();
            var masterCenterBudgetPromotionTypeTransferID = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.BudgetPromotionType && o.Key == BudgetPromotionTypeKeys.Transfer).Select(o => o.ID).FirstAsync();

            var model = new PRJ.BudgetPromotionSyncJob();

            model.Status = BackgroundJobStatus.Waiting;

            var syncItems = new List<PRJ.BudgetPromotionSyncItem>();
            var temp = budgetPromotions.GroupBy(o => o.UnitID).Select(o => new
            {
                Unit = db.Units.Where(p => p.ID == o.Key).FirstOrDefault(),
                BudgetPromotions = o.Select(p => p).ToList()
            }).ToList();

            var data = temp.Select(o => new
            {
                Unit = o.Unit,
                BudgetPromotionSale = o.BudgetPromotions.Where(p => p.BudgetPromotionTypeMasterCenterID == masterCenterBudgetPromotionTypeSaleID && p.ActiveDate <= DateTime.Now).OrderByDescending(p => p.ActiveDate).FirstOrDefault(),
                BudgetPromotionTransfer = o.BudgetPromotions.Where(p => p.BudgetPromotionTypeMasterCenterID == masterCenterBudgetPromotionTypeTransferID && p.ActiveDate <= DateTime.Now).OrderByDescending(p => p.ActiveDate).FirstOrDefault()
            }).ToList();


            var budgetPromotionSyncStatusSyncingMasterCenterID = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.BudgetPromotionSyncStatus && o.Key == BudgetPromotionSyncStatusKeys.Syncing).Select(o => o.ID).FirstAsync();

            foreach (var item in data)
            {
                var unit = await db.Units.Where(o => o.ID == item.Unit.ID).FirstOrDefaultAsync();
                var synItem = new PRJ.BudgetPromotionSyncItem();
                synItem.BudgetPromotionSyncJobID = model.ID;
                synItem.SAPWBSObject_P = unit.SAPWBSObject_P;
                synItem.SaleBudgetPromotionID = item.BudgetPromotionSale?.ID;
                synItem.TransferBudgetPromotionID = item.BudgetPromotionTransfer?.ID;
                synItem.Amount = Convert.ToDecimal(item.BudgetPromotionSale?.Budget + item.BudgetPromotionTransfer?.Budget);
                synItem.BudgetPromotionSyncStatusMasterCenterID = budgetPromotionSyncStatusSyncingMasterCenterID;
                synItem.Retry = 0;
                synItem.Currency = "THB";
                syncItems.Add(synItem);
            }

            await db.BudgetPromotionSyncJobs.AddAsync(model);

            await db.BudgetPromotionSyncItems.AddRangeAsync(syncItems);

            await db.SaveChangesAsync();
        }

        public async static Task<bool> CheckCalendarAsync(this DatabaseContext db, Guid gID, DateTime ChkdDate)
        {
            var chkCalendar = await db.CalendarLocks.Where(o => o.CompanyID == gID && o.LockDate.Date == ChkdDate.Date).FirstOrDefaultAsync() ?? new ACC.CalendarLock();
            bool resCalendar = chkCalendar.IsLocked;
            return resCalendar;
        }

        public async static Task CheckCalendarThrowErrorAsync(this DatabaseContext db, Guid gID, DateTime ChkdDate, int ErrorMessage = 1)
        {
            var chkCalendar = await db.CalendarLocks.Include(o => o.Company).Where(o => o.CompanyID == gID && o.LockDate.Date == ChkdDate.Date).FirstOrDefaultAsync() ?? new ACC.CalendarLock();
            bool resCalendar = chkCalendar.IsLocked;

            ValidateException ex = new ValidateException();

            if (resCalendar)
            {
                var errMsg = new MST.ErrorMessage();

                switch (ErrorMessage)
                {
                    case 1: //ไม่สามารถเลือกวันที่ "[date]" ได้ บริษัท "[company]" ปิดบัญชีแล้ว กรุณาติดต่อฝ่ายบัญชี 
                        errMsg = await db.ErrorMessages.FirstAsync(o => o.Key == "ERR0091");
                        break;
                    case 2: //ไม่สามารถแก้ไขได้เนื่องจาก วันที่ "[date]" ได้ บริษัท "[company]" ปิดบัญชีแล้ว กรุณาติดต่อฝ่ายบัญชี
                        errMsg = await db.ErrorMessages.FirstAsync(o => o.Key == "ERR0163");
                        break;
                    default:
                        errMsg = await db.ErrorMessages.FirstAsync(o => o.Key == "ERR0091");
                        break;
                }
                string CompanyName = string.Format("{0} - {1}", chkCalendar.Company.SAPCompanyID, chkCalendar.Company.NameTH);
                string strReceiveDate = ChkdDate.ToString("dd/MM/yyyy");
                var msg = errMsg.Message.Replace("[company]", CompanyName);
                msg = msg.Replace("[date]", strReceiveDate);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }

            if (ex.HasError)
                throw ex;
        }

        public async static Task<bool> CheckCalendar(this DatabaseContext db, Guid gID, DateTime ChkdDate)
        {
            var chkCalendar = await db.CalendarLocks.Where(o => o.CompanyID == gID && o.LockDate.Date == ChkdDate.Date && !o.IsDeleted).FirstOrDefaultAsync() ?? new ACC.CalendarLock();
            bool resCalendar = chkCalendar.IsLocked;
            return resCalendar;
        }

        public async static Task<bool> CheckDepositAsync(this DatabaseContext db, Guid PaymentMethodID)
        {
            var DepositDetail = await db.DepositDetails.Include(o => o.DepositHeader).Where(x => x.PaymentMethodID == PaymentMethodID && !x.IsDeleted).FirstOrDefaultAsync() ?? new FIN.DepositDetail();
            bool resDeposit = false;
            if (DepositDetail?.DepositHeader?.DepositNo != null)
                resDeposit = true;

            return resDeposit;
        }

        public async static Task<bool> CheckPostGLAsync(this DatabaseContext db, Guid RefID, string RefType)
        {
            var PostGL = await db.PostGLHeaders.Where(x => x.ReferentID == RefID && x.ReferentType == RefType).FirstOrDefaultAsync() ?? new ACC.PostGLHeader();
            bool resPostGL = false;
            if (PostGL?.ReferentID != null)
                resPostGL = true;

            return resPostGL;
        }

        public async static Task<bool> CheckMemoMoveMoneyAsync(this DatabaseContext db, Guid PaymentMethodID)
        {
            var MemoMoveMoney = await db.MemoMoveMoneys.Where(x => x.PaymentMethodID == PaymentMethodID).FirstOrDefaultAsync() ?? new FIN.MemoMoveMoney();
            bool resIsPrint = false;
            if (MemoMoveMoney.IsPrint)
                resIsPrint = true;

            return resIsPrint;
        }

        public async static Task<bool> CheckDepositAsync(this DatabaseContext db, Guid? PaymentID, Guid? PaymentMethodID)
        {
            var DepositHeader = await db.DepositDetails
                .Include(o => o.DepositHeader)
                .Include(o => o.PaymentMethod)
                .Where(x => (x.PaymentMethodID == PaymentMethodID || x.PaymentMethod.PaymentID == PaymentID) && !string.IsNullOrEmpty(x.DepositHeader.DepositNo) && !x.PaymentMethod.Payment.QuotationID.HasValue)
                .Select(o => o.DepositHeader)
                .FirstOrDefaultAsync() ?? new FIN.DepositHeader();

            bool IsDeposit = false;

            if (!string.IsNullOrEmpty(DepositHeader.DepositNo))
                IsDeposit = true;

            return IsDeposit;
        }
    }

}
