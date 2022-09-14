using Smits.Etg.FileRepositorySystem.DL;
using Smits.Etg.FileRepositorySystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smits.Etg.FileRepositorySystem.BL
{
    public class EmployeeBL
    {
        private EmployeeDL empdl;
        public IEnumerable<Employee> GetEmployeeAndEmployeeProjectDetails(int? EmployeeProject_Employee)
        {
            empdl = new EmployeeDL();
            return empdl.GetEmployeeAndEmployeeProjectDetails(EmployeeProject_Employee);
        }
        public IEnumerable<Employee> GetAllEmployeeListForDropDown()
        {
            empdl = new EmployeeDL();
            return empdl.GetAllEmployeeListForDropDown();
        }

        public List<Employee> GetAllEmployeeListForExport()
        {
            empdl = new EmployeeDL();
            return empdl.GetAllEmployeeListForExport();
        }

        public IEnumerable<Employee> GetAllEmployeeList()
        {
            empdl = new EmployeeDL();
            return empdl.GetAllEmployeeList();
        }
        public Employee GetEmployeeDetails(int? id)
        {
            empdl = new EmployeeDL();
            return empdl.GetEmployeeDetails(id);
        }

        public Employee GetEmployeeDetails(string employeeId = "")
        {
            empdl = new EmployeeDL();
            return empdl.GetEmployeeDetails(employeeId);
        }

        #region Validation
        public class PositionResult
        {
            public bool result { get; set; }
            public int posId { get; set; }

        }
        public PositionResult IsPositionNameIdValidByPositionName(string PositionName)
        {
            empdl = new EmployeeDL();
            PositionResult values = new PositionResult();

            values.result = empdl.IsPositionNameIdValidByPositionName(PositionName).result;
            values.posId = empdl.IsPositionNameIdValidByPositionName(PositionName).posId;

            return values;
        }

        public class DepartmentResult
        {
            public bool result { get; set; }
            public int deptId { get; set; }

        }
        public DepartmentResult IsDepartmentCodeIdValid(string DepartmentCode)
        {
            empdl = new EmployeeDL();
            DepartmentResult values = new DepartmentResult();

            values.result = empdl.IsDepartmentCodeIdValid(DepartmentCode).result;
            values.deptId = empdl.IsDepartmentCodeIdValid(DepartmentCode).deptId;

            return values;
        }
        public bool IsEmployeeExistById(int? id = 0)
        {
            empdl = new EmployeeDL();
            return empdl.IsEmployeeExistById(id);
        }
        public bool IsEmployeeIdExist(string EmpId, bool forUpdate = false, int? Id = 0)
        {
            empdl = new EmployeeDL();
            return empdl.IsEmployeeIdExist(EmpId, forUpdate, Id);
        }

        public bool IsNameIsValid(string fname, string lname, bool forUpdate = false, int? Id = 0)
        {
            empdl = new EmployeeDL();
            return empdl.IsNameIsValid(fname, lname, forUpdate, Id);
        }
        public bool IsEmailAddressExist(string EmailAdd, bool forUpdate = false, int? Id = 0)
        {
            empdl = new EmployeeDL();
            return empdl.IsEmailAddressExist(EmailAdd, forUpdate, Id);
        }


        #endregion

        #region CRUD
        public int CreateEmployee(Employee employee)
        {
            empdl = new EmployeeDL();
            return empdl.CreateEmployee(employee);
        }

        public int UpdateEmployee(Employee employee)
        {
            empdl = new EmployeeDL();
            return empdl.UpdateEmployee(employee);
        }

        public int DeleteEmployee(int id)
        {
            empdl = new EmployeeDL();
            return empdl.DeleteEmployee(id);
        }

        public int sp_DeleteEmployee(int id)
        {
            empdl = new EmployeeDL();
            return empdl.sp_DeleteEmployee(id);
        }


            #endregion

    }
}
