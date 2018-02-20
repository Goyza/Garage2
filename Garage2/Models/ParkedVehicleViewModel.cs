using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Garage2.Models
{
    public class ParkedVehicleViewModel
    {
        private Parking parking = new Parking();
        //ParkedVehicle class contains list of available vehicle 
        public int Id { get; set; }
            public string RegistrationNumber { get; set; }
            public string VehicleType { get; set; }
            public DateTime CheckInTime { get; set; }
            [DisplayFormat(DataFormatString = "{0:%d}d {0:%h}h {0:%m}m", ApplyFormatInEditMode = true)]
            public TimeSpan ParkingTime { get { return DateTime.Now-CheckInTime; } }
            public string ParkingPlace { get { return parking.GetParkingPlaceString(Id); } }
            public string Customer { get; set; }
           
    }
}