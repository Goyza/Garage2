using Garage2.DataAccessLayer;
using System;
using System.Collections.Generic;
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
        public string VehicleType { get; set; }
        public int ParkedVehicleId { get; set; }
        public int ParkingSize { get { return parkingSize; }}

        public IEnumerable<int> GetFreeParkingPlace(string vehicleType)
        {
            int vehicleTypeSize;
            emptyLine = new List<int>();

            switch (vehicleType)
            {
                case "Car":
                    vehicleTypeSize = 1;
                    break;
                case "Bus":
                    vehicleTypeSize = 2;
                    break;
                case "Boat":
                    vehicleTypeSize = 3;
                    break;
                default:
                    vehicleTypeSize = 1;
                    break;
            }

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

            return emptyLine;
        }

        public IEnumerable<int> GetParkingPlaceId(int VehicleId)
        {
                return db.Parkings.Where(r => r.ParkedVehicleId.Equals(VehicleId)).Select(r => r.Id);
        }

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


        public int GetOccupiedParkingPlaces()
        {
            var parkingCount = db.Parkings.GroupBy(r => r.ParkingPlace).Select(g=> g.Key);

            return parkingCount.Count();
        }

    }

}