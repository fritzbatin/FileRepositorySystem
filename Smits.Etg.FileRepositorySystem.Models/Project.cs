namespace Smits.Etg.FileRepositorySystem.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Web.Mvc;

    public partial class Project
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Project()
        {
            EmployeeProjects = new HashSet<EmployeeProject>();
        }

        [Key]
        public int Id { get; set; }

        [Remote("IsProjectCodeExist", "Project", AdditionalFields = "Id", ErrorMessage = "Project Code already exists.")]
        [Required(ErrorMessage = "Position Code Required")]
        [StringLength(10, MinimumLength = 2)]
        [DisplayName("Project Code")]
        public string Code { get; set; }

        [Required(ErrorMessage = "Position Name Required")]
        [StringLength(255, MinimumLength = 2)]
        [DisplayName("Project Name")]
        public string Name { get; set; }

        [StringLength(255)]
        [DisplayName("Description")]
        public string Description { get; set; }


        [Column(TypeName = "date")]
        [Required(ErrorMessage = "Start Date Required")]
        [DisplayName("Start Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy/MM/dd}")]
        public DateTime StartDate { get; set; }

        [Column(TypeName = "date")]
        [DisplayName("End Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        public DateTime? EndDate { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public int? Duration { get; set; }

        [Range(1, Int32.MaxValue, ErrorMessage = "Please select Business Unit")]
        [Required(ErrorMessage = "Please select Business Unit")]
        [DisplayName("Business Unit")]
        public int Project_BusinessUnit { get; set; }


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

        public virtual BusinessUnit BusinessUnit { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EmployeeProject> EmployeeProjects { get; set; }
    }
}
