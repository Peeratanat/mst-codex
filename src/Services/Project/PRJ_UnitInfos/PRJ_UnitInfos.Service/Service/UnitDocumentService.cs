using Base.DTOs.SAL;
using Database.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using static Base.DTOs.SAL.UnitDocumentDTO;
using Database.Models.SAL;
using PagingExtensions;
using Database.Models.MasterKeys;
using Database.Models.MST;
using Common.Helper.Logging;
using PRJ_UnitInfos.Services;
using PRJ_UnitInfos.Params.Filters;
using PRJ_UnitInfos.Params.Outputs;

namespace PRJ_UnitInfos.Services
{
    public class UnitDocumentService : IUnitDocumentService
    {
        private readonly DatabaseContext DB;
        public LogModel logModel { get; set; }

        public UnitDocumentService(DatabaseContext db)
        {
            logModel = new LogModel("UnitDocumentService", null);
            this.DB = db;
        }

        public async Task<UnitDocumentPaging> GetUnitDocumentDropdownListAsync(UnitDocumentFilter filter, PageParam pageParam, UnitDocumentSortByParam sortByParam, CancellationToken cancellationToken = default)
        {

            var query = GetUnitDocumentQueryResult();

            #region Filter

            // สถานะ จอง สัญญา โอน เท่านั้น
            List<string> UnitStatusCase =
            [
                BookingStatusKeys.Booking,
                BookingStatusKeys.Contract,
                BookingStatusKeys.TransferOwnership,
            ];

            query = query.Where(q => UnitStatusCase.Contains(q.UnitStatus.Key));

            // ต้องยังไม่ยกเลิก
            query = query.Where(q => !q.Booking.IsCancelled);

            // ต้องยืนยันจองแล้ว
            query = query.Where(q => q.Booking.IsPaid ?? true);

            // ยังไม่ตั้งเรื่องโอน
            query = query.Where(q => !q.Transfer.IsReadyToTransfer);

            if (!string.IsNullOrEmpty(filter.DocumentNo))
            {
                query = query.Where(q => q.Booking.BookingNo.Contains(filter.DocumentNo) || q.Agreement.AgreementNo.Contains(filter.DocumentNo));
            }

            if ((filter.CompanyID ?? Guid.Empty) != Guid.Empty)
            {
                query = query.Where(q => q.Project.Company.ID == filter.CompanyID);
            }

            if ((filter.ProjectID ?? Guid.Empty) != Guid.Empty)
            {
                query = query.Where(q => q.Project.ID == filter.ProjectID);
            }

            if (!string.IsNullOrEmpty(filter.UnitNo))
            {
                query = query.Where(q => q.Unit.UnitNo.Contains(filter.UnitNo));
            }

            #endregion Filter

            var pageOuput = PagingHelper.Paging(pageParam, ref query);

            var queryResults = await query.ToListAsync(cancellationToken);

            var results = queryResults.Select(o => CreateFromQuery(o, DB)).ToList() ?? new List<UnitDocumentDTO>();

            if (!string.IsNullOrEmpty(filter.CustomerName))
            {
                results = results.Where(q => q.CustomerName.Contains(filter.CustomerName)).ToList() ?? new List<UnitDocumentDTO>();
            }

            UnitDocumentDTO.SortBy(sortByParam, ref results);

            return new UnitDocumentPaging()
            {
                UnitDocuments = results,
                PageOutput = pageOuput
            };

        }

        // แยก Query ไว้ เพราะแต่ละหน้าใช้ filter ไม่เหมือนกัน จะได้แยกใช้ได้
        private IQueryable<UnitDocumentQueryResult> GetUnitDocumentQueryResult()
        {
            var query = from u in DB.Units.AsNoTracking()
                        .Include(o => o.UnitStatus)
                        .Include(o => o.Project)
                            .ThenInclude(o => o.Company)

                        join b in DB.Bookings.Include(o => o.BookingStatus) on u.ID equals b.UnitID into bData
                        from bModel in bData.DefaultIfEmpty()

                        join ag in DB.Agreements on bModel.ID equals ag.BookingID into agData
                        from agModel in agData.DefaultIfEmpty()

                        join tf in DB.Transfers on agModel.ID equals tf.AgreementID into tfData
                        from tfModel in tfData.DefaultIfEmpty()

                        select new UnitDocumentQueryResult
                        {
                            Unit = u,
                            UnitStatus = u.UnitStatus,
                            Project = u.Project,
                            Company = u.Project.Company,
                            Booking = bModel,
                            BookingStatus = bModel.BookingStatus,
                            Agreement = agModel,
                            Transfer = tfModel,
                        };

            return query;
        }


        public async Task<DocumentOwnerDTO> GetDocumentOwnerAsync(Guid BookingID, CancellationToken cancellationToken = default)
        {
            if (BookingID == Guid.Empty)
                return null;

            var model = new DocumentOwnerQueryResult();

            var agreementOwner = await DB.AgreementOwners.AsNoTracking()
                .Include(o => o.Agreement)
                .Where(o => o.Agreement.BookingID == BookingID && o.Agreement.IsCancel == false && o.IsMainOwner == true)
                .Select(o => new DocumentOwnerQueryResult { Agreement = o.Agreement, AgreementOwner = o }).FirstOrDefaultAsync(cancellationToken) ?? null;

            if (agreementOwner != null)
            {
                model = agreementOwner;
            }
            else
            {
                var bookingOwner = await DB.BookingOwners.AsNoTracking()
                  .Include(o => o.Booking)
                  .Where(o => o.Booking.ID == BookingID && o.IsMainOwner == true && o.IsAgreementOwner == false)
                  .Select(o => new DocumentOwnerQueryResult { Booking = o.Booking, BookingOwner = o }).FirstOrDefaultAsync(cancellationToken);

                model = bookingOwner;
            }

            var result = DocumentOwnerDTO.CreateFromQueryResult(model) ?? new DocumentOwnerDTO();

            return result;


        }
    }
}
