namespace Smits.Etg.FileRepositorySystem.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class EmployeeSalary
    {
        
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter currency equal or greater than 10,000")]
        [Range(10000.00, 9999999.99)]
        [DataType(DataType.Currency)]
        [Column(TypeName = "money")]
        public decimal Salary { get; set; }

        [Required(ErrorMessage = "Please Enter Employee Id!")]
        [DisplayName("Employee Id")]
        public int EmployeeSalary_EmpId { get; set; }

        public DateTimeOffset? Created { get; set; }

        [StringLength(255)]
        public string CreatedBy { get; set; }

        public DateTimeOffset? Modified { get; set; }

        [StringLength(255)]
        public string ModifiedBy { get; set; }

        [StringLength(10)]
        public string RowVersion { get; set; }

        public virtual Employee Employee { get; set; }
    }
}
