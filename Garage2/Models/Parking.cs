using Garage2.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Garage2.Models
{
    public class Parking
    {
        private GarageContext db = new GarageContext();
        private int parkingSize = 20;
        private List<int> emptyLine;

        public int Id { get; set; }
        public int ParkingPlace { get; set; }
        //public string VehicleType { get; set; }
        [Required]
        public int ParkedVehicleId { get; set; }
        //
        public virtual ParkedVehicle parkedVehicle { get; set; }

        public int ParkingSize { get { return parkingSize; }}

        //Enum list of available places for new Vehicle
        public IEnumerable<int> GetFreeParkingPlace(int vehicleTypeId)
        {
            int vehicleTypeSize;
            emptyLine = new List<int>();

            var xxx = db.VehicleTypeLists.Where(r => r.Id == vehicleTypeId).First();

            vehicleTypeSize = xxx.RequredSpace;
            

            if (vehicleTypeSize < 0)
            {
                var motolist = db.Parkings.Where(k => k.parkedVehicle.VehicleTypeList.VehicleType.Equals("Moto")).GroupBy(p => p.ParkingPlace).Where(k => k.Count() < 3).OrderBy(x => x.Key)
                    .Select(g => new { Name = g.Key, Count = g.Count() });

                if (motolist.Count() >0)
                {
                    var firstmoto = motolist.Select(r => r.Name).First();

                        emptyLine.Add(firstmoto);
                    
                }
                else
                {
                    vehicleTypeSize= 1;
                    var tempSize = vehicleTypeSize;

                    for (int i = 1; i <= parkingSize; i++)
                    {
                        if (tempSize > 0)
                        {
                            var firstEmpty = db.Parkings.Where(r => r.ParkingPlace.Equals(i));
                            if (firstEmpty.Count() == 0)
                            {
                                emptyLine.Add(i);
                                tempSize -= 1;
                            }
                            else
                            {
                                emptyLine = new List<int>();
                                tempSize = vehicleTypeSize;
                            }
                        }
                    }
                }


            }
            else
            {
                var tempSize = vehicleTypeSize;
                for (int i = 1; i <= parkingSize; i++)
                {
                    if (tempSize > 0)
                    {
                        var firstEmpty = db.Parkings.Where(r => r.ParkingPlace.Equals(i));
                        if (firstEmpty.Count() == 0)
                        {
                            emptyLine.Add(i);
                            tempSize -= 1;
                        }
                        else
                        {
                            emptyLine = new List<int>();
                            tempSize = vehicleTypeSize;
                        }
                    }
                }
                if (tempSize>0)
                {
                    emptyLine = new List<int>();
                }

            }



            return emptyLine;
        }

        //Enum List places for VehicleID
        public IEnumerable<int> GetParkingPlaceId(int VehicleId)
        {
                return db.Parkings.Where(r => r.ParkedVehicleId.Equals(VehicleId)).Select(r => r.Id);
        }

        //Enum List places for Group
        public IEnumerable<VehicleStatGroup> GetParkingVehicles()
        {
           var model= db.ParkedVehicles.GroupBy(p => p.VehicleTypeList.VehicleType)
                            .Select(g => new VehicleStatGroup{ VehicleType = g.Key
                            , Quantity = g.Count() });

            return model;
        }

        // String places for VehicleID
        public string GetParkingPlaceString(int VehicleId)
        {
            string place="";
            var places = db.Parkings.Where(r => r.ParkedVehicleId.Equals(VehicleId)).Select(r => r.ParkingPlace);
            foreach (var item in places)
            {
                place += " "+item.ToString();
            }
            return place;
        }

        //Quntity of Occupied  standard places
        public int GetOccupiedParkingPlaces()
        {
            var parkingCount = db.Parkings.GroupBy(r => r.ParkingPlace).Select(g=> g.Key);

            return parkingCount.Count();
        }
        //Enum List af available places
        public IEnumerable<ParkingStatView> GetAllFreeParkingPlace()
        {
            for (int i = 1; i <= parkingSize; i++)
            {
                var firstEmpty = db.Parkings.Where(r => r.ParkingPlace.Equals(i));
                if (firstEmpty.Count() == 0)
                {
                    yield return new ParkingStatView() { ParkingPlace = i, PlaceInfo = "Empty" };
                }
                else
                {
                        var motolist = db.Parkings.Where(k => k.parkedVehicle.VehicleTypeList.VehicleType.Equals("Moto")).Where(r=>r.ParkingPlace==i)
                            .GroupBy(p => p.ParkingPlace)                          
                            .Select(g => new { Name = g.Key, Count = g.Count() });
                    if (motolist.Count()>0)
                    {
                        foreach (var item in motolist)
                        {
                            yield return new ParkingStatView() { ParkingPlace = i, PlaceInfo = $"Moto:{item.Count}/3" };
                        }
                   
                    }
                    else
                    yield return new ParkingStatView() { ParkingPlace = i, PlaceInfo = "Occupied" };
                }


            }
       
        }
        //Quntity of FREE moto places
        public int GetFreeMotoPlaces()
        {
            var motolist = db.Parkings.Where(k => k.parkedVehicle.VehicleTypeList.VehicleType.Equals("Moto")).GroupBy(p => p.ParkingPlace).Where(k => k.Count() < 3).OrderBy(x => x.Key)
                .Select(g => new { Name = g.Key, Count = 3-g.Count() });

            if (motolist.Count()==0)
            {
                return 0;
            }
            else
            { 
            return motolist.Sum(r=>r.Count);
            }
        }

        public string statstring()
        {
            var outstring = "";
            foreach (var item in GetAllFreeParkingPlace())
            {
                outstring += " " + item.ParkingPlace + ":" + item.PlaceInfo;
            }

            return outstring;
        }

        // String places for VehicleID
        public static string GetParkingPlaceStatic(int VehicleId)
        {
            GarageContext dbstat = new GarageContext();
            string place = "";
            var places = dbstat.Parkings.Where(r => r.ParkedVehicleId.Equals(VehicleId)).Select(r => r.ParkingPlace);
            foreach (var item in places)
            {
                place += " " + item.ToString();
            }
            return place;
        }

    }

}