using System;
using Common.VNextFramework.Model;
using Microsoft.AspNetCore.Mvc.RazorPages;
using static SMS.Data.Entitys.User;

namespace SMS.Model.Response.User
{
    public class SearchUserResponse : PageResultModel<SearchUserItem>
    {
    }

    public class SearchUserItem
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public UserRoleEnum Role { get; set; }
        public UserStatusEnum Status { get; set; }
        public string CreatedOn { get; set; }
    }
}