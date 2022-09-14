using Smits.Etg.FileRepositorySystem.DL;
using Smits.Etg.FileRepositorySystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smits.Etg.FileRepositorySystem.BL
{
    public class DepartmentBL
    {
        private DepartmentDL deptdl;

        public IEnumerable<Department> GetAllDepartmentListForDropDown()
        {
            deptdl = new DepartmentDL();
            return deptdl.GetAllDepartmentListForDropDown();
        }

        public IEnumerable<Department> GetAllDepartmentList()
        {
            deptdl = new DepartmentDL();
            return deptdl.GetAllDepartmentList();
        }

        public Department FindDepartmentById(int? Id)
        {
            deptdl = new DepartmentDL();
            return deptdl.FindDepartmentById(Id);
        }

        public IEnumerable<Department> GetListOfEmployeePerDepartmentByCode(string dcode)
        {
            deptdl = new DepartmentDL();
            return deptdl.GetListOfEmployeePerDepartmentByCode(dcode);
        }


        #region Validation
        public bool IsDepartmentCodeExist(string deptCode, bool forUpdate = false, int? Id = 0)
        {
            deptdl = new DepartmentDL();
            return deptdl.IsDepartmentCodeExist(deptCode, forUpdate, Id);
        }
        public bool IsDepartmentNameExist(string deptName, bool forUpdate = false, int? Id = 0)
        {
            deptdl = new DepartmentDL();
            return deptdl.IsDepartmentNameExist(deptName, forUpdate, Id);
        }

        public bool IsDeparmtentExistById(int? id = 0)
        {
            deptdl = new DepartmentDL();
            return deptdl.IsDeparmtentExistById(id);

        }

        #endregion

        #region CRUD

        public int CreateDepartment(Department department)
        {
            deptdl = new DepartmentDL();
            return deptdl.CreateDepartment(department);
        }
        public int UpdateDepartment(Department department)
        {
            deptdl = new DepartmentDL();
            return deptdl.UpdateDepartment(department);
        }

        public int DeleteDepartment(int id)
        {
            deptdl = new DepartmentDL();
            return deptdl.DeleteDepartment(id);
        }
        public int sp_DeleteDepartment(int? id)
        {
            deptdl = new DepartmentDL();
            return deptdl.sp_DeleteDepartment(id);
        }
        #endregion



    }

}
