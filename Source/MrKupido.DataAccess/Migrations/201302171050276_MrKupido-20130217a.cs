namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MrKupido20130217a : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "GoolgeId", c => c.String(maxLength: 50));
            AddColumn("dbo.Users", "FacebookId", c => c.String(maxLength: 50));
            AddColumn("dbo.Users", "WindowsLiveId", c => c.String(maxLength: 50));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "WindowsLiveId");
            DropColumn("dbo.Users", "FacebookId");
            DropColumn("dbo.Users", "GoolgeId");
        }
    }
}
