using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SMS.Service
{
    public class BaseService
    {
        protected Guid UserId;

        private readonly IHttpContextAccessor _httpContextAccessor;
        public BaseService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            Resolve();
        }

        private void Resolve()
        {
            if (_httpContextAccessor.HttpContext != null)
            {
                var primaryClaim = _httpContextAccessor.HttpContext.User?.FindFirst(ClaimTypes.PrimarySid);
                if (primaryClaim != null)
                {
                    UserId = Guid.Parse(primaryClaim.Value);
                }
            }
        }
    }
}
