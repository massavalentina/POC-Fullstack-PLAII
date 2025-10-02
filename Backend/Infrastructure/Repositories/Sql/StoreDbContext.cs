using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Sql
{
    /// <summary>
    /// Contexto de almacenamiento en base de datos. Aca se definen los nombres de 
    /// las tablas, y los mapeos entre los objetos
    /// </summary>
    internal class StoreDbContext : DbContext
    {
        public DbSet<DummyEntity> DummyEntity { get; set; }
        public DbSet<Car> Cars { get; set; }

        public StoreDbContext(DbContextOptions<StoreDbContext> options) : base(options)
        {
        }

        protected StoreDbContext()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DummyEntity>().ToTable("DummyEntity");
            modelBuilder.Entity<Car>(b =>
            {
                b.ToTable("Cars");
                b.HasKey(x => x.Id);
                b.Property(x => x.Id).ValueGeneratedNever();
                b.Property(x => x.Make).IsRequired().HasMaxLength(30);
                b.Property(x => x.Model).IsRequired().HasMaxLength(30);
                b.Property(x => x.Color).IsRequired().HasMaxLength(30);
                b.Property(x => x.ModelYear).IsRequired();
                b.Property(x => x.MotorNumber).IsRequired().HasMaxLength(50);
                b.Property(x => x.ChassisNumber).IsRequired().HasMaxLength(50);
                b.HasIndex(x => x.ChassisNumber).IsUnique(); 
                b.HasIndex(x => x.MotorNumber).IsUnique();   
            });
        }
    }
}
