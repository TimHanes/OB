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
        [HiddenInput(DisplayValue = false)]
        public int UserId { get; set; }

        [HiddenInput(DisplayValue = false)]
        public int NumWrongPassword { get; set; }

        [Required(ErrorMessage = "Please enter a Login")]
        public string Login { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Please enter a Password")]
        public string Password { get; set; }

       [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "Please enter a Email")]
        public string Email { get; set; }

        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Please enter a Address")]
        public string Address { get; set; }
  
    }
}
