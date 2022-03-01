using System;

namespace BC.CSM.Response.User
{
    public class SearchAadUserResponse
    {
        public Guid Id { get; set; }
        public string Mail { get; set; }
        public string DisplayName { get; set; }
    }
}