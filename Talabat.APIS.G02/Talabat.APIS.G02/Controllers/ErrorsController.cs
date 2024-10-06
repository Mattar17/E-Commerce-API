using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.APIS.G02.Errors;

namespace Talabat.APIS.G02.Controllers
{
    [Route("errors/{code}")]
    [ApiController]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ErrorsController : ControllerBase
    {

        public ActionResult Error(int code)
        {
            return NotFound(new ApiResponse(code));
        }
    }
}
