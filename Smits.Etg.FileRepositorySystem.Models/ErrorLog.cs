namespace Smits.Etg.FileRepositorySystem.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ErrorLog
    {
        public int ID { get; set; }

        public long ErrorNumber { get; set; }

        [Required]
        public string ErrorMessage { get; set; }

        [Required]
        [StringLength(50)]
        public string MessageType { get; set; }

        [Required]
        [StringLength(500)]
        public string MethodName { get; set; }

        public string FriendlyErrorMessage { get; set; }

        public string Exception { get; set; }

        public DateTimeOffset? Created { get; set; }

        [StringLength(255)]
        public string Createdby { get; set; }

        public DateTimeOffset? Modified { get; set; }

        [StringLength(255)]
        public string Modifieddby { get; set; }

        [Column(TypeName = "timestamp")]
        [MaxLength(8)]
        [Timestamp]
        public byte[] RowVersion { get; set; }
    }
}
