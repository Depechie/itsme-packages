using Microsoft.AspNetCore.Mvc;
using dotnet_core_api.Integrations;

namespace dotnet_core_api.Controllers
{
    [Route("production/[controller]")]
    [ApiController]
    public class RedirectController : ControllerBase
    {
        private IItsmeClient _itsmeClient;
        public RedirectController(IItsmeClient itsmeClient)
        {
            _itsmeClient = itsmeClient;
        }

        [HttpGet()]
        public ActionResult<Itsme.User> Get([FromQuery(Name = "code")] string code, [FromQuery(Name = "state")] string state)
        {
            return _itsmeClient.GetUserDetails(code, state);
        }
    }
}
