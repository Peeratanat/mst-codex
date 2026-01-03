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
using Database.Models.DbQueries.CMS;
using Base.DTOs.SAL;

namespace Base.DTOs.CMS
{
    public class CommissionLowRiseVeiwDTO : BaseDTO
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
        /// วันที่โอนจริง
        /// </summary>
        [Description("วันที่โอนจริง")]
        public DateTime? ActualTransferDate { get; set; }


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
        /// ค่าคอมมิสชั่น New Launch LC ปิดการขาย (30%)
        /// </summary>
        [Description("ค่าคอมมิสชั่น New Launch LC ปิดการขาย (30%)")]
        public decimal? SaleUserNewLaunchPaid { get; set; }
        /// <summary>
        /// ค่าคอมมิสชั่น New Launch LC ประจำโครงการ (70%)
        /// </summary>
        [Description("ค่าคอมมิสชั่น New Launch LC ประจำโครงการ (70%)")]
        public decimal? ProjectSaleNewLaunchPaid { get; set; }
        /// <summary>
        /// รวมค่าคอมมิสชั่น New Launch (100%)
        /// </summary>
        [Description("รวมค่าคอมมิสชั่น New Launch (100%)")]
        public decimal? TotalNewLaunchPaid { get; set; }


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


        //public static CommissionLowRiseVeiwDTO CreateFromModel(CalculateLowRiseSale model)
        //{
        //    if (model != null)
        //    {
        //        var result = new CommissionLowRiseVeiwDTO()
        //        {
        //            Id = model.ID,
        //            Unit = PRJ.UnitDropdownDTO.CreateFromModel(model.Agreement.Unit),
        //            SaleUser = USR.UserListDTO.CreateFromModel(model.SaleUser),
        //            ProjectSaleUser = USR.UserListDTO.CreateFromModel(model.ProjectSaleUser),
        //            CommissionPercentRate = model.CommissionPercentRate,
        //            CommissionPercentType = model.CommissionPercentType,
        //            //TotalContractNetAmount = model.Agreement,
        //            //ActualTransferDate = model.Transfer.ActualTransferDate,
        //            SaleUserSalePaid = model.SaleUserSalePaid,
        //            ProjectSaleSalePaid = model.ProjectSaleSalePaid,
        //            TotalSalePaid = model.SaleUserSalePaid ?? 0 + model.ProjectSaleSalePaid ?? 0,
        //            SaleUserNewLaunchPaid = model.SaleUserNewLaunchPaid,
        //            ProjectSaleNewLaunchPaid = model.ProjectSaleNewLaunchPaid,
        //            TotalNewLaunchPaid = model.SaleUserNewLaunchPaid ?? 0 + model.ProjectSaleNewLaunchPaid ?? 0,
        //            CommissionForThisMonth = model.CommissionForThisMonth,

        //        };
        //        return result;
        //    }
        //    else
        //    {
        //        return null;
        //    }
        //}

