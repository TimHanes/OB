using System;
using System.Linq;
using System.Net;
using System.Security.Principal;
using System.Text;
using log4net;
using OnlineBankingForManagers.Domain.Abstract;
using OnlineBankingForManagers.Domain.Components;
using OnlineBankingForManagers.Domain.Handlers;
using OnlineBankingForManagers.Domain.Personages;


namespace OnlineBankingForManagers.Domain.Concrete
{
   
    public class EntityFrameworkUserUserProvider : IUserProvider
    {
        private EntityFrameworkDbContext context = new EntityFrameworkDbContext();
        readonly ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public bool Register(User user)
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

            try
            {
                context.SaveChanges();
            }
            catch
            {
                logger.Error("Cann't save user "+user.Login+" into DB");
                return false;

            }
            return true;
        }
        public User Delete(int userID)
        {
            User dbEntry = context.Users.Find(userID);
            if (dbEntry != null)
            {
                context.Users.Remove(dbEntry);
                context.SaveChanges();
            }
            return dbEntry;
        }

        public VerificationType Authentification(string login, string password)
        {
            User dbUser = context.Users
                 .FirstOrDefault(p => p.Login == login);

            if (dbUser == null) return VerificationType.LoginIncorrect;
            
           

           

            if (dbUser.Password != password)
            {
                dbUser.NumWrongPassword++;
                context.SaveChanges();

                if (dbUser.NumWrongPassword == 5)
                {

                    logger.Warn("Blocked " + dbUser.Login + " account");

                    new Sendler().SendMail("smtp.mail.ru", "timofey-taliya@mail.ru", "Trans220", dbUser.Email,
                        "Online Banking",

                        "Your account is blocked. For unblocked ckick on this link " +
                        "http://localhost:2599/" + "Account/UnBlockedAccount/?login=" + dbUser.Login);                    
                }
                if (dbUser.NumWrongPassword >= 5) return VerificationType.Blocked;
                return VerificationType.PasswordIncorrect;
            }
            
            if (dbUser.NumWrongPassword > 0)
            {
                dbUser.NumWrongPassword = 0;
                context.SaveChanges();  
            }          
            return VerificationType.Executed;
        }

        public User Edit(User user)
        {
            return user;
        }

        public bool UnBlocked(string login)
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
