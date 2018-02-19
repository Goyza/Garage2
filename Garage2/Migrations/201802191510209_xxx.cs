namespace Garage2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class xxx : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        PersonalIdentityNumber = c.Int(nullable: false),
                        TelephoNnumber = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ParkedVehicles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RegistrationNumber = c.String(nullable: false),
                        Brand = c.String(maxLength: 30),
                        Model = c.String(maxLength: 30),
                        Color = c.String(maxLength: 30),
                        FuelType = c.String(maxLength: 30),
                        CheckInTime = c.DateTime(nullable: false),
                        CustomerId = c.Int(nullable: false),
                        VehicleTypeListId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Customers", t => t.CustomerId, cascadeDelete: true)
                .ForeignKey("dbo.VehicleTypeLists", t => t.VehicleTypeListId, cascadeDelete: true)
                .Index(t => t.CustomerId)
                .Index(t => t.VehicleTypeListId);
            
            CreateTable(
                "dbo.Parkings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ParkingPlace = c.Int(nullable: false),
                        ParkedVehicleId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ParkedVehicles", t => t.ParkedVehicleId, cascadeDelete: true)
                .Index(t => t.ParkedVehicleId);
            
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
            DropForeignKey("dbo.ParkedVehicles", "VehicleTypeListId", "dbo.VehicleTypeLists");
            DropForeignKey("dbo.Parkings", "ParkedVehicleId", "dbo.ParkedVehicles");
            DropForeignKey("dbo.ParkedVehicles", "CustomerId", "dbo.Customers");
            DropIndex("dbo.Parkings", new[] { "ParkedVehicleId" });
            DropIndex("dbo.ParkedVehicles", new[] { "VehicleTypeListId" });
            DropIndex("dbo.ParkedVehicles", new[] { "CustomerId" });
            DropTable("dbo.VehicleTypeLists");
            DropTable("dbo.Parkings");
            DropTable("dbo.ParkedVehicles");
            DropTable("dbo.Customers");
        }
    }
}