        public static CommissionLowRiseVeiwDTO CreateFromQueryResult(CommissionLowRiseVeiwQueryResult model)
        {
            if (model != null)
            {
                var result = new CommissionLowRiseVeiwDTO()
                {
                    Id = model.CalculateLowRiseSale?.ID ?? model.CalculateLowRiseTransfer?.ID,
                    Unit = PRJ.UnitDropdownDTO.CreateFromModel(model.CalculateLowRiseSale.Agreement.Unit ?? model.CalculateLowRiseTransfer.Transfer.Unit),
                    SaleUser = USR.UserListDTO.CreateFromModel(model.CalculateLowRiseSale.SaleUser ?? model.CalculateLowRiseTransfer.SaleUser),
                    ProjectSaleUser = USR.UserListDTO.CreateFromModel(model.CalculateLowRiseSale.ProjectSaleUser ?? model.CalculateLowRiseTransfer.SaleUser),
                    CommissionPercentRate = model.CalculateLowRiseSale.CommissionPercentRate ?? model.CalculateLowRiseTransfer.CommissionPercentRate,
                    CommissionPercentType = MST.MasterCenterDropdownDTO.CreateFromModel(model.CalculateLowRiseSale.CommissionPercentType ?? model.CalculateLowRiseTransfer.CommissionPercentType),
                    TotalContractNetAmount = model.Contract.SellingPrice - (model.Contract.TransferDiscount ?? 0) - (model.Contract.FreeDownAmount ?? 0),
                    ActualTransferDate = model.Transfer.TransferDate,
                    SaleUserSalePaid = model.CalculateLowRiseSale.SaleUserSalePaid ?? model.CalculateLowRiseTransfer.SaleUserSalePaid,
                    ProjectSaleSalePaid = model.CalculateLowRiseSale.ProjectSaleSalePaid ?? model.CalculateLowRiseTransfer.ProjectSaleSalePaid,
                    TotalSalePaid = (
                                    model.CalculateLowRiseSale != null
                                        ? (model.CalculateLowRiseSale.SaleUserSalePaid ?? 0) + (model.CalculateLowRiseSale.ProjectSaleSalePaid ?? 0)
                                        : (model.CalculateLowRiseTransfer.SaleUserSalePaid ?? 0) + (model.CalculateLowRiseTransfer.ProjectSaleSalePaid ?? 0)
                                ),
                    SaleUserNewLaunchPaid = model.CalculateLowRiseSale.SaleUserNewLaunchPaid ?? 0,
                    ProjectSaleNewLaunchPaid = model.CalculateLowRiseSale.ProjectSaleNewLaunchPaid ?? 0,
                    TotalNewLaunchPaid = (model.CalculateLowRiseSale.SaleUserNewLaunchPaid ?? 0) + (model.CalculateLowRiseSale.ProjectSaleNewLaunchPaid ?? 0),
                    CommissionForThisMonth = (model.CalculateLowRiseSale.SaleUserSalePaid ?? model.CalculateLowRiseTransfer.SaleUserSalePaid ?? 0)
                                                + (model.CalculateLowRiseSale.ProjectSaleSalePaid ?? model.CalculateLowRiseTransfer.ProjectSaleSalePaid ?? 0)
                                                + (model.CalculateLowRiseSale.SaleUserNewLaunchPaid ?? 0)
                                                + (model.CalculateLowRiseSale.ProjectSaleNewLaunchPaid ?? 0)
                };

                return result;
            }
            else
            {
                return null;
            }
        }

