using Database.Models;
using System;
using System.Threading.Tasks;
using FileStorage;
using Microsoft.Extensions.Configuration;
using Confluent.Kafka;
using Confluent.SchemaRegistry.Serdes;
using Confluent.SchemaRegistry;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Database.Models.SAL;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Base.DTOs.MST;
using Database.Models.PRJ;
using System.Reactive;
using System.Net.Http;
using Database.Models.USR;
using System.Net;
using Microsoft.AspNetCore.Mvc;

namespace Finance.Services
{
    public class KafkaService : IKafkaService
    {
        private readonly DatabaseContext DB;
        private readonly DbQueryContext DBQuery;
        private ProducerConfig producerConfig = new ProducerConfig();
        private readonly IProducer<Null, string> _producer;
        private SchemaRegistryConfig schemaregistryConfig;
        public KafkaService(DatabaseContext db, IConfiguration configuration, DbQueryContext dbQuery)
        {
            DB = db;
            DBQuery = dbQuery;

            schemaregistryConfig = new SchemaRegistryConfig
            {
                Url = Environment.GetEnvironmentVariable("KAFKA_SCHEMAREGISTRYCONFIGURL")  //"uatschemaregistry01.ap-thai.com:8081";
            };

            producerConfig = new ProducerConfig
            {
                BootstrapServers = Environment.GetEnvironmentVariable("Kafka_Bootstrapservers"),
                SaslUsername = Environment.GetEnvironmentVariable("Kafka_SaslUsername"),
                SaslPassword = Environment.GetEnvironmentVariable("Kafka_SaslPassword"),
                SslCaPem = "-----BEGIN CERTIFICATE-----\n" + Environment.GetEnvironmentVariable("Kafka_SslCaPem") + "\n-----END CERTIFICATE-----",
                SslCertificatePem = "-----BEGIN CERTIFICATE-----\n" + Environment.GetEnvironmentVariable("Kafka_SslCertificatePem") + "\n-----END CERTIFICATE-----",
                SslKeyPem = "-----BEGIN PRIVATE KEY-----\n" + Environment.GetEnvironmentVariable("Kafka_SslKeyPem") + "\n-----END PRIVATE KEY-----",
                SecurityProtocol = SecurityProtocol.SaslSsl,
                SslEndpointIdentificationAlgorithm = SslEndpointIdentificationAlgorithm.None,
                SaslMechanism = SaslMechanism.ScramSha512
            };
            _producer = new ProducerBuilder<Null, string>(producerConfig).Build();
        }
        public KafkaService()
        {
            schemaregistryConfig = new SchemaRegistryConfig
            {
                Url = Environment.GetEnvironmentVariable("KAFKA_SCHEMAREGISTRYCONFIGURL")  //"uatschemaregistry01.ap-thai.com:8081";
            };

            producerConfig = new ProducerConfig
            {
                BootstrapServers = Environment.GetEnvironmentVariable("Kafka_Bootstrapservers"),
                SaslUsername = Environment.GetEnvironmentVariable("Kafka_SaslUsername"),
                SaslPassword = Environment.GetEnvironmentVariable("Kafka_SaslPassword"),
                SslCaPem = "-----BEGIN CERTIFICATE-----\n" + Environment.GetEnvironmentVariable("Kafka_SslCaPem") + "\n-----END CERTIFICATE-----",
                SslCertificatePem = "-----BEGIN CERTIFICATE-----\n" + Environment.GetEnvironmentVariable("Kafka_SslCertificatePem") + "\n-----END CERTIFICATE-----",
                SslKeyPem = "-----BEGIN PRIVATE KEY-----\n" + Environment.GetEnvironmentVariable("Kafka_SslKeyPem") + "\n-----END PRIVATE KEY-----",
                SecurityProtocol = SecurityProtocol.SaslSsl,
                SslEndpointIdentificationAlgorithm = SslEndpointIdentificationAlgorithm.None,
                SaslMechanism = SaslMechanism.ScramSha512,
                Acks = Acks.None,
                LingerMs = 40,
                BatchSize = 3276000
            };
            _producer = new ProducerBuilder<Null, string>(producerConfig).Build();
        }
        public KafkaService(DatabaseContext db, DbQueryContext dbQuery)
        {
            DB = db;
            DBQuery = dbQuery;
            schemaregistryConfig = new SchemaRegistryConfig
            {
                Url = Environment.GetEnvironmentVariable("KAFKA_SCHEMAREGISTRYCONFIGURL")  //"uatschemaregistry01.ap-thai.com:8081";
            };

            producerConfig = new ProducerConfig
            {
                BootstrapServers = Environment.GetEnvironmentVariable("Kafka_Bootstrapservers"),
                SaslUsername = Environment.GetEnvironmentVariable("Kafka_SaslUsername"),
                SaslPassword = Environment.GetEnvironmentVariable("Kafka_SaslPassword"),
                SslCaPem = "-----BEGIN CERTIFICATE-----\n" + Environment.GetEnvironmentVariable("Kafka_SslCaPem") + "\n-----END CERTIFICATE-----",
                SslCertificatePem = "-----BEGIN CERTIFICATE-----\n" + Environment.GetEnvironmentVariable("Kafka_SslCertificatePem") + "\n-----END CERTIFICATE-----",
                SslKeyPem = "-----BEGIN PRIVATE KEY-----\n" + Environment.GetEnvironmentVariable("Kafka_SslKeyPem") + "\n-----END PRIVATE KEY-----",
                SecurityProtocol = SecurityProtocol.SaslSsl,
                SslEndpointIdentificationAlgorithm = SslEndpointIdentificationAlgorithm.None,
                SaslMechanism = SaslMechanism.ScramSha512
            };
            _producer = new ProducerBuilder<Null, string>(producerConfig).Build();
        }
        public KafkaService(DatabaseContext db)
        {
            DB = db;
            schemaregistryConfig = new SchemaRegistryConfig
            {
                Url = Environment.GetEnvironmentVariable("KAFKA_SCHEMAREGISTRYCONFIGURL")  //"uatschemaregistry01.ap-thai.com:8081";
            };

            producerConfig = new ProducerConfig
            {
                BootstrapServers = Environment.GetEnvironmentVariable("Kafka_Bootstrapservers"),
                SaslUsername = Environment.GetEnvironmentVariable("Kafka_SaslUsername"),
                SaslPassword = Environment.GetEnvironmentVariable("Kafka_SaslPassword"),
                SslCaPem = "-----BEGIN CERTIFICATE-----\n" + Environment.GetEnvironmentVariable("Kafka_SslCaPem") + "\n-----END CERTIFICATE-----",
                SslCertificatePem = "-----BEGIN CERTIFICATE-----\n" + Environment.GetEnvironmentVariable("Kafka_SslCertificatePem") + "\n-----END CERTIFICATE-----",
                SslKeyPem = "-----BEGIN PRIVATE KEY-----\n" + Environment.GetEnvironmentVariable("Kafka_SslKeyPem") + "\n-----END PRIVATE KEY-----",
                SecurityProtocol = SecurityProtocol.SaslSsl,
                SslEndpointIdentificationAlgorithm = SslEndpointIdentificationAlgorithm.None,
                SaslMechanism = SaslMechanism.ScramSha512
            };
            _producer = new ProducerBuilder<Null, string>(producerConfig).Build();
        }
        private async Task ProduceMessage(string topic, object data, bool isLog = false)
        {
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? string.Empty;

            if (data == null) 
                throw new ArgumentNullException(nameof(data));
                
            if (string.IsNullOrWhiteSpace(topic)) 
                throw new ArgumentNullException(nameof(topic));

            try
            {
                var serializedData = JsonConvert.SerializeObject(data);
                var envLower = environment.ToLowerInvariant();

                if (!isLog)
                {
                    if (envLower == "dev" || envLower == "development" || envLower == "d" || envLower == "local")
                    {
                        topic = $"dev-{topic}";
                    }
                    else if (envLower == "uat" || envLower == "u")
                    {
                        topic = $"uat-{topic}";
                    }
                }

                _producer.Produce(topic, new Message<Null, string>
                {
                    Value = serializedData
                },
                deliveryReport =>
                {
                    if (deliveryReport.Error.IsError)
                    {
                        Console.WriteLine($"[Kafka] Failed to deliver to {topic}: {deliveryReport.Error.Reason} [Timestamp]: {DateTime.Now}");
                    }else{
                        Console.WriteLine($"Delivered '{deliveryReport.Value}' to '{deliveryReport.TopicPartitionOffset}'");
                    }
                });
            }
            catch (KafkaException ex) when (ex.Error.Code == ErrorCode.Local_QueueFull)
            {
                Console.WriteLine($"[Kafka] Queue full, drop message for topic '{topic}' [Timestamp]: {DateTime.Now}");
            }
            catch (KafkaException ex)
            {
                Console.WriteLine($"[Kafka] Producer error: {ex.Error.Reason} [Timestamp]: {DateTime.Now}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[Kafka] Unexpected producer error: {ex.Message} [{ex.InnerException}] [Timestamp]: {DateTime.Now}");
            }

            await Task.CompletedTask;
        }

