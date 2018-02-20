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
        private VehicleTypeList vehicleTypeList = new VehicleTypeList();

        //Search By Registration Number
        public ActionResult SearchByRegNumber(string searchByRegNum = "", string brand = "", string vehicletype = "", string vehiclemodel = "", string color = "", string typeoffuel = "", string Sorting = "") {
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
                        model = model.OrderBy(x => x.VehicleTypeList.VehicleType);
                        break;
                    case "CheckinTime":
                        model = model.OrderBy(x => x.CheckInTime);
                        break;
                    



                }
               


            }

            if (searchByRegNum != "")
            {
                model = model.Where(r => r.RegistrationNumber.Contains(searchByRegNum));

            }

            if (brand != "")
            {
                model = model.Where(r => r.Brand.Contains(brand));

            }

            if (vehicletype != "")
            {
                model = model.Where(r => r.VehicleTypeList.VehicleType.Contains(vehicletype));

            }

            if (color != "")
            {
                model = model.Where(r => r.Color.Contains(color));

            }

            if (typeoffuel != "")
            {
                model = model.Where(r => r.FuelType.Contains(typeoffuel));

            }

            if (vehiclemodel != "")
            {
                model = model.Where(r => r.Model.Contains(vehiclemodel));
            }
            
         

            //     model = db.ParkedVehicles.Where(r => searchByRegNum == null || 
            //r.RegistrationNumber.Contains(searchByRegNum) || searchByAny == null ||
            //r.VehicleType.Contains(searchByAny)).Select(g => new ParkedVehicleViewModel { Id = g.Id, RegistrationNumber = g.RegistrationNumber, VehicleType = g.VehicleType, CheckInTime = g.CheckInTime });
            var xxxx = model.Select(g => new ParkedVehicleViewModel { Id = g.Id
                , RegistrationNumber = g.RegistrationNumber, VehicleType = g.VehicleTypeList.VehicleType, CheckInTime = g.CheckInTime, Customer = g.Customer.LastName + " " + g.Customer.FirstName });


            return View(xxxx);
    }

        public ActionResult Statistics()
        {
            var vehicles = db.ParkedVehicles.Select(g => g.VehicleTypeList.VehicleType);

            int cars = 0;
            int buses = 0;
            int boats = 0;
            int moto = 0;
            int undefined = 0;

            foreach (string e in vehicles)
                {
                    if(e == "Car")
                    {
                    cars++;                  
                    }
                    else if(e == "Bus")
                    {
                    buses++; ;     
                    }
                    else if (e == "Boats")
                    {
                    boats++;
                    }
                else if (e == "Moto")
                {
                    moto++;
                }
                else if (e == "Undefined")
                    {
                    undefined++;
                    }
                }

            Statistics statistics = new Statistics
            {
                NumberOfVehicles = vehicles.Count(),
                NumbeOfCars = cars,
                NumberOfBuses = buses,
                NumberOfBoats = boats,
                NumberOfMoto = moto,
                NumberOfUndefined = undefined
            };

            var checkInTime = db.ParkedVehicles.Select(t => t.CheckInTime);
 
            foreach (DateTime t in checkInTime)
            {
                statistics.TotParkingTime += DateTime.Now - t;
            }

          //  statistics.Revenue = (Decimal) statistics.TotParkingTime.TotalMinutes * CostPerMinute.costPerMinute;
            statistics.ParkingStatString = parking.GetAllFreeParkingPlace();
            statistics.ParkingStatGroup = parking.GetParkingVehicles();

            return View(statistics);
        }



    // GET: ParkedVehicles
    public ActionResult Index(string searchByRegNum = "", [Bind(Prefix = "VehicleTypeList")] string VehicleType = "")
        {
           ViewBag.VehicleTypeList = new SelectList(db.VehicleTypeLists, "VehicleType", "VehicleType");


            var model = db.ParkedVehicles.Select(g => new ParkedVehicleViewModel { Id  = g.Id, RegistrationNumber=g.RegistrationNumber
                , VehicleType= g.VehicleTypeList.VehicleType, CheckInTime=g.CheckInTime, Customer=g.Customer.LastName +" "+ g.Customer.FirstName
                //   , ParkingPlace = parking.GetParkingPlaceString(g.Id)
            }
                );


            if (searchByRegNum != "")
            {
                model = model.Where(r => r.RegistrationNumber.Contains(searchByRegNum));

            }
           
            if (VehicleType != "")
            {
                model = model.Where(r => r.VehicleType.Contains(VehicleType));

            }
          
            return View(model);
        }
        public ActionResult DetailedIndex()
        {

            var model = db.ParkedVehicles.Select(g => g);
             
            return View(model);
        }


        // GET: Kvitto
        public ActionResult Kvitto(KvittoViewModel Kvitto)
        {
            var kvitto = Kvitto;
           
            return View(kvitto);

        }

        // GET: Kvitto
        public ActionResult PrintableKvitto(string RegistrationNumber, DateTime CheckInTime, DateTime CheckOutTime, TimeSpan ParkingTime, string Customer)
            
        {
            var kvittoModel = new KvittoViewModel
            {
                RegistrationNumber = RegistrationNumber,
                CheckInTime = CheckInTime,
                CheckOutTime = CheckOutTime,
                ParkingTime = ParkingTime,
                Customer = Customer.ToString()
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
            //ViewBag.VehicleTypeListId = new SelectList(db.VehicleTypeLists, "Id", "VehicleType");
           // ViewBag.CustomerId = new SelectList(db.Customers, "Id", "LastName");
            var model = new ParkingVehicleEdit();           
            return View(model);
        }

        // POST: ParkedVehicles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,RegistrationNumber,Brand,VehicleType,Model,Color,FuelType,CustomerId,VehicleTypeListId")] ParkingVehicleEdit parkedVehicleEdit)
        {
            //var xxx = db.VehicleTypeLists.Where(r => r.Id == parkedVehicleEdit.VehicleTypeListId).First();           

            if (ModelState.IsValid)
            {
             //   parkedVehicle.CheckInTime = DateTime.Now;

                ParkedVehicle parkedVehicle = new ParkedVehicle()
                {
                    Brand = parkedVehicleEdit.Brand,
                    Id = parkedVehicleEdit.Id,
                    CheckInTime = DateTime.Now,
                
                    Color = parkedVehicleEdit.Color,
                    FuelType = parkedVehicleEdit.FuelType,
                    Model = parkedVehicleEdit.Model,
                    RegistrationNumber = parkedVehicleEdit.RegistrationNumber,
                   // VehicleType = xxx.VehicleType,
                    CustomerId = parkedVehicleEdit.CustomerId,
                    VehicleTypeListId=parkedVehicleEdit.VehicleTypeListId
                };

                db.ParkedVehicles.Add(parkedVehicle);
                db.SaveChanges();
                //Parking     
                var newParkingPlace = parking.GetFreeParkingPlace(parkedVehicle.VehicleTypeListId);
                foreach (var item in newParkingPlace)
                {
                    var parkingVehicle = new Parking() { //VehicleType = parkedVehicle.VehicleType ,
                        ParkingPlace = item , ParkedVehicleId = parkedVehicle.Id };
                    db.Parkings.Add(parkingVehicle);
                }

                db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(parkedVehicleEdit);
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
        public ActionResult Edit([Bind(Include = "Id,RegistrationNumber,Brand,VehicleType,Model,Color,FuelType,CheckInTime,CustomerId,VehicleTypeListId")] ParkedVehicle parkedVehicle)
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

            var kvittoModel = new KvittoViewModel
            {
                RegistrationNumber = parkedVehicle.RegistrationNumber,
                CheckInTime = parkedVehicle.CheckInTime,
                CheckOutTime = DateTime.Now,
                ParkingTime = DateTime.Now - parkedVehicle.CheckInTime,
                Customer = parkedVehicle.Customer.LastName
            };

            db.ParkedVehicles.Remove(parkedVehicle);
            db.SaveChanges();
            //Remove Parking     
            //var removeParkingPlace = parking.GetParkingPlaceId(parkedVehicle.Id);
            //foreach (var item in removeParkingPlace)
            //{
            //    Parking removedVehicle = db.Parkings.Find(item);
            //    db.Parkings.Remove(removedVehicle);
            //}

            //db.SaveChanges();

            return RedirectToAction("Kvitto", kvittoModel);
        }


        //GetAllSpaces
        public ActionResult GaragePlacesInfo ()
        {
            var model = new ParkingInfoViewModel()
            {
                ParkingTotalSpace = parking.ParkingSize,
                ParkingAvailableSpace = parking.ParkingSize - parking.GetOccupiedParkingPlaces(),
                ParkingMotoAvailableSpace= (parking.ParkingSize - parking.GetOccupiedParkingPlaces()) * 3 + parking.GetFreeMotoPlaces()
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
