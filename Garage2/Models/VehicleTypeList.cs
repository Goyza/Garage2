using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Garage2.Models
{
    public class VehicleTypeList
    {
        public int Id { get; set; }
        public string VehicleType { get; set; }
        public int RequredSpace { get; set; }
        public virtual ICollection<ParkedVehicle> parkedVehicle { get; set; }
    }
}