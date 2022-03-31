using static SMS.Data.Entitys.User;

namespace SMS.Model.Request.User
{
    public class CreateAdminUserRequest
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public UserStatusEnum Status { get; set; }
        public UserRoleEnum Role { get; set; }
        public string PassWord { get; set; }
    }
}