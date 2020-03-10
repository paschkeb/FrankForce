using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FrankForce.Models
{
    public class UserModel
    {
        [Display(Name = "User ID")]
        public int UserId { get; set; }
  
        [Display(Name = "Organization ID")]
        [Required]
        public int OrganizationId { get; set; }
        
        [Display(Name = "First Name")]
        [Required]
        [StringLength(50, ErrorMessage = "First Name cannot be longer than 50 characters.")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [Required]
        [StringLength(50, ErrorMessage = "Last Name cannot be longer than 50 characters.")]
        public string LastName { get; set; }

        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email Address")]
        [Required]
        [StringLength(320, ErrorMessage = "An Email address cannot be longer than 320 characters.")]
        public string EmailAddress { get; set; }

        [Display(Name = "Confirm Email")]
        [Compare("EmailAddress", ErrorMessage = "Make sure your email address matches")]
        public string ConfirmEmail { get; set; }

        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Phone Number")]
        [Required]
        public string PhoneNumber { get; set; }
    }
}
