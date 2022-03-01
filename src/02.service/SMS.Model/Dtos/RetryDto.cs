using SMS.Base.Enums;

namespace SMS.Model.Dtos
{
    public class RetryDto
    {
        public InheritTypeEnum? InheritType { get; set; }

        public bool Enable { get; set; } = true;

        public int? RetryCount { get; set; }
    }
}