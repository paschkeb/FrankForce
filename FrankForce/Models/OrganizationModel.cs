using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FrankForce.Models
{
    public class OrganizationModel
    {
        [Editable(false)]
        [Display(Name = "Organization ID")]
        public int OrganizationId { get; set; }

        [Display(Name = "Organization Name")]
        [Required(ErrorMessage = "Organization Name required")]
        [StringLength(100, ErrorMessage = "Please limit name to 100 characters.")]
        public string Name { get; set; }

        [Display(Name = "Description")]
        [StringLength(500, ErrorMessage = "Please limit description to 500 characters.")]
        public string Description { get; set; }
    }
}
