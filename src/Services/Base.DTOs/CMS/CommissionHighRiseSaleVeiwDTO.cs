using Database.Models;
using Database.Models.CMS;
using Database.Models.USR;
using Database.Models.SAL;
using ErrorHandling;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using models = Database.Models;
using Base.DTOs.SAL;
using Database.Models.DbQueries.CMS;

namespace Base.DTOs.CMS
{
    public class CommissionHighRiseSaleVeiwDTO : BaseDTO
    {
        /// <summary>
        /// แปลง
        /// </summary>
        [Description("แปลง")]
        public PRJ.UnitDropdownDTO Unit { get; set; }

        /// <summary>
        /// สัญญา
        /// </summary>
        [Description("สัญญา")]
        public AgreementDropdownDTO Agreement { get; set; }

        /// <summary>
        /// LC ปิดการขาย
        /// </summary>
        [Description("LC ปิดการขาย")]
        public USR.UserListDTO SaleUser { get; set; }

        /// <summary>
        /// LC ประจำโครงการ
        /// </summary>
        [Description("LC ประจำโครงการ")]
        public USR.UserListDTO ProjectSaleUser { get; set; }

        /// <summary>
        /// อัตรา % Commission
        /// </summary>
        [Description("อัตรา % Commission")]
        public decimal? CommissionPercentRate { get; set; }

        /// <summary>
        /// ประเภท Rate Commission
        /// </summary>
        [Description("ประเภท Rate Commission")]
        public MST.MasterCenterDropdownDTO CommissionPercentType { get; set; }

        /// <summary>
        /// มูลค่าสัญญา
        /// </summary>
        [Description("มูลค่าสัญญา")]
        public decimal? TotalContractNetAmount { get; set; }

        /// <summary>
        /// วันที่อนุมัติสัญญา
        /// </summary>
        [Description("วันที่อนุมัติสัญญา")]
        public DateTime? SignAgreementDate { get; set; }


        /// <summary>
        /// ค่าคอมมิสชั่น LC ปิดการขาย (30%)
        /// </summary>
        [Description("ค่าคอมมิสชั่น LC ปิดการขาย (30%)")]
        public decimal? SaleUserSalePaid { get; set; }
        /// <summary>
        /// ค่าคอมมิสชั่น LC ประจำโครงการ (70%)
        /// </summary>
        [Description("ค่าคอมมิสชั่น LC ประจำโครงการ (70%)")]
        public decimal? ProjectSaleSalePaid { get; set; }
        /// <summary>
        /// รวมค่าคอมมิสชั่น (100%)
        /// </summary>
        [Description("รวมค่าคอมมิสชั่น (100%)")]
        public decimal? TotalSalePaid { get; set; }


        /// <summary>
        /// ค่าคอมมิสชั่นที่จ่ายในเดือนนี้
        /// </summary>
        [Description("ค่าคอมมิสชั่นที่จ่ายในเดือนนี้")]
        public decimal? CommissionForThisMonth { get; set; }

        /// <summary>
        /// วันที่ใบเสร็จรับเงินค่าทำสัญญา
        /// </summary>
        [Description("วันที่ใบเสร็จรับเงินค่าทำสัญญา")]
        public DateTime? ReceiptDate { get; set; }

        public static CommissionHighRiseSaleVeiwDTO CreateFromQueryResult(CommissionHighRiseSaleVeiwQueryResult model)
        {
            if (model != null)
            {
                var result = new CommissionHighRiseSaleVeiwDTO()
                {
                    Id = model.CalculateHighRiseSale?.ID,
                    Unit = PRJ.UnitDropdownDTO.CreateFromModel(model.CalculateHighRiseSale.Agreement.Unit),
                    SaleUser = USR.UserListDTO.CreateFromModel(model.CalculateHighRiseSale.SaleUser),
                    ProjectSaleUser = USR.UserListDTO.CreateFromModel(model.CalculateHighRiseSale.ProjectSaleUser),
                    CommissionPercentRate = model.CalculateHighRiseSale.CommissionPercentRate,
                    CommissionPercentType = MST.MasterCenterDropdownDTO.CreateFromModel(model.CalculateHighRiseSale.CommissionPercentType),
                    TotalContractNetAmount = model.Contract.SellingPrice - model.Contract.TransferDiscount ?? 0 - model.Contract.FreeDownAmount ?? 0,
                    SignAgreementDate = model.Contract.ApproveDate,
                    SaleUserSalePaid = model.CalculateHighRiseSale.SaleUserSalePaid,
                    ProjectSaleSalePaid = model.CalculateHighRiseSale.ProjectSaleSalePaid,
                    TotalSalePaid = model.CalculateHighRiseSale.SaleUserSalePaid ?? 0 + model.CalculateHighRiseSale.ProjectSaleSalePaid ?? 0,
                    CommissionForThisMonth = model.CalculateHighRiseSale.SaleUserSalePaid ?? 0
                                                + model.CalculateHighRiseSale.ProjectSaleSalePaid ?? 0
                };

                return result;
            }
            else
            {
                return null;
            }
        }

