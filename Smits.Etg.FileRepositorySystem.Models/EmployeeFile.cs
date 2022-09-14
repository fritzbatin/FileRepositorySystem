namespace Smits.Etg.FileRepositorySystem.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class EmployeeFile
    {
        public int Id { get; set; }


        [Display(Name = "File name")]
        [Required]
        [StringLength(255)]
        public string FileName { get; set; }

        [Display(Name = "File Bytes")]
        [Required]
        public byte[] FileBytes { get; set; }
        [Display(Name = "Content Type")]
        [StringLength(255)]
        public string ContentType { get; set; }

        [Display(Name = "Employee Id")]
        public int EmployeeId { get; set; }

        public DateTimeOffset? Created { get; set; }

        [Display(Name = "Created by")]
        [StringLength(255)]
        public string CreatedBy { get; set; }

        public DateTimeOffset? Modified { get; set; }

        [StringLength(255)]
        public string ModifiedBy { get; set; }

        [Column(TypeName = "timestamp")]
        [MaxLength(8)]
        [Timestamp]
        public byte[] RowVersion { get; set; }

        public virtual Employee Employee { get; set; }
    }
}
