using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OnlineBankingForManagers.WebUI.Models
{
    public class LoginViewModel
    {
        [Required]
        [StringLength(16)]
        [Display(Name = "Login")]

        public string UserName { get; set; }

        [Required]
        [StringLength(16)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

       [Display(Name = "Remember me")]
        public bool RememberMe { get; set; }
    }
   
} 