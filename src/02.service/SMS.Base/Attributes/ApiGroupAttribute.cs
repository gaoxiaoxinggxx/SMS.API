using Microsoft.AspNetCore.Mvc.ApiExplorer;
using SMS.Base.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.Base.Attributes
{
    public class ApiGroupAttribute : Attribute, IApiDescriptionGroupNameProvider
    {
        public ApiGroupAttribute(ApiGroupNameEnum groupName)
        {
            GroupName = groupName.ToString();
        }
        public string GroupName { get; }
    }
}
