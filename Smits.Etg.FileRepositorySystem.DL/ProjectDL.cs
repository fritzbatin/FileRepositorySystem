using Smits.Etg.FileRepositorySystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Data;

namespace Smits.Etg.FileRepositorySystem.DL
{
   
    public class ProjectDL
    {
        private Entities db;

        public IEnumerable<Project> GetAllProjectDropDownList()
        {
            using (db = new Entities())
            {
                var projects = db.Projects.Include(p => p.BusinessUnit);
                return projects.OrderBy(p => p.Code).ToList();
            }
        }
        public IEnumerable<Project> GetAllProjectList()
        {
            using(db = new Entities())
            {
                var projects = db.Projects.Include(p => p.BusinessUnit);
                return  projects.OrderByDescending(e => (e.Modified.HasValue) ? e.Modified : e.Created).ToList();
            }
        }

        public Project FindProjectDetails(int? id)
        {
            using (db = new Entities())
            {
                var project =  db.Projects.Include(p => p.BusinessUnit).Where(p => p.Id == id).FirstOrDefault();
                return project;
            }
        }

        #region Validation
        public bool IsIdExist(int? id)
        {
            using (db = new Entities())
            {
                bool status = db.Projects.Any(e => e.Id == id);
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

        public bool IsProjectCodeExist(string projectCode, bool forUpdate = false, int? Id = 0)
        {
            using (db = new Entities())
            {
                bool status = true;
                if (forUpdate == false)
                {
                    Project project = db.Projects.Where(d => d.Code.ToUpper() == projectCode.ToUpper()).FirstOrDefault();
                    if (project != null)
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
                    Project project = db.Projects.Where(d => d.Id == Id).FirstOrDefault();
                    var currentDepratmentCode = project.Code.ToString();
                    if (projectCode == currentDepratmentCode)
                    {
                        status = true;
                    }
                    else
                    {
                        project = db.Projects.Where(d => d.Code.ToUpper() == projectCode.ToUpper()).FirstOrDefault();
                        if (project != null)
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
        public int CreatePRoject(Project project)
        {
            using (db = new Entities())
            {
                db.Projects.Add(project);
                db.SaveChanges();
                return project.Id;
            }
        }

        public int UpdateProject(Project project)
        {
            using (db = new Entities())
            {
                var pro = db.Projects.Find(project.Id);
                if (pro != null)
                {
                    pro.Code = project.Code;
                    pro.Name = project.Name;
                    pro.Description = project.Description;
                    pro.StartDate = project.StartDate;
                    pro.EndDate = project.EndDate;
                    pro.Project_BusinessUnit = project.Project_BusinessUnit;
                    pro.Modified = project.Modified;
                    pro.ModifiedBy = project.ModifiedBy;

                    var updated = db.SaveChanges();

                    return updated;
                }
                else
                {
                    return 0;
                }

            }
        }
        public int DeleteProject(int id)
        {
            using (db = new Entities())
            {
                try
                {
                    Project proj = db.Projects.Find(id);
                    if (proj != null)
                    {
                        db.Projects.Remove(proj);
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
        public int sp_DeleteProject(int? id)
        {
            using (db = new Entities())
            {

                var idPar = new SqlParameter("Id", SqlDbType.Int);
                idPar.Value = id;

                var idParameter = id.HasValue ?
                    new SqlParameter("Id", id) :
                    new SqlParameter("Id", typeof(int));
                db.Database.SqlQuery<Entities>("exec PROJECT_DeleteExistingRecord @Id", idParameter).SingleOrDefault();
                var result = db.Projects.Find(id);
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
        #endregion


    }
}
