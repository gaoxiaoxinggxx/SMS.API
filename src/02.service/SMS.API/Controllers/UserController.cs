using SMS.Model.Response.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SMS.Model.Request.User;
using SMS.Service.Interfaces;
using System.Threading.Tasks;

namespace SMS.API.Controllers
{
    public class UserController : AppBaseApiController
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// 创建用户
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpPost("create")]
        public async Task<bool> CreateUser(CreateAdminUserRequest req)
        {
            return await _userService.CreateUser(req);
        }

        /// <summary>
        /// 获取当前用户信息
        /// </summary>
        /// <returns></returns>
        [HttpPost("current")]
        public async Task<CurrentUserResponse> GetCurrentInfo()
        {
            return await _userService.GetCurrentInfo();
        }
    }
}
