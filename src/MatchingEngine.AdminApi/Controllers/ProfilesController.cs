using System;
using System.Threading.Tasks;
using MatchingEngine.AdminApi.Models.Profiles;
using MatchingEngine.AdminApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MatchingEngine.AdminApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ProfilesController : ControllerBase
    {
        private readonly IUsersService _usersService;

        public ProfilesController(IUsersService usersService)
        {
            _usersService = usersService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var userId = Guid.Parse(User.Identity.Name);

            var user = await _usersService.GetByIdAsync(userId);

            var response = new ProfileModel
            {
                Id = user.Id, UserName = user.UserName, FullName = user.FullName, Email = user.Email
            };

            return Ok(response);
        }
    }
}
