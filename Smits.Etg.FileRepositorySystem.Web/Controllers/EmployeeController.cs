using LINQtoCSV;
using Smits.Etg.FileRepositorySystem.BL;
using Smits.Etg.FileRepositorySystem.Models;
using Smits.Etg.FileRepositorySystem.Web.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.Mvc;

namespace Smits.Etg.FileRepositorySystem.Web.Controllers
{
    public class EmployeeController : Controller
    {
        private EmployeeBL _empbl;
        private DepartmentBL _deptbl;
        private PositionBL _posbl;
        private CustomMethodBL _cmethodbl;

        #region Export to CSV

      
        /// <summary>
        /// Instead of saving the file to disk, 
        /// we can also write the file into the response stream so the user can download the file.
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult DownloadAsCSV()
        {
            _empbl = new EmployeeBL();
            _posbl = new PositionBL();
            _deptbl = new DepartmentBL();

            List<Employee> list = _empbl.GetAllEmployeeListForExport();
            List<UploadEmployeeData> empList = new List<UploadEmployeeData>();

          
            foreach (var item in list)
            {
                var position = _posbl.FindPositionById(item.Employee_Position);
                var department = _deptbl.FindDepartmentById(item.Employee_Department);

                empList.Add(new UploadEmployeeData
                {
                    EmployeeId = item.EmployeeId,
                    FirstName = item.FirstName,
                    MiddleName = item.MiddleName,
                    LastName = item.LastName,
                    EmailAddress = item.EmailAddress,
                    Employee_Position = position.Name,
                    Employee_Department = department.Code,
                });
            }

           

            CsvFileDescription csvFileDescription = new CsvFileDescription
            {
                SeparatorChar = ',',
                FirstLineHasColumnNames = true,
                EnforceCsvColumnAttribute = true
            };
            CsvContext csvContext = new CsvContext();
            byte[] file = null;
            using (MemoryStream memoryStream = new MemoryStream())
            {
                using (StreamWriter streamWriter = new StreamWriter(memoryStream))
                {
                    csvContext.Write(empList, streamWriter, csvFileDescription);
                    streamWriter.Flush();
                    file = memoryStream.ToArray();
                }
            }
            return File(file, "text/csv", "Employees.csv");
        }


        #endregion



        #region Upload CSV to db

        public ActionResult UploadEmployeeDataFromCSV()
        {
            List<UploadEmployeeData> empList = new List<UploadEmployeeData>();
            ViewBag.Employee = empList;
            ViewBag.EmployeeNotUploaded = TempData["EmployeeNotUploaded"];
            ViewBag.Employeeuploaded = TempData["Employee"];

            return View();
        }

        [DllImport(@"urlmon.dll", CharSet = CharSet.Auto)]
        private extern static System.UInt32 FindMimeFromData(System.UInt32 pBC,
        [MarshalAs(UnmanagedType.LPStr)] System.String pwzUrl,
        [MarshalAs(UnmanagedType.LPArray)] byte[] pBuffer,
        System.UInt32 cbSize, [MarshalAs(UnmanagedType.LPStr)] System.String pwzMimeProposed,
        System.UInt32 dwMimeFlags,
        out System.UInt32 ppwzMimeOut,
        System.UInt32 dwReserverd);

