using EBTCO.Core.Api;
using Microsoft.AspNetCore.Mvc;

namespace EBTCO
{
    [ApiController]
    public class BaseController : ControllerBase
    {
        protected ActionResult GetApiResponse<T>(APIResponse<T> obj)
        {
            if (obj.IsSuccessStatusCode)
                return StatusCode(obj.StatusCode, obj.Data);

            return StatusCode(obj.StatusCode, obj.Errors);
        }
    }
}
