namespace Smits.Etg.FileRepositorySystem.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class UserActivityLog
    {
        public int Id { get; set; }

        public Guid UniqueKey { get; set; }

        [Required]
        public string Activity { get; set; }

        [Required]
        [StringLength(50)]
        public string MethodType { get; set; }

        [Required]
        public string AbsoluteUrl { get; set; }

        [StringLength(255)]
        public string UserAgent { get; set; }

        [StringLength(255)]
        public string ClientIPAddress { get; set; }

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
    }
}
