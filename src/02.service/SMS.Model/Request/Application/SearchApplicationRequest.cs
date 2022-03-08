using Common.VNextFramework.Model;

namespace SMS.Model.Request.Application
{
    public class SearchApplicationRequest : PageQueryModel
    {
        public string Name { get; set; }

        public bool With { get; set; }
    }
}