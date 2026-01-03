using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Database.Models
{
    public class DbContextFactory : IDesignTimeDbContextFactory<DatabaseContext>
    {
        private static string _connectionString;

        public DatabaseContext CreateDbContext()
        {
            return CreateDbContext(null);
        }

        public DatabaseContext CreateDbContext(string[] args)
        {
            if (args != null && args.Length > 0)
            {
                _connectionString = args[0];
            }
            else
            {
                // Get environment
                string environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
                environment = "Local";
                // Build config
                IConfiguration config;
                if (!string.IsNullOrEmpty(environment))
                {
                    config = new ConfigurationBuilder()
                    .AddJsonFile($"appsettings.{environment}.json")
                    .Build();
                }
                else
                {
                    config = new ConfigurationBuilder()
                    .AddJsonFile($"appsettings.Local.json")
                    .Build();
                }
                _connectionString = config["DBConnectionString"];

                if (string.IsNullOrEmpty(_connectionString))
                {
                    LoadConnectionString();
                }
            }

            var builder = new DbContextOptionsBuilder<DatabaseContext>();
            builder.UseSqlServer(_connectionString, o => o.CommandTimeout((int)TimeSpan.FromHours(5).TotalSeconds));

            return new DatabaseContext(builder.Options);
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
