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
    public class CommissionHighRiseTransferVeiwDTO : BaseDTO
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
        /// โอนกรรมสิทธิ์
        /// </summary>
        [Description("โอนกรรมสิทธิ์")]
        public TransferDropdownDTO Transfer { get; set; }

        /// <summary>
        /// LC โอน
        /// </summary>
        [Description("LC โอน")]
        public USR.UserListDTO LCTransfer { get; set; }

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
        /// มูลค่าโอนจริง
        /// </summary>
        [Description("มูลค่าโอนจริง")]
        public decimal? NetSellPrice { get; set; }

        /// <summary>
        /// วันที่โอนจริง
        /// </summary>
        [Description("วันที่โอนจริง")]
        public DateTime? TransferDate { get; set; }


        /// <summary>
        /// ค่าคอมมิสชั่นโอน
        /// </summary>
        [Description("ค่าคอมมิสชั่นโอน")]
        public decimal? LCTransferPaid { get; set; }


        /// <summary>
        /// ค่าคอมมิสชั่นที่จ่ายในเดือนนี้
        /// </summary>
        [Description("ค่าคอมมิสชั่นที่จ่ายในเดือนนี้")]
        public decimal? CommissionForThisMonth { get; set; }


        public static CommissionHighRiseTransferVeiwDTO CreateFromQueryResult(CommissionHighRiseTransferVeiwQueryResult model)
        {
            if (model != null)
            {
                var result = new CommissionHighRiseTransferVeiwDTO()
                {
                    Id = model.CalculateHighRiseTransfer?.ID,
                    Unit = PRJ.UnitDropdownDTO.CreateFromModel(model.CalculateHighRiseTransfer.Transfer.Unit),
                    LCTransfer = USR.UserListDTO.CreateFromModel(model.CalculateHighRiseTransfer.LCTransfer),
                    CommissionPercentRate = model.CalculateHighRiseTransfer.CommissionPercentRate,
                    CommissionPercentType = MST.MasterCenterDropdownDTO.CreateFromModel(model.CalculateHighRiseTransfer.CommissionPercentType),
                    NetSellPrice = model.Transfer.NetSellPrice ?? 0,
                    TransferDate = model.Transfer.TransferDate,
                    LCTransferPaid = model.CalculateHighRiseTransfer.LCTransferPaid,
                    CommissionForThisMonth = model.CalculateHighRiseTransfer.LCTransferPaid ?? 0
                };

                return result;
            }
            else
            {
                return null;
            }
        }

        public static void SortBy(CommissionHighRiseTransferVeiwSortByParam sortByParam, ref IQueryable<CommissionHighRiseTransferVeiwQueryResult> query)
        {
            IOrderedQueryable<CommissionHighRiseTransferVeiwQueryResult> orderQuery;
            if (sortByParam.SortBy != null)
            {
                switch (sortByParam.SortBy.Value)
                {
                    case CommissionHighRiseTransferVeiwSortBy.UnitNo:
                        if (sortByParam.Ascending) orderQuery = query.OrderBy(o => o.CalculateHighRiseTransfer.UnitNo);
                        else orderQuery = query.OrderByDescending(o => o.CalculateHighRiseTransfer.UnitNo);
                        break;

                    case CommissionHighRiseTransferVeiwSortBy.LCTransfer:
                        if (sortByParam.Ascending) orderQuery = query.OrderBy(o => o.LCTransferName);
                        else orderQuery = query.OrderByDescending(o => o.LCTransferName);
                        break;

                    case CommissionHighRiseTransferVeiwSortBy.CommissionPercentRate:
                        if (sortByParam.Ascending) orderQuery = query.OrderBy(o => o.CalculateHighRiseTransfer.CommissionPercentRate);
                        else orderQuery = query.OrderByDescending(o => o.CalculateHighRiseTransfer.CommissionPercentRate);
                        break;

                    case CommissionHighRiseTransferVeiwSortBy.CommissionPercentType:
                        if (sortByParam.Ascending) orderQuery = query.OrderBy(o => o.CalculateHighRiseTransfer.CommissionPercentType.Name);
                        else orderQuery = query.OrderByDescending(o => o.CalculateHighRiseTransfer.CommissionPercentType.Name);
                        break;

                    case CommissionHighRiseTransferVeiwSortBy.NetSellPrice:
                        if (sortByParam.Ascending) orderQuery = query.OrderBy(o => o.Transfer.NetSellPrice);
                        else orderQuery = query.OrderByDescending(o => o.Transfer.NetSellPrice);
                        break;

                    case CommissionHighRiseTransferVeiwSortBy.TransferDate:
                        if (sortByParam.Ascending) orderQuery = query.OrderBy(o => o.Transfer.TransferDate);
                        else orderQuery = query.OrderByDescending(o => o.Transfer.TransferDate);
                        break;

                    case CommissionHighRiseTransferVeiwSortBy.LCTransferPaid:
                        if (sortByParam.Ascending) orderQuery = query.OrderBy(o => o.CalculateHighRiseTransfer.LCTransferPaid);
                        else orderQuery = query.OrderByDescending(o => o.CalculateHighRiseTransfer.LCTransferPaid);
                        break;

                    case CommissionHighRiseTransferVeiwSortBy.CommissionForThisMonth:
                        if (sortByParam.Ascending) orderQuery = query.OrderBy(o => o.CalculateHighRiseTransfer.TotalCommissionPaid);
                        else orderQuery = query.OrderByDescending(o => o.CalculateHighRiseTransfer.TotalCommissionPaid);
                        break;

                    default:
                        orderQuery = query.OrderByDescending(o => o.CalculateHighRiseTransfer.UnitNo);
                        break;
                }
            }
            else
            {
                orderQuery = query.OrderByDescending(o => o.CalculateHighRiseTransfer.UnitNo);
            }

            orderQuery.ThenBy(o => o.CalculateHighRiseTransfer.ID);
            query = orderQuery;
        }

        public static CommissionHighRiseTransferVeiwDTO CreateFromQuery(dbqCommissionHighRiseTransferList model, DatabaseContext db)
        {
            if (model != null)
            {
                var modelUnit = db.Units.Where(e => e.ID == model.UnitID).FirstOrDefault();
                var modelLCTransfer = db.Users.IgnoreQueryFilters().Where(e => e.ID == model.LCTransferID).FirstOrDefault();
                var modelCommissionPercentType = db.MasterCenters.Where(e => e.ID == model.CommissionPercentTypeMasterCenterID).FirstOrDefault();
                var modelAgreement = db.Agreements.Where(e => e.ID == model.AgreementID).FirstOrDefault();
                var modelTransfer = db.Transfers.Where(e => e.ID == model.TransferID).FirstOrDefault();

                var result = new CommissionHighRiseTransferVeiwDTO()
                {
                    Id = model.UnitID,
                    Unit = PRJ.UnitDropdownDTO.CreateFromModel(modelUnit),
                    Agreement = AgreementDropdownDTO.CreateFromModel(modelAgreement),
                    Transfer = TransferDropdownDTO.CreateFromModel(modelTransfer),
                    LCTransfer = USR.UserListDTO.CreateFromModel(modelLCTransfer),
                    CommissionPercentRate = model.CommissionPercentRate ?? 0,
                    CommissionPercentType = MST.MasterCenterDropdownDTO.CreateFromModel(modelCommissionPercentType),
                    NetSellPrice = model.NetSellPrice,
                    TransferDate = model.TransferDate,
                    LCTransferPaid = model.LCTransferPaid ?? 0,
                    CommissionForThisMonth = model.CommissionForThisMonth ?? 0
                };

                return result;
            }
            else
            {
                return null;
            }
        }
    }


    public class CommissionHighRiseTransferVeiwQueryResult
    {
        public CalculateHighRiseTransfer CalculateHighRiseTransfer { get; set; }
        public CommissionContract Contract { get; set; }
        public CommissionTransfer Transfer { get; set; }
        public models.PRJ.Project Project { get; set; }
        public models.PRJ.Unit Unit { get; set; }
        public User LCTransferName { get; set; }
    }
}
