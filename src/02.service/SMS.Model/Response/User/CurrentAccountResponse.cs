using static SMS.Data.Entitys.User;

namespace BC.CSM.Response.User
{
    public class CurrentAccountResponse
    {
        public string Name { get; set; }
        public UserRoleEnum Role { get; set; }
    }
}