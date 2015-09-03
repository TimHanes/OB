using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace OnlineBankingForManagers.Domain.Personages
{
   public class User
    {
        public int UserId { get; set; }

        public int NumWrongPassword { get; set; }

        public string Login { get; set; }

        public string Password { get; set; }

     
        public string Email { get; set; }

        public string Address { get; set; }
  
    }
}
