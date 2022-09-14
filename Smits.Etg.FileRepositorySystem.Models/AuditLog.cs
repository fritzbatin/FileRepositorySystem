namespace Smits.Etg.FileRepositorySystem.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class AuditLog
    {
        public int Id { get; set; }

        public Guid UniqueKey { get; set; }

        [Required]
        [StringLength(256)]
        public string TableName { get; set; }

        [StringLength(50)]
        public string EventType { get; set; }

        [StringLength(256)]
        public string RowID { get; set; }

        [StringLength(256)]
        public string ColumnName { get; set; }

        public string OldValue { get; set; }

        public string NewValue { get; set; }

        public DateTimeOffset? Created { get; set; }

        [StringLength(256)]
        public string CreatedBy { get; set; }

        [Column(TypeName = "timestamp")]
        [MaxLength(8)]
        [Timestamp]
        public byte[] RowVersion { get; set; }
    }
}
