namespace Smits.Etg.FileRepositorySystem.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class RolePermission
    {
        public int Id { get; set; }

        public Guid UniqueKey { get; set; }

        [StringLength(255)]
        public string CreatedBy { get; set; }

        public DateTimeOffset? Created { get; set; }

        [StringLength(255)]
        public string ModifiedBy { get; set; }

        public DateTimeOffset? Modified { get; set; }

        [Column(TypeName = "timestamp")]
        [MaxLength(8)]
        [Timestamp]
        public byte[] RowVersion { get; set; }

        [Required]
        [StringLength(128)]
        public string RolePermission_Role { get; set; }

        public int RolePermission_ModuleAccess { get; set; }

        public virtual AspNetRole AspNetRole { get; set; }

        public virtual ModuleAccess ModuleAccess { get; set; }
    }
}
