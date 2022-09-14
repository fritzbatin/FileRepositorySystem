using Smits.Etg.FileRepositorySystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Data;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;

namespace Smits.Etg.FileRepositorySystem.DL
{
    public class EmployeeDL
    {
        private Entities db;
        public IEnumerable<Employee> GetEmployeeAndEmployeeProjectDetails(int? EmployeeProject_Employee)
        {
            using (db = new Entities())
            {
                var employee = db.Employees.Where(e => e.Id == EmployeeProject_Employee);
                return employee.ToList();
            }

        }

        public IEnumerable<Employee> GetAllEmployeeListForDropDown()
        {
            using (db = new Entities())
            {
                var emp = db.Employees.OrderByDescending(e => e.FirstName);
                return emp.ToList();
            }
        }


        public List<Employee> GetAllEmployeeListForExport()
        {
            using (db = new Entities())
            {
                var emp = db.Employees.Include(d => d.Department).Include(p => p.Position);
                return emp.ToList();
            }

        }
        public IEnumerable<Employee> GetAllEmployeeList()
        {
            using(db = new Entities())
            {
                var emp = db.Employees.Include(d => d.Department).Include(p => p.Position).OrderByDescending(e => e.Modified.HasValue ? e.Modified : e.Created);
                return emp.ToList();
            }
        }

        public Employee GetEmployeeDetails(int? id)
        {
            using (db = new Entities())
            {
                var employee = db.Employees.Include(e => e.Department).Include(e => e.Position).Where(e => e.Id == id)
                    .Include(e => e.EmployeeSalaries).Include(e => e.EmployeeFiles).FirstOrDefault();
                return employee;
            }
        }

        public Employee GetEmployeeDetails(string employeeId = "")
        {
            using (db = new Entities())
            {
                var employee = db.Employees.Include(e => e.Department).Include(e => e.Position).Where(e => e.EmployeeId == employeeId)
                     .Include(e => e.EmployeeSalaries).Include(e => e.EmployeeFiles).FirstOrDefault();
                return employee;
            }
        }

        #region Validation


        public class PositionResult
        {
            public bool result { get; set; }
            public int posId { get; set; }

        }

        public PositionResult IsPositionNameIdValidByPositionName(string PositionName)
        {
            using (db = new Entities())
            {
                Position position = db.Positions.Where(p => p.Name == PositionName).FirstOrDefault();
                //bool status;
                PositionResult values = new PositionResult();

                if (position == null)
                {
                    values.posId = 0;
                    values.result = false;
                }
                else
                {
                    values.result = true;
                    values.posId = position.Id;
                }

                return values;
            }
            
        }

        public class DepartmentResult
        {
            public bool result { get; set; }
            public int deptId { get; set; }

        }
        public DepartmentResult IsDepartmentCodeIdValid(string DepartmentCode)
        {
            using (db = new Entities())
            {
                Department department = db.Departments.Where(d => d.Code == DepartmentCode).FirstOrDefault();
                DepartmentResult values = new DepartmentResult();

                if (department == null)
                {
                    values.deptId = 0;
                    values.result = false;
                }
                else
                {
                    values.result = true;
                    values.deptId = department.Id;
                }


                return values;
            }
               
        }

        public bool IsEmployeeExistById(int? id = 0)
        {
            using (db = new Entities())
            {
                bool status = db.Employees.Any(e => e.Id == id);
                return status;
            }
        }

        public bool IsNameIsValid(string fname, string lname, bool forUpdate = false, int? Id = 0)
        {
            using (db = new Entities())
            {
                bool status = false;

                if (forUpdate == false)
                {
                    if (string.IsNullOrEmpty(fname) || string.IsNullOrEmpty(lname))
                    {
                        status = false;
                    }
                    else
                    {
                        //If Employee First name and Last name Exist, status should be false unless true
                        var employee = db.Employees.Where(e => e.FirstName.ToLower() == fname.ToLower() && e.LastName.ToLower() == lname.ToLower()).FirstOrDefault();

                        if (employee != null)
                        {
                            status = false;
                        }
                        else
                        {
                            status = true;
                        }

                    }
                }
                else
                {
                    if (string.IsNullOrEmpty(fname) || string.IsNullOrEmpty(lname))
                    {
                        status = false;
                    }
                    else
                    {
                        var employee = db.Employees.Where(e => e.Id == Id).FirstOrDefault();

                        var currentFirstname = employee.FirstName.ToString();
                        var currentLastname = employee.LastName.ToString();

                        if (fname == currentFirstname && lname == currentLastname)
                        {
                            status = true;
                        }
                        else
                        {
                            var checkResult = db.Employees.Where(e => e.FirstName.ToLower() == fname.ToLower() && e.LastName.ToLower() == lname.ToLower()).FirstOrDefault();
                            if (checkResult != null)
                            {
                                status = false;
                            }
                            else
                            {
                                status = true;
                            }
                        }
                    }
                }

                return status;
            }
        }

