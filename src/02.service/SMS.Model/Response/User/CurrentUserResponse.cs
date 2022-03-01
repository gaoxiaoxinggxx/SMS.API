using static SMS.Data.Entitys.User;

namespace BC.CSM.Response.User
{
    public class CurrentUserResponse
    {
        public string Name { get; set; }

        public string Email { get; set; }

        public UserRoleEnum Role { get; set; }
    }
}