using MessengerApplication.Models;

namespace MessengerApplication.Abstraction
{
    public interface IUserAuthenticationService
    {
        User Authenticate(UserLogin model);
    }
}
