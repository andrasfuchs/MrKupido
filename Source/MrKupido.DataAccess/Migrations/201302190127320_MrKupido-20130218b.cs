namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MrKupido20130218b : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "GoogleId", c => c.String(maxLength: 50));
            DropColumn("dbo.Users", "GoolgeId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "GoolgeId", c => c.String(maxLength: 50));
            DropColumn("dbo.Users", "GoogleId");
        }
    }
}
