using MessengerApplication.Abstraction;
using MessengerApplication.Models;
using MessengerApplication.rsa;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MessengerApplication.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly IUserAuthenticationService _authenticationService;

        public LoginController(IConfiguration config, IUserAuthenticationService service)
        {
            _config = config;
            _authenticationService = service;
        }


        [AllowAnonymous]
        [HttpPost]
        public ActionResult Login([FromBody] UserLogin userLogin)
        {
            var user = _authenticationService.Authenticate(userLogin);
            if (user != null)
            { 
            var token = GenerateToken(user);
                return Ok(token);
            }
            return NotFound("User not found");
        }

        private string GenerateToken(User user)
        {
            //var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var securityKey = new RsaSecurityKey(RSATools.GetPrivateKey());
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.RsaSha256Signature);
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Email),
                new Claim(ClaimTypes.Role, user.Role.ToString())
            };

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
                _config["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(15),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
