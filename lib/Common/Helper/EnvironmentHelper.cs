
using System;

namespace Common.Helper
{
    public static class EnvironmentHelper
    {
        public static string GetEnvironment(string envName)
        {
            var env = Environment.GetEnvironmentVariable(envName);

            if (env == null) { throw new Exception($"Application not found EnvironmentVariable : " + envName); }

            return env;
        }
        public static bool GetIsDevelopment()
        {
            var environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development";
            return environmentName.ToLower() == "development" || environmentName.ToLower() == "uat"; ;
        }
    } 

    public static class GlobalEnvironment
    {
        public const string DefaultConnection = "DBConnectionString";
        public const string Kafka_Bootstrapservers = "Kafka_Bootstrapservers";
        public const string Kafka_SchemaRegistryUrl = "Kafka_SchemaRegistryUrl";
        public const string Kafka_SslCaPem = "Kafka_SslCaPem";
        public const string Kafka_SslCertificatePem = "Kafka_SslCertificatePem";
        public const string Kafka_SslKeyPem = "Kafka_SslKeyPem";
        public const string Kafka_SaslUsername = "Kafka_SaslUsername";
        public const string Kafka_SaslPassword = "Kafka_SaslPassword"; 
    }

    public class EnvironmentMediaServiceHelper
    {
        public string BaseUrl;
        public string AccessKey;
        public EnvironmentMediaServiceHelper()
        {
            BaseUrl = Environment.GetEnvironmentVariable("MediaBaseUrl") ?? "";
            AccessKey = Environment.GetEnvironmentVariable("MediaAccessKey") ?? "";
        }
    }

    public class EnvironmentAuthenHelper  
    {
        public string Authority;
        public string ApiName;
        public string ApiSecret;
        public bool IsDevelopment;
        public EnvironmentAuthenHelper()
        {
            Authority = Environment.GetEnvironmentVariable("Authority") ?? "";
            ApiName = Environment.GetEnvironmentVariable("ApiName") ?? "";
            ApiSecret = Environment.GetEnvironmentVariable("ApiSecret") ?? "";
            IsDevelopment = Convert.ToBoolean(Environment.GetEnvironmentVariable("IsDevelopment"));
        }
    }

    //public class EnvironmentAuthorizeHelper
    //{
    //    public string BaseUrl;
    //    public EnvironmentAuthorizeHelper()
    //    {
    //        BaseUrl = Environment.GetEnvironmentVariable("AuthBaseUrl") ?? "";
    //    }
    //}

    //public class EnvironmentKafkaHelper
    //{
    //    public string Bootstrapservers;
    //    public string MaxRetries;
    //    public string KafkaTimeout;
    //    public string SslCaPem;
    //    public string SslCertificatePem;
    //    public string SslKeyPem;
    //    public string SaslUsername;
    //    public string SaslPassword;
    //    public bool IsDevelopment;
    //    public EnvironmentKafkaHelper()
    //    {
    //        IsDevelopment = Environment.GetEnvironmentVariable("IsDevelopment") == "true";
    //        Bootstrapservers = Environment.GetEnvironmentVariable("Kafka_Bootstrapservers") ?? "";
    //        MaxRetries = Environment.GetEnvironmentVariable("Kafka_MaxRetries") ?? "";
    //        KafkaTimeout = Environment.GetEnvironmentVariable("Kafka_Timeout") ?? "";
    //        SslCaPem = Environment.GetEnvironmentVariable("Kafka_SslCaPem") ?? "";
    //        SslCertificatePem = Environment.GetEnvironmentVariable("Kafka_SslCertificatePem") ?? "";
    //        SslKeyPem = Environment.GetEnvironmentVariable("Kafka_SslKeyPem") ?? "";
    //        SaslUsername = Environment.GetEnvironmentVariable("Kafka_SaslUsername") ?? "";
    //        SaslPassword = Environment.GetEnvironmentVariable("Kafka_SaslPassword") ?? "";
    //    }
    //}

    //public class EnvaronmentMailHelper
    //{
    //    public string MailServiceURL;
    //    public string MailSystem;
    //    public string MailFromName;
    //    public string MailLogo;
    //    public string MailTest;
    //    public string IsMailTest;

    //    public EnvaronmentMailHelper()
    //    {
    //        MailServiceURL = Environment.GetEnvironmentVariable("MailServiceURL") ?? "";
    //        MailSystem = Environment.GetEnvironmentVariable("MailSystem") ?? "";
    //        MailFromName = Environment.GetEnvironmentVariable("MailFromName") ?? "";
    //        MailLogo = Environment.GetEnvironmentVariable("MailLogo") ?? "";
    //        MailTest = Environment.GetEnvironmentVariable("MailTest") ?? "";
    //        IsMailTest = Environment.GetEnvironmentVariable("IsMailTest") ?? "";
    //    }
    //}   
}
