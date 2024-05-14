using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;

public class ApplicationDbContext : DbContext
{
    public DbSet<Game> Games { get; set; }
    public DbSet<GraphicsPackage> GraphicsPackages { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Username=postgres;Password=postgres;Database=CourseWorkVar15");
        if (!optionsBuilder.IsConfigured)
        {
            using (var context = new ApplicationDbContext())
            {
                context.Database.EnsureCreated();
            }
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Game>().HasKey(e => e.Id);
        modelBuilder.Entity<Game>().Property(e => e.Id).ValueGeneratedOnAdd();
        modelBuilder.Entity<Game>().Property(e => e.Name).IsRequired().HasMaxLength(100);
        modelBuilder.Entity<Game>().Property(e => e.Developer).IsRequired().HasMaxLength(100);
        modelBuilder.Entity<Game>().Property(e => e.Platform).IsRequired().HasMaxLength(50);
        modelBuilder.Entity<Game>().Property(e => e.LicenseType).IsRequired();
        modelBuilder.Entity<Game>().Property(e => e.PricePerYear).HasColumnType("decimal(18,2)").IsRequired();

        modelBuilder.Entity<GraphicsPackage>().HasKey(e => e.Id);
        modelBuilder.Entity<GraphicsPackage>().Property(e => e.Id).ValueGeneratedOnAdd();
        modelBuilder.Entity<GraphicsPackage>().Property(e => e.Name).IsRequired().HasMaxLength(100);
        modelBuilder.Entity<GraphicsPackage>().Property(e => e.Manufacturer).IsRequired().HasMaxLength(100);
        modelBuilder.Entity<GraphicsPackage>().Property(e => e.GraphicsType).IsRequired();
        modelBuilder.Entity<GraphicsPackage>().Property(e => e.PricePerYear).HasColumnType("decimal(18,2)").IsRequired();
    }
}