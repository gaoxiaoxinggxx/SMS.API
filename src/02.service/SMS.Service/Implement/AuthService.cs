using Commen.Helper;
using Commen.Model.Exceptions;
using Common.VNextFramework.EntityFramework;
using Microsoft.AspNetCore.SignalR;
using SMS.Base;
using SMS.Data.Entitys;
using SMS.Model.Request.User;
using SMS.Model.Response.User;
using SMS.Service.Hubs;
using SMS.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SMS.Service.Implement
{
    public class AuthService : IAuthService
    {
        private readonly IEFAsyncRepository<User> _userUserRepository;
        private readonly IAppSettings _appSettings;
        private readonly IHubContext<ChatHub> _charHubContext;

        public AuthService(IEFAsyncRepository<User> userUserRepository, IAppSettings appSettings, IHubContext<ChatHub> charHubContext)
        {
            _userUserRepository = userUserRepository;
            _appSettings = appSettings;
            _charHubContext = charHubContext;
        }

        public async Task<UserAuthResponse> Auth(UserAuthRequest req)
        {
            var password = MD5Helper.MD5EncodingOnly(req.Password);
            var user = await _userUserRepository.GetSingleAsync(x => x.Name == req.Name && x.Password == password);
            if (user == null)
            {
                throw UserAuthException.UserNameOrPasswordNotMatch;
            }

            var identityData = new Dictionary<string, string>
            {
                { ClaimTypes.PrimarySid , user.Id.ToString() },
                { ClaimTypes.NameIdentifier , user.Id.ToString() }
            };

            var clientAuthConfig = _appSettings.AppClientAuthConfig;

            // notify examiner front end
            await _charHubContext.Clients.User("gaoxiaoxing").SendCoreAsync("candidateDisconnected", new object[] { "高小星" });

            return new UserAuthResponse()
            {
                Data = "success",
                Token = TokenHelper.GenerateToken(identityData, clientAuthConfig.Secret, clientAuthConfig.Issuer, Convert.ToInt32(clientAuthConfig.DefaultExpiredMinutes))
            };
        }
    }
}
