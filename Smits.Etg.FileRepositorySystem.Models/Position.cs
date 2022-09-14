namespace Smits.Etg.FileRepositorySystem.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Web.Mvc;

    public partial class Position
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Position()
        {
            Employees = new HashSet<Employee>();
        }

        public int Id { get; set; }

        [Remote("IsPositionNameValid", "Position", AdditionalFields = "Id", ErrorMessage = "Position Name already exists.")]
        [StringLength(255, MinimumLength = 2)]
        [Required(ErrorMessage = "Position Required")]
        [DisplayName("Position")]
        public string Name { get; set; }

        [StringLength(255)]
        [DisplayName("Description")]
        public string Description { get; set; }

        [DisplayName("Created")]
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
        public virtual ICollection<Employee> Employees { get; set; }
    }
}
