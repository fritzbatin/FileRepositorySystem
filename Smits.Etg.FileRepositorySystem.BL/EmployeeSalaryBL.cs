using Smits.Etg.FileRepositorySystem.DL;
using Smits.Etg.FileRepositorySystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smits.Etg.FileRepositorySystem.BL
{
    public class EmployeeSalaryBL
    {
        private EmployeeSalaryDL empsaldl;

        public IEnumerable<EmployeeSalary> GetEmployeeSalaryHistory(int? id)
        {
            empsaldl = new EmployeeSalaryDL();
            return empsaldl.GetEmployeeSalaryHistory(id);
        }
        public EmployeeSalary GetLatestSalary(int? id)
        {
            empsaldl = new EmployeeSalaryDL();
            return empsaldl.GetLatestSalary(id);
        }

        public int CreateSalary(EmployeeSalary employeeSalary)
        {
            empsaldl = new EmployeeSalaryDL();
            return empsaldl.CreateSalary(employeeSalary);
        }

    }
}
