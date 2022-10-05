using System.Data.SqlClient;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace BookStore.HealthChecks
{
    public class CustomHealthCheck : IHealthCheck
    {
        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            try
            {
                //throw new NotImplementedException();
            }
            catch (SqlException e)
            {
                return HealthCheckResult.Unhealthy(e.Message);
            }
            return HealthCheckResult.Healthy();
        }
    }
}
