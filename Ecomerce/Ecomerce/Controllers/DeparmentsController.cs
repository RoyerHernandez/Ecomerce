using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Ecomerce.Models;

namespace Ecomerce.Controllers
{
    public class DeparmentsController : Controller
    {
        private EcomerceContext db = new EcomerceContext();

        // GET: Deparments
        public ActionResult Index()
        {
            return View(db.Deparments.ToList());
        }

        // GET: Deparments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Deparment deparment = db.Deparments.Find(id);
            if (deparment == null)
            {
                return HttpNotFound();
            }
            return View(deparment);
        }

        // GET: Deparments/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Deparments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DepartmentId,Name")] Deparment deparment)
        {
            if (ModelState.IsValid)
            {
                db.Deparments.Add(deparment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(deparment);
        }

        // GET: Deparments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Deparment deparment = db.Deparments.Find(id);
            if (deparment == null)
            {
                return HttpNotFound();
            }
            return View(deparment);
        }

        // POST: Deparments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DepartmentId,Name")] Deparment deparment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(deparment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(deparment);
        }

        // GET: Deparments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Deparment deparment = db.Deparments.Find(id);
            if (deparment == null)
            {
                return HttpNotFound();
            }
            return View(deparment);
        }

        // POST: Deparments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Deparment deparment = db.Deparments.Find(id);
            db.Deparments.Remove(deparment);
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
