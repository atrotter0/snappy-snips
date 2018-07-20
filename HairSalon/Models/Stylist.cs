using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HairSalon;

namespace HairSalon.Models
{
    [Table("Stylists")]
    public class Stylist
    {
        [Key]
        public int StylistId { get; set; }

        [Required(ErrorMessage = "First Name is required.")]
        [StringLength(50, ErrorMessage = "First Name is not valid. Max length is 50 characters.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is required.")]
        [StringLength(75, ErrorMessage = "Last Name is not valid. Max length is 75 characters.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Phone is required.")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Phone number format is not valid.")]
        public string Phone { get; set; }

        public virtual ICollection<Client> Clients { get; set; }
    }
}
