using SMS.Model.Request.User;
using SMS.Model.Response.User;
using System.Threading.Tasks;

namespace SMS.Service.Interfaces
{
    public interface IAuthService
    {
        Task<UserAuthResponse> Auth(UserAuthRequest req);
    }
}
