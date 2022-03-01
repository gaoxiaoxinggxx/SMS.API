using System;
using System.Collections.Generic;
using SMS.Data.Entities;
using SMS.Model.Dtos;
using SMS.Model.Dtos;

namespace BC.CSM.Response.Application
{
    public class GetApplicationResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }

        public List<AuthInfoDto> AuthInfos { get; set; }
    }
}
