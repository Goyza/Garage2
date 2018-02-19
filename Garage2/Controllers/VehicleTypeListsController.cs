using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Garage2.DataAccessLayer;
using Garage2.Models;

namespace Garage2.Controllers
{
    public class VehicleTypeListsController : Controller
    {
        private GarageContext db = new GarageContext();

        // GET: VehicleTypeLists
        public ActionResult Index()
        {
            return View(db.VehicleTypeLists.ToList());
        }

        // GET: VehicleTypeLists/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VehicleTypeList vehicleTypeList = db.VehicleTypeLists.Find(id);
            if (vehicleTypeList == null)
            {
                return HttpNotFound();
            }
            return View(vehicleTypeList);
        }

        // GET: VehicleTypeLists/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: VehicleTypeLists/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,VehicleType,RequredSpace")] VehicleTypeList vehicleTypeList)
        {
            if (ModelState.IsValid)
            {
                db.VehicleTypeLists.Add(vehicleTypeList);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(vehicleTypeList);
        }

        // GET: VehicleTypeLists/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VehicleTypeList vehicleTypeList = db.VehicleTypeLists.Find(id);
            if (vehicleTypeList == null)
            {
                return HttpNotFound();
            }
            return View(vehicleTypeList);
        }

        // POST: VehicleTypeLists/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,VehicleType,RequredSpace")] VehicleTypeList vehicleTypeList)
        {
            if (ModelState.IsValid)
            {
                db.Entry(vehicleTypeList).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(vehicleTypeList);
        }

        // GET: VehicleTypeLists/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VehicleTypeList vehicleTypeList = db.VehicleTypeLists.Find(id);
            if (vehicleTypeList == null)
            {
                return HttpNotFound();
            }
            return View(vehicleTypeList);
        }

        // POST: VehicleTypeLists/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            VehicleTypeList vehicleTypeList = db.VehicleTypeLists.Find(id);
            db.VehicleTypeLists.Remove(vehicleTypeList);
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
