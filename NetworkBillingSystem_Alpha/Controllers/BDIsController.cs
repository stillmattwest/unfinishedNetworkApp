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
            var bDIs = db.BDIs.Include(b => b.Department).OrderBy(x => x.BDINumber);
            return View(bDIs.ToList());
        }

        // GET: BDIs/Details/5
        public ActionResult Details(int? id)
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
            ViewBag.DepartmentId = new SelectList(db.Departments, "DepartmentID", "Name");
            return View();
        }

        // POST: BDIs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BDIID,BDINumber,DepartmentId")] BDI bDI)
        {
            if (ModelState.IsValid)
            {
                db.BDIs.Add(bDI);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DepartmentId = new SelectList(db.Departments, "DepartmentID", "Name", bDI.DepartmentId);
            return View(bDI);
        }

        // GET: BDIs/Edit/5
        public ActionResult Edit(int? id)
        {
            var depts = db.Departments.ToList().OrderBy(x => x.Name);
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BDI bDI = db.BDIs.Find(id);
            if (bDI == null)
            {
                return HttpNotFound();
            }
            ViewBag.DepartmentId = new SelectList(depts, "DepartmentID", "Name", bDI.DepartmentId);
            return View(bDI);
        }

        // POST: BDIs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BDIID,BDINumber,DepartmentId")] BDI bDI)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bDI).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DepartmentId = new SelectList(db.Departments, "DepartmentID", "Name", bDI.DepartmentId);
            return View(bDI);
        }

        // GET: BDIs/Delete/5
        public ActionResult Delete(int? id)
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
        public ActionResult DeleteConfirmed(int id)
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
