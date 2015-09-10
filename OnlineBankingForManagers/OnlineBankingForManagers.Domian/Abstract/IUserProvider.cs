using System.Linq;
using OnlineBankingForManagers.Domain.Components;
using OnlineBankingForManagers.Domain.Personages;

namespace OnlineBankingForManagers.Domain.Abstract
{
    public interface IUserProvider
    {
        DbResultType Authentification(string login, string password);
        DbResultType Edit(User user);
        DbResultType Delete(int userId);        
        bool UnBlocked(string login);
    }
}