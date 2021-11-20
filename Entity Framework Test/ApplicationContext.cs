using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Entity_Framework_Test
{
    public class ApplicationContext : DbContext
    {

        public ApplicationContext()
        {
            //Database.EnsureDeleted();
            //Database.EnsureCreated();

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=DESKTOP-4J1KLEK;Database=Hospital_EF;Trusted_Connection=True;");
            //optionsBuilder.LogTo(System.Console.WriteLine);
        }
        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.HasDbFunction(() => Diapason(default, default));
        //}

        //public IQueryable<Ascribe> Diapason(int min_price, int max_price) => FromExpression(() => Diapason(min_price, max_price));
        public virtual DbSet<Ascribe> Ascribes { get; set; }
        public virtual DbSet<Diagnosis> Diagnosis { get; set; }
        public virtual DbSet<Doctor> Doctors { get; set; }
        public virtual DbSet<Patient> Patients { get; set; }
        public virtual DbSet<Reception> Receptions { get; set; }
        public virtual DbSet<Specialization> Specializations { get; set; }

        
    }
}
