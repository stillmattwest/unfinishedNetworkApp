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
    public class ConnectedDevicesController : Controller
    {
        private ApplicationContext db = new ApplicationContext();

        // GET: ConnectedDevices
        public ActionResult Index()
        {
            return View(db.ConnectedDevices.ToList());
        }

        // GET: ConnectedDevices/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ConnectedDevice connectedDevice = db.ConnectedDevices.Find(id);
            if (connectedDevice == null)
            {
                return HttpNotFound();
            }
            return View(connectedDevice);
        }

        // GET: ConnectedDevices/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ConnectedDevices/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ConnectedDeviceID,Mac")] ConnectedDevice connectedDevice)
        {
            if (ModelState.IsValid)
            {
                db.ConnectedDevices.Add(connectedDevice);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(connectedDevice);
        }

        // GET: ConnectedDevices/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ConnectedDevice connectedDevice = db.ConnectedDevices.Find(id);
            if (connectedDevice == null)
            {
                return HttpNotFound();
            }
            return View(connectedDevice);
        }

        // POST: ConnectedDevices/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ConnectedDeviceID,Mac")] ConnectedDevice connectedDevice)
        {
            if (ModelState.IsValid)
            {
                db.Entry(connectedDevice).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(connectedDevice);
        }

        // GET: ConnectedDevices/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ConnectedDevice connectedDevice = db.ConnectedDevices.Find(id);
            if (connectedDevice == null)
            {
                return HttpNotFound();
            }
            return View(connectedDevice);
        }

        // POST: ConnectedDevices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ConnectedDevice connectedDevice = db.ConnectedDevices.Find(id);
            db.ConnectedDevices.Remove(connectedDevice);
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
