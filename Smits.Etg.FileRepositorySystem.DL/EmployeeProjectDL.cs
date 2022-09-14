using Smits.Etg.FileRepositorySystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Data;
using System.Data.SqlClient;

namespace Smits.Etg.FileRepositorySystem.DL
{
    public class EmployeeProjectDL
    {
        private Entities db;

        public IEnumerable<EmployeeProject> GetAllEmployeeProjectList()
        {
            using (db = new Entities())
            {
                var employeeProjects = db.EmployeeProjects.Include(e => e.Employee).Include(e => e.Project).Include(e => e.Employee.Department).Include(e => e.Employee.Position);
                return employeeProjects.OrderByDescending(e => (e.Modified.HasValue) ? e.Modified : e.Created).ToList();
            }
                
        }

        public EmployeeProject GetEmployeeProjectDetails(int? id)
        {
            using (db = new Entities())
            {
                EmployeeProject employeeProject = db.EmployeeProjects
                    .Include(e => e.Employee)
                    .Include(e => e.Project)
                    .Include(e => e.Employee.Department)
                    .Include(e => e.Employee.Position)
                    .Where(ep => ep.Id == id)
                    .FirstOrDefault();
                return employeeProject;
            }
            
        }


        #region Validation
        public bool IsIdExist(int? id)
        {
            using (db = new Entities())
            {
                bool status = db.EmployeeProjects.Any(e => e.Id == id);
                if (status) // its a new object
                {
                    return true;
                }
                else // its an existing object so exclude existing objects with the id
                {
                    return false;
                }
            }

        }

        public bool IsProjectAvailableForEmployee(int ProjectId, int EmployeeId, bool forUpdate = false, int? Id = 0)
        {
            using (db = new Entities())
            {
                bool status = true;
                if (forUpdate == false)
                {
                    EmployeeProject employeeproject = db.EmployeeProjects.Where(ep => ep.EmployeeProject_Project == ProjectId && ep.EmployeeProject_Employee == EmployeeId).FirstOrDefault();
                    if (employeeproject != null)
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
                    EmployeeProject employeeproject = db.EmployeeProjects.Where(ep => ep.Id == Id).FirstOrDefault();
                    var currentProject = employeeproject.EmployeeProject_Project;
                    var currentEmployee = employeeproject.EmployeeProject_Employee;

                    if (ProjectId == currentProject && EmployeeId == currentEmployee)
                    {
                        status = true;
                    }
                    else
                    {
                        employeeproject = db.EmployeeProjects.Where(ep => ep.EmployeeProject_Project == ProjectId && ep.EmployeeProject_Employee == EmployeeId).FirstOrDefault();
                        if (employeeproject != null)
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
        public int CreateEmployeeProject(EmployeeProject employeeproject)
        {

            using (db = new Entities())
            {

                db.EmployeeProjects.Add(employeeproject);
                db.SaveChanges();
                return employeeproject.Id;
            }
        }

        public int UpdateEmployeeProject(EmployeeProject employeeproject)
        {
            using (db = new Entities())
            {
                var empProject = db.EmployeeProjects.Find(employeeproject.Id);
                if (empProject != null)
                {
                    empProject.EmployeeProject_Employee = employeeproject.EmployeeProject_Employee;
                    empProject.EmployeeProject_Project = employeeproject.EmployeeProject_Project;
                    empProject.Modified = employeeproject.Modified;
                    empProject.ModifiedBy = employeeproject.ModifiedBy;

                    db.SaveChanges();
                    return empProject.Id;
                }
                else
                {
                    return 0;
                }

            }
        }

        public int DeleteEmployeeProject(int? id)
        {
            using (db = new Entities())
            {
                EmployeeProject employeeProject = db.EmployeeProjects.Find(id);
                db.EmployeeProjects.Remove(employeeProject);

                return db.SaveChanges();
            }
        }


        #endregion

    }
}
