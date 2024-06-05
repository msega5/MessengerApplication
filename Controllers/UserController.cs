using MessengerApplication.Abstraction;
using MessengerApplication.Models.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;


namespace MessengerApplication.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private IUserRepository _repository;
        public UserController(IUserRepository repository)
        {
            _repository = repository;
        }

        [AllowAnonymous]
        [HttpPost(template: "UsersList")]
        public ActionResult<IEnumerable<UserDTO>> GetEmail()
        {
            
            return Ok(User);
        }       
    }
}
