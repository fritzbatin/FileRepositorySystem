using Smits.Etg.FileRepositorySystem.BL;
using Smits.Etg.FileRepositorySystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Smits.Etg.FileRepositorySystem.Web.Controllers
{
    public class ProjectController : Controller
    {
        private ProjectBL _pbl;
        private CustomMethodBL _cmBL;

        #region VALIDATION
        public JsonResult IsProjectCodeExist(string Code, int? Id = 0)
        {
            _cmBL = new CustomMethodBL();
            _pbl = new ProjectBL();
            var status = _pbl.IsProjectCodeExist(_cmBL.removeWhiteSpaces(Code), _pbl.IsIdExist(Id), Id);
            return Json(status, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region CRUD

        // GET: Project
        public ActionResult Index()
        {
            _pbl = new ProjectBL();

            var projects = _pbl.GetAllProjectList();
            return View(projects);
        }

        // GET: Project/Details/5
        public ActionResult Details(int? id)
        {
            _pbl = new ProjectBL();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var project = _pbl.FindProjectDetails(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            return View(project);
        }

        // GET: Project/Create
        public ActionResult Create()
        {
            ViewBag.DateTimeCreated = DateTimeOffset.Now;
            ViewBag.CreatedBy = User.Identity.Name;

            _pbl = new ProjectBL();
            ViewBag.Project_BusinessUnit = new SelectList(_pbl.GetAllProjectDropDownList(), "Id", "Code");
            return View();
        }

        // POST: Project/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Code,Name,Description,Project_BusinessUnit")] Project project, string dateStart, string dateEnd)
        {
            _pbl = new ProjectBL();

            project.Code = project.Code.ToUpper();
            if (!string.IsNullOrEmpty(dateStart))
            {
                project.StartDate = Convert.ToDateTime(dateStart);
            }
            else
            {
                ModelState.AddModelError("dateStart", "Please select date");
            }

            if (ModelState.IsValid)
            {
                if (!string.IsNullOrEmpty(dateEnd))
                {
                    project.EndDate = Convert.ToDateTime(dateEnd);
                }
                project.Created = DateTimeOffset.Now;
                project.CreatedBy = User.Identity.Name;

                var result =_pbl.CreatePRoject(project);
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
            ViewBag.Project_BusinessUnit = new SelectList(_pbl.GetAllProjectDropDownList(), "Id", "Code", project.Project_BusinessUnit);
            return View(project);
        }

        // GET: Project/Edit/5
        public ActionResult Edit(int? id)
        {
            _pbl = new ProjectBL();

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var project = _pbl.FindProjectDetails(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            project.ModifiedBy = User.Identity.Name;
            project.Modified = DateTimeOffset.Now;

            ViewBag.StartDate = project.StartDate.ToString("yyyy/MM/dd");
            ViewBag.EndDate = project.EndDate?.ToString("yyyy/MM/dd");

            ViewBag.Project_BusinessUnit = new SelectList(_pbl.GetAllProjectDropDownList(), "Id", "Code", project.Project_BusinessUnit);

            return View(project);
        }

        // POST: Project/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Code,Name,Description,Project_BusinessUnit")] Project project, string dateStart, string dateEnd)
        {
            _pbl = new ProjectBL();

            project.Code = project.Code.ToUpper();
            project.StartDate = Convert.ToDateTime(dateStart);
            if (!string.IsNullOrEmpty(dateEnd))
            {
                project.EndDate = Convert.ToDateTime(dateEnd);
            }
            ModelState.Clear();
            TryValidateModel(project);

            if (ModelState.IsValid)
            {
                project.ModifiedBy = User.Identity.Name;
                project.Modified = DateTimeOffset.Now;

                var result = _pbl.UpdateProject(project);
                if (result > 0)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return View("Error");
                }
            }
            project.ModifiedBy = User.Identity.Name;
            project.Modified = DateTimeOffset.Now;
            ViewBag.Project_BusinessUnit = new SelectList(_pbl.GetAllProjectDropDownList(), "Id", "Code", project.Project_BusinessUnit);
            return View(project);
        }

        // GET: Project/Delete/5
        public ActionResult Delete(int? id)
        {
            _pbl = new ProjectBL();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var project = _pbl.FindProjectDetails(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            return View(project);
        }

        // POST: Project/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _pbl = new ProjectBL();

            var project = _pbl.FindProjectDetails(id);

            Nullable<int> Deleteresult = null;
            //Deleteresult = _pbl.sp_DeleteProject(id); //Stored Procedure
            Deleteresult = _pbl.DeleteProject(id); //Stored Procedure
            ViewBag.Message = Deleteresult;
            
            return View("Index", _pbl.GetAllProjectList());
        }
        #endregion


    }
}
