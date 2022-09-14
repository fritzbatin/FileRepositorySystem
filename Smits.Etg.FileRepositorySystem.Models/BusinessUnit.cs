namespace Smits.Etg.FileRepositorySystem.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Web.Mvc;

    public partial class BusinessUnit
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public BusinessUnit()
        {
            Projects = new HashSet<Project>();
        }

        public int Id { get; set; }

        [Remote("IsBusinessUnitCodeValid", "BusinessUnit", AdditionalFields = "Id", ErrorMessage = "Business Unit Code already exists.")]
        [StringLength(10)]
        [Required(ErrorMessage = "Business Code Required")]
        [DisplayName("Business Code")]
        public string Code { get; set; }

        [StringLength(255)]
        [Required(ErrorMessage = "Business Unit Required")]
        [DisplayName("Business Unit")]
        public string Name { get; set; }

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

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Project> Projects { get; set; }
    }
}
