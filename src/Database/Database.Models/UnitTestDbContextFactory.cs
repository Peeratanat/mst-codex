using System; 
using Database.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Database.UnitTestExtensions
{
    public class UnitTestDbContextFactory : IDisposable
    {
        private readonly string _connectionString = @"Server=192.168.4.157;Database=crmrevo_dev;User Id=revo;Password=P@ssw0rd;Timeout=300; Application Name= CRMMasterAPI;Encrypt=False;";
        //private readonly string _connectionString = @"Server=192.168.4.91;Database=crmrevo;User Id=revo;Password=P@ssw0rd;Timeout=300;";

        private DbContextOptions<DatabaseContext> CreateOptions()
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(_connectionString);
            int Timeout = builder.ConnectTimeout;

            return new DbContextOptionsBuilder<DatabaseContext>()
                .UseSqlServer(_connectionString, opts =>
                {
                    opts.CommandTimeout(Timeout);
                    opts.EnableRetryOnFailure();
                }).Options;
        }

        private DbContextOptions<DbQueryContext> CreateDbQueryOptions()
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(_connectionString);
            int Timeout = builder.ConnectTimeout;

            return new DbContextOptionsBuilder<DbQueryContext>()
                .UseSqlServer(_connectionString, opts =>
                {
                    opts.CommandTimeout(Timeout);
                    opts.EnableRetryOnFailure();
                }).Options;
        }

        public DatabaseContext CreateContext()
        {
            var db = new DatabaseContext(CreateOptions());

            return db;
        }

        public DbQueryContext CreateDbQueryContext()
        {
            var dbQuery = new DbQueryContext(CreateDbQueryOptions());

            return dbQuery;
        }

        public void Dispose() { }
    }
}
