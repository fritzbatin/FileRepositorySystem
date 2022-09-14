using Smits.Etg.FileRepositorySystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace Smits.Etg.FileRepositorySystem.DL
{
    public class DepartmentDL
    {
        private Entities db;
        public IEnumerable<Department> GetAllDepartmentListForDropDown()
        {
            using(db = new Entities())
            {
                var dept = db.Departments.OrderBy(e => e.Code);
                return dept.ToList();
            }
        }

        public IEnumerable<Department> GetAllDepartmentList()
        {
            using (db = new Entities())
            {
                var dept = db.Departments.OrderByDescending(e => (e.Modified.HasValue) ? e.Modified : e.Created).ToList();
                return dept.ToList();
            }
        }
      

        public Department FindDepartmentById(int? Id)
        {
            using (db = new Entities())
            {
                Department department =  db.Departments.Find(Id);
                return department;
            }
        }

        public IEnumerable<Department> GetListOfEmployeePerDepartmentByCode(string dcode)
        {
            using (db = new Entities())
            {
                var department = db.Departments.Include(e => e.Employees).Where(e => e.Code == dcode).OrderByDescending(e => (e.Modified.HasValue) ? e.Modified : e.Created).ToList();
                return department;
            }

        }

        #region Validation
        public bool IsDeparmtentExistById(int? id = 0)
        {
            using (db = new Entities())
            {
                bool status = db.Departments.Any(e => e.Id == id);
                return status;
            }

        }

        public bool IsDepartmentNameExist(string deptName, bool forUpdate = false, int? Id = 0)
        {
            using (db = new Entities())
            {
                bool status = true;
                if (forUpdate == false)
                {
                    Department depratment = db.Departments.Where(d => d.Name.ToLower() == deptName.ToLower()).FirstOrDefault();
                    if (depratment != null)
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
                    Department depratment = db.Departments.Where(d => d.Id == Id).FirstOrDefault();
                    var currentDepratmentName = depratment.Name.ToString();
                    if (deptName == currentDepratmentName)
                    {
                        status = true;
                    }
                    else
                    {
                        depratment = db.Departments.Where(d => d.Name.ToLower() == deptName.ToLower()).FirstOrDefault();
                        if (depratment != null)
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


        public bool IsDepartmentCodeExist(string deptCode, bool forUpdate = false, int? Id = 0)
        {
            using (db = new Entities())
            {
                bool status = true;
                if (forUpdate == false)
                {
                    Department depratment = db.Departments.Where(d => d.Code.ToUpper() == deptCode.ToUpper()).FirstOrDefault();
                    if (depratment != null)
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
                    Department depratment = db.Departments.Where(d => d.Id == Id).FirstOrDefault();
                    var currentDepratmentCode = depratment.Code.ToString();
                    if (deptCode == currentDepratmentCode)
                    {
                        status = true;
                    }
                    else
                    {
                        depratment = db.Departments.Where(d => d.Code.ToUpper() == deptCode.ToUpper()).FirstOrDefault();
                        if (depratment != null)
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
        public int CreateDepartment(Department department)
        {
            using (db = new Entities())
            {

                db.Departments.Add(department);
                db.SaveChanges();
                return department.Id;
            }
        }

        public int UpdateDepartment(Department department)
        {
            using (db = new Entities())
            {
                var dept = db.Departments.Find(department.Id);
                if (dept != null)
                {
                    dept.Code = department.Code;
                    dept.Name = department.Name;
                    dept.Modified = department.Modified;
                    dept.ModifiedBy = department.ModifiedBy;

                    db.SaveChanges();
                    return department.Id;
                }
                else
                {
                    return 0;
                }

            }
        }

        public int sp_DeleteDepartment(int? id)
        {
            using (db = new Entities())
            {

                var idPar = new SqlParameter("Id", SqlDbType.Int);
                idPar.Value = id;

                var idParameter = id.HasValue ?
                    new SqlParameter("Id", id) :
                    new SqlParameter("Id", typeof(int));
                db.Database.SqlQuery<Entities>("exec DEPT_DeleteUnusedExistingRec @Id", idParameter).SingleOrDefault();
                var result = db.Departments.Find(id);
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

        public int DeleteDepartment(int id)
        {
            using (db = new Entities())
            {
                try
                {
                    Department dept = db.Departments.Find(id);
                    if (dept != null)
                    {
                        db.Departments.Remove(dept);
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
