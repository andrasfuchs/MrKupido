namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MrKupido_20120802a : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.FilterItems", "GlichemicalIndex", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.FilterItems", "GlichemicalIndex");
        }
    }
}
