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
        public ActionResult Details(string id)
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

        // GET: ConnectedDevices/Delete/5
        public ActionResult Delete(string id)
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
        public ActionResult DeleteConfirmed(string id)
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
