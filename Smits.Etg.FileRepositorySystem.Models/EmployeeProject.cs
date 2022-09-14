namespace Smits.Etg.FileRepositorySystem.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Web.Mvc;

    public partial class EmployeeProject
    {
        [Key]
        public int Id { get; set; }

       
        [Remote("IsProjectAvailbleForEmployee", "EmployeeProject", AdditionalFields = "Id,EmployeeProject_Employee", ErrorMessage = "The project already assigned.")]
        [Required(ErrorMessage = "Please select Project")]
        [Range(1, int.MaxValue, ErrorMessage = "Select Project")]
        [DisplayName("Project")]
        public int EmployeeProject_Project { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Select Employee")]
        [Required(ErrorMessage = "Please select Employee")]
        [DisplayName("Employee")]
        public int EmployeeProject_Employee { get; set; }

        public DateTimeOffset? Created { get; set; }

        [DisplayName("Created By")]
        public string CreatedBy { get; set; }

        public DateTimeOffset? Modified { get; set; }

        [DisplayName("Modified by")]
        public string ModifiedBy { get; set; }

        [Column(TypeName = "timestamp")]
        [MaxLength(8)]
        [Timestamp]
        public byte[] RowVersion { get; set; }

        public virtual Employee Employee { get; set; }

        public virtual Project Project { get; set; }
    }
}
