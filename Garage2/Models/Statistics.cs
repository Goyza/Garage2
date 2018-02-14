using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Garage2.Models
{
    public class Statistics
    {
        public int NumberOfVehicles { get; set; }
        public int NumbeOfCars { get; set; }
        public int NumberOfBuses { get; set; }
        public int NumberOfBoats { get; set; }
        public int NumberOfUndefined { get; set; }
        [DisplayFormat(DataFormatString = ("{0:%d}d {0:%h}h {0:%m}m"), ApplyFormatInEditMode = true)]
        public TimeSpan TotParkingTime { get; set; }
        public Decimal Revenue { get; set; }
    }
}