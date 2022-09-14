using Smits.Etg.FileRepositorySystem.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smits.Etg.FileRepositorySystem.Models
{
    [Table("vEmployeeListPerPosition")]
    public partial class vEmployeeListPerPosition
    {

        [DisplayName("Name")]
        public string FullName
        {
            get
            {
                return FirstName + " " + MiddleName + " " + LastName;
            }
        }

        [Key]
        public int Id { get; set; }

        [DisplayName("Employee Id")]
        public string EmployeeId { get; set; }

        [DisplayName("First Name")]
        public string FirstName { get; set; }

        [DisplayName("Middle Name")]
        public string MiddleName { get; set; }

        [DisplayName("Last Name")]
        public string LastName { get; set; }

        [DisplayName("Position")]
        public string Name { get; set; }

        public string Description { get; set; }

        public Nullable<System.DateTimeOffset> Created { get; set; }

        [DisplayName("Created By")]
        public string CreatedBy { get; set; }


        public Nullable<System.DateTimeOffset> Modified { get; set; }

        [DisplayName("Modified By")]
        public string ModifiedBy { get; set; }

      
    }
}