        public static void SortBy(CommissionLowRiseVeiwSortByParam sortByParam, ref IQueryable<CommissionLowRiseVeiwQueryResult> query)
        {
            IOrderedQueryable<CommissionLowRiseVeiwQueryResult> orderQuery;
            if (sortByParam.SortBy != null)
            {
                switch (sortByParam.SortBy.Value)
                {
                    case CommissionLowRiseVeiwSortBy.UnitNo:
                        if (sortByParam.Ascending) orderQuery = query.OrderBy(o => o.CalculateLowRiseSale.UnitNo ?? o.CalculateLowRiseTransfer.UnitNo);
                        else orderQuery = query.OrderByDescending(o => o.CalculateLowRiseSale.UnitNo ?? o.CalculateLowRiseTransfer.UnitNo);
                        break;

                    case CommissionLowRiseVeiwSortBy.SaleUser:
                        if (sortByParam.Ascending) orderQuery = query.OrderBy(o => o.SaleUserName);
                        else orderQuery = query.OrderByDescending(o => o.SaleUserName);
                        break;

                    case CommissionLowRiseVeiwSortBy.ProjectSaleUser:
                        if (sortByParam.Ascending) orderQuery = query.OrderBy(o => o.ProjectSaleUserName);
                        else orderQuery = query.OrderByDescending(o => o.ProjectSaleUserName);
                        break;

                    case CommissionLowRiseVeiwSortBy.CommissionPercentRate:
                        if (sortByParam.Ascending) orderQuery = query.OrderBy(o => o.CalculateLowRiseSale.CommissionPercentRate);
                        else orderQuery = query.OrderByDescending(o => o.CalculateLowRiseSale.CommissionPercentRate);
                        break;

                    case CommissionLowRiseVeiwSortBy.CommissionPercentType:
                        if (sortByParam.Ascending) orderQuery = query.OrderBy(o => o.CalculateLowRiseSale.CommissionPercentType.Name);
                        else orderQuery = query.OrderByDescending(o => o.CalculateLowRiseSale.CommissionPercentType.Name);
                        break;

                    case CommissionLowRiseVeiwSortBy.TotalContractNetAmount:
                        if (sortByParam.Ascending) orderQuery = query.OrderBy(o => (o.Contract.SellingPrice - (o.Contract.TransferDiscount ?? 0) - (o.Contract.FreeDownAmount ?? 0)));
                        else orderQuery = query.OrderByDescending(o => (o.Contract.SellingPrice - (o.Contract.TransferDiscount ?? 0) - (o.Contract.FreeDownAmount ?? 0)));
                        break;

                    case CommissionLowRiseVeiwSortBy.ActualTransferDate:
                        if (sortByParam.Ascending) orderQuery = query.OrderBy(o => o.Transfer.TransferDate);
                        else orderQuery = query.OrderByDescending(o => o.Transfer.TransferDate);
                        break;

                    case CommissionLowRiseVeiwSortBy.SaleUserSalePaid:
                        if (sortByParam.Ascending) orderQuery = query.OrderBy(o => o.CalculateLowRiseSale.SaleUserSalePaid);
                        else orderQuery = query.OrderByDescending(o => o.CalculateLowRiseSale.SaleUserSalePaid);
                        break;

                    case CommissionLowRiseVeiwSortBy.ProjectSaleSalePaid:
                        if (sortByParam.Ascending) orderQuery = query.OrderBy(o => o.CalculateLowRiseSale.ProjectSaleSalePaid);
                        else orderQuery = query.OrderByDescending(o => o.CalculateLowRiseSale.ProjectSaleSalePaid);
                        break;

                    case CommissionLowRiseVeiwSortBy.TotalSalePaid:
                        if (sortByParam.Ascending) orderQuery = query.OrderBy(o => (o.CalculateLowRiseSale.SaleUserSalePaid + o.CalculateLowRiseSale.ProjectSaleSalePaid));
                        else orderQuery = query.OrderByDescending(o => (o.CalculateLowRiseSale.SaleUserSalePaid + o.CalculateLowRiseSale.ProjectSaleSalePaid));
                        break;

                    case CommissionLowRiseVeiwSortBy.SaleUserNewLaunchPaid:
                        if (sortByParam.Ascending) orderQuery = query.OrderBy(o => o.CalculateLowRiseSale.SaleUserNewLaunchPaid);
                        else orderQuery = query.OrderByDescending(o => o.CalculateLowRiseSale.SaleUserNewLaunchPaid);
                        break;

                    case CommissionLowRiseVeiwSortBy.ProjectSaleNewLaunchPaid:
                        if (sortByParam.Ascending) orderQuery = query.OrderBy(o => o.CalculateLowRiseSale.ProjectSaleNewLaunchPaid);
                        else orderQuery = query.OrderByDescending(o => o.CalculateLowRiseSale.ProjectSaleNewLaunchPaid);
                        break;

                    case CommissionLowRiseVeiwSortBy.TotalNewLaunchPaid:
                        if (sortByParam.Ascending) orderQuery = query.OrderBy(o => (o.CalculateLowRiseSale.SaleUserNewLaunchPaid + o.CalculateLowRiseSale.ProjectSaleNewLaunchPaid));
                        else orderQuery = query.OrderByDescending(o => (o.CalculateLowRiseSale.SaleUserNewLaunchPaid + o.CalculateLowRiseSale.ProjectSaleNewLaunchPaid));
                        break;

                    case CommissionLowRiseVeiwSortBy.CommissionForThisMonth:
                        if (sortByParam.Ascending) orderQuery = query.OrderBy(o => o.CalculateLowRiseSale.TotalCommissionPaid);
                        else orderQuery = query.OrderByDescending(o => o.CalculateLowRiseSale.TotalCommissionPaid);
                        break;

                    default:
                        orderQuery = query.OrderByDescending(o => o.CalculateLowRiseSale.UnitNo ?? o.CalculateLowRiseTransfer.UnitNo);
                        break;
                }
            }
            else
            {
                orderQuery = query.OrderByDescending(o => o.CalculateLowRiseSale.UnitNo ?? o.CalculateLowRiseTransfer.UnitNo);
            }

            orderQuery.ThenBy(o => o.CalculateLowRiseSale.ID);
            query = orderQuery;
        }

