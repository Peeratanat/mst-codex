using Base.DTOs.MST;
using Base.DTOs.PRJ;
using Base.DTOs.SAL;
using Base.DTOs.USR;
using Database.Models;
using Database.Models.DbQueries.Finance;
using Database.Models.MST;
using Database.Models.PRJ;
using Database.Models.SAL;
using Database.Models.USR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Base.DTOs.FIN
{
    public class PaymentUnitInfoDTO
    {
        /// <summary>
        /// ข้อมูลโครงการ
        /// </summary>
        public ProjectDropdownDTO Project { get; set; }

        /// <summary>
        /// ข้อมูลแปลง
        /// </summary>
        public UnitDTO Unit { get; set; }

        /// <summary>
        /// ข้อมูลใบจอง
        /// </summary>
        public BookingDTO Booking { get; set; }

        /// <summary>
        /// เจ้าของห้อง
        /// </summary>
        public List<UnitOwner> UnitOwnerList { get; set; }

        /// <summary>
        /// LC ผู้รับผิดชอบ
        /// </summary>
        public UserDTO LCOwner { get; set; }

        /// <summary>
        /// วันที่นัดโอน
        /// </summary>
        public DateTime? ScheduleTransferDate { get; set; }

        /// <summary>
        /// วันที่โอนจริง
        /// </summary>
        public DateTime? ActualTransferDate { get; set; }

        /// <summary>
        /// LC โอน
        /// </summary>
        public UserDTO TransferSaleUser { get; set; }

        /// <summary>
        /// สถานะเครียมโอน
        /// </summary>
        public MasterCenterDTO ReadyToTransferStatus { get; set; }

        /// <summary>
        /// วันที่เตรียมโอน
        /// </summary>
        public DateTime? ReadyToTransferDate { get; set; }

        /// <summary>
        /// รับชำระเงินก่อนโอนได้หรือยัง
        /// </summary>
        public bool IsPreTransferPayment { get; set; }

        public bool IsCanPay_LC { get; set; }

        /// <summary>
        /// AgreementNo
        /// </summary>
        public string AgreementNo { get; set; }
        /// <summary>
        /// ContractDate
        /// </summary>
        public DateTime? ContractDate { get; set; }

        /// <summary>
        /// IsShowPayment แสดงปุ่มรับเงิน
        /// </summary>
        public bool IsShowPayment { get; set; }

        public async static Task<PaymentUnitInfoDTO> CreateFromQueryResultForPaymentAsync(PaymentUnitInfoQueryResult model, DatabaseContext db, DbQueryContext dbQuery)
        {
            if (model != null)
            {
                var result = new PaymentUnitInfoDTO();

                result.Project = ProjectDropdownDTO.CreateFromModel(model.Project);
                result.Unit = UnitDTO.CreateFromModel(model.Unit, null, null);

                result.Booking = BookingDTO.CreateFromModelForPayment(model.Booking);

                result.ScheduleTransferDate = model.Transfer?.ScheduleTransferDate;
                result.ActualTransferDate = model.Transfer?.ActualTransferDate;

                result.TransferSaleUser = UserDTO.CreateFromModel(model.TransferSaleUser);

                result.IsPreTransferPayment = (!string.IsNullOrEmpty(model.Transfer?.TransferNo) && !(model.Transfer?.IsTransferConfirmed ?? false));

                result.IsCanPay_LC = !(model.Transfer?.IsReadyToTransfer ?? false);

                result.LCOwner = UserDTO.CreateFromModel(model.SaleUser);

                result.IsShowPayment = (model.Transfer?.IsTransferConfirmed ?? false) == true ? false : true;

                if (model.Transfer != null)
                {
                    result.ReadyToTransferStatus = new MasterCenterDTO();
                    if (model.Transfer.IsAccountApproved)
                    {
                        result.ReadyToTransferStatus.Name = "สิ้นสุดการโอน";
                    }
                    else if (model.Transfer.IsPaymentConfirmed)
                    {
                        result.ReadyToTransferStatus.Name = "ยืนยันชำระเงิน";
                    }
                    else if (model.Transfer.IsTransferConfirmed)
                    {
                        result.ReadyToTransferStatus.Name = "ยืนยันโอนจริง";
                    }
                    else if (model.Transfer.IsReadyToTransfer)
                    {
                        result.ReadyToTransferStatus.Name = "พร้อมโอน";
                    }
                    else if (!string.IsNullOrEmpty(model.Transfer?.TransferNo))
                    {
                        result.ReadyToTransferStatus.Name = "ตั้งเรื่องโอน";
                    }
                    else
                    {
                        result.ReadyToTransferStatus.Name = "-";
                    }
                }
                result.ReadyToTransferDate = model.Transfer?.ReadyToTransferDate;

                result.UnitOwnerList = new List<UnitOwner>();

                #region Owner
                if (model.Agreement?.AgreementNo != null)
                {
                    string sqlQuery = sqlGetActiveAgreementOwner.QueryString;
                    List<SqlParameter> ParamList = sqlGetActiveAgreementOwner.QueryFilter(ref sqlQuery, model.Agreement.ID);

                    var ActiveAgreementOwnerList = await dbQuery.sqlGetActiveAgreementOwnerResults.FromSqlRaw(sqlQuery, ParamList.ToArray()).Select(o => o.AgreementOwnerID).ToListAsync() ?? new List<Guid?>();

                    result.AgreementNo = model.Agreement?.AgreementNo;
                    result.ContractDate = model.Agreement?.ContractDate;
                    var AgreementOwnerModel = await (from ago in db.AgreementOwners
                          .Include(o => o.National)
                          .Include(o => o.Gender)
                          .Include(o => o.National)
                            //.Where(o => o.AgreementID == model.Agreement.ID)
                            .Where(o => ActiveAgreementOwnerList.Contains(o.ID))
                                                     let agoPhone = db.AgreementOwnerPhones.Where(o => o.AgreementOwnerID == ago.ID && o.IsMain == true).FirstOrDefault()
                                                     let agoEmail = db.AgreementOwnerEmails.Where(o => o.AgreementOwnerID == ago.ID && o.IsMain == true).FirstOrDefault()
                                                     select new
                                                     {
                                                         AgreementOwner = ago,
                                                         Phone = agoPhone ?? new AgreementOwnerPhone(),
                                                         Email = agoEmail ?? new AgreementOwnerEmail(),
                                                     }).OrderBy(o => o.AgreementOwner.Order).ToListAsync();

                    if (AgreementOwnerModel != null)
                    {
                        foreach (var item in AgreementOwnerModel)
                        {
                            var UnitOwner = new UnitOwner();
                            UnitOwner.FirstNameTH = item.AgreementOwner.FirstNameTH;
                            UnitOwner.LastNameTH = item.AgreementOwner.LastNameTH;

                            UnitOwner.FirstNameEN = item.AgreementOwner.FirstNameEN;
                            UnitOwner.LastNameEN = item.AgreementOwner.LastNameEN;

                            UnitOwner.National = item.AgreementOwner.National?.Name;
                            UnitOwner.ContactNo = item.AgreementOwner.ContactNo;
                            UnitOwner.CitizenIdentityNo = item.AgreementOwner.CitizenIdentityNo;

                            UnitOwner.ActiveDate = item.AgreementOwner.Updated;

                            UnitOwner.PhoneNumber = item.Phone?.PhoneNumber;
                            UnitOwner.Email = item.Email?.Email;

                            UnitOwner.DisplayName = (item.AgreementOwner.IsThaiNationality) ? item.AgreementOwner.FullnameTH : item.AgreementOwner.FullnameEN;

                            result.UnitOwnerList.Add(UnitOwner);
                        }
                    }
                }
                else
                {
                    var BookingOwnerModel = await (from bo in db.BookingOwners
                                        .Include(o => o.National)
                                        .Include(o => o.Gender)
                                        .Where(o => o.BookingID == model.Booking.ID && !o.IsAgreementOwner)

                                                   let boPhone = db.BookingOwnerPhones.Where(o => o.BookingOwnerID == bo.ID && o.IsMain == true).FirstOrDefault()
                                                   let boEmail = db.BookingOwnerEmails.Where(o => o.BookingOwnerID == bo.ID && o.IsMain == true).FirstOrDefault()

                                                   select new
                                                   {
                                                       BookingOwner = bo,
                                                       Phone = boPhone ?? new BookingOwnerPhone(),
                                                       Email = boEmail ?? new BookingOwnerEmail(),
                                                   }).OrderBy(o => o.BookingOwner.Order).ToListAsync();

                    if (BookingOwnerModel != null)
                    {
                        foreach (var item in BookingOwnerModel)
                        {
                            var UnitOwner = new UnitOwner();
                            UnitOwner.FirstNameTH = item.BookingOwner.FirstNameTH;
                            UnitOwner.LastNameTH = item.BookingOwner.LastNameTH;

                            UnitOwner.FirstNameEN = item.BookingOwner.FirstNameEN;
                            UnitOwner.LastNameEN = item.BookingOwner.LastNameEN;

                            UnitOwner.National = item.BookingOwner.National?.Name;
                            UnitOwner.ContactNo = item.BookingOwner.ContactNo;
                            UnitOwner.CitizenIdentityNo = item.BookingOwner.CitizenIdentityNo;

                            UnitOwner.ActiveDate = item.BookingOwner.Updated;

                            UnitOwner.PhoneNumber = item.Phone?.PhoneNumber;
                            UnitOwner.Email = item.Email?.Email;

                            UnitOwner.DisplayName = (item.BookingOwner.IsThaiNationality) ? item.BookingOwner.FullnameTH : item.BookingOwner.FullnameEN;

                            result.UnitOwnerList.Add(UnitOwner);
                        }
                    }
                }
                #endregion

                return result;
            }
            else
            {
                return null;
            }
        }
    }

    public class PaymentUnitInfoQueryResult
    {
        public Unit Unit { get; set; }
        public Project Project { get; set; }
        public Booking Booking { get; set; }
        public Agreement Agreement { get; set; }

        public Transfer Transfer { get; set; }
        public User SaleUser { get; set; }
        public User TransferSaleUser { get; set; }
        public MasterCenter TransferStatus { get; set; }
    }

    public class UnitOwner
    {
        public string FirstNameTH { get; set; }
        public string LastNameTH { get; set; }

        public string FirstNameEN { get; set; }
        public string LastNameEN { get; set; }

        public string National { get; set; }

        public string ContactNo { get; set; }

        public DateTime? ActiveDate { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        public string CitizenIdentityNo { get; set; }

        public string DisplayName { get; set; }
    }
}
