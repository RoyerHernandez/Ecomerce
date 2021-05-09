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
    [Authorize(Roles ="User")]
    public class CustomersController : Controller
    {
        private EcomerceContext db = new EcomerceContext();

        // GET: Customers
        public ActionResult Index(int? page = null)
        {
            page = (page ?? 1);
            var user = db.Users.Where(u => u.UserName == User.Identity.Name).FirstOrDefault();

            var qry = (from cu in db.Customers
                       join cc in db.CompanyCustomers on cu.CustomerId equals cc.CustomerId
                       join co in db.Companies on cc.CompanyId equals co.CompanyId
                       where co.CompanyId == user.CompanyId
                       select new { cu }).ToList();

            var customers = new List<Customer>();
            foreach (var item in qry)
            {
                customers.Add(item.cu);
            }

            return View(customers.OrderBy(c => c.FirstName).ThenBy(c => c.LastName).ToPagedList((int)page,5));
        }

        // GET: Customers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // GET: Customers/Create
        public ActionResult Create()
        {            
            ViewBag.CityId = new SelectList(CombosHelper.GetCities(0), "CityId", "Name");
            ViewBag.DepartmentId = new SelectList(CombosHelper.GetDeparments(), "DepartmentId", "Name");            
            return View();
        }

        // POST: Customers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Customer customer)
        {
            if (ModelState.IsValid)
            {
                using (var transaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        db.Customers.Add(customer);
                        var response = DbHelper.SaveChanges(db);


                        if (!response.Succeeded)
                        {
                            ModelState.AddModelError(string.Empty, response.Message);
                            transaction.Rollback();
                            ViewBag.CityId = new SelectList(CombosHelper.GetCities(customer.DepartmentId), "CityId", "Name", customer.CityId);
                            ViewBag.DepartmentId = new SelectList(CombosHelper.GetDeparments(), "DepartmentId", "Name", customer.DepartmentId);
                            return View(customer);
                        }

                        UsersHelper.CreateUserASP(customer.UserName, "Customer", customer.UserName);

                        var user = db.Users.Where(u => u.UserName == User.Identity.Name).FirstOrDefault();
                        var companyCustomer = new CompanyCustomer
                        {
                            CompanyId = user.CompanyId,
                            CustomerId = customer.CustomerId
                        };

                        db.CompanyCustomers.Add(companyCustomer);

                        if (customer.PhotoCustomerFile != null)
                        {
                            var folder = "~/Content/Customers";
                            var file = string.Format("{0}.jpg", customer.CustomerId);
                            var responsephoto = FilesHelper.UploadPhoto(customer.PhotoCustomerFile, folder, file);
                            if (responsephoto)
                            {
                                var pic = string.Format("{0}/{1}.jpg", folder, customer.CustomerId);
                                customer.PhotoCustomer = pic;
                                db.Entry(customer).State = EntityState.Modified;
                                db.SaveChanges();
                            }
                        }

                        db.SaveChanges();
                        transaction.Commit();
                        return RedirectToAction("Index");

                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        ModelState.AddModelError(string.Empty, ex.Message);
                    }
                }
            }

            ViewBag.CityId = new SelectList(CombosHelper.GetCities(customer.DepartmentId), "CityId", "Name", customer.CityId);
            ViewBag.DepartmentId = new SelectList(CombosHelper.GetDeparments(), "DepartmentId", "Name", customer.DepartmentId);
            return View(customer);
        }

        // GET: Customers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }

            ViewBag.CityId = new SelectList(CombosHelper.GetCities(customer.DepartmentId), "CityId", "Name", customer.CityId);
            ViewBag.DepartmentId = new SelectList(CombosHelper.GetDeparments(), "DepartmentId", "Name", customer.DepartmentId);
            return View(customer);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Customer customer)
        {
            if (ModelState.IsValid)
            {

                using (var transaction = db.Database.BeginTransaction())
                {

                    try
                    {
                        db.Entry(customer).State = EntityState.Modified;
                        var response = DbHelper.SaveChanges(db);

                        if (!response.Succeeded)
                        {
                            ModelState.AddModelError(string.Empty, response.Message);
                            transaction.Rollback();
                            ViewBag.CityId = new SelectList(CombosHelper.GetCities(customer.DepartmentId), "CityId", "Name", customer.CityId);
                            ViewBag.DepartmentId = new SelectList(CombosHelper.GetDeparments(), "DepartmentId", "Name", customer.DepartmentId);
                            return View(customer);
                        }

                        if (customer.PhotoCustomerFile != null)
                        {
                            var pic = string.Empty;
                            var folder = "~/Content/Customers";
                            var file = string.Format("{0}.jpg", customer.CustomerId);
                            var responsePhoto = FilesHelper.UploadPhoto(customer.PhotoCustomerFile, folder, file);
                            if (responsePhoto)
                            {
                                pic = string.Format("{0}/{1}.jpg", folder, customer.CustomerId);
                                customer.PhotoCustomer = pic;
                            }
                        }

                        // TODO: Validate when the Customer E-Mail Change
                        db.SaveChanges();
                        transaction.Commit();
                        return RedirectToAction("Index");

                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        ModelState.AddModelError(string.Empty, ex.Message);
                    }
                }               
            }
            ViewBag.CityId = new SelectList(db.Cities, "CityId", "Name", customer.CityId);
            ViewBag.CompanyId = new SelectList(db.Companies, "CompanyId", "Name", customer.CustomerId);
            ViewBag.DepartmentId = new SelectList(db.Deparments, "DepartmentId", "Name", customer.DepartmentId);
            return View(customer);
        }

        // GET: Customers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var customer = db.Customers.Find(id);
            var user = db.Users.Where(u => u.UserName == User.Identity.Name).FirstOrDefault();
            var companyCustomer = db.CompanyCustomers.Where(cc => cc.CompanyId == user.CompanyId && cc.CustomerId == customer.CustomerId).FirstOrDefault();

            using (var transaction = db.Database.BeginTransaction())
            {
                db.CompanyCustomers.Remove(companyCustomer);
                db.Customers.Remove(customer);
                var response = DbHelper.SaveChanges(db);

                if (response.Succeeded)
                {
                    transaction.Commit();
                    return RedirectToAction("Index");
                }

                transaction.Rollback();
                ModelState.AddModelError(string.Empty, response.Message);
                return View(customer);
            }
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