        [HttpPost]
        public ActionResult UploadCsv(HttpPostedFileBase attachmentcsv)
        {
          
            if (attachmentcsv == null)
            {
                this.AddNotification("Please select file!", NotificationType.INFO);
                //ErrorMessage = "Please select file!";
                return RedirectToAction("UploadEmployeeDataFromCSV");
            }
            //byte[] bytes;
            //using (BinaryReader br = new BinaryReader(attachmentcsv.InputStream))
            //{
            //    bytes = br.ReadBytes(attachmentcsv.ContentLength);
            //}
            //System.UInt32 mimetype;
            //FindMimeFromData(0, null, bytes, 256, null, 0, out mimetype, 0);
            //System.IntPtr mimeTypePtr = new IntPtr(mimetype);
            //string mime = Marshal.PtrToStringUni(mimeTypePtr);
            //Marshal.FreeCoTaskMem(mimeTypePtr);
            //if (mime == "text/csv" || mime == "text/plain")
            //{

            //}
            //else
            //{
            //    this.AddNotification("MIME type is invalid - Only CSV file allowed!", NotificationType.WARNING);
            //    return RedirectToAction("UploadEmployeeDataFromCSV");
            //}
            _cmethodbl = new CustomMethodBL();
            _empbl = new EmployeeBL();

         

            CsvFileDescription csvFileDescription = new CsvFileDescription
            {
                SeparatorChar = ',',
                FirstLineHasColumnNames = true,
                IgnoreUnknownColumns = true
            };
            CsvContext csvContext = new CsvContext();
            StreamReader streamReader = new StreamReader(attachmentcsv.InputStream);
            IEnumerable<UploadEmployeeData> list = csvContext.Read<UploadEmployeeData>(streamReader, csvFileDescription);
            List<UploadEmployeeData> empList = new List<UploadEmployeeData>();
            List<UploadEmployeeData> listOfEmployeeNotUploaded = new List<UploadEmployeeData>();
            string _remarks = "";



            foreach (var item in list)
            {
                if (string.IsNullOrEmpty(item.FirstName) || string.IsNullOrEmpty(item.LastName) || string.IsNullOrEmpty(item.EmployeeId) || string.IsNullOrEmpty(item.EmailAddress) || string.IsNullOrEmpty(item.Employee_Position) || string.IsNullOrEmpty(item.Employee_Department))
                {

                    if (string.IsNullOrEmpty(item.FirstName))
                    {
                        _remarks = "No FirstName, ";
                    }
                    if (string.IsNullOrEmpty(item.LastName))
                    {
                        _remarks += "No LastName, ";
                    }
                    if (string.IsNullOrEmpty(item.EmployeeId))
                    {
                        _remarks += "No Employee Id, ";
                    }
                    if (string.IsNullOrEmpty(item.EmailAddress))
                    {
                        _remarks += "No Email Address, ";
                    }

                    if (string.IsNullOrEmpty(item.Employee_Position))
                    {
                        _remarks += "No Position Name, ";
                    }

                    if (string.IsNullOrEmpty(item.Employee_Department))
                    {
                        _remarks += "No Department Code, ";
                    }

                    listOfEmployeeNotUploaded.Add(new UploadEmployeeData
                    {
                        EmployeeId = item.EmployeeId,
                        FirstName = item.FirstName,
                        MiddleName = item.MiddleName,
                        LastName = item.LastName,
                        EmailAddress = item.EmailAddress,
                        Employee_Position = item.Employee_Position,
                        Employee_Department = item.Employee_Department,
                        Remarks = _remarks
                    });
                    _remarks = string.Empty;
                }
                else
                {
                    item.EmployeeId = _cmethodbl.removeWhiteSpaces(item.EmployeeId.Trim());
                    item.FirstName = _cmethodbl.removeWhiteSpaces(item.FirstName.Trim());
                    if (!string.IsNullOrEmpty(item.MiddleName))
                    {
                        item.MiddleName = _cmethodbl.removeWhiteSpaces(item.MiddleName.Trim());
                    }
                    item.LastName = _cmethodbl.removeWhiteSpaces(item.LastName.Trim());
                    item.EmailAddress = _cmethodbl.removeWhiteSpaces(item.EmailAddress.Trim());
                    item.Employee_Position = _cmethodbl.removeWhiteSpaces(item.Employee_Position.Trim());
                    item.Employee_Department = _cmethodbl.removeWhiteSpaces(item.Employee_Department.Trim());

                    var empdata = _empbl.GetEmployeeDetails(item.EmployeeId);
                    var empid = (empdata != null) ? empdata.Id : 0;
                    var IsEmployeeIdAvailable = _empbl.IsEmployeeIdExist(item.EmployeeId);
                    var IsEmpNameValid = _empbl.IsNameIsValid(item.FirstName, item.LastName);
                    var IsEmailAddValid = _empbl.IsEmailAddressExist(item.EmailAddress);
                    var IsPositionNameIdValid = _empbl.IsPositionNameIdValidByPositionName(item.Employee_Position);
                    var IsDepartmentCodeIdValid = _empbl.IsDepartmentCodeIdValid(item.Employee_Department);

                    if (!IsEmployeeIdAvailable)
                    {
                        //for update
                        

                        IsEmpNameValid = _empbl.IsNameIsValid(item.FirstName, item.LastName, true, empid);
                        IsEmailAddValid = _empbl.IsEmailAddressExist(item.EmailAddress, true, empid);
                        IsPositionNameIdValid = _empbl.IsPositionNameIdValidByPositionName(item.Employee_Position);
                        IsDepartmentCodeIdValid = _empbl.IsDepartmentCodeIdValid(item.Employee_Department);
                    }

                    //if (!IsEmployeeIdExisted)
                    //{
                    //    _remarks += "EmployeeId existed/invalid, ";
                    //}
                    //if (!IsEmpNameValid)
                    //{
                    //    _remarks += "Name existed, ";
                    //}
                    _remarks += (!IsEmployeeIdAvailable) ? "Update - " : "Create - ";

                    if (!IsEmailAddValid)
                    {
                        _remarks += "EmailAddress invalid/existed, ";
                    }
                    if (!IsPositionNameIdValid.result)
                    {
                        _remarks += "Position Invalid, ";
                    }
                    if (!IsDepartmentCodeIdValid.result)
                    {
                        _remarks += "Department Invalid, ";
                    }

                    if ((IsEmailAddValid) && (IsEmpNameValid) && (IsPositionNameIdValid.result) && (IsDepartmentCodeIdValid.result))
                    {

                        Employee employee = new Employee();

                        employee.EmployeeId = _cmethodbl.removeWhiteSpaces(item.EmployeeId.Trim());
                        employee.FirstName = _cmethodbl.removeWhiteSpaces(item.FirstName.Trim());
                        if (!string.IsNullOrEmpty(item.MiddleName))
                        {
                            employee.MiddleName = _cmethodbl.removeWhiteSpaces(item.MiddleName.Trim());
                        }

                        employee.LastName = _cmethodbl.removeWhiteSpaces(item.LastName.Trim());
                        employee.EmailAddress = _cmethodbl.removeWhiteSpaces(item.EmailAddress.Trim());
                        employee.CreatedBy = User.Identity.Name;
                        employee.Created = DateTimeOffset.Now;

                       
                        employee.Employee_Position = IsPositionNameIdValid.posId;
                        employee.Employee_Department = IsDepartmentCodeIdValid.deptId;
                        employee.Id = empid;

                        ModelState.Clear();
                        TryValidateModel(employee);

                        if (ModelState.IsValid)
                        {

                            if (!IsEmployeeIdAvailable)
                            {
                                //Calling Update method in BL
                                var result = _empbl.UpdateEmployee(employee);
                                _remarks += result > 0 ? "Success" : "Failed";
                            }
                            else
                            {
                                //Calling Create method in BL
                                var result = _empbl.CreateEmployee(employee);
                                _remarks += result > 0 ? "Success" : "Failed";
                            }

                            empList.Add(new UploadEmployeeData
                            {

                                EmployeeId = item.EmployeeId,
                                FirstName = item.FirstName,
                                MiddleName = item.MiddleName,
                                LastName = item.LastName,
                                EmailAddress = item.EmailAddress,
                                Employee_Position = item.Employee_Position,
                                Employee_Department = item.Employee_Department,
                                Remarks = _remarks


                            });
                            _remarks = string.Empty;


                        }


                    }
                    else
                    {
                        listOfEmployeeNotUploaded.Add(new UploadEmployeeData
                        {
                            EmployeeId = item.EmployeeId,
                            FirstName = item.FirstName,
                            MiddleName = item.MiddleName,
                            LastName = item.LastName,
                            EmailAddress = item.EmailAddress,
                            Employee_Position = item.Employee_Position,
                            Employee_Department = item.Employee_Department,
                            Remarks = _remarks
                        });
                        _remarks = string.Empty;

                    }
                }

            }

             

            TempData["EmployeeNotUploaded"] = listOfEmployeeNotUploaded.OrderByDescending(e => e.EmployeeId);
            TempData["Employee"] = empList.OrderByDescending(e => e.EmployeeId);
       
               
            //return View("UploadEmployeeDataFromCSV");
            return RedirectToAction("UploadEmployeeDataFromCSV");
               
           
        }
        
       
        #endregion


