using Smits.Etg.FileRepositorySystem.BL;
using Smits.Etg.FileRepositorySystem.Models;
using Smits.Etg.FileRepositorySystem.Web.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Smits.Etg.FileRepositorySystem.Web.Controllers
{
    public class EmployeeFileController : Controller
    {
        private EmployeeBL _empBL;
        private EmployeeFileBL _empfBL;

        // GET: EmployeeFile
        public ActionResult Index(int? id, string errMsg = null)
        {
            _empfBL = new EmployeeFileBL();
            _empBL = new EmployeeBL();
            ViewBag.ErrorMessage = errMsg;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ViewBag.EmpId = id;
            var emp = _empBL.GetEmployeeDetails(id);

            var employeeFiles = _empfBL.GetAllEmployeeFileById(id);

            if (employeeFiles == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ViewBag.EmployeeName = emp.FullName;
            return View(employeeFiles);
        }

        public string ErrorMessage { get; set; }
        public decimal filesize { get; set; }

        [DllImport(@"urlmon.dll", CharSet = CharSet.Auto)]
        private extern static System.UInt32 FindMimeFromData(System.UInt32 pBC,
        [MarshalAs(UnmanagedType.LPStr)] System.String pwzUrl,
        [MarshalAs(UnmanagedType.LPArray)] byte[] pBuffer,
        System.UInt32 cbSize, [MarshalAs(UnmanagedType.LPStr)] System.String pwzMimeProposed,
        System.UInt32 dwMimeFlags,
        out System.UInt32 ppwzMimeOut,
        System.UInt32 dwReserverd);
        [HttpPost]
        public ActionResult Index(HttpPostedFileBase postedFile, int id)
        {
            //var employeeFiles = db.EmployeeFiles.Include(e => e.Employee).Where(ef => ef.EmployeeId == id);

            if (postedFile == null)
            {
                this.AddNotification("Please select file!", NotificationType.INFO);
                //ErrorMessage = "Please select file!";
                return RedirectToAction("Index", new { id = id, errMsg = ErrorMessage });
            }
            byte[] bytes;
            filesize = 500000;

            if (postedFile.ContentLength > filesize)
            {
                filesize = (filesize / 1000) / 100;
                //ErrorMessage = "File size should be " + filesize + "MB or less";
                this.AddNotification("File size should be " + filesize + "MB or less", NotificationType.INFO);
            }
            else
            {
                using (BinaryReader br = new BinaryReader(postedFile.InputStream))
                {
                    bytes = br.ReadBytes(postedFile.ContentLength);
                }
                System.UInt32 mimetype;
                FindMimeFromData(0, null, bytes, 256, null, 0, out mimetype, 0);
                System.IntPtr mimeTypePtr = new IntPtr(mimetype);
                string mime = Marshal.PtrToStringUni(mimeTypePtr);
                Marshal.FreeCoTaskMem(mimeTypePtr);

                if (mime == "application/pdf" || mime == "image/jpeg" || mime == "image/pjpeg" || mime == "image/png")
                {
                    EmployeeFile empfile = new EmployeeFile();

                    empfile.FileName = Path.GetFileName(postedFile.FileName);
                    empfile.ContentType = postedFile.ContentType;
                    empfile.FileBytes = bytes;
                    empfile.EmployeeId = id;
                    empfile.Created = DateTimeOffset.Now;
                    empfile.CreatedBy = User.Identity.Name;

                    _empfBL = new EmployeeFileBL();
                    var result = _empfBL.CreateEmployeeFile(empfile);
                    if (result > 0)
                    {
                        //ErrorMessage = "File Is Successfully Uploaded";
                        this.AddNotification("File Is Successfully Uploaded", NotificationType.SUCCESS);
                    }
                    else
                    {
                        //ErrorMessage = "File not upload error";
                        this.AddNotification("File upload error", NotificationType.ERROR);
                    }


                }
                else
                {
                    //ErrorMessage = "MIME type is invalid - Only JPEG/PDF/PNG file allowed!";
                    this.AddNotification("MIME type is invalid - Only JPEG/PDF/PNG file allowed!", NotificationType.WARNING);
                }

            }

            return RedirectToAction("Index", new { id = id, errMsg = ErrorMessage });
        }

        [HttpPost]
        public FileResult DownloadFile(int? fileId)
        {
            _empfBL = new EmployeeFileBL();

            var employeeFiles = _empfBL.DownloadFile(fileId);

            return File(employeeFiles.FileBytes, employeeFiles.ContentType, employeeFiles.FileName);
        }

      

        // GET: EmployeeFile/Delete/5
        public ActionResult Delete(int? id)
        {
            _empfBL = new EmployeeFileBL();
            _empBL = new EmployeeBL();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var employeeFile = _empfBL.FindEmployeeFileById(id);
            var emp = _empBL.GetEmployeeDetails(employeeFile.EmployeeId);

            ViewBag.employee = emp;

            if (employeeFile == null)
            {
                return HttpNotFound();
            }
            return View(employeeFile);
        }

        // POST: EmployeeFiles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _empfBL = new EmployeeFileBL();
            _empBL = new EmployeeBL();

            var employeeFile = _empfBL.FindEmployeeFileById(id);
    
            var result = _empfBL.DeleteEmployeeFile(employeeFile);
            if (result > 0)
            {
                this.AddNotification("File delete error", NotificationType.ERROR);
            }
            else
            {
                this.AddNotification("File deleted", NotificationType.SUCCESS);
            }
            //return RedirectToAction("Index");

            return RedirectToAction("Index", new { id = employeeFile.EmployeeId });
        }
    }
}
