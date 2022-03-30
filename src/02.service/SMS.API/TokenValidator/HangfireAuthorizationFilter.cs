using Hangfire.Annotations;
using Hangfire.Dashboard;

namespace SMS.API.TokenValidator
{
    public class HangfireAuthorizationFilter : IDashboardAuthorizationFilter
    {
        public bool Authorize([NotNull] DashboardContext context)
        {
            return true;
        }
    }
}
