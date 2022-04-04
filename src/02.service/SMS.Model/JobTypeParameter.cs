using SMS.Model.Dtos;
using Swashbuckle.AspNetCore.Annotations;

namespace SMS.Model
{
    [SwaggerSubType(typeof(HttpJobDto))]
    public class JobTypeParameter
    {
    }
}