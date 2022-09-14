namespace Smits.Etg.FileRepositorySystem.Models
{
    using LINQtoCSV;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Web.Mvc;



    public partial class Employee
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Employee()
        {
            EmployeeFiles = new HashSet<EmployeeFile>();
            EmployeeProjects = new HashSet<EmployeeProject>();
            EmployeeSalaries = new HashSet<EmployeeSalary>();
        }
        #region Required only for CSV
        //==============Required only for CSV
        [CsvColumn(Name = "Employee Id", FieldIndex = 1)]
        public string EmpId { get { return EmployeeId; } }

        [CsvColumn(Name = "First Name", FieldIndex = 2)]
        public string fName { get { return FirstName; } }

        [CsvColumn(Name = "Middle Name", FieldIndex = 3)]
        public string mName { get { return MiddleName; } }

        [CsvColumn(Name = "Last Name", FieldIndex = 4)]
        public string lName { get { return LastName; } }

        [CsvColumn(Name = "Email Address", FieldIndex = 5)]
        public string emailAdd { get { return EmailAddress; } }

        [CsvColumn(Name = "Position", FieldIndex = 6)]
        public string PosName { get { return Position.Name; } }

        [CsvColumn(Name = "Department", FieldIndex = 7)]
        public string DeptName { get { return Department.Name; } }
        //==============Required only for CSV

        #endregion
        [DisplayName("Name")]
        public string FullName
        {
            get
            {
              return  FirstName + " " + MiddleName + " " + LastName;
            }
        }

        [Key]
        public int Id { get; set; }

        [DisplayName("Employee Id")]
        [Required(ErrorMessage = "Please Enter Employee Id!")]
        [StringLength(10)]
        [Remote("IsEmployeeIdAvailable", "Employee", AdditionalFields = "Id", ErrorMessage = "Employee Id already exists.")]
        public string EmployeeId { get; set; }

        [DisplayName("First Name")]
        [Required(ErrorMessage = "Please Enter First Name!")]
        [StringLength(50)]
        public string FirstName { get; set; }

        [DisplayName("Middle Name")]
        [StringLength(50)]
        public string MiddleName { get; set; }

        [DisplayName("Last Name")]
        [Required(ErrorMessage = "Please Enter Last Name!")]
        [StringLength(50, MinimumLength = 3)]
        [Remote("IsNamevalid", "Employee", AdditionalFields = "FirstName,Id", ErrorMessage = "Employee First name w/ Last name already exists.")]
        public string LastName { get; set; }

        [DisplayName("Email Address")]
        [Required(ErrorMessage = "Please provide email address!")]
        [EmailAddress]
        [StringLength(255)]
        [Remote("IsEmailAddressAvailable", "Employee", AdditionalFields = "Id", ErrorMessage = "Email already exists in database.")]
        public string EmailAddress { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Select Position")]
        [Required(ErrorMessage = "Select Position")]
        [DisplayName("Position")]
        public int Employee_Position { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Select Department")]
        [Required(ErrorMessage = "Select Department")]
        [DisplayName("Department")]
        public int Employee_Department { get; set; }

        [DisplayName("Created")]
        public DateTimeOffset? Created { get; set; }

        [DisplayName("Created By")]
        [StringLength(255)]
        public string CreatedBy { get; set; }

        [DisplayName("Modified")]
        public DateTimeOffset? Modified { get; set; }

        [StringLength(255)]
        public string ModifiedBy { get; set; }

        [DisplayName("Modified by")]
        [Column(TypeName = "timestamp")]
        [MaxLength(8)]
        [Timestamp]
        public byte[] RowVersion { get; set; }

        public virtual Department Department { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EmployeeFile> EmployeeFiles { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EmployeeProject> EmployeeProjects { get; set; }

        public virtual Position Position { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EmployeeSalary> EmployeeSalaries { get; set; }
    }
}
