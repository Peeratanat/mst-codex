using Database.Models;
using Database.Models.CMS;
using Database.Models.DbQueries.CMS;
using Database.Models.USR;
using ErrorHandling;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using models = Database.Models;

namespace Base.DTOs.CMS
{
    public class CommissionSettingDTO : BaseDTO
    {
        /// <summary>
        /// โครงการ
        /// </summary>
        [Description("โครงการ")]
        public PRJ.ProjectDropdownDTO Project { get; set; }

        /// <summary>
        /// New Launch
        /// </summary>
        [Description("New Launch")]
        public string NewLaunch { get; set; }

        /// <summary>
        /// Fix ตามแบบบ้าน ขาย
        /// </summary>
        [Description("Fix ตามแบบบ้าน ขาย")]
        public string RateSettingFixSaleModel { get; set; }

        /// <summary>
        /// Fix ตามแบบบ้าน โอน
        /// </summary>
        [Description("Fix ตามแบบบ้าน โอน")]
        public string RateSettingFixTransferModel { get; set; }

        /// <summary>
        /// Fix % Ranking ขาย
        /// </summary>
        [Description("Fix % Ranking ขาย")]
        public string RateSettingFixSale { get; set; }

        /// <summary>
        /// Fix % Ranking โอน
        /// </summary>
        [Description("Fix % Ranking โอน")]
        public string RateSettingFixTransfer { get; set; }

        /// <summary>
        /// % Ranking ขาย
        /// </summary>
        [Description("% Ranking ขาย")]
        public string RateSettingSale { get; set; }

        /// <summary>
        /// % Ranking โอน
        /// </summary>
        [Description("% Ranking โอน")]
        public string RateSettingTransfer { get; set; }

        /// <summary>
        /// Agent Rate
        /// </summary>
        [Description("Agent Rate")]
        public string RateSettingAgent { get; set; }


        public static CommissionSettingDTO CreateFromQuery(dbqCommissionSettingList model, DatabaseContext db)
        {
            if (model != null)
            {
                var modelProject = db.Projects.Where(e => e.ID == model.ProjectID).FirstOrDefault();

                CommissionSettingDTO result = new CommissionSettingDTO();

                result.Id = model.ProjectID;
                result.Project = PRJ.ProjectDropdownDTO.CreateFromModel(modelProject);
                result.NewLaunch = StringFormatDate(model.MinGeneralSetting, model.MaxGeneralSetting);
                result.RateSettingFixSaleModel = StringFormatDate(model.MinRateSettingFixSaleModel, model.MaxRateSettingFixSaleModel);
                result.RateSettingFixTransferModel = StringFormatDate(model.MinRateSettingFixTransferModel, model.MaxRateSettingFixTransferModel);
                result.RateSettingFixSale = StringFormatDate(model.MinRateSettingFixSale, model.MaxRateSettingFixSale);
                result.RateSettingFixTransfer = StringFormatDate(model.MinRateSettingFixTransfer, model.MaxRateSettingFixTransfer);
                result.RateSettingSale = StringFormatDate(model.MinRateSettingSale, model.MaxRateSettingSale);
                result.RateSettingTransfer = StringFormatDate(model.MinRateSettingTransfer, model.MaxRateSettingTransfer);
                result.RateSettingAgent = StringFormatDate(model.MinRateSettingAgent, model.MaxRateSettingAgent);

                return result;
            }
            else
            {
                return null;
            }
        }

        public static CommissionSettingDTO CreateFromQueryResult(CommissionSettingQueryResult model)
        {
            if (model != null)
            {
                var result = new CommissionSettingDTO()
                {
                    Id = model.Project.ID,
                    Project = PRJ.ProjectDropdownDTO.CreateFromModel(model.Project),
                    NewLaunch = StringFormatDate(model.GeneralSettingMin.ActiveDate, model.GeneralSettingMax.ActiveDate),
                    RateSettingFixSaleModel = StringFormatDate(model.RateSettingFixSaleModelMin.ActiveDate, model.RateSettingFixSaleModelMax.ActiveDate),
                    RateSettingFixTransferModel = StringFormatDate(model.RateSettingFixTransferModelMin.ActiveDate, model.RateSettingFixTransferModelMax.ActiveDate),
                    RateSettingFixSale = StringFormatDate(model.RateSettingFixSaleMin.ActiveDate, model.RateSettingFixSaleMax.ActiveDate),
                    RateSettingFixTransfer = StringFormatDate(model.RateSettingFixTransferMin.ActiveDate, model.RateSettingFixTransferMax.ActiveDate),
                    RateSettingSale = StringFormatDate(model.RateSettingSaleMin.ActiveDate, model.RateSettingSaleMax.ActiveDate),
                    RateSettingTransfer = StringFormatDate(model.RateSettingTransferMin.ActiveDate, model.RateSettingTransferMax.ActiveDate),
                    RateSettingAgent = StringFormatDate(model.RateSettingAgentMin.ActiveDate, model.RateSettingAgentMax.ActiveDate)
                };

                return result;
            }
            else
            {
                return null;
            }
        }

