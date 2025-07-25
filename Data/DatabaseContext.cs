using Microsoft.EntityFrameworkCore;
using SubjectHelper.Models;

namespace SubjectHelper.Data;

public class DatabaseContext : DbContext
{
    private const string DatabasePath = "SubjectHelperDB.db";
    
    public DbSet<Subject> Subjects { get; set; }
    public DbSet<Evaluation> Evaluations { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite($"Data Source={DatabasePath}");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Subject>()
            .Property(s => s.Code)
            .HasDefaultValue("");
        modelBuilder.Entity<Subject>()
            .HasIndex(s => s.Name)
            .IsUnique();
    }
}