using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HairSalon.Models
{
    [Table("Specialties")]
    public class Specialty
    {
        [Key]
        public int SpecialtyId { get; set; }

        [Required(ErrorMessage = "Type is required.")]
        [StringLength(80, ErrorMessage = "Type is not valid. Max length is 80 characters.")]
        public string Name { get; set; }

        public virtual ICollection<StylistSpecialty> StylistsSpecialties { get; set; }
    }
}