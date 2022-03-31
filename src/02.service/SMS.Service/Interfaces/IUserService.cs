using SMS.Model.Request.User;
using System.Threading.Tasks;

namespace SMS.Service.Interfaces
{
    public interface IUserService
    {
        Task<bool> CreateUser(CreateAdminUserRequest req);
    }
}
