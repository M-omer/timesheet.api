using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

using timesheet.model;

namespace timesheet.data
{
    public class TimesheetDb : DbContext
    {
        public TimesheetDb(DbContextOptions<TimesheetDb> options)
            : base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Task> Tasks { get; set; }
        public DbSet<TimeSheet> TimeSheets { get; set; }
        public DbSet<Days> Days { get; set; }
        public DbSet<TaskEmployee> TaskEmployees { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TaskEmployee>()
                .HasKey(x => new { x.TaskID, x.EmployeeID });
            modelBuilder.Entity<TaskEmployee>()
                .HasOne(x => x.Employee)
                .WithMany(x => x.TaskEmployees)
                .HasForeignKey(x => x.EmployeeID);
            modelBuilder.Entity<TaskEmployee>()
                .HasOne(x => x.Task)
                .WithMany(x => x.TaskEmployees)
                .HasForeignKey(x => x.TaskID);
            base.OnModelCreating(modelBuilder);
        }
    }
}
