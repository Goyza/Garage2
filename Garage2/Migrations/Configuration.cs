namespace Garage2.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Garage2.DataAccessLayer.GarageContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Garage2.DataAccessLayer.GarageContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
    //        context.ParkedVehicles.AddOrUpdate(n => n.RegistrationNumber,
    //            new Models.ParkedVehicle { Brand = "Volvo", Color = "Red", RegistrationNumber = "AAA666", VehicleType = "Sedan", FuelType = "Diesel", Model = "C4", CheckInTime = DateTime.Now },
    //            new Models.ParkedVehicle { Brand = "Ford", Color = "Blue", RegistrationNumber = "BBB777", VehicleType = "Sedan", FuelType = "Diesel", Model = "A1", CheckInTime = DateTime.Now }
    //);
        }
    }
}
