using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SMS.Base.Attributes;
using SMS.Base.Enums;
using SMS.Model.Request.User;
using SMS.Model.Response.User;
using SMS.Service.Interfaces;
using System.Threading.Tasks;

namespace SMS.API.Controllers
{
    [AllowAnonymous]
    public class AuthController : AppBaseApiController
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        /// <summary>
        /// 鉴权
        /// </summary>
        /// <returns></returns>
        [HttpPost("auth")]
        public Task<UserAuthResponse> Auth(UserAuthRequest req)
        {
            return _authService.Auth(req);
        }

        
    }
}
