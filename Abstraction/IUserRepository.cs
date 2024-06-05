using MessengerApplication.Models;

namespace MessengerApplication.Abstraction
{
    public interface IUserRepository
    {
        public void UserAdd(string email, string password, RoleId roleId);
        public RoleId UserCheck(string email,  string password);
        public void UserDelete(string email, string password, RoleId roleId);
    }
}
