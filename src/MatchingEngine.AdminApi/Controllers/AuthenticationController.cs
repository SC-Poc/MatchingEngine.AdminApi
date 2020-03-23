using System;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Lykke.Common.Api.Contract.Responses;
using MatchingEngine.AdminApi.Configuration;
using MatchingEngine.AdminApi.Models.Authentication;
using MatchingEngine.AdminApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace MatchingEngine.AdminApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api")]
    public class AuthenticationController : ControllerBase
    {
        private readonly AppConfig _configuration;
        private readonly IUsersService _usersService;

        public AuthenticationController(AppConfig configuration, IUsersService usersService)
        {
            _configuration = configuration;
            _usersService = usersService;
        }

        /// <summary>
        /// Validates credentials and creates authentication token.
        /// </summary>
        /// <param name="model">The credentials.</param>
        /// <returns>
        /// 200 - The authentication token.
        /// </returns>
        /// <response code="200">The sing in result.</response>
        /// <response code="401">Unauthorized.</response>
        [AllowAnonymous]
        [HttpPost("sign-in")]
        [ProducesResponseType(typeof(SignInResultModel), (int) HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorResponse), (int) HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> SignInAsync([FromBody] SignInModel model)
        {
            var user = await _usersService.GetByCredentialsAsync(model.Email, model.Password);

            if (user == null)
                return Unauthorized();

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration.AdminApi.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] {new Claim(ClaimTypes.Name, user.Id.ToString())}),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            var response = new SignInResultModel {Token = tokenHandler.WriteToken(token)};

            return Ok(response);
        }
    }
}
