using LINQtoCSV;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smits.Etg.FileRepositorySystem.Models
{
    public class UploadEmployeeData
    {
        #region Required only for CSV
        //==============Required only for CSV
        [CsvColumn(Name = "Employee Id", FieldIndex = 1)]
        public string EmpId { get { return EmployeeId; } }

        [CsvColumn(Name = "First Name", FieldIndex = 2)]
        public string fName { get { return FirstName; } }

        [CsvColumn(Name = "Middle Name", FieldIndex = 3)]
        public string mName { get { return MiddleName; } }

        [CsvColumn(Name = "Last Name", FieldIndex = 4)]
        public string lName { get { return LastName; } }

        [CsvColumn(Name = "Email Address", FieldIndex = 5)]
        public string emailAdd { get { return EmailAddress; } }

        [CsvColumn(Name = "Position", FieldIndex = 6)]
        public string PosName { get { return Employee_Position; } }

        [CsvColumn(Name = "Department", FieldIndex = 7)]
        public string DeptName { get { return Employee_Department; } }
        //==============Required only for CSV
        #endregion


        [DisplayName("Employee Id")]
        public string EmployeeId { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }

        [DisplayName("Email address")]
        public string EmailAddress { get; set; }

        [DisplayName("Position")]
        public string Employee_Position { get; set; }

        [DisplayName("Department")]
        public string Employee_Department { get; set; }
        public string Remarks { get; set; }
    }
}
