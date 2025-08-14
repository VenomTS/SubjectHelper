using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using SubjectHelper.Models;
using SubjectHelper.Models.ScheduleMaker;

namespace SubjectHelper.Data;

public sealed class DatabaseContext : DbContext
{
    private const string DatabasePath = "SubjectHelperDB.db";
    
    public DbSet<Subject> Subjects { get; set; }
    public DbSet<Evaluation> Evaluations { get; set; }
    public DbSet<Absence> Absences { get; set; }

    public DbSet<SMSubject> SMSubjects { get; set; }
    public DbSet<SMSection> SMSections { get; set; }
    public DbSet<SMTime> SMTimes { get; set; }

    public DatabaseContext()
    {
        Database.EnsureCreated();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var currentWorkingDirectory = AppDomain.CurrentDomain.BaseDirectory;

        var finalPath = Path.Combine(currentWorkingDirectory, DatabasePath);
        
        optionsBuilder.UseSqlite($"Data Source={finalPath}");
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