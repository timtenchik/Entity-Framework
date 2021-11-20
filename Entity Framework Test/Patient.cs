using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Entity_Framework_Test
{
    public class Patient
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)] //Primary key not identity
        public int Id { get; set; }

        [Required]
        [StringLength(20)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(20)]
        public string LastName { get; set; }

        [Required]
        [StringLength(30)]
        public string Address { get; set; }

        [StringLength(30)]
        public string Email { get; set; }

        [StringLength(20)]
        public string Phone { get; set; }

        [Column(TypeName = "date")]
        public DateTime? Birthday { get; set; }

        public virtual ICollection<Reception> Receptions { get; set; }
    }
}
