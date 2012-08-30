namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MrKupido_20120830a : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ImportedRecipes", "OriginalDirections", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ImportedRecipes", "OriginalDirections");
        }
    }
}
