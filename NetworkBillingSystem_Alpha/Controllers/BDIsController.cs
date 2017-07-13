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
    public class BDIsController : Controller
    {
        private ApplicationContext db = new ApplicationContext();

        // GET: BDIs
        public ActionResult Index()
        {
            return View(db.BDIs.ToList());
        }

        // GET: BDIs/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BDI bDI = db.BDIs.Find(id);
            if (bDI == null)
            {
                return HttpNotFound();
            }
            return View(bDI);
        }

        // GET: BDIs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BDIs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BDINumber")] BDI bDI)
        {
            if (ModelState.IsValid)
            {
                db.BDIs.Add(bDI);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(bDI);
        }

        // GET: BDIs/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BDI bDI = db.BDIs.Find(id);
            if (bDI == null)
            {
                return HttpNotFound();
            }
            return View(bDI);
        }

        // POST: BDIs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BDINumber")] BDI bDI)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bDI).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(bDI);
        }

        // GET: BDIs/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BDI bDI = db.BDIs.Find(id);
            if (bDI == null)
            {
                return HttpNotFound();
            }
            return View(bDI);
        }

        // POST: BDIs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            BDI bDI = db.BDIs.Find(id);
            db.BDIs.Remove(bDI);
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
