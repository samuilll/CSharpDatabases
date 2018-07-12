using Microsoft.EntityFrameworkCore;

using Forum.Data.Models;

namespace Forum.Data
{
  public  class ForumDbContext:DbContext
    {
        public ForumDbContext()
        {
        }

        public ForumDbContext(DbContextOptions options)
            :base(options)
        {

        }

        public DbSet<Tag> Tags { get; set; }

        public DbSet<PostTag> PostsTags { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Post> Posts { get; set; }

        public DbSet<Reply> Replies { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(Configuraion.ConnectionbString);
            }
        }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Category>()
               .HasMany(c => c.Posts)
               .WithOne(p => p.Category)
               .HasForeignKey(p => p.CategoryId);

            builder.Entity<Post>()
                .HasMany(p => p.Replies)
                .WithOne(r => r.Post)
                .HasForeignKey(r => r.PostId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<User>()
                .ToTable("Users")
                .HasMany(u => u.Posts)
                .WithOne(p => p.Author)
                .HasForeignKey(p => p.AuthorId);

            builder.Entity<User>()
                          .HasMany(u => u.Replies)
                          .WithOne(r => r.Author)
                          .HasForeignKey(r => r.AuthorId);

            builder.Entity<PostTag>()
                .ToTable("PostsTags")
                .HasKey(pt => new { pt.PostId, pt.TagId });


            builder.Entity<Tag>()
                .ToTable("Tags");
        }
    }
}
