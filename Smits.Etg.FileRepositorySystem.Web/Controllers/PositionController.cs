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
    public class PositionController : Controller
    {
        private PositionBL _posBL;
        private CustomMethodBL _cmethodBL;

        #region Validation
        public JsonResult IsPositionNameValid(string Name, int? Id)
        {
            _posBL = new PositionBL();
            _cmethodBL = new CustomMethodBL();

            var status = _posBL.IsPositionNameValid(_cmethodBL.removeWhiteSpaces(Name), _posBL.FindPositionById(Id) == null ? false : true , Id);
            return Json(status, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region CRUD
        public ActionResult listOfEmployeePerPos(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            _posBL = new PositionBL();

            var pos = _posBL.FindPositionByEmpId(id);
            if (pos == null)
            {
                return HttpNotFound();
            }

            var posn = "";
            foreach (var item in pos.Take(1))
            {
                posn = item.Name;
            }
            ViewBag.PositionName = posn;

            return View(pos);
        }


        // GET: Position
        public ActionResult Index()
        {
            _posBL = new PositionBL();
            return View(_posBL.GetAllPositionList());
        }

        // GET: Position/Details/5
        public ActionResult Details(int? id)
        {
            _posBL = new PositionBL();

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var position = _posBL.FindPositionById(id);

            if (position == null)
            {
                return HttpNotFound();
            }
            return View(position);
        }

        // GET: Position/Create
        public ActionResult Create()
        {
            ViewBag.DateTimeCreated = DateTimeOffset.Now;
            ViewBag.CreatedBy = User.Identity.Name;
            return View();
        }

        // POST: Position/Create
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Create([Bind(Include = "Name,Description")] Position position)
        {
            _cmethodBL = new CustomMethodBL();
            position.Name = _cmethodBL.removeWhiteSpaces(position.Name);
            

            ModelState.Clear();
            TryValidateModel(position);

            if (ModelState.IsValid)
            {
                position.Created = DateTimeOffset.Now;
                position.CreatedBy = User.Identity.Name;

                _posBL = new PositionBL();
                var result = _posBL.CreatePosition(position);
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

            return View(position);
        }

        // GET: Position/Edit/5
        public ActionResult Edit(int? id)
        {
            _posBL = new PositionBL();

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            
            var position = _posBL.FindPositionById(id);

            if (position == null)
            {
                return HttpNotFound();
            }

            position.ModifiedBy = User.Identity.Name;
            position.Modified = DateTimeOffset.Now;

            return View(position);
        }

        // POST: Position/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public  ActionResult Edit([Bind(Include = "Id,Name,Description,Created,CreatedBy,Modified,ModifiedBy")] Position position)
        {
            _cmethodBL = new CustomMethodBL();
            _posBL = new PositionBL();

            position.Name = _cmethodBL.removeWhiteSpaces(position.Name);

            ModelState.Clear();
            TryValidateModel(position);

            if (ModelState.IsValid)
            {
                var result = _posBL.UpdatePosition(position);
                if (result > 0)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return View("Error");
                }
              
            }
            position.ModifiedBy = User.Identity.Name;
            position.Modified = DateTimeOffset.Now;
            return View(position);
        }

        // GET: Position/Delete/5
        public ActionResult Delete(int? id)
        {
            _posBL = new PositionBL();

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var position = _posBL.FindPositionById(id);
            if (position == null)
            {
                return HttpNotFound();
            }
            return View(position);
        }

        // POST: Position/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public  ActionResult DeleteConfirmed(int id)
        {
            _posBL = new PositionBL();

            var position = _posBL.FindPositionById(id);

            Nullable<int> Deleteresult = null;
            //Deleteresult = _posBL.sp_DeletePosition(id); //Stored Procedure

            Deleteresult = _posBL.DeletePosition(id); 
            ViewBag.Message = Deleteresult;

            _posBL = new PositionBL();
            return View("Index",_posBL.GetAllPositionList());
        }
        #endregion


    }
}
