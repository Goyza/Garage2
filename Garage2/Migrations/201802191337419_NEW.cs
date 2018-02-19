namespace Garage2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NEW : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ParkedVehicleViewModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RegistrationNumber = c.String(),
                        VehicleType = c.String(),
                        CheckInTime = c.DateTime(nullable: false),
                        Customer = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ParkedVehicleViewModels");
        }
    }
}
