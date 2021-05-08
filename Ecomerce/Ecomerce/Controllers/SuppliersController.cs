using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Ecomerce.Clases;
using Ecomerce.Models;
using PagedList;

namespace Ecomerce.Controllers
{
    [Authorize(Roles = "User")]
    public class SuppliersController : Controller
    {
        private EcomerceContext db = new EcomerceContext();

        // GET: Suppliers
        public ActionResult Index(int? page = null)
        {
            page = (page ?? 1);
            var suppliers = db.Suppliers.Include(s => s.City).Include(s => s.Deparment);
            return View(suppliers.OrderBy(s => s.City.Name).ThenBy(s => s.FirstName).ToPagedList((int)page,5));
        }

        // GET: Suppliers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Supplier supplier = db.Suppliers.Find(id);
            if (supplier == null)
            {
                return HttpNotFound();
            }
            return View(supplier);
        }

        // GET: Suppliers/Create
        public ActionResult Create()
        {
            ViewBag.CityId = new SelectList(CombosHelper.GetCities(0), "CityId", "Name");
            ViewBag.DepartmentId = new SelectList(CombosHelper.GetDeparments(), "DepartmentId", "Name");
            return View();
        }

        // POST: Suppliers/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Supplier supplier)
        {
            if (ModelState.IsValid)
            {
                db.Suppliers.Add(supplier);
                var response = DbHelper.SaveChanges(db);

                if (!response.Succeeded)
                {
                    ModelState.AddModelError(string.Empty, response.Message);
                    ViewBag.CityId = new SelectList(CombosHelper.GetCities(supplier.DepartmentId), "CityId", "Name", supplier.CityId);
                    ViewBag.DepartmentId = new SelectList(CombosHelper.GetDeparments(), "DepartmentId", "Name", supplier.DepartmentId);
                    return View(supplier);
                }

                if (supplier.PhotoSupplierFile != null)
                {
                    var folder = "~/Content/Suppliers";
                    var file = string.Format("{0}.jpg", supplier.SupplierId);
                    var responsephoto = FilesHelper.UploadPhoto(supplier.PhotoSupplierFile, folder, file);
                    if (responsephoto)
                    {
                        var pic = string.Format("{0}/{1}.jpg", folder, supplier.SupplierId);
                        supplier.PhotoSupplier = pic;
                        db.Entry(supplier).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }

                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CityId = new SelectList(CombosHelper.GetCities(supplier.DepartmentId), "CityId", "Name", supplier.CityId);
            ViewBag.DepartmentId = new SelectList(CombosHelper.GetDeparments(), "DepartmentId", "Name", supplier.DepartmentId);
            return View(supplier);
        }

        // GET: Suppliers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var supplier = db.Suppliers.Find(id);
            if (supplier == null)
            {
                return HttpNotFound();
            }
            ViewBag.CityId = new SelectList(CombosHelper.GetCities(supplier.DepartmentId), "CityId", "Name", supplier.CityId);
            ViewBag.DepartmentId = new SelectList(CombosHelper.GetDeparments(), "DepartmentId", "Name", supplier.DepartmentId);
            return View(supplier);
        }

        // POST: Suppliers/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Supplier supplier)
        {
            if (ModelState.IsValid)
            {               
                try
                {
                    db.Entry(supplier).State = EntityState.Modified;
                    var response = DbHelper.SaveChanges(db);

                    if (!response.Succeeded)
                    {
                        ModelState.AddModelError(string.Empty, response.Message);
                        ViewBag.CityId = new SelectList(CombosHelper.GetCities(supplier.DepartmentId), "CityId", "Name", supplier.CityId);
                        ViewBag.DepartmentId = new SelectList(CombosHelper.GetDeparments(), "DepartmentId", "Name", supplier.DepartmentId);
                        return View(supplier);
                    }
                    if (supplier.PhotoSupplierFile != null)
                    {
                        var pic = string.Empty;
                        var folder = "~/Content/Suppliers";
                        var file = string.Format("{0}.jpg", supplier.SupplierId);
                        var responsePhoto = FilesHelper.UploadPhoto(supplier.PhotoSupplierFile, folder, file);
                        if (responsePhoto)
                        {
                            pic = string.Format("{0}/{1}.jpg", folder, supplier.SupplierId);
                            supplier.PhotoSupplier = pic;
                        }
                    }
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception)
                {

                    throw;
                }
            }
            ViewBag.CityId = new SelectList(CombosHelper.GetCities(supplier.DepartmentId), "CityId", "Name", supplier.CityId);
            ViewBag.DepartmentId = new SelectList(CombosHelper.GetDeparments(), "DepartmentId", "Name", supplier.DepartmentId);
            return View(supplier);
        }

        // GET: Suppliers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Supplier supplier = db.Suppliers.Find(id);
            if (supplier == null)
            {
                return HttpNotFound();
            }
            return View(supplier);
        }

        // POST: Suppliers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Supplier supplier = db.Suppliers.Find(id);
            db.Suppliers.Remove(supplier);
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
