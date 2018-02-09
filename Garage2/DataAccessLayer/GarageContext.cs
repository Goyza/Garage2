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
    }
}