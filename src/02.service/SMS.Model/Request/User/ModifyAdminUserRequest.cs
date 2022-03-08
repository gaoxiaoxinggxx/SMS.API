using System;
using static SMS.Data.Entitys.User;

namespace SMS.Model.Request.User
{
    public class ModifyAdminUserRequest
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public UserStatusEnum Status { get; set; }
        public UserRoleEnum Role { get; set; }
    }
}