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
        [StringLength(16, ErrorMessage = "The Login must have 8 to 16 characters", MinimumLength = 8)]
        public string UserName { get; set; }

       
        [Required]
        [StringLength(16, ErrorMessage = "The password must have 8 to 16 characters", MinimumLength = 8)]
        [DataType(DataType.Password)]
        [Display(Name = "Password:")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Repeat Password:")]
        [Compare("Password", ErrorMessage = "Passwords do not match")]
        public string ConfirmPassword { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email:")]
        public string Email { get; set; }

        
        [DataType(DataType.MultilineText)]
        [Display(Name = "Address:")]
        public string Address {
            get { return address; }
            set {
                address = value ?? "no address";
            }
        }
    }
}