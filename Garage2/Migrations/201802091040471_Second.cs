namespace Garage2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Second : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ParkedVehicles", "VehicleType", c => c.String(maxLength: 30));
            DropColumn("dbo.ParkedVehicles", "Type");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ParkedVehicles", "Type", c => c.String(maxLength: 30));
            DropColumn("dbo.ParkedVehicles", "VehicleType");
        }
    }
}
