using System.Data.Entity;
using OnlineBankingForManagers.Domain.Personages;

namespace OnlineBankingForManagers.Domain.Concrete
{
    class EntityFrameworkDbContext : DbContext
    {
        public DbSet<Client> Clients { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
