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
    public class DepartmentController : Controller
    {
        private DepartmentBL _deptBL;
        private CustomMethodBL _cmethodbl;

        #region Validation
        public JsonResult IsDepartmentCodeExist(string Code, int? Id = 0)
        {
            _cmethodbl = new CustomMethodBL();
            _deptBL = new DepartmentBL();

            var IsDepartemtExist = _deptBL.IsDeparmtentExistById(Id);

            var status = _deptBL.IsDepartmentCodeExist(_cmethodbl.removeWhiteSpaces(Code), IsDepartemtExist, Id);
            return Json(status, JsonRequestBehavior.AllowGet);
        }

        public JsonResult IsDepartmentNameExist(string Name, int? Id = 0)
        {
            _cmethodbl = new CustomMethodBL();
            _deptBL = new DepartmentBL();

            var IsDepartemtExist = _deptBL.IsDeparmtentExistById(Id);

            var status = _deptBL.IsDepartmentNameExist(_cmethodbl.removeWhiteSpaces(Name), IsDepartemtExist, Id);
            return Json(status, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region CRUD

        public ActionResult listOfEmployeePerDept(int? id)
        {
            _deptBL = new DepartmentBL();

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var department = _deptBL.FindDepartmentById(id);
            if (department == null)
            {
                return HttpNotFound();
            }

            var dcode = department.Code;
            ViewBag.depCode = dcode;
            var dept = _deptBL.GetListOfEmployeePerDepartmentByCode(dcode);

            return View(dept);
        }

        // GET: Department
        public ActionResult Index()
        {
            _deptBL = new DepartmentBL();
            return View(_deptBL.GetAllDepartmentList());
        }

        // GET: Department/Details/5
        public ActionResult Details(int? id)
        {
            _deptBL = new DepartmentBL();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var department = _deptBL.FindDepartmentById(id);
            if (department == null)
            {
                return HttpNotFound();
            }
            return View(department);

        }

        // GET: Department/Create
        public ActionResult Create()
        {
            ViewBag.DateTimeCreated = DateTimeOffset.Now;
            ViewBag.CreatedBy = User.Identity.Name;
            return View();
        }

        // POST: Department/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Name,Code")] Department department)
        {
            _cmethodbl = new CustomMethodBL();
            department.Code = _cmethodbl.removeWhiteSpaces(department.Code.ToUpper());

            if (!string.IsNullOrEmpty(department.Name))
            {
                department.Name = _cmethodbl.removeWhiteSpaces(department.Name);
            }

            ModelState.Clear();
            TryValidateModel(department);

            if (ModelState.IsValid)
            {
                department.Created = DateTimeOffset.Now;
                department.CreatedBy = User.Identity.Name;

                _deptBL = new DepartmentBL();

                var result = _deptBL.CreateDepartment(department);
                if (result > 0)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return View("Error");
                }

            }


            department.Created = DateTimeOffset.Now;
            department.CreatedBy = User.Identity.Name;

            return View(department);
        }

        // GET: Departments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            _deptBL = new DepartmentBL();
            var department = _deptBL.FindDepartmentById(id);
            if (department == null)
            {
                return HttpNotFound();
            }

            department.Modified = DateTimeOffset.Now;
            department.ModifiedBy = User.Identity.Name;

            return View(department);
        }

        // POST: Department/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Code,Name")] Department department)
        {
            _cmethodbl = new CustomMethodBL();
            department.Code = _cmethodbl.removeWhiteSpaces(department.Code.ToUpper());
            if (!string.IsNullOrEmpty(department.Name))
            {
                department.Name = _cmethodbl.removeWhiteSpaces(department.Name);
            }

            ModelState.Clear();
            TryValidateModel(department);

            if (ModelState.IsValid)
            {

                department.Modified = DateTimeOffset.Now;
                department.ModifiedBy = User.Identity.Name;

                _deptBL = new DepartmentBL();

                var result = _deptBL.UpdateDepartment(department);
                if (result > 0)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return View("Error");
                }
             
            }
            department.Modified = DateTimeOffset.Now;
            department.ModifiedBy = User.Identity.Name;

            return View(department);
        }

        // GET: Department/Delete/5
        public ActionResult Delete(int? id)
        {
            _deptBL = new DepartmentBL();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var department = _deptBL.FindDepartmentById(id);
            if (department == null)
            {
                return HttpNotFound();
            }
            return View(department);
        }

        // POST: Department/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _deptBL = new DepartmentBL();

            var department = _deptBL.FindDepartmentById(id);
            Nullable<int> Deleteresult = null;
            //Deleteresult = _deptBL.sp_DeleteDepartment(id);
            Deleteresult = _deptBL.DeleteDepartment(id);

            ViewBag.Message = Deleteresult;

            return View("Index",_deptBL.GetAllDepartmentList());
           
        }

        #endregion
    }
}
