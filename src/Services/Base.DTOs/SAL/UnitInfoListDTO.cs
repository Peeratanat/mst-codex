using Base.DTOs.USR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using models = Database.Models;
using Database.Models.DbQueries.SAL;
using Database.Models;
using Newtonsoft.Json;

namespace Base.DTOs.SAL
{
    public class UnitInfoListDTO : BaseDTO
    {
        /// <summary>
        /// แปลง
        /// </summary>
        public PRJ.UnitDropdownDTO Unit { get; set; }

        /// <summary>
        /// โครงการ
        /// </summary>
        public PRJ.ProjectDropdownDTO Project { get; set; }

        /// <summary>
        /// ชื่อจริง (ผู้จอง/ผู้ทำสัญญา)
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// นามสกุล (ผู้จอง/ผู้ทำสัญญา)
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// ใบจอง
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public BookingListDTO Booking { get; set; }

        /// <summary>
        /// ใบสัญญา
        /// </summary>
        public AgreementListDTO Agreement { get; set; }

        /// <summary>
        /// โปรโอน
        /// </summary>
        public SAL.TransferPromotionDTO TransferPromotion { get; set; }

        /// <summary>
        /// ธนาคารที่ขอสินเชื่อ
        /// </summary>
        public FIN.BankAccNameDTO Bank { get; set; }

        /// <summary>
        /// ธนาคารที่ขอสินเชื่อ
        /// </summary>
        public CreditBankingDTO CreditBanking { get; set; }

        /// <summary>
        /// โอนกรรมสิทธิ์
        /// </summary>
        public TransferListDTO Transfer { get; set; }

        /// <summary>
        /// LC ผู้รับผิดชอบ
        /// </summary>
        public UserDTO LCOwner { get; set; }

        /*
        public static async Task<UnitInfoListDTO> CreateFromQueryResultAsync(UnitInfoListQueryResult model, models.DatabaseContext DB)
        {
            if (model != null)
            {
                var firstName = "";
                var lastName = "";
                if (model.AgreementOwner != null)
                {
                    firstName = model.AgreementOwner.FirstNameTH;
                    lastName = model.AgreementOwner.LastNameTH;
                }
                else if (model.BookingOwner != null)
                {
                    firstName = model.BookingOwner.FirstNameTH;
                    lastName = model.BookingOwner.LastNameTH;
                }

                var result = new UnitInfoListDTO()
                {
                    Id = model.Unit.ID,
                    Unit = PRJ.UnitDropdownDTO.CreateFromModel(model.Unit),
                    Project = PRJ.ProjectDropdownDTO.CreateFromModel(model.Unit.Project),
                    FirstName = firstName,
                    LastName = lastName,
                    Booking = BookingListDTO.CreateFromModel(model.Booking, DB),
                    Agreement = AgreementListDTO.CreateFromModel(model.Agreement, DB),
                    Transfer = TransferListDTO.CreateFromModel(model.Transfer, DB),
                    Bank = new FIN.BankAccNameDTO(), //TODO : Kim ธนาคารที่ขอโฉนด
                    LCOwner = UserDTO.CreateFromModel(model.LCOwner)
                };

                //var TransferPromotion = await DB.TransferPromotions.Where(o => o.BookingID == model.Booking.ID).FirstOrDefaultAsync();
                result.TransferPromotion = await TransferPromotionDTO.CreateFromModelAsync(model.TransferPromotion, DB);

                //var LoanStatusApproveID = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == "LoanStatus" && o.Key == "1").Select(o => o.ID).FirstOrDefaultAsync();
                //var CreditBanking = await DB.CreditBankings.Where(o => o.BookingID == model.Booking.ID && (o.IsUseBank.HasValue && o.IsUseBank.Value) && o.LoanStatusMasterCenterID == LoanStatusApproveID).FirstOrDefaultAsync();
                result.CreditBanking = CreditBankingDTO.CreateFromModel(model.CreditBanking);

                return result;
            }
            else
            {
                return null;
            }
        }

        public static void SortBy(UnitInfoListSortByParam sortByParam, ref IQueryable<UnitInfoListQueryResult> query)
        {
            if (sortByParam.SortBy != null)
            {
                switch (sortByParam.SortBy.Value)
                {
                    case UnitInfoListSortBy.UnitNo:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.Unit.UnitNo);
                        else query = query.OrderByDescending(o => o.Unit.UnitNo);
                        break;
                    case UnitInfoListSortBy.HouseNo:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.Unit.HouseNo);
                        else query = query.OrderByDescending(o => o.Unit.HouseNo);
                        break;
                    case UnitInfoListSortBy.FullName:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.BookingOwner.FirstNameTH).ThenBy(o => o.BookingOwner.LastNameTH);
                        else query = query.OrderByDescending(o => o.BookingOwner.FirstNameTH).ThenByDescending(o => o.BookingOwner.LastNameTH);
                        break;
                    case UnitInfoListSortBy.BookingNo:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.Booking.BookingNo);
                        else query = query.OrderByDescending(o => o.Booking.BookingNo);
                        break;
                    case UnitInfoListSortBy.ProjectNo:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.Project.ProjectNo);
                        else query = query.OrderByDescending(o => o.Project.ProjectNo);
                        break;
                    case UnitInfoListSortBy.UnitStatus:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.UnitStatus.Name);
                        else query = query.OrderByDescending(o => o.UnitStatus.Name);
                        break;
                    default:
                        query = query.OrderBy(o => o.Unit.UnitNo);
                        break;
                }
            }
            else
            {
                query = query.OrderBy(o => o.Unit.UnitNo);
            }
        }
        */

