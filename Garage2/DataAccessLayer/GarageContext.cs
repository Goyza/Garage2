using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Garage2.DataAccessLayer
{
    public class GarageContext: DbContext
    {
        public GarageContext() : base("name=DefaultConnection")
        {

        }

        public DbSet<Models.ParkedVehicle> ParkedVehicles { get; set; }
        public DbSet<Models.Parking> Parkings { get; set; }
        public DbSet<Models.VehicleTypeList> VehicleTypeLists  { get; set; }
        public DbSet<Models.Customer> Customers { get; set; }
    }
}