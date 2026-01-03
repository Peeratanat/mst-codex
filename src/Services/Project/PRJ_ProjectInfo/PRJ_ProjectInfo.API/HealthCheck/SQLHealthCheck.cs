using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Data;
using Microsoft.Data.SqlClient;

namespace PRJ_ProjectInfo.API.HealthCheck;

public class SQLHealthCheck : IHealthCheck
{
    public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
    {
        if (CheckConnection())
            return Task.FromResult(HealthCheckResult.Healthy("Connect DB Successfully !!"));
        return Task.FromResult(HealthCheckResult.Unhealthy("Cannot Connect To DB !!"));
    }

    private bool CheckConnection()
    {
        try
        {
            var Result = false;
            var connectionString = Environment.GetEnvironmentVariable("DBConnectionString");
            using (var conn = new SqlConnection(connectionString))
            {
                conn.Open();
                if (conn.State == ConnectionState.Open)
                {
                    Result = true;
                }
                conn.Close();
            }
                
            return Result;
        }
        catch
        {
            return false;
        }
    }

}
