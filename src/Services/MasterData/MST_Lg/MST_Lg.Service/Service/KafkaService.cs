using Confluent.Kafka;
using Newtonsoft.Json;
using Database.Models.PRJ;

namespace MST_Lg.Services
{
    public class KafkaService
    {
        private readonly IProducer<string, string> _producer;
        public KafkaService(IProducer<string, string> producer)
        {
            _producer = producer;
        }
        private async Task ProduceMessage(string topic, object data, bool isLog = false)
        {
            //topic = "IssuesTracking-" + topic;
            //
            string env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            env = env.ToLower();
            if (!isLog)
            {
                if (env == "dev" || env == "development" || env == "d" || env == "local") topic = "dev-" + topic;
                if (env == "uat" || env == "u") topic = "uat-" + topic;
            }
            //
            string serailizedData = JsonConvert.SerializeObject(data);
            var result = await _producer.ProduceAsync(topic, new Message<string, string> { Key = topic, Value = serailizedData });
            _producer.Flush(TimeSpan.FromSeconds(10));
            Console.WriteLine($"Delivered '{result.Value}' to '{result.TopicPartitionOffset}'");

        }
        public async Task ProduceNewLG(LetterGuarantee LetterGuaranteeID)
        {
            if (LetterGuaranteeID != null)
            {
                var data = new
                {
                    letterGuarantee_id = LetterGuaranteeID?.ID,
                    issue_date = LetterGuaranteeID?.IssueDate,
                    expire_date = LetterGuaranteeID?.ExpiredDate,
                    meter_number = LetterGuaranteeID?.MeterNumber,
                    isJuristicSetup = LetterGuaranteeID?.IsJuristicSetup,
                    juristicSetup_date = LetterGuaranteeID?.JuristicSetupDate,
                    juristicSetupBy = LetterGuaranteeID?.JuristicSetupBy,
                    juristicSetup_remark = LetterGuaranteeID?.JuristicSetupRemarks,
                    bank_id = LetterGuaranteeID?.BankID,
                    bank = LetterGuaranteeID?.Banks?.Alias,
                    company_id = LetterGuaranteeID?.CompanyID,
                    company = LetterGuaranteeID?.Company?.NameTH,
                    company_code = LetterGuaranteeID?.Company?.Code,
                    cost_center = LetterGuaranteeID?.CostCenter,
                    project_id = LetterGuaranteeID?.ProjectID,
                    project_name = LetterGuaranteeID?.Project?.ProjectNameTH,
                    project_area = LetterGuaranteeID?.ProjectArea,
                    letterOfGuarantee_no = LetterGuaranteeID?.LetterOfGuaranteeNo,
                    lgGuarantor_name = LetterGuaranteeID?.LGGuarantor?.Name,
                    lgGuarantorMasterCenter_id = LetterGuaranteeID?.LGGuarantorMasterCenterID,
                    lgTypeMasterCenter_id = LetterGuaranteeID?.LGTypeMasterCenterID,
                    lgType_name = LetterGuaranteeID?.LGType?.Name,
                    issue_amount = LetterGuaranteeID?.IssueAmount,
                    refund_amount = LetterGuaranteeID?.RefundAmount,
                    remain_amount = LetterGuaranteeID?.RemainAmount,
                    lgGuaranteeConditionMasterCenter_id = LetterGuaranteeID?.LGGuaranteeConditionsMasterCenterID,
                    lgGuaranteeCondition_name = LetterGuaranteeID?.LGGuaranteeConditions?.Name,
                    remark = LetterGuaranteeID?.Remark,
                    isCancel = LetterGuaranteeID?.IsCanceled,
                    cancel_date = LetterGuaranteeID?.CancelDate,
                    cancelByUserID = LetterGuaranteeID?.CancelByUserID,
                    cancel_remark = LetterGuaranteeID?.CancelRemark,
                    effective_date = LetterGuaranteeID?.EffectiveDate,
                    expirePeriod_date = LetterGuaranteeID?.ExpiredPeriodDate,
                    conditioncal_fee = LetterGuaranteeID?.ConditionCalFee,
                    fee_rate = LetterGuaranteeID?.FeeRate,
                    isDelete = LetterGuaranteeID?.IsDeleted,
                    created = LetterGuaranteeID?.Created,
                    created_by = LetterGuaranteeID?.CreatedBy?.DisplayName,
                    upated = LetterGuaranteeID?.Updated,
                    update_by = LetterGuaranteeID?.UpdatedBy?.DisplayName,
                    status = "ADD",
                    ProjectNo = LetterGuaranteeID?.Project.ProjectNo,
                    feeRateAmountByPeriod = LetterGuaranteeID?.FeeRateAmountByPeriod,
                    lgSubTypeKey = LetterGuaranteeID?.LGSubType?.Key,
                    lgSubTypeName = LetterGuaranteeID?.LGSubType?.Name,
                };
                string TOPIC = Environment.GetEnvironmentVariable("KAFKA_TOPIC_NEW_LG");
                await ProduceMessage(TOPIC, data);
            }
        }
        public async Task ProduceEditLG(LetterGuarantee LetterGuaranteeID)
        {
            if (LetterGuaranteeID != null)
            {
                var data = new
                {
                    letterGuarantee_id = LetterGuaranteeID.ID,
                    issue_date = LetterGuaranteeID.IssueDate,
                    expire_date = LetterGuaranteeID.ExpiredDate,
                    meter_number = LetterGuaranteeID.MeterNumber,
                    isJuristicSetup = LetterGuaranteeID.IsJuristicSetup,
                    juristicSetup_date = LetterGuaranteeID.JuristicSetupDate,
                    juristicSetupBy = LetterGuaranteeID.JuristicSetupBy,
                    juristicSetup_remark = LetterGuaranteeID.JuristicSetupRemarks,
                    bank_id = LetterGuaranteeID.BankID,
                    bank = LetterGuaranteeID.Banks?.Alias,
                    company_id = LetterGuaranteeID.CompanyID,
                    company = LetterGuaranteeID.Company?.NameTH,
                    company_code = LetterGuaranteeID.Company?.Code,
                    cost_center = LetterGuaranteeID.CostCenter,
                    project_id = LetterGuaranteeID.ProjectID,
                    project_name = LetterGuaranteeID.Project?.ProjectNameTH,
                    project_area = LetterGuaranteeID.ProjectArea,
                    letterOfGuarantee_no = LetterGuaranteeID.LetterOfGuaranteeNo,
                    lgGuarantor_name = LetterGuaranteeID.LGGuarantor?.Name,
                    lgGuarantorMasterCenter_id = LetterGuaranteeID.LGGuarantorMasterCenterID,
                    lgTypeMasterCenter_id = LetterGuaranteeID.LGTypeMasterCenterID,
                    lgType_name = LetterGuaranteeID.LGType?.Name,
                    issue_amount = LetterGuaranteeID.IssueAmount,
                    refund_amount = LetterGuaranteeID.RefundAmount,
                    remain_amount = LetterGuaranteeID.RemainAmount,
                    lgGuaranteeConditionMasterCenter_id = LetterGuaranteeID.LGGuaranteeConditionsMasterCenterID,
                    lgGuaranteeCondition_name = LetterGuaranteeID.LGGuaranteeConditions?.Name,
                    remark = LetterGuaranteeID.Remark,
                    isCancel = LetterGuaranteeID.IsCanceled,
                    cancel_date = LetterGuaranteeID.CancelDate,
                    cancelByUserID = LetterGuaranteeID.CancelByUserID,
                    cancel_remark = LetterGuaranteeID.CancelRemark,
                    effective_date = LetterGuaranteeID.EffectiveDate,
                    expirePeriod_date = LetterGuaranteeID.ExpiredPeriodDate,
                    conditioncal_fee = LetterGuaranteeID.ConditionCalFee,
                    fee_rate = LetterGuaranteeID.FeeRate,
                    isDelete = LetterGuaranteeID.IsDeleted,
                    created = LetterGuaranteeID.Created,
                    created_by = LetterGuaranteeID.CreatedBy?.DisplayName,
                    upated = LetterGuaranteeID.Updated,
                    update_by = LetterGuaranteeID?.UpdatedBy?.DisplayName,
                    status = "UPDATE",
                    ProjectNo = LetterGuaranteeID.Project.ProjectNo,
                    feeRateAmountByPeriod = LetterGuaranteeID.FeeRateAmountByPeriod,
                    lgSubTypeKey = LetterGuaranteeID?.LGSubType?.Key,
                    lgSubTypeName = LetterGuaranteeID?.LGSubType?.Name,
                };
                string TOPIC = Environment.GetEnvironmentVariable("KAFKA_TOPIC_EDIT_LG");
                await ProduceMessage(TOPIC, data);
            }
        }
        public async Task ProduceInActiveLG(LetterGuarantee LetterGuaranteeID)
        {
            if (LetterGuaranteeID != null)
            {
                var data = new
                {
                    letterGuarantee_id = LetterGuaranteeID.ID,
                    issue_date = LetterGuaranteeID.IssueDate,
                    expire_date = LetterGuaranteeID.ExpiredDate,
                    meter_number = LetterGuaranteeID.MeterNumber,
                    isJuristicSetup = LetterGuaranteeID.IsJuristicSetup,
                    juristicSetup_date = LetterGuaranteeID.JuristicSetupDate,
                    juristicSetupBy = LetterGuaranteeID.JuristicSetupBy,
                    juristicSetup_remark = LetterGuaranteeID.JuristicSetupRemarks,
                    bank_id = LetterGuaranteeID.BankID,
                    bank = LetterGuaranteeID.Banks?.Alias,
                    company_id = LetterGuaranteeID.CompanyID,
                    company = LetterGuaranteeID.Company?.NameTH,
                    company_code = LetterGuaranteeID.Company?.Code,
                    cost_center = LetterGuaranteeID.CostCenter,
                    project_id = LetterGuaranteeID.ProjectID,
                    project_name = LetterGuaranteeID.Project?.ProjectNameTH,
                    project_area = LetterGuaranteeID.ProjectArea,
                    letterOfGuarantee_no = LetterGuaranteeID.LetterOfGuaranteeNo,
                    lgGuarantor_name = LetterGuaranteeID.LGGuarantor?.Name,
                    lgGuarantorMasterCenter_id = LetterGuaranteeID.LGGuarantorMasterCenterID,
                    lgTypeMasterCenter_id = LetterGuaranteeID.LGTypeMasterCenterID,
                    lgType_name = LetterGuaranteeID.LGType?.Name,
                    issue_amount = LetterGuaranteeID.IssueAmount,
                    refund_amount = LetterGuaranteeID.RefundAmount,
                    remain_amount = LetterGuaranteeID.RemainAmount,
                    lgGuaranteeConditionMasterCenter_id = LetterGuaranteeID.LGGuaranteeConditionsMasterCenterID,
                    lgGuaranteeCondition_name = LetterGuaranteeID.LGGuaranteeConditions?.Name,
                    remark = LetterGuaranteeID.Remark,
                    isCancel = LetterGuaranteeID.IsCanceled,
                    cancel_date = LetterGuaranteeID.CancelDate,
                    cancelByUserID = LetterGuaranteeID.CancelByUserID,
                    cancel_remark = LetterGuaranteeID.CancelRemark,
                    effective_date = LetterGuaranteeID.EffectiveDate,
                    expirePeriod_date = LetterGuaranteeID.ExpiredPeriodDate,
                    conditioncal_fee = LetterGuaranteeID.ConditionCalFee,
                    fee_rate = LetterGuaranteeID.FeeRate,
                    isDelete = LetterGuaranteeID.IsDeleted,
                    created = LetterGuaranteeID.Created,
                    created_by = LetterGuaranteeID.CreatedBy?.DisplayName,
                    upated = LetterGuaranteeID.Updated,
                    update_by = LetterGuaranteeID?.UpdatedBy?.DisplayName,
                    status = "InActive",
                    ProjectNo = LetterGuaranteeID.Project?.ProjectNo,
                    feeRateAmountByPeriod = LetterGuaranteeID.FeeRateAmountByPeriod,
                    lgSubTypeKey = LetterGuaranteeID?.LGSubType?.Key,
                    lgSubTypeName = LetterGuaranteeID?.LGSubType?.Name,
                };
                string TOPIC = Environment.GetEnvironmentVariable("KAFKA_TOPIC_INACTIVE_LG");
                await ProduceMessage(TOPIC, data);
            }
        }
        public async Task ProduceActiveLG(LetterGuarantee LetterGuaranteeID)
        {
            if (LetterGuaranteeID != null)
            {
                var data = new
                {
                    letterGuarantee_id = LetterGuaranteeID.ID,
                    issue_date = LetterGuaranteeID.IssueDate,
                    expire_date = LetterGuaranteeID.ExpiredDate,
                    meter_number = LetterGuaranteeID.MeterNumber,
                    isJuristicSetup = LetterGuaranteeID.IsJuristicSetup,
                    juristicSetup_date = LetterGuaranteeID.JuristicSetupDate,
                    juristicSetupBy = LetterGuaranteeID.JuristicSetupBy,
                    juristicSetup_remark = LetterGuaranteeID.JuristicSetupRemarks,
                    bank_id = LetterGuaranteeID.BankID,
                    bank = LetterGuaranteeID.Banks?.Alias,
                    company_id = LetterGuaranteeID.CompanyID,
                    company = LetterGuaranteeID.Company?.NameTH,
                    company_code = LetterGuaranteeID.Company?.Code,
                    cost_center = LetterGuaranteeID.CostCenter,
                    project_id = LetterGuaranteeID.ProjectID,
                    project_name = LetterGuaranteeID.Project?.ProjectNameTH,
                    project_area = LetterGuaranteeID.ProjectArea,
                    letterOfGuarantee_no = LetterGuaranteeID.LetterOfGuaranteeNo,
                    lgGuarantor_name = LetterGuaranteeID.LGGuarantor?.Name,
                    lgGuarantorMasterCenter_id = LetterGuaranteeID.LGGuarantorMasterCenterID,
                    lgTypeMasterCenter_id = LetterGuaranteeID.LGTypeMasterCenterID,
                    lgType_name = LetterGuaranteeID.LGType?.Name,
                    issue_amount = LetterGuaranteeID.IssueAmount,
                    refund_amount = LetterGuaranteeID.RefundAmount,
                    remain_amount = LetterGuaranteeID.RemainAmount,
                    lgGuaranteeConditionMasterCenter_id = LetterGuaranteeID.LGGuaranteeConditionsMasterCenterID,
                    lgGuaranteeCondition_name = LetterGuaranteeID.LGGuaranteeConditions?.Name,
                    remark = LetterGuaranteeID.Remark,
                    isCancel = LetterGuaranteeID.IsCanceled,
                    cancel_date = LetterGuaranteeID.CancelDate,
                    cancelByUserID = LetterGuaranteeID.CancelByUserID,
                    cancel_remark = LetterGuaranteeID.CancelRemark,
                    effective_date = LetterGuaranteeID.EffectiveDate,
                    expirePeriod_date = LetterGuaranteeID.ExpiredPeriodDate,
                    conditioncal_fee = LetterGuaranteeID.ConditionCalFee,
                    fee_rate = LetterGuaranteeID.FeeRate,
                    isDelete = LetterGuaranteeID.IsDeleted,
                    created = LetterGuaranteeID.Created,
                    created_by = LetterGuaranteeID.CreatedBy?.DisplayName,
                    upated = LetterGuaranteeID.Updated,
                    update_by = LetterGuaranteeID?.UpdatedBy?.DisplayName,
                    status = "Active",
                    ProjectNo = LetterGuaranteeID.Project.ProjectNo,
                    feeRateAmountByPeriod = LetterGuaranteeID.FeeRateAmountByPeriod,
                    lgSubTypeKey = LetterGuaranteeID?.LGSubType?.Key,
                    lgSubTypeName = LetterGuaranteeID?.LGSubType?.Name,
                };
                string TOPIC = Environment.GetEnvironmentVariable("KAFKA_TOPIC_ACTIVE_LG");
                await ProduceMessage(TOPIC, data);
            }
        }
        public async Task ProduceDeleteLG(LetterGuarantee LetterGuaranteeID)
        {
            if (LetterGuaranteeID != null)
            {
                var data = new
                {
                    letterGuarantee_id = LetterGuaranteeID.ID,
                    issue_date = LetterGuaranteeID.IssueDate,
                    expire_date = LetterGuaranteeID.ExpiredDate,
                    meter_number = LetterGuaranteeID.MeterNumber,
                    isJuristicSetup = LetterGuaranteeID.IsJuristicSetup,
                    juristicSetup_date = LetterGuaranteeID.JuristicSetupDate,
                    juristicSetupBy = LetterGuaranteeID.JuristicSetupBy,
                    juristicSetup_remark = LetterGuaranteeID.JuristicSetupRemarks,
                    bank_id = LetterGuaranteeID.BankID,
                    bank = LetterGuaranteeID.Banks?.Alias,
                    company_id = LetterGuaranteeID.CompanyID,
                    company = LetterGuaranteeID.Company?.NameTH,
                    company_code = LetterGuaranteeID.Company?.Code,
                    cost_center = LetterGuaranteeID.CostCenter,
                    project_id = LetterGuaranteeID.ProjectID,
                    project_name = LetterGuaranteeID.Project?.ProjectNameTH,
                    project_area = LetterGuaranteeID.ProjectArea,
                    letterOfGuarantee_no = LetterGuaranteeID.LetterOfGuaranteeNo,
                    lgGuarantor_name = LetterGuaranteeID.LGGuarantor?.Name,
                    lgGuarantorMasterCenter_id = LetterGuaranteeID.LGGuarantorMasterCenterID,
                    lgTypeMasterCenter_id = LetterGuaranteeID.LGTypeMasterCenterID,
                    lgType_name = LetterGuaranteeID.LGType?.Name,
                    issue_amount = LetterGuaranteeID.IssueAmount,
                    refund_amount = LetterGuaranteeID.RefundAmount,
                    remain_amount = LetterGuaranteeID.RemainAmount,
                    lgGuaranteeConditionMasterCenter_id = LetterGuaranteeID.LGGuaranteeConditionsMasterCenterID,
                    lgGuaranteeCondition_name = LetterGuaranteeID.LGGuaranteeConditions?.Name,
                    remark = LetterGuaranteeID.Remark,
                    isCancel = LetterGuaranteeID.IsCanceled,
                    cancel_date = LetterGuaranteeID.CancelDate,
                    cancelByUserID = LetterGuaranteeID.CancelByUserID,
                    cancel_remark = LetterGuaranteeID.CancelRemark,
                    effective_date = LetterGuaranteeID.EffectiveDate,
                    expirePeriod_date = LetterGuaranteeID.ExpiredPeriodDate,
                    conditioncal_fee = LetterGuaranteeID.ConditionCalFee,
                    fee_rate = LetterGuaranteeID.FeeRate,
                    isDelete = LetterGuaranteeID.IsDeleted,
                    created = LetterGuaranteeID.Created,
                    created_by = LetterGuaranteeID.CreatedBy?.DisplayName,
                    upated = LetterGuaranteeID.Updated,
                    update_by = LetterGuaranteeID?.UpdatedBy?.DisplayName,
                    status = "DELETE",
                    ProjectNo = LetterGuaranteeID.Project.ProjectNo,
                    feeRateAmountByPeriod = LetterGuaranteeID.FeeRateAmountByPeriod,
                    lgSubTypeKey = LetterGuaranteeID?.LGSubType?.Key,
                    lgSubTypeName = LetterGuaranteeID?.LGSubType?.Name,
                };
                string TOPIC = Environment.GetEnvironmentVariable("KAFKA_TOPIC_DELETE_LG");
                await ProduceMessage(TOPIC, data);
            }
        }
    }

    class ApplicationLog
    {
        public string HttpMethod { get; set; } = "";
        public string HttpStatusCode { get; set; } = "";
        public string LogLevel { get; set; } = "";
        public string LogMessage { get; set; } = "";
        public string LogSystem { get; set; } = "";
        public string Module { get; set; } = "";
        public string SubModule { get; set; } = "";
        public DateTime TimeStamp { get; set; }
        public string User { get; set; } = "";
    }
}
