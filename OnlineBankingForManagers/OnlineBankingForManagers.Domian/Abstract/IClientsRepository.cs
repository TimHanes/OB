using System.Linq;
using OnlineBankingForManagers.Domain.Models;
using OnlineBankingForManagers.Domain.Personages;

namespace OnlineBankingForManagers.Domain.Abstract
{
    public interface IClientRepository
    {
        IQueryable<Client> Clients { get; }
        void SaveClient(Client client);
        Client DeleteClient(int clientId);
    }
}
