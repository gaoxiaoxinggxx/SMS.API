using System.Collections.Generic;
using SMS.Model.Response.User;
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
        
        [HttpPost("current")]
        public async Task<CurrentUserResponse> GetCurrentInfo()
        {
            return await _userService.GetCurrentInfo();
        }

        [HttpPost("search")]
        public async Task<SearchUserResponse> Search(SearchUserRequest req)
        {
            return await _userService.Search(req);
        }
    }
}
