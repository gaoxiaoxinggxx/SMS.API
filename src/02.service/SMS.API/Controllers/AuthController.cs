using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SMS.Base.Attributes;
using SMS.Base.Enums;
using SMS.Model.Request.User;
using SMS.Service.Interfaces;
using System.Threading.Tasks;

namespace SMS.API.Controllers
{
    public class AuthController : AppBaseApiController
    {
        /// <summary>
        /// 鉴权
        /// </summary>
        /// <returns></returns>
        [HttpPost("auth")]
        public async Task<bool> Auth()
        {
            return await Task.FromResult(true);
        }
    }
}
