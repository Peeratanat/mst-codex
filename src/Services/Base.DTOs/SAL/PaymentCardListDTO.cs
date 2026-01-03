using Base.DTOs;
using Base.DTOs.SAL.Sortings;
using Database.Models;
using Database.Models.PRJ;
using Database.Models.SAL;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.DTOs.SAL
{
    public class PaymentCardListDTO : BaseDTO
    {
        /// <summary>
        /// เลือก
        /// </summary>
        public bool IsSelected { get; set; }

        /// <summary>
        /// โครงการ
        /// </summary>
        public PRJ.ProjectDropdownDTO Project { get; set; }

        /// <summary>
        /// แปลง
        /// </summary>
        public PRJ.UnitDropdownDTO Unit { get; set; }

        /// <summary>
        /// สัญญา
        /// </summary>
        public AgreementDropdownDTO Agreement { get; set; }

        /// <summary>
        /// ผู้ทำสัญญา
        /// </summary>
        public AgreementOwnerDTO Owner { get; set; }

        /// <summary>
        /// เบอร์โทรศัพท์
        /// </summary>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// สถานะการพิมพ์
        /// </summary>
        public bool? IsPrintPaymentCard { get; set; }

        /// <summary>
        /// วันที่พิมพ์ Payment card
        /// </summary>
        public DateTime? PrintPaymentCardDate { get; set; }

        public static PaymentCardListDTO CreateFromQueryResult(PaymentCardQueryResult model)
        {
            if (model != null)
            {
                //var aOwner = AgreementOwnerDTO.CreateFromModel(model.AgreementOwner);

                var result = new PaymentCardListDTO()
                {
                    Id = model.Agreement.ID,
                    Project = PRJ.ProjectDropdownDTO.CreateFromModel(model.Project),
                    Unit = PRJ.UnitDropdownDTO.CreateFromModel(model.Unit),
                    Agreement = AgreementDropdownDTO.CreateFromModel(model.Agreement),
                    Owner = AgreementOwnerDTO.CreateFromModel(model.AgreementOwner),
                    PhoneNumber = model.PhoneNumber,
                    IsPrintPaymentCard = model.Agreement.IsPrintPaymentCard,
                    PrintPaymentCardDate = model.Agreement.PrintPaymentCardDate,
                    IsSelected = false
                };

                return result;
            }
            else
            {
                return null;
            }
        }

        public static async Task<PaymentCardListDTO> CreateFromModelAsync(Agreement model, DatabaseContext DB)
        {
            if (model != null)
            {
                var AgreementOwner = await DB.AgreementOwners.Where(o => o.AgreementID == model.ID && o.IsMainOwner == true).FirstOrDefaultAsync();
                var aOwner = AgreementOwnerDTO.CreateFromModel(AgreementOwner);
                var phones = await DB.AgreementOwnerPhones.Include(o => o.PhoneType).Where(o => o.AgreementOwnerID == AgreementOwner.ID && o.IsDeleted == false).ToListAsync();
  
                var result = new PaymentCardListDTO()
                {
                    Id = model.ID,
                    Unit = PRJ.UnitDropdownDTO.CreateFromModel(model.Unit),
                    Project = PRJ.ProjectDropdownDTO.CreateFromModel(model.Project),
                    Agreement = AgreementDropdownDTO.CreateFromModel(model),
                    Owner = aOwner,
                    PhoneNumber = phones[0].PhoneNumber,
                    IsPrintPaymentCard = model.IsPrintPaymentCard,
                    PrintPaymentCardDate = model.PrintPaymentCardDate,
                    IsSelected = false
                };

                return result;
            }
            else
            {
                return null;
            }
        }

        public static void SortBy(PaymentCardListSortByParam sortByParam, ref IQueryable<PaymentCardQueryResult> query)
        {
            if (sortByParam.SortBy != null)
            {
                switch (sortByParam.SortBy.Value)
                {
                    case PaymentCardListSortBy.UnitNo:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.Unit.UnitNo);
                        else query = query.OrderByDescending(o => o.Unit.UnitNo);
                        break;
                    case PaymentCardListSortBy.AgreementNo:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.Agreement.AgreementNo);
                        else query = query.OrderByDescending(o => o.Agreement.AgreementNo);
                        break;
                    case PaymentCardListSortBy.FullName:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.AgreementOwner.FullnameTH);
                        else query = query.OrderByDescending(o => o.AgreementOwner.FullnameTH);
                        break;
                    case PaymentCardListSortBy.PhoneNumber:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.PhoneNumber);
                        else query = query.OrderByDescending(o => o.PhoneNumber);
                        break;
                    case PaymentCardListSortBy.IsPrintPaymentCard:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.Agreement.IsPrintPaymentCard);
                        else query = query.OrderByDescending(o => o.Agreement.IsPrintPaymentCard);
                        break;
                    case PaymentCardListSortBy.PrintPaymentCardDate:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.Agreement.PrintPaymentCardDate);
                        else query = query.OrderByDescending(o => o.Agreement.PrintPaymentCardDate);
                        break;
                    default:
                        query = query.OrderByDescending(o => o.Unit.UnitNo);
                        break;
                }
            }
            else
            {
                query = query.OrderByDescending(o => o.Unit.UnitNo);
            }
        }
    }

    public class PaymentCardQueryResult
    {
        public Project Project { get; set; }
        public Unit Unit { get; set; }
        public Agreement Agreement { get; set; }
        public AgreementOwner AgreementOwner { get; set; }
        public string PhoneNumber { get; set; }
    }
}
