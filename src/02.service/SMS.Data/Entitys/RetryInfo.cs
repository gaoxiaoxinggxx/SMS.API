using Commen.EntityFramworkCore;
using SMS.Base.Enums;

namespace SMS.Data.Entitys
{
    public class RetryInfo : SequentialGuidEntity
    {
        public InheritTypeEnum? InheritType { get; set; }

        public bool Enable { get; set; }

        public int? RetryCount { get; set; }
    }
}
