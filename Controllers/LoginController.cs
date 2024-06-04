using MessengerApplication.Abstraction;
using MessengerApplication.Models;
using MessengerApplication.Repo;
using MessengerApplication.rsa;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
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
        //private readonly IUserAuthenticationService _authenticationService;
        private readonly IUserRepository _userRepository;

        public LoginController(IConfiguration config,
                                //IUserAuthenticationService service,
                                IUserRepository userRepository)
        {
            _config = config;
            //_authenticationService = service;
            _userRepository = userRepository;
        }

        private RoleId RoleIDToRole(RoleId roleId)
        {
            if (roleId == RoleId.Admin) return RoleId.Admin;
            return RoleId.User;
        }


        [AllowAnonymous]
        [HttpPost]
        public ActionResult Login([FromBody] UserLogin userLogin)
        {
            try
            {
                var roleId = _userRepository.UserCheck(userLogin.Email, userLogin.Password);
                var user = new User { Email = userLogin.Email, RoleId = RoleIDToRole(roleId) };
                var token = GenerateToken(user);
                return Ok(token);
            }
            catch (Exception e)
            {
            return StatusCode(500, e.Message);
            }
        }


        [AllowAnonymous]
        [HttpPost]
        [Route("AddAdmin")]
        public ActionResult AddAdmin([FromBody] UserLogin userLogin)
        {
            try
            {
                _userRepository.UserAdd(userLogin.Email, userLogin.Password, RoleId.Admin);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
            return Ok();
        }

        
        
        [HttpPost]
        [Route("AddUser")]
        [Authorize(Roles ="Admin")]
        public ActionResult AddUser([FromBody] UserLogin userLogin)
        {
            try
            {
                _userRepository.UserAdd(userLogin.Email, userLogin.Password, RoleId.User);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
            return Ok();
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