        #region validation

        public JsonResult IsEmployeeIdAvailable(string EmployeeId, int? Id = 0)

        {
            _empbl = new EmployeeBL();
            _cmethodbl = new CustomMethodBL();

            var IsEmpIdExist = _empbl.IsEmployeeExistById(Id);
            var status = _empbl.IsEmployeeIdExist(_cmethodbl.removeWhiteSpaces(EmployeeId), IsEmpIdExist, Id);

            return Json(status, JsonRequestBehavior.AllowGet);
        }

        public JsonResult IsNamevalid(string LastName, string FirstName, int? Id = 0)
        {
            _empbl = new EmployeeBL();
            _cmethodbl = new CustomMethodBL();

            var IsEmpIdExist = _empbl.IsEmployeeExistById(Id);
            var status = _empbl.IsNameIsValid(_cmethodbl.removeWhiteSpaces(FirstName), _cmethodbl.removeWhiteSpaces(LastName), IsEmpIdExist, Id);
            return Json(status, JsonRequestBehavior.AllowGet);
        }

        //Email Validation
        public JsonResult IsEmailAddressAvailable(string EmailAddress, int? Id = 0)
        {
            _empbl = new EmployeeBL();
            _cmethodbl = new CustomMethodBL();

            var IsEmpIdExist = _empbl.IsEmployeeExistById(Id);
            var status = _empbl.IsEmailAddressExist(_cmethodbl.removeWhiteSpaces(EmailAddress), IsEmpIdExist, Id);

            return Json(status, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region CRUD
        // GET: Employee
        public ActionResult Index()
        {
            _empbl = new EmployeeBL();

            return View(_empbl.GetAllEmployeeList());
        }

        // GET: Employee/Details/5
        [HttpGet]
        public ActionResult Details(int? id)
        {
            _empbl = new EmployeeBL();

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var IsEmpExist = _empbl.GetEmployeeDetails(id);

            if (IsEmpExist == null)
            {
                return HttpNotFound();
            }
            return View(IsEmpExist);


        }

        // GET: Employee/Create
        public ActionResult Create()
        {
            _deptbl = new DepartmentBL();
            _posbl = new PositionBL();

            ViewBag.DateTimeCreated = DateTimeOffset.Now;
            ViewBag.CreatedBy = User.Identity.Name;
            ViewBag.Employee_Department = new SelectList(_deptbl.GetAllDepartmentListForDropDown(), "Id", "Code");
            ViewBag.Employee_Position = new SelectList(_posbl.GetAllPositionListForDropDown(), "Id", "Name");
            return View();

        }

        // POST: Employees/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EmployeeId,FirstName,MiddleName,LastName,EmailAddress,Employee_Position,Employee_Department")] Employee employee)
        {
            _cmethodbl = new CustomMethodBL();

            employee.EmployeeId = _cmethodbl.removeWhiteSpaces(employee.EmployeeId);
            employee.FirstName = _cmethodbl.removeWhiteSpaces(employee.FirstName);
            if (!string.IsNullOrEmpty(employee.MiddleName))
            {
                employee.MiddleName = _cmethodbl.removeWhiteSpaces(employee.MiddleName);
            }
            employee.LastName = _cmethodbl.removeWhiteSpaces(employee.LastName);
            employee.EmailAddress = _cmethodbl.removeWhiteSpaces(employee.EmailAddress);
            ModelState.Clear();
            TryValidateModel(employee);

            if (ModelState.IsValid)
            {
                _empbl = new EmployeeBL();

                employee.CreatedBy = User.Identity.Name;
                employee.Created = DateTimeOffset.Now;

                var result = _empbl.CreateEmployee(employee);
                if(result > 0)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return View("Error");
                }
              
            }

            _deptbl = new DepartmentBL();
            _posbl = new PositionBL();

            ViewBag.DateTimeCreated = DateTimeOffset.Now;
            ViewBag.CreatedBy = User.Identity.Name;
            ViewBag.Employee_Department = new SelectList(_deptbl.GetAllDepartmentListForDropDown(), "Id", "Code");
            ViewBag.Employee_Position = new SelectList(_posbl.GetAllPositionListForDropDown(), "Id", "Name");

            return View(employee);
        }


        // GET: Employee/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            _empbl = new EmployeeBL();
            var employee = _empbl.GetEmployeeDetails(id);

            if (employee == null)
            {
                return HttpNotFound();
            }

            _deptbl = new DepartmentBL();
            _posbl = new PositionBL();

            ViewBag.DateTimeCreated = DateTimeOffset.Now;
            ViewBag.CreatedBy = User.Identity.Name;
            ViewBag.Employee_Department = new SelectList(_deptbl.GetAllDepartmentListForDropDown(), "Id", "Code", employee.Employee_Department);
            ViewBag.Employee_Position = new SelectList(_posbl.GetAllPositionListForDropDown(), "Id", "Name", employee.Employee_Position);

            return View(employee);
        }


