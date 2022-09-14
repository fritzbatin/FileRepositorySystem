using Smits.Etg.FileRepositorySystem.BL;
using Smits.Etg.FileRepositorySystem.Models;
using Smits.Etg.FileRepositorySystem.Web.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Smits.Etg.FileRepositorySystem.Web.Controllers
{
    public class EmployeeProjectController : Controller
    {
        private EmployeeProjectBL _employeeprojectBL;
        private EmployeeBL _empbl;
        private ProjectBL _projectbl;

        #region Validation
      
        public JsonResult IsProjectAvailbleForEmployee(int EmployeeProject_Project, int EmployeeProject_Employee, int? Id = 0)
        {
            _employeeprojectBL = new EmployeeProjectBL();
            var IsEmpIdExist = _employeeprojectBL.IsIdExist(Id);
            var status = _employeeprojectBL.IsProjectAvailableForEmployee(EmployeeProject_Project, EmployeeProject_Employee, IsEmpIdExist, Id);
            return Json(status, JsonRequestBehavior.AllowGet);

        }


        #endregion

        #region CRUD


        // GET: EmployeeProject
        public ActionResult Index()
        {
            _employeeprojectBL = new EmployeeProjectBL();
            return View(_employeeprojectBL.GetAllEmployeeProjectList());
        }

        // GET: EmployeeProject/Details/5
        public ActionResult Details(int? id)
        {
            _employeeprojectBL = new EmployeeProjectBL();

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var employeeProject = _employeeprojectBL.GetEmployeeProjectDetails(id);

            if (employeeProject == null)
            {
                return HttpNotFound();
            }
            return View(employeeProject);
        }

        // GET: EmployeeProject/Create
        public ActionResult Create()
        {
            _empbl = new EmployeeBL();
            _projectbl = new ProjectBL();

            ViewBag.DateTimeCreated = DateTimeOffset.Now;
            ViewBag.CreatedBy = User.Identity.Name;
            ViewBag.empProjEmp = 0;

            ViewBag.EmployeeProject_Employee = new SelectList(_empbl.GetAllEmployeeListForDropDown(), "Id", "FullName");
            ViewBag.EmployeeProject_Project = new SelectList(_projectbl.GetAllProjectDropDownList(), "Id", "Code");
            return View();
        }

        // POST: EmployeeProject/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EmployeeProject_Project,EmployeeProject_Employee")] EmployeeProject employeeProject)
        {
            _employeeprojectBL = new EmployeeProjectBL();
            _empbl = new EmployeeBL();
            _projectbl = new ProjectBL();

            if (ModelState.IsValid)
            {
                employeeProject.CreatedBy = User.Identity.Name;
                employeeProject.Created = DateTimeOffset.Now;
               var result = _employeeprojectBL.CreateEmployeeProject(employeeProject);

                if (result > 0)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return View("Error");
                }
            }

            ViewBag.DateTimeCreated = DateTimeOffset.Now;
            ViewBag.CreatedBy = User.Identity.Name;

            ViewBag.EmployeeProject_Employee = new SelectList(_empbl.GetAllEmployeeListForDropDown(), "Id", "FullName", employeeProject.EmployeeProject_Employee, "Select");
            ViewBag.EmployeeProject_Project = new SelectList(_projectbl.GetAllProjectDropDownList(), "Id", "Code", employeeProject.EmployeeProject_Project);
            return View(employeeProject);

        }


        // GET: EmployeeProject/Edit/5
        public ActionResult Edit(int? id)
        {
            _employeeprojectBL = new EmployeeProjectBL();
            _empbl = new EmployeeBL();
            _projectbl = new ProjectBL();

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var employeeProject = _employeeprojectBL.GetEmployeeProjectDetails(id);

            if (employeeProject == null)
            {
                return HttpNotFound();
            }
            
            ViewBag.EmployeeProject_Employee = new SelectList(_empbl.GetAllEmployeeListForDropDown(), "Id", "FullName", employeeProject.EmployeeProject_Employee);
            ViewBag.EmployeeProject_Project = new SelectList(_projectbl.GetAllProjectDropDownList(), "Id", "Code", employeeProject.EmployeeProject_Project);

            employeeProject.ModifiedBy = User.Identity.Name;
            employeeProject.Modified = DateTimeOffset.Now;

            var employee = _empbl.GetEmployeeAndEmployeeProjectDetails(employeeProject.EmployeeProject_Employee);

            foreach (var item in employee)
            {
                ViewBag.EmployeeName = item.FullName;
                ViewBag.EmployeeId = item.Id;
            }
            ViewBag.empProjEmp = id;
            return View(employeeProject);
        }

        // POST: EmployeeProject/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,EmployeeProject_Project,EmployeeProject_Employee,Modified,ModifiedBy")] EmployeeProject employeeProject)
        {
            _empbl = new EmployeeBL();
            _projectbl = new ProjectBL();
            _employeeprojectBL = new EmployeeProjectBL();

            if (ModelState.IsValid)
            {
                employeeProject.ModifiedBy = User.Identity.Name;
                employeeProject.Modified = DateTimeOffset.Now;
               var result = _employeeprojectBL.UpdateEmployeeProject(employeeProject);
                if (result > 0)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return View("Error");
                }
                //db.Entry(employeeProject).State = EntityState.Modified;
                //await db.SaveChangesAsync();
            }
            ViewBag.EmployeeProject_Employee = new SelectList(_empbl.GetAllEmployeeListForDropDown(), "Id", "FullName", employeeProject.EmployeeProject_Employee);
            ViewBag.EmployeeProject_Project = new SelectList(_projectbl.GetAllProjectDropDownList(), "Id", "Code", employeeProject.EmployeeProject_Project);
            return View(employeeProject);

        }

        // GET: EmployeeProject/Delete/5
        public ActionResult Delete(int? id)
        {
            _employeeprojectBL = new EmployeeProjectBL();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var employeeProject = _employeeprojectBL.GetEmployeeProjectDetails(id);
            if (employeeProject == null)
            {
                return HttpNotFound();
            }
            return View(employeeProject);
        }

        // POST: EmployeeProject/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public  ActionResult DeleteConfirmed(int id)
        {
            _employeeprojectBL = new EmployeeProjectBL();

            var employeeProject = _employeeprojectBL.GetEmployeeProjectDetails(id);
            var result = _employeeprojectBL.DeleteEmployeeProject(id);

            if (result > 0)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return View("Error");
            }
        }

        #endregion
    }
}
