using Microsoft.AspNetCore.Mvc;
using dotnet_core_api.Integrations;

namespace dotnet_core_api.Controllers
{
    [Route("production/[controller]")]
    [ApiController]
    public class RequestUriController : ControllerBase
    {
        private IItsmeClient _itsmeClient;
        public RequestUriController(IItsmeClient itsmeClient)
        {
            _itsmeClient = itsmeClient;
        }

        [HttpGet()]
        public ActionResult<string> Get()
        {
            return _itsmeClient.CreateRequestURIPayload();
        }
    }
}
