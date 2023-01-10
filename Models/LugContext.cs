using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace CAPSTONEPROJECT.Models
{
    public partial class LugContext : DbContext
    {
        public LugContext()
        {
        }

        public LugContext(DbContextOptions<LugContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<Application> Applications { get; set; }
        public virtual DbSet<ApplicationStatus> ApplicationStatuses { get; set; }
        public virtual DbSet<ApplicationType> ApplicationTypes { get; set; }
        public virtual DbSet<BackupSalary> BackupSalaries { get; set; }
        public virtual DbSet<CheckAttendanceFeedback> CheckAttendanceFeedbacks { get; set; }
        public virtual DbSet<ContractSalary> ContractSalaries { get; set; }
        public virtual DbSet<ContractType> ContractTypes { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<EmployeeSkill> EmployeeSkills { get; set; }
        public virtual DbSet<EmployeeType> EmployeeTypes { get; set; }
        public virtual DbSet<Position> Positions { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Salary> Salaries { get; set; }
        public virtual DbSet<Shift> Shifts { get; set; }
        public virtual DbSet<Skill> Skills { get; set; }
        public virtual DbSet<SystemSetting> SystemSettings { get; set; }
        public virtual DbSet<WorkSchedule> WorkSchedules { get; set; }
        public virtual DbSet<WorkScheduleStatus> WorkScheduleStatuses { get; set; }
        public virtual DbSet<Workplace> Workplaces { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=.\\SQLFORLABWEB;Database=LUG;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Account>(entity =>
            {
                entity.Property(e => e.DelFlag).HasDefaultValueSql("((0))");

                entity.HasOne(d => d.AccountNavigation)
                    .WithOne(p => p.Account)
                    .HasForeignKey<Account>(d => d.AccountId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Account_Employee");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Accounts)
                    .HasForeignKey(d => d.RoleId)
                    .HasConstraintName("FK_Account_Role");
            });

            modelBuilder.Entity<Application>(entity =>
            {
                entity.HasOne(d => d.ApplicationStatus)
                    .WithMany(p => p.Applications)
                    .HasForeignKey(d => d.ApplicationStatusId)
                    .HasConstraintName("FK_Application_ApplicationStatus");

                entity.HasOne(d => d.ApplicationType)
                    .WithMany(p => p.Applications)
                    .HasForeignKey(d => d.ApplicationTypeId)
                    .HasConstraintName("FK_Application_ApplicationType");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.Applications)
                    .HasForeignKey(d => d.EmployeeId)
                    .HasConstraintName("FK_Application_Employee");

                entity.HasOne(d => d.Shift)
                    .WithMany(p => p.Applications)
                    .HasForeignKey(d => d.ShiftId)
                    .HasConstraintName("FK_Application_WorkSchedule");
            });

            modelBuilder.Entity<BackupSalary>(entity =>
            {
                entity.HasOne(d => d.Salary)
                    .WithMany(p => p.BackupSalaries)
                    .HasForeignKey(d => d.SalaryId)
                    .HasConstraintName("FK_BackupSalary_Salary");
            });

            modelBuilder.Entity<CheckAttendanceFeedback>(entity =>
            {
                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.CheckAttendanceFeedbacks)
                    .HasForeignKey(d => d.EmployeeId)
                    .HasConstraintName("FK_CheckAttendanceFeedback_Employee");
            });

            modelBuilder.Entity<ContractSalary>(entity =>
            {
                entity.Property(e => e.ContractSalary1).HasDefaultValueSql("((0))");

                entity.HasOne(d => d.ContractType)
                    .WithMany(p => p.ContractSalaries)
                    .HasForeignKey(d => d.ContractTypeId)
                    .HasConstraintName("FK_ContractSalary_ContractType");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.ContractSalaries)
                    .HasForeignKey(d => d.EmployeeId)
                    .HasConstraintName("FK_ContractSalary_Employee");
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.Property(e => e.DelFlag).HasDefaultValueSql("((0))");

                entity.HasOne(d => d.EmployeeType)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.EmployeeTypeId)
                    .HasConstraintName("FK_Employee_EmployeeType1");

                entity.HasOne(d => d.Position)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.PositionId)
                    .HasConstraintName("FK_Employee_Position");

                entity.HasOne(d => d.Workplace)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.WorkplaceId)
                    .HasConstraintName("FK_Employee_WorkPlace");
            });

            modelBuilder.Entity<EmployeeSkill>(entity =>
            {
                entity.HasKey(e => new { e.EmployeeId, e.SkillId });

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.EmployeeSkills)
                    .HasForeignKey(d => d.EmployeeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EmployeeSkill_Employee");

                entity.HasOne(d => d.Skill)
                    .WithMany(p => p.EmployeeSkills)
                    .HasForeignKey(d => d.SkillId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EmployeeSkill_Skill");
            });

            modelBuilder.Entity<EmployeeType>(entity =>
            {
                entity.Property(e => e.DelFlag).HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<Position>(entity =>
            {
                entity.Property(e => e.DelFlag).HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.Property(e => e.DelFlag).HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<Salary>(entity =>
            {
                entity.Property(e => e.Income).HasDefaultValueSql("((0))");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.Salaries)
                    .HasForeignKey(d => d.EmployeeId)
                    .HasConstraintName("FK_Salary_Employee");
            });

            modelBuilder.Entity<Shift>(entity =>
            {
                entity.Property(e => e.DelFlag).HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<Skill>(entity =>
            {
                entity.Property(e => e.DelFlag).HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<WorkSchedule>(entity =>
            {
                entity.Property(e => e.IsOt).HasDefaultValueSql("((0))");

                entity.Property(e => e.WorkHours).HasDefaultValueSql("((0))");

                entity.Property(e => e.WorkScheduleStatusId).HasDefaultValueSql("((4))");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.WorkSchedules)
                    .HasForeignKey(d => d.EmployeeId)
                    .HasConstraintName("FK_WorkSchedule_Employee");

                entity.HasOne(d => d.Shift)
                    .WithMany(p => p.WorkSchedules)
                    .HasForeignKey(d => d.ShiftId)
                    .HasConstraintName("FK_WorkSchedule_Shift");

                entity.HasOne(d => d.WorkScheduleStatus)
                    .WithMany(p => p.WorkSchedules)
                    .HasForeignKey(d => d.WorkScheduleStatusId)
                    .HasConstraintName("FK_WorkSchedule_WorkScheduleStatus");

                entity.HasOne(d => d.Workplace)
                    .WithMany(p => p.WorkSchedules)
                    .HasForeignKey(d => d.WorkplaceId)
                    .HasConstraintName("FK_WorkSchedule_Workplace");
            });

            modelBuilder.Entity<Workplace>(entity =>
            {
                entity.Property(e => e.DelFlag).HasDefaultValueSql("((0))");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
