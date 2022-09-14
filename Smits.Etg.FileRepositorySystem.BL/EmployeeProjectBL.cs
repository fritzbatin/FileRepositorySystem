using Smits.Etg.FileRepositorySystem.DL;
using Smits.Etg.FileRepositorySystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smits.Etg.FileRepositorySystem.BL
{
    public class EmployeeProjectBL
    {
        private EmployeeProjectDL _empprojectDL;
        public IEnumerable<EmployeeProject> GetAllEmployeeProjectList()
        {
            _empprojectDL = new EmployeeProjectDL();
            return _empprojectDL.GetAllEmployeeProjectList();
        }
        public EmployeeProject GetEmployeeProjectDetails(int? id)
        {
            _empprojectDL = new EmployeeProjectDL();
            return _empprojectDL.GetEmployeeProjectDetails(id);
        }

        #region Validation
        public bool IsIdExist(int? id)
        {
            _empprojectDL = new EmployeeProjectDL();
            return _empprojectDL.IsIdExist(id);

        }

        public bool IsProjectAvailableForEmployee(int ProjectId, int EmployeeId, bool forUpdate = false, int? Id = 0)
        {
            _empprojectDL = new EmployeeProjectDL();
            return _empprojectDL.IsProjectAvailableForEmployee(ProjectId,EmployeeId,forUpdate,Id);
        }
        #endregion



        #region CRUD
        public int CreateEmployeeProject(EmployeeProject employeeproject)
        {
            _empprojectDL = new EmployeeProjectDL();
            return _empprojectDL.CreateEmployeeProject(employeeproject);
        }

        public int UpdateEmployeeProject(EmployeeProject employeeproject)
        {
            _empprojectDL = new EmployeeProjectDL();
            return _empprojectDL.UpdateEmployeeProject(employeeproject);
        }

        public int DeleteEmployeeProject(int? id)
        {
            _empprojectDL = new EmployeeProjectDL();
            return _empprojectDL.DeleteEmployeeProject(id);
        }


        #endregion
    }
}
