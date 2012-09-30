namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MrKupido_20120928a : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.FilterItems", "ExpirationTime");
            DropColumn("dbo.FilterItems", "StorageTemperature");
            DropColumn("dbo.FilterItems", "GlichemicalIndex");
            DropColumn("dbo.FilterItems", "PotencialAlkalinity");
            DropColumn("dbo.FilterItems", "GrammsPerLiter");
            DropColumn("dbo.FilterItems", "GrammsPerPiece");
            DropColumn("dbo.FilterItems", "KCaloriesPerGramm");
        }
        
        public override void Down()
        {
            AddColumn("dbo.FilterItems", "KCaloriesPerGramm", c => c.Single());
            AddColumn("dbo.FilterItems", "GrammsPerPiece", c => c.Single());
            AddColumn("dbo.FilterItems", "GrammsPerLiter", c => c.Single());
            AddColumn("dbo.FilterItems", "PotencialAlkalinity", c => c.Single());
            AddColumn("dbo.FilterItems", "GlichemicalIndex", c => c.Int());
            AddColumn("dbo.FilterItems", "StorageTemperature", c => c.Single());
            AddColumn("dbo.FilterItems", "ExpirationTime", c => c.Int());
        }
    }
}
