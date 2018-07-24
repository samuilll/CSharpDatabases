namespace PhotoShare.Data
{
    using Microsoft.EntityFrameworkCore;

    using Models;
    using Configuration;
    using System.Linq;
    using System.ComponentModel.DataAnnotations;
    using System.Collections.Generic;

    public class PhotoShareContext : DbContext
    { 
        public PhotoShareContext() { }

        public PhotoShareContext(DbContextOptions options)
            :base(options) { }

        public DbSet<User> Users { get; set; }

        public DbSet<Album> Albums { get; set; }

        public DbSet<Picture> Pictures { get; set; }

        public DbSet<Tag> Tags { get; set; }

        public DbSet<AlbumRole> AlbumRoles { get; set; }

        public DbSet<Town> Towns { get; set; }
		
		public DbSet<AlbumTag> AlbumTags { get; set; }

        public DbSet<Friendship> Friendships { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AlbumConfig());

            modelBuilder.ApplyConfiguration(new AlbumRoleConfig());

            modelBuilder.ApplyConfiguration(new AlbumTagConfig());

            modelBuilder.ApplyConfiguration(new FriendshipConfig());

            modelBuilder.ApplyConfiguration(new PictureConfig());

            modelBuilder.ApplyConfiguration(new TagConfig());

            modelBuilder.ApplyConfiguration(new TownConfig());

            modelBuilder.ApplyConfiguration(new UserConfig());
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ServerConfig.ConnectionString);
        }

        public override int SaveChanges()
        {
            var entities = this.ChangeTracker.Entries().Where(e=>e.State==EntityState.Modified|| e.State == EntityState.Added)
                .Select(e=>e.Entity).ToList();

            foreach (var entity in entities)
            {
                try
                {
                    Validate(entity);
                }
                catch (ValidationException e)
                {
                    this.Entry(entity).State = EntityState.Detached;
                    throw  new ValidationException(e.Message);
                }
            }
            return base.SaveChanges();
        }

        private void  Validate(object entity)
        {
            var validationContext = new ValidationContext(entity);


            Validator.ValidateObject(
                entity,
                validationContext,
                validateAllProperties: true);
        }
    }
}