using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Entity_Framework_Test
{
    
    public class Ascribe
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        public virtual Reception Reception { get; set; }

        [StringLength(20)]
        public string Drug { get; set; }

        [StringLength(120)]
        public string Application_Method { get; set; }

        [StringLength(70)]
        public string Contraindication { get; set; }

        [Column(TypeName = "money")]
        public decimal? Price { get; set; }

        public int? ProductCount { get; set; }

        
    }
}
