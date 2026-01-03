using Confluent.Kafka;
using External.Kafka.Interface;
using External.Kafka.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace External.Kafka.Implement
{
    public class KafkaProducer : IKakfaProducer
    {
 
        private readonly IProducer<string, string> _producer;

        public KafkaProducer(IProducer<string, string> producer)
        {
            _producer = producer;  
        }

        public async Task WriteApplicationLog(ApplicationLog log)
        {
            await ProduceMessage("application-log", log);
        } 

        public async Task ProduceMessage(string topic, object data)
        {
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? string.Empty;

            if (data == null) throw new ArgumentNullException(nameof(data));
            if (string.IsNullOrEmpty(topic)) throw new ArgumentNullException(nameof(topic));
            
            try
            {
                var message = GetMessage(data);
                string key = topic;

                // ปรับ topic ตาม environment
                if (environment.ToLower() == "development")
                {
                    topic = $"dev-{topic}";
                }
                else if (environment.ToLower() == "uat")
                {
                    topic = $"uat-{topic}";
                }

                // ✅ Fire-and-Forget Pattern - ไม่รอผลลัพธ์เลย
                _producer.Produce(topic, new Message<string, string> { Key = key, Value = message }, 
                    deliveryReport =>
                    {
                        // Callback นี้จะถูกเรียกใน background thread
                        if (deliveryReport.Error.IsError)
                        {
                            Console.WriteLine($"[Kafka] Failed to deliver to {topic}: {deliveryReport.Error.Reason} [Timestamp]: {DateTime.Now}");
                        }
                        // ส่งสำเร็จก็ไม่ต้อง log (ประหยัด I/O)
                    });

                // ✅ Return ทันที ไม่รอ Kafka
                await Task.CompletedTask;
            }
            catch (Exception ex)
            {
                // Log error แต่ไม่ throw เพื่อไม่ให้ API error
                Console.WriteLine($"[Kafka] Producer error: {ex.Message} [{ex.InnerException}] [Timestamp]: {DateTime.Now}");
            }
        }

        private string GetMessage(object log) =>
            JsonConvert.SerializeObject(log);

    }
}
