using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SMS.Service.Hubs
{
    public class CustomUserIdProvider : IUserIdProvider
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CustomUserIdProvider(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string GetUserId(HubConnectionContext connection)
        {
            if (_httpContextAccessor.HttpContext != null)
            {
                var primaryClaim = _httpContextAccessor.HttpContext.User?.FindFirst(ClaimTypes.PrimarySid);
                if (primaryClaim != null)
                {
                   return primaryClaim.Value;
                }
            }
            return String.Empty;
        }
    }
}
