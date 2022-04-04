using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SMS.Base.Attributes;
using SMS.Base.Enums;

namespace SMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BaseApiController : ControllerBase
    {
    }

    [ApiGroup(ApiGroupNameEnum.App)]
    public class AppBaseApiController : BaseApiController
    { 
    }

    [ApiGroup(ApiGroupNameEnum.Other)]
    public class OtherBaseApiController : BaseApiController
    { 
    }
}
