using Microsoft.EntityFrameworkCore;
using InternPortal.Domain.Entities;

namespace InternPortal.Infrastructure.Persistence;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<User> Users { get; set; }

    public DbSet<InternPortal.Domain.Entities.Application> Applications { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

       

        modelBuilder.Entity<InternPortal.Domain.Entities.Application>()
            .HasIndex(a => a.UserId)
            .IsUnique();

        modelBuilder.Entity<InternPortal.Domain.Entities.Application>()
            .HasOne(a => a.User)
            .WithOne(u => u.Application)
            .HasForeignKey<InternPortal.Domain.Entities.Application>(a => a.UserId);

    }
}