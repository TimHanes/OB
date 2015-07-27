using System.Linq;
using OnlineBankingForManagers.Domain.Abstract;
using OnlineBankingForManagers.Domain.Personages;


namespace OnlineBankingForManagers.Domain.Concrete
{
    public class EntityFrameworkUserAuthProvider : IAuthProvider
    {
        private EntityFrameworkDbContext context = new EntityFrameworkDbContext();
        
        public bool CreateUser(User user)
        {
            if (user.UserId == 0)
            {
                context.Users.Add(user);
            }
            else
            {
                User dbEntry = context.Users.Find(user.UserId);
                if (dbEntry != null)
                {
                    dbEntry.Login = user.Login;
                    dbEntry.Address = user.Address;
                    dbEntry.Email = user.Email;
                    dbEntry.Password = user.Password;
                }
            }
            context.SaveChanges();
            return true;
        }
        public User DeleteUser(int userID)
        {
            User dbEntry = context.Users.Find(userID);
            if (dbEntry != null)
            {
                context.Users.Remove(dbEntry);
                context.SaveChanges();
            }
            return dbEntry;
        }

        public bool AuthUser(string login, string password)
        {
            User dbUser = context.Users
                 .FirstOrDefault(p => p.Login == login);

            if ((dbUser != null) && (dbUser.Password == password)) return true;
            else return false;
        }

        public User EditUser(User user)
        {
            return user;
        }
    }
}
