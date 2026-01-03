using Base.DTOs.PRJ;
using Base.DTOs.SAL;
using Database.Models;
using Database.Models.SAL;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.DTOs.FIN
{
    public class AgreementNoTransferDTO
    {

        public Guid? BookingID { get; set; }
        public UnitDropdownDTO Unit { get; set; }
        //public AgreementOwnerDropdownDTO OwnerName { get; set; }

        public string OwnerName { get; set; }
        public static void SortBy(DirectCreditDebitApprovalFormSortByParam sortByParam, ref IQueryable<Agreement> query)
        {
            if (query != null)
            {
                if (sortByParam.SortBy != null)
                {
                    switch (sortByParam.SortBy.Value)
                    {
                        case DirectCreditDebitApprovalFormSortBy.UnitNo:
                            if (sortByParam.Ascending) query = query.OrderBy(o => o.Unit.UnitNo);
                            else query = query.OrderByDescending(o => o.Unit.UnitNo);
                            break;
                        case DirectCreditDebitApprovalFormSortBy.CustomerName:
                            if (sortByParam.Ascending) query = query.OrderBy(o => o.MainOwnerName);
                            else query = query.OrderByDescending(o => o.MainOwnerName);
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
        }

        public static AgreementNoTransferDTO CreateFromModel(Agreement model)
        {
            if (model != null)
            {
                var result = new AgreementNoTransferDTO();
                result.Unit = UnitDropdownDTO.CreateFromModel(model.Unit);
                result.OwnerName = model.AllOwnerName; 
                result.BookingID = model.BookingID;
                return result;
            }
            else
            {
                return null;
            }
        }

    }
}
