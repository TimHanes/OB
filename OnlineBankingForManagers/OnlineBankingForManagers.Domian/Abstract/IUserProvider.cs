using System.Linq;
using OnlineBankingForManagers.Domain.Components;
using OnlineBankingForManagers.Domain.Personages;

namespace OnlineBankingForManagers.Domain.Abstract
{
    public interface IUserProvider
    {
        VerificationType Authentification(string login, string password);
        bool Register(User user);
        User Delete(int userId);
        User Edit(User user);
        bool UnBlocked(string login);
    }
}