        public static void SortBy(CommissionHighRiseSaleVeiwSortByParam sortByParam, ref IQueryable<CommissionHighRiseSaleVeiwQueryResult> query)
        {
            IOrderedQueryable<CommissionHighRiseSaleVeiwQueryResult> orderQuery;
            if (sortByParam.SortBy != null)
            {
                switch (sortByParam.SortBy.Value)
                {
                    case CommissionHighRiseSaleVeiwSortBy.UnitNo:
                        if (sortByParam.Ascending) orderQuery = query.OrderBy(o => o.CalculateHighRiseSale.UnitNo);
                        else orderQuery = query.OrderByDescending(o => o.CalculateHighRiseSale.UnitNo);
                        break;

                    case CommissionHighRiseSaleVeiwSortBy.SaleUser:
                        if (sortByParam.Ascending) orderQuery = query.OrderBy(o => o.SaleUserName);
                        else orderQuery = query.OrderByDescending(o => o.SaleUserName);
                        break;

                    case CommissionHighRiseSaleVeiwSortBy.ProjectSaleUser:
                        if (sortByParam.Ascending) orderQuery = query.OrderBy(o => o.ProjectSaleUserName);
                        else orderQuery = query.OrderByDescending(o => o.ProjectSaleUserName);
                        break;

                    case CommissionHighRiseSaleVeiwSortBy.CommissionPercentRate:
                        if (sortByParam.Ascending) orderQuery = query.OrderBy(o => o.CalculateHighRiseSale.CommissionPercentRate);
                        else orderQuery = query.OrderByDescending(o => o.CalculateHighRiseSale.CommissionPercentRate);
                        break;

                    case CommissionHighRiseSaleVeiwSortBy.CommissionPercentType:
                        if (sortByParam.Ascending) orderQuery = query.OrderBy(o => o.CalculateHighRiseSale.CommissionPercentType.Name);
                        else orderQuery = query.OrderByDescending(o => o.CalculateHighRiseSale.CommissionPercentType.Name);
                        break;

                    case CommissionHighRiseSaleVeiwSortBy.TotalContractNetAmount:
                        if (sortByParam.Ascending) orderQuery = query.OrderBy(o => (o.Contract.SellingPrice - (o.Contract.TransferDiscount ?? 0) - (o.Contract.FreeDownAmount ?? 0)));
                        else orderQuery = query.OrderByDescending(o => (o.Contract.SellingPrice - (o.Contract.TransferDiscount ?? 0) - (o.Contract.FreeDownAmount ?? 0)));
                        break;

                    case CommissionHighRiseSaleVeiwSortBy.SignAgreementDate:
                        if (sortByParam.Ascending) orderQuery = query.OrderBy(o => o.Contract.ApproveDate);
                        else orderQuery = query.OrderByDescending(o => o.Contract.ApproveDate);
                        break;

                    case CommissionHighRiseSaleVeiwSortBy.SaleUserSalePaid:
                        if (sortByParam.Ascending) orderQuery = query.OrderBy(o => o.CalculateHighRiseSale.SaleUserSalePaid);
                        else orderQuery = query.OrderByDescending(o => o.CalculateHighRiseSale.SaleUserSalePaid);
                        break;

                    case CommissionHighRiseSaleVeiwSortBy.ProjectSaleSalePaid:
                        if (sortByParam.Ascending) orderQuery = query.OrderBy(o => o.CalculateHighRiseSale.ProjectSaleSalePaid);
                        else orderQuery = query.OrderByDescending(o => o.CalculateHighRiseSale.ProjectSaleSalePaid);
                        break;

                    case CommissionHighRiseSaleVeiwSortBy.TotalSalePaid:
                        if (sortByParam.Ascending) orderQuery = query.OrderBy(o => (o.CalculateHighRiseSale.SaleUserSalePaid + o.CalculateHighRiseSale.ProjectSaleSalePaid));
                        else orderQuery = query.OrderByDescending(o => (o.CalculateHighRiseSale.SaleUserSalePaid + o.CalculateHighRiseSale.ProjectSaleSalePaid));
                        break;

                    case CommissionHighRiseSaleVeiwSortBy.CommissionForThisMonth:
                        if (sortByParam.Ascending) orderQuery = query.OrderBy(o => o.CalculateHighRiseSale.TotalCommissionPaid);
                        else orderQuery = query.OrderByDescending(o => o.CalculateHighRiseSale.TotalCommissionPaid);
                        break;

                    default:
                        orderQuery = query.OrderByDescending(o => o.CalculateHighRiseSale.UnitNo);
                        break;
                }
            }
            else
            {
                orderQuery = query.OrderByDescending(o => o.CalculateHighRiseSale.UnitNo);
            }

            orderQuery.ThenBy(o => o.CalculateHighRiseSale.ID);
            query = orderQuery;
        }

