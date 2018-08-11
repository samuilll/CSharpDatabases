using FastFood.Models;
using FastFood.Models.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;



namespace FastFood.Data
{
	public class FastFoodDbContext : DbContext
	{
        public DbSet<Category> Categories { get; set; }

        public DbSet<Employee> Employees { get; set; }

        public DbSet<Item> Items { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<OrderItem> OrderItems { get; set; }

        public DbSet<Position> Positions { get; set; }

        public FastFoodDbContext()
		{
		}

		public FastFoodDbContext(DbContextOptions options)
			: base(options)
		{
		}

		protected override void OnConfiguring(DbContextOptionsBuilder builder)
		{
			if (!builder.IsConfigured)
			{
				builder.UseSqlServer(Configuration.ConnectionString);
			}
		}

		protected override void OnModelCreating(ModelBuilder builder)
		{
            builder.Entity<Employee>(e =>
            {
                e.HasOne(em => em.Position)
                .WithMany(p => p.Employees)
                .HasForeignKey(em => em.PositionId);
            }
            );

            builder.Entity<Position>(e =>
            {
                e.HasAlternateKey(p => p.Name);
            }
           );

            builder.Entity<Item>(e =>
            {
                e.HasAlternateKey(i => i.Name);

                e.HasOne(i => i.Category)
                .WithMany(c => c.Items)
                .HasForeignKey(i => i.CategoryId);
            }
          );
            builder.Entity<Order>(e =>
            {
                e.Property(o => o.Type)
                  .HasDefaultValue(OrderType.ForHere);

                e.HasOne(o => o.Employee)
                .WithMany(em => em.Orders)
                .HasForeignKey(o => o.EmployeeId);
            }
         );

            builder.Entity<OrderItem>(e =>
            {
                e.HasKey(oi => new { oi.ItemId, oi.OrderId });

                e.HasOne(oi => oi.Order)
                .WithMany(o => o.OrderItems)
                .HasForeignKey(oi => oi.OrderId);

                e.HasOne(oi => oi.Item)
               .WithMany(i => i.OrderItems)
               .HasForeignKey(oi => oi.ItemId);
            }
    );
        }
	}
}