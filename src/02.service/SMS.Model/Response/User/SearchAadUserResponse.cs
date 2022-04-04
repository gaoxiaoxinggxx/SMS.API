using System;

namespace SMS.Model.Response.User
{
    public class SearchAadUserResponse
    {
        public Guid Id { get; set; }
        public string Mail { get; set; }
        public string DisplayName { get; set; }
    }
}