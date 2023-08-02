using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Torc.Assesment.Api.Model;
using Torc.Assesment.Dal;

namespace Torc.Assesment.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    [EnableCors]
    public class SecurityController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly IUserRepository _userRepository;


        public SecurityController(IConfiguration config, IUserRepository userRepository)
        {
            _config = config;
            _userRepository = userRepository;

        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> CreateToken([FromBody] User user)
        {
            var authUser = await _userRepository.AuthenticateUser(user.Username, user.Password);

            if (authUser != null)
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
                        new Claim("Id", authUser.Id.ToString()),
                        new Claim(ClaimTypes.Role, authUser.Role), // Extras - Add Role to JWT
                        new Claim(JwtRegisteredClaimNames.Sub, user.Username),
                        new Claim(JwtRegisteredClaimNames.Email, user.Username),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
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
                var stringToken = tokenHandler.WriteToken(token);
                Log.Information($"Authorization success for user {user.Username} role {authUser.Role}. JWT Token generated.");
                var loggedUser = new LoggedUser { Id = authUser.Id, Username = authUser.Username, Role = authUser.Role, Token = stringToken };

                return Ok(loggedUser);
            }

            Log.Warning($"Authorization failed for user: {user.Username}");
            return Unauthorized();
        }

    }
}
