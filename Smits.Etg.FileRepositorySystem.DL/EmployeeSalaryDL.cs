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
    public class EmployeeSalaryDL
    {
        private Entities db;
        public IEnumerable<EmployeeSalary> GetEmployeeSalaryHistory(int? id)
        {
            using (db = new Entities())
            {
                var employeesalary = db.EmployeeSalaries.Include(e => e.Employee).Include(d => d.Employee.Department).Include(p => p.Employee.Position).Where(e => e.EmployeeSalary_EmpId == id).OrderByDescending(e => e.Created);

                return employeesalary.ToList();
            }
        }

        public EmployeeSalary GetLatestSalary(int? id)
        {
            using (db = new Entities())
            {
                var employeesalary = db.EmployeeSalaries.Include(e => e.Employee).Include(d => d.Employee.Department).Include(p => p.Employee.Position).Where(e => e.EmployeeSalary_EmpId == id).OrderByDescending(e => e.Created);

                return employeesalary.FirstOrDefault();
            }
        }

        public int CreateSalary(EmployeeSalary employeeSalary)
        {

            using (db = new Entities())
            {

                db.EmployeeSalaries.Add(employeeSalary);
                db.SaveChanges();
                return employeeSalary.Id;
            }
        }
    }
}
