namespace Garage2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ö : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.ParkedVehicleViewModels", "Customer");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ParkedVehicleViewModels", "Customer", c => c.String());
        }
    }
}
