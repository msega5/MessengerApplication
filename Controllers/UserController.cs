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

        //[HttpPost(template: "UserAdd")]
        //public ActionResult UserAdd(UserDTO user)
        //{
        //    _repository.UserAdd(user);

        //    return Ok();
        //}

        //[HttpGet(template: "Exists")]
        //public ActionResult<bool> Exists(string email)
        //{
        //    return Ok(_repository.Exists(email));
        //}
    }
}
