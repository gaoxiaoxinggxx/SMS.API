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


        [HttpPost("create")]
        public async Task<bool> CreateUser(CreateAdminUserRequest req)
        {
            return await _userService.CreateUser(req);
        }
    }
}
