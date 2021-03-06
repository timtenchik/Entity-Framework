// <auto-generated />
using System;
using Entity_Framework_Test;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Entity_Framework_Test.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    [Migration("20211103224028_ThirdMigration")]
    partial class ThirdMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.11")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Entity_Framework_Test.Ascribe", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<string>("Application_Method")
                        .HasMaxLength(120)
                        .HasColumnType("nvarchar(120)");

                    b.Property<string>("Contraindication")
                        .HasMaxLength(70)
                        .HasColumnType("nvarchar(70)");

                    b.Property<string>("Drug")
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<decimal?>("Price")
                        .HasColumnType("money");

                    b.Property<int?>("ProductCount")
                        .HasColumnType("int");

                    b.Property<int?>("ReceptionId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ReceptionId");

                    b.ToTable("Ascribes");
                });

            modelBuilder.Entity("Entity_Framework_Test.Diagnosis", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<string>("Disease")
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.HasKey("Id");

                    b.ToTable("Diagnosis");
                });

            modelBuilder.Entity("Entity_Framework_Test.Doctor", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<string>("Address")
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<DateTime?>("Birthday")
                        .HasColumnType("date");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("Phone")
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<int?>("SpecializationId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("SpecializationId");

                    b.ToTable("Doctors");
                });

            modelBuilder.Entity("Entity_Framework_Test.Patient", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<DateTime?>("Birthday")
                        .HasColumnType("date");

                    b.Property<string>("Email")
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("Phone")
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("Id");

                    b.ToTable("Patients");
                });

            modelBuilder.Entity("Entity_Framework_Test.Reception", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<DateTime?>("Date")
                        .HasColumnType("datetime2");

                    b.Property<int>("DiagnosisId")
                        .HasColumnType("int");

                    b.Property<int>("DoctorId")
                        .HasColumnType("int");

                    b.Property<int>("PatientId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("DiagnosisId");

                    b.HasIndex("DoctorId");

                    b.HasIndex("PatientId");

                    b.ToTable("Receptions");
                });

            modelBuilder.Entity("Entity_Framework_Test.Specialization", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<string>("Specialize")
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.HasKey("Id");

                    b.ToTable("Specializations");
                });

            modelBuilder.Entity("Entity_Framework_Test.Ascribe", b =>
                {
                    b.HasOne("Entity_Framework_Test.Reception", "Reception")
                        .WithMany("Ascribes")
                        .HasForeignKey("ReceptionId");

                    b.Navigation("Reception");
                });

            modelBuilder.Entity("Entity_Framework_Test.Doctor", b =>
                {
                    b.HasOne("Entity_Framework_Test.Specialization", "Specialization")
                        .WithMany("Doctors")
                        .HasForeignKey("SpecializationId");

                    b.Navigation("Specialization");
                });

            modelBuilder.Entity("Entity_Framework_Test.Reception", b =>
                {
                    b.HasOne("Entity_Framework_Test.Diagnosis", "Diagnos")
                        .WithMany("Receptions")
                        .HasForeignKey("DiagnosisId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Entity_Framework_Test.Doctor", "Doctor")
                        .WithMany("Receptions")
                        .HasForeignKey("DoctorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Entity_Framework_Test.Patient", "Patient")
                        .WithMany("Receptions")
                        .HasForeignKey("PatientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Diagnos");

                    b.Navigation("Doctor");

                    b.Navigation("Patient");
                });

            modelBuilder.Entity("Entity_Framework_Test.Diagnosis", b =>
                {
                    b.Navigation("Receptions");
                });

            modelBuilder.Entity("Entity_Framework_Test.Doctor", b =>
                {
                    b.Navigation("Receptions");
                });

            modelBuilder.Entity("Entity_Framework_Test.Patient", b =>
                {
                    b.Navigation("Receptions");
                });

            modelBuilder.Entity("Entity_Framework_Test.Reception", b =>
                {
                    b.Navigation("Ascribes");
                });

            modelBuilder.Entity("Entity_Framework_Test.Specialization", b =>
                {
                    b.Navigation("Doctors");
                });
#pragma warning restore 612, 618
        }
    }
}
