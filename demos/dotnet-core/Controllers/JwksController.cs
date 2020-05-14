using Microsoft.AspNetCore.Mvc;

namespace dotnet_core_api.Controllers
{
    [Route("production/jwks.json")]
    [ApiController]
    public class JwksController : ControllerBase
    {
        [HttpGet()]
        public ActionResult<string> Get()
        {
            var jwks = System.IO.File.ReadAllText("../keys/jwks_public.json");
            return Content(jwks, "application/json");
        }
    }
}
