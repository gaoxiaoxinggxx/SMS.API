using SMS.Base.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace SMS.Model.Dtos
{
    public class AuthInfoDto
    {
        public Guid Id { get; set; }
        public AuthTypeEnum AuthType { get; set; }

        public object AuthParameters { get; set; }
    }
}
