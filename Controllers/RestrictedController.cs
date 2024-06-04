using MessengerApplication.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;


namespace MessengerApplication.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RestrictedController : ControllerBase
    {
        [HttpGet]
        [Route("Admins")]
        [Authorize(Roles = "Admin")]
        public IActionResult AdminEndPoint()
        {
            var currentUser = GetCurrentUser();
            return Ok($"Welcome! You're an {currentUser.Role}");
        }

        [HttpGet]
        [Route("Users")]
        [Authorize(Roles = "Admin, User")]
        public IActionResult UserEndPoint()
        {
            var currentUser = GetCurrentUser();
            return Ok($"Welcome! You're an {currentUser.Role}");
        }

        private User GetCurrentUser()
        {
            var identify = HttpContext.User.Identity as ClaimsIdentity;
            if (identify != null)
            {
                var userClaims = identify.Claims;
                return new User
                {
                    Email = userClaims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value,
                    Role = (UserRole)Enum.Parse(typeof(UserRole), userClaims.FirstOrDefault(x => x.Type == ClaimTypes.Role)?.Value)
                };
            }
            return null;
        }
    }
}
