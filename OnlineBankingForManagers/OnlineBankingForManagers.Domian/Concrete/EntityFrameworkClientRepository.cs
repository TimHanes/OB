using System.Linq;
using log4net;
using OnlineBankingForManagers.Domain.Abstract;
using OnlineBankingForManagers.Domain.Components;
using OnlineBankingForManagers.Domain.Models;
using OnlineBankingForManagers.Domain.Personages;

namespace OnlineBankingForManagers.Domain.Concrete
{
    public class EntityFrameworkClientRepository : IClientRepository
    {
      private EntityFrameworkDbContext context = new EntityFrameworkDbContext();
      readonly ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
      public IQueryable<Client> Clients
      {
          get { return context.Clients; }
      }
      public DbResultType SaveClient(Client client)
      {
          if (client.ClientId == 0)
          {
              if (context.Clients.FirstOrDefault(p => p.ContractNumber == client.ContractNumber) != null)
                  return DbResultType.ContractNumberIsOccupied;
              if ((context.Clients.FirstOrDefault(p => (p.FirstName == client.FirstName) &
                  (p.LastName == client.LastName)) != null))
                  return DbResultType.NameIsOccupied;
              if (context.Clients.FirstOrDefault(p => p.PhoneNumber == client.PhoneNumber) != null)
                  return DbResultType.PhoneIsOccupied;              
              context.Clients.Add(client);
          }
          else
          {
              Client dbEntry = context.Clients.Find(client.ClientId);
              if (dbEntry != null)
              {
                  dbEntry.ContractNumber = client.ContractNumber;
                  dbEntry.FirstName = client.FirstName;
                  dbEntry.LastName = client.LastName;
                  dbEntry.DateOfBirth = client.DateOfBirth;
                  dbEntry.PhoneNumber = client.PhoneNumber;
                  dbEntry.Status = client.Status;
                  dbEntry.Deposit = client.Deposit;
              }
          }
          try
          {
              context.SaveChanges();
          }
          catch
          {
              logger.Error("Cann't save client " + client.FirstName+" "+client.LastName + " into DB");
              return DbResultType.NotAvailable;

          }
          return DbResultType.Executed;
      }
      public DbResultType DeleteClient(int clientID, ref string name)
      {
          Client dbEntry = context.Clients.Find(clientID);
          name = dbEntry.FirstName + " " + dbEntry.LastName;
          if (dbEntry != null)
          {
              context.Clients.Remove(dbEntry);              
          }
          try
          {
              context.SaveChanges();
          }
          catch
          {
              logger.Error("Cann't delete client " + name + "out of DB");
              return DbResultType.NotAvailable;

          }
          return DbResultType.Executed;
      }
    }
}
