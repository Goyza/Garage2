using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Garage2.Models
{
    public class ParkingInfoViewModel
    {
        public int ParkingTotalSpace { get; set; }
        public int ParkingAvailableSpace  { get; set; }
        public int ParkingMotoAvailableSpace { get; set; }
    }
}