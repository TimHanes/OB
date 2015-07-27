using System.Linq;
using OnlineBankingForManagers.Domain.Personages;

namespace OnlineBankingForManagers.Domain.Abstract
{
    public interface IAuthProvider
    {
        bool AuthUser(string login, string password);
        bool CreateUser(User user);
        User DeleteUser(int userId);
        User EditUser(User user);
    }
}