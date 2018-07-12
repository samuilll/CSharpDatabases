
namespace P01_HospitalDatabase.Data
{
    using Models;
    using Microsoft.EntityFrameworkCore;
    public class HospitalContext : DbContext
    {
        public HospitalContext()
        {

        }

        public HospitalContext(DbContextOptions options)
            : base(options)
        {

        }

        public DbSet<Patient> Patients { get; set; }

        public DbSet<Visitation> Visitations { get; set; }

        public DbSet<Medicament> Medicaments { get; set; }

        public DbSet<PatientMedicament> PatientsMedicaments { get; set; }

        public DbSet<Diagnose> Diagnoses { get; set; }

        public DbSet<Doctor> Doctors { get; set; }



        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            base.OnConfiguring(builder);

            if (!builder.IsConfigured)
            {
                builder.UseSqlServer(Configuration.ConnectionbString);
            }
        }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Patient>(e =>
            {
                e.Property("FirstName")
                           .HasMaxLength(50)
                           .IsRequired(true)
                           .IsUnicode(true);

                e.Property("LastName")
                .HasMaxLength(50)
                .IsRequired(true)
                 .IsUnicode(true);

                e.Property("Address")
                .HasMaxLength(250)
                .IsRequired(true)
                 .IsUnicode(true);

                e.Property("Email")
                .HasMaxLength(80)
                .IsRequired(true)
                  .IsUnicode(true);
            });



            builder.Entity<Visitation>(e =>
            {
                e.Property("Comments")
                        .HasMaxLength(250)
                           .IsUnicode(true);

                e.HasOne(v => v.Patient)
                .WithMany(p => p.Visitations);

                e.Property(v => v.DoctorId)
                .IsRequired(false);

                e.HasOne(v => v.Doctor)
                .WithMany(d => d.Visitations)
                .HasForeignKey(v => v.DoctorId);

            });


            builder.Entity<Diagnose>(dg =>
            {
                dg.Property("Name")
                         .HasMaxLength(50);

                dg.Property("Comments")
                .HasMaxLength(250);

                dg.HasOne(d => d.Patient)
                 .WithMany(p => p.Diagnoses);
            });

            builder.Entity<Doctor>(e =>
            {
                e.HasKey(d => d.DoctorId);

                e.Property(d=>d.Name)
                .IsRequired(true)
                .HasMaxLength(100)
                .IsUnicode(true);

                e.Property(d => d.Specialty)
                .IsRequired(true)
                .HasMaxLength(100)
                .IsUnicode(true);

            }
                );



            builder.Entity<Medicament>(med =>
            {
                med.Property("Name")
                      .HasMaxLength(50);

                med.ToTable("Medicaments");
            });


            builder.Entity<PatientMedicament>(pm =>
            {
                pm.HasKey(e => new { e.PatientId, e.MedicamentId });

                pm.HasOne(e => e.Patient)
                .WithMany(p => p.Prescriptions);

                pm.HasOne(e => e.Medicament)
                 .WithMany(m => m.Prescriptions);
            });

        }


    }
}
