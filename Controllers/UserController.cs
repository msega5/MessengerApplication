using MessengerApplication.Abstraction;
using MessengerApplication.Models.DTO;
using Microsoft.AspNetCore.Mvc;


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

        [HttpPost(template: "UsersList")]
        public ActionResult<IEnumerable<UserDTO>> GetEmail()
        {
            return Ok(User);
        }       
    }
}
