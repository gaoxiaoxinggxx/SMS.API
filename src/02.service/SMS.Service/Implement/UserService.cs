using Commen.Helper;
using Common.VNextFramework.EntityFramework;
using SMS.Data.Entitys;
using SMS.Model.Request.User;
using SMS.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.Service.Implement
{
    public class UserService : IUserService
    {
        private readonly IEFAsyncRepository<User> _userUserRepository;

        public UserService(IEFAsyncRepository<User> userUserRepository)
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
    }
}
