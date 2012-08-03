namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MrKupido_20120803b : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.FilterItems", "PotencialAlkalinity", c => c.Single());
        }
        
        public override void Down()
        {
            DropColumn("dbo.FilterItems", "PotencialAlkalinity");
        }
    }
}
