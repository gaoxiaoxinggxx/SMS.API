using System;
using System.Collections.Generic;
using SMS.Base.Enums;

namespace SMS.Model.Response.Application
{
    public class SearchApplicationResponse
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public Dictionary<AuthTypeEnum, object> AuthInfos { get; set; }
    }
}