        public static CommissionHighRiseSaleVeiwDTO CreateFromQuery(dbqCommissionHighRiseSaleList model, DatabaseContext db)
        {
            if (model != null)
            {
                var modelUnit = db.Units.Where(e => e.ID == model.UnitID).FirstOrDefault();
                var modelSaleUser = db.Users.IgnoreQueryFilters().Where(e => e.ID == model.SaleUserID).FirstOrDefault();
                var modelProjectSaleUser = db.Users.IgnoreQueryFilters().Where(e => e.ID == model.ProjectSaleUserID).FirstOrDefault();
                var modelCommissionPercentType = db.MasterCenters.Where(e => e.ID == model.CommissionPercentTypeMasterCenterID).FirstOrDefault();
                var modelAgreement = db.Agreements.Where(e => e.ID == model.AgreementID).FirstOrDefault();
                var modelAgent = db.Agents.Where(e => e.ID == model.AgentID).FirstOrDefault();

                var result = new CommissionHighRiseSaleVeiwDTO()
                {
                    Id = model.UnitID,
                    Unit = PRJ.UnitDropdownDTO.CreateFromModel(modelUnit),
                    Agreement = AgreementDropdownDTO.CreateFromModel(modelAgreement),
                    //SaleUser = USR.UserListDTO.CreateFromModel(modelSaleUser),
                    ProjectSaleUser = USR.UserListDTO.CreateFromModel(modelProjectSaleUser),
                    CommissionPercentRate = model.CommissionPercentRate ?? 0,
                    CommissionPercentType = MST.MasterCenterDropdownDTO.CreateFromModel(modelCommissionPercentType),
                    TotalContractNetAmount = model.TotalContractNetAmount,
                    SignAgreementDate = model.SignAgreementDate,
                    SaleUserSalePaid = model.SaleUserSalePaid ?? 0,
                    ProjectSaleSalePaid = model.ProjectSaleSalePaid ?? 0,
                    TotalSalePaid = model.TotalSalePaid ?? 0,
                    CommissionForThisMonth = model.CommissionForThisMonth ?? 0,
                    ReceiptDate = model.ReceiptDate
                };

                if (modelAgent != null)
                {
                    result.SaleUser = new USR.UserListDTO()
                    {
                        Id = modelAgent.ID,
                        //EmployeeNo = modelAgent.EmployeeNo,
                        FirstName = modelAgent.NameTH,
                        //LastName = modelAgent.LastName,
                        DisplayName = modelAgent.NameTH,
                        //PhoneNo = modelAgent.PhoneNo
                    };
                }
                else
                {
                    result.SaleUser = USR.UserListDTO.CreateFromModel(modelSaleUser);
                }

                return result;
            }
            else
            {
                return null;
            }
        }
    }

    public class CommissionHighRiseSaleVeiwQueryResult
    {
        public CalculateHighRiseSale CalculateHighRiseSale { get; set; }
        //public CalculateHighRiseTransfer CalculateHighRiseTransfer { get; set; }
        public CommissionContract Contract { get; set; }
        //public CommissionTransfer Transfer { get; set; }
        public models.PRJ.Project Project { get; set; }
        public models.PRJ.Unit Unit { get; set; }
        public User SaleUserName { get; set; }
        public User ProjectSaleUserName { get; set; }
    }
}
