using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace OnlineBankingForManagers.WebUI.Models
{
    public class RegisterViewModel
    {
        private string address;


        [Required]
        [Display(Name = "Login:")]
        public string UserName { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Пароль должен иметь от 6 до 100 символов", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password:")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Repeat Password:")]
        [Compare("Password", ErrorMessage = "Пароли не совпадают.")]
        public string ConfirmPassword { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email:")]
        public string Email { get; set; }

        
        [DataType(DataType.MultilineText)]
        [Display(Name = "Address:")]
        public string Address {
            get { return address; }
            set
            {
                if (value != null) address = value;
                else address = "no address";
            }
        }
    }
}