using Commen.EntityFramworkCore;
using SMS.Base.Enums;
using System;

namespace SMS.Data.Entitys
{
    public class AuthInfo : SequentialGuidEntity
    {
        public AuthTypeEnum AuthType { get; set; }

        public object AuthParameters { get; set; }
        public Guid ApplicationId { get; set; }
    }
}