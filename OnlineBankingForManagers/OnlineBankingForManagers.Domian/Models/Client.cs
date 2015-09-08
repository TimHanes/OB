using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using OnlineBankingForManagers.Domain.Components;
using OnlineBankingForManagers.Domain.Models;

namespace OnlineBankingForManagers.Domain.Models
{
    public class Client
    {
         [HiddenInput(DisplayValue = false)]
        public int ClientId { get; set; }


        
         [Required(ErrorMessage = "Please enter a Client Contract Number")]
        public string ContractNumber { get; set; }

        
        [Required(ErrorMessage = "Please enter a Firstname")]
         public string FirstName { get; set; }

        [Required(ErrorMessage = "Please enter a Lastname")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Please enter a Date of Birth ")]
        [DataType(DataType.Date)]
        public string DateOfBirth { get; set; }

        [Required(ErrorMessage = "Please enter a Phone Number ")]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

        [Display(Name = "Status")]
        [Required(ErrorMessage = "Please enter a Status")]
        public StatusType Status { get; set; }

       
        public bool Deposit  { get; set; }
  
    }
}