        public async Task ProduceBooking(Guid? BookingID)
        {
            var booking = await DB.Bookings.FirstOrDefaultAsync(o => o.ID == BookingID);
            if (booking != null)
            {
                var data = new
                {
                    booking_id = BookingID.GetValueOrDefault(),
                    project_id = booking?.ProjectID,
                    unit_id = booking?.UnitID
                };
                string TOPIC_NEW_BOOKING = Environment.GetEnvironmentVariable("KAFKA_TOPIC_NEW_BOOKING");
                //Console.WriteLine("TOPIC_NEW_BOOKING : " + TOPIC_NEW_BOOKING);
                await ProduceMessage(TOPIC_NEW_BOOKING, data);
            }
        }

        public async Task ProduceCancelBooking(Guid? BookingID)
        {
            var booking = await DB.Bookings.IgnoreQueryFilters().FirstOrDefaultAsync(o => o.ID == BookingID);
            if (booking != null)
            {
                var data = new
                {
                    booking_id = BookingID.GetValueOrDefault(),
                    project_id = booking?.ProjectID,
                    unit_id = booking?.UnitID,
                    cancel_type = booking?.CancelType,
                    cancel_date = booking.CancelDate.GetValueOrDefault().ToString("yyyy-MM-dd HH:mm:ss.fff")
                };

                string TOPIC = Environment.GetEnvironmentVariable("KAFKA_TOPIC_CANCEL_BOOKING");
                await ProduceMessage(TOPIC, data);
            }
        }
        public async Task ProduceTransfers(Guid? TransferID)
        {
            var transfers = await DB.Transfers.Where(o => o.ID == TransferID).Include(x => x.Agreement).FirstOrDefaultAsync();
            if (transfers != null)
            {
                var data = new
                {
                    project_id = transfers?.ProjectID,
                    unit_id = transfers?.UnitID,
                    booking_id = transfers?.Agreement?.BookingID,
                    transfer_id = transfers?.ID,
                    actualtransfer_date = transfers?.ActualTransferDate.GetValueOrDefault().ToString("yyyy-MM-dd HH:mm:ss.fff")
                };
                string TOPIC = Environment.GetEnvironmentVariable("KAFKA_TOPIC_NEW_TRANSFERS");
                await ProduceMessage(TOPIC, data);
            }
        }
        public async Task ProduceAgreement(Guid? AgreementID)
        {
            var agreement = await DB.Agreements.FirstOrDefaultAsync(o => o.ID == AgreementID);
            if (agreement != null)
            {
                var data = new
                {
                    agreement_id = AgreementID.GetValueOrDefault(),
                    project_id = agreement?.ProjectID,
                    unit_id = agreement?.UnitID,
                    booking_id = agreement?.BookingID
                };
                string TOPIC = Environment.GetEnvironmentVariable("KAFKA_TOPIC_NEW_AGREEMENT");
                await ProduceMessage(TOPIC, data);
            }

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
        public async Task ProduceLogElastic(string logStr, string module, string SubModule)
        {
            /*var data = new
            {
                HttpMethod = "POST",
                HttpStatusCode = "200",
                LogLevel = "INFO",
                LogMessage = log,
                LogSystem = "CRM-API",
                Module = module,
                SubModule = SubModule,
                TimeStamp = DateTime.Now,
                User = null
                User = "System",
            };*/
            ApplicationLog log = new ApplicationLog();
            log.HttpMethod = "POST";
            log.HttpStatusCode = "200";
            log.LogLevel = "INFO";
            log.LogMessage = logStr.Trim();
            log.LogSystem = "CRM_API";
            log.Module = module;
            log.SubModule = SubModule.Trim();
            log.TimeStamp = DateTime.Now;
            log.User = "System";

            string TOPIC = "application-log";
            await ProduceMessage(TOPIC, log, true);
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
