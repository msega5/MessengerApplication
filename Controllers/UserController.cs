using MessengerApplication.Abstraction;
using MessengerApplication.Models;
using MessengerApplication.Models.DTO;
using MessengerApplication.Repo;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyModel;
using System.ComponentModel.DataAnnotations;


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

        [HttpPost(template: "UsersList")]
        public ActionResult<IEnumerable<UserDTO>> GetEmail()
        {
            return Ok(User);
        }

       
    }
}
