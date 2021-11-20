using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Entity_Framework_Test
{
    public class Reception
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        public int PatientId { get; set; }

        public int DoctorId { get; set; }

        public int DiagnosisId { get; set; }

        public DateTime? Date { get; set; }

       public virtual ICollection<Ascribe> Ascribes { get; set; }

        public virtual Diagnosis Diagnos { get; set; }

        public virtual Doctor Doctor { get; set; }

        public virtual Patient Patient { get; set; }
    }
}
