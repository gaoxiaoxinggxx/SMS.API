using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SMS.Base.Attributes;
using SMS.Base.Enums;

namespace SMS.API.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [ApiGroup(ApiGroupNameEnum.Auth)]
    public class BaseApiController : ControllerBase
    {
    }
}
