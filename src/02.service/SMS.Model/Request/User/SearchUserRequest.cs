using BCChina.VNextFramework.Model;

namespace SMS.Model.Request.User
{
    public class SearchUserRequest : PageQueryModel
    {
        public string Keyword { get; set; }
    }
}