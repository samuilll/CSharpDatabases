namespace PetClinic.Data
{
    using Microsoft.EntityFrameworkCore;
    using PetClinic.Models;

    public class PetClinicContext : DbContext
    {

        public DbSet<Animal> Animals { get; set; }

        public DbSet<AnimalAid> AnimalAids { get; set; }

        public DbSet<Passport> Passports { get; set; }

        public DbSet<Procedure> Procedures { get; set; }

        public DbSet<ProcedureAnimalAid> ProceduresAnimalAids { get; set; }

        public DbSet<Vet> Vets { get; set; }
        public PetClinicContext() { }

        public PetClinicContext(DbContextOptions options)
            :base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(Configuration.ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Animal>
                (e => 
                {
                    e.HasOne(a => a.Passport)
                    .WithOne(p => p.Animal)
                    .HasForeignKey<Animal>(a => a.PassportSerialNumber)
                    .OnDelete(DeleteBehavior.Restrict);
                }
                );

            builder.Entity<Passport>
               (e =>
               {
                   e.HasKey(p => p.SerialNumber);

                   e.Property(p => p.SerialNumber)
                    .HasColumnType("CHAR(10)");
               }
               );

            builder.Entity<Vet>
             (e =>
             {
                 e.HasAlternateKey(v => v.PhoneNumber);
             }
             );

            builder.Entity<Procedure>
            (e =>
            {
                e.HasOne(p => p.Animal)
                .WithMany(a => a.Procedures)
                .HasForeignKey(p => p.AnimalId);

                e.HasOne(p => p.Vet)
               .WithMany(v => v.Procedures)
               .HasForeignKey(p => p.VetId);
            }
            );

            builder.Entity<ProcedureAnimalAid>
          (e =>
          {
              e.HasKey(paa=>new {paa.AnimalAidId,paa.ProcedureId});

              e.HasOne(pa => pa.AnimalAid)
              .WithMany(aa => aa.AnimalAidProcedures)
              .HasForeignKey(pa => pa.AnimalAidId);

              e.HasOne(pa => pa.Procedure)
               .WithMany(p => p.ProcedureAnimalAids)
               .HasForeignKey(pa => pa.ProcedureId);
          }
          );

            builder.Entity<AnimalAid>
        (e =>
        {
            e.HasAlternateKey(aa =>aa.Name);
        }
        );
        }
    }
}
