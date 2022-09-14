using Smits.Etg.FileRepositorySystem.DL;
using Smits.Etg.FileRepositorySystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smits.Etg.FileRepositorySystem.BL
{
    public class EmployeeFileBL
    {
        private EmployeeFileDL _empfdl;
        public int CreateEmployeeFile(EmployeeFile employeefile)
        {
            _empfdl = new EmployeeFileDL();
            return _empfdl.CreateEmployeeFile(employeefile);
        }
        public IEnumerable<EmployeeFile> GetAllEmployeeFileById(int? id)
        {
            _empfdl = new EmployeeFileDL();
            return _empfdl.GetAllEmployeeFileById(id);
        }
        public EmployeeFile DownloadFile(int? fileId)
        {
            _empfdl = new EmployeeFileDL();
            return _empfdl.DownloadFile(fileId);
        }
        public EmployeeFile FindEmployeeFileById(int? id)
        {
            _empfdl = new EmployeeFileDL();
            return _empfdl.FindEmployeeFileById(id);
        }

        public int DeleteEmployeeFile(EmployeeFile employeefile)
        {
            _empfdl = new EmployeeFileDL();
            return _empfdl.DeleteEmployeeFile(employeefile);
        }
    }
}
