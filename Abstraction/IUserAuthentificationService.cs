using MessengerApplication.Models;

namespace MessengerApplication.Abstraction
{
    public interface IUserAuthentificationService
    {
        User Authenticate(UserLogin model);
    }
}
