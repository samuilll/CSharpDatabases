using Microsoft.EntityFrameworkCore;
using P01_StudentSystem.Data.Models;

namespace P01_StudentSystem.Data
{
    public class StudentSystemContext : DbContext
    {
        public StudentSystemContext()
        {

        }

        public StudentSystemContext(DbContextOptions options)
            :base(options)
        {

        }
        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Homework> HomeworkSubmissions { get; set; }
        public DbSet<Resource> Resources { get; set; }
        public DbSet<StudentCourse> StudentCourses { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(Configuration.ConnectionString);
            }
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {


            builder.Entity<Student>(e =>
            {
                e.Property(s => s.Name)
                .IsUnicode(true)
                .HasMaxLength(100);

                e.Property(s => s.PhoneNumber)
                .IsUnicode(false)
                .HasColumnName("Char(10)");

                e.Property(s => s.Birthday)
                .IsRequired(false);

                e.HasMany(s => s.HomeworkSubmissions)
                                .WithOne(hs => hs.Student)
                                .HasForeignKey(hs => hs.StudentId);
            });

            builder.Entity<Course>(e =>
            {
                e.Property(c => c.Name)
                .IsUnicode(true)
                .HasMaxLength(80);

                e.Property(c => c.Description)
                .IsUnicode(true);

                e.HasMany(c => c.Resources)
                .WithOne(r => r.Course)
                .HasForeignKey(r=>r.CourseId);

                e.HasMany(c => c.HomeworkSubmissions)
                               .WithOne(hs => hs.Course)
                               .HasForeignKey(hs => hs.CourseId);

            });

            builder.Entity<Resource>(e =>
            {
                e.Property(r => r.Name)
                .IsUnicode(true)
                .HasMaxLength(50);

                e.Property(r => r.Url)
                .IsUnicode(false);

            });

            builder.Entity<Homework>(e =>
            {
                e.Property(h => h.Content)
                .IsUnicode(false);
            });

            builder.Entity<StudentCourse>(e =>
            {
                e.HasKey(sc=>new { sc.StudentId,sc.CourseId});

                e.HasOne(sc => sc.Student)
                .WithMany(s => s.CourseEnrollments)
                .HasForeignKey(sc => sc.StudentId);

                e.HasOne(sc => sc.Course)
                .WithMany(c => c.StudentsEnrolled)
                .HasForeignKey(sc => sc.CourseId);

            });


            base.OnModelCreating(builder);
        }


    }
}
