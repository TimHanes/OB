using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;
using OnlineBankingForManagers.Domain.Attributes;
using OnlineBankingForManagers.Domain.Components;
using OnlineBankingForManagers.Domain.Models;

namespace OnlineBankingForManagers.Domain.Models
{
    public class Client
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [HiddenInput(DisplayValue = false)]
        public int ClientId { get; set; }
     
        [Range(1,99999999)]
        [Required(ErrorMessage = "Please enter a Client Contract Number")]         
        public int? ContractNumber { get; set; }

        [StringLength(16, ErrorMessage = "The Firstname must has not more 16 characters")]
        [Required(ErrorMessage = "Please enter a Firstname")]
         public string FirstName { get; set; }

        [StringLength(16, ErrorMessage = "The Lastname must has not more 16 characters")]
        [Required(ErrorMessage = "Please enter a Lastname")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Please enter a Date of Birth ")]      
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DateTimeRange(100, ErrorMessage = "Value for 'Date of birth' must be between {1:d} and {2:d}")]
        public DateTime DateOfBirth { get; set; }

        [StringLength(15, ErrorMessage = "The Phone Number must has not more 10 characters")]
        [Required(ErrorMessage = "Please enter a Phone Number ")]
        [DisplayFormat(DataFormatString = "{0:(###) ###-##-##}", ApplyFormatInEditMode = true)]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

        [Display(Name = "Status")]
        [Required(ErrorMessage = "Please enter a Status")]
        [EnumDataType(typeof(StatusType), ErrorMessage = "Please choose Status")]
        public StatusType Status { get; set; }

        public bool Deposit  { get; set; }
  
    }
}