        public static UnitInfoListDTO CreateFromQuery(dbqUnitInfoList model, DatabaseContext db)
        {
            if (model != null)
            {
                var modelUnit = db.Units
                                    .Include(o => o.Tower).Include(o => o.Floor).Include(o => o.UnitStatus)
                                    .Where(e => e.ID == model.UnitID).FirstOrDefault();
                var modelProject = db.Projects
                                    .Include(o => o.ProjectStatus)
                                    .Include(o => o.ProductType)
                                    //.Include(o => o.Company)
                                    .Include(o => o.BG)
                                    .Where(e => e.ID == model.ProjectID).FirstOrDefault();

                var modelBooking = db.Bookings
                                    .Include(o => o.BookingStatus)
                                    .Include(o => o.CreditBankingType)
                                    .Where(e => e.ID == model.BookingID).FirstOrDefault();
                var modelAgreement = db.Agreements
                                        .Include(o => o.AgreementStatus)
                                        .Include(o => o.SignContractRequestUser)
                                        .Where(e => e.ID == model.AgreementID).FirstOrDefault();
                var modelTransferPromotion = db.TransferPromotions
                                                .Include(o => o.MasterPromotion)
                                                .Include(o => o.TransferPromotionStatus)
                                                .Where(e => e.ID == model.TransferPromotionID).FirstOrDefault();
                var modelTransfer = db.Transfers
                                    .Include(o => o.TransferStatus)
                                    .Where(e => e.ID == model.TransferID).FirstOrDefault();
                var modelCreditBanking = db.CreditBankings
                                    .Include(o => o.Bank)
                                    .Include(o => o.FinancialInstitution)
                                    .Include(o => o.BankBranch)
                                    .Include(o => o.LoanStatus)
                                    .Where(e => e.ID == model.CreditBankingID).FirstOrDefault();
                var modelLCOwner = db.Users.Where(e => e.ID == model.LCOwnerID).FirstOrDefault();

                UnitInfoListDTO result = new UnitInfoListDTO();

                result.Unit = PRJ.UnitDropdownDTO.CreateFromModel(modelUnit);
                result.Project = PRJ.ProjectDropdownDTO.CreateFromModel(modelProject);
                result.FirstName = model.FirstName;
                result.LastName = model.LastName;
                result.Booking = BookingListDTO.CreateFromModel(modelBooking, db);
                result.Agreement = AgreementListDTO.CreateFromModel(modelAgreement, db);
                result.TransferPromotion = TransferPromotionDTO.CreateFromModel(modelTransferPromotion, db);
                //result.Bank = FIN.BankAccNameDTO
                result.CreditBanking = CreditBankingDTO.CreateFromModel(modelCreditBanking, db);
                result.Transfer = TransferListDTO.CreateFromModel(modelTransfer, db);
                result.LCOwner = UserDTO.CreateFromModel(modelLCOwner);
                result.Id = model.UnitID;

                return result;
            }
            else
            {
                return null;
            }
        }
    }

    public class UnitInfoListQueryResult
    {
        public models.PRJ.Unit Unit { get; set; }
        public models.PRJ.Project Project { get; set; }
        public models.MST.MasterCenter UnitStatus { get; set; }
        public models.SAL.Booking Booking { get; set; }
        public models.SAL.Agreement Agreement { get; set; }
        public models.SAL.Transfer Transfer { get; set; }
        public models.SAL.BookingOwner BookingOwner { get; set; }
        public models.SAL.AgreementOwner AgreementOwner { get; set; }
        public models.USR.User LCOwner { get; set; }
        public models.SAL.CreditBanking CreditBanking { get; set; }
        public models.PRM.TransferPromotion TransferPromotion { get; set; }
    }
}
