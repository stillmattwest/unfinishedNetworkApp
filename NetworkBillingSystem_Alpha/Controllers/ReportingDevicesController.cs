using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using NetworkBillingSystem_Alpha.DAL;
using NetworkBillingSystem_Alpha.Models;

namespace NetworkBillingSystem_Alpha.Controllers
{
    public class ReportingDevicesController : Controller
    {
        private ApplicationContext db = new ApplicationContext();

        // GET: ReportingDevices
        public ActionResult Index()
        {
            return View(db.ReportingDevices.ToList());
        }

        // GET: ReportingDevices/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ReportingDevice reportingDevice = db.ReportingDevices.Find(id);
            if (reportingDevice == null)
            {
                return HttpNotFound();
            }
            return View(reportingDevice);
        }

        // GET: ReportingDevices/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ReportingDevices/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ReportingDeviceID,DeviceName,IPAddress,UserName,Password")] ReportingDevice reportingDevice)
        {
            if (ModelState.IsValid)
            {
                db.ReportingDevices.Add(reportingDevice);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(reportingDevice);
        }

        // GET: ReportingDevices/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ReportingDevice reportingDevice = db.ReportingDevices.Find(id);
            if (reportingDevice == null)
            {
                return HttpNotFound();
            }
            return View(reportingDevice);
        }

        // POST: ReportingDevices/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ReportingDeviceID,DeviceName,IPAddress,UserName,Password")] ReportingDevice reportingDevice)
        {
            if (ModelState.IsValid)
            {
                db.Entry(reportingDevice).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(reportingDevice);
        }

        // GET: ReportingDevices/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ReportingDevice reportingDevice = db.ReportingDevices.Find(id);
            if (reportingDevice == null)
            {
                return HttpNotFound();
            }
            return View(reportingDevice);
        }

        // POST: ReportingDevices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ReportingDevice reportingDevice = db.ReportingDevices.Find(id);
            db.ReportingDevices.Remove(reportingDevice);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
