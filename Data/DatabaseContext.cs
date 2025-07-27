using Microsoft.EntityFrameworkCore;
using SubjectHelper.Models;
using SubjectHelper.Models.ScheduleMaker;

namespace SubjectHelper.Data;

public class DatabaseContext : DbContext
{
    private const string DatabasePath = "SubjectHelperDB.db";
    
    public DbSet<Subject> Subjects { get; set; }
    public DbSet<Evaluation> Evaluations { get; set; }

    public DbSet<SubjectSM> SubjectSM { get; set; }
    public DbSet<SectionSM> SectionSM { get; set; }
    public DbSet<TimeSM> TimeSM { get; set; }

    public DatabaseContext()
    {
        Database.EnsureCreated();
    }

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

        modelBuilder.Entity<SubjectSM>()
            .HasIndex(s => s.Title)
            .IsUnique();
        
        modelBuilder.Entity<SectionSM>()
            .HasIndex(s => s.Title)
            .IsUnique();
    }
}