using System.Collections.Generic;
using SMS.Model.Request.User;
using SMS.Model.Response.User;
using System.Threading.Tasks;

namespace SMS.Service.Interfaces
{
    public interface IUserService
    {
        Task<bool> CreateUser(CreateAdminUserRequest req);
        Task<CurrentUserResponse> GetCurrentInfo();
        Task<SearchUserResponse> Search(SearchUserRequest req);
    }
}
