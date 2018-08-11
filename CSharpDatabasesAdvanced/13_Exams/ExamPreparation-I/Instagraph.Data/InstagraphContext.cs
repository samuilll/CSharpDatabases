using Instagraph.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Instagraph.Data
{
    public class InstagraphContext : DbContext
    {
        public DbSet<Comment> Comments { get; set; }

        public DbSet<Picture> Pictures { get; set; }

        public DbSet<Post> Posts { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<UserFollower> UsersFollowers { get; set; }

        public InstagraphContext() { }

        public InstagraphContext(DbContextOptions options)
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
            builder.Entity<User>(e =>
            {
                e.Property(u => u.Username)
                .HasMaxLength(30);

                e.HasAlternateKey(u => u.Username);

                e.Property(u => u.Password)
               .HasMaxLength(20);

                e.HasOne(u => u.ProfilePicture)
                .WithMany(pf => pf.Users)
                .HasForeignKey(u => u.ProfilePictureId)
                               .OnDelete(DeleteBehavior.Restrict);
                ;

            });

            builder.Entity<Post>(e =>
            {
                e.HasOne(p => p.Picture)
                .WithMany(pi => pi.Posts)
                .HasForeignKey(p => p.PictureId)
                               .OnDelete(DeleteBehavior.Restrict);
                ;


                e.HasOne(p => p.User)
               .WithMany(u => u.Posts)
               .HasForeignKey(p => p.UserId)
                              .OnDelete(DeleteBehavior.Restrict);
                
            });

            builder.Entity<Comment>(e =>
            {
                e.Property(c => c.Content)
                 .HasMaxLength(250);

                e.HasOne(c => c.Post)
                .WithMany(p => p.Comments)
                .HasForeignKey(c => c.PostId)
                               .OnDelete(DeleteBehavior.Restrict);
                ;

                e.HasOne(c => c.User)
               .WithMany(u => u.Comments)
               .HasForeignKey(c => c.UserId).OnDelete(DeleteBehavior.Restrict);
                ;

            }
            );

            builder.Entity<UserFollower>(e =>
            {
                e.HasKey(uf => new { uf.UserId, uf.FollowerId });

                e.HasOne(uf => uf.User)
                .WithMany(u => u.Followers)
                .HasForeignKey(uf => uf.UserId)
                               .OnDelete(DeleteBehavior.Restrict);
          


                e.HasOne(uf => uf.Follower)
               .WithMany(u => u.UsersFollowing)
               .HasForeignKey(uf => uf.FollowerId)
                              .OnDelete(DeleteBehavior.Restrict);


            }
            );
        }
    }
}
