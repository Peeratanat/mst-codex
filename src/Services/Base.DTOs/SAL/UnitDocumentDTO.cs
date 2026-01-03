using System;
using Database.Models.SAL;
using Database.Models.PRJ;
using Base.DTOs.PRJ;
using Database.Models;
using Database.Models.MST;
using System.Collections.Generic;
using System.Linq;
using Base.DTOs.FIN;
using Base.DTOs.MST;
using Database.Models.DbQueries.FIN;

namespace Base.DTOs.SAL
{
    public class UnitDocumentDTO : BaseDTO
    {

        /// <summary>
        /// เลขที่เอกสาร
        /// </summary>
        public Guid? BookingID { get; set; }


        /// <summary>
        /// เลขที่เอกสาร
        /// </summary>
        public string DocumentNo { get; set; }

        /// <summary>
        /// แปลง
        /// </summary>
        public UnitDropdownDTO Unit { get; set; }

        /// <summary>
        /// โครงการ
        /// </summary>
        public ProjectDropdownDTO Project { get; set; }

        /// <summary>
        /// ชื่อลูกค้า
        /// </summary>  
        public string CustomerName { get; set; }
        public string CustomerNo { get; set; }

        public class UnitDocumentQueryResult
        {
            public Unit Unit { get; set; }
            public MasterCenter UnitStatus { get; set; }


            public Project Project { get; set; }

            public Company Company { get; set; }

            public Booking Booking { get; set; }

            public MasterCenter BookingStatus { get; set; }

            public Agreement Agreement { get; set; }

            public Transfer Transfer { get; set; }

            public string DocumentNo { get; set; }
            public string CustomerName { get; set; }
        }

        public static UnitDocumentDTO CreateFromQuery(UnitDocumentQueryResult query, DatabaseContext db)
        {
            if (query != null)
            {
                var result = new UnitDocumentDTO()
                {
                    BookingID = query.Booking.ID,
                    DocumentNo = query.Agreement.AgreementNo ?? query.Booking.BookingNo,
                    Unit = UnitDropdownDTO.CreateFromModel(query.Unit),
                    Project = ProjectDropdownDTO.CreateFromModel(query.Project),
                };

                var CustomerName = "";

                List<string> CustomerList = new List<string>();

                if ((query.Agreement.AgreementNo ?? "") != "")
                {
                    var AgreementOwnerModel = db.AgreementOwners.Where(e => e.AgreementID == query.Agreement.ID).ToList() ?? new List<AgreementOwner>();
                    if (AgreementOwnerModel.Any())
                        CustomerList = AgreementOwnerModel.Select(e => (e.FirstNameTH ?? "") + " " + (e.LastNameTH ?? "")).ToList() ?? new List<string>();
                }
                else
                {
                    var BookingOwnerModel = db.BookingOwners.Where(e => e.BookingID == query.Booking.ID).ToList() ?? new List<BookingOwner>();
                    if (BookingOwnerModel.Any())
                        CustomerList = BookingOwnerModel.Select(e => (e.FirstNameEN ?? "") + " " + (e.LastNameEN ?? "")).ToList() ?? new List<string>();
                }


                if (CustomerList.Any())
                {
                    foreach (var name in CustomerList)
                    {
                        if (name.Replace(" ", "").Length > 0)
                            CustomerName += "," + name;
                    }

                    CustomerName = (CustomerName.Length > 2) ? CustomerName.Substring(1, CustomerName.Length - 1) : CustomerName;
                }

                result.CustomerName = CustomerName;


                return result;
            }
            else
            {
                return null;
            }
        }

        public static UnitDocumentDTO CreateFromSPFINUnitDocument(dbqFINUnitDocument model)
        {
            if (model != null)
            {
                var result = new UnitDocumentDTO()
                {
                    BookingID = model.BookingID,
                    DocumentNo = model.DocumentNo,
                    Unit = new UnitDropdownDTO {Id = model.UnitID??Guid.Empty,
                                                UnitNo = model.UnitNo },
                    Project = new ProjectDropdownDTO {Id = model.ProjectID,
                                                        ProjectNameTH = model.ProjectNameTH,
                                                        ProjectNo = model.ProjectNo },
                    CustomerName = model.CustomerName,
                    CustomerNo = model.CustomerNo
                };
                result.Project.Company = new CompanyDropdownDTO { Id = model.CompanyID ?? Guid.Empty};
                return result;
            }
            else
            {
                return null;
            }
        }

        public static void SortByQueryResult(UnitDocumentFINSortByParam sortByParam, ref IQueryable<UnitDocumentQueryResult> model)
        {
            if (sortByParam.SortBy != null)
            {

                switch (sortByParam.SortBy.Value)
                {
                    case UnitDocumentFinSortBy.DocumentNo:
                        if (sortByParam.Ascending) model = model.OrderBy(o => o.DocumentNo);
                        else model = model.OrderByDescending(o => o.DocumentNo);
                        break;

                    case UnitDocumentFinSortBy.Project:
                        if (sortByParam.Ascending) model = model.OrderBy(o => o.Project.ProjectNo);
                        else model = model.OrderByDescending(o => o.Project.ProjectNo);
                        break;

                    case UnitDocumentFinSortBy.Unit:
                        if (sortByParam.Ascending) model = model.OrderBy(o => o.Unit.UnitNo);
                        else model = model.OrderByDescending(o => o.Unit.UnitNo);
                        break;

                    case UnitDocumentFinSortBy.CustomerName:
                        if (sortByParam.Ascending) model = model.OrderBy(o => o.CustomerName);
                        else model = model.OrderByDescending(o => o.CustomerName);
                        break;
                    default:
                        model = model.OrderBy(o => o.Project.ProjectNo).ThenBy(o => o.Unit.UnitNo);
                        break;
                }
            }
            else
            {
                model = model.OrderBy(o => o.Project.ProjectNo).ThenBy(o => o.Unit.UnitNo);
            }
        }

        public static void SortBy(UnitDocumentSortByParam sortByParam, ref List<UnitDocumentDTO> model)
        {
            if (sortByParam.SortBy != null)
            {

                switch (sortByParam.SortBy.Value)
                {
                    case UnitDocumentSortBy.DocumentNo:
                        if (sortByParam.Ascending) model = model.OrderBy(o => o.DocumentNo).ToList();
                        else model = model.OrderByDescending(o => o.DocumentNo).ToList();
                        break;

                    case UnitDocumentSortBy.Project:
                        if (sortByParam.Ascending) model = model.OrderBy(o => o.Project.ProjectNo).ToList();
                        else model = model.OrderByDescending(o => o.Project.ProjectNo).ToList();
                        break;

                    case UnitDocumentSortBy.Unit:
                        if (sortByParam.Ascending) model = model.OrderBy(o => o.Unit.UnitNo).ToList();
                        else model = model.OrderByDescending(o => o.Unit.UnitNo).ToList();
                        break;

                    case UnitDocumentSortBy.CustomerName:
                        if (sortByParam.Ascending) model = model.OrderBy(o => o.CustomerName).ToList();
                        else model = model.OrderByDescending(o => o.CustomerName).ToList();
                        break;
                    default:
                        model = model.OrderBy(o => o.Project.ProjectNo).ThenBy(o => o.Unit.UnitNo).ToList();
                        break;
                }
            }
            else
            {
                model = model.OrderBy(o => o.Project.ProjectNo).ThenBy(o => o.Unit.UnitNo).ToList();
            }
        }
    }
}
