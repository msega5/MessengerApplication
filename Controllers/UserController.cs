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

        [HttpPost(template: "AddUser")]
        public ActionResult AddUser(UserDTO user)
        {
            _repository.AddUser(user);

            return Ok();
        }

        [HttpGet(template: "Exists")]
        public ActionResult<bool> Exists(string email)
        {
            return Ok(_repository.Exists(email));
        }
    }
}
