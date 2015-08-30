using System;
using System.Linq;
using System.Net;
using System.Security.Principal;
using System.Text;
using OnlineBankingForManagers.Domain.Abstract;
using OnlineBankingForManagers.Domain.Components;
using OnlineBankingForManagers.Domain.Handlers;
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

        public VerificationType AuthUser(string login, string password)
        {
            User dbUser = context.Users
                 .FirstOrDefault(p => p.Login == login);

            if (dbUser == null) return VerificationType.LoginIncorrect;
            
            if (dbUser.NumWrongPassword > 5)
            {
                if (dbUser.Password == password) return VerificationType.UnBlocked;

                new Sendler().SendMail("smtp.mail.ru", "timofey-taliya@mail.ru", "Trans220", dbUser.Email,
                    "Online Banking",

                    "Your account is blocked. For unblocked ckick on this link " +
                    "http://localhost:2599/" + "Account/UnBlockedAccount/?login=" + dbUser.Login);
                return VerificationType.Blocked;
            }

            if (dbUser.Password != password)
            {
                dbUser.NumWrongPassword++;
                context.SaveChanges();
                return VerificationType.PasswordIncorrect;
            }
            if (dbUser.NumWrongPassword > 0)
            {
                dbUser.NumWrongPassword = 0;
                context.SaveChanges();  
            }          
            return VerificationType.Executed;
        }

        public User EditUser(User user)
        {
            return user;
        }

        public bool UnBlockedUser(string login)
        {
            User dbUser = context.Users
                 .FirstOrDefault(p => p.Login == login);

            if (dbUser == null) return false;
            dbUser.NumWrongPassword = 0;
            context.SaveChanges();
            return true;
        }

    }
}
