using MessengerApplication.Models;
using MessengerApplication.Models.DTO;

namespace MessengerApplication.Abstraction
{
    public interface IUserRepository
    {
        //public void AddUser(UserDTO user);
        //public bool Exists(string email);
        public void UserAdd(string email, string password, RoleId roleId);
        public RoleId UserCheck(string email,  string password);
    }
}
