using Common.VNextFramework.Model;

namespace SMS.Model.Request.User
{
    public class SearchUserRequest : PageQueryModel
    {
        public string Name { get; set; }
        public string Email { get; set; }
    }
}