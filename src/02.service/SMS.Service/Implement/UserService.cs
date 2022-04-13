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
using System.Linq;
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

        public async Task<SearchUserResponse> Search(SearchUserRequest req)
        {
            var userSpec = PagedSpecification<User>.GetSpecification(req);
            if (!string.IsNullOrEmpty((req.Name)))
            {
                userSpec.AddPredicate(x=>x.Name.Contains(req.Name));
            }

            if (!string.IsNullOrEmpty(req.Email))
            {
                userSpec.AddPredicate(x=>x.Email.Contains(req.Email));
            }

            var users = await _userUserRepository.GetPagedListAsync(userSpec);
            return new SearchUserResponse
            {
                PageIndex = users.PageIndex,
                PageSize = users.PageSize,
                Total = users.Total,
                Data = users.Data.Select(x => 
                new SearchUserItem
                {
                    Id = x.Id,
                    Name = x.Name,
                    Email = x.Email,
                    Role = x.Role,
                    Status = x.Status,
                    CreatedOn = x.CreatedOn!.Value.ToString("yyyy-MM-dd HH:mm:ss")
                }).ToList()
            };
        }
    }
}
