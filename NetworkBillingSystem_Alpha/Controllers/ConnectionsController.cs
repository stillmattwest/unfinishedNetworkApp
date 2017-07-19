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
    public class ConnectionsController : Controller
    {
        private ApplicationContext db = new ApplicationContext();

        // GET: Connections
        public ActionResult Index()
        {
            var connections = db.Connections.Include(c => c.BDI).Include(c => c.ConnectedDevice).Include(c => c.Department).Include(c => c.ReportingDevice);
            return View(connections.ToList().OrderBy(x => x.BDINumber));
        }

        // GET: Connections/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Connection connection = db.Connections.Find(id);
            if (connection == null)
            {
                return HttpNotFound();
            }
            return View(connection);
        }

        // GET: Connections/Create
        public ActionResult Create()
        {
            ViewBag.BDINumber = new SelectList(db.BDIs, "BDINumber", "BDINumber");
            ViewBag.ConnectedDeviceID = new SelectList(db.ConnectedDevices, "ConnectedDeviceID", "Mac");
            ViewBag.DepartmentID = new SelectList(db.Departments, "DepartmentID", "Name");
            ViewBag.ReportingDeviceID = new SelectList(db.ReportingDevices, "ReportingDeviceID", "DeviceName");
            return View();
        }

        // POST: Connections/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ConnectionID,ConnectionDateTime,BDINumber,ConnectedDeviceID,ReportingDeviceID,DepartmentID")] Connection connection)
        {
            if (ModelState.IsValid)
            {
                db.Connections.Add(connection);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.BDINumber = new SelectList(db.BDIs, "BDINumber", "BDINumber", connection.BDINumber);
            ViewBag.ConnectedDeviceID = new SelectList(db.ConnectedDevices, "ConnectedDeviceID", "Mac", connection.ConnectedDeviceID);
            ViewBag.DepartmentID = new SelectList(db.Departments, "DepartmentID", "Name", connection.DepartmentID);
            ViewBag.ReportingDeviceID = new SelectList(db.ReportingDevices, "ReportingDeviceID", "DeviceName", connection.ReportingDeviceID);
            return View(connection);
        }

        // GET: Connections/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Connection connection = db.Connections.Find(id);
            if (connection == null)
            {
                return HttpNotFound();
            }
            ViewBag.BDINumber = new SelectList(db.BDIs, "BDINumber", "BDINumber", connection.BDINumber);
            ViewBag.ConnectedDeviceID = new SelectList(db.ConnectedDevices, "ConnectedDeviceID", "Mac", connection.ConnectedDeviceID);
            ViewBag.DepartmentID = new SelectList(db.Departments, "DepartmentID", "Name", connection.DepartmentID);
            ViewBag.ReportingDeviceID = new SelectList(db.ReportingDevices, "ReportingDeviceID", "DeviceName", connection.ReportingDeviceID);
            return View(connection);
        }

        // POST: Connections/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ConnectionID,ConnectionDateTime,BDINumber,ConnectedDeviceID,ReportingDeviceID,DepartmentID")] Connection connection)
        {
            if (ModelState.IsValid)
            {
                db.Entry(connection).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BDINumber = new SelectList(db.BDIs, "BDINumber", "BDINumber", connection.BDINumber);
            ViewBag.ConnectedDeviceID = new SelectList(db.ConnectedDevices, "ConnectedDeviceID", "Mac", connection.ConnectedDeviceID);
            ViewBag.DepartmentID = new SelectList(db.Departments, "DepartmentID", "Name", connection.DepartmentID);
            ViewBag.ReportingDeviceID = new SelectList(db.ReportingDevices, "ReportingDeviceID", "DeviceName", connection.ReportingDeviceID);
            return View(connection);
        }

        // GET: Connections/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Connection connection = db.Connections.Find(id);
            if (connection == null)
            {
                return HttpNotFound();
            }
            return View(connection);
        }

        // POST: Connections/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Connection connection = db.Connections.Find(id);
            db.Connections.Remove(connection);
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
