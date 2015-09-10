using System.Linq;
using OnlineBankingForManagers.Domain.Components;
using OnlineBankingForManagers.Domain.Models;


namespace OnlineBankingForManagers.Domain.Abstract
{
    public interface IClientRepository
    {
        IQueryable<Client> Clients { get; }
        DbResultType SaveClient(Client client);
        DbResultType DeleteClient(int clientId, ref string name);
    }
}
