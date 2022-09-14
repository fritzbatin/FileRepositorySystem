using Smits.Etg.FileRepositorySystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace Smits.Etg.FileRepositorySystem.DL
{
    public class EmployeeFileDL
    {
        private Entities db;

        public IEnumerable<EmployeeFile> GetAllEmployeeFileById(int? id)
        {
            using (db = new Entities())
            {
                var employeeFiles = db.EmployeeFiles.Include(e => e.Employee).Where(ef => ef.EmployeeId == id).OrderByDescending(e => (e.Modified.HasValue) ? e.Modified : e.Created).ToList();
                return employeeFiles;
            }
        }

        public EmployeeFile DownloadFile(int? fileId)
        {
            using (db = new Entities())
            {
                var employeeFiles = db.EmployeeFiles.ToList().Find(ef => ef.Id == fileId.Value);

                return employeeFiles;

            }
        }
        public EmployeeFile FindEmployeeFileById(int? id)
        {
            using (db = new Entities())
            {
                EmployeeFile employeeFile = db.EmployeeFiles.Find(id);
                return employeeFile; 
            }
        }
        #region CRUD

        public int CreateEmployeeFile(EmployeeFile employeefile)
        {
            using (db = new Entities())
            {
                db.EmployeeFiles.Add(employeefile);
                db.SaveChanges();
                return employeefile.Id;
            }
        }

        public int DeleteEmployeeFile(EmployeeFile employeefile)
        {
            using (db = new Entities())
            {
                try
                {
                    EmployeeFile empf = db.EmployeeFiles.Find(employeefile.Id);
                    if (empf != null)
                    {
                        db.EmployeeFiles.Remove(empf);
                        db.SaveChanges();
                        return 1;
                    }
                    else
                    {
                        return 0;
                    }
                }catch(Exception ex)
                {
                    return 0;
                }
              
                
            }
        }
        #endregion
    }
}
