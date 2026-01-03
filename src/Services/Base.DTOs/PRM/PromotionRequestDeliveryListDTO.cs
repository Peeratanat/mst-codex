using Base.DTOs.PRJ;
using Base.DTOs.PRM.Sortings;
using Database.Models;
using Database.Models.DbQueries.PRM;
using Database.Models.PRM;
using Database.Models.SAL;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Base.DTOs.PRM
{
    /// <summary>
    /// ข้อมูลเบิก/ส่งมอบโปรโมชั่น
    /// </summary>
    public class PromotionRequestDeliveryListDTO : BaseDTO
    {
        /// <summary>
        /// โครงการ
        /// Project/api/Projects/DropdownList
        /// </summary>
        public PRJ.ProjectDropdownDTO Project { get; set; }

        /// <summary>
        /// แปลง
        /// </summary>
        public PRJ.UnitDropdownDTO Unit { get; set; }

        /// <summary>
        /// ชื่อลูกค้า
        /// </summary>
        public string CustomerName { get; set; }

        /// <summary>
        /// วันที่ทำสัญญา
        /// </summary>
        public DateTime? ContractDate { get; set; }

        /// <summary>
        /// วันที่โอน
        /// กรณียังไม่ได้ทำการโอน ให้แสดงวันที่นัดโอนกรรมสิทธิ์ตามสัญญา
        /// กรณีที่โอนกรรมสิทธิ์แล้ว ให้ใช้วันที่โอนจริง
        /// </summary>
        public DateTime? TransferDate { get; set; }

        /// <summary>
        /// สถานะเบิกโปรขาย
        /// 0 = ไม่มีรายการเบิก
        /// 1 = รอเบิก 
        /// 2 = เบิกยังไม่ครบ
        /// 3 = เบิกครบแล้ว
        /// </summary>
        public int SalePromotionRequestStatus { get; set; }

        /// <summary>
        /// สถานะเบิกโปรโอน
        /// 0 = ไม่มีรายการเบิก
        /// 1 = รอเบิก 
        /// 2 = เบิกยังไม่ครบ
        /// 3 = เบิกครบแล้ว
        /// </summary>
        public int TransferPromotionRequestStatus { get; set; }

        /// <summary>
        /// สถานะส่งมอบโปรขาย
        /// 0 = ไม่มีรายการส่งมอบ 
        /// 1 = รอส่งมอบ  
        /// 2 = ส่งมอบยังไม่ครบ
        /// 3 = ส่งมอบครบแล้ว
        /// </summary>
        public int SalePromotionDeliveryStatus { get; set; }

        /// <summary>
        /// สถานะส่งมอบโปรโอน
        /// 0 = ไม่มีรายการส่งมอบ 
        /// 1 = รอส่งมอบ  
        /// 2 = ส่งมอบยังไม่ครบ
        /// 3 = ส่งมอบครบแล้ว
        /// </summary>
        public int TransferPromotionDeliveryStatus { get; set; }

        //public bool 

        //public static PromotionRequestDeliveryListDTO CreateFromQueryResult(PromotionRequestDeliveryListQueryResult model, DatabaseContext DB)
        //{
        //    try
        //    {
        //        if (model != null)
        //        {
        //            var result = new PromotionRequestDeliveryListDTO()
        //            {
        //                Id = model.Agreement.ID,
        //                Project = PRJ.ProjectDropdownDTO.CreateFromModel(model.Agreement.Project),
        //                Unit = PRJ.UnitDropdownDTO.CreateFromModel(model.Agreement.Unit),
        //                CustomerName = model.AgreementOwner.FirstNameTH + " " + model.AgreementOwner.LastNameTH,
        //                ContractDate = model.Agreement.ContractDate,
        //                TransferDate = model.Transfer?.ActualTransferDate ?? model.Agreement.TransferOwnershipDate,
        //                //SalePromotionRequestStatus = model.SalePromotionRequestStatus,
        //                //TransferPromotionRequestStatus = model.TransferPromotionRequestStatus,
        //                //SalePromotionDeliveryStatus = model.SalePromotionDeliveryStatus,
        //                //TransferPromotionDeliveryStatus = model.TransferPromotionDeliveryStatus
        //            };

        //            //สถานะเบิกโปรขาย
        //            if (model.SalePromotionAmount == 0)
        //            {
        //                result.SalePromotionRequestStatus = 0;
        //            }
        //            else if (model.SalePromotionAmount > 0 && model.SalePromotionRequestAmount == 0)
        //            {
        //                result.SalePromotionRequestStatus = 1;
        //            }
        //            else if (model.SalePromotionAmount > 0 && model.SalePromotionRequestAmount > 0 && model.SalePromotionAmount > model.SalePromotionRequestAmount)
        //            {
        //                result.SalePromotionRequestStatus = 2;
        //            }
        //            else if (model.SalePromotionAmount > 0 && model.SalePromotionRequestAmount > 0 && model.SalePromotionAmount == model.SalePromotionRequestAmount)
        //            {
        //                result.SalePromotionRequestStatus = 3;
        //            }

        //            //สถานะเบิกโปรโอน
        //            if (model.TransferPromotionAmount == 0)
        //            {
        //                result.TransferPromotionRequestStatus = 0;
        //            }
        //            else if (model.TransferPromotionAmount > 0 && model.TransferPromotionRequestAmount == 0)
        //            {
        //                result.TransferPromotionRequestStatus = 1;
        //            }
        //            else if (model.TransferPromotionAmount > 0 && model.TransferPromotionRequestAmount > 0 && model.TransferPromotionAmount > model.TransferPromotionRequestAmount)
        //            {
        //                result.TransferPromotionRequestStatus = 2;
        //            }
        //            else if (model.TransferPromotionAmount > 0 && model.TransferPromotionRequestAmount > 0 && model.TransferPromotionAmount == model.TransferPromotionRequestAmount)
        //            {
        //                result.TransferPromotionRequestStatus = 3;
        //            }

        //            //สถานะส่งมอบโปรขาย
        //            if (model.SalePromotionRequestAmount == 0)
        //            {
        //                result.SalePromotionDeliveryStatus = 0;
        //            }
        //            else if (model.SalePromotionRequestAmount > 0 && model.SalePromotionDeliveryAmount == 0)
        //            {
        //                result.SalePromotionDeliveryStatus = 1;
        //            }
        //            else if (model.SalePromotionRequestAmount > 0 && model.SalePromotionDeliveryAmount > 0 && model.SalePromotionRequestAmount > model.SalePromotionDeliveryAmount)
        //            {
        //                result.SalePromotionDeliveryStatus = 2;
        //            }
        //            else if (model.SalePromotionRequestAmount > 0 && model.SalePromotionDeliveryAmount > 0 && model.SalePromotionRequestAmount == model.SalePromotionDeliveryAmount)
        //            {
        //                result.SalePromotionDeliveryStatus = 3;
        //            }

        //            //สถานะส่งมอบโปรโอน
        //            if (model.TransferPromotionRequestAmount == 0)
        //            {
        //                result.TransferPromotionDeliveryStatus = 0;
        //            }
        //            else if (model.TransferPromotionRequestAmount > 0 && model.TransferPromotionDeliveryAmount == 0)
        //            {
        //                result.TransferPromotionDeliveryStatus = 1;
        //            }
        //            else if (model.TransferPromotionRequestAmount > 0 && model.TransferPromotionDeliveryAmount > 0 && model.TransferPromotionRequestAmount > model.TransferPromotionDeliveryAmount)
        //            {
        //                result.TransferPromotionDeliveryStatus = 2;
        //            }
        //            else if (model.TransferPromotionRequestAmount > 0 && model.TransferPromotionDeliveryAmount > 0 && model.TransferPromotionRequestAmount == model.TransferPromotionDeliveryAmount)
        //            {
        //                result.TransferPromotionDeliveryStatus = 3;
        //            }

        //            return result;
        //        }
        //        else
        //        {
        //            return null;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        var x = ex.Message;
        //        return null;
        //    }
        //}

        //public static void SortBy(PromotionRequestDeliveryListSortByParam sortByParam, ref IQueryable<PromotionRequestDeliveryListQueryResult> query)
        //{
        //    IOrderedQueryable<PromotionRequestDeliveryListQueryResult> orderQuery;
        //    if (sortByParam.SortBy != null)
        //    {
        //        switch (sortByParam.SortBy.Value)
        //        {
        //            case PromotionRequestDeliveryListSortBy.UnitNo:
        //                if (sortByParam.Ascending) orderQuery = query.OrderBy(o => o.Agreement.Unit.UnitNo);
        //                else orderQuery = query.OrderByDescending(o => o.Agreement.Unit.UnitNo);
        //                break;
        //            case PromotionRequestDeliveryListSortBy.CustomerName:
        //                if (sortByParam.Ascending) orderQuery = query.OrderBy(o => o.AgreementOwner.FirstNameTH);
        //                else orderQuery = query.OrderByDescending(o => o.AgreementOwner.FirstNameTH);
        //                break;
        //            case PromotionRequestDeliveryListSortBy.ContractDate:
        //                if (sortByParam.Ascending) orderQuery = query.OrderBy(o => o.Agreement.ContractDate);
        //                else orderQuery = query.OrderByDescending(o => o.Agreement.ContractDate);
        //                break;
        //            case PromotionRequestDeliveryListSortBy.TransferDate:
        //                if (sortByParam.Ascending) orderQuery = query.OrderBy(o => o.Transfer.ActualTransferDate).ThenBy(o => o.Agreement.TransferOwnershipDate);
        //                else orderQuery = query.OrderByDescending(o => o.Transfer.ActualTransferDate).ThenByDescending(o => o.Agreement.TransferOwnershipDate);
        //                break;
        //            //case PromotionRequestDeliveryListSortBy.SalePromotionRequestStatus:
        //            //    if (sortByParam.Ascending) orderQuery = query.OrderBy(o => o.SalePromotionRequestStatus);
        //            //    else orderQuery = query.OrderByDescending(o => o.SalePromotionRequestStatus);
        //            //    break;
        //            //case PromotionRequestDeliveryListSortBy.TransferPromotionRequestStatus:
        //            //    if (sortByParam.Ascending) orderQuery = query.OrderBy(o => o.TransferPromotionRequestStatus);
        //            //    else orderQuery = query.OrderByDescending(o => o.TransferPromotionRequestStatus);
        //            //    break;
        //            //case PromotionRequestDeliveryListSortBy.SalePromotionDeliveryStatus:
        //            //    if (sortByParam.Ascending) orderQuery = query.OrderBy(o => o.SalePromotionDeliveryStatus);
        //            //    else orderQuery = query.OrderByDescending(o => o.SalePromotionDeliveryStatus);
        //            //    break;
        //            //case PromotionRequestDeliveryListSortBy.TransferPromotionDeliveryStatus:
        //            //    if (sortByParam.Ascending) orderQuery = query.OrderBy(o => o.TransferPromotionDeliveryStatus);
        //            //    else orderQuery = query.OrderByDescending(o => o.TransferPromotionDeliveryStatus);
        //            //    break;
        //            default:
        //                orderQuery = query.OrderBy(o => o.Agreement.Unit.UnitNo);
        //                break;
        //        }
        //    }
        //    else
        //    {
        //        orderQuery = query.OrderBy(o => o.Agreement.Unit.UnitNo);
        //    }

        //    orderQuery.ThenBy(o => o.Agreement.ID);
        //    query = orderQuery;
        //}

        //public class PromotionRequestDeliveryListQueryResult
        //{
        //    public Agreement Agreement { get; set; }
        //    public AgreementOwner AgreementOwner { get; set; }
        //    public Transfer Transfer { get; set; }
        //    public int? SalePromotionAmount { get; set; }
        //    public int? TransferPromotionAmount { get; set; }
        //    public int? SalePromotionRequestAmount { get; set; }
        //    public int? TransferPromotionRequestAmount { get; set; }
        //    public int? SalePromotionDeliveryAmount { get; set; }
        //    public int? TransferPromotionDeliveryAmount { get; set; }
        //}

        public static PromotionRequestDeliveryListDTO CreateFromQuery(dbqPromotionRequestDeliveryList model, DatabaseContext db)
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

                var agreemet = db.Agreements
                                    .Where(o=>o.UnitID == modelUnit.ID).FirstOrDefault();
                var transfer = db.Transfers
                                    .Where(o=>o.AgreementID == agreemet.ID).FirstOrDefault();

                PromotionRequestDeliveryListDTO result = new PromotionRequestDeliveryListDTO();

                result.Unit = PRJ.UnitDropdownDTO.CreateFromModel(modelUnit);
                result.Project = PRJ.ProjectDropdownDTO.CreateFromModel(modelProject);
                result.CustomerName = model.FullName;
                result.ContractDate = model.ContractDate;
                //result.TransferDate = model.TransferDate;

                if (transfer?.IsTransferConfirmed == true)
                {
                    result.TransferDate = transfer?.ActualTransferDate;
                }
                else
                {
                    result.TransferDate = agreemet?.TransferOwnershipDate;
                }

                //สถานะเบิกโปรขาย
                if (model.SalePromotionAmount == 0)
                {
                    result.SalePromotionRequestStatus = 0;
                }
                else if (model.SalePromotionAmount > 0 && model.SalePromotionRequestAmount == 0 )
                {
                    result.SalePromotionRequestStatus = 1;
                }
                else if (model.SalePromotionAmount > 0 && model.SalePromotionRequestAmount > 0 && model.SalePromotionAmount > model.SalePromotionRequestAmount)
                {
                    result.SalePromotionRequestStatus = 2;
                }
                else if (model.SalePromotionAmount > 0 && model.SalePromotionRequestAmount > 0 && model.SalePromotionAmount <= model.SalePromotionRequestAmount)
                {
                    result.SalePromotionRequestStatus = 3;
                }

                //สถานะเบิกโปรโอน
                if (model.TransferPromotionAmount == 0)
                {
                    result.TransferPromotionRequestStatus = 0;
                }
                else if (model.TransferPromotionAmount > 0 && model.TransferPromotionRequestAmount == 0)
                {
                    result.TransferPromotionRequestStatus = 1;
                }
                else if (model.TransferPromotionAmount > 0 && model.TransferPromotionRequestAmount > 0 && model.TransferPromotionAmount > model.TransferPromotionRequestAmount)
                {
                    result.TransferPromotionRequestStatus = 2;
                }
                else if (model.TransferPromotionAmount > 0 && model.TransferPromotionRequestAmount > 0 && model.TransferPromotionAmount <= model.TransferPromotionRequestAmount)
                {
                    result.TransferPromotionRequestStatus = 3;
                }

                //สถานะส่งมอบโปรขาย
                if (model.SalePromotionRequestAmount == 0)
                {
                    result.SalePromotionDeliveryStatus = 0;
                }
                else if (model.SalePromotionRequestAmount > 0 && model.SalePromotionDeliveryAmount == 0)
                {
                    result.SalePromotionDeliveryStatus = 1;
                }
                else if (model.SalePromotionRequestAmount > 0 && model.SalePromotionDeliveryAmount > 0 && model.SalePromotionRequestAmount > model.SalePromotionDeliveryAmount)
                {
                    result.SalePromotionDeliveryStatus = 2;
                }
                else if (model.SalePromotionRequestAmount > 0 && model.SalePromotionDeliveryAmount > 0 && model.SalePromotionRequestAmount <= model.SalePromotionDeliveryAmount)
                {
                    result.SalePromotionDeliveryStatus = 3;
                }

                //สถานะส่งมอบโปรโอน
                if (model.TransferPromotionRequestAmount == 0)
                {
                    result.TransferPromotionDeliveryStatus = 0;
                }
                else if (model.TransferPromotionRequestAmount > 0 && model.TransferPromotionDeliveryAmount == 0)
                {
                    result.TransferPromotionDeliveryStatus = 1;
                }
                else if (model.TransferPromotionRequestAmount > 0 && model.TransferPromotionDeliveryAmount > 0 && model.TransferPromotionRequestAmount > model.TransferPromotionDeliveryAmount)
                {
                    result.TransferPromotionDeliveryStatus = 2;
                }
                else if (model.TransferPromotionRequestAmount > 0 && model.TransferPromotionDeliveryAmount > 0 && model.TransferPromotionRequestAmount <= model.TransferPromotionDeliveryAmount)
                {
                    result.TransferPromotionDeliveryStatus = 3;
                }

                result.Id = model.AgreementID;

                return result;
            }
            else
            {
                return null;
            }
        }
    }
}
