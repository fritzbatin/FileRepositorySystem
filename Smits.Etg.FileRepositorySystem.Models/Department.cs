namespace Smits.Etg.FileRepositorySystem.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Web.Mvc;

    public partial class Department
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Department()
        {
            Employees = new HashSet<Employee>();
        }

        public int Id { get; set; }

        [StringLength(10, MinimumLength = 2)]
        [Required(ErrorMessage = "Department code required")]
        [DisplayName("Department Code")]
        [Remote("IsDepartmentCodeExist", "Department", AdditionalFields = "Id", ErrorMessage = "Department Code already exists.")]
        public string Code { get; set; }

        [StringLength(255, MinimumLength = 3)]
        [DisplayName("Department")]
        [Required(ErrorMessage = "Department name required")]
        public string Name { get; set; }

        public DateTimeOffset? Created { get; set; }

        [StringLength(255)]
        public string CreatedBy { get; set; }

        public DateTimeOffset? Modified { get; set; }

        [StringLength(255)]
        public string ModifiedBy { get; set; }

        [Column(TypeName = "timestamp")]
        [MaxLength(8)]
        [Timestamp]
        public byte[] RowVersion { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Employee> Employees { get; set; }
    }
}