        public static void SortBy(CommissionSettingSortByParam sortByParam, ref IQueryable<CommissionSettingQueryResult> query)
        {
            IOrderedQueryable<CommissionSettingQueryResult> orderQuery;
            if (sortByParam.SortBy != null)
            {
                switch (sortByParam.SortBy.Value)
                {
                    case CommissionSettingSortBy.ProjectNo:
                        if (sortByParam.Ascending) orderQuery = query.OrderBy(o => o.Project.ProjectNo);
                        else orderQuery = query.OrderByDescending(o => o.Project.ProjectNo);
                        break;
                    case CommissionSettingSortBy.ProjectName:
                        if (sortByParam.Ascending) orderQuery = query.OrderBy(o => o.Project.ProjectNameTH);
                        else orderQuery = query.OrderByDescending(o => o.Project.ProjectNameTH);
                        break;
                    case CommissionSettingSortBy.NewLaunch:
                        if (sortByParam.Ascending) orderQuery = query.OrderBy(o => o.GeneralSettingMin.ActiveDate);
                        else orderQuery = query.OrderByDescending(o => o.GeneralSettingMin.ActiveDate);
                        break;
                    case CommissionSettingSortBy.RateSettingFixSaleModel:
                        if (sortByParam.Ascending) orderQuery = query.OrderBy(o => o.RateSettingFixSaleModelMin.ActiveDate);
                        else orderQuery = query.OrderByDescending(o => o.RateSettingFixSaleModelMin.ActiveDate);
                        break;
                    case CommissionSettingSortBy.RateSettingFixTransferModel:
                        if (sortByParam.Ascending) orderQuery = query.OrderBy(o => o.RateSettingFixTransferModelMin.ActiveDate);
                        else orderQuery = query.OrderByDescending(o => o.RateSettingFixTransferModelMin.ActiveDate);
                        break;
                    case CommissionSettingSortBy.RateSettingFixSale:
                        if (sortByParam.Ascending) orderQuery = query.OrderBy(o => o.RateSettingFixSaleMin.ActiveDate);
                        else orderQuery = query.OrderByDescending(o => o.RateSettingFixSaleMin.ActiveDate);
                        break;
                    case CommissionSettingSortBy.RateSettingFixTransfer:
                        if (sortByParam.Ascending) orderQuery = query.OrderBy(o => o.RateSettingFixTransferMin.ActiveDate);
                        else orderQuery = query.OrderByDescending(o => o.RateSettingFixSaleModelMin.ActiveDate);
                        break;
                    case CommissionSettingSortBy.RateSettingSale:
                        if (sortByParam.Ascending) orderQuery = query.OrderBy(o => o.RateSettingSaleMin.ActiveDate);
                        else orderQuery = query.OrderByDescending(o => o.RateSettingSaleMin.ActiveDate);
                        break;
                    case CommissionSettingSortBy.RateSettingTransfer:
                        if (sortByParam.Ascending) orderQuery = query.OrderBy(o => o.RateSettingTransferMin.ActiveDate);
                        else orderQuery = query.OrderByDescending(o => o.RateSettingTransferMin.ActiveDate);
                        break;
                    case CommissionSettingSortBy.RateSettingAgent:
                        if (sortByParam.Ascending) orderQuery = query.OrderBy(o => o.RateSettingAgentMin.ActiveDate);
                        else orderQuery = query.OrderByDescending(o => o.RateSettingAgentMin.ActiveDate);
                        break;
                    default:
                        orderQuery = query.OrderBy(o => o.Project.ProjectNo);
                        break;
                }
            }
            else
            {
                orderQuery = query.OrderBy(o => o.Project.ProjectNo);
            }

            orderQuery.ThenBy(o => o.Project.BG.BGNo);
            query = orderQuery;
        }

        private static string StringFormatDate(DateTime? dStart, DateTime? dEnd)
        {
            if (dStart.HasValue && dEnd.HasValue)
            {
                if (dStart.Value == dEnd.Value)
                {
                    return string.Format("{0:MMM yyyy} - Persent", dStart.Value);
                }
                else
                {
                    return string.Format("{0:MMM yyyy} - {1:MMM yyyy}", dStart.Value, dEnd.Value);
                }
            }
            else if (dStart.HasValue && !dEnd.HasValue)
            {
                return string.Format("{0:MMM yyyy} - Persent", dStart.Value);
            }

            return "-";
        }
    }

    public class CommissionSettingQueryResult
    {
        public models.PRJ.Project Project { get; set; }
        public GeneralSetting GeneralSettingMin { get; set; }
        public GeneralSetting GeneralSettingMax { get; set; }
        public RateSettingFixSaleModel RateSettingFixSaleModelMin { get; set; }
        public RateSettingFixSaleModel RateSettingFixSaleModelMax { get; set; }
        public RateSettingFixTransferModel RateSettingFixTransferModelMin { get; set; }
        public RateSettingFixTransferModel RateSettingFixTransferModelMax { get; set; }
        public RateSettingFixSale RateSettingFixSaleMin { get; set; }
        public RateSettingFixSale RateSettingFixSaleMax { get; set; }
        public RateSettingFixTransfer RateSettingFixTransferMin { get; set; }
        public RateSettingFixTransfer RateSettingFixTransferMax { get; set; }
        public RateSettingSale RateSettingSaleMin { get; set; }
        public RateSettingSale RateSettingSaleMax { get; set; }
        public RateSettingTransfer RateSettingTransferMin { get; set; }
        public RateSettingTransfer RateSettingTransferMax { get; set; }
        public RateSettingAgent RateSettingAgentMin { get; set; }
        public RateSettingAgent RateSettingAgentMax { get; set; }
    }
}
