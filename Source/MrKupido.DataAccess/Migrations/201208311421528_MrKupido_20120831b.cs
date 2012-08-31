namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MrKupido_20120831b : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.FilterItems", "GrammsPerLiter", c => c.Single());
            AddColumn("dbo.FilterItems", "GrammsPerPiece", c => c.Single());
            AddColumn("dbo.FilterItems", "KCaloriesPerGramm", c => c.Single());
        }
        
        public override void Down()
        {
            DropColumn("dbo.FilterItems", "KCaloriesPerGramm");
            DropColumn("dbo.FilterItems", "GrammsPerPiece");
            DropColumn("dbo.FilterItems", "GrammsPerLiter");
        }
    }
}
