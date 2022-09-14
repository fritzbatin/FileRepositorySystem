namespace Smits.Etg.FileRepositorySystem.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Data.Entity.Core.Objects;
    using System.Data.Entity.Infrastructure;
    using System.Data.SqlClient;

    public partial class Entities : DbContext
    {
        public Entities()
            : base("name=Entities")
        {
        }


        //== Authentication

        #region Authentication

        public virtual DbSet<Access> Accesses { get; set; }
        public virtual DbSet<AspNetRole> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUser> AspNetUsers { get; set; }
        public virtual DbSet<AuditLog> AuditLogs { get; set; }
        public virtual DbSet<DatabaseArchivingLog> DatabaseArchivingLogs { get; set; }
        public virtual DbSet<EmailLog> EmailLogs { get; set; }
        public virtual DbSet<ModuleAccess> ModuleAccesses { get; set; }
        public virtual DbSet<Module> Modules { get; set; }
        public virtual DbSet<PasswordHistory> PasswordHistories { get; set; }
        public virtual DbSet<RoleDetail> RoleDetails { get; set; }
        public virtual DbSet<RolePermission> RolePermissions { get; set; }
        public virtual DbSet<SystemConfig> SystemConfigs { get; set; }
        public virtual DbSet<UserActivityLog> UserActivityLogs { get; set; }
        public virtual DbSet<UserDetail> UserDetails { get; set; }

        #endregion

        //== Authentication

        #region Employee

        public virtual DbSet<BusinessUnit> BusinessUnits { get; set; }
        public virtual DbSet<Department> Departments { get; set; }
        public virtual DbSet<EmployeeFile> EmployeeFiles { get; set; }
        public virtual DbSet<EmployeeProject> EmployeeProjects { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<EmployeeSalary> EmployeeSalaries { get; set; }
        public virtual DbSet<ErrorLog> ErrorLogs { get; set; }
        public virtual DbSet<Position> Positions { get; set; }
        public virtual DbSet<Project> Projects { get; set; }
        public virtual DbSet<vEmployeeListPerPosition> vEmployeeListPerPositions { get; set; }

        #endregion
      

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //== Authentication

            #region Authentication
           
            modelBuilder.Entity<Access>()
                .Property(e => e.RowVersion)
                .IsFixedLength();

            modelBuilder.Entity<Access>()
                .HasMany(e => e.ModuleAccesses)
                .WithRequired(e => e.Access)
                .HasForeignKey(e => e.ModuleAccess_Access);

            modelBuilder.Entity<AspNetRole>()
                .HasMany(e => e.RoleDetails)
                .WithRequired(e => e.AspNetRole)
                .HasForeignKey(e => e.RoleDetail_AspNetRole);

            modelBuilder.Entity<AspNetRole>()
                .HasMany(e => e.RolePermissions)
                .WithRequired(e => e.AspNetRole)
                .HasForeignKey(e => e.RolePermission_Role);

            modelBuilder.Entity<AspNetRole>()
                .HasMany(e => e.AspNetUsers)
                .WithMany(e => e.AspNetRoles)
                .Map(m => m.ToTable("AspNetUserRoles").MapLeftKey("RoleId").MapRightKey("UserId"));

            modelBuilder.Entity<AspNetUser>()
                .HasMany(e => e.AspNetUserClaims)
                .WithRequired(e => e.AspNetUser)
                .HasForeignKey(e => e.UserId);

            modelBuilder.Entity<AspNetUser>()
                .HasMany(e => e.AspNetUserLogins)
                .WithRequired(e => e.AspNetUser)
                .HasForeignKey(e => e.UserId);

            modelBuilder.Entity<AspNetUser>()
                .HasMany(e => e.PasswordHistories)
                .WithRequired(e => e.AspNetUser)
                .HasForeignKey(e => e.PasswordHistory_AspNetUser);

            modelBuilder.Entity<AspNetUser>()
                .HasMany(e => e.UserDetails)
                .WithRequired(e => e.AspNetUser)
                .HasForeignKey(e => e.UserDetail_AspNetUser)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<AuditLog>()
                .Property(e => e.RowVersion)
                .IsFixedLength();

            modelBuilder.Entity<DatabaseArchivingLog>()
                .Property(e => e.RowVersion)
                .IsFixedLength();

            modelBuilder.Entity<EmailLog>()
                .Property(e => e.RowVersion)
                .IsFixedLength();

            modelBuilder.Entity<ModuleAccess>()
                .Property(e => e.RowVersion)
                .IsFixedLength();

            modelBuilder.Entity<ModuleAccess>()
                .HasMany(e => e.RolePermissions)
                .WithRequired(e => e.ModuleAccess)
                .HasForeignKey(e => e.RolePermission_ModuleAccess);

            modelBuilder.Entity<Module>()
                .Property(e => e.RowVersion)
                .IsFixedLength();

            modelBuilder.Entity<Module>()
                .HasMany(e => e.ModuleAccesses)
                .WithRequired(e => e.Module)
                .HasForeignKey(e => e.ModuleAccess_Module);

            modelBuilder.Entity<PasswordHistory>()
                .Property(e => e.RowVersion)
                .IsFixedLength();

            modelBuilder.Entity<RoleDetail>()
                .Property(e => e.RowVersion)
                .IsFixedLength();

            modelBuilder.Entity<RolePermission>()
                .Property(e => e.RowVersion)
                .IsFixedLength();

            modelBuilder.Entity<SystemConfig>()
                .Property(e => e.RowVersion)
                .IsFixedLength();

            modelBuilder.Entity<UserActivityLog>()
                .Property(e => e.RowVersion)
                .IsFixedLength();

            modelBuilder.Entity<UserDetail>()
                .Property(e => e.RowVersion)
                .IsFixedLength();

            #endregion

            //== Authentication

            #region Employee
          
            modelBuilder.Entity<BusinessUnit>()
                .Property(e => e.RowVersion)
                .IsFixedLength();

            modelBuilder.Entity<BusinessUnit>()
                .HasMany(e => e.Projects)
                .WithRequired(e => e.BusinessUnit)
                .HasForeignKey(e => e.Project_BusinessUnit)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Department>()
                .Property(e => e.RowVersion)
                .IsFixedLength();

            modelBuilder.Entity<Department>()
                .HasMany(e => e.Employees)
                .WithRequired(e => e.Department)
                .HasForeignKey(e => e.Employee_Department)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<EmployeeFile>()
                .Property(e => e.RowVersion)
                .IsFixedLength();

            modelBuilder.Entity<EmployeeProject>()
                .Property(e => e.RowVersion)
                .IsFixedLength();

            modelBuilder.Entity<Employee>()
                .Property(e => e.RowVersion)
                .IsFixedLength();

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.EmployeeProjects)
                .WithRequired(e => e.Employee)
                .HasForeignKey(e => e.EmployeeProject_Employee)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.EmployeeSalaries)
                .WithRequired(e => e.Employee)
                .HasForeignKey(e => e.EmployeeSalary_EmpId);

            modelBuilder.Entity<EmployeeSalary>()
                .Property(e => e.Salary)
                .HasPrecision(19, 4);

            modelBuilder.Entity<EmployeeSalary>()
                .Property(e => e.RowVersion)
                .IsFixedLength();

            modelBuilder.Entity<ErrorLog>()
                .Property(e => e.RowVersion)
                .IsFixedLength();

            modelBuilder.Entity<Position>()
                .Property(e => e.RowVersion)
                .IsFixedLength();

            modelBuilder.Entity<Position>()
                .HasMany(e => e.Employees)
                .WithRequired(e => e.Position)
                .HasForeignKey(e => e.Employee_Position)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Project>()
                .Property(e => e.RowVersion)
                .IsFixedLength();

            modelBuilder.Entity<Project>()
                .HasMany(e => e.EmployeeProjects)
                .WithRequired(e => e.Project)
                .HasForeignKey(e => e.EmployeeProject_Project)
                .WillCascadeOnDelete(false);

            #endregion
        }



    }
}
