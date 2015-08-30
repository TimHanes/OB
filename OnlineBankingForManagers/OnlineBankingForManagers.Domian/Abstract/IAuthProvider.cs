using System.Linq;
using OnlineBankingForManagers.Domain.Components;
using OnlineBankingForManagers.Domain.Personages;

namespace OnlineBankingForManagers.Domain.Abstract
{
    public interface IAuthProvider
    {
        VerificationType AuthUser(string login, string password);
        bool CreateUser(User user);
        User DeleteUser(int userId);
        User EditUser(User user);
        bool UnBlockedUser(string login);
    }
}