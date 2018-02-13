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
    public class ParkedVehiclesController : Controller
    {

        
        
        private GarageContext db = new GarageContext();
        private Parking parking = new Parking();
        //Search By Registration Number


        public ActionResult SearchByRegNumber(string searchByRegNum = "", string searchByAny = "", string Sorting = "") {
            var model = db.ParkedVehicles.Select(g => g);


            //model = model.Where(r => r.VehicleType.Contains(searchByAny) && r.RegistrationNumber.Contains(searchByRegNum));


            //if (searchByRegNum != "")
            //{


            //    model = model.Where(r => r.Model.Contains(searchByAny));

            //}
            //else if (searchByAny != "")
            //{
            //    model = model.Where(r => r.Model.Contains(searchByAny));

            //}

            //else
            //{
            //    model = model.Where(r => r.Color.Contains("rabbit"));

            //}


            if(Sorting != "")
            {
                switch (Sorting)
                {

                    case "RegistrationNumber":
                        model = model.OrderBy(x => x.RegistrationNumber);
                        break;
                    case "VehicleType":
                        model = model.OrderBy(x => x.VehicleType);
                        break;
                    case "CheckinTime":
                        model = model.OrderBy(x => x.CheckInTime);
                        break;
                   

                }
               


            }




            if (searchByRegNum != "")
            {
                if (searchByAny != "")

                    model = model.Where(r => r.VehicleType.Contains(searchByAny) && r.RegistrationNumber.Contains(searchByRegNum));

                else
                {

                    model = model.Where(r => r.RegistrationNumber.Contains(searchByRegNum));

                }

            }
            else if (searchByAny != "")
            {


                model = model.Where(r => r.VehicleType.Contains(searchByAny));

            }
            else
            {

                model = model.Select(r => r);

            }

            //     model = db.ParkedVehicles.Where(r => searchByRegNum == null || 
            //r.RegistrationNumber.Contains(searchByRegNum) || searchByAny == null ||
            //r.VehicleType.Contains(searchByAny)).Select(g => new ParkedVehicleViewModel { Id = g.Id, RegistrationNumber = g.RegistrationNumber, VehicleType = g.VehicleType, CheckInTime = g.CheckInTime });
            var xxxx = model.Select(g => new ParkedVehicleViewModel { Id = g.Id, RegistrationNumber = g.RegistrationNumber, VehicleType = g.VehicleType, CheckInTime = g.CheckInTime });


            return View(xxxx);
    }


    // GET: ParkedVehicles
    public ActionResult Index()
        {

            var model = db.ParkedVehicles.Select(g => new ParkedVehicleViewModel { Id  = g.Id, RegistrationNumber=g.RegistrationNumber
                , VehicleType= g.VehicleType, CheckInTime=g.CheckInTime
             //   , ParkingPlace = parking.GetParkingPlaceString(g.Id)
            }
                );
            return View(model);

        }

        // GET: Kvitto
        public ActionResult Kvitto(ParkedVehicle parkedVehicle)
        {
            var kvittoModel = new KvittoViewModel
            {
                RegistrationNumber = parkedVehicle.RegistrationNumber,
                CheckInTime = parkedVehicle.CheckInTime,
                CheckOutTime = DateTime.Now,
                ParkingTime = DateTime.Now - parkedVehicle.CheckInTime,
            };
            return View(kvittoModel);

        }


        // GET: ParkedVehicles/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ParkedVehicle parkedVehicle = db.ParkedVehicles.Find(id);
            if (parkedVehicle == null)
            {
                return HttpNotFound();
            }
            return View(parkedVehicle);
        }

        // GET: ParkedVehicles/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ParkedVehicles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,RegistrationNumber,Brand,VehicleType,Model,Color,FuelType")] ParkedVehicle parkedVehicle)
        {
            if (ModelState.IsValid)
            {
                parkedVehicle.CheckInTime = DateTime.Now;
                db.ParkedVehicles.Add(parkedVehicle);
                db.SaveChanges();
                //Parking     
                var newParkingPlace = parking.GetFreeParkingPlace(parkedVehicle.VehicleType);
                foreach (var item in newParkingPlace)
                {
                    var parkingVehicle = new Parking() { VehicleType = parkedVehicle.VehicleType, ParkingPlace = item , ParkedVehicleId = parkedVehicle.Id };
                    db.Parkings.Add(parkingVehicle);
                }

                db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(parkedVehicle);
        }

        // GET: ParkedVehicles/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ParkedVehicle parkedVehicle = db.ParkedVehicles.Find(id);
            if (parkedVehicle == null)
            {
                return HttpNotFound();
            }
            return View(parkedVehicle);
        }

        // POST: ParkedVehicles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,RegistrationNumber,Brand,VehicleType,Model,Color,FuelType,CheckInTime")] ParkedVehicle parkedVehicle)
        {
            if (ModelState.IsValid)
            {
                db.Entry(parkedVehicle).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(parkedVehicle);
        }

        // GET: ParkedVehicles/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ParkedVehicle parkedVehicle = db.ParkedVehicles.Find(id);
            if (parkedVehicle == null)
            {
                return HttpNotFound();
            }
            return View(parkedVehicle);
        }

        // POST: ParkedVehicles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ParkedVehicle parkedVehicle = db.ParkedVehicles.Find(id);
            db.ParkedVehicles.Remove(parkedVehicle);
            db.SaveChanges();
            //Remove Parking     
            var removeParkingPlace = parking.GetParkingPlaceId(parkedVehicle.Id);
            foreach (var item in removeParkingPlace)
            {
                Parking removedVehicle = db.Parkings.Find(item);
                db.Parkings.Remove(removedVehicle);
            }

            db.SaveChanges();




            return RedirectToAction("Kvitto", parkedVehicle);
        }
        //GetAllSpaces
        public ActionResult GaragePlacesInfo ()
        {
            var model = new ParkingInfoViewModel()
            {
                ParkingTotalSpace = parking.ParkingSize,
                ParkingAvailableSpace = parking.GetOccupiedParkingPlaces()
            };
            
            return PartialView(model);

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
