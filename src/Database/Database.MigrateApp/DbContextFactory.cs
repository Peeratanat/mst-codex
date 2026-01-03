using Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Text;

namespace Database.MigrateApp
{
    public class DbContextFactory : IDesignTimeDbContextFactory<DatabaseContext>
    {
        private static string _connectionString;

        public DatabaseContext CreateDbContext()
        {
            return CreateDbContext(new string[0]);
        }

        public DatabaseContext CreateDbContext(string[] args)
        {
            if (args.Length > 0)
            {
                _connectionString = args[0];
            }
            if (string.IsNullOrEmpty(_connectionString))
            {
                LoadConnectionString();
            }

            var builder = new DbContextOptionsBuilder<DatabaseContext>();
            builder.UseSqlServer(_connectionString);

            return new DatabaseContext(builder.Options);
        }

        private static void LoadConnectionString()
        {
            // _connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=AP_CRM;Trusted_Connection=True;";
            //_connectionString = @"Data Source=softever.co.th;Initial Catalog=AP_CRM_LOCAL;User Id=sa;Password=@minMitDev02;Trusted_Connection=False;";
            _connectionString = @"Server=192.168.2.27;Database=crmrevo_test;User Id=crmv;Password=apt@ven2018;";
        }
    }
}
