using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;

namespace Database.AuditLogs
{
    public class DbContextFactory : IDesignTimeDbContextFactory<AuditLogContext>
    {
        private static string _connectionString;

        public AuditLogContext CreateDbContext()
        {
            return CreateDbContext(null);
        }

        public AuditLogContext CreateDbContext(string[] args)
        {
            if (args != null && args.Length > 0)
            {
                _connectionString = args[0];
            }
            else
            {
                // Get environment
                string environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

                // Build config
                IConfiguration config = new ConfigurationBuilder()
                    .AddJsonFile($"appsettings.{environment}.json")
                    .Build();
                _connectionString = config["AuditLogDBConnectionString"];

                if (string.IsNullOrEmpty(_connectionString))
                {
                    LoadConnectionString();
                }
            }

            var builder = new DbContextOptionsBuilder<AuditLogContext>();
            builder.UseSqlServer(_connectionString, o => o.CommandTimeout((int)TimeSpan.FromHours(5).TotalSeconds));

            return new AuditLogContext(builder.Options);
        }

        private static void LoadConnectionString()
        {
            //_connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=AP_CRM;Trusted_Connection=True;";
            // _connectionString = @"Server=crmrevo.database.windows.net;Database=crmdb;User Id=noppolp;Password=!@qwaszzzzzZ;";
            //_connectionString = @"Data Source=softever.co.th;Initial Catalog=AP_CRM;User Id=sa;Password=@minMitDev02;Trusted_Connection=False;";
            //_connectionString = @"Data Source=softever.co.th;Initial Catalog=AP_CRM_LOCAL;User Id=sa;Password=@minMitDev02;Trusted_Connection=False;";
            //_connectionString = @"Server=192.168.2.27;Database=crmrevo_data_new;User Id=crmv;Password=apt@ven2018;";
            //_connectionString = @"Server=192.168.2.27;Database=crmrevo_dev;User Id=crmv;Password=apt@ven2018;";
            //_connectionString = @"Server=192.168.2.27;Database=crmrevo;User Id=crmv;Password=apt@ven2018;";
            //_connectionString = @"Server=192.168.2.27;Database=crmrevo_test;User Id=crmv;Password=apt@ven2018;";
            //_connectionString = @"Server=192.168.2.27;Database=crmrevo_data_new;User Id=crmv;Password=apt@ven2018;";
        }
    }
}
