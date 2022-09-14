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
    public class BusinessUnitController : Controller
    {
        private BusinessUnitBL _businessUnitBL;
        private CustomMethodBL _cmethodbl;

        public JsonResult IsBusinessUnitCodeValid(string Code, int? Id)
        {
            _businessUnitBL = new BusinessUnitBL();
            _cmethodbl = new CustomMethodBL();

            var status = _businessUnitBL.IsBUCodeExist(_cmethodbl.removeWhiteSpaces(Code.ToUpper()), _businessUnitBL.FindBusinessUnitById(Id) == null ? false : true, Id);
            return Json(status, JsonRequestBehavior.AllowGet);
        }

        // GET: BusinessUnit
        public ActionResult Index()
        {
            _businessUnitBL = new BusinessUnitBL();
            return View(_businessUnitBL.GetAllBusinessUnitList());
        }

        // GET: BusinessUnit/Details/5
        public ActionResult Details(int? id)
        {
            _businessUnitBL = new BusinessUnitBL();

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var businessUnit = _businessUnitBL.FindBusinessUnitById(id);

            if (businessUnit == null)
            {
                return HttpNotFound();
            }
            return View(businessUnit);

        }

        // GET: BusinessUnit/Create
        public ActionResult Create()
        {
            ViewBag.DateTimeCreated = DateTimeOffset.Now;
            ViewBag.CreatedBy = User.Identity.Name;

            return View();
        }

        // POST: BusinessUnit/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Code,Name")] BusinessUnit businessUnit)
        {
            _businessUnitBL = new BusinessUnitBL();
            _cmethodbl = new CustomMethodBL();

            businessUnit.Code = _cmethodbl.removeWhiteSpaces(businessUnit.Code.ToUpper());

            ModelState.Clear();
            TryValidateModel(businessUnit);

            if (!string.IsNullOrEmpty(businessUnit.Name))
            {
                businessUnit.Name = _cmethodbl.removeWhiteSpaces(businessUnit.Name);
            }

            ModelState.Clear();
            TryValidateModel(businessUnit);

            if (ModelState.IsValid)
            {
                businessUnit.Created = DateTimeOffset.Now;
                businessUnit.CreatedBy = User.Identity.Name;

                var result = _businessUnitBL.CreateBU(businessUnit);
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

            return View(businessUnit);
        }

        // GET: BusinessUnit/Edit/5
        public ActionResult Edit(int? id)
        {
            _businessUnitBL = new BusinessUnitBL();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var businessUnit = _businessUnitBL.FindBusinessUnitById(id);
            if (businessUnit == null)
            {
                return HttpNotFound();
            }

            businessUnit.ModifiedBy = User.Identity.Name;
            businessUnit.Modified = DateTimeOffset.Now;
            return View(businessUnit);

        }

        // POST: BusinessUnit/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Code,Name,Created,CreatedBy")] BusinessUnit businessUnit)
        {
            _businessUnitBL = new BusinessUnitBL();
            _cmethodbl = new CustomMethodBL();


            businessUnit.Code = _cmethodbl.removeWhiteSpaces(businessUnit.Code.ToUpper());

            if (!string.IsNullOrEmpty(businessUnit.Name))
            {
                businessUnit.Name = _cmethodbl.removeWhiteSpaces(businessUnit.Name);
            }

            ModelState.Clear();
            TryValidateModel(businessUnit);

            if (ModelState.IsValid)
            {
                businessUnit.ModifiedBy = User.Identity.Name;
                businessUnit.Modified = DateTimeOffset.Now;

                var result = _businessUnitBL.UpdateBU(businessUnit);
                if (result > 0)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return View("Error");
                }

            }
            businessUnit.ModifiedBy = User.Identity.Name;
            businessUnit.Modified = DateTimeOffset.Now;
            return View(businessUnit);
        }

        // GET: BusinessUnit/Delete/5
        public ActionResult Delete(int? id)
        {
            _businessUnitBL = new BusinessUnitBL();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var businessUnit = _businessUnitBL.FindBusinessUnitById(id);
            if (businessUnit == null)
            {
                return HttpNotFound();
            }
            return View(businessUnit);
           
        }

        // POST: BusinessUnit/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _businessUnitBL = new BusinessUnitBL();

            Nullable<int> Deleteresult = null;
            Deleteresult = _businessUnitBL.sp_DeleteBU(id);
            Deleteresult = _businessUnitBL.DeleteBusinessUnit(id);

            ViewBag.Message = Deleteresult;
            return View("Index",_businessUnitBL.GetAllBusinessUnitList());
        }

    }
}