        public bool IsEmployeeIdExist(string EmpId, bool forUpdate = false, int? Id = 0)
        {
            using (db = new Entities())
            {
                bool status = true;
                var employee = db.Employees.Where(e => e.EmployeeId.ToLower() == EmpId.ToLower()).FirstOrDefault();

                if (forUpdate == false)
                {
                    if (employee != null)
                    {
                        status = false;
                    }
                    else
                    {
                        status = true;
                    }
                }
                return status;
            }
        }

        public bool IsEmailAddressExist(string EmailAdd, bool forUpdate = false, int? Id = 0)
        {
            using (db = new Entities())
            {
                bool status = false;
                if (forUpdate == false)
                {
                    var employee = db.Employees.Where(e => e.EmailAddress.ToLower() == EmailAdd.ToLower()).FirstOrDefault();
                    if (employee != null)
                    {
                        status = false;
                    }
                    else
                    {
                        status = true;
                    }
                }
                else
                {
                    var employee = db.Employees.Where(e => e.Id == Id).FirstOrDefault();
                    var currentEmailAdd = employee.EmailAddress.ToString();

                    if (EmailAdd == currentEmailAdd)
                    {
                        status = true;
                    }
                    else
                    {
                        employee = db.Employees.Where(e => e.EmailAddress.ToLower() == EmailAdd.ToLower()).FirstOrDefault();
                        if (employee != null)
                        {
                            status = false;
                        }
                        else
                        {
                            status = true;
                        }
                    }
                }

                return status;
            }
            
        }


        #endregion

        #region CRUD
        public int CreateEmployee(Employee employee)
        {

            using (db = new Entities())
            {

                db.Employees.Add(employee);
                db.SaveChanges();
                return employee.Id;
            }
        }

        public int UpdateEmployee(Employee employee)
        {
            using (db = new Entities())
            {
                var emp = db.Employees.Find(employee.Id);
                if (emp != null)
                {
                    emp.FirstName = employee.FirstName;
                    emp.MiddleName = employee.MiddleName;
                    emp.LastName = employee.LastName;
                    emp.EmailAddress = employee.EmailAddress;
                    emp.Employee_Position = employee.Employee_Position;
                    emp.Employee_Department = employee.Employee_Department;
                    emp.Modified = employee.Modified;
                    emp.ModifiedBy = employee.ModifiedBy;

                    db.SaveChanges();
                    return employee.Id;
                }
                else
                {
                    return 0;
                }
               
            }
        }

        public int sp_DeleteEmployee(int? id)
        {
           

            using (db = new Entities())
            {

                var idPar = new SqlParameter("Id", SqlDbType.Int);
                idPar.Value = id;

                var idParameter = id.HasValue ?
                    new SqlParameter("Id", id) :
                    new SqlParameter("Id", typeof(int));
                db.Database.SqlQuery<Entities>("exec EMP_DeleteExistingRecord @Id", idParameter).SingleOrDefault();
                var result = db.Employees.Find(id);
                if (result == null)
                {
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
        }

        public int DeleteEmployee(int id)
        {
            using (db = new Entities())
            {
                try
                {
                    Employee emp = db.Employees.Find(id);
                    if (emp != null)
                    {
                        db.Employees.Remove(emp);
                        db.SaveChanges();
                        return 1;
                    }
                    else
                    {
                        return 0;
                    }
                }
                catch (Exception ex)
                {
                    return 0;
                }


            }
        }


        #endregion
    }
}
