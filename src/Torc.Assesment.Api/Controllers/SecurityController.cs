using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Torc.Assesment.Api.Model;

namespace Torc.Assesment.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class SecurityController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly ILogger _logger;

        public SecurityController(IConfiguration config, ILogger logger)
        {
            _config = config;
            _logger = logger;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public IActionResult CreateToken([FromBody] User user)
        {
            if (user.Username == "joaoprado" && user.Password == "joaopassword")
            {
                var jwtSection = _config.GetSection("JWT");

                var issuer = jwtSection["Issuer"];
                var audience = jwtSection["Audience"];
                var keyString = jwtSection["Key"];
                var key = Encoding.ASCII.GetBytes(keyString);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new[]
                    {
                        new Claim("Id", Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Sub, user.Username),
                        new Claim(JwtRegisteredClaimNames.Email, user.Username),
                        new Claim(JwtRegisteredClaimNames.Jti,
                        Guid.NewGuid().ToString())
                    }),
                    Expires = DateTime.UtcNow.AddMinutes(5),
                    Issuer = issuer,
                    Audience = audience,
                    SigningCredentials = new SigningCredentials
                    (new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha512Signature)
                };
                var tokenHandler = new JwtSecurityTokenHandler();
                var token = tokenHandler.CreateToken(tokenDescriptor);
                var jwtToken = tokenHandler.WriteToken(token);
                var stringToken = tokenHandler.WriteToken(token);
                _logger.LogInformation($"Authorization success for user {user.Username}. JWT Token generated.");
                return Ok(stringToken);
            }

            _logger.LogWarning($"Authorization failed for user: {user.Username}");
            return Unauthorized();
        }

    }
}
