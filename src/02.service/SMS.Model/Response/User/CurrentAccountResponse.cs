using static SMS.Data.Entitys.User;

namespace SMS.Model.Response.User
{
    public class CurrentAccountResponse
    {
        public string Name { get; set; }
        public UserRoleEnum Role { get; set; }
    }
}