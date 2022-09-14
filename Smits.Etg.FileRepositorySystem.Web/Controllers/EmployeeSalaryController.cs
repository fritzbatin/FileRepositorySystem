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
    public class EmployeeSalaryController : Controller
    {
        private EmployeeSalaryBL _empsalbl;
        private EmployeeBL _empbl;
        public int myId { get; set; }

        // GET: EmployeeSalary
        public ActionResult Index(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            _empsalbl = new EmployeeSalaryBL();

            var IsEmpExist = _empsalbl.GetEmployeeSalaryHistory(id);

            if (IsEmpExist == null)
            {
                return HttpNotFound();
            }
            _empbl = new EmployeeBL();

            ViewBag.empData = _empbl.GetEmployeeDetails(id);
            
            return View(IsEmpExist);
         
        }

        // GET: EmployeeSalary/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: EmployeeSalary/Create
        public ActionResult Create(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            _empsalbl = new EmployeeSalaryBL();

            var latestSalary = _empsalbl.GetLatestSalary(id);
           
            _empbl = new EmployeeBL();

            ViewBag.empData = _empbl.GetEmployeeDetails(id);

            if (latestSalary != null)
            {
                ViewBag.Salary = latestSalary.Salary;
            }
            else
            {
                ViewBag.Salary = 0.00M;
            }


            return View(latestSalary);
        }

        // POST: EmployeeSalary/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public  ActionResult Create([Bind(Include = "Salary,EmployeeSalary_EmpId")] EmployeeSalary employeeSalary, int empId)
        {
          

            employeeSalary.EmployeeSalary_EmpId = empId;
            ModelState.Clear();
            TryValidateModel(employeeSalary);

            if (ModelState.IsValid)
            {
                _empsalbl = new EmployeeSalaryBL();

                employeeSalary.Created = DateTimeOffset.Now;
                employeeSalary.CreatedBy = User.Identity.Name;
                if (_empsalbl.CreateSalary(employeeSalary) > 0)
                {
                    return RedirectToAction("Details", "Employee", new { id = empId });
                }

            }

            var id = empId;


            _empsalbl = new EmployeeSalaryBL();
            var latestSalary = _empsalbl.GetLatestSalary(id);
            if (latestSalary != null)
            {
                ViewBag.Salary = employeeSalary.Salary;
            }
            else
            {
                ViewBag.Salary = 0.00M;
            }

            _empbl = new EmployeeBL();

            ViewBag.empData = _empbl.GetEmployeeDetails(id);

           

            return View(employeeSalary);
        }

        // GET: EmployeeSalary/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: EmployeeSalary/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: EmployeeSalary/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: EmployeeSalary/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
