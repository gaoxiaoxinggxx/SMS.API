using Commen.EntityFramworkCore;
using System.Collections.Generic;

namespace SMS.Data.Entitys
{
    public class Application : SequentialGuidEntity
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public ICollection<AuthInfo> AuthInfos { get; set; }
    }
}