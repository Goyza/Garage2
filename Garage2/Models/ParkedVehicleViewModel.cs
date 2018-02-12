using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Garage2.Models
{
    public class ParkedVehicleViewModel
    {
            //ParkedVehicle class contains list of available vehicle 
            public int Id { get; set; }
            public string RegistrationNumber { get; set; }
            public string VehicleType { get; set; }
            public DateTime CheckInTime { get; set; }
            public TimeSpan ParkingTime { get { return DateTime.Now-CheckInTime; } }

    }
}