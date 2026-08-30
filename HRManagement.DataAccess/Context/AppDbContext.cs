using HRManagement.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;


namespace HRManagement.DataAccess.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<EmployeeProfile> EmployeeProfiles { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<EmployeePosition> EmployeePositions { get; set; }
        public DbSet<Attendance> Attendances { get; set; }
        public DbSet<LeaveType> LeaveTypes { get; set; }
        public DbSet<LeaveRequest> LeaveRequests { get; set; }
        public DbSet<Salary> Salaries { get; set; }
        public DbSet<Payroll> Payrolls { get; set; }
        public DbSet<PerformanceReview> PerformanceReviews { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<EmployeeProject> EmployeeProjects { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // =========================
            // Primary Keys
            // =========================

            modelBuilder.Entity<Employee>().HasKey(e => e.EmployeeId);

            modelBuilder.Entity<EmployeeProfile>().HasKey(p => p.EmployeeProfileId);

            modelBuilder.Entity<Department>().HasKey(d => d.DepartmentId);

            modelBuilder.Entity<Position>().HasKey(p => p.PositionId);

            modelBuilder.Entity<Attendance>().HasKey(a => a.AttendanceId);

            modelBuilder.Entity<LeaveType>().HasKey(lt => lt.LeaveTypeId);

            modelBuilder.Entity<LeaveRequest>().HasKey(lr => lr.LeaveRequestId);

            modelBuilder.Entity<Salary>().HasKey(s => s.SalaryId);

            modelBuilder.Entity<Payroll>().HasKey(p => p.PayrollId);

            modelBuilder.Entity<PerformanceReview>().HasKey(pr => pr.PerformanceReviewId);

            modelBuilder.Entity<Project>().HasKey(p => p.ProjectId);

            modelBuilder.Entity<User>().HasKey(u => u.UserId);

            modelBuilder.Entity<Role>().HasKey(r => r.RoleId);


            // =========================
            // Employee - Department
            // One-to-Many
            // =========================

            modelBuilder.Entity<Employee>().HasOne(e => e.Department).WithMany(d => d.Employees).HasForeignKey(e => e.DepartmentId)
                .OnDelete(DeleteBehavior.Restrict);


            // =========================
            // Employee - EmployeeProfile
            // One-to-One
            // =========================

            modelBuilder.Entity<Employee>()
                .HasOne(e => e.EmployeeProfile)
                .WithOne(p => p.Employee)
                .HasForeignKey<EmployeeProfile>(p => p.EmployeeId)
                .OnDelete(DeleteBehavior.Cascade);


            // =========================
            // Employee - Position
            // Many-to-Many
            // =========================

            modelBuilder.Entity<EmployeePosition>().HasKey(ep => new
                {
                    ep.EmployeeId,
                    ep.PositionId
                });

            modelBuilder.Entity<EmployeePosition>()
                .HasOne(ep => ep.Employee)
                .WithMany(e => e.EmployeePositions)
                .HasForeignKey(ep => ep.EmployeeId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<EmployeePosition>()
                .HasOne(ep => ep.Position)
                .WithMany(p => p.EmployeePositions)
                .HasForeignKey(ep => ep.PositionId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Position>().Property(p => p.BaseSalary).HasPrecision(18, 2);


            // =========================
            // Employee - Attendance
            // One-to-Many
            // =========================

            modelBuilder.Entity<Attendance>()
                .HasOne(a => a.Employee)
                .WithMany(e => e.Attendances)
                .HasForeignKey(a => a.EmployeeId)
                .OnDelete(DeleteBehavior.Cascade);


            // =========================
            // Employee - LeaveRequest
            // One-to-Many
            // =========================

            modelBuilder.Entity<LeaveRequest>()
                .HasOne(lr => lr.Employee)
                .WithMany(e => e.LeaveRequests)
                .HasForeignKey(lr => lr.EmployeeId)
                .OnDelete(DeleteBehavior.Cascade);


            // =========================
            // LeaveType - LeaveRequest
            // One-to-Many
            // =========================

            modelBuilder.Entity<LeaveRequest>()
                .HasOne(lr => lr.LeaveType)
                .WithMany(lt => lt.LeaveRequests)
                .HasForeignKey(lr => lr.LeaveTypeId)
                .OnDelete(DeleteBehavior.Restrict);


            // =========================
            // Employee - Salary
            // One-to-Many
            // =========================

            modelBuilder.Entity<Salary>()
                .HasOne(s => s.Employee)
                .WithMany(e => e.Salaries)
                .HasForeignKey(s => s.EmployeeId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Salary>().Property(s => s.BasicSalary).HasPrecision(18, 2);

            modelBuilder.Entity<Salary>().Property(s => s.Allowances).HasPrecision(18, 2);

            modelBuilder.Entity<Salary>().Property(s => s.Deductions).HasPrecision(18, 2);


            // =========================
            // Employee - Payroll
            // One-to-Many
            // =========================

            modelBuilder.Entity<Payroll>()
                .HasOne(p => p.Employee)
                .WithMany(e => e.Payrolls)
                .HasForeignKey(p => p.EmployeeId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Payroll>().Property(p => p.GrossSalary).HasPrecision(18, 2);

            modelBuilder.Entity<Payroll>().Property(p => p.TotalDeductions).HasPrecision(18, 2);

            modelBuilder.Entity<Payroll>().Property(p => p.NetSalary).HasPrecision(18, 2);


            // =========================
            // Employee - PerformanceReview
            // One-to-Many
            // =========================

            modelBuilder.Entity<PerformanceReview>()
                .HasOne(pr => pr.Employee)
                .WithMany(e => e.PerformanceReviews)
                .HasForeignKey(pr => pr.EmployeeId)
                .OnDelete(DeleteBehavior.Cascade);


            // Reviewer - Employee
            modelBuilder.Entity<PerformanceReview>()
                .HasOne(pr => pr.Reviewer)
                .WithMany(e => e.ReviewsGiven)
                .HasForeignKey(pr => pr.ReviewerId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PerformanceReview>().Property(pr => pr.Score).HasPrecision(5, 2);

            modelBuilder.Entity<PerformanceReview>()
                .ToTable(t => t.HasCheckConstraint("CK_PerformanceReview_Score",
                    "Score >= 0 AND Score <= 100"
                ));


            // =========================
            // Employee - Project
            // Many-to-Many
            // =========================

            modelBuilder.Entity<EmployeeProject>().HasKey(ep => new
                {
                    ep.EmployeeId,
                    ep.ProjectId
                });

            modelBuilder.Entity<EmployeeProject>()
                .HasOne(ep => ep.Employee)
                .WithMany(e => e.EmployeeProjects)
                .HasForeignKey(ep => ep.EmployeeId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<EmployeeProject>()
                .HasOne(ep => ep.Project)
                .WithMany(p => p.EmployeeProjects)
                .HasForeignKey(ep => ep.ProjectId)
                .OnDelete(DeleteBehavior.Cascade);


            // =========================
            // Project Constraints
            // =========================

            modelBuilder.Entity<Project>()
                .Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(150);

            modelBuilder.Entity<Project>()
                .ToTable(t => t.HasCheckConstraint(
                    "CK_Project_Dates",
                    "[EndDate] IS NULL OR [EndDate] >= [StartDate]"
                ));


            // =========================
            // User - Employee
            // One-to-One
            // =========================

            modelBuilder.Entity<User>()
                .HasOne(u => u.Employee)
                .WithOne(e => e.User)
                .HasForeignKey<User>(u => u.EmployeeId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<User>().HasIndex(u => u.EmployeeId).IsUnique();


            // =========================
            // User - Role
            // Many-to-Many
            // =========================

            modelBuilder.Entity<UserRole>().HasKey(ur => new
                {
                    ur.UserId,
                    ur.RoleId
                });

            modelBuilder.Entity<UserRole>()
                .HasOne(ur => ur.User)
                .WithMany(u => u.UserRoles)
                .HasForeignKey(ur => ur.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<UserRole>()
                .HasOne(ur => ur.Role)
                .WithMany(r => r.UserRoles)
                .HasForeignKey(ur => ur.RoleId)
                .OnDelete(DeleteBehavior.Cascade);


            // =========================
            // User Constraints
            // =========================

            modelBuilder.Entity<User>()
                .Property(u => u.Username)
                .IsRequired()
                .HasMaxLength(50);

            modelBuilder.Entity<User>().HasIndex(u => u.Username).IsUnique();


            // =========================
            // Role Constraints
            // =========================

            modelBuilder.Entity<Role>().Property(r => r.Name).IsRequired().HasMaxLength(50);

            modelBuilder.Entity<Role>().HasIndex(r => r.Name).IsUnique();

            // =========================
            // Seed Data
            // =========================

            modelBuilder.Entity<Department>().HasData(
                new Department
                {
                    DepartmentId = 1,Name = "Human Resources"
                },
                new Department
                {
                    DepartmentId = 2,
                    Name = "Information Technology"
                },
                new Department
                {
                    DepartmentId = 3,Name = "Finance"
                }
            );

            modelBuilder.Entity<Position>().HasData(
                new Position
                {
                    PositionId = 1,
                    Title = "HR Manager",
                    Description = "Human Resources Manager",
                    BaseSalary = 1000
                },
                new Position
                {
                    PositionId = 2,
                    Title = "Software Developer",
                    Description = "Backend Software Developer",
                    BaseSalary = 800
                },
                new Position
                {
                    PositionId = 3,
                    Title = "Accountant",
                    Description = "Finance Accountant",
                    BaseSalary = 700
                }
            );

            modelBuilder.Entity<LeaveType>().HasData(
                new LeaveType
                {
                    LeaveTypeId = 1,
                    Name = "Annual Leave",
                    Description = "Annual vacation leave"
                },
                new LeaveType
                {
                    LeaveTypeId = 2,
                    Name = "Sick Leave",
                    Description = "Leave due to illness"
                },
                new LeaveType
                {
                    LeaveTypeId = 3,
                    Name = "Unpaid Leave",
                    Description = "Leave without salary"
                }
            );

            modelBuilder.Entity<Role>().HasData(
                new Role
                {
                    RoleId = 1,
                    Name = "Admin",
                    Description = "System Administrator"
                },
                new Role
                {
                    RoleId = 2,
                    Name = "HR",
                    Description = "Human Resources"
                },
                new Role
                {
                    RoleId = 3,
                    Name = "Employee",
                    Description = "Regular Employee"
                }
            );
        }
    }
}
