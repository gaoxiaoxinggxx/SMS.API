using Commen.Helper;
using Common.VNextFramework.EntityFramework;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;
using SMS.Data.Entitys;
using SMS.Model.Request.User;
using SMS.Model.Response.User;
using SMS.Service.Hubs;
using SMS.Service.Interfaces;
using System;
using System.Threading.Tasks;

namespace SMS.Service.Implement
{
    public class UserService : BaseService , IUserService
    {
        private readonly IEFAsyncRepository<User> _userUserRepository;
        private readonly IHubContext<ChatHub> _charHubContext;

        public UserService(IHttpContextAccessor httpContextAccessor, IEFAsyncRepository<User> userUserRepository, IHubContext<ChatHub> charHubContext) : base(httpContextAccessor)
        {
            _userUserRepository = userUserRepository;
            _charHubContext = charHubContext;
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
            // notify examiner front end
            await _charHubContext.Clients.User(UserId.ToString()).SendCoreAsync("candidateDisconnected", new object[] { "user" });
            await _charHubContext.Clients.All.SendCoreAsync("candidateDisconnected", new object[] { "all" });

            return new CurrentUserResponse() 
            {
                Name = user.Name,
                Email = user.Email,
                Role = user.Role
            };
        }
    }
}
