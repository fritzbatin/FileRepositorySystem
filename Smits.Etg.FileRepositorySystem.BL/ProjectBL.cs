using Smits.Etg.FileRepositorySystem.DL;
using Smits.Etg.FileRepositorySystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smits.Etg.FileRepositorySystem.BL
{
    public class ProjectBL
    {
        private ProjectDL _pdl;

        public IEnumerable<Project> GetAllProjectDropDownList()
        {
            _pdl = new ProjectDL();
            return _pdl.GetAllProjectDropDownList();
        }

        public IEnumerable<Project> GetAllProjectList()
        {
            _pdl = new ProjectDL();
            return _pdl.GetAllProjectList();
        }
        public Project FindProjectDetails(int? id)
        {
            _pdl = new ProjectDL();
            return _pdl.FindProjectDetails(id);
        }

        #region Validation
        public bool IsIdExist(int? id)
        {
            _pdl = new ProjectDL();
            return _pdl.IsIdExist(id);
        }
        public bool IsProjectCodeExist(string projectCode, bool forUpdate = false, int? Id = 0)
        {
            _pdl = new ProjectDL();
            return _pdl.IsProjectCodeExist(projectCode, forUpdate, Id);
        }
        #endregion

        #region CRUD
        public int CreatePRoject(Project project)
        {
            _pdl = new ProjectDL();
            return _pdl.CreatePRoject(project);
        }

        public int UpdateProject(Project project)
        {
            _pdl = new ProjectDL();
            return _pdl.UpdateProject(project);
        }

        public int sp_DeleteProject(int? id)
        {
            _pdl = new ProjectDL();
            return _pdl.sp_DeleteProject(id);
        }

        public int DeleteProject(int id)
        {
            _pdl = new ProjectDL();
            return _pdl.DeleteProject(id);
        }
        #endregion
    }
}
