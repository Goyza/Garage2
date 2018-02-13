namespace Garage2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Parking : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ParkedVehicles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RegistrationNumber = c.String(nullable: false),
                        Brand = c.String(maxLength: 30),
                        VehicleType = c.String(maxLength: 30),
                        Model = c.String(maxLength: 30),
                        Color = c.String(maxLength: 30),
                        FuelType = c.String(maxLength: 30),
                        CheckInTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Parkings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ParkingPlace = c.Int(nullable: false),
                        VehicleType = c.String(),
                        ParkedVehicleId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.VehicleTypeLists",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        VehicleType = c.String(),
                        RequredSpace = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.VehicleTypeLists");
            DropTable("dbo.Parkings");
            DropTable("dbo.ParkedVehicles");
        }
    }
}
