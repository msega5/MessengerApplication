using MessengerApplication.Models.DTO;

namespace MessengerApplication.Abstraction
{
    public interface IUserRepository
    {
        public void AddUser(UserDTO user);
        public bool Exists(string email);
    }
}
