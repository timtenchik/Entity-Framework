using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Entity_Framework_Test
{

    public class Diagnosis
    {
        

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [StringLength(30)]
        public string Disease { get; set; }

        public virtual ICollection<Reception> Receptions { get; set; }
    }
}
