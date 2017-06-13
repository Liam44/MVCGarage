using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MVCGarage.DataAccess;
using MVCGarage.Models;

namespace MVCGarage.Controllers
{
    public class DefaultFeesController : Controller
    {
        private GarageContext db = new GarageContext();

        // GET: DefaultFees
        public ActionResult Index()
        {
            var defaultFees = db.DefaultFees.Include(d => d.VehicleType);
            return View(defaultFees.ToList());
        }

        // GET: DefaultFees/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DefaultFee defaultFee = db.DefaultFees.Find(id);
            if (defaultFee == null)
            {
                return HttpNotFound();
            }
            return View(defaultFee);
        }

        // GET: DefaultFees/Create
        public ActionResult Create()
        {
            ViewBag.ID = new SelectList(db.VehicleTypes, "ID", "Type");
            return View();
        }

        // POST: DefaultFees/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Fee,VehicleTypeID")] DefaultFee defaultFee)
        {
            if (ModelState.IsValid)
            {
                db.DefaultFees.Add(defaultFee);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ID = new SelectList(db.VehicleTypes, "ID", "Type", defaultFee.ID);
            return View(defaultFee);
        }

        // GET: DefaultFees/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DefaultFee defaultFee = db.DefaultFees.Find(id);
            if (defaultFee == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID = new SelectList(db.VehicleTypes, "ID", "Type", defaultFee.ID);
            return View(defaultFee);
        }

        // POST: DefaultFees/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Fee,VehicleTypeID")] DefaultFee defaultFee)
        {
            if (ModelState.IsValid)
            {
                db.Entry(defaultFee).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ID = new SelectList(db.VehicleTypes, "ID", "Type", defaultFee.ID);
            return View(defaultFee);
        }

        // GET: DefaultFees/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DefaultFee defaultFee = db.DefaultFees.Find(id);
            if (defaultFee == null)
            {
                return HttpNotFound();
            }
            return View(defaultFee);
        }

        // POST: DefaultFees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DefaultFee defaultFee = db.DefaultFees.Find(id);
            db.DefaultFees.Remove(defaultFee);
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
