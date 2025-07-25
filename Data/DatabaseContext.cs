using System;
using Microsoft.EntityFrameworkCore;
using SubjectHelper.Models;

namespace SubjectHelper.Data;

public class DatabaseContext : DbContext
{
    private const string ConnectionString = "Server=localhost;Database=SubjectHelperDB;User=root;Password=;";
    
    public DbSet<Subject> Subjects { get; set; }
    public DbSet<Evaluation> Evaluations { get; set; }

    public DatabaseContext()
    {
        Database.EnsureDeleted();
        Database.EnsureCreated();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseMySql(ConnectionString, new MySqlServerVersion(new Version(10, 4, 32)));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Subject>()
            .Property(s => s.Code)
            .HasDefaultValue("");
        modelBuilder.Entity<Subject>()
            .HasIndex(s => s.Name)
            .IsUnique();

        modelBuilder.Entity<Subject>().HasData(
            new Subject { Id = 1, Name = "Computer Architecture", Code = "CS304" },
            new Subject { Id = 2, Name = "Database Management", Code = "CS306" },
            new Subject { Id = 3, Name = "Operations Research I", Code = "IE303" },
            new Subject { Id = 4, Name = "Introduction to Management", Code = "MAN102" },
            new Subject { Id = 5, Name = "Discrete Mathematics II", Code = "MATH209" }
        );
        
        modelBuilder.Entity<Evaluation>().HasData(
        // Student 1
        new Evaluation { Id = 1, Name = "Midterm", Weight = 30m, Grade = 67, SubjectId = 1 },
        new Evaluation { Id = 2, Name = "Quiz 1", Weight = 5m, Grade = 100, SubjectId = 1 },
        new Evaluation { Id = 3, Name = "Quiz 2", Weight = 5m, Grade = 80, SubjectId = 1 },
        new Evaluation { Id = 4, Name = "Project 1", Weight = 10m, Grade = 100, SubjectId = 1 },
        new Evaluation { Id = 5, Name = "Project 2", Weight = 10m, Grade = 100, SubjectId = 1 },
        new Evaluation { Id = 6, Name = "Final", Weight = 40m, Grade = 100, SubjectId = 1 },

        // Student 2
        new Evaluation { Id = 7, Name = "Midterm", Weight = 30m, Grade = 80, SubjectId = 2 },
        new Evaluation { Id = 8, Name = "Quiz 1", Weight = 5m, Grade = 100, SubjectId = 2 },
        new Evaluation { Id = 9, Name = "Quiz 2", Weight = 5m, Grade = 100, SubjectId = 2 },
        new Evaluation { Id = 10, Name = "Project 1", Weight = 10m, Grade = 95, SubjectId = 2 },
        new Evaluation { Id = 11, Name = "Project 2", Weight = 10m, Grade = 100, SubjectId = 2 },
        new Evaluation { Id = 12, Name = "Final", Weight = 40m, Grade = 83, SubjectId = 2 },

        // Student 3
        new Evaluation { Id = 13, Name = "Midterm", Weight = 20m, Grade = 100, SubjectId = 3 },
        new Evaluation { Id = 14, Name = "Quiz 1", Weight = 5m, Grade = 88, SubjectId = 3 },
        new Evaluation { Id = 15, Name = "Quiz 2", Weight = 5m, Grade = 100, SubjectId = 3 },
        new Evaluation { Id = 16, Name = "Quiz 3", Weight = 5m, Grade = 96, SubjectId = 3 },
        new Evaluation { Id = 17, Name = "Quiz 4", Weight = 5m, Grade = 96, SubjectId = 3 },
        new Evaluation { Id = 18, Name = "Project", Weight = 30m, Grade = 95, SubjectId = 3 },
        new Evaluation { Id = 19, Name = "Final", Weight = 30m, Grade = 100, SubjectId = 3 },

        // Student 4
        new Evaluation { Id = 20, Name = "Midterm I", Weight = 30m, Grade = 96, SubjectId = 4 },
        new Evaluation { Id = 21, Name = "Midterm II", Weight = 30m, Grade = 100, SubjectId = 4 },
        new Evaluation { Id = 22, Name = "Final", Weight = 40m, Grade = 100, SubjectId = 4 },

        // Student 5
        new Evaluation { Id = 23, Name = "Quiz 1", Weight = 10m, Grade = 99, SubjectId = 5 },
        new Evaluation { Id = 24, Name = "Midterm", Weight = 30m, Grade = 96, SubjectId = 5 },
        new Evaluation { Id = 25, Name = "Quiz 2", Weight = 10m, Grade = 100, SubjectId = 5 },
        new Evaluation { Id = 26, Name = "In-Lab Assignment", Weight = 15m, Grade = 100, SubjectId = 5 },
        new Evaluation { Id = 27, Name = "Final", Weight = 35m, Grade = 96, SubjectId = 5 }
    );
    }
    
    /*
     *
     INSERT INTO `Subjects`(`Name`) VALUES ('CA'),('DB'),('OR'),('MAN'),('DM2');
     INSERT INTO Evaluations (Name, Weightm, Grade, student_id)
       VALUES 
           -- Student 1
           ('Midterm', 30, 67, 1),
           ('Quiz 1', 5, 100, 1),
           ('Quiz 2', 5, 80, 1),
           ('Project 1', 10, 100, 1),
           ('Project 2', 10, 100, 1),
           ('Final', 40, 100, 1),
           
           -- Student 2
           ('Midterm', 30, 80, 2),
           ('Quiz 1', 5, 100, 2),
           ('Quiz 2', 5, 100, 2),
           ('Project 1', 10, 95, 2),
           ('Project 2', 10, 100, 2),
           ('Final', 40, 83, 2),
           
           -- Student 3
           ('Midterm', 20, 100, 3),
           ('Quiz 1', 5, 88, 3),
           ('Quiz 2', 5, 100, 3),
           ('Quiz 3', 5, 96, 3),
           ('Quiz 4', 5, 96, 3),
           ('Project', 30, 95, 3),
           ('Final', 30, 100, 3),
           
           -- Student 4
           ('Midterm I', 30, 96, 4),
           ('Midterm II', 30, 100, 4),
           ('Final', 40, 100, 4),
           
           -- Student 5
           ('Quiz 1', 10, 99, 5),
           ('Midterm', 30, 96, 5),
           ('Quiz 2', 10, 100, 5),
           ('In-Lab Assignment', 15, 100, 5),
           ('Final', 35, 96, 5);
     */
}