using System;
using static SMS.Data.Entitys.User;

namespace SMS.Model.Response.User
{
    public class SearchUserItemResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public UserStatusEnum Status { get; set; }
        public UserRoleEnum Role { get; set; }
    }
}