        public static CommissionLowRiseVeiwDTO CreateFromQuery(dbqCommissionLowRiseList model, DatabaseContext db)
        {
            if (model != null)
            {
                var modelUnit = db.Units.Where(e => e.ID == model.UnitID).FirstOrDefault();
                var modelSaleUser = db.Users.IgnoreQueryFilters().Where(e => e.ID == model.SaleUserID).FirstOrDefault();
                var modelProjectSaleUser = db.Users.IgnoreQueryFilters().Where(e => e.ID == model.ProjectSaleUserID).FirstOrDefault();
                var modelCommissionPercentType = db.MasterCenters.Where(e => e.ID == model.CommissionPercentTypeMasterCenterID).FirstOrDefault();
                var modelAgreement = db.Agreements.Where(e => e.ID == model.AgreementID).FirstOrDefault();
                var modelAgent = db.Agents.Where(e => e.ID == model.AgentID).FirstOrDefault();

                var result = new CommissionLowRiseVeiwDTO()
                {
                    Id = model.UnitID,
                    Unit = PRJ.UnitDropdownDTO.CreateFromModel(modelUnit),
                    Agreement = AgreementDropdownDTO.CreateFromModel(modelAgreement),
                    //SaleUser = USR.UserListDTO.CreateFromModel(modelSaleUser),
                    ProjectSaleUser = USR.UserListDTO.CreateFromModel(modelProjectSaleUser),
                    CommissionPercentRate = model.CommissionPercentRate ?? 0,
                    CommissionPercentType = MST.MasterCenterDropdownDTO.CreateFromModel(modelCommissionPercentType),
                    TotalContractNetAmount = model.TotalContractNetAmount,
                    ActualTransferDate = model.ActualTransferDate,
                    SaleUserSalePaid = model.SaleUserSalePaid ?? 0,
                    ProjectSaleSalePaid = model.ProjectSaleSalePaid ?? 0,
                    TotalSalePaid = model.TotalSalePaid ?? 0,
                    SaleUserNewLaunchPaid = model.SaleUserNewLaunchPaid ?? 0,
                    ProjectSaleNewLaunchPaid = model.ProjectSaleNewLaunchPaid ?? 0,
                    TotalNewLaunchPaid = model.SaleUserNewLaunchPaid ?? 0,
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

    public class CommissionLowRiseVeiwQueryResult
    {
        public CalculateLowRiseSale CalculateLowRiseSale { get; set; }
        public CalculateLowRiseTransfer CalculateLowRiseTransfer { get; set; }
        public CommissionContract Contract { get; set; }
        public CommissionTransfer Transfer { get; set; }
        public models.PRJ.Project Project { get; set; }
        public models.PRJ.Unit Unit { get; set; }
        public User SaleUserName { get; set; }
        public User ProjectSaleUserName { get; set; }
    }
}
