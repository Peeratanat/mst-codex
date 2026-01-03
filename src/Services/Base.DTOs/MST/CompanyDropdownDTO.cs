using Database.Models.MST;
using System;
using System.Linq;

namespace Base.DTOs.MST
{
    public class CompanyDropdownDTO
    {
        public Guid Id { get; set; }
        /// <summary>
        /// รหัสริษัท
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// ชื่อบริษัทภาษาอังกฤษ
        /// </summary>
        public string NameEN { get; set; }
        /// <summary>
        /// ชื่อบริษัทภาษาไทย
        /// </summary>
        public string NameTH { get; set; }
        /// <summary>
        /// รหัสบริษัท SAP
        /// </summary>
        public string SAPCompanyID { get; set; }
        public string TaxID { get; set; }

        public static void SortBy(CompanyDropdownSortByParam sortByParam, ref IQueryable<Company> query)
        {
            if (sortByParam.SortBy != null)
            {
                switch (sortByParam.SortBy.Value)
                {
                    case CompanyDropdownSortBy.Code:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.Code);
                        else query = query.OrderByDescending(o => o.Code);
                        break;
                    case CompanyDropdownSortBy.NameTH:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.NameTH);
                        else query = query.OrderByDescending(o => o.NameTH);
                        break;
                    case CompanyDropdownSortBy.NameEN:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.NameEN);
                        else query = query.OrderByDescending(o => o.NameEN);
                        break;
                    case CompanyDropdownSortBy.SAPCompanyID:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.SAPCompanyID);
                        else query = query.OrderByDescending(o => o.SAPCompanyID);
                        break;
                    default:
                        query = query.OrderBy(o => o.SAPCompanyID);
                        break;
                }
            }
            else
            {
                query = query.OrderBy(o => o.SAPCompanyID);
            }
        }

        public static CompanyDropdownDTO CreateFromModel(Company model)
        {
            if (model != null)
            {
                var result = new CompanyDropdownDTO()
                {
                    Id = model.ID,
                    Code = model.Code,
                    NameEN = model.NameEN,
                    NameTH = model.NameTH,
                    SAPCompanyID = model.SAPCompanyID,
                    TaxID = model.TaxID
                };

                return result;
            }
            else
            {
                return null;
            }
        }
    }
}
