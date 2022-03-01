using System;

namespace SMS.Model.Request.Application
{
    public class ModifyApplicationRequest
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
    }
}