using System.Linq;
using OnlineBankingForManagers.Domain.Abstract;
using OnlineBankingForManagers.Domain.Personages;

namespace OnlineBankingForManagers.Domain.Concrete
{
    public class EntityFrameworkClientRepository : IClientRepository
    {
      private EntityFrameworkDbContext context = new EntityFrameworkDbContext();
      public IQueryable<Client> Clients
      {
          get { return context.Clients; }
      }
      public void SaveClient(Client client)
      {
          if (client.ClientId == 0)
          {
              context.Clients.Add(client);
          }
          else
          {
              Client dbEntry = context.Clients.Find(client.ClientId);
              if (dbEntry != null)
              {
                  dbEntry.ContractNumber = client.ContractNumber;
                  dbEntry.Firstname = client.Firstname;
                  dbEntry.Lastname = client.Lastname;
                  dbEntry.DateOfBirth = client.DateOfBirth;
                  dbEntry.PhoneNumber = client.PhoneNumber;
                  dbEntry.Status = client.Status;
                  dbEntry.Deposit = client.Deposit;
              }
          }
          context.SaveChanges();
      }
      public Client DeleteClient(int clientID)
      {
          Client dbEntry = context.Clients.Find(clientID);
          if (dbEntry != null)
          {
              context.Clients.Remove(dbEntry);
              context.SaveChanges();
          }
          return dbEntry;
      }
    }
}
