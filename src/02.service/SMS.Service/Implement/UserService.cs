using Commen.Helper;
using Commen.Model.Exceptions;
using Common.VNextFramework.EntityFramework;
using Microsoft.AspNetCore.Http;
using SMS.Base;
using SMS.Data.Entitys;
using SMS.Model.Request.User;
using SMS.Model.Response.User;
using SMS.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SMS.Service.Implement
{
    public class UserService : BaseService , IUserService
    {
        private readonly IEFAsyncRepository<User> _userUserRepository;

        public UserService(IHttpContextAccessor httpContextAccessor, IEFAsyncRepository<User> userUserRepository) : base(httpContextAccessor)
        {
            _userUserRepository = userUserRepository;
        }

        public async Task<bool> CreateUser(CreateAdminUserRequest req)
        {
            var user = new User() 
            {
                Name = req.Name,
                Email = req.Email,
                Password = MD5Helper.MD5EncodingOnly(req.PassWord),
                Role = req.Role,
                Status = req.Status
            };
            await _userUserRepository.AddAsync(user);
            return await _userUserRepository.SaveChangeAsync();
        }

        public async Task<CurrentUserResponse> GetCurrentInfo()
        {
            var user = await _userUserRepository.GetSingleAsync(x => x.Id == UserId);
            if (user == null)
            {
                throw new Exception("user does not exist.");
            }
            return new CurrentUserResponse() 
            {
                Name = user.Name,
                Email = user.Email,
                Role = user.Role
            };
        }
    }
}
