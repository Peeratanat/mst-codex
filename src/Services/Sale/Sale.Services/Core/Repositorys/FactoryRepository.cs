using Database.Models;

namespace Sale.Services.Core.Repositorys
{
    public class FactoryRepository : IFactoryRepository
    {
        private DatabaseContext _db { get; set; }
        private DbQueryContext _dbQuery { get; set; }
        // private readonly IAgreementRepository _agreementRepository;
        // private readonly IMasterCenterRepository _masterRepository;
        // private readonly IBookingRepository _bookingRepository;
        // private readonly IUnitRepository _unitRepository;
        public FactoryRepository(DatabaseContext db)
        {
            _db = db;
        }
        public FactoryRepository(DatabaseContext db, DbQueryContext dbQuery)
        {
            _db = db;
            _dbQuery = dbQuery;
        }
        // public IMasterCenterRepository CreateMasterStatusRepository()
        // {
        //     return new MasterCenterRepository(_db);
        // }

        // public IPromotionRepository CreatePromotionRepository()
        // {
        //     return new PromotionRepository(_db);
        // }

        // public IUnitRepository CreateUnitRepository()
        // {
        //     return new UnitRepository(_db, _dbQuery);
        // }

        // public IBookingRepository CreateBookingRepository()
        // {
        //     return new BookingRepository(_db);
        // }

        // public IWorkFlowMinPriceRepository CreateWorkFlowMinPriceRepository()
        // {
        //     return new WorkFlowMinPriceRepository(_db, _dbQuery);
        // }

        // public IWorkFlowPriceListRepository CreateWorkFlowPriceListRepository()
        // {
        //     return new WorkFlowPriceListRepository(_db);
        // }

        // public IWorkFlowChangeUnitRepository CreateWorkFlowChangeUnitRepository()
        // {
        //     return new WorkFlowChangeUnitRepository(_db);
        // }

        // public IAgreementRepository CreateAgreementRepository()
        // {
        //     return new AgreementRepository(_db, _dbQuery);
        // }

        // public IQuotationRepository CreateQuotationRepository()
        // {
        //     return new QuotationRepository(_db);
        // }

        // public ITranferRepository CreateTranferRepository()
        // {
        //     return new TranferRepository(_db, _dbQuery);
        // }

        // public DatabaseContext CreateDb()
        // {
        //     return _db;
        // }

        // public DbQueryContext CreateDbQuery()
        // {
        //     return _dbQuery;
        // }
    }
}
