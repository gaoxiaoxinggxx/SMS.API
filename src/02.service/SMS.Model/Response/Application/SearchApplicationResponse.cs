using System;
using System.Collections.Generic;
using SMS.Base.Enums;

namespace BC.CSM.Response.Application
{
    public class SearchApplicationResponse
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public Dictionary<AuthTypeEnum, object> AuthInfos { get; set; }
    }
}