        // POST: Employee/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,EmployeeId,FirstName,MiddleName,LastName,EmailAddress,Employee_Position,Employee_Department,Created,CreatedBy,Modified,ModifiedBy")] Employee employee)
        {
            _cmethodbl = new CustomMethodBL();

            employee.ModifiedBy = User.Identity.Name;
            employee.Modified = DateTimeOffset.Now;
            employee.EmployeeId = _cmethodbl.removeWhiteSpaces(employee.EmployeeId);
            employee.FirstName = _cmethodbl.removeWhiteSpaces(employee.FirstName);

            if (!string.IsNullOrEmpty(employee.MiddleName))
            {
                employee.MiddleName = _cmethodbl.removeWhiteSpaces(employee.MiddleName);
            }

            employee.LastName = _cmethodbl.removeWhiteSpaces(employee.LastName);
            employee.EmailAddress = _cmethodbl.removeWhiteSpaces(employee.EmailAddress);

            ModelState.Clear();
            TryValidateModel(employee);

            if (ModelState.IsValid)
            {
                _empbl = new EmployeeBL();

                var result = _empbl.UpdateEmployee(employee);
                if (result > 0)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return View("Error");
                }

            }

            _deptbl = new DepartmentBL();
            _posbl = new PositionBL();

            ViewBag.DateTimeCreated = DateTimeOffset.Now;
            ViewBag.CreatedBy = User.Identity.Name;
            ViewBag.Employee_Department = new SelectList(_deptbl.GetAllDepartmentListForDropDown(), "Id", "Code", employee.Employee_Department);
            ViewBag.Employee_Position = new SelectList(_posbl.GetAllPositionListForDropDown(), "Id", "Name", employee.Employee_Position);

            return View();
        }


        // GET: Employee/Delete/5
        public ActionResult Delete(int? id)
        {
            _empbl = new EmployeeBL();

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var IsEmpExist = _empbl.GetEmployeeDetails(id);

            if (IsEmpExist == null)
            {
                return HttpNotFound();
            }
            return View(IsEmpExist);

        }

        // POST: Employee/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {

            _empbl = new EmployeeBL();

            Nullable<int> Deleteresult = null;

            //Deleteresult = _empbl.sp_DeleteEmployee(id); //Stored Procedure

            Deleteresult = _empbl.DeleteEmployee(id); 
            ViewBag.Message = Deleteresult;

            return View("Index", _empbl.GetAllEmployeeList());

        }

        #endregion

    }
}
