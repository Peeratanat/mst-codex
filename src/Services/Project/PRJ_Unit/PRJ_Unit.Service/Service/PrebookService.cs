using Base.DTOs;
using Base.DTOs.PRJ;
using Common.Helper.Logging;
using Dapper;
using Database.Models;
using Database.Models.DbQueries;
using Database.Models.DbQueries.PRJ;
using Database.Models.LOG;
using Database.Models.MasterKeys;
using Database.Models.PRJ;
using ErrorHandling;
using ExcelExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using OfficeOpenXml;
using PagingExtensions;
using PRJ_Unit.Params.Filters;
using PRJ_Unit.Params.Outputs;
using PRJ_Unit.Services.Excels;
using PRJ_Unit.Services.Sap;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Globalization;
using FileStorage;
namespace PRJ_Unit.Services
{
    public class PreBookService : IPreBookService
    {
        public LogModel logModel { get; set; }
        private readonly DatabaseContext DB;
        int Timeout = 300;
        public PreBookService(DatabaseContext db)
        {
            logModel = new LogModel("PreBookService", null);
            DB = db;
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(Environment.GetEnvironmentVariable("DBConnectionString"));
            Timeout = builder.ConnectTimeout;
            DB.Database.SetCommandTimeout(Timeout);
        }

        public async Task<List<UnitDropdownDTO>> GetUnitPreBookDropdownListAsync(Guid projectID, string unitNo)
        {

            var master = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.PrebookStatus && (o.Key == "PB5" || o.Key == "PB6" || o.Key == "PB7")).Select(o => o.ID).ToListAsync();


            // เช้คว่า Prebook มีการจ่ายเงินแล้ว และ QT Status ไม่ใช่ ย้ายแปลง ยกเลิกจอง,สัญญา
            var qrs = await DB.PaymentPrebooks
                    .Include(o => o.Quotation)
                    .ThenInclude(o => o.Unit)
                    .Where(o => !master.Contains(o.Quotation.QuotationStatusMasterCenterID.Value))
                    .ToListAsync();

            // 
            var unitIDs = qrs.Where(o => o.Quotation.IsPrebook && o.Quotation.ProjectID == projectID).Select(o => o.Quotation.Unit.ID).ToList();

            //เช็คที่ใบจองจริง
            var QTS = await DB.Bookings
                    .Where(o => unitIDs.Contains(o.UnitID))
                    .ToListAsync();
            // ได้ UnitID ของใบจองจริง
            var QTSs = QTS.Where(o => !o.IsCancelled && o.ProjectID == projectID).Select(o => o.UnitID).ToList();
            // ได้ ID ที่มีการยกเลิก
            var FromBookingID = QTS.Where(o => o.IsCancelled && o.ProjectID == projectID).Select(o => o.ID).ToList();
            // เช็คว่ามีการย้ายแปลงไหม
            var checkUnitChange = await DB.Bookings.Where(o => FromBookingID.Contains(o.ChangeFromBookingID.Value)).Select(o => o.UnitID).ToListAsync();
            // มีใน prebook และจองจริง เอามา union กัน
            var unitID = unitIDs.Concat(QTSs).ToList();
            // มีใน prebook และจองจริง เอามา union กับ แปลงที่มีการย้าย
            var unitID2 = unitID.Concat(checkUnitChange).ToList();

            var unitStatus = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.UnitStatus && o.Key == "0").Select(o => o.ID).FirstOrDefaultAsync();
            IQueryable<Unit> query = DB.Units.Where(x => x.ProjectID == projectID && x.UnitStatusMasterCenterID == unitStatus && (x.AssetType.Key == AssetTypeKeys.Unit && (x.IsNotSale == false || x.IsNotSale == null)));
            if (!string.IsNullOrEmpty(unitNo))
            {
                query = query.Where(q => q.UnitNo.Contains(unitNo));
            }

            var queryResults = await query.OrderBy(o => o.UnitNo).OrderBy(x => x.UnitNo).ToListAsync();
            var results = queryResults.Select(o => UnitDropdownDTO.CreateFromModel(o)).ToList();

            //ไม่มีที่จ่ายเงินแล้ว หรือ ยกเลิกแล้ว
            var unitUse = results.Where(o => !unitID2.Contains(o.Id)).ToList();

            //จ่ายเงิน online payment แล้ว 
            var unitList = unitUse.Select(x => x.Id).ToList();


            var unitOnlinePayment = await (from p in DB.OnlinePaymentPrebooks.Where(x => unitList.Contains(x.UnitID ?? Guid.Empty) && (x.IsCancel == false || x.IsCancel == null))
                                           join h in DB.OnlinePaymentHistorys.Where(x => x.Payment_Status.Equals("PAID"))
                                           on p.OnlinePaymentHistoryID equals h.ID
                                           //orderby p.Created descending
                                           group p by p.UnitID into g
                                           select new { UnitID = g.Key, OnlinePaymentPrebookID = g.OrderByDescending(o => o.Created).FirstOrDefault().ID }).ToListAsync();
            //ของแต่ ยกเลิก หรือ ย้ายแปลงไปแล้ว ให้เอา unit กลับมา
            List<Guid> onlinePaymentID = unitOnlinePayment.Select(x => x.OnlinePaymentPrebookID).ToList();

            var quotations = await DB.Quotations.IgnoreQueryFilters().Where(o => onlinePaymentID.Contains(o.RefOnlinePaymentPrebookID ?? Guid.NewGuid())).Select(o => o.ID).ToListAsync();
            var booking = await DB.Bookings.Where(o => quotations.Contains(o.QuotationID ?? Guid.NewGuid()) && o.IsCancelled == true).Select(o => o.UnitID).ToListAsync();
            var unitOnlinePaymentUnitID = unitOnlinePayment.Where(x => !booking.Contains(x.UnitID ?? Guid.NewGuid())).Select(x => x.UnitID).ToList();
            unitUse = unitUse.Where(o => !unitOnlinePaymentUnitID.Contains(o.Id)).ToList();

            return unitUse;
        }

    }

}
