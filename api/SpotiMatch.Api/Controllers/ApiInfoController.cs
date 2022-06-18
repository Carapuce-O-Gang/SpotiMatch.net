using Microsoft.AspNetCore.Mvc;

namespace SpotiMatch.Api.Controllers
{
    public class ApiInfo
    {
        public string Name { get; set; }
        public string Version { get; set; }
    }

    public class ApiStatus
    {
        public bool Online { get; set; }
    }

    public class ApiVersion
    {
        public string Version { get; set; }
    }

    [ApiController]
    [Route("api")]
    [Produces("application/json")]
    public class ApiInfoController : ControllerBase
    {
        private readonly string Name = "SpotiMatch";
        private readonly bool Online = true;
        private readonly string Version = "1.0";

        [HttpGet]
        public ActionResult<ApiInfo> Get()
        {
            ApiInfo apiInfo = new ApiInfo() { Name = this.Name, Version = this.Version };

            return Ok(apiInfo);
        }

        [HttpGet]
        [Route("status")]
        public ActionResult<ApiStatus> GetStatus()
        {
            ApiStatus status = new ApiStatus() { Online = this.Online };

            return Ok(status);
        }

        [HttpGet]
        [Route("version")]
        public ActionResult<ApiVersion> GetVersion()
        {
            ApiVersion status = new ApiVersion() { Version = this.Version };

            return Ok(status);
        }
    }
}
