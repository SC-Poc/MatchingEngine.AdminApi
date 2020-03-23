using System.Net;
using Lykke.Common;
using Lykke.Common.Api.Contract.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MatchingEngine.AdminApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IsAliveController : Controller
    {
        /// <summary>
        /// Checks service is alive
        /// </summary>
        [HttpGet]
        [AllowAnonymous]
        [ProducesResponseType(typeof(IsAliveResponse), (int)HttpStatusCode.OK)]
        public IActionResult Get()
        {
            return Ok(new IsAliveResponse
            {
                Name = AppEnvironment.Name,
                Version = AppEnvironment.Version,
                Env = AppEnvironment.EnvInfo,
                IssueIndicators = new IsAliveResponse.IssueIndicator[] {},
#if DEBUG
                IsDebug = true,
#else
                IsDebug = false,
#endif
            });
        }
    }
}
