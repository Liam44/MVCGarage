namespace MVCGarage.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CheckIns : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CheckIns", "CheckOutTime", c => c.DateTime());
            AddColumn("dbo.CheckIns", "TotalAmount", c => c.Double());
            DropColumn("dbo.CheckIns", "Free");
        }
        
        public override void Down()
        {
            AddColumn("dbo.CheckIns", "Free", c => c.Boolean(nullable: false));
            DropColumn("dbo.CheckIns", "TotalAmount");
            DropColumn("dbo.CheckIns", "CheckOutTime");
        }
    